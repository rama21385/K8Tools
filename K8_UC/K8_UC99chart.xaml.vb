Imports System.Collections.ObjectModel

Public Class K8_UC99chart

    Private ReadOnly CNVSmarginLeft As Double = 25
    Private ReadOnly CNVSmarginRight As Double = 10
    Private ReadOnly CNVSmarginTop As Double = 10
    Private ReadOnly CNVSmarginBottom As Double = 25
    Private CNVSwidth As Double = 0
    Private CNVSheight As Double = 0
    Private ChartWidthPixel As Double = 0
    Private ChartHeightPixel As Double = 0

    Public SelectedCategory As New K8_CL02category
    Private ChartYRangeValue As Double = 0

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub RefreshChart(StatisticChart As Boolean)

        Dim ChartMonths As Int32 = 0
        Dim ChartMonthIndex As Int32 = 0
        Dim ChartStatItems As Int32 = 0
        Dim ChartStatItemIndex As Int32 = 0
        Dim Xticks As New Line
        Dim Xgrid As New Line

        CNVS99.Children.Clear()

        Dim PatternX As DoubleCollection = New DoubleCollection()
        PatternX.Add(1)
        PatternX.Add(2)

        CNVSwidth = Me.ActualWidth
        CNVSheight = Me.ActualHeight
        ChartWidthPixel = CNVSwidth - CNVSmarginLeft - CNVSmarginRight
        ChartHeightPixel = CNVSheight - CNVSmarginTop - CNVSmarginBottom


        ChartYRangeValue = (Math.Abs(SelectedCategory.CategoryChartYMin) + Math.Abs(SelectedCategory.CategoryChartYMax))
        If ChartYRangeValue = 0 Then ChartYRangeValue = 100
        If SelectedCategory.CategoryChartYMax = 0 Then SelectedCategory.CategoryChartYMax = 100

        Dim Yticks As New Line
        Dim Ygrid As New Line
        Dim StepFactor As Int32 = 10
        If Math.Abs(SelectedCategory.CategoryChartYMin) + Math.Abs(SelectedCategory.CategoryChartYMax) <= 10 Then
            StepFactor = 1
        ElseIf Math.Abs(SelectedCategory.CategoryChartYMin) + Math.Abs(SelectedCategory.CategoryChartYMax) <= 100 Then
            StepFactor = 10
        ElseIf Math.Abs(SelectedCategory.CategoryChartYMin) + Math.Abs(SelectedCategory.CategoryChartYMax) <= 1000 Then
            StepFactor = 100
        ElseIf Math.Abs(SelectedCategory.CategoryChartYMin) + Math.Abs(SelectedCategory.CategoryChartYMax) <= 10000 Then
            StepFactor = 1000
        Else
            StepFactor = 10
        End If

        For Each MinMax As Int32 In {SelectedCategory.CategoryChartYMin, SelectedCategory.CategoryChartYMax}
            If MinMax <> 0 Then

                For I As Int32 = 0 To MinMax Step (CInt(MinMax / Math.Abs(MinMax)) * StepFactor)
                    Ygrid = New Line
                    Yticks = New Line
                    DesignYgrid(Ygrid, I)
                    DesignYticks(Yticks, I)

                    CNVS99.Children.Add(Ygrid)
                    CNVS99.Children.Add(Yticks)
                Next
            End If
        Next


        If StatisticChart = False Then
            For Each ChartMonth As Int32 In ListOfMonths
                ChartMonths += ChartMonth
                ChartMonthIndex += 1
                Xgrid = New Line
                Xticks = New Line

                DesignXgridMonth(Xgrid, ChartMonths, 366)
                DesignXticksMonth(Xticks, ChartMonths, 366)

                CNVS99.Children.Add(Xgrid)
                CNVS99.Children.Add(Xticks)

                If ChartMonthIndex <= 12 Then

                    Dim XaxisLabelMonths As New TextBlock
                    DesignXaxisMonthLabels(XaxisLabelMonths, ChartMonthIndex, ChartMonths, 366)
                    CNVS99.Children.Add(XaxisLabelMonths)

                End If
            Next

            Dim AxisX As New Line
            DesignXaxis(AxisX)
            CNVS99.Children.Add(AxisX)
        Else
            For Each ChartStatItem As Int32 In ListOfStatItems
                ChartStatItems = ChartStatItem
                ChartStatItemIndex += 1
                Xgrid = New Line
                Xticks = New Line

                DesignXgridMonth(Xgrid, ChartStatItems, 100)
                DesignXticksMonth(Xticks, ChartStatItems, 100)

                CNVS99.Children.Add(Xgrid)
                CNVS99.Children.Add(Xticks)

                'If ChartMonthIndex <= 12 Then

                '    Dim XaxisLabelMonths As New TextBlock
                '    DesignXaxisMonthLabels(XaxisLabelMonths, ChartMonthIndex, ChartMonths, 100)
                '    CNVS99.Children.Add(XaxisLabelMonths)

                'End If
            Next

            Dim AxisX As New Line
            DesignXaxis(AxisX)
            CNVS99.Children.Add(AxisX)
        End If

        Dim AxisY As New Line
        DesignYaxis(AxisY)
        CNVS99.Children.Add(AxisY)



    End Sub

    Public Sub AddStatisticLinesToChart(ByRef CurveToAdd As ObservableCollection(Of K8_CL03measurement))

        Dim ChartMonths As Int32 = 0
        Dim ChartMonthIndex As Int32 = 0
        Dim ChartQuarters As Int32 = 0
        Dim ChartQuarterIndex As Int32 = 0
        Dim Ymonth As Decimal = 0

        Dim Xold As Decimal = 0
        Dim ChartLine As New Line

        ChartMonths = 0
        ChartMonthIndex = 0
        For Each ChartMonth In ListOfMonths
            ChartMonths += ChartMonth
            ChartMonthIndex += 1
            ChartLine = New Line

            If ChartMonthIndex = 2 Then Ymonth = SelectedCategory.CurveAvgJan
            If ChartMonthIndex = 3 Then Ymonth = SelectedCategory.CurveAvgFeb
            If ChartMonthIndex = 4 Then Ymonth = SelectedCategory.CurveAvgMar
            If ChartMonthIndex = 5 Then Ymonth = SelectedCategory.CurveAvgApr
            If ChartMonthIndex = 6 Then Ymonth = SelectedCategory.CurveAvgMay
            If ChartMonthIndex = 7 Then Ymonth = SelectedCategory.CurveAvgJun
            If ChartMonthIndex = 8 Then Ymonth = SelectedCategory.CurveAvgJul
            If ChartMonthIndex = 9 Then Ymonth = SelectedCategory.CurveAvgAug
            If ChartMonthIndex = 10 Then Ymonth = SelectedCategory.CurveAvgSep
            If ChartMonthIndex = 11 Then Ymonth = SelectedCategory.CurveAvgOct
            If ChartMonthIndex = 12 Then Ymonth = SelectedCategory.CurveAvgNov
            If ChartMonthIndex = 13 Then Ymonth = SelectedCategory.CurveAvgDec

            If ChartMonthIndex > 1 Then

                With ChartLine
                    If SelectedCategory.CategoryChartColor Is Nothing Then SelectedCategory.CategoryChartColor = "#FFFF0000"
                    Dim MyCol As Windows.Media.Color = CType(ColorConverter.ConvertFromString(SelectedCategory.CategoryChartColor), Color)

                    Dim MyBrush As Windows.Media.Brush = New SolidColorBrush(MyCol)
                    .Fill = MyBrush
                    .Stroke = MyBrush
                    .StrokeThickness = 2
                    .Y1 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * Ymonth) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                    .Y2 = .Y1
                    .X1 = CNVSmarginLeft + (ChartWidthPixel / 366 * Xold)
                    .X2 = CNVSmarginLeft + (ChartWidthPixel / 366 * ChartMonths)
                End With

                CNVS99.Children.Add(ChartLine)
            End If
            Xold = ChartMonths
        Next

        ChartQuarters = 0
        ChartQuarterIndex = 0
        For Each ChartQuarter In ListofQuarters
            ChartQuarters += ChartQuarter
            ChartQuarterIndex += 1
            ChartLine = New Line

            If ChartQuarterIndex = 2 Then Ymonth = SelectedCategory.CurveAvgQ1
            If ChartQuarterIndex = 3 Then Ymonth = SelectedCategory.CurveAvgQ2
            If ChartQuarterIndex = 4 Then Ymonth = SelectedCategory.CurveAvgQ3
            If ChartQuarterIndex = 5 Then Ymonth = SelectedCategory.CurveAvgQ4

            If ChartQuarterIndex > 1 Then

                With ChartLine
                    If SelectedCategory.CategoryChartColor Is Nothing Then SelectedCategory.CategoryChartColor = "#FFFF0000"
                    Dim MyCol As Windows.Media.Color = CType(ColorConverter.ConvertFromString(SelectedCategory.CategoryChartColor), Color)

                    Dim MyBrush As Windows.Media.Brush = New SolidColorBrush(MyCol)
                    .Fill = MyBrush
                    .Stroke = MyBrush
                    .StrokeThickness = 2
                    .Y1 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * Ymonth) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                    .Y2 = .Y1
                    .X1 = CNVSmarginLeft + (ChartWidthPixel / 366 * Xold)
                    .X2 = CNVSmarginLeft + (ChartWidthPixel / 366 * ChartQuarters)
                End With

                CNVS99.Children.Add(ChartLine)
            End If
            Xold = ChartQuarters
        Next

    End Sub

    Public Sub AddCurveToChart(ByRef CurveToAdd As ObservableCollection(Of K8_CL03measurement), HighliteCurve As Boolean, Xgridmax As Int32, StatisticChart As Boolean)

        Dim ItemCounter As Int32 = 0
        Dim Xold As Decimal
        Dim Yold As Decimal
        Dim Ynew As Decimal
        Dim ChartLine As New Line
        Dim FirstValue As Decimal = 0

        Dim Curve_StrokeThickness As Double = 1
        Dim Curve_StrokeDashArray As New DoubleCollection From {2, 2}
        If SelectedCategory.CategoryChartColor Is Nothing Then SelectedCategory.CategoryChartColor = "#FFFF0000"
        Dim Curve_Color As Windows.Media.Color = CType(ColorConverter.ConvertFromString(SelectedCategory.CategoryChartColor), Color)
        Dim Curve_Brush As Windows.Media.Brush = New SolidColorBrush(Curve_Color)

        If HighliteCurve = True Or StatisticChart=True Then
            Curve_StrokeThickness = 2
            Curve_StrokeDashArray = New DoubleCollection From {1, 0}
            If StatisticChart = False Then Curve_Brush = Brushes.Black
        End If

        Dim NewCounterOffset As Decimal = 0

        For Each CurveItem In CurveToAdd

            If SelectedCategory.NewCounterActive = True AndAlso CurveItem.MeasDate >= SelectedCategory.NewCounterDate Then
                NewCounterOffset = SelectedCategory.NewCounterOffsetValue
            End If

            ItemCounter += 1
            If (ItemCounter = 1 And SelectedCategory.CategoryValuesRelativeTo1st = True) And (StatisticChart = False) Then
                FirstValue = CurveItem.MeasValue
            End If

            If ItemCounter > 1 Then
                ChartLine = New Line
                With ChartLine
                    .Fill = Curve_Brush
                    .Stroke = Curve_Brush
                    .StrokeThickness = Curve_StrokeThickness
                    .StrokeDashArray = Curve_StrokeDashArray


                    .Y1 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * Yold) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                    Ynew = CurveItem.MeasValue - FirstValue + NewCounterOffset
                    .Y2 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * Ynew) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                    .X1 = CNVSmarginLeft + (ChartWidthPixel / Xgridmax * Xold)
                    If StatisticChart = False Then
                        .X2 = CNVSmarginLeft + (ChartWidthPixel / Xgridmax * CurveItem.MeasDayOfYear)
                    Else
                        .X2 = CNVSmarginLeft + (ChartWidthPixel / Xgridmax * CurveItem.MeasDateYear)
                    End If
                End With

            End If

            If StatisticChart = False Then
                Xold = CurveItem.MeasDayOfYear
            Else
                Xold = CurveItem.MeasDateYear
            End If
            Yold = Ynew

            CNVS99.Children.Add(ChartLine)

        Next

    End Sub

    Private Sub DesignYaxis(ByRef AxisY As Line)

        With AxisY
            .Fill = Brushes.Black
            .Stroke = Brushes.Black
            .StrokeThickness = 1
            .X1 = CNVSmarginLeft
            .X2 = CNVSmarginLeft
            .Y1 = CNVSheight - CNVSmarginBottom
            .Y2 = 0 + CNVSmarginTop
        End With

    End Sub

    Private Sub DesignYgrid(ByRef Ygrid As Line, Ypos As Int32)

        With Ygrid
            .Fill = Brushes.LightGray
            .Stroke = Brushes.LightGray
            .StrokeThickness = 1
            .Y1 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * Ypos) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
            .Y2 = .Y1
            .X1 = 0 + CNVSmarginLeft
            .X2 = CNVSwidth - CNVSmarginRight
        End With

    End Sub

    Private Sub DesignYticks(ByRef Yticks As Line, Ypos As Int32)

        With Yticks
            .Fill = Brushes.Black
            .Stroke = Brushes.Black
            .StrokeThickness = 1
            .Y1 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * Ypos) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
            .Y2 = .Y1
            .X1 = 0 + CNVSmarginLeft - 5
            .X2 = 0 + CNVSmarginLeft + 5
        End With

    End Sub

    Private Sub DesignXaxis(ByRef AxisX As Line)

        With AxisX
            .Fill = Brushes.Black
            .Stroke = Brushes.Black
            .StrokeThickness = 1
            .X1 = 0 + CNVSmarginLeft
            .X2 = CNVSwidth - CNVSmarginRight
            .Y1 = CNVSheight - CNVSmarginBottom + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
            .Y2 = CNVSheight - CNVSmarginBottom + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
        End With

    End Sub

    Private Sub DesignXgridMonth(ByRef Xgrid As Line, Xpos As Int32, Xgridmax As Int32)

        With Xgrid
            .Fill = Brushes.LightGray
            .Stroke = Brushes.LightGray
            .StrokeThickness = 1
            .Y1 = CNVSheight - CNVSmarginBottom
            .Y2 = 0 + CNVSmarginTop
            .X1 = CNVSmarginLeft + (ChartWidthPixel / Xgridmax * Xpos)
            .X2 = .X1
        End With

    End Sub

    Private Sub DesignXticksMonth(ByRef Xticks As Line, Xpos As Int32, Xgridmax As Int32)

        With Xticks
            .Fill = Windows.Media.Brushes.Black
            .Stroke = Windows.Media.Brushes.Black
            .StrokeThickness = 1
            .Y1 = CNVSheight - CNVSmarginBottom - 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
            .Y2 = CNVSheight - CNVSmarginBottom + 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
            .X1 = CNVSmarginLeft + (ChartWidthPixel / Xgridmax * Xpos)
            .X2 = .X1
        End With

    End Sub

    Private Sub DesignXaxisMonthLabels(ByRef XaxisLabelMonths As TextBlock, ByRef ChartMonthIndex As Int32, ByRef ChartMonths As Int32, Xgridmax As Int32)

        With XaxisLabelMonths
            .Text = Format(New DateTime(1968, ChartMonthIndex, 1), "MMM")

            .FontStyle = FontStyles.Italic
            .FontWeight = FontWeights.Normal
            .FontFamily = New FontFamily("Arial")
        End With
        Canvas.SetLeft(XaxisLabelMonths, CNVSmarginLeft + (ChartWidthPixel / Xgridmax * ChartMonths) + 5)
        Canvas.SetTop(XaxisLabelMonths, CNVSheight - CNVSmarginBottom + 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin))

    End Sub

End Class
