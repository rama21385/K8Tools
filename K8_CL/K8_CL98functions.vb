Public Class K8_CL98functions

    Public Shared Function GetCategoryID(CatName As String, CatYear As UInt16) As String

        If String.IsNullOrEmpty(CatName.Trim) = True AndAlso CatYear = 0 Then
            Return String.Empty
        Else
            Return CatName.Trim.ToUpper & Format(CatYear, "0000")
        End If

    End Function

End Class
