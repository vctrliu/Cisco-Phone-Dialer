Imports System.Configuration
Imports System.Collections.Specialized
Module Module1
    Public lnumcust As Long
    Public lnumPCTS As Long
    Public CustList() As String

    Public Structure Directory
        Public name As String
        Public phone As String
    End Structure

    Public pctslist() As Directory
    Public spdlist(7) As Directory
    Public AutoCompletelist As New AutoCompleteStringCollection


End Module
