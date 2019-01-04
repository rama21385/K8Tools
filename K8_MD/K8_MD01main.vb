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

    Public ListOfMonths() As Int32 = {0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
    Public ListofQuarters() As Int32 = {0, 31 + 29 + 31,
                                           30 + 31 + 30,
                                           31 + 31 + 30,
                                           31 + 30 + 31}
    Public ListOfHalfYears() As Int32 = {0, 31 + 29 + 31 + 30 + 31 + 30,
                                            31 + 31 + 30 + 31 + 30 + 31}
    Public ListOfYear() As Int32 = {0, 366}

End Module
