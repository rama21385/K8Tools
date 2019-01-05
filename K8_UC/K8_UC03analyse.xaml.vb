Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class K8_UC03analyse
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Dim MultiCurveChartLeft As New K8_UC99chart
    Dim MultiCurveChartRight As New K8_UC99chart
    Private SelectedCategory As New K8_CL02category

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        K8_CL05analysis.LoadXMLIntoCollection()

        CMBBX03_AnalysisCollection.DataContext = KiebitzAllAnalysis
        LSTBX03_CategoriesLoaded.DataContext = KiebitzCategories

        ResetChart()

    End Sub

    Private Sub LoadAndSortTheList(sender As Object, e As SelectionChangedEventArgs)

        Dim CounterChart As Int32 = 0
        ResetChart()

        If CMBBX03_AnalysisCollection.SelectedIndex > -1 Then

            LSTBX03_Categories.ItemsSource = KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems

            For Each CollItem As K8_CL04analyse In LSTBX03_Categories.Items
                CounterChart += 1

                K8_CL03measurement.LoadXMLIntoCollection(CollItem.AnalyseCategoryName, CollItem.AnalyseCategoryName.Substring(0, InStr(CollItem.AnalyseCategoryName, "_") - 1))

                For Each CategoryItem In KiebitzCategories
                    If CategoryItem.CategoryInternalID = CollItem.AnalyseCategoryName Then
                        SelectedCategory = CategoryItem
                        Exit For
                    End If
                Next

                MultiCurveChartLeft.SelectedCategory = SelectedCategory
                If CounterChart = 1 Then MultiCurveChartLeft.RefreshChart()

                If LSTBX03_Categories.SelectedItem Is CollItem Then
                    MultiCurveChartLeft.AddCurveToChart(KiebitzMeasCurve, True)
                Else
                    MultiCurveChartLeft.AddCurveToChart(KiebitzMeasCurve, False)
                End If

            Next
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
        'MultiCurveChartLeft = New K8_UC99chart
        VWBX03A.Child = MultiCurveChartLeft

        VWBX03B.Child = Nothing
        'MultiCurveChartRight = New K8_UC99chart
        VWBX03B.Child = MultiCurveChartRight

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
                    If ItemOfCollection.AnalyseCategoryName Is LSTBX03_CategoriesLoaded.SelectedValue Then Exit Sub
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
        End If

    End Sub
End Class
