Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.FileIO

Public Class K8_UC02input
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Dim SingleCurveChart As New K8_UC99chart

#Region "Local properties"

    Private SelectedMeasGroup As String
    Private SelectedCategory As New K8_CL02category
    Private _ChangesMade As Boolean
    Private _CurveMin As Decimal
    Private _CurveMax As Decimal
    Private _CurveAvgYear As Decimal
    Private _CurveAvgQ1 As Decimal
    Private _CurveAvgQ2 As Decimal
    Private _CurveAvgQ3 As Decimal
    Private _CurveAvgQ4 As Decimal
    Private _CurveAvgJan As Decimal
    Private _CurveAvgFeb As Decimal
    Private _CurveAvgMar As Decimal
    Private _CurveAvgApr As Decimal
    Private _CurveAvgMay As Decimal
    Private _CurveAvgJun As Decimal
    Private _CurveAvgJul As Decimal
    Private _CurveAvgAug As Decimal
    Private _CurveAvgSep As Decimal
    Private _CurveAvgOct As Decimal
    Private _CurveAvgNov As Decimal
    Private _CurveAvgDec As Decimal

    Public Property ChangesMade As Boolean
        Get
            Return _ChangesMade
        End Get
        Set(value As Boolean)
            _ChangesMade = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveMin As Decimal
        Get
            Return _CurveMin
        End Get
        Set(value As Decimal)
            _CurveMin = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveMax As Decimal
        Get
            Return _CurveMax
        End Get
        Set(value As Decimal)
            _CurveMax = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgYear As Decimal
        Get
            Return Math.Round(_CurveAvgYear, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgYear = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgQ1 As Decimal
        Get
            Return Math.Round(_CurveAvgQ1, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgQ1 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgQ2 As Decimal
        Get
            Return Math.Round(_CurveAvgQ2, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgQ2 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgQ3 As Decimal
        Get
            Return Math.Round(_CurveAvgQ3, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgQ3 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgQ4 As Decimal
        Get
            Return Math.Round(_CurveAvgQ4, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgQ4 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgJan As Decimal
        Get
            Return Math.Round(_CurveAvgJan, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgJan = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgFeb As Decimal
        Get
            Return Math.Round(_CurveAvgFeb, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgFeb = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgMar As Decimal
        Get
            Return Math.Round(_CurveAvgMar, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgMar = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgApr As Decimal
        Get
            Return Math.Round(_CurveAvgApr, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgApr = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgMay As Decimal
        Get
            Return Math.Round(_CurveAvgMay, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgMay = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgJun As Decimal
        Get
            Return Math.Round(_CurveAvgJun, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgJun = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgJul As Decimal
        Get
            Return Math.Round(_CurveAvgJul, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgJul = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgAug As Decimal
        Get
            Return Math.Round(_CurveAvgAug, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgAug = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgSep As Decimal
        Get
            Return Math.Round(_CurveAvgSep, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgSep = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgOct As Decimal
        Get
            Return Math.Round(_CurveAvgOct, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgOct = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgNov As Decimal
        Get
            Return Math.Round(_CurveAvgNov, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgNov = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgDec As Decimal
        Get
            Return Math.Round(_CurveAvgDec, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgDec = value
            NotifyPropertyChanged()
        End Set
    End Property

#End Region

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        CMBBX02_Category.DataContext = KiebitzCats
        DTGRD02_Measurements.DataContext = KiebitzCurve

        CMBBX02_Category.SelectedIndex = -1

        ChangesMade = False

        VWBX02.Child = Nothing
        VWBX02.Child = SingleCurveChart

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

            SingleCurveChart.SelectedCategory = SelectedCategory
            SingleCurveChart.RefreshChart()
            ChangesMade = True

        End If

    End Sub

    Private Sub DeleteMeasValue(sender As Object, e As RoutedEventArgs)

        Dim SelItem As New K8_CL03measurement
        SelItem = DTGRD02_Measurements.SelectedItem
        KiebitzCurve.Remove(SelItem)

        SingleCurveChart.SelectedCategory = SelectedCategory
        SingleCurveChart.RefreshChart()
        ChangesMade = True

    End Sub

    Private Sub ModifyMeasValue(sender As Object, e As RoutedEventArgs)

        Dim SelItem As New K8_CL03measurement
        SelItem = DTGRD02_Measurements.SelectedItem
        SelItem.MeasComment = TXTBX0_MeasComment.Text

        SingleCurveChart.SelectedCategory = SelectedCategory
        SingleCurveChart.RefreshChart()
        ChangesMade = True

    End Sub

    Private Sub RefreshChart()

        SingleCurveChart.CNVS99.Children.Clear()

        Dim PatternX As DoubleCollection = New DoubleCollection()
        PatternX.Add(1)
        PatternX.Add(2)

        Dim CNVSmarginLeft As Double = 25
        Dim CNVSmarginRight As Double = 10
        Dim CNVSmarginTop As Double = 10
        Dim CNVSmarginBottom As Double = 25

        Dim CNVSwidth = SingleCurveChart.ActualWidth
        Dim CNVSheight = SingleCurveChart.ActualHeight
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
                SingleCurveChart.CNVS99.Children.Add(Ygrid)
                SingleCurveChart.CNVS99.Children.Add(Yticks)
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
            SingleCurveChart.CNVS99.Children.Add(Xgrid)
            SingleCurveChart.CNVS99.Children.Add(Xticks)

            If ChartMonthIndex <= 12 Then

                Dim Month01 As New TextBlock
                Dim MonthName As String = Format(New DateTime(1968, ChartMonthIndex, 1), "MMM")
                Month01.Text = MonthName

                Month01.FontStyle = FontStyles.Italic
                Month01.FontWeight = FontWeights.Normal
                Month01.FontFamily = New FontFamily("Arial")
                Canvas.SetLeft(Month01, CNVSmarginLeft + (ChartWidthPixel / 365 * ChartMonths) + 5)
                Canvas.SetTop(Month01, CNVSheight - CNVSmarginBottom + 5 + (ChartHeightPixel / ChartYRangeValue * SelectedCategory.CategoryChartYMin))
                SingleCurveChart.CNVS99.Children.Add(Month01)

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

        SingleCurveChart.CNVS99.Children.Add(AxisY)
        SingleCurveChart.CNVS99.Children.Add(AxisX)


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

            SingleCurveChart.CNVS99.Children.Add(ChartLine)
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

        SingleCurveChart.SelectedCategory = SelectedCategory
        SingleCurveChart.RefreshChart()
        GetMinAndMax()

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

        Dim CSVselect As New K8_WW01
        CSVselect.ShowDialog()
        If CSVselect.DialogResult = False Then Exit Sub


        Dim Dateiname As String = CSVselect.SelectedCSVfile
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

                    If FieldData(0) <> String.Empty And FieldData(1) <> String.Empty Then

                        KiebitzCurve.Add(New K8_CL03measurement With {
                                    .CategoryInternalID = CMBBX02_Category.SelectedValue,
                                    .MeasComment = String.Empty,
                                    .MeasValue = CDec(FieldData(1).Replace(",", ".")),
                                    .MeasDateDay = CurveDate(0),
                                    .MeasDateMonth = CurveDate(1),
                                    .MeasDateYear = CurveDate(2),
                                    .MeasStatus = K8ENUMS.CollectionItemStatus.add})

                    End If
                Loop
            End With
        End Using


        DTGRD02_Measurements.DataContext = KiebitzCurve
        DTGRD02_Measurements.ScrollIntoView(KiebitzCurve.Item(KiebitzCurve.Count - 1))

        SingleCurveChart.SelectedCategory = SelectedCategory
        SingleCurveChart.RefreshChart()
        ChangesMade = True

    End Sub


    Public Sub GetMinAndMax()

        CurveMin = 1000000.0
        CurveMax = -1000000.0
        Dim CurveNrItems As Int32 = 0
        Dim CurveItemsSum As Decimal = 0

        Dim CurveItemsSumQ1 As Decimal = 0
        Dim CurveNrItemsQ1 As Int32 = 0
        Dim CurveItemsSumQ2 As Decimal = 0
        Dim CurveNrItemsQ2 As Int32 = 0
        Dim CurveItemsSumQ3 As Decimal = 0
        Dim CurveNrItemsQ3 As Int32 = 0
        Dim CurveItemsSumQ4 As Decimal = 0
        Dim CurveNrItemsQ4 As Int32 = 0

        Dim CurveItemsSumJan As Decimal = 0
        Dim CurveNrItemsJan As Int32 = 0
        Dim CurveItemsSumFeb As Decimal = 0
        Dim CurveNrItemsFeb As Int32 = 0
        Dim CurveItemsSumMar As Decimal = 0
        Dim CurveNrItemsMar As Int32 = 0
        Dim CurveItemsSumApr As Decimal = 0
        Dim CurveNrItemsApr As Int32 = 0
        Dim CurveItemsSumMay As Decimal = 0
        Dim CurveNrItemsMay As Int32 = 0
        Dim CurveItemsSumJun As Decimal = 0
        Dim CurveNrItemsJun As Int32 = 0
        Dim CurveItemsSumJul As Decimal = 0
        Dim CurveNrItemsJul As Int32 = 0
        Dim CurveItemsSumAug As Decimal = 0
        Dim CurveNrItemsAug As Int32 = 0
        Dim CurveItemsSumSep As Decimal = 0
        Dim CurveNrItemsSep As Int32 = 0
        Dim CurveItemsSumOct As Decimal = 0
        Dim CurveNrItemsOct As Int32 = 0
        Dim CurveItemsSumNov As Decimal = 0
        Dim CurveNrItemsNov As Int32 = 0
        Dim CurveItemsSumDec As Decimal = 0
        Dim CurveNrItemsDec As Int32 = 0

        For Each CurveItem In KiebitzCurve
            CurveNrItems += 1
            CurveItemsSum += CurveItem.MeasValue
            If CurveItem.MeasValue < CurveMin Then CurveMin = CurveItem.MeasValue
            If CurveItem.MeasValue > CurveMax Then CurveMax = CurveItem.MeasValue

            If CurveItem.MeasDate.Month <= 3 Then
                CurveItemsSumQ1 += CurveItem.MeasValue
                CurveNrItemsQ1 += 1
            ElseIf CurveItem.MeasDate.Month <= 6 Then
                CurveItemsSumQ2 += CurveItem.MeasValue
                CurveNrItemsQ2 += 1
            ElseIf CurveItem.MeasDate.Month <= 9 Then
                CurveItemsSumQ3 += CurveItem.MeasValue
                CurveNrItemsQ3 += 1
            ElseIf CurveItem.MeasDate.Month <= 12 Then
                CurveItemsSumQ4 += CurveItem.MeasValue
                CurveNrItemsQ4 += 1
            End If
            If CurveItem.MeasDate.Month = 1 Then
                CurveNrItemsJan += 1
                CurveItemsSumJan += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 2 Then
                CurveNrItemsFeb += 1
                CurveItemsSumFeb += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 3 Then
                CurveNrItemsMar += 1
                CurveItemsSumMar += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 4 Then
                CurveNrItemsApr += 1
                CurveItemsSumApr += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 5 Then
                CurveNrItemsMay += 1
                CurveItemsSumMay += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 6 Then
                CurveNrItemsJun += 1
                CurveItemsSumJun += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 7 Then
                CurveNrItemsJul += 1
                CurveItemsSumJul += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 8 Then
                CurveNrItemsAug += 1
                CurveItemsSumAug += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 9 Then
                CurveNrItemsSep += 1
                CurveItemsSumSep += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 10 Then
                CurveNrItemsOct += 1
                CurveItemsSumOct += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 11 Then
                CurveNrItemsNov += 1
                CurveItemsSumNov += CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 12 Then
                CurveNrItemsDec += 1
                CurveItemsSumDec += CurveItem.MeasValue
            End If

        Next

        If CurveNrItems > 0 Then

            CurveAvgYear = CurveItemsSum / CurveNrItems
            CurveAvgQ1 = CurveItemsSumQ1 / CurveNrItemsQ1
            CurveAvgQ2 = CurveItemsSumQ2 / CurveNrItemsQ2
            CurveAvgQ3 = CurveItemsSumQ3 / CurveNrItemsQ3
            CurveAvgQ4 = CurveItemsSumQ4 / CurveNrItemsQ4
            CurveAvgJan = CurveItemsSumJan / CurveNrItemsJan
            CurveAvgFeb = CurveItemsSumFeb / CurveNrItemsFeb
            CurveAvgMar = CurveItemsSumMar / CurveNrItemsMar
            CurveAvgApr = CurveItemsSumApr / CurveNrItemsApr
            CurveAvgMay = CurveItemsSumMay / CurveNrItemsMay
            CurveAvgJun = CurveItemsSumJun / CurveNrItemsJun
            CurveAvgJul = CurveItemsSumJul / CurveNrItemsJul
            CurveAvgAug = CurveItemsSumAug / CurveNrItemsAug
            CurveAvgSep = CurveItemsSumSep / CurveNrItemsSep
            CurveAvgOct = CurveItemsSumOct / CurveNrItemsOct
            CurveAvgNov = CurveItemsSumNov / CurveNrItemsNov
            CurveAvgDec = CurveItemsSumDec / CurveNrItemsDec

        End If

    End Sub



End Class
