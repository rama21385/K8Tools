Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class K8_UC03analyse
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Dim MultiCurveChart1 As New K8_UC99chart
    Dim MultiCurveChart2 As New K8_UC99chart
    Private SelectedCategory As New K8_CL02category
    Private Sub DoThingsWhenUnloading(sender As Object, e As RoutedEventArgs)

        KiebitzMeasCurve.Clear()

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        K8_CL05analysis.LoadXMLIntoCollection()

        CMBBX03_AnalysisCollection.DataContext = KiebitzAllAnalysis
        LSTBX03_CategoriesLoaded.DataContext = KiebitzCategories
        LSTBX03_CategoriesLoaded.Items.SortDescriptions.Add(New SortDescription("CategoryInternalID", ListSortDirection.Ascending))
        ResetChart()

    End Sub

    Private Sub LoadAndSortTheList(sender As Object, e As RoutedEventArgs)
        LoadAndSortTheList(Nothing, Nothing)
    End Sub

    Private Sub LoadAndSortTheList(sender As Object, e As SelectionChangedEventArgs)

        Dim StatisticStep As Int32 = 0 'wird verwendet für Chart02: Statistik "von Jahr zu Jahr"
        Dim CounterChart As Int32 = 0
        ResetChart()

        If CMBBX03_AnalysisCollection.SelectedIndex > -1 Then

            If KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems.Count <= 0 Then
                LSTBX03_Categories.ItemsSource = Nothing
                Exit Sub
            End If

            LSTBX03_Categories.ItemsSource = KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems

            ListOfStatItems.Clear()

            StatisticStep = 100 \ LSTBX03_Categories.Items.Count
            For I = 0 To 100 Step StatisticStep
                ListOfStatItems.Add(I)
            Next

            Dim KiebitzStatCurve As New ObservableCollection(Of K8_CL03measurement)

            For Each CollItem As K8_CL04analyse In LSTBX03_Categories.Items
                CounterChart += 1

                K8_CL03measurement.LoadXMLIntoCollection(CollItem.AnalyseCategoryName, CollItem.AnalyseCategoryName.Substring(0, InStr(CollItem.AnalyseCategoryName, "_") - 1))

                For Each CategoryItem In KiebitzCategories
                    If CategoryItem.CategoryInternalID = CollItem.AnalyseCategoryName Then
                        SelectedCategory = CategoryItem
                        MultiCurveChart1.SelectedCategory = SelectedCategory
                        MultiCurveChart2.SelectedCategory = SelectedCategory
                        Exit For
                    End If
                Next

                If CounterChart = 1 Then
                    MultiCurveChart1.RefreshChart(False, CBool(Me.CHKBX_DarkMode.IsChecked))
                    MultiCurveChart2.RefreshChart(True, CBool(Me.CHKBX_DarkMode.IsChecked))
                End If

                If LSTBX03_Categories.SelectedItem Is CollItem Then
                    MultiCurveChart1.AddCurveToChart(KiebitzMeasCurve, True, 366, False)
                Else
                    MultiCurveChart1.AddCurveToChart(KiebitzMeasCurve, False, 366, False)
                End If

                KiebitzStatCurve.Add(New K8_CL03measurement With {
                                     .MeasValue = SelectedCategory.CurveDeltaYear,
                                     .MeasDateMonth = 1,
                                     .MeasDateDay = 1,
                                     .MeasDateYear = CUShort((100 \ LSTBX03_Categories.Items.Count) * CounterChart - StatisticStep \ 2)})
            Next

            MultiCurveChart2.AddCurveToChart(KiebitzStatCurve, False, 100, True)

        End If

    End Sub

    Private Sub SaveSettings(sender As Object, e As RoutedEventArgs)

        Dim SetFound As Boolean = False

        If sender Is BTN03_SAVEsettings Then

            Dim CollectionToRemove As New List(Of Int32)
            Dim Counter As Int32
            For Each AnalyseCollection In KiebitzAllAnalysis
                If AnalyseCollection.AnalysisCollectionItems.Count = 0 Then
                    CollectionToRemove.Add(Counter)
                End If
                Counter += 1
            Next

            For Each Index As Int32 In CollectionToRemove
                KiebitzAllAnalysis.RemoveAt(Index)
            Next

            K8_CL05analysis.SaveCollectionAsXML()

            CMBBX03_AnalysisCollection.SelectedIndex = -1
            LSTBX03_Categories.ItemsSource = Nothing
            ResetChart()
        ElseIf sender Is BTN03_EXITnosave Then
            CloseMe(Nothing, Nothing)
        End If

    End Sub

    Private Sub ResetChart()

        VWBX03A.Child = Nothing
        VWBX03A.Child = MultiCurveChart1
        VWBX03B.Child = Nothing
        VWBX03B.Child = MultiCurveChart2

    End Sub

    Private Sub CloseMe(sender As Object, e As RoutedEventArgs)

        Dim ParentGrid As Grid = CType(Me.Parent, Grid)
        ParentGrid.Children.Remove(Me)

    End Sub

    Private Sub ItemAddDeleteMove(sender As Object, e As RoutedEventArgs)

        Dim ItemIndex As Int32 = LSTBX03_Categories.SelectedIndex
        Dim NewItemIndex As Int32 = -1

        If (sender IsNot BTN03_Add And sender IsNot BTN03_Update) And ItemIndex = -1 Then Exit Sub

        If sender Is BTN03_Add Then
            If CMBBX03_AnalysisCollection.SelectedIndex > -1 Then

                For Each ItemOfCollection In KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems
                    If ItemOfCollection.AnalyseCategoryName = LSTBX03_CategoriesLoaded.SelectedValue.ToString Then
                        Exit Sub
                    End If
                Next

                For Each CategoryItem In KiebitzCategories
                    If CategoryItem.CategoryInternalID = LSTBX03_CategoriesLoaded.SelectedValue.ToString Then
                        SelectedCategory = CategoryItem
                        Exit For
                    End If
                Next

                Dim ItemToAdd As New K8_CL04analyse With {
                    .AnalyseCategoryName = LSTBX03_CategoriesLoaded.SelectedValue.ToString,
                    .AnalyseCategoryColor = SelectedCategory.CategoryChartColor}

                KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems.Add(ItemToAdd)
                LSTBX03_Categories.SelectedIndex = LSTBX03_Categories.Items.Count - 1

            End If
            Exit Sub
        End If


        If sender Is BTN03_Remove Then
            If ItemIndex > -1 Then
                KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems.RemoveAt(ItemIndex)
                LSTBX03_Categories.SelectedIndex = -1
            End If
            Exit Sub
        End If

        If sender Is BTN03_Update Then

            For Each CollItem In KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems
                For Each LoadedItem In KiebitzCategories
                    If LoadedItem.CategoryInternalID = CollItem.AnalyseCategoryName Then
                        CollItem.AnalyseCategoryColor = LoadedItem.CategoryChartColor
                        Exit For
                    End If
                Next
            Next
            Exit Sub
        End If

        Dim ItemToMove As K8_CL04analyse = KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems.Item(ItemIndex)

        If sender Is BTN03_Up Then
            NewItemIndex = ItemIndex - 1
        ElseIf sender Is BTN03_Down Then
            NewItemIndex = ItemIndex + 1
            If NewItemIndex = LSTBX03_Categories.Items.Count Then
                NewItemIndex = ItemIndex
            End If
        End If

        If NewItemIndex >= 0 Then
            KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems.RemoveAt(ItemIndex)
            KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems.Insert(NewItemIndex, ItemToMove)
            LSTBX03_Categories.SelectedIndex = NewItemIndex
        End If

    End Sub

    Private Sub AddNewCollection(sender As Object, e As RoutedEventArgs)

        If TXTBX03_CollectionName.Text.Trim <> String.Empty Then

            Dim NewCollection As New K8_CL05analysis With {.AnalysisCollectionName = TXTBX03_CollectionName.Text.Trim}
            Dim ItemFound As Boolean = False

            For Each ItemOfCollection In KiebitzAllAnalysis
                If ItemOfCollection.AnalysisCollectionName = TXTBX03_CollectionName.Text.Trim Then
                    ItemFound = True
                    Exit Sub
                End If
            Next

            KiebitzAllAnalysis.Add(New K8_CL05analysis With {.AnalysisCollectionName = TXTBX03_CollectionName.Text.Trim, .AnalysisCollectionItems = New ObservableCollection(Of K8_CL04analyse)})
            CMBBX03_AnalysisCollection.SelectedIndex = CMBBX03_AnalysisCollection.Items.Count - 1
            TXTBX03_CollectionName.Text = String.Empty
        End If

    End Sub

    Private Sub ChangeOrderOfChart(sender As Object, e As RoutedEventArgs)

        VWBX03A.Visibility = Visibility.Hidden
        VWBX03B.Visibility = Visibility.Hidden

        If sender Is RDBTN_VWBX01 Then VWBX03A.Visibility = Visibility.Visible
        If sender Is RDBTN_VWBX02 Then VWBX03B.Visibility = Visibility.Visible

    End Sub

End Class
