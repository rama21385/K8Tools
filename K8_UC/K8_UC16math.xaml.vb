Public Class K8_UC16math
    Private Sub Calculate(sender As Object, e As RoutedEventArgs)

        Dim Math1Int As Int32
        Dim Math2Int As Int32

        If CHBX16_IsBinary1.IsChecked = False Then
            Math1Int = CInt(Val(Me.TXTBX16_Math1.Text))
            Math2Int = CInt(Val(Me.TXTBX16_Math2.Text))
        Else
            Math1Int = Convert.ToInt32(Me.TXTBX16_Math1.Text, 2)
            Math2Int = Convert.ToInt32(Me.TXTBX16_Math2.Text, 2)
        End If

        If sender Is RDBTN16_Math_01 Then
            TXTBX16_MathResult.Text = (Math1Int ^ Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_02 Then
            TXTBX16_MathResult.Text = (Math1Int - Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_03 Then
            TXTBX16_MathResult.Text = (Math1Int * Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_04 Then
            TXTBX16_MathResult.Text = (Math1Int / Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_05 Then
            TXTBX16_MathResult.Text = (Math1Int \ Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_06 Then
            TXTBX16_MathResult.Text = (Math1Int Mod Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_07 Then
            TXTBX16_MathResult.Text = (Math1Int + Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_08 Then
            TXTBX16_Math2.Text = ""
            TXTBX16_MathResult.Text = (Not Math1Int).ToString

        ElseIf sender Is RDBTN16_Math_09 Then
            TXTBX16_MathResult.Text = (Math1Int And Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_10 Then
            TXTBX16_MathResult.Text = (Math1Int Or Math2Int).ToString

        ElseIf sender Is RDBTN16_Math_11 Then
            TXTBX16_MathResult.Text = (Math1Int Xor Math2Int).ToString
        End If

    End Sub

End Class
