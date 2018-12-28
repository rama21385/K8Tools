Imports System.Collections.ObjectModel

Module K8_MD01main

    Public Enum UserControlItems
        none = 0
        UC01settings = 1
        UC02input = 2
        UC03analyse = 3

    End Enum

    Public NumberOfActiveMenu As UserControlItems

    Public KiebitzCats As New ObservableCollection(Of K8_CL02category)
    Public KiebitzCurve As New ObservableCollection(Of K8_CL03measurement)
    Public KiebitzAnalyse As New ObservableCollection(Of K8_CL04analyse)
    Public KiebitzAllAnalysis As New ObservableCollection(Of K8_CL05analysis)

End Module
