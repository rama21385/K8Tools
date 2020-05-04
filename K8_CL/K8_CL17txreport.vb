Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Windows.Media.Animation

Public Class K8_CL17txreport

    Private _TestRun As New TRXtestrun
    Private _Times As New TRXtimes
    Private _TestSettings As New TRXtestsettings
    Private _Result As New TRXresults
    Private _TestDefinitions As New TRXtestdefinitions
    Private _TestEntries As New TRXtestentries
    Private _TestLists As New TRXtestlists
    Private _ResultSummary As New TRXresultsummary

    Public Property TestRun As TRXtestrun
        Get
            Return _TestRun
        End Get
        Set(value As TRXtestrun)
            _TestRun = value
        End Set
    End Property

    Public Property Times As TRXtimes
        Get
            Return _Times
        End Get
        Set(value As TRXtimes)
            _Times = value
        End Set
    End Property

    Public Property TestSettings As TRXtestsettings
        Get
            Return _TestSettings
        End Get
        Set(value As TRXtestsettings)
            _TestSettings = value
        End Set
    End Property

    Public Property Result As TRXresults
        Get
            Return _Result
        End Get
        Set(value As TRXresults)
            _Result = value
        End Set
    End Property

    Public Property TestDefinitions As TRXtestdefinitions
        Get
            Return _TestDefinitions
        End Get
        Set(value As TRXtestdefinitions)
            _TestDefinitions = value
        End Set
    End Property

    Public Property TestEntries As TRXtestentries
        Get
            Return _TestEntries
        End Get
        Set(value As TRXtestentries)
            _TestEntries = value
        End Set
    End Property

    Public Property TestLists As TRXtestlists
        Get
            Return _TestLists
        End Get
        Set(value As TRXtestlists)
            _TestLists = value
        End Set
    End Property

    Public Property ResultSummary As TRXresultsummary
        Get
            Return _ResultSummary
        End Get
        Set(value As TRXresultsummary)
            _ResultSummary = value
        End Set
    End Property

    Public Shared Sub LoadXMLIntoCollection()

        KiebitzAllAnalysis.Clear()

        If File.Exists("testresult3.txt") = True Then

            Dim document As XDocument = XDocument.Load("testresult3.txt")

            Dim TRXcomplete As IEnumerable(Of XElement) = document.Root.Elements()

            MyTRXreporter.TestRun.Id = document.Root.Attribute("id").Value
            MyTRXreporter.TestRun.Name = document.Root.Attribute("name").Value
            MyTRXreporter.TestRun.RunUser = document.Root.Attribute("runUser").Value

            For Each RootNode As XElement In TRXcomplete
                'This is very sensitive on Upper/Lower case of the attributes !!!!!!!!

                If RootNode.Name.LocalName = "Times" Then
                    MyTRXreporter.Times.Creation = CDate(RootNode.Attribute("creation").Value)
                    MyTRXreporter.Times.Queuing = CDate(RootNode.Attribute("queuing").Value)
                    MyTRXreporter.Times.Start = CDate(RootNode.Attribute("start").Value)
                    MyTRXreporter.Times.Finish = CDate(RootNode.Attribute("finish").Value)

                ElseIf RootNode.Name.LocalName = "TestSettings" Then
                    MyTRXreporter.TestSettings.Name = RootNode.Attribute("name").Value
                    MyTRXreporter.TestSettings.Id = RootNode.Attribute("id").Value
                    MyTRXreporter.TestSettings.Deployment.Rundeploymentroot = RootNode.Elements("Deployment.runDeploymentRoot").Value

                ElseIf RootNode.Name.LocalName = "Results" Then
                    For Each UnitTestResultItem As XElement In RootNode.Elements '  .Element("UnitTestResult").Elements
                        Dim TempOutput As New TRXoutput With {.Errorinfo = New TRXerrorinfo With {.Message = "", .Stacktrace = ""}}
                        If UnitTestResultItem.HasElements Then
                            For Each SingleOutputItem In UnitTestResultItem.Elements
                                If SingleOutputItem.Name.LocalName = "Output" Then
                                    For Each Item In SingleOutputItem.Elements
                                        If Item.Name.LocalName = "ErrorInfo" Then
                                            For Each ErrorInfoItem In Item.Elements
                                                If ErrorInfoItem.Name.LocalName = "Message" Then
                                                    TempOutput.Errorinfo.Message = ErrorInfoItem.Value
                                                ElseIf ErrorInfoItem.Name.LocalName = "StackTrace" Then
                                                    TempOutput.Errorinfo.Stacktrace = ErrorInfoItem.Value
                                                End If
                                            Next
                                        End If
                                    Next

                                End If
                            Next
                        End If

                        'Next
                        MyTRXreporter.Result.UnitTestResult.Add(New TRXunittestresult With {
                        .ExecutionId = UnitTestResultItem.Attribute("executionId").Value,
                        .TestId = UnitTestResultItem.Attribute("testId").Value,
                        .TestName = UnitTestResultItem.Attribute("testName").Value,
                        .ComputerName = UnitTestResultItem.Attribute("computerName").Value,
                        .Duration = TimeSpan.Parse(UnitTestResultItem.Attribute("duration").Value),
                        .StartTime = CDate(UnitTestResultItem.Attribute("startTime").Value),
                        .EndTime = CDate(UnitTestResultItem.Attribute("endTime").Value),
                        .TestType = UnitTestResultItem.Attribute("testType").Value,
                        .Outcome = UnitTestResultItem.Attribute("outcome").Value,
                        .TestListID = UnitTestResultItem.Attribute("testListId").Value,
                        .RelativeResultsDirectory = UnitTestResultItem.Attribute("relativeResultsDirectory").Value,
                        .Output = TempOutput
                        })

                    Next
                ElseIf RootNode.Name.LocalName = "TestDefinitions" Then
                    Dim TempUnitTest As New TRXunittest
                    Dim TempExecution As New TRXexecution
                    Dim TempTestmethod As New TRXtestmethod

                    For Each UnitTestItem As XElement In RootNode.Elements
                        If UnitTestItem.Name.LocalName = "UnitTest" Then
                            TempUnitTest = New TRXunittest With {
                            .Name = UnitTestItem.Attribute("name").Value,
                            .Storage = UnitTestItem.Attribute("storage").Value,
                            .Id = UnitTestItem.Attribute("id").Value}
                            If UnitTestItem.HasElements Then
                                For Each UnitTestSubItem In UnitTestItem.Elements
                                    If UnitTestSubItem.Name.LocalName = "Execution" Then
                                        TempExecution = New TRXexecution With {.Id = UnitTestSubItem.Attribute("id").Value}
                                    End If
                                    If UnitTestSubItem.Name.LocalName = "TestMethod" Then
                                        TempTestmethod = New TRXtestmethod With {
                                        .Codebase = UnitTestSubItem.Attribute("codeBase").Value,
                                        .Adaptertypename = UnitTestSubItem.Attribute("adapterTypeName").Value,
                                        .Classname = UnitTestSubItem.Attribute("className").Value,
                                        .Name = UnitTestSubItem.Attribute("name").Value}
                                    End If
                                Next
                            End If
                        End If

                        TempUnitTest.Execution = TempExecution
                        TempUnitTest.Testmethod = TempTestmethod
                        MyTRXreporter.TestDefinitions.UnitTest.Add(TempUnitTest)

                    Next
                ElseIf RootNode.Name.LocalName = "TestEntries" Then
                    For Each TestEntryItem As XElement In RootNode.Elements

                        MyTRXreporter.TestEntries.Testentry.Add(New TRXtestentry With {
                        .Testid = TestEntryItem.Attribute("testId").Value,
                        .Executionid = TestEntryItem.Attribute("executionId").Value,
                        .Testlistid = TestEntryItem.Attribute("testListId").Value
                        })

                    Next
                ElseIf RootNode.Name.LocalName = "TestLists" Then
                    For Each TestListItem As XElement In RootNode.Elements

                        MyTRXreporter.TestLists.Testlist.Add(New TRXtestlist With {
                        .Name = TestListItem.Attribute("name").Value,
                        .Id = TestListItem.Attribute("id").Value
                        })

                    Next
                ElseIf RootNode.Name.LocalName = "ResultSummary" Then
                    MyTRXreporter.ResultSummary.Outcome = RootNode.Attribute("outcome").Value
                    For Each CountersItem As XElement In RootNode.Elements
                        If CountersItem.Name.LocalName = "Counters" Then
                            MyTRXreporter.ResultSummary.Counters.Total = CInt(CountersItem.Attribute("total").Value)
                            MyTRXreporter.ResultSummary.Counters.Executed = CInt(CountersItem.Attribute("executed").Value)
                            MyTRXreporter.ResultSummary.Counters.Passed = CInt(CountersItem.Attribute("passed").Value)
                            MyTRXreporter.ResultSummary.Counters.Failed = CInt(CountersItem.Attribute("failed").Value)
                            MyTRXreporter.ResultSummary.Counters.Error = CInt(CountersItem.Attribute("error").Value)
                            MyTRXreporter.ResultSummary.Counters.Timeout = CInt(CountersItem.Attribute("timeout").Value)
                            MyTRXreporter.ResultSummary.Counters.Aborted = CInt(CountersItem.Attribute("aborted").Value)
                            MyTRXreporter.ResultSummary.Counters.Inconclusive = CInt(CountersItem.Attribute("inconclusive").Value)
                            MyTRXreporter.ResultSummary.Counters.Passedbutrunaborted = CInt(CountersItem.Attribute("passedButRunAborted").Value)
                            MyTRXreporter.ResultSummary.Counters.Notrunnable = CInt(CountersItem.Attribute("notRunnable").Value)
                            MyTRXreporter.ResultSummary.Counters.Notexecuted = CInt(CountersItem.Attribute("notExecuted").Value)
                            MyTRXreporter.ResultSummary.Counters.Disconnected = CInt(CountersItem.Attribute("disconnected").Value)
                            MyTRXreporter.ResultSummary.Counters.Warning = CInt(CountersItem.Attribute("warning").Value)
                            MyTRXreporter.ResultSummary.Counters.Completed = CInt(CountersItem.Attribute("completed").Value)
                            MyTRXreporter.ResultSummary.Counters.Inprogress = CInt(CountersItem.Attribute("inProgress").Value)
                            MyTRXreporter.ResultSummary.Counters.Pending = CInt(CountersItem.Attribute("pending").Value)
                        End If

                    Next
                End If
            Next

        End If
    End Sub


