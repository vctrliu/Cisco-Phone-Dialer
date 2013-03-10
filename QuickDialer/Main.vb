Imports System
Imports System.Xml

Public Class Main
    Dim sNumber As String 'Used to hold the number being dialed
    Dim isMini As Boolean 'Determine whether the form is in compact mode
    Dim sfilename As String 'xml file that stores the phone list info

    '=====Form Load Events=====

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sfilename = "QuickDialer.xml"
        'sfilename = "test.xml"

        Call CallCommandLineArgs() 'Execute any command line arguments
        Call LoadContacts() 'Load the contacts list
        Call LoadcboPCTS() 'Load the dropdown team contacts box
        Call PaintSpdButtons() 'Populate the speed dial buttons
        Call PaintSpdIndex() 'Populate the speed dial menu (for editing)
        Call PaintIconContextMenu() 'Populate taskbar button right click menu
        Call BuildAutoComplete() 'Create autocompletion reference list

    End Sub

    'Load customer contacts
    Private Sub LoadContacts()

        Dim reader As XmlTextReader = New XmlTextReader(sfilename) 'initalize xml reader
        Dim lspdReadIndex As Long 'counter for loading speed dials

        Do While reader.Read()

            Dim cnode As TreeNode 'xml reader will insert the contacts into a treenode for the phonelist

            Select Case reader.NodeType 'Check each node's typ
                Case XmlNodeType.Element
                    'MessageBox.Show(reader.Name & "element")

                    'Load the speed dials from the file
                    If reader.Name = "spd" Then
                        Dim sSpdName As String
                        Dim sSpdNum As String

                        reader.MoveToFirstAttribute()
                        sSpdNum = reader.Value
                        reader.MoveToNextAttribute()
                        sSpdName = reader.Value

                        With spdlist(lspdReadIndex) 'Populate the speed dial directory array
                            .name = sSpdName
                            .phone = sSpdNum
                        End With

                        lspdReadIndex = lspdReadIndex + 1

                    End If

                    'Load the organiztion's name, create a new node for the organization
                    If reader.Name = "customer" Then
                        Dim sCustName As String
                        Dim newnode As New TreeNode
                        Dim ltempindex As Long

                        reader.MoveToFirstAttribute()
                        sCustName = reader.Value
                        'MessageBox.Show(sCustName)

                        With newnode
                            .Name = sCustName
                            .Text = sCustName
                        End With

                        CustTree.Nodes.Add(newnode)
                        cnode = newnode
                        newnode = Nothing 'Set to "nothing" or else it errors

                        lnumcust = lnumcust + 1
                        ltempindex = lnumcust - 1

                        ReDim Preserve CustList(ltempindex) 'Add organization's name to the array of organizations
                        CustList(ltempindex) = sCustName
                    End If

                    'Load the each contact for the organization
                    If reader.Name = "contact" Then
                        Dim sContName As String
                        Dim sContPhone As String
                        Dim newnode As New TreeNode

                        reader.MoveToFirstAttribute()
                        sContName = reader.Value
                        reader.MoveToNextAttribute()
                        sContPhone = reader.Value
                        'MessageBox.Show(sContName & " " & sContPhone)

                        With newnode
                            .Name = sContName
                            .Text = sContName
                            .Tag = sContPhone
                        End With

                        cnode.Nodes.Add(newnode)
                        newnode = Nothing
                    End If

                    'Load pcts team member phone numbers
                    If reader.Name = "pcts" Then
                        Dim ltempcounter As Long
                        Dim stempname As String
                        Dim stempnum As String

                        reader.MoveToFirstAttribute()
                        'MessageBox.Show(reader.Value)
                        stempname = reader.Value
                        reader.MoveToNextAttribute()
                        'MessageBox.Show(reader.Value)
                        stempnum = reader.Value

                        lnumPCTS = lnumPCTS + 1
                        ltempcounter = lnumPCTS - 1
                        ReDim Preserve pctslist(ltempcounter) 'Add to the pcts directory array

                        With pctslist(ltempcounter)
                            .name = stempname
                            .phone = stempnum
                        End With
                        'MessageBox.Show(pctslist(ltempcounter).name)
                    End If

            End Select

        Loop

        lspdReadIndex = 0
        reader.Close()
        reader = Nothing

    End Sub

    'Load PCTS List
    Private Sub LoadcboPCTS()
        'Populate the PCTS dropdown combo using the PCTS directory array
        Dim lindex As Long
        Dim lmax As Long
        lmax = lnumPCTS - 1

        For lindex = 0 To lmax
            cboPCTS.Items.Add(pctslist(lindex).name & " (x" & pctslist(lindex).phone & ")")
        Next lindex



    End Sub

    'Populate Speed dial butons using the speed dial directory array
    Private Sub PaintSpdButtons()
        Dim newbut As Button
        Dim lindex As Long
        For Each newbut In PnlSpdButtons.Controls
            newbut.Text = spdlist(lindex).name
            newbut.Tag = lindex
            lindex = lindex + 1

        Next
    End Sub

    'Populate Speed dial index using te speed dial directory array
    Private Sub PaintSpdIndex()
        Dim txt1 As TextBox
        Dim txt2 As TextBox
        Dim lindex1 As Long
        Dim lindex2 As Long

        'Populate left panels with names
        For Each txt1 In PnlSpdIndex.Panel1.Controls
            txt1.Text = spdlist(lindex1).name
            txt1.Tag = lindex1
            lindex1 = lindex1 + 1
        Next

        'Populate right panels with corresponding phone numbers
        For Each txt2 In PnlSpdIndex.Panel2.Controls
            txt2.Text = spdlist(lindex2).phone
            txt2.Tag = lindex2
            lindex2 = lindex2 + 1
        Next
    End Sub

    'Populate right click context menu for taskbar icon
    Private Sub PaintIconContextMenu()
        Dim MenuItem As ToolStripMenuItem
        Dim lindex As Long

        For Each MenuItem In IconMenuStrip.Items
            If lindex < 7 Then
                If spdlist(lindex).phone = "" Then
                    MenuItem.Tag = ""
                    lindex = lindex + 1
                Else
                    MenuItem.Text = spdlist(lindex).name
                    MenuItem.Tag = spdlist(lindex).phone
                    lindex = lindex + 1
                End If
            Else
                Exit Sub
            End If


        Next
    End Sub

    'Populate the Auto Completion Suggestion list
    'Because autocomplete isn't intelligent to start lookups from the middle of a string, 
    'entries are duplicated, one entry is name first, second is phone number first

    Private Sub BuildAutoComplete()
        Dim pnode As TreeNode
        Dim cnode As TreeNode
        Dim lctr As Long
        Dim stempparentname As String
        Dim stempname1 As String
        Dim stempname2 As String


        'Traverses all top level nodes in the custtree, add all subnodes to list
        For Each pnode In CustTree.Nodes
            If pnode.Level = 0 Then
                'Assign the Organization Name
                stempparentname = pnode.Name

                'Traverse each Organization node and add the child nodes to the autocomplete list.
                For Each cnode In pnode.Nodes
                    stempname1 = cnode.Name & " @ " & stempparentname & " >> " & cnode.Tag 'Outputs: Customer Name @ Organization Name >> Phone Number
                    stempname2 = cnode.Tag & " << " & cnode.Name & " @ " & stempparentname 'Outputs: Phone Number << Customer Name @ Organization
                    AutoCompletelist.Add(stempname1)
                    AutoCompletelist.Add(stempname2)

                Next
            End If
        Next

        'Add all PCTS to list
        For lctr = 0 To lnumPCTS - 1
            stempname1 = pctslist(lctr).name & " >> " & pctslist(lctr).phone 'outputs: PCTS name >> phone
            stempname2 = pctslist(lctr).phone & " << " & pctslist(lctr).name 'outputs: phone << PCTS name
            AutoCompletelist.Add(stempname1)
            AutoCompletelist.Add(stempname2)

        Next


        'Assign the Autocomplete list as a custom source to the two input text boxes
        txtInput.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtInput.AutoCompleteCustomSource = AutoCompletelist
        txtTaskbarCall.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtTaskbarCall.AutoCompleteCustomSource = AutoCompletelist


    End Sub

    '=====Phone call functions=====

    'Dial any command line arguments
    Private Sub CallCommandLineArgs()
        'Used when command line arguments are passed to program
        Dim sdialme As String
        Dim isCloseout As Boolean
        sdialme = ""

        'If "c" is in the command line, close the application after dialing
        For Each argument As String In My.Application.CommandLineArgs
            If argument = "c" Then
                isCloseout = True
                argument = ""
            End If
            sdialme = sdialme & CStr(argument)
        Next
        Call InitializeCall(sdialme)

        If isCloseout Then
            Threading.Thread.Sleep(1000)
            Me.Close()
        End If
    End Sub

    'Call button on "Call" Tab
    Private Sub cmdCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCall.Click
        sNumber = txtInput.Text
        Call InitializeCall(sNumber)
    End Sub

    'Call button on "Phonebook" Tab
    Private Sub cmdCall2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCall2.Click
        Call CallfromPhoneList()
    End Sub

    'Validate the selected node and call if it's level 1 (not an organization)
    Private Sub CallfromPhoneList()
        'Calls the selected Treenode
        Dim cnode As TreeNode
        cnode = CustTree.SelectedNode
        If cnode.Level = 1 Then
            Call InitializeCall(cnode.Tag)
            TabControl1.SelectTab(0)
        End If
    End Sub


    Private Sub InitializeCall(ByVal sInitInput As String)
        Dim sDialString As String
        Call Strip(sInitInput)

        If Len(sNumber) = 0 Then
            Exit Sub
        ElseIf Len(sNumber) = 4 Then 'internal extension
            sDialString = sNumber
            DialOut(sDialString)
        ElseIf Len(sNumber) = 7 Then 'local number
            sDialString = "8" & sNumber
            DialOut(sDialString)
        ElseIf Len(sNumber) = 10 Then 'long distance, without the 1
            sDialString = "81" & sNumber
            DialOut(sDialString)
        ElseIf Len(sNumber) = 11 Then 'long distance, with the 1. Yay!
            sDialString = "8" & sNumber
            DialOut(sDialString)
        Else
            sDialString = sNumber
            DialOut(sDialString)
        End If

    End Sub

    'Strip the input dial string.
    Private Sub Strip(ByVal sStripInput As String)
        If Len(sStripInput) = 0 Then
            Exit Sub
        End If

        'For autocomplete entries, find the ">>" and strip out all preceding characters
        '>> is the separator when the contact name precedes the actual phone number
        If InStr(sStripInput, ">>") > 0 Then
            Dim ltrimhere As Long
            ltrimhere = InStr(sStripInput, ">>")
            sStripInput = Mid(sStripInput, ltrimhere)

            'For autocomplete entries, find the "<<" and strip out all preceding characters
            '>> is the separator when the contact name follows the actual phone number
        ElseIf InStr(sStripInput, "<<") > 0 Then
            Dim ltrimhere As Integer
            ltrimhere = InStr(sStripInput, "<<")
            sStripInput = Mid(sStripInput, 1, ltrimhere)
        End If

        'Replaces sNumber with a correctly formatted value
        Dim illegalChars As Char() = "!@#$%^&*(){}[]""_+<>?/ -.".ToCharArray()
        Dim str As String
        str = sStripInput
        For Each ch In illegalChars
            str = str.Replace(ch, "")
        Next

        sNumber = str

    End Sub

    'This function calls into the webdialer thingy in guru that sends the SOAP message to your phone.
    Private Sub DialOut(ByVal sNum As String)
        Dim sURL As String
        Dim sCommandLine As String
        txtInput.Text = sNum

        sURL = "http://guru/services/Webdialer.asmx/CallNumber?extension="
        sCommandLine = sURL & sNum
        objweb.Navigate(sCommandLine)
        'MessageBox.Show(sCommandLine)

    End Sub

    'Call the PCTS you select from the drop down list
    Private Sub cboPCTS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPCTS.SelectedIndexChanged
        'Call the selected PCTS from the dropdown
        txtInput.Text = pctslist(cboPCTS.SelectedIndex).phone
        Call InitializeCall(txtInput.Text)

    End Sub

    Private Sub CustTree_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles CustTree.DoubleClick
        'Call the selected customer contact when double clicked
        If CustTree.SelectedNode.Level = 1 Then
            'MessageBox.Show(CustTree.SelectedNode.Tag)
            Call InitializeCall(CustTree.SelectedNode.Tag)
        End If
    End Sub

    Private Sub CustTree_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CustTree.KeyDown
        'Call the selected customer contact when Enter key is pressed
        If e.KeyValue = 13 Then
            If CustTree.SelectedNode.Level = 1 Then
                'MessageBox.Show(CustTree.SelectedNode.Tag)
                Call InitializeCall(CustTree.SelectedNode.Tag)
            End If
        End If
    End Sub

    '=====End call commands=====

    Private Sub cmdEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnd.Click
        Call endcall()

    End Sub

    Private Sub cmdEnd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnd2.Click
        Call endcall()

    End Sub

    Private Sub endcall()
        objweb.Navigate("http://guru/services/Webdialer.asmx/HangUpCall")
        txtInput.Text = ""
    End Sub

    '=====Status Bar Buttons=====
    'Always on top
    Private Sub toolTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolTop.Click
        If Me.TopMost Then
            Me.TopMost = False
        Else
            Me.TopMost = True
        End If
    End Sub

    'Toggle Size
    Private Sub toolMini_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolMini.Click
        If isMini Then
            Me.Width = 255
            Me.Height = 341
            Me.FormBorderStyle = 3
            isMini = False
        Else
            Me.Width = 255
            Me.Height = 95
            Me.FormBorderStyle = 5
            isMini = True
        End If
    End Sub

    'Minimize to tray
    Private Sub ToolMintoTray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMintoTray.Click
        Me.Visible = False
        Me.ShowInTaskbar = False
        IconTaskbar.Visible = True
    End Sub

    'Restore from taskbar
    Private Sub IconTaskbar_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles IconTaskbar.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then

            Me.ShowInTaskbar = True
            Me.Visible = True
            Me.Focus()
            IconTaskbar.Visible = False

        End If

    End Sub


    '=====Other command buttons=====

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        txtInput.Text = ""
    End Sub


    '=====Contact Commands=====
    Private Sub cmdNewContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewContact.Click
        Call CreateNew()
    End Sub

    Private Sub cmdNewContact2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewContact2.Click
        Call CreateNew()
    End Sub

    Private Sub cmdEditList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditList.Click
        Call EditList()
    End Sub

    Private Sub cmdDeleteList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteList.Click
        Call DeletefromList()
    End Sub

    Private Sub CreateNew()
        'For creating a new Customer contact
        Dim PopUp As New NewCust
        Dim sTempName As String
        Dim sCustName As String
        Dim sTempPhone As String

        PopUp.sNewNum = txtInput.Text

        PopUp.ShowDialog()

        If PopUp.isNewContact = False Then
            Exit Sub
        End If

        sTempName = PopUp.sNewName
        sTempPhone = PopUp.sNewNum
        sCustName = PopUp.sNewCust

        If PopUp.isNewCust Then
            Dim tnode As New TreeNode
            Dim snode As New TreeNode
            Dim ltempindex As Long

            With tnode
                .Name = sCustName
                .Text = sCustName
            End With

            CustTree.Nodes.Add(tnode)
            lnumcust = lnumcust + 1

            ltempindex = lnumcust - 1
            ReDim Preserve CustList(ltempindex)
            CustList(ltempindex) = sCustName

            With snode
                .Name = sTempName
                .Text = sTempName
                .Tag = sTempPhone
            End With

            tnode.Nodes.Add(snode)

        Else
            Dim tnode As TreeNode
            Dim snode As New TreeNode
            tnode = findnode(sCustName)
            With snode
                .Name = sTempName
                .Text = sTempName
                .Tag = sTempPhone
            End With
            tnode.Nodes.Add(snode)


        End If
        Call SaveFile()
        PopUp = Nothing

    End Sub

    Private Function findnode(ByRef input As String)
        'Find the correct Customer node to insert the new customer contact node
        Dim fnode As TreeNode
        Dim rnode As TreeNode



        For Each fnode In CustTree.Nodes
            If fnode.Level = 0 Then
                If fnode.Name = input Then
                    rnode = fnode
                End If
            End If
        Next fnode

        Return rnode


    End Function

    Private Sub EditList()

        On Error GoTo ErrLine

        If IsNothing(CustTree.SelectedNode()) Then
            Exit Sub
        End If

        If CustTree.SelectedNode.Level = 1 Then
            Dim cnode As TreeNode
            cnode = CustTree.SelectedNode

            Dim popup As New EditCust
            popup.sEditNum = cnode.Tag
            popup.sEditName = cnode.Name
            popup.ShowDialog()

            If popup.isEdited Then
                cnode.Name = popup.sEditName
                cnode.Text = popup.sEditName
                cnode.Tag = popup.sEditNum
                Call SaveFile()
            End If

            popup = Nothing

            'More efficient method(?): 
            'Using popup As New EditCust
            '    popup.sEditNum = cnode.Tag
            '    popup.sEditName = cnode.Name
            '    popup.ShowDialog()

            '    If popup.isEdited Then
            '        cnode.Name = popup.sEditName
            '        cnode.Text = popup.sEditName
            '        cnode.Tag = popup.sEditNum
            '        Call SaveFile()
            '    End If
            'End Using


        ElseIf CustTree.SelectedNode.Level = 0 Then
            Dim cnode As TreeNode
            cnode = CustTree.SelectedNode
            Dim ltempindex As Long 'Index of the customer being edited
            ltempindex = FindCust(cnode.Name)

            Dim popup As New EditCust
            popup.sEditName = cnode.Name
            popup.isEditCustomer = True

            popup.ShowDialog()

            If popup.isEdited Then
                cnode.Name = popup.sEditName
                cnode.Text = popup.sEditName
                CustList(ltempindex) = popup.sEditName
                Call SaveFile()

            End If

            popup = Nothing

        End If

