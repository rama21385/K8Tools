Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports K8Tools
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
    Private _SelectedCategory As K8_CL02category
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

    Public Property SelectedCategory As K8_CL02category
        Get
            Return _SelectedCategory
        End Get
        Set(value As K8_CL02category)
            _SelectedCategory = value
            NotifyPropertyChanged()
        End Set
    End Property


#End Region

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        CMBBX02_Category.DataContext = KiebitzCategories

        CMBBX02_Category.Items.SortDescriptions.Add(New SortDescription("CategoryInternalID", ListSortDirection.Ascending))

        DTGRD02_Measurements.DataContext = KiebitzMeasCurve

        CMBBX02_Category.SelectedIndex = -1

        ChangesMade = False

        VWBX02.Child = Nothing
        VWBX02.Child = SingleCurveChart

    End Sub


    Private Sub AddNewMeasValue(sender As Object, e As RoutedEventArgs)

        Dim NewDate As DateTime
        Try
            NewDate = New DateTime(CInt(Val(TXTBX0_MeasYear.Text)), CInt(Val(TXTBX0_MeasMonth.Text)), CInt(Val(TXTBX0_MeasDay.Text)))
        Catch ex As Exception
            TXTBX0_MeasDay.Focus()
            Exit Sub
        End Try

        Dim CurveItemFound As Boolean = False

        For Each CurveItem In KiebitzMeasCurve
            If CurveItem.MeasDate = NewDate Then
                CurveItemFound = True
                Exit For
            End If
        Next

        If CurveItemFound = False Then

            KiebitzMeasCurve.Add(New K8_CL03measurement With {
                .CategoryInternalID = CStr(CMBBX02_Category.SelectedValue),
                .MeasComment = TXTBX0_MeasComment.Text,
                .MeasValue = CDec(Val(TXTBX0_MeasValue.Text)),
                .MeasDateDay = CUShort(Val(TXTBX0_MeasDay.Text)),
                .MeasDateMonth = CUShort(Val(TXTBX0_MeasMonth.Text)),
                .MeasDateYear = SelectedCategory.CategoryYear,
                .MeasStatus = K8ENUMS.CollectionItemStatus.add})


            DTGRD02_Measurements.DataContext = KiebitzMeasCurve
            DTGRD02_Measurements.ScrollIntoView(KiebitzMeasCurve.Item(KiebitzMeasCurve.Count - 1))

            RefreshChart()

        End If

    End Sub

    Private Sub DeleteMeasValue(sender As Object, e As RoutedEventArgs)

        Dim SelItem As New K8_CL03measurement
        SelItem = CType(DTGRD02_Measurements.SelectedItem, K8_CL03measurement)
        KiebitzMeasCurve.Remove(SelItem)

        RefreshChart()

    End Sub

    Private Sub ModifyMeasValue(sender As Object, e As RoutedEventArgs)

        Dim SelItem As New K8_CL03measurement
        SelItem = CType(DTGRD02_Measurements.SelectedItem, K8_CL03measurement)
        SelItem.MeasComment = TXTBX0_MeasComment.Text

        RefreshChart()

    End Sub


    Private Sub LoadAndSortTheList(sender As Object, e As SelectionChangedEventArgs)

        ChangesMade = False
        Dim TempCategoryID As String = CStr(CMBBX02_Category.SelectedValue)
        K8_CL03measurement.LoadXMLIntoCollection(TempCategoryID, TempCategoryID.Substring(0, InStr(TempCategoryID, "_") - 1))

        For Each item In KiebitzMeasCurve
            item.CategoryInternalID = TempCategoryID
        Next

        DTGRD02_Measurements.DataContext = KiebitzMeasCurve

        For Each CategoryItem In KiebitzCategories
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
            K8_CL03measurement.SaveCollectionAsXML(SelectedCategory.CategoryName)
            K8_CL02category.SaveCollectionAsXML()
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
        CSVselect.WindowStartupLocation = WindowStartupLocation.CenterOwner

        CSVselect.Owner = Application.Current.MainWindow
        CSVselect.StartFolder = K8_DirCSV
        CSVselect.ShowExistingFiles()
        CSVselect.ShowDialog()
        If CSVselect.DialogResult = False Then Exit Sub

        KiebitzMeasCurve.Clear()

        Dim Dateiname As String = CStr(CSVselect.SelectedCSVfile)

        Using csvParser As New TextFieldParser(K8_DirCSV & "\" & Dateiname)
            With csvParser
                ' Feld-Trennzeichen
                .SetDelimiters(";")
                ' Festlegen, ob die Datenfelder in Anführungszeichen stehen
                .HasFieldsEnclosedInQuotes = False ' bzw. False

                ' Falls die 1. Zeile die Spaltennamen enthält
                'Dim Header As String = .ReadLine()

                ' Datei zeilenweise durchlaufen
                Dim FieldData() As String
                Do While Not .EndOfData
                    ' alle Datenfelder der aktuellen Datenzeile lesen
                    FieldData = .ReadFields()
                    If FieldData(0) <> String.Empty And FieldData(1) <> String.Empty Then

                        Dim CurveDate() As String = FieldData(0).Split("."c)
                        If Val(CurveDate(2)) < 2000 Then CurveDate(2) = CStr(Val(CurveDate(2)) + 2000)
                        If FieldData(0) <> String.Empty And FieldData(1) <> String.Empty Then

                            KiebitzMeasCurve.Add(New K8_CL03measurement With {
                                .CategoryInternalID = CStr(CMBBX02_Category.SelectedValue),
                                .MeasComment = String.Empty,
                                .MeasValue = CDec(Val(FieldData(1).Replace(",", "."))),
                                .MeasDateDay = CUShort(Val(CurveDate(0))),
                                .MeasDateMonth = CUShort(Val(CurveDate(1))),
                                .MeasDateYear = CUShort(Val(CurveDate(2))),
                                .MeasStatus = K8ENUMS.CollectionItemStatus.add})

                        End If
                    End If
                Loop
            End With
        End Using


        DTGRD02_Measurements.DataContext = KiebitzMeasCurve
        DTGRD02_Measurements.ScrollIntoView(KiebitzMeasCurve.Item(KiebitzMeasCurve.Count - 1))

        RefreshChart()

    End Sub

    Private Sub RefreshChart()

        CalculateStatistics()
        SingleCurveChart.SelectedCategory = SelectedCategory
        SingleCurveChart.RefreshChart(False)
        SingleCurveChart.AddCurveToChart(KiebitzMeasCurve, False, 366, False)
        SingleCurveChart.AddStatisticLinesToChart(KiebitzMeasCurve)
        ChangesMade = True

    End Sub

    Public Sub CalculateStatistics()

        SelectedCategory.CurveMin = 1000000D
        SelectedCategory.CurveMax = -1000000D
        SelectedCategory.CurveAvgYear = 0
        SelectedCategory.CurveAvgQ1 = 0
        SelectedCategory.CurveAvgQ2 = 0
        SelectedCategory.CurveAvgQ3 = 0
        SelectedCategory.CurveAvgQ4 = 0
        SelectedCategory.CurveAvgJan = 0
        SelectedCategory.CurveAvgFeb = 0
        SelectedCategory.CurveAvgMar = 0
        SelectedCategory.CurveAvgApr = 0
        SelectedCategory.CurveAvgMay = 0
        SelectedCategory.CurveAvgJun = 0
        SelectedCategory.CurveAvgJul = 0
        SelectedCategory.CurveAvgAug = 0
        SelectedCategory.CurveAvgSep = 0
        SelectedCategory.CurveAvgOct = 0
        SelectedCategory.CurveAvgNov = 0
        SelectedCategory.CurveAvgDec = 0

        SelectedCategory.CurveDeltaYear = 0
        SelectedCategory.CurveDeltaQ1 = 0
        SelectedCategory.CurveDeltaQ2 = 0
        SelectedCategory.CurveDeltaQ3 = 0
        SelectedCategory.CurveDeltaQ4 = 0

        Dim CurveNrItems As Int32 = 0
        Dim CurveItemsSum As Decimal = 0

        Dim CurveItemsSumHJ1 As Decimal = 0
        Dim CurveNrItemsHJ1 As Int32 = 0
        Dim CurveItemsSumHJ2 As Decimal = 0
        Dim CurveNrItemsHJ2 As Int32 = 0

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

        Dim CurveDeltaHJ1a As Decimal = 0
        Dim CurveDeltaHJ1b As Decimal = 0
        Dim CurveDeltaHJ1 As Boolean = False
        Dim CurveDeltaHJ2a As Decimal = 0
        Dim CurveDeltaHJ2b As Decimal = 0
        Dim CurveDeltaHJ2 As Boolean = False
        Dim CurveDeltaQ1a As Decimal = 0
        Dim CurveDeltaQ1b As Decimal = 0
        Dim CurveDeltaQ1 As Boolean = False
        Dim CurveDeltaQ2a As Decimal = 0
        Dim CurveDeltaQ2b As Decimal = 0
        Dim CurveDeltaQ2 As Boolean = False
        Dim CurveDeltaQ3a As Decimal = 0
        Dim CurveDeltaQ3b As Decimal = 0
        Dim CurveDeltaQ3 As Boolean = False
        Dim CurveDeltaQ4a As Decimal = 0
        Dim CurveDeltaQ4b As Decimal = 0
        Dim CurveDeltaQ4 As Boolean = False

        Dim CurveDeltaJana As Decimal = 0
        Dim CurveDeltaJanb As Decimal = 0
        Dim CurveDeltaJan As Boolean = False
        Dim CurveDeltaFeba As Decimal = 0
        Dim CurveDeltaFebb As Decimal = 0
        Dim CurveDeltaFeb As Boolean = False
        Dim CurveDeltaMara As Decimal = 0
        Dim CurveDeltaMarb As Decimal = 0
        Dim CurveDeltaMar As Boolean = False

        Dim CurveDeltaApra As Decimal = 0
        Dim CurveDeltaAprb As Decimal = 0
        Dim CurveDeltaApr As Boolean = False
        Dim CurveDeltaMaya As Decimal = 0
        Dim CurveDeltaMayb As Decimal = 0
        Dim CurveDeltaMay As Boolean = False
        Dim CurveDeltaJuna As Decimal = 0
        Dim CurveDeltaJunb As Decimal = 0
        Dim CurveDeltaJun As Boolean = False

        Dim CurveDeltaJula As Decimal = 0
        Dim CurveDeltaJulb As Decimal = 0
        Dim CurveDeltaJul As Boolean = False
        Dim CurveDeltaAuga As Decimal = 0
        Dim CurveDeltaAugb As Decimal = 0
        Dim CurveDeltaAug As Boolean = False
        Dim CurveDeltaSepa As Decimal = 0
        Dim CurveDeltaSepb As Decimal = 0
        Dim CurveDeltaSep As Boolean = False

        Dim CurveDeltaOcta As Decimal = 0
        Dim CurveDeltaOctb As Decimal = 0
        Dim CurveDeltaOct As Boolean = False
        Dim CurveDeltaNova As Decimal = 0
        Dim CurveDeltaNovb As Decimal = 0
        Dim CurveDeltaNov As Boolean = False
        Dim CurveDeltaDeca As Decimal = 0
        Dim CurveDeltaDecb As Decimal = 0
        Dim CurveDeltaDec As Boolean = False

        For Each CurveItem In KiebitzMeasCurve
            CurveNrItems += 1
            CurveItemsSum += CurveItem.MeasValue
            If CurveItem.MeasValue < SelectedCategory.CurveMin Then SelectedCategory.CurveMin = CurveItem.MeasValue
            If CurveItem.MeasValue > SelectedCategory.CurveMax Then SelectedCategory.CurveMax = CurveItem.MeasValue

            If CurveItem.MeasDate.Month <= 6 Then
                CurveItemsSumHJ1 += CurveItem.MeasValue
                CurveNrItemsHJ1 += 1
                If CurveDeltaHJ1 = False Then
                    CurveDeltaHJ1a = CurveItem.MeasValue
                    CurveDeltaHJ1 = True
                End If
                CurveDeltaHJ1b = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month <= 12 Then
                CurveItemsSumHJ2 += CurveItem.MeasValue
                CurveNrItemsHJ2 += 1
                If CurveDeltaHJ2 = False Then
                    CurveDeltaHJ2a = CurveDeltaHJ1b 'last day of previous half year is first value of next half year
                    CurveDeltaHJ2 = True
                End If
                CurveDeltaHJ2b = CurveItem.MeasValue
            End If

            If CurveItem.MeasDate.Month <= 3 Then
                CurveItemsSumQ1 += CurveItem.MeasValue
                CurveNrItemsQ1 += 1
                If CurveDeltaQ1 = False Then
                    CurveDeltaQ1a = CurveItem.MeasValue
                    CurveDeltaQ1 = True
                End If
                CurveDeltaQ1b = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month <= 6 Then
                CurveItemsSumQ2 += CurveItem.MeasValue
                CurveNrItemsQ2 += 1
                If CurveDeltaQ2 = False Then
                    CurveDeltaQ2a = CurveDeltaQ1b 'last day of previous quarter is first value of next quarter
                    CurveDeltaQ2 = True
                End If
                CurveDeltaQ2b = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month <= 9 Then
                CurveItemsSumQ3 += CurveItem.MeasValue
                CurveNrItemsQ3 += 1
                If CurveDeltaQ3 = False Then
                    CurveDeltaQ3a = CurveDeltaQ2b
                    CurveDeltaQ3 = True
                End If
                CurveDeltaQ3b = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month <= 12 Then
                CurveItemsSumQ4 += CurveItem.MeasValue
                CurveNrItemsQ4 += 1
                If CurveDeltaQ4 = False Then
                    CurveDeltaQ4a = CurveDeltaQ3b
                    CurveDeltaQ4 = True
                End If
                CurveDeltaQ4b = CurveItem.MeasValue
            End If

            If CurveItem.MeasDate.Month = 1 Then
                CurveNrItemsJan += 1
                CurveItemsSumJan += CurveItem.MeasValue
                If CurveDeltaJan = False Then
                    CurveDeltaJana = CurveItem.MeasValue
                    CurveDeltaJan = True
                End If
                CurveDeltaJanb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 2 Then
                CurveNrItemsFeb += 1
                CurveItemsSumFeb += CurveItem.MeasValue
                If CurveDeltaFeb = False Then
                    CurveDeltaFeba = CurveDeltaJanb
                    CurveDeltaFeb = True
                End If
                CurveDeltaFebb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 3 Then
                CurveNrItemsMar += 1
                CurveItemsSumMar += CurveItem.MeasValue
                If CurveDeltaMar = False Then
                    CurveDeltaMara = CurveDeltaFebb
                    CurveDeltaMar = True
                End If
                CurveDeltaMarb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 4 Then
                CurveNrItemsApr += 1
                CurveItemsSumApr += CurveItem.MeasValue
                If CurveDeltaApr = False Then
                    CurveDeltaApra = CurveDeltaMarb
                    CurveDeltaApr = True
                End If
                CurveDeltaAprb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 5 Then
                CurveNrItemsMay += 1
                CurveItemsSumMay += CurveItem.MeasValue
                If CurveDeltaMay = False Then
                    CurveDeltaMaya = CurveDeltaAprb
                    CurveDeltaMay = True
                End If
                CurveDeltaMayb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 6 Then
                CurveNrItemsJun += 1
                CurveItemsSumJun += CurveItem.MeasValue
                If CurveDeltaJun = False Then
                    CurveDeltaJuna = CurveDeltaMayb
                    CurveDeltaJun = True
                End If
                CurveDeltaJunb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 7 Then
                CurveNrItemsJul += 1
                CurveItemsSumJul += CurveItem.MeasValue
                If CurveDeltaJul = False Then
                    CurveDeltaJula = CurveDeltaJunb
                    CurveDeltaJul = True
                End If
                CurveDeltaJulb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 8 Then
                CurveNrItemsAug += 1
                CurveItemsSumAug += CurveItem.MeasValue
                If CurveDeltaAug = False Then
                    CurveDeltaAuga = CurveDeltaJulb
                    CurveDeltaAug = True
                End If
                CurveDeltaAugb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 9 Then
                CurveNrItemsSep += 1
                CurveItemsSumSep += CurveItem.MeasValue
                If CurveDeltaSep = False Then
                    CurveDeltaSepa = CurveDeltaAugb
                    CurveDeltaSep = True
                End If
                CurveDeltaSepb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 10 Then
                CurveNrItemsOct += 1
                CurveItemsSumOct += CurveItem.MeasValue
                If CurveDeltaOct = False Then
                    CurveDeltaOcta = CurveDeltaSepb
                    CurveDeltaOct = True
                End If
                CurveDeltaOctb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 11 Then
                CurveNrItemsNov += 1
                CurveItemsSumNov += CurveItem.MeasValue
                If CurveDeltaNov = False Then
                    CurveDeltaNova = CurveDeltaOctb
                    CurveDeltaNov = True
                End If
                CurveDeltaNovb = CurveItem.MeasValue
            ElseIf CurveItem.MeasDate.Month = 12 Then
                CurveNrItemsDec += 1
                CurveItemsSumDec += CurveItem.MeasValue
                If CurveDeltaDec = False Then
                    CurveDeltaDeca = CurveDeltaNovb
                    CurveDeltaDec = True
                End If
                CurveDeltaDecb = CurveItem.MeasValue
            End If

        Next

        If CurveNrItems > 0 Then

            SelectedCategory.CurveAvgYear = CurveItemsSum / CurveNrItems
            SelectedCategory.CurveAvgHJ1 = CurveItemsSumHJ1 / CurveNrItemsHJ1
            SelectedCategory.CurveAvgHJ2 = CurveItemsSumHJ2 / CurveNrItemsHJ2
            SelectedCategory.CurveAvgQ1 = CurveItemsSumQ1 / CurveNrItemsQ1
            SelectedCategory.CurveAvgQ2 = CurveItemsSumQ2 / CurveNrItemsQ2
            SelectedCategory.CurveAvgQ3 = CurveItemsSumQ3 / CurveNrItemsQ3
            SelectedCategory.CurveAvgQ4 = CurveItemsSumQ4 / CurveNrItemsQ4
            SelectedCategory.CurveAvgJan = CurveItemsSumJan / CurveNrItemsJan
            SelectedCategory.CurveAvgFeb = CurveItemsSumFeb / CurveNrItemsFeb
            SelectedCategory.CurveAvgMar = CurveItemsSumMar / CurveNrItemsMar
            SelectedCategory.CurveAvgApr = CurveItemsSumApr / CurveNrItemsApr
            SelectedCategory.CurveAvgMay = CurveItemsSumMay / CurveNrItemsMay
            SelectedCategory.CurveAvgJun = CurveItemsSumJun / CurveNrItemsJun
            SelectedCategory.CurveAvgJul = CurveItemsSumJul / CurveNrItemsJul
            SelectedCategory.CurveAvgAug = CurveItemsSumAug / CurveNrItemsAug
            SelectedCategory.CurveAvgSep = CurveItemsSumSep / CurveNrItemsSep
            SelectedCategory.CurveAvgOct = CurveItemsSumOct / CurveNrItemsOct
            SelectedCategory.CurveAvgNov = CurveItemsSumNov / CurveNrItemsNov
            SelectedCategory.CurveAvgDec = CurveItemsSumDec / CurveNrItemsDec

            SelectedCategory.CurveDeltaYear = KiebitzMeasCurve(KiebitzMeasCurve.Count - 1).MeasValue - KiebitzMeasCurve(0).MeasValue
            SelectedCategory.CurveDeltaHJ1 = CurveDeltaHJ1b - CurveDeltaHJ1a
            SelectedCategory.CurveDeltaHJ2 = CurveDeltaHJ2b - CurveDeltaHJ2a
            SelectedCategory.CurveDeltaQ1 = CurveDeltaQ1b - CurveDeltaQ1a
            SelectedCategory.CurveDeltaQ2 = CurveDeltaQ2b - CurveDeltaQ2a
            SelectedCategory.CurveDeltaQ3 = CurveDeltaQ3b - CurveDeltaQ3a
            SelectedCategory.CurveDeltaQ4 = CurveDeltaQ4b - CurveDeltaQ4a
            SelectedCategory.CurveDeltaJan = CurveDeltaJanb - CurveDeltaJana
            SelectedCategory.CurveDeltaFeb = CurveDeltaFebb - CurveDeltaFeba
            SelectedCategory.CurveDeltaMar = CurveDeltaMarb - CurveDeltaMara
            SelectedCategory.CurveDeltaApr = CurveDeltaAprb - CurveDeltaApra
            SelectedCategory.CurveDeltaMay = CurveDeltaMayb - CurveDeltaMaya
            SelectedCategory.CurveDeltaJun = CurveDeltaJunb - CurveDeltaJuna
            SelectedCategory.CurveDeltaJul = CurveDeltaJulb - CurveDeltaJula
            SelectedCategory.CurveDeltaAug = CurveDeltaAugb - CurveDeltaAuga
            SelectedCategory.CurveDeltaSep = CurveDeltaSepb - CurveDeltaSepa
            SelectedCategory.CurveDeltaOct = CurveDeltaOctb - CurveDeltaOcta
            SelectedCategory.CurveDeltaNov = CurveDeltaNovb - CurveDeltaNova
            SelectedCategory.CurveDeltaDec = CurveDeltaDecb - CurveDeltaDeca
        End If

    End Sub

    Private Sub IncreaseDecreaseDayMonth(sender As Object, e As KeyEventArgs)

        Dim CurrentValue As Int32 = 0
        Dim MaxValue As Int32 = 0

        If sender Is TXTBX0_MeasDay Then
            CurrentValue = CInt(Val(TXTBX0_MeasDay.Text))
            MaxValue = 31
        ElseIf sender Is TXTBX0_MeasMonth Then
            CurrentValue = CInt(Val(TXTBX0_MeasMonth.Text))
            MaxValue = 12
        End If

        If sender Is TXTBX0_MeasDay Or sender Is TXTBX0_MeasMonth Then
            If e.Key = Key.Up Then
                CurrentValue = (CurrentValue Mod MaxValue) + 1
            ElseIf e.Key = Key.Down Then
                CurrentValue -= 1
                If CurrentValue = 0 Then CurrentValue = MaxValue
            End If

            TXTBX0_MeasDay.Text = CStr(CurrentValue)

        End If

    End Sub
End Class
