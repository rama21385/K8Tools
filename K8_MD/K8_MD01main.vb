Imports System.Collections.ObjectModel

Module K8_MD01main

    Public Enum UserControlItems
        none = 0
        UC01settings = 1
        UC02input = 2
    End Enum

    Public NumberOfActiveMenu As UserControlItems

    Public KiebitzCats As New ObservableCollection(Of K8_CL02category)
    Public KiebitzCurve As New ObservableCollection(Of K8_CL03measurement)

End Module
