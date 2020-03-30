Public Class K8_UC12stringformat

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Private Sub ShowNewFormattedText(sender As Object, e As SelectionChangedEventArgs)

        Dim MySelection As ListBoxItem
        MySelection = CType(LSTBX_Formats.SelectedItem, ListBoxItem)
        Try
            LBL_Output.Content = String.Format("{" & MySelection.Content.ToString & "}", CInt(Val(TXTBX_Input01.Text)))
        Catch ex As Exception
            LBL_Output.Content = "not valid"

        End Try
    End Sub

End Class
