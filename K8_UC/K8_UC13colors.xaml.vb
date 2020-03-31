Imports System.Reflection

Public Class K8_UC13colors
    Private Sub ZeigeFarben(sender As Object, e As RoutedEventArgs)

        Dim MyColorCollection1() As PropertyInfo = GetType(System.Windows.Media.Brushes).GetProperties
        Dim MyColorCollection2 As SortedList = New SortedList()

        Me.LSTBX13_Farben.Items.Clear()

        For Each Farbe In MyColorCollection1
            'This is necesary because e.g. CYAN and AQUA are identical
            Try
                MyColorCollection2.Add(New SolidColorBrush(CType(Windows.Media.ColorConverter.ConvertFromString(Farbe.Name), Windows.Media.Color)).ToString, Farbe.Name)
            Catch ex As Exception

            End Try
        Next

        For Each Farbe In MyColorCollection1
            Me.LSTBX13_Farben.Items.Add(Farbe.Name & " - " & (New SolidColorBrush(CType(Windows.Media.ColorConverter.ConvertFromString(Farbe.Name), Windows.Media.Color)).ToString))
            Me.LSTBX13_Farben.Items.Add(New ListBoxItem With {.Content = Farbe.Name, .Background = New SolidColorBrush(CType(Windows.Media.ColorConverter.ConvertFromString(Farbe.Name), Windows.Media.Color))})
            Me.LSTBX13_Farben.Items.Add("--------------------")
        Next

    End Sub

    Public Sub ZeigeFarbwaehler(sender As Object, e As RoutedEventArgs)

        ZeigeMRLFarbwaehler(Me)

    End Sub

    Public Sub ZeigeMRLFarbwaehler(dependencyObject As DependencyObject)

        Dim SelectedColor As String = "White"

        Dim MyForm As New MRLColorPicker.UCcolors
        MyForm.Owner = Window.GetWindow(dependencyObject)
        If MyForm.ShowDialog = True Then
            SelectedColor = MyForm.SelectedRectangle.SelectedColorValueHex
        Else
            Exit Sub
        End If

        RCTNGL13_GewaehlteFarbe.Fill = MyForm.SelectedRectangle.SelectedFillColor

    End Sub

End Class
