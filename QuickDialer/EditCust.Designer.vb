<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditCust
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditCust))
        Me.lblNewName = New System.Windows.Forms.Label()
        Me.lblNewPhone = New System.Windows.Forms.Label()
        Me.txtEditName = New System.Windows.Forms.TextBox()
        Me.txtEditPhone = New System.Windows.Forms.TextBox()
        Me.cmdExitEdit = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblNewName
        '
        Me.lblNewName.AutoSize = True
        Me.lblNewName.Location = New System.Drawing.Point(12, 9)
        Me.lblNewName.Name = "lblNewName"
        Me.lblNewName.Size = New System.Drawing.Size(35, 13)
        Me.lblNewName.TabIndex = 8
        Me.lblNewName.Text = "Name"
        '
        'lblNewPhone
        '
        Me.lblNewPhone.AutoSize = True
        Me.lblNewPhone.Location = New System.Drawing.Point(12, 35)
        Me.lblNewPhone.Name = "lblNewPhone"
        Me.lblNewPhone.Size = New System.Drawing.Size(38, 13)
        Me.lblNewPhone.TabIndex = 7
        Me.lblNewPhone.Text = "Phone"
        '
        'txtEditName
        '
        Me.txtEditName.Location = New System.Drawing.Point(75, 6)
        Me.txtEditName.Name = "txtEditName"
        Me.txtEditName.Size = New System.Drawing.Size(166, 20)
        Me.txtEditName.TabIndex = 5
        '
        'txtEditPhone
        '
        Me.txtEditPhone.Location = New System.Drawing.Point(75, 32)
        Me.txtEditPhone.Name = "txtEditPhone"
        Me.txtEditPhone.Size = New System.Drawing.Size(166, 20)
        Me.txtEditPhone.TabIndex = 6
        '
        'cmdExitEdit
        '
        Me.cmdExitEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExitEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExitEdit.Location = New System.Drawing.Point(186, 58)
        Me.cmdExitEdit.Name = "cmdExitEdit"
        Me.cmdExitEdit.Size = New System.Drawing.Size(55, 24)
        Me.cmdExitEdit.TabIndex = 12
        Me.cmdExitEdit.Text = "Exit"
        Me.cmdExitEdit.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.Location = New System.Drawing.Point(125, 58)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(55, 24)
        Me.cmdEdit.TabIndex = 11
        Me.cmdEdit.Text = "Save"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'EditCust
        '
        Me.AcceptButton = Me.cmdEdit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdExitEdit
        Me.ClientSize = New System.Drawing.Size(247, 88)
        Me.Controls.Add(Me.cmdExitEdit)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.lblNewName)
        Me.Controls.Add(Me.lblNewPhone)
        Me.Controls.Add(Me.txtEditName)
        Me.Controls.Add(Me.txtEditPhone)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EditCust"
        Me.Text = "Edit Contact"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNewName As System.Windows.Forms.Label
    Friend WithEvents lblNewPhone As System.Windows.Forms.Label
    Friend WithEvents txtEditName As System.Windows.Forms.TextBox
    Friend WithEvents txtEditPhone As System.Windows.Forms.TextBox
    Friend WithEvents cmdExitEdit As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
End Class
