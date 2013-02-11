<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewCust
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewCust))
        Me.txtNewPhone = New System.Windows.Forms.TextBox()
        Me.txtNewName = New System.Windows.Forms.TextBox()
        Me.lblNewPhone = New System.Windows.Forms.Label()
        Me.lblNewName = New System.Windows.Forms.Label()
        Me.cboCustList = New System.Windows.Forms.ComboBox()
        Me.lblNewCust = New System.Windows.Forms.Label()
        Me.chkNewCust = New System.Windows.Forms.CheckBox()
        Me.txtNewCust = New System.Windows.Forms.TextBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtNewPhone
        '
        Me.txtNewPhone.Location = New System.Drawing.Point(72, 34)
        Me.txtNewPhone.Name = "txtNewPhone"
        Me.txtNewPhone.Size = New System.Drawing.Size(166, 20)
        Me.txtNewPhone.TabIndex = 2
        '
        'txtNewName
        '
        Me.txtNewName.Location = New System.Drawing.Point(72, 8)
        Me.txtNewName.Name = "txtNewName"
        Me.txtNewName.Size = New System.Drawing.Size(166, 20)
        Me.txtNewName.TabIndex = 0
        '
        'lblNewPhone
        '
        Me.lblNewPhone.AutoSize = True
        Me.lblNewPhone.Location = New System.Drawing.Point(9, 37)
        Me.lblNewPhone.Name = "lblNewPhone"
        Me.lblNewPhone.Size = New System.Drawing.Size(38, 13)
        Me.lblNewPhone.TabIndex = 3
        Me.lblNewPhone.Text = "Phone"
        '
        'lblNewName
        '
        Me.lblNewName.AutoSize = True
        Me.lblNewName.Location = New System.Drawing.Point(9, 11)
        Me.lblNewName.Name = "lblNewName"
        Me.lblNewName.Size = New System.Drawing.Size(35, 13)
        Me.lblNewName.TabIndex = 4
        Me.lblNewName.Text = "Name"
        '
        'cboCustList
        '
        Me.cboCustList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustList.FormattingEnabled = True
        Me.cboCustList.Location = New System.Drawing.Point(72, 60)
        Me.cboCustList.Name = "cboCustList"
        Me.cboCustList.Size = New System.Drawing.Size(166, 21)
        Me.cboCustList.TabIndex = 5
        '
        'lblNewCust
        '
        Me.lblNewCust.AutoSize = True
        Me.lblNewCust.Location = New System.Drawing.Point(9, 63)
        Me.lblNewCust.Name = "lblNewCust"
        Me.lblNewCust.Size = New System.Drawing.Size(51, 13)
        Me.lblNewCust.TabIndex = 6
        Me.lblNewCust.Text = "Customer"
        '
        'chkNewCust
        '
        Me.chkNewCust.AutoSize = True
        Me.chkNewCust.Location = New System.Drawing.Point(137, 90)
        Me.chkNewCust.Name = "chkNewCust"
        Me.chkNewCust.Size = New System.Drawing.Size(101, 17)
        Me.chkNewCust.TabIndex = 7
        Me.chkNewCust.Text = "New Customer?"
        Me.chkNewCust.UseVisualStyleBackColor = True
        '
        'txtNewCust
        '
        Me.txtNewCust.Enabled = False
        Me.txtNewCust.Location = New System.Drawing.Point(72, 60)
        Me.txtNewCust.Name = "txtNewCust"
        Me.txtNewCust.Size = New System.Drawing.Size(166, 20)
        Me.txtNewCust.TabIndex = 8
        Me.txtNewCust.Visible = False
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(122, 113)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(55, 22)
        Me.cmdSave.TabIndex = 9
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.Location = New System.Drawing.Point(183, 113)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(55, 22)
        Me.cmdExit.TabIndex = 10
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'NewCust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(242, 141)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.txtNewCust)
        Me.Controls.Add(Me.chkNewCust)
        Me.Controls.Add(Me.lblNewCust)
        Me.Controls.Add(Me.cboCustList)
        Me.Controls.Add(Me.lblNewName)
        Me.Controls.Add(Me.lblNewPhone)
        Me.Controls.Add(Me.txtNewName)
        Me.Controls.Add(Me.txtNewPhone)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewCust"
        Me.Text = "New Contact"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNewPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtNewName As System.Windows.Forms.TextBox
    Friend WithEvents lblNewPhone As System.Windows.Forms.Label
    Friend WithEvents lblNewName As System.Windows.Forms.Label
    Friend WithEvents cboCustList As System.Windows.Forms.ComboBox
    Friend WithEvents lblNewCust As System.Windows.Forms.Label
    Friend WithEvents chkNewCust As System.Windows.Forms.CheckBox
    Friend WithEvents txtNewCust As System.Windows.Forms.TextBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
End Class
