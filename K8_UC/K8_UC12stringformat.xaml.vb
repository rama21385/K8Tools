Imports System.Globalization

Public Class K8_UC12stringformat

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Private Sub ShowNewFormattedText(sender As Object, e As SelectionChangedEventArgs)

        Dim MySelection As ListBoxItem = Nothing
        If RDBTN_02_StringFormat.IsChecked = True Then
            MySelection = CType(LSTBX_Formats1.SelectedItem, ListBoxItem)
        ElseIf RDBTN_02_ToString.IsChecked = True Then
            MySelection = CType(LSTBX_Formats2.SelectedItem, ListBoxItem)
        End If
        Try
            If RDBTN_01_Input.IsChecked = True Then
                If RDBTN_02_StringFormat.IsChecked = True Then
                    If CHKBX_Integer.IsChecked = False Then
                        LBL_Output.Content = String.Format("{" & MySelection.Content.ToString & "}", Val(TXTBX_Input01.Text.Replace(",", ".")))
                    Else
                        LBL_Output.Content = String.Format("{" & MySelection.Content.ToString & "}", CInt(Val(TXTBX_Input01.Text.Replace(",", "."))))
                    End If
                ElseIf RDBTN_02_ToString.IsChecked = True Then
                    If CHKBX_Integer.IsChecked = False Then
                        LBL_Output.Content = Val(TXTBX_Input01.Text.Replace(",", ".")).ToString(MySelection.Content.ToString, CultureInfo.CurrentCulture)
                    Else
                        LBL_Output.Content = CInt(Val(TXTBX_Input01.Text.Replace(",", "."))).ToString(MySelection.Content.ToString, CultureInfo.CurrentCulture)
                    End If
                End If
            ElseIf RDBTN_01_Now.IsChecked = True Then
                If RDBTN_02_StringFormat.IsChecked = True Then
                    LBL_Output.Content = String.Format("{" & MySelection.Content.ToString & "}", Now())
                ElseIf RDBTN_02_ToString.IsChecked = True Then
                    LBL_Output.Content = Now.ToString(MySelection.Content.ToString, CultureInfo.CurrentCulture)

                End If
            End If
        Catch ex As Exception
            LBL_Output.Content = "not valid"

        End Try
    End Sub

    Private Sub SetDefaultValue(sender As Object, e As RoutedEventArgs)

        If sender Is RDBTN_01_PI Then
            TXTBX_Input01.Text = Math.PI.ToString.Substring(0, 10)
            RDBTN_01_Input.IsChecked = True
        End If
    End Sub

    Private Sub ChangeListOfFormats(sender As Object, e As RoutedEventArgs)

        If sender Is RDBTN_02_StringFormat Then
            LSTBX_Formats1.Visibility = Visibility.Visible
            LSTBX_Formats2.Visibility = Visibility.Hidden
        ElseIf sender Is RDBTN_02_ToString Then
            LSTBX_Formats1.Visibility = Visibility.Hidden
            LSTBX_Formats2.Visibility = Visibility.Visible
        End If

    End Sub
End Class
