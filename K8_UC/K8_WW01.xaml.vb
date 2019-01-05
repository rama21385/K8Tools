Imports System.ComponentModel
Imports System.IO

Public Class K8_WW01

    Public Property SelectedCSVfile As String

    Private ListOfCSVfiles As New List(Of String)

    Public StartFolder As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Private Sub SelectAndClose(sender As Object, e As MouseButtonEventArgs)

        SelectedCSVfile = CStr(LSTBX_ListOfCSVfiles.SelectedItem)
        If SelectedCSVfile IsNot Nothing Then
            DialogResult = True
        Else
            DialogResult = False
        End If

    End Sub

    Public Sub ShowExistingFiles()

        LSTBX_ListOfCSVfiles.ItemsSource = ListOfCSVfiles

        For Each ItemFile As String In System.IO.Directory.GetFiles(StartFolder, "*.csv", IO.SearchOption.TopDirectoryOnly)
            ListOfCSVfiles.Add(ItemFile.Substring(InStrRev(ItemFile, "\")))
        Next

    End Sub

End Class
