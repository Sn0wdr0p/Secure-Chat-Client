' These are the libaries I will be using
Imports System.Net.Sockets
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Imports System.ComponentModel

Public Class Client
    'Initialises variables that will be nessasary for connecting to the server and receving data
    Dim clientSocket As New System.Net.Sockets.TcpClient ' This sets up the location that the client will use to connect to the server
    Dim serverStream As NetworkStream ' The stream from the server
    Dim readData As String ' What is recived from the server stream
    Dim key() As Byte ' This will hold the secret key
    Dim myPublicKey() As Byte ' This will hold this clients public key
    Dim disconnectFlag As Boolean = Nothing ' This will indicate if a disconnect message has been sent
    Dim connected As Boolean = False ' This will indicate if the client is connected or not
    Dim msgTooBig As Boolean = False ' This will be used to restrict the error messges on if the message is too big or not

    ' This routine will be called when the user presses the 'send' button
    Private Sub Send(sender As System.Object, e As System.EventArgs) Handles Send_Btn.Click
        ' Gets and holds the encrypted message/initialization vector(IV) that will be sent
        Dim tempArray() As Array
        tempArray = encryptMsg(Msg_Box.Text + "$")

        ' Sends the IV
        serverStream.Write(tempArray(1), 0, tempArray(1).Length)
        serverStream.Flush() ' This clears the stream of data

        ' Sends the encrypted message
        serverStream.Write(tempArray(0), 0, tempArray(0).Length)
        serverStream.Flush() ' This clears the stream of data

        ' This clears the message input field (for user experience)
        Msg_Box.Text = ""
    End Sub

    ' This routine prints the data from the 'readData' variable to the chat log
    Private Sub msg()
        ' .InvokeRequired is used to check where the method was called form (if it was on a different thread in the CPU)
        If Me.InvokeRequired Then
            ' If it is an invoke method is called to fix it
            Me.Invoke(New MethodInvoker(AddressOf msg))
        Else
            ' Prints the data to the chat log on a new line
            Chat_Log.Text = Chat_Log.Text + Environment.NewLine + " >> " + readData

            ' Auto scrolls to the bottom of the chat log
            Chat_Log.SelectionStart = Chat_Log.Text.Length
            Chat_Log.ScrollToCaret()
        End If
    End Sub

    ' Connects to the server. This is called when the user presses the 'connect to server' button
    Private Sub Connect_to_Server(sender As System.Object, e As System.EventArgs) Handles Connect_Btn.Click
        ' Sets up the progress bar
        connectProg.Minimum = 0
        connectProg.Maximum = 70

        Try
            ' Connects to the inputed IP address on the inputted port number
            clientSocket.Connect(System.Net.IPAddress.Parse("127.0.0.1"), 8888) ' The IP 127.0.0.1 is the same on every machine. It refers to the current machine. I chose the port 8888 because it is rarely used.
            connectProg.Value = 10 ' Updates the progress bar
        Catch
            MsgBox("There seems to be no client available. Please make sure a server is open and try again.")
            connectProg.Value = 0
            Exit Sub
        End Try

        ' Sets up the server stream to be the connected socket
        serverStream = clientSocket.GetStream()
        connectProg.Value = 20 ' Updates the progress bar

        ' Holds the public key from the server when its sent
        Dim serverKey() As Byte

        'Try

        ' This manages the set up of the Eliptical Curve Diffie-Hellman protocal
        Using ECDH As New ECDiffieHellmanCng()
            ' Sets the key derivation function for the key exchange
            ' TODO: add more info on this
            ECDH.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash
            connectProg.Value = 30 ' Updates the progress bar

            ' Sets the key hash algorithm for the key exchange
            ' TODO: add more info on this
            ECDH.HashAlgorithm = CngAlgorithm.Sha256
            connectProg.Value = 40 ' Updates the progress bar

            ' Gets the public key from this client so it can be sent
            myPublicKey = ECDH.PublicKey.ToByteArray()
            connectProg.Value = 50 ' Updates the progress bar

            ' Sends the public key to the server
            sendKey()
            connectProg.Value = 60 ' Updates the progress bar

            ' Listens for the public key from the server
            serverKey = listenForKey()

            Dim tempkeyhelpmeplz = CngKey.Import(serverKey, CngKeyBlobFormat.EccPublicBlob)

            ' Sets the public key based on the public keys of the server and this client
            ' according to the derivation function the hash algorithm that was previously specified
            key = ECDH.DeriveKeyMaterial(tempkeyhelpmeplz)
        End Using
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        connectProg.Value = 70 ' Updates the progress bar

        ' Prints that the client has connected to the server
        readData = "Conected to Chat Server ..."
        msg()

        ' Encrypts the name the user inputed with the secret key
        Dim nameOut As Array = encryptMsg(Name_Box.Text + "$")

        ' Sends the IV
        serverStream.Write(nameOut(1), 0, nameOut(1).Length)
        serverStream.Flush() ' This clears the stream of data

        ' Sends the encrypted message
        serverStream.Write(nameOut(0), 0, nameOut(0).Length)
        serverStream.Flush() ' This clears the stream of data

        ' Prints the backend if the option has been selected
        If debug.Checked = True Then
            MsgBox("The secret key is: " + Encoding.Unicode.GetString(key))
        End If

        ' Enables the message box now that the client has fully connected
        Msg_Box.Enabled = True

        ' Enables the disconnect button
        disconnect.Enabled = True

        'Indicates that the client is connected
        connected = True

        ' Locks the controls
        Name_Box.Enabled = False
        Connect_Btn.Enabled = False

        ' Starts a new thread in the CPU to recieve messages
        Dim ctThread As Threading.Thread = New Threading.Thread(AddressOf getMessage)
        ctThread.Start()
    End Sub

    ' This is created in its own thread in the CPU to always recive messages in the background
    Private Sub getMessage()
        Try
            ' Starts an infinate loop that will recive messages
            While (True)
                ' Recives the IV and assigns it to a variable
                Dim inIVStream(15) As Byte ' This is 16 bytes/128 bits
                serverStream.Read(inIVStream, 0, inIVStream.Length)
                Dim iv() As Byte = inIVStream
                serverStream.Flush() ' This clears the stream of data

                ' Recives the encrypted message and assigns it to a variable
                Dim inStream(1023) As Byte ' This is 1MB (Mega byte)
                serverStream.Read(inStream, 0, inStream.Length)
                Dim cMessage() As Byte = inStream
                serverStream.Flush() ' This clears the stream of data

                If debug.Checked = True Then
                    MsgBox("Ciphertext Recived: " & Encoding.Unicode.GetString(cMessage))
                End If


                ' Unencrypts the message and assigns it to the global 'readData' variable so it can be printed
                readData = unencryptMsg(cMessage, iv)

                ' Prints the read data to the chat log
                msg()
            End While
        Catch
            Application.Exit()
        End Try
    End Sub

    ' Used to just get the public key from the server
    Private Function listenForKey()
        Dim thing = False
        Dim clientPublicKey As Byte()
        'While (thing = True)
        ' Variable that will hold the key
        Dim inStream(139) As Byte

        ' Reads the key from the server stream
        serverStream.Read(inStream, 0, inStream.Length)

        ' Returns the key to the variable this function was called from
        clientPublicKey = inStream
        'End While
        Return clientPublicKey
    End Function

    ' Sends this clients public key
    Private Sub sendKey()
        ' Assigns the key to a temparary variable and writes it to the server stream
        Dim outStream() As Byte = myPublicKey
        If debug.Checked = True Then
            MsgBox("Public key length: " & myPublicKey.Length)
        End If
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush() ' This clears the stream of data
    End Sub

    ' Encrypts the given message with the secret key
    Private Function encryptMsg(ByVal msg As String)
        Try
            ' Sets up a AES symetric encryption provider that can perform AES encryption and decryption
            Using aes As New AesCryptoServiceProvider()
                ' Assigns the AES key to the secret key gained from the Elliptical Curve Diffie-Hellman protocal
                aes.Key = key

                'Gets the IV generated by the AES provider and assigns it to a temparary variable
                Dim iv = aes.IV

                ' Sets up the Padding mode for the encryption. This adds data to the message so it creates a full block of data (1 block is 16 bytes)
                ' this is so it can be encrypted and decrypted
                aes.Padding = PaddingMode.PKCS7

                ' This sets the mode by which the plaintext will be encrypted
                ' This mode uses feedback to make every encrypted block individual
                aes.Mode = CipherMode.CBC

                ' Begins encrypting the message
                Using ciphertext As New MemoryStream() ' Uses memory as a backing store

                    ' Creates a stream that links streams to cryptographic tranformations
                    ' In this case the memory stream 'ciphertext' is linked to the AES Crypto Provider 'aes'
                    Using cs As New CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write)

                        ' Converts the message to somthing that can be encrypted and sent (string to byte array)
                        Dim plaintextMessage As Byte() = Encoding.Unicode.GetBytes(msg)

                        ' Encrypts what is in 'plaintextMessage' to an encrypted array of bytes that is written to/stored in the memory stream 'ciphertext'
                        cs.Write(plaintextMessage, 0, plaintextMessage.Length)

                        ' Closes the current stream, releases resources and finalises the encryption process
                        cs.Close()

                        ' Creates an array that will hold the encrypted message and the IV for that message
                        Dim msgArray(2) As Array
                        msgArray(0) = ciphertext.ToArray()
                        msgArray(1) = iv

                        ' Prints the backend if the option has been selected
                        If debug.Checked = True Then
                            MsgBox("Initialization Vector: " + Encoding.Unicode.GetString(iv))
                            MsgBox("Ciphertext: " + Encoding.Unicode.GetString(ciphertext.ToArray()))
                        End If

                        ' Returns the message array with the IV and encrypted message
                        Return msgArray
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Function

    ' Unencrypts the given message with the given IV and the secret key
    Private Function unencryptMsg(ByVal cMessage() As Byte, ByVal iv() As Byte)
        ' Sets up a AES symetric encryption provider that can perform AES encryption and decryption
        Using aes As New AesCryptoServiceProvider()
            ' Assigns the AES key to the secret key gained from the Elliptical Curve Diffie-Hellman protocal
            aes.Key = key

            ' Sets the IV of the AES provider to the recived IV
            ' This is done because IV's are unique to a encrypted message and must be the same for encryption and decryption
            aes.IV = iv

            ' Sets up the Padding mode for the decryption
            aes.Padding = PaddingMode.None

            ' This sets the mode by which the plaintext will be decrypted
            aes.Mode = CipherMode.CBC

            ' Begins decrypting the message
            Using plaintext As New MemoryStream() ' Uses memory as a backing store

                ' Creates a stream that links streams to cryptographic tranformations
                ' In this case the memory stream 'ciphertext' is linked to the AES Crypto Provider 'aes'
                Using cs As New CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write)
                    ' Decrypts what is in 'cMessage' to an array of bytes that is written to/stored in the memory stream 'plaintext'
                    cs.Write(cMessage, 0, cMessage.Length)

                    ' Closes the current stream, releases resources and finalises the decryption process
                    cs.Close()

                    ' Creates a variable that will hold the unencrypted string
                    Dim message As String = Encoding.Unicode.GetString(plaintext.ToArray())
                    message = message.Substring(0, message.IndexOf("$"))

                    ' Returns the plaintext message
                    Return message
                End Using
            End Using
        End Using
    End Function

    ' Routine called when the text in the name field is changed
    Private Sub Name_Box_TextChanged(sender As Object, e As EventArgs) Handles Name_Box.TextChanged
        ' If there is nothing in the field then the connect button is disabled
        If Not Name_Box.Text = "" Then
            Connect_Btn.Enabled = True
        Else
            Connect_Btn.Enabled = False
        End If

        If Name_Box.TextLength > 9 Then
            MsgBox("Names can not be longer than 9 characters")
            Name_Box.Text = ""
        End If

    End Sub

    ' Routine called when the text in the message field is changed
    Private Sub Msg_Box_TextChanged(sender As System.Object, e As System.EventArgs) Handles Msg_Box.TextChanged
        ' If the dollar sign is used it is removed
        If Msg_Box.Text.Contains("$") Then
            ' Messages the user to inform them that it is a reserved character
            MsgBox("I am afraid that the '$' character is reserved and can not be used.")
            Msg_Box.Text = Msg_Box.Text.Replace("$", "")
        End If

        ' If the message exceeds a size, send button is disabled and a message is given out
        If Encoding.Unicode.GetByteCount(Msg_Box.Text) > 1000 And msgTooBig = False Then
            MsgBox("I am afraid messages are limited to 1000 Bytes (about 500 characters)")
            Send_Btn.Enabled = False
            msgSize.ForeColor = Color.Red
            msgTooBig = True
        ElseIf Encoding.Unicode.GetByteCount(Msg_Box.Text) < 1000 And msgTooBig = True Then
            Send_Btn.Enabled = True
            msgSize.ForeColor = Color.Black
            msgTooBig = False
        End If


        ' If there is no text in the message field: the send button is disabled
        If Msg_Box.Text = "" Then
            Send_Btn.Enabled = False
        ElseIf msgTooBig = False And Not (Msg_Box.Text = "") Then
            Send_Btn.Enabled = True
        End If

        msgSize.Text = "Current message size: " & Encoding.Unicode.GetByteCount(Msg_Box.Text) & " Bytes/1000 Bytes"
    End Sub

    ' This routine is called when the window loads up
    Private Sub Client_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Makes the chat log read only so users can not modify the contents
        Chat_Log.ReadOnly = True
    End Sub

    ' Disconnects the client from the server whent the 'Disconnect and Close' button is pressed
    Private Sub disconnect_Click(sender As Object, e As EventArgs) Handles disconnect.Click, Me.Closing
        ' Checks if a disconnect message hasbeen sent before
        If (connected = False) Then ' if the value is unassigned we havent even connected
            Application.Exit()
        End If
        If (disconnectFlag = False And connected = True) Then
            disconnectFlag = True ' changes state to indicate the message has been sent
            ' Fills the message box with a message that the this client has disconnected and then sends it
            Msg_Box.Text = "has left the chat-room"
            Send_Btn.PerformClick()

            MsgBox("Disconnected from server")
            ' Exits the application
            Application.Exit()
        End If
    End Sub
End Class