End Class

Public Class TRXtestrun

    Private _id As String
    Private _name As String
    Private _runUser As String

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Property RunUser As String
        Get
            Return _runUser
        End Get
        Set(value As String)
            _runUser = value
        End Set
    End Property
End Class

Public Class TRXtimes

    Private _Creation As DateTime
    Private _queuing As DateTime
    Private _start As DateTime
    Private _finish As DateTime

    Public Property Creation As Date
        Get
            Return _Creation
        End Get
        Set(value As Date)
            _Creation = value
        End Set
    End Property

    Public Property Queuing As Date
        Get
            Return _queuing
        End Get
        Set(value As Date)
            _queuing = value
        End Set
    End Property

    Public Property Start As Date
        Get
            Return _start
        End Get
        Set(value As Date)
            _start = value
        End Set
    End Property

    Public Property Finish As Date
        Get
            Return _finish
        End Get
        Set(value As Date)
            _finish = value
        End Set
    End Property
End Class

Public Class TRXtestsettings

    Private _name As String
    Private _id As String
    Private _deployment As New TRXdeployment

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property

    Public Property Deployment As TRXdeployment
        Get
            Return _deployment
        End Get
        Set(value As TRXdeployment)
            _deployment = value
        End Set
    End Property
End Class

Public Class TRXdeployment

    Private _rundeploymentroot As String

    Public Property Rundeploymentroot As String
        Get
            Return _rundeploymentroot
        End Get
        Set(value As String)
            _rundeploymentroot = value
        End Set
    End Property