ErrLine: Exit Sub

    End Sub

    Private Function FindCust(ByVal input As String)
        Dim lindex As Long
        Dim lctr As Long

        For lctr = 0 To lnumcust - 1
            If CustList(lctr) = input Then
                lindex = lctr
            End If
        Next

        'Find the index of the changed customer
        Return lindex

    End Function

    Private Sub DeletefromList()
        On Error GoTo ErrLine

        Dim ltempindex As Long
        Dim cnode As TreeNode

        If IsNothing(CustTree.SelectedNode()) Then
            Exit Sub
        End If

        cnode = CustTree.SelectedNode

        If MsgBox("Are you sure you want to delete " & cnode.Name & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            ltempindex = FindCust(cnode.Name)
            Call RemoveFromCustList(ltempindex)
            CustTree.Nodes.Remove(cnode)
            Call SaveFile()
        End If

ErrLine: Exit Sub
    End Sub

    Private Sub RemoveFromCustList(ByVal lcustindex As Long)
        Dim temparray(lnumcust - 1) As String
        Dim lctr As Long


        For lctr = 0 To lnumcust - 2
            If lctr < lcustindex Then
                temparray(lctr) = CustList(lctr)
            Else
                temparray(lctr) = CustList(lctr + 1)
            End If
        Next

        lnumcust = lnumcust - 1
        ReDim CustList(lnumcust - 1)
        CustList = temparray


    End Sub

    '=====Save File Commands==================
    Private Sub cmdSaveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveFile.Click
        Call SaveSpdChanges()
        Call SaveFile()
        Call PaintSpdButtons()
        Call PaintIconContextMenu()

    End Sub

    Private Sub SaveSpdChanges()
        'Saves the Speed Button index to the spdlist array
        Dim txt1 As TextBox
        Dim txt2 As TextBox
        Dim lindex1 As Long
        Dim lindex2 As Long


        For Each txt1 In PnlSpdIndex.Panel1.Controls
            spdlist(lindex1).name = txt1.Text
            lindex1 = lindex1 + 1

        Next

        For Each txt2 In PnlSpdIndex.Panel2.Controls
            spdlist(lindex2).phone = txt2.Text
            lindex2 = lindex2 + 1

        Next
    End Sub

    Private Sub SaveFile()

        Dim settings As New XmlWriterSettings
        settings.Indent = True

        Dim xmlwrt As XmlWriter = XmlWriter.Create(sfilename, settings)

        With xmlwrt
            .WriteStartDocument()
            .WriteStartElement("Config") '<config>


            'Write the Speed dials section
            .WriteStartElement("speed-dials") '<speed-dials>
            .WriteComment("SPEED DIALS: You can only have 8 speed dials, additional entries will not be used")

            Dim lindex As Long
            For lindex = 0 To 7
                .WriteStartElement("spd") '<spd>
                .WriteAttributeString("phone", spdlist(lindex).phone)
                .WriteAttributeString("name", spdlist(lindex).name)
                .WriteEndElement() '</spd>
            Next

            .WriteEndElement() '</speed-dials>


            'Write the Phonebook
            .WriteStartElement("phonebook")
            .WriteComment("PHONEBOOK: No limit, add as many customers and contacts as you'd like")

            'Recurse all nodes, find customer nodes
            Dim wnode As TreeNode
            For Each wnode In CustTree.Nodes

                'Recurse customer node, find contact nodes
                If wnode.Level = 0 Then
                    .WriteStartElement("customer") '<customer>
                    .WriteAttributeString("name", wnode.Text)

                    Dim wsnode As TreeNode
                    For Each wsnode In wnode.Nodes
                        .WriteStartElement("contact") '</contact>
                        .WriteAttributeString("name", wsnode.Text)
                        .WriteAttributeString("phone", wsnode.Tag)
                        .WriteEndElement() '</contact>
                    Next
                    .WriteEndElement() '</customer>

                End If
            Next
            .WriteEndElement() '</phonebook>

            'Write PC Systems
            .WriteStartElement("pcsys") '<pcsys>
            .WriteComment("PC SYSTEMS LIST: Must be changed in XML, can't add it in the program")
            For lindex = 0 To pctslist.Count - 1
                .WriteStartElement("pcts") '<pcts>
                .WriteAttributeString("name", pctslist(lindex).name)
                .WriteAttributeString("phone", pctslist(lindex).phone)
                .WriteEndElement() '</pcts>
            Next
            .WriteEndElement() '</pcsys>

            'Close off the XML file
            .WriteEndElement() '</config>
            .WriteEndDocument()
            .Close()


        End With
    End Sub

    '=====Speed dial button commands===========

    Private Sub Button0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button0.Click
        Dim myitem As Control = CType(sender, Control)
        Call InitializeCall(spdlist(myitem.Tag).phone)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim myitem As Control = CType(sender, Control)
        Call InitializeCall(spdlist(myitem.Tag).phone)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim myitem As Control = CType(sender, Control)
        Call InitializeCall(spdlist(myitem.Tag).phone)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim myitem As Control = CType(sender, Control)
        Call InitializeCall(spdlist(myitem.Tag).phone)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim myitem As Control = CType(sender, Control)
        Call InitializeCall(spdlist(myitem.Tag).phone)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim myitem As Control = CType(sender, Control)
        Call InitializeCall(spdlist(myitem.Tag).phone)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim myitem As Control = CType(sender, Control)
        Call InitializeCall(spdlist(myitem.Tag).phone)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim myitem As Control = CType(sender, Control)
        Call InitializeCall(spdlist(myitem.Tag).phone)

    End Sub

    '=====Taskbar Icon Context Menu commands===

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Call InitializeCall(ToolStripMenuItem1.Tag)
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Call InitializeCall(ToolStripMenuItem2.Tag)
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Call InitializeCall(ToolStripMenuItem3.Tag)
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Call InitializeCall(ToolStripMenuItem4.Tag)
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        Call InitializeCall(ToolStripMenuItem5.Tag)
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        Call InitializeCall(ToolStripMenuItem6.Tag)
    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        Call InitializeCall(ToolStripMenuItem7.Tag)
    End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        Call InitializeCall(ToolStripMenuItem8.Tag)
    End Sub

    Private Sub txtTaskbarCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTaskbarCall.Click
        txtTaskbarCall.Text = ""
    End Sub

    Private Sub txtTaskbarCall_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTaskbarCall.KeyDown
        If e.KeyValue = 13 Then
            txtInput.Text = txtTaskbarCall.Text
            sNumber = txtTaskbarCall.Text
            Call InitializeCall(sNumber)
        End If
    End Sub

    Private Sub EndCallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndCallToolStripMenuItem.Click
        Call endcall()
    End Sub

    '=====Phonelist Context Menu commands======

    Private Sub CustTree_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles CustTree.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectednode As TreeNode
            Dim spoint As Point
            spoint = Control.MousePosition
            selectednode = CustTree.GetNodeAt(CustTree.PointToClient(spoint))

            If IsNothing(selectednode) Then
                Exit Sub
            Else
                CustTree.SelectedNode = selectednode
                PhonebookMenuStrip.Show(spoint)
            End If
        End If
    End Sub

    Private Sub Call_ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Call_ToolStripMenuItem.Click
        Call CallfromPhoneList()

    End Sub

    Private Sub New_ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles New_ToolStripMenuItem.Click
        Call CreateNew()

    End Sub

    Private Sub Edit_ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Edit_ToolStripMenuItem.Click
        Call EditList()
    End Sub

    Private Sub Delete_ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_ToolStripMenuItem.Click
        Call DeletefromList()

    End Sub

    '=====Drag and drop treenodes============== Adapted from www.codeproject.com/KB/vb/TreeViewDragAndDrop.aspx

    Private Sub CustTree_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles CustTree.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)

    End Sub

    Private Sub CustTree_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles CustTree.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub CustTree_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles CustTree.DragOver
        'Check that there is a TreeNode being dragged 
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedTreeview As TreeView = CType(sender, TreeView)

        'As the mouse moves over nodes, provide feedback to the user by highlighting the node that is the current drop target
        Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
        Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

        'See if the targetNode is currently selected, if so no need to validate again
        If Not (selectedTreeview.SelectedNode Is targetNode) Then
            'Select the node currently under the cursor
            selectedTreeview.SelectedNode = targetNode

            'Check that the selected node is not the dropNode and also that it is not a child of the dropNode and therefore an invalid target
            Dim dropNode As TreeNode = _
                CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Do Until targetNode Is Nothing
                If targetNode Is dropNode Then
                    e.Effect = DragDropEffects.None
                    Exit Sub
                End If
                targetNode = targetNode.Parent
            Loop

            'Currently selected node is a suitable target
            e.Effect = DragDropEffects.Move
        End If

    End Sub

    Private Sub CustTree_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles CustTree.DragDrop

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then
            Exit Sub
        End If

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedTreeview As TreeView = CType(sender, TreeView)

        'Get the TreeNode being dragged
        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        'The target node should be selected from the DragOver event
        Dim targetNode As TreeNode = selectedTreeview.SelectedNode

        'If there is no targetNode exit
        If targetNode Is Nothing Then
            Exit Sub

            'Child nodes moved to different parent nodes get dropped to the bottom
        ElseIf dropNode.Level = 1 Then
            If targetNode.Level = 0 Then
                dropNode.Remove()
                targetNode.Nodes.Add(dropNode)

                'Moving child node within parent node
            Else
                Dim tempindex As Integer
                Dim parentnode As TreeNode
                tempindex = targetNode.Index

                parentnode = targetNode.Parent

                dropNode.Remove()
                parentnode.Nodes.Insert(tempindex, dropNode)
            End If

            'Moving Parent nodes
        ElseIf dropNode.Level = 0 Then

            If targetNode.Level = 0 Then
                'enumerate all child nodes
                Dim numnodes As Integer
                numnodes = dropNode.GetNodeCount(False)
                Dim childnodes(numnodes - 1) As TreeNode
                Dim tempindex As Integer
                Dim lindex As Long
                Dim tempnode As New TreeNode

                For Each tempnode In dropNode.Nodes
                    childnodes(lindex) = tempnode
                    lindex = lindex + 1
                Next

                tempindex = targetNode.Index
                dropNode.Remove()
                CustTree.Nodes.Insert(tempindex, dropNode)

                'Repopulate all child nodes
                For Each tempnode In childnodes
                    tempnode.Remove()
                    dropNode.Nodes.Add(tempnode)
                Next

                'Prevent parent nodes from moving to child nodes
            ElseIf targetNode.Level = 1 Then
                Exit Sub

            End If

        End If


        'Ensure the newley created node is visible to the user and select it
        dropNode.EnsureVisible()
        selectedTreeview.SelectedNode = dropNode

        Call SaveFile()


    End Sub


    '=====Misc. functions=====

    Private Sub lblPCTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPCTS.Click
        If cboPCTS.DroppedDown Then
            cboPCTS.DroppedDown = False
        Else
            cboPCTS.DroppedDown = True
        End If
    End Sub


End Class