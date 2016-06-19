<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Client
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Send_Btn = New System.Windows.Forms.Button()
        Me.Msg_Box = New System.Windows.Forms.TextBox()
        Me.Chat_Log = New System.Windows.Forms.TextBox()
        Me.Connect_Btn = New System.Windows.Forms.Button()
        Me.Name_Box = New System.Windows.Forms.TextBox()
        Me.connectProg = New System.Windows.Forms.ProgressBar()
        Me.debug = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.disconnect = New System.Windows.Forms.Button()
        Me.msgSize = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Send_Btn
        '
        Me.Send_Btn.Enabled = False
        Me.Send_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Send_Btn.Location = New System.Drawing.Point(485, 152)
        Me.Send_Btn.Name = "Send_Btn"
        Me.Send_Btn.Size = New System.Drawing.Size(132, 23)
        Me.Send_Btn.TabIndex = 0
        Me.Send_Btn.Text = "Send Button"
        Me.Send_Btn.UseVisualStyleBackColor = True
        '
        'Msg_Box
        '
        Me.Msg_Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Msg_Box.Enabled = False
        Me.Msg_Box.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Msg_Box.Location = New System.Drawing.Point(12, 62)
        Me.Msg_Box.Multiline = True
        Me.Msg_Box.Name = "Msg_Box"
        Me.Msg_Box.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Msg_Box.Size = New System.Drawing.Size(605, 84)
        Me.Msg_Box.TabIndex = 0
        '
        'Chat_Log
        '
        Me.Chat_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Chat_Log.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chat_Log.Location = New System.Drawing.Point(12, 181)
        Me.Chat_Log.Multiline = True
        Me.Chat_Log.Name = "Chat_Log"
        Me.Chat_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Chat_Log.Size = New System.Drawing.Size(605, 365)
        Me.Chat_Log.TabIndex = 10
        '
        'Connect_Btn
        '
        Me.Connect_Btn.Enabled = False
        Me.Connect_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Connect_Btn.Location = New System.Drawing.Point(485, 12)
        Me.Connect_Btn.Name = "Connect_Btn"
        Me.Connect_Btn.Size = New System.Drawing.Size(132, 23)
        Me.Connect_Btn.TabIndex = 0
        Me.Connect_Btn.Text = "Connect Button"
        Me.Connect_Btn.UseVisualStyleBackColor = True
        '
        'Name_Box
        '
        Me.Name_Box.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name_Box.Location = New System.Drawing.Point(56, 14)
        Me.Name_Box.Name = "Name_Box"
        Me.Name_Box.Size = New System.Drawing.Size(122, 21)
        Me.Name_Box.TabIndex = 0
        '
        'connectProg
        '
        Me.connectProg.Location = New System.Drawing.Point(12, 41)
        Me.connectProg.Name = "connectProg"
        Me.connectProg.Size = New System.Drawing.Size(605, 15)
        Me.connectProg.TabIndex = 5
        '
        'debug
        '
        Me.debug.AutoSize = True
        Me.debug.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.debug.Location = New System.Drawing.Point(184, 16)
        Me.debug.Name = "debug"
        Me.debug.Size = New System.Drawing.Size(173, 19)
        Me.debug.TabIndex = 7
        Me.debug.Text = "Show behind the scences?"
        Me.debug.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 15)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Name:"
        '
        'disconnect
        '
        Me.disconnect.Enabled = False
        Me.disconnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.disconnect.Location = New System.Drawing.Point(12, 152)
        Me.disconnect.Name = "disconnect"
        Me.disconnect.Size = New System.Drawing.Size(145, 23)
        Me.disconnect.TabIndex = 10
        Me.disconnect.Text = "Disconnect and Close"
        Me.disconnect.UseVisualStyleBackColor = True
        '
        'msgSize
        '
        Me.msgSize.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.msgSize.AutoSize = True
        Me.msgSize.Location = New System.Drawing.Point(274, 158)
        Me.msgSize.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.msgSize.Name = "msgSize"
        Me.msgSize.Size = New System.Drawing.Size(206, 13)
        Me.msgSize.TabIndex = 10
        Me.msgSize.Text = "Current message size: 0 Bytes/1000 Bytes"
        '
        'Client
        '
        Me.AcceptButton = Me.Send_Btn
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 558)
        Me.Controls.Add(Me.msgSize)
        Me.Controls.Add(Me.disconnect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.debug)
        Me.Controls.Add(Me.connectProg)
        Me.Controls.Add(Me.Name_Box)
        Me.Controls.Add(Me.Connect_Btn)
        Me.Controls.Add(Me.Chat_Log)
        Me.Controls.Add(Me.Msg_Box)
        Me.Controls.Add(Me.Send_Btn)
        Me.Name = "Client"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Chatroom Client"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Send_Btn As System.Windows.Forms.Button
    Friend WithEvents Msg_Box As System.Windows.Forms.TextBox
    Friend WithEvents Chat_Log As System.Windows.Forms.TextBox
    Friend WithEvents Connect_Btn As System.Windows.Forms.Button
    Friend WithEvents Name_Box As System.Windows.Forms.TextBox
    Friend WithEvents connectProg As System.Windows.Forms.ProgressBar
    Friend WithEvents debug As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents disconnect As System.Windows.Forms.Button
    Friend WithEvents msgSize As Label
End Class