End Class

#Region "RESULTS"

Public Class TRXresults

    Private _UnitTestResult As New ObservableCollection(Of TRXunittestresult)

    Public Property UnitTestResult As ObservableCollection(Of TRXunittestresult)
        Get
            Return _UnitTestResult
        End Get
        Set(value As ObservableCollection(Of TRXunittestresult))
            _UnitTestResult = value
        End Set
    End Property
End Class
Public Class TRXunittestresult

    Private _executionId As String
    Private _testId As String
    Private _testName As String
    Private _computerName As String
    Private _duration As TimeSpan
    Private _startTime As DateTime
    Private _endTime As DateTime
    Private _testType As String
    Private _outcome As String
    Private _testListID As String
    Private _relativeResultsDirectory As String
    Private _output As New TRXoutput

    Public Property ExecutionId As String
        Get
            Return _executionId
        End Get
        Set(value As String)
            _executionId = value
        End Set
    End Property

    Public Property TestId As String
        Get
            Return _testId
        End Get
        Set(value As String)
            _testId = value
        End Set
    End Property

    Public Property TestName As String
        Get
            Return _testName
        End Get
        Set(value As String)
            _testName = value
        End Set
    End Property

    Public Property ComputerName As String
        Get
            Return _computerName
        End Get
        Set(value As String)
            _computerName = value
        End Set
    End Property

    Public Property Duration As TimeSpan
        Get
            Return _duration
        End Get
        Set(value As TimeSpan)
            _duration = value
        End Set
    End Property

    Public Property StartTime As Date
        Get
            Return _startTime
        End Get
        Set(value As Date)
            _startTime = value
        End Set
    End Property

    Public Property EndTime As Date
        Get
            Return _endTime
        End Get
        Set(value As Date)
            _endTime = value
        End Set
    End Property

    Public Property TestType As String
        Get
            Return _testType
        End Get
        Set(value As String)
            _testType = value
        End Set
    End Property

    Public Property Outcome As String
        Get
            Return _outcome
        End Get
        Set(value As String)
            _outcome = value
        End Set
    End Property

    Public Property TestListID As String
        Get
            Return _testListID
        End Get
        Set(value As String)
            _testListID = value
        End Set
    End Property

    Public Property RelativeResultsDirectory As String
        Get
            Return _relativeResultsDirectory
        End Get
        Set(value As String)
            _relativeResultsDirectory = value
        End Set
    End Property

    Public Property Output As TRXoutput
        Get
            Return _output
        End Get
        Set(value As TRXoutput)
            _output = value
        End Set
    End Property
