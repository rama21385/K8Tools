Imports System.ComponentModel

Public Class K8_WW01

    Public Property SelectedCSVfile

    Private ListOfCSVfiles As Array

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ListOfCSVfiles = System.IO.Directory.GetFiles(".\", "*.csv", IO.SearchOption.TopDirectoryOnly).ToArray

        LSTBX_ListOfCSVfiles.ItemsSource = ListOfCSVfiles

    End Sub

    Private Sub SelectAndClose(sender As Object, e As MouseButtonEventArgs)

        SelectedCSVfile = LSTBX_ListOfCSVfiles.SelectedItem
        DialogResult = True

    End Sub
End Class
