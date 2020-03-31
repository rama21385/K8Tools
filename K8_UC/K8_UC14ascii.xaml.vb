Public Class K8_UC14ascii
    Private Sub CreateASCIITable(sender As Object, e As RoutedEventArgs)

        Dim Result As New System.Text.StringBuilder
        Dim Counter As Integer
        Dim Testchar As Char
        Dim ZeileConst As String = "%1   %2   %3   %4   %5   %6   %7   %8   %9   %A   %B"
        Dim Zeile As String = ""
        Dim DoAppend As Boolean = False

        Dim True1 As String = "   ---   "
        Dim True2 As String = True1
        Dim True3 As String = True1
        Dim True4 As String = True1
        Dim True5 As String = True1
        Dim True6 As String = True1
        Dim True7 As String = True1
        Dim True8 As String = True1
        Dim True9 As String = True1
        Dim TrueA As String = True1
        Dim TrueB As String = True1


        Result.AppendLine("LET = Letter")
        Result.AppendLine("CON = Console Character")
        Result.AppendLine("DIG = Digit")
        Result.AppendLine("LOD = Letter or Digit")
        Result.AppendLine("LOW = Lower Case Character")
        Result.AppendLine("NUM = Numerisch")
        Result.AppendLine("PUN = Punctuation")
        Result.AppendLine("WHI = White Spaces")
        Result.AppendLine("SEP = Separator")
        Result.AppendLine("SYM = Symbol")
        Result.AppendLine("UPP = Upper Case Character")


        For Counter = 0 To 255
            Testchar = Chr(Counter)
            True1 = "  ---  "
            True2 = True1
            True3 = True1
            True4 = True1
            True5 = True1
            True6 = True1
            True7 = True1
            True8 = True1
            True9 = True1
            TrueA = True1
            TrueB = True1
            Zeile = ""

            If Char.IsLetter(Testchar) Then True1 = "  LET  "
            If Char.IsControl(Testchar) Then True2 = "  CON  "
            If Char.IsDigit(Testchar) Then True3 = "  DIG  "
            If Char.IsLetterOrDigit(Testchar) Then True4 = "  LOD  "
            If Char.IsLower(Testchar) Then True5 = "  LOW  "
            If Char.IsNumber(Testchar) Then True6 = "  NUM  "
            If Char.IsPunctuation(Testchar) Then True7 = "  PUN  "
            If Char.IsSeparator(Testchar) Then True8 = "  SEP  "
            If Char.IsSymbol(Testchar) Then True9 = "  SYM  "
            If Char.IsUpper(Testchar) Then TrueA = "  UPP  "
            If Char.IsWhiteSpace(Testchar) Then TrueB = "  WHI  "
            Zeile = ZeileConst.Replace("%1", True1).Replace("%2", True2).Replace("%3", True3).Replace("%4", True4).Replace("%5", True5).Replace("%6", True6).Replace("%7", True7).Replace("%8", True8).Replace("%9", True9).Replace("%A", TrueA).Replace("%B", TrueB)


            DoAppend = False

            If CHKBX_LET.IsChecked = True And Char.IsLetter(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_CON.IsChecked = True And Char.IsControl(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_DIG.IsChecked = True And Char.IsDigit(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_LOD.IsChecked = True And Char.IsLetterOrDigit(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_LOW.IsChecked = True And Char.IsLower(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_NUM.IsChecked = True And Char.IsNumber(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_PUN.IsChecked = True And Char.IsPunctuation(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_SEP.IsChecked = True And Char.IsSeparator(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_SYM.IsChecked = True And Char.IsSymbol(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_UPP.IsChecked = True And Char.IsUpper(Testchar) = True Then
                DoAppend = True
            ElseIf CHKBX_WHI.IsChecked = True And Char.IsWhiteSpace(Testchar) = True Then
                DoAppend = True
            End If

            If DoAppend = True Then
                If Counter < 32 Then
                    Result.AppendLine(Format(Counter, "000") & "   " & Zeile)
                Else
                    Result.AppendLine(Format(Counter, "000") & " " & Testchar & " " & Zeile)
                End If
            End If

        Next

        Result.AppendLine()

        For Counter = 0 To 255
            Testchar = Chr(Counter)
            If CHKBX_LET.IsChecked = True And Char.IsLetter(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_CON.IsChecked = True And Char.IsControl(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_DIG.IsChecked = True And Char.IsDigit(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_LOD.IsChecked = True And Char.IsLetterOrDigit(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_LOW.IsChecked = True And Char.IsLower(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_NUM.IsChecked = True And Char.IsNumber(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_PUN.IsChecked = True And Char.IsPunctuation(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_SEP.IsChecked = True And Char.IsSeparator(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_SYM.IsChecked = True And Char.IsSymbol(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_UPP.IsChecked = True And Char.IsUpper(Testchar) = True Then
                Result.Append(Testchar)
            ElseIf CHKBX_WHI.IsChecked = True And Char.IsWhiteSpace(Testchar) = True Then
                Result.Append(Testchar)
            End If

        Next Counter

        TXTBX14_ASCII.Text = Result.ToString


    End Sub
End Class