End Class

Public Class TRXoutput

    Private _errorinfo As New TRXerrorinfo

    Public Property Errorinfo As TRXerrorinfo
        Get
            Return _errorinfo
        End Get
        Set(value As TRXerrorinfo)
            _errorinfo = value
        End Set
    End Property
End Class

Public Class TRXerrorinfo

    Private _message As String
    Private _stacktrace As String

    Public Property Message As String
        Get
            Return _message
        End Get
        Set(value As String)
            _message = value
        End Set
    End Property

    Public Property Stacktrace As String
        Get
            Return _stacktrace
        End Get
        Set(value As String)
            _stacktrace = value
        End Set
    End Property
End Class


#End Region

Public Class TRXtestdefinitions

    Private _UnitTest As New ObservableCollection(Of TRXunittest)

    Public Property UnitTest As ObservableCollection(Of TRXunittest)
        Get
            Return _UnitTest
        End Get
        Set(value As ObservableCollection(Of TRXunittest))
            _UnitTest = value
        End Set
    End Property
End Class
Public Class TRXunittest

    Private _name As String
    Private _storage As String
    Private _id As String
    Private _execution As New TRXexecution
    Private _testmethod As New TRXtestmethod

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Property Storage As String
        Get
            Return _storage
        End Get
        Set(value As String)
            _storage = value
        End Set
    End Property

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property

    Public Property Execution As TRXexecution
        Get
            Return _execution
        End Get
        Set(value As TRXexecution)
            _execution = value
        End Set
    End Property

    Public Property Testmethod As TRXtestmethod
        Get
            Return _testmethod
        End Get
        Set(value As TRXtestmethod)
            _testmethod = value
        End Set
    End Property
End Class
Public Class TRXexecution

    Private _id As String

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property
End Class
Public Class TRXtestmethod

    Private _codebase As String
    Private _adaptertypename As String
    Private _classname As String
    Private _name As String

    Public Property Codebase As String
        Get
            Return _codebase
        End Get
        Set(value As String)
            _codebase = value
        End Set
    End Property

    Public Property Adaptertypename As String
        Get
            Return _adaptertypename
        End Get
        Set(value As String)
            _adaptertypename = value
        End Set
    End Property

    Public Property Classname As String
        Get
            Return _classname
        End Get
        Set(value As String)
            _classname = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
End Class

Public Class TRXtestentries

    Private _testentry As New ObservableCollection(Of TRXtestentry)

    Public Property Testentry As ObservableCollection(Of TRXtestentry)
        Get
            Return _testentry
        End Get
        Set(value As ObservableCollection(Of TRXtestentry))
            _testentry = value
        End Set
    End Property
