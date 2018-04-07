Imports System.Collections.ObjectModel

Module K8_MD01main

    Public Enum UserControlItems
        none = 0
        UC01settings = 1
        UC02input = 2
    End Enum

    Public NumberOfActiveMenu As UserControlItems

    Public Kiebitz As New ObservableCollection(Of K8_CL02category)
    Public MyCurve1 As New ObservableCollection(Of K8_CL03measurement)
    Public MyCurve2 As New ObservableCollection(Of K8_CL03measurement)

End Module
