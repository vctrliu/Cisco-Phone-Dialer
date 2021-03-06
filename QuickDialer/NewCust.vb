﻿'NewCust.vb is the form for collecting new customer contact info.

Public Class NewCust



    'Public variables to pass back to main form
    Public sNewNum As String
    Public sNewName As String
    Public sNewCust As String 'Will store the contact's current organization, whether or not it's a new organization.
    Public isNewContact As Boolean
    Public isNewCust As Boolean


    Private Sub NewCust_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtNewPhone.Text = sNewNum
        cboCustList.Items.AddRange(CustList) 'Load existing organization list
    End Sub


    Private Sub chkNewCust_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNewCust.CheckedChanged
        'Control form layout based on "New Customer" checkbox
        If chkNewCust.Checked Then
            cboCustList.Visible = False
            cboCustList.Enabled = False
            cboCustList.SelectedIndex = -1

            txtNewCust.Visible = True
            txtNewCust.Enabled = True

        Else
            cboCustList.Visible = True
            cboCustList.Enabled = True

            txtNewCust.Visible = False
            txtNewCust.Enabled = False
            txtNewCust.Text = ""


        End If
    End Sub


    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        isNewContact = False
        Call Me.Hide()

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If txtNewCust.Text = "" Then 'If contact belongs to a new organization.
            If chkNewCust.Checked Then
                MessageBox.Show("Customer field empty")
                Exit Sub
            End If

        ElseIf txtNewName.Text = "" Then
            MessageBox.Show("Name field empty")
            Exit Sub
        ElseIf txtNewPhone.Text = "" Then
            MessageBox.Show("Phone field empty")
            Exit Sub
        End If

        sNewName = txtNewName.Text
        sNewNum = txtNewPhone.Text

        If chkNewCust.Checked Then
            sNewCust = txtNewCust.Text
            isNewCust = True
        Else
            sNewCust = cboCustList.SelectedItem

        End If

        isNewContact = True

        'MessageBox.Show(sNewCust & sNewName & sNewNum)
        Me.Hide()

    End Sub
End Class