End Class
Public Class TRXtestentry

    Private _testid As String
    Private _executionid As String
    Private _testlistid As String

    Public Property Testid As String
        Get
            Return _testid
        End Get
        Set(value As String)
            _testid = value
        End Set
    End Property

    Public Property Executionid As String
        Get
            Return _executionid
        End Get
        Set(value As String)
            _executionid = value
        End Set
    End Property

    Public Property Testlistid As String
        Get
            Return _testlistid
        End Get
        Set(value As String)
            _testlistid = value
        End Set
    End Property
End Class

Public Class TRXtestlists

    Private _testlist As New ObservableCollection(Of TRXtestlist)

    Public Property Testlist As ObservableCollection(Of TRXtestlist)
        Get
            Return _testlist
        End Get
        Set(value As ObservableCollection(Of TRXtestlist))
            _testlist = value
        End Set
    End Property
End Class
Public Class TRXtestlist

    Private _name As String
    Private _id As String

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property
End Class

Public Class TRXresultsummary

    Private _outcome As String
    Private _counters As New TRXcounters

    Public Property Outcome As String
        Get
            Return _outcome
        End Get
        Set(value As String)
            _outcome = value
        End Set
    End Property

    Public Property Counters As TRXcounters
        Get
            Return _counters
        End Get
        Set(value As TRXcounters)
            _counters = value
        End Set
    End Property
End Class
Public Class TRXcounters

    Private _total As Int32
    Private _executed As Int32
    Private _passed As Int32
    Private _failed As Int32
    Private _error As Int32
    Private _timeout As Int32
    Private _aborted As Int32
    Private _inconclusive As Int32
    Private _passedbutrunaborted As Int32
    Private _notrunnable As Int32
    Private _notexecuted As Int32
    Private _disconnected As Int32
    Private _warning As Int32
    Private _completed As Int32
    Private _inprogress As Int32
    Private _pending As Int32

    Public Property Total As Integer
        Get
            Return _total
        End Get
        Set(value As Integer)
            _total = value
        End Set
    End Property

    Public Property Executed As Integer
        Get
            Return _executed
        End Get
        Set(value As Integer)
            _executed = value
        End Set
    End Property

    Public Property Passed As Integer
        Get
            Return _passed
        End Get
        Set(value As Integer)
            _passed = value
        End Set
    End Property

    Public Property Failed As Integer
        Get
            Return _failed
        End Get
        Set(value As Integer)
            _failed = value
        End Set
    End Property

    Public Property [Error] As Integer
        Get
            Return _error
        End Get
        Set(value As Integer)
            _error = value
        End Set
    End Property

    Public Property Timeout As Integer
        Get
            Return _timeout
        End Get
        Set(value As Integer)
            _timeout = value
        End Set
    End Property

    Public Property Aborted As Integer
        Get
            Return _aborted
        End Get
        Set(value As Integer)
            _aborted = value
        End Set
    End Property

    Public Property Inconclusive As Integer
        Get
            Return _inconclusive
        End Get
        Set(value As Integer)
            _inconclusive = value
        End Set
    End Property

    Public Property Passedbutrunaborted As Integer
        Get
            Return _passedbutrunaborted
        End Get
        Set(value As Integer)
            _passedbutrunaborted = value
        End Set
    End Property

    Public Property Notrunnable As Integer
        Get
            Return _notrunnable
        End Get
        Set(value As Integer)
            _notrunnable = value
        End Set
    End Property

    Public Property Notexecuted As Integer
        Get
            Return _notexecuted
        End Get
        Set(value As Integer)
            _notexecuted = value
        End Set
    End Property

    Public Property Disconnected As Integer
        Get
            Return _disconnected
        End Get
        Set(value As Integer)
            _disconnected = value
        End Set
    End Property

    Public Property Warning As Integer
        Get
            Return _warning
        End Get
        Set(value As Integer)
            _warning = value
        End Set
    End Property

    Public Property Completed As Integer
        Get
            Return _completed
        End Get
        Set(value As Integer)
            _completed = value
        End Set
    End Property

    Public Property Inprogress As Integer
        Get
            Return _inprogress
        End Get
        Set(value As Integer)
            _inprogress = value
        End Set
    End Property

    Public Property Pending As Integer
        Get
            Return _pending
        End Get
        Set(value As Integer)
            _pending = value
        End Set
    End Property
End Class
