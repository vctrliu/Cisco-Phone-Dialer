Public Class EditCust
    Public sEditNum As String
    Public sEditName As String
    Public isEdited As Boolean
    Public isEditCustomer As Boolean



    Private Sub EditCust_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtEditName.Text = sEditName
        txtEditPhone.Text = sEditNum
        If isEditCustomer Then
            txtEditPhone.Visible = False
        End If

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If txtEditName.Text = "" Then
            MessageBox.Show("Name field empty")
            Exit Sub
        Else
            sEditName = txtEditName.Text
        End If


        If Not isEditCustomer Then
            If txtEditPhone.Text = "" Then
                MessageBox.Show("Phone field empty")
                Exit Sub
            Else
                sEditNum = txtEditPhone.Text
            End If

        End If
        



        isEdited = True
        Me.Hide()



    End Sub

    Private Sub cmdExitEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExitEdit.Click
        isEdited = False
        Me.Hide()

    End Sub
End Class