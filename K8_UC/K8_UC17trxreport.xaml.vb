Imports System.Windows.Markup
Imports Microsoft.VisualBasic.ApplicationServices

Public Class K8_UC17trxreport

    Dim CounterLinesOrItems As Int32
    Dim EnoughIsEnough As Boolean
    Dim ItemToPrintIndex As Int32
    Dim ItemToPrintIndexLastUsed As Int32

    Dim PrintAreaWidth As Single = 96 * (8.27 - 1)
    Dim PrintAreaHeight As Single = 96 * (11.96 - 1)

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Private Function CreateTRXDocument() As FixedDocument

        Return CreateFixedDocumentWithPages()

    End Function

    Private Function CreateFixedDocumentWithPages() As FixedDocument

        Dim PageCurrentNr As Int32
        Dim PageTotalNr As Int32
        EnoughIsEnough = False

        Dim TempFixedDocument As New FixedDocument
        TempFixedDocument.DocumentPaginator.PageSize = New Size(96 * 8.27, 96 * 11.96)
        Do
            PageCurrentNr += 1
            PageTotalNr += 1
            ItemToPrintIndex = -1
            CounterLinesOrItems = 0
            Dim TempPageContent As PageContent = CreatePageContent(PageCurrentNr, PageTotalNr)
        Loop Until EnoughIsEnough = True

        PageCurrentNr = 0
        EnoughIsEnough = False
        ItemToPrintIndexLastUsed = -1

        Do
            PageCurrentNr += 1
            ItemToPrintIndex = -1
            CounterLinesOrItems = 0
            Dim TempPageContent As PageContent = CreatePageContent(PageCurrentNr, PageTotalNr)
            TempFixedDocument.Pages.Add(TempPageContent)
        Loop Until EnoughIsEnough = True

        Return TempFixedDocument
    End Function

    Private Function CreatePageContent(PageNr As Int32, PageTotalNr As Int32) As PageContent

        Dim PageContent As New PageContent
        Dim MyPage As FixedPage = New FixedPage With {.Width = 96 * 8.27, .Height = 96 * 11.96, .Background = Brushes.White}

        Dim TRXheader As UIElement = CreateHeader(PageNr, PageTotalNr)
        FixedPage.SetLeft(TRXheader, 96 * 0.5)
        FixedPage.SetTop(TRXheader, 96 * 0.5)
        MyPage.Children.Add(TRXheader)

        Dim NewFrame As New Border With {
            .Width = PrintAreaWidth,
            .Height = PrintAreaHeight,
            .BorderBrush = Brushes.MidnightBlue,
            .BorderThickness = New Thickness(2, 2, 2, 2)}
        FixedPage.SetLeft(NewFrame, 96 * 0.5)
        FixedPage.SetTop(NewFrame, 96 * 0.5)
        MyPage.Children.Add(NewFrame)

        Dim TRXpagereport As UIElement = CreatePageReport(PageNr, PageTotalNr)
        FixedPage.SetLeft(TRXpagereport, 96 * 0.5)
        FixedPage.SetTop(TRXpagereport, 96 * 0.5)
        MyPage.Children.Add(TRXpagereport)


        'EnoughIsEnough = True
        CType(PageContent, IAddChild).AddChild(MyPage)
        Return PageContent

    End Function

    Private Function CreateHeader(PageNr As Int32, PageTotalNr As Int32) As Canvas

        Dim Canvas1 As Canvas = New Canvas With {.Width = PrintAreaWidth, .Height = PrintAreaHeight}

        Dim TitleHeader As TextBlock = New TextBlock
        With TitleHeader
            .Foreground = Brushes.Black
            .FontFamily = New System.Windows.Media.FontFamily("Arial")
            .FontWeight = FontWeights.Bold
            .FontSize = 36
            .Text = "Visual Studio TRX Report"
            .Width = Canvas1.Width
            .TextAlignment = TextAlignment.Center
            .Arrange(New Rect(0, 0, Canvas1.Width, 1000))
        End With
        Canvas.SetLeft(TitleHeader, 0)
        Canvas.SetTop(TitleHeader, 0)
        Canvas1.Children.Add(TitleHeader)

        Dim TitleSubHeader1 As TextBlock = New TextBlock
        With TitleSubHeader1
            .Foreground = Brushes.Black
            .FontFamily = New System.Windows.Media.FontFamily("Arial")
            .FontWeight = FontWeights.Bold
            .FontSize = 18
            .Text = "XPhys"
            .Width = Canvas1.Width
            .TextAlignment = TextAlignment.Center
            .Arrange(New Rect(0, 0, Canvas1.Width, 1000))
        End With
        Canvas.SetLeft(TitleSubHeader1, 0)
        Canvas.SetTop(TitleSubHeader1, TitleHeader.DesiredSize.Height)
        Canvas1.Children.Add(TitleSubHeader1)

        Dim TitleSubHeader2 As TextBlock = New TextBlock
        With TitleSubHeader2
            .Foreground = Brushes.Black
            .FontFamily = New System.Windows.Media.FontFamily("Arial")
            .FontWeight = FontWeights.Bold
            .FontSize = 14
            .Text = "Page " & PageNr & "/" & PageTotalNr & " - " & FormatDateTime(Now(), DateFormat.LongDate) & " - " & FormatDateTime(Now(), DateFormat.LongTime)
            .Width = Canvas1.Width
            .TextAlignment = TextAlignment.Center
            .Arrange(New Rect(0, 0, Canvas1.Width, 1000))
        End With
        Canvas.SetLeft(TitleSubHeader2, 0)
        Canvas.SetTop(TitleSubHeader2, TitleHeader.DesiredSize.Height + TitleSubHeader1.DesiredSize.Height)
        Canvas1.Children.Add(TitleSubHeader2)

        Return Canvas1

    End Function

    Private Function CreatePageReport(PageNr As Int32, PageTotalNr As Int32) As Canvas

        Dim MaxNrOfLinesReached As Boolean = False
        Dim Canvas1 As Canvas = New Canvas With {.Width = PrintAreaWidth, .Height = PrintAreaHeight}
        Dim Padding1 As Int32 = 30
        Dim Indent1 As Int32 = 5

        If PageNr = 1 Then

            Dim SectionHeader1 As TextBlock = New TextBlock
            With SectionHeader1
                .Foreground = Brushes.Black
                .FontFamily = New System.Windows.Media.FontFamily("Arial")
                .FontWeight = FontWeights.Bold
                .FontSize = 14
                If PageNr = 1 Then
                    .Text = "Test Summary"
                End If
                .Width = Canvas1.Width
                .TextAlignment = TextAlignment.Center
                .Arrange(New Rect(0, 0, Canvas1.Width, 1000))
            End With
            Canvas.SetLeft(SectionHeader1, 0)
            Canvas.SetTop(SectionHeader1, ConvMMToInch(35) * 96)
            Canvas1.Children.Add(SectionHeader1)


            Dim SectionHeader2 As TextBlock = New TextBlock
            With SectionHeader2
                .Foreground = Brushes.Black
                .FontFamily = New System.Windows.Media.FontFamily("Arial")
                .FontWeight = FontWeights.Bold
                .FontSize = 14
                If PageNr = 1 Then
                    .Text = "Result Summary: " & MyTRXreporter.ResultSummary.Outcome
                End If
                .Width = Canvas1.Width
                .TextAlignment = TextAlignment.Center
                .Arrange(New Rect(0, 0, Canvas1.Width, 1000))
            End With
            Canvas.SetLeft(SectionHeader2, 0)
            Canvas.SetTop(SectionHeader2, ConvMMToInch(100) * 96)
            Canvas1.Children.Add(SectionHeader2)


            Dim Page1ContentSection1 As New TextBlock
            With Page1ContentSection1
                .Foreground = Brushes.Black
                .FontFamily = New System.Windows.Media.FontFamily("Courier New")
                .FontWeight = FontWeights.Bold
                .FontSize = 10
                .Width = Canvas1.Width
                .Text = ""
                .TextAlignment = TextAlignment.Left
            End With

            Dim Page1ContentSection2 As New TextBlock
            With Page1ContentSection2
                .Foreground = Brushes.Black
                .FontFamily = New System.Windows.Media.FontFamily("Courier New")
                .FontWeight = FontWeights.Bold
                .FontSize = 10
                .Width = Canvas1.Width
                .Text = ""
                .TextAlignment = TextAlignment.Left
            End With

            Page1ContentSection1.Text &= Space(Indent1) & "Windows User: ".PadRight(Padding1) & MyTRXreporter.TestRun.RunUser
            Page1ContentSection1.Text &= vbCrLf & Space(Indent1) & "Test Start: ".PadRight(Padding1) & MyTRXreporter.Times.Start.ToString
            Page1ContentSection1.Text &= vbCrLf & Space(Indent1) & "Test Finish: ".PadRight(Padding1) & MyTRXreporter.Times.Finish.ToString

            'Summary Information
            Page1ContentSection2.Text &= Space(Indent1) & "Aborted: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Aborted.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Complete: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Completed.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Disconnected: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Disconnected.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Error: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Error.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Executed: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Executed.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Failed: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Failed.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Inconclusive: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Inconclusive.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "In progress: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Inprogress.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Not executed: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Notexecuted.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Not runnable: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Notrunnable.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Passed: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Passed.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Passed but run aborted: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Passedbutrunaborted.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Pending: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Pending.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Timeout: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Timeout.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Total: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Total.ToString
            Page1ContentSection2.Text &= vbCrLf & Space(Indent1) & "Warning: ".PadRight(Padding1) & MyTRXreporter.ResultSummary.Counters.Warning.ToString

            Page1ContentSection1.Width = Canvas1.Width
            Page1ContentSection1.Arrange(New Rect(0, 0, Canvas1.Width, 1000))
            Page1ContentSection2.Width = Canvas1.Width
            Page1ContentSection2.Arrange(New Rect(0, 0, Canvas1.Width, 1000))

            Canvas.SetLeft(Page1ContentSection1, 0)
            Canvas.SetTop(Page1ContentSection1, ConvMMToInch(45) * 96)
            Canvas1.Children.Add(Page1ContentSection1)

            Canvas.SetLeft(Page1ContentSection2, 0)
            Canvas.SetTop(Page1ContentSection2, ConvMMToInch(110) * 96)
            Canvas1.Children.Add(Page1ContentSection2)

        ElseIf PageNr >= 2 Then

            Dim Page2Content As New TextBlock
            With Page2Content
                .Foreground = Brushes.Black
                .FontFamily = New System.Windows.Media.FontFamily("Courier New")
                .FontWeight = FontWeights.Bold
                .FontSize = 10
                .Width = Canvas1.Width
                .Text = ""
                .TextWrapping = TextWrapping.Wrap
                .TextAlignment = TextAlignment.Left
            End With

            For Each TestResultItem In MyTRXreporter.Result.UnitTestResult
                ItemToPrintIndex += 1
                If ItemToPrintIndex > ItemToPrintIndexLastUsed Then
                    CounterLinesOrItems += 1
                    Page2Content.Text &= vbCrLf & Space(Indent1) & "Test Name: ".PadRight(Padding1) & TestResultItem.TestName
                    Page2Content.Text &= vbCrLf & Space(Indent1) & "Computer Name: ".PadRight(Padding1) & TestResultItem.ComputerName
                    For Each TestDefinitionItem In MyTRXreporter.TestDefinitions.UnitTest
                        If TestResultItem.TestId = TestDefinitionItem.Id Then
                            Page2Content.Text &= vbCrLf & Space(Indent1) & "Class Name: ".PadRight(Padding1) & TestDefinitionItem.Testmethod.Classname
                        End If
                    Next
                    Page2Content.Text &= vbCrLf & Space(Indent1) & "Start Time: ".PadRight(Padding1) & TestResultItem.StartTime
                    Page2Content.Text &= vbCrLf & Space(Indent1) & "End Time: ".PadRight(Padding1) & TestResultItem.EndTime
                    Page2Content.Text &= vbCrLf & Space(Indent1) & "Duration: ".PadRight(Padding1) & TestResultItem.Duration.ToString
                    Page2Content.Text &= vbCrLf & Space(Indent1) & "Outcome: ".PadRight(Padding1) & TestResultItem.Outcome
                    If TestResultItem.Outcome.ToUpper = "FAILED" Then

                        Page2Content.Text &= vbCrLf & Space(Indent1) & "     Message: ".PadRight(Padding1) & TestResultItem.Output.Errorinfo.Message
                        Page2Content.Text &= vbCrLf & Space(Indent1) & "     StackTrace: ".PadRight(Padding1) & TestResultItem.Output.Errorinfo.Stacktrace

                    End If
                    Page2Content.Text &= vbCrLf & StrDup(75, "-"c)

                End If
                'separator line between items
                If CounterLinesOrItems >= 5 Then
                    ItemToPrintIndexLastUsed = ItemToPrintIndex
                    MaxNrOfLinesReached = True
                    Exit For
                End If


            Next

            Canvas.SetLeft(Page2content, 0)
            Canvas.SetTop(Page2Content, ConvMMToInch(45) * 96)
            Canvas1.Children.Add(Page2content)

        End If

        If ItemToPrintIndex >= MyTRXreporter.Result.UnitTestResult.Count - 1 Then
            EnoughIsEnough = True
        End If

        Return Canvas1

    End Function

    Private Function ConvMMToInch(Millimeter As Single) As Single
        Return CSng(Millimeter / 25.4)
    End Function

    Private Sub DoThingsWhenLoaded(sender As Object, e As RoutedEventArgs)

        K8_CL17txreport.LoadXMLIntoCollection()

        'Me.Content = MyTRXreporter.Result.UnitTestResult(0).ComputerName

        Dim TRXdocument As FixedDocument = CreateTRXDocument()
        DCMTVWR.Document = TryCast(TRXdocument, IDocumentPaginatorSource)
        DCMTVWR.FitToWidth()

    End Sub
End Class
