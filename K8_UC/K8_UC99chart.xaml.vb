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

    Public Sub RefreshChart()

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
        For Each MinMax As Int32 In {SelectedCategory.CategoryChartYMin, SelectedCategory.CategoryChartYMax}
            For I = 0 To MinMax Step (MinMax / Math.Abs(MinMax)) * 10
                Ygrid = New Line
                Yticks = New Line
                DesignYgrid(Ygrid, I)
                DesignYticks(Yticks, I)

                CNVS99.Children.Add(Ygrid)
                CNVS99.Children.Add(Yticks)
            Next
        Next

        Dim ChartMonths As Double = 0
        Dim ChartMonthIndex As Int32 = 0
        Dim Xticks As New Line
        Dim Xgrid As New Line


        For Each ChartMonth As Int32 In {0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
            ChartMonths += ChartMonth
            ChartMonthIndex += 1
            Xgrid = New Line
            Xticks = New Line

            DesignXgrid(Xgrid, ChartMonths)
            DesignXticks(Xticks, ChartMonths)

            CNVS99.Children.Add(Xgrid)
            CNVS99.Children.Add(Xticks)

            If ChartMonthIndex <= 12 Then

                Dim Month01 As New TextBlock
                Dim MonthName As String = Format(New DateTime(1968, ChartMonthIndex, 1), "MMM")
                Month01.Text = MonthName

                Month01.FontStyle = FontStyles.Italic
                Month01.FontWeight = FontWeights.Normal
                Month01.FontFamily = New FontFamily("Arial")
                Canvas.SetLeft(Month01, CNVSmarginLeft + (ChartWidthPixel / 365 * ChartMonths) + 5)
                Canvas.SetTop(Month01, CNVSheight - CNVSmarginBottom + 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin))
                CNVS99.Children.Add(Month01)

            End If
        Next

        Dim AxisY As New Line
        DesignYaxis(AxisY)
        CNVS99.Children.Add(AxisY)

        Dim AxisX As New Line
        DesignXaxis(AxisX)
        CNVS99.Children.Add(AxisX)


        Dim ItemCounter As Int32 = 0
        Dim Xold As Int32
        Dim Yold As Int32
        Dim ChartLine As New Line
        For Each item In KiebitzCurve
            ItemCounter += 1

            If ItemCounter > 1 Then
                ChartLine = New Line
                With ChartLine
                    If SelectedCategory.CategoryChartColor Is Nothing Then SelectedCategory.CategoryChartColor = "#FFFF0000"
                    Dim MyCol As Windows.Media.Color = Windows.Media.ColorConverter.ConvertFromString(SelectedCategory.CategoryChartColor)

                    Dim MyBrush As Windows.Media.Brush = New SolidColorBrush(MyCol)
                    .Fill = MyBrush
                    .Stroke = MyBrush
                    .StrokeThickness = 2
                    .Y1 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * Yold) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                    .Y2 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * item.MeasValue) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                    .X1 = CNVSmarginLeft + (ChartWidthPixel / 365 * Xold)
                    .X2 = CNVSmarginLeft + (ChartWidthPixel / 365 * item.MeasDayOfYear)
                End With
            End If

            Xold = item.MeasDayOfYear
            Yold = item.MeasValue

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

    Private Sub DesignXgrid(ByRef Xgrid As Line, Xpos As Int32)

        With Xgrid
            .Fill = Brushes.LightGray
            .Stroke = Brushes.LightGray
            .StrokeThickness = 1
            .Y1 = CNVSheight - CNVSmarginBottom
            .Y2 = 0 + CNVSmarginTop
            .X1 = CNVSmarginLeft + (ChartWidthPixel / 365 * Xpos)
            .X2 = .X1
        End With

    End Sub

    Private Sub DesignXticks(ByRef Xticks As Line, Xpos As Int32)

        With Xticks
            .Fill = Windows.Media.Brushes.Black
            .Stroke = Windows.Media.Brushes.Black
            .StrokeThickness = 1
            .Y1 = CNVSheight - CNVSmarginBottom - 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
            .Y2 = CNVSheight - CNVSmarginBottom + 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
            .X1 = CNVSmarginLeft + (ChartWidthPixel / 365 * Xpos)
            .X2 = .X1
        End With

    End Sub

End Class
