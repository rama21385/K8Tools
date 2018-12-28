Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.FileIO

Public Class K8_UC02input
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private SelectedMeasGroup As String
    Private SelectedCategory As New K8_CL02category
    Private _ChangesMade As Boolean

    Public Property ChangesMade As Boolean
        Get
            Return _ChangesMade
        End Get
        Set(value As Boolean)
            _ChangesMade = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        CMBBX02_Category.DataContext = KiebitzCats
        DTGRD02_Measurements.DataContext = KiebitzCurve

        CMBBX02_Category.SelectedIndex = -1

        ChangesMade = False

    End Sub


    Private Sub AddNewMeasValue(sender As Object, e As RoutedEventArgs)

        Dim NewDate As DateTime
        Try
            NewDate = New DateTime(Val(TXTBX0_MeasYear.Text), Val(TXTBX0_MeasMonth.Text), Val(TXTBX0_MeasDay.Text))
        Catch ex As Exception
            Exit Sub
        End Try

        Dim CurveItemFound As Boolean = False

        For Each CurveItem In KiebitzCurve
            If CurveItem.MeasDate = NewDate Then
                CurveItemFound = True
                Exit For
            End If
        Next

        If CurveItemFound = False Then

            KiebitzCurve.Add(New K8_CL03measurement With {
                .CategoryInternalID = CMBBX02_Category.SelectedValue,
                .MeasComment = TXTBX0_MeasComment.Text,
                .MeasValue = CDec(Val(TXTBX0_MeasValue.Text)),
                .MeasDateDay = CInt(Val(TXTBX0_MeasDay.Text)),
                .MeasDateMonth = CInt(Val(TXTBX0_MeasMonth.Text)),
                .MeasDateYear = CInt((TXTBX0_MeasYear.Text)),
                .MeasStatus = K8ENUMS.CollectionItemStatus.add})


            DTGRD02_Measurements.DataContext = KiebitzCurve
            DTGRD02_Measurements.ScrollIntoView(KiebitzCurve.Item(KiebitzCurve.Count - 1))

            RefreshChart()
            ChangesMade = True

        End If

    End Sub

    Private Sub DeleteMeasValue(sender As Object, e As RoutedEventArgs)

        Dim SelItem As New K8_CL03measurement
        SelItem = DTGRD02_Measurements.SelectedItem
        KiebitzCurve.Remove(SelItem)

        RefreshChart()
        ChangesMade = True

    End Sub

    Private Sub ModifyMeasValue(sender As Object, e As RoutedEventArgs)

        Dim SelItem As New K8_CL03measurement
        SelItem = DTGRD02_Measurements.SelectedItem
        SelItem.MeasComment = TXTBX0_MeasComment.Text

        RefreshChart()
        ChangesMade = True

    End Sub

    Private Sub RefreshChart()

        CNVS02_Chart.Children.Clear()

        Dim PatternX As DoubleCollection = New DoubleCollection()
        PatternX.Add(1)
        PatternX.Add(2)

        Dim CNVSmarginLeft As Double = 25
        Dim CNVSmarginRight As Double = 10
        Dim CNVSmarginTop As Double = 10
        Dim CNVSmarginBottom As Double = 25

        Dim CNVSwidth = CNVS02_Chart.ActualWidth
        Dim CNVSheight = CNVS02_Chart.ActualHeight
        Dim ChartWidthPixel As Double = CNVSwidth - CNVSmarginLeft - CNVSmarginRight
        Dim ChartHeightPixel As Double = CNVSheight - CNVSmarginTop - CNVSmarginBottom
        Dim ChartYRangeValue As Double = (Math.Abs(SelectedCategory.CategoryChartYMin) + Math.Abs(SelectedCategory.CategoryChartYMax))
        If ChartYRangeValue = 0 Then ChartYRangeValue = 100
        If SelectedCategory.CategoryChartYMax = 0 Then SelectedCategory.CategoryChartYMax = 100

        Dim Yticks As New Line
        Dim Ygrid As New Line
        For Each MinMax As Int32 In {SelectedCategory.CategoryChartYMin, SelectedCategory.CategoryChartYMax}

            For I = 0 To MinMax Step (MinMax / Math.Abs(MinMax)) * 10
                Yticks = New Line
                Ygrid = New Line
                With Yticks
                    .Fill = Brushes.Black
                    .Stroke = Brushes.Black
                    .StrokeThickness = 1
                    .Y1 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * I) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                    .Y2 = .Y1
                    .X1 = 0 + CNVSmarginLeft - 5
                    .X2 = 0 + CNVSmarginLeft + 5
                End With
                With Ygrid
                    .Fill = Brushes.LightGray
                    .Stroke = Brushes.LightGray
                    .StrokeThickness = 1
                    .Y1 = CNVSheight - CNVSmarginBottom - (ChartHeightPixel / ChartYRangeValue * I) + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                    .Y2 = .Y1
                    .X1 = 0 + CNVSmarginLeft
                    .X2 = CNVSwidth - CNVSmarginRight
                End With
                CNVS02_Chart.Children.Add(Ygrid)
                CNVS02_Chart.Children.Add(Yticks)
            Next

        Next

        Dim ChartMonths As Double = 0
        Dim ChartMonthIndex As Int32 = 0
        Dim Xticks As New Line
        Dim Xgrid As New Line

        'If DateTime.IsLeapYear(SelectedCategory.CategoryYear) = True Then
        'End If

        For Each ChartMonth As Int32 In {0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
            ChartMonths += ChartMonth
            ChartMonthIndex += 1
            Xticks = New Line
            Xgrid = New Line
            With Xticks
                .Fill = Windows.Media.Brushes.Black
                .Stroke = Windows.Media.Brushes.Black
                .StrokeThickness = 1
                .Y1 = CNVSheight - CNVSmarginBottom - 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                .Y2 = CNVSheight - CNVSmarginBottom + 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
                .X1 = CNVSmarginLeft + (ChartWidthPixel / 365 * ChartMonths)
                .X2 = .X1
            End With
            With Xgrid
                .Fill = Brushes.LightGray
                .Stroke = Brushes.LightGray
                .StrokeThickness = 1
                .Y1 = CNVSheight - CNVSmarginBottom
                .Y2 = 0 + CNVSmarginTop
                .X1 = CNVSmarginLeft + (ChartWidthPixel / 365 * ChartMonths)
                .X2 = .X1
            End With
            CNVS02_Chart.Children.Add(Xgrid)
            CNVS02_Chart.Children.Add(Xticks)

            If ChartMonthIndex <= 12 Then

                Dim Month01 As New TextBlock
                Dim MonthName As String = Format(New DateTime(1968, ChartMonthIndex, 1), "MMM")
                Month01.Text = MonthName

                Month01.FontStyle = FontStyles.Italic
                Month01.FontWeight = FontWeights.Normal
                Month01.FontFamily = New FontFamily("Arial")
                Canvas.SetLeft(Month01, CNVSmarginLeft + (ChartWidthPixel / 365 * ChartMonths) + 5)
                Canvas.SetTop(Month01, CNVSheight - CNVSmarginBottom + 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin))
                CNVS02_Chart.Children.Add(Month01)

            End If
        Next

        Dim AxisY As New Line
        With AxisY
            .Fill = Brushes.Black
            .Stroke = Brushes.Black
            .StrokeThickness = 1
            .X1 = CNVSmarginLeft
            .X2 = CNVSmarginLeft
            .Y1 = CNVSheight - CNVSmarginBottom
            .Y2 = 0 + CNVSmarginTop
        End With

        Dim AxisX As New Line
        With AxisX
            .Fill = Brushes.Black
            .Stroke = Brushes.Black
            .StrokeThickness = 1
            '.StrokeDashArray = PatternX
            .X1 = 0 + CNVSmarginLeft
            .X2 = CNVSwidth - CNVSmarginRight
            .Y1 = CNVSheight - CNVSmarginBottom + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
            .Y2 = CNVSheight - CNVSmarginBottom + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin)
        End With

        CNVS02_Chart.Children.Add(AxisY)
        CNVS02_Chart.Children.Add(AxisX)


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

            CNVS02_Chart.Children.Add(ChartLine)
        Next

    End Sub


    Private Sub LoadAndSortTheList(sender As Object, e As SelectionChangedEventArgs)

        ChangesMade = False
        Dim TempCategoryID As String = CMBBX02_Category.SelectedValue
        K8_CL03measurement.LoadXMLIntoCollection(TempCategoryID)

        For Each item In KiebitzCurve
            item.CategoryInternalID = TempCategoryID
        Next

        DTGRD02_Measurements.DataContext = KiebitzCurve

        For Each CategoryItem In KiebitzCats
            If CategoryItem.CategoryInternalID = TempCategoryID Then
                SelectedCategory = CategoryItem
                Exit For
            End If
        Next


        DTGRD02_Measurements.Items.SortDescriptions.Clear()
        DTGRD02_Measurements.Items.SortDescriptions.Add(New SortDescription("MeasDate", ListSortDirection.Ascending))
        DTGRD02_Measurements.Items.Refresh()
        SelectedMeasGroup = CType(CMBBX02_Category.SelectedItem, K8_CL02category).CategoryName.ToString

        RefreshChart()

    End Sub

    Private Sub SaveSettings(sender As Object, e As RoutedEventArgs)

        If sender Is BTN02_SAVEsettings Then
            K8_CL03measurement.SaveCollectionAsXML()
        ElseIf sender Is BTN02_EXITnosave Then
            CloseMe(Nothing, Nothing)
        End If

    End Sub

    Private Sub CloseMe(sender As Object, e As RoutedEventArgs)

        Dim ParentGrid As Grid = CType(Me.Parent, Grid)
        ParentGrid.Children.Remove(Me)

    End Sub

    Private Sub ImportCSV(sender As Object, e As RoutedEventArgs)

        Dim Dateiname As String = "Wetter_2001min.csv"
        Using csvParser As New TextFieldParser(Dateiname)
            With csvParser
                ' Feld-Trennzeichen
                .SetDelimiters(",")
                ' Festlegen, ob die Datenfelder in Anführungszeichen stehen
                .HasFieldsEnclosedInQuotes = False ' bzw. False

                ' Falls die 1. Zeile die Spaltennamen enthält
                'Dim Header As String = .ReadLine()

                ' Datei zeilenweise durchlaufen
                Dim FieldData() As String
                Do While Not .EndOfData
                    ' alle Datenfelder der aktuellen Datenzeile lesen
                    FieldData = .ReadFields()
                    Dim CurveDate() As String = FieldData(0).Split(".")

                    KiebitzCurve.Add(New K8_CL03measurement With {
                        .CategoryInternalID = CMBBX02_Category.SelectedValue,
                        .MeasComment = String.Empty,
                        .MeasValue = CDec(FieldData(1).Replace(",", ".")),
                        .MeasDateDay = CurveDate(0),
                        .MeasDateMonth = CurveDate(1),
                        .MeasDateYear = CurveDate(2),
                        .MeasStatus = K8ENUMS.CollectionItemStatus.add})

                Loop
            End With
        End Using


        DTGRD02_Measurements.DataContext = KiebitzCurve
        DTGRD02_Measurements.ScrollIntoView(KiebitzCurve.Item(KiebitzCurve.Count - 1))

        RefreshChart()
        ChangesMade = True

    End Sub

End Class
