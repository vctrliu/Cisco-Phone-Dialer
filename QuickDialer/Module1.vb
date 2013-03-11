'Module1.vb declares several public variables that are used by several forms.

Imports System.Configuration
Imports System.Collections.Specialized
Module Module1
    Public lnumcust As Long 'number of organizations in phone list file
    Public lnumPCTS As Long 'number of PCTS in phone list file
    Public CustList() As String 'array with name of each organization

    Public Structure Directory
        Public name As String
        Public phone As String
    End Structure

    Public pctslist() As Directory 'PCTS Team directory array
    Public spdlist(7) As Directory 'Speed dial directory array (max 8 entries)
    Public AutoCompletelist As New AutoCompleteStringCollection 'Autocompletion list


End Module
