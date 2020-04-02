Imports System.Collections.ObjectModel

Module K8_MD01main

    Public Enum UserControlItems
        none = 0
        UC01settings = 1
        UC02input = 2
        UC03analyse = 3

        UC11password = 11
        UC12stringformat = 12
        UC13colors = 13
        UC14ascii = 14
        UC15mosaiqcrc = 15
        UC16math = 16

    End Enum

    Public NumberOfActiveMenu As UserControlItems

    Public KiebitzCategories As New ObservableCollection(Of K8_CL02category)
    Public KiebitzMeasCurve As New ObservableCollection(Of K8_CL03measurement)
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
    'Public ListOfStatItems() As Int32 = {0, 50, 100}
    Public ListOfStatItems As New ArrayList From {0, 50, 100}

    Public K8_DirHome As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\..\Kiebitz8\"
    Public K8_DirCSV As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\..\Kiebitz8\Import_CSV\"
    Public K8_DirReport As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\..\Kiebitz8\Reports\"
    Public K8_DirSettings As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\..\Kiebitz8\Settings\"


End Module
