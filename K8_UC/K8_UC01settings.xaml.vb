Imports System.ComponentModel
Imports System.Drawing
Imports MRLColorPicker

Public Class K8_UC01settings


    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        LSTVW01_Categories.DataContext = KiebitzCategories

        'K8_CL02category.LoadXMLIntoCollection()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'CMBBX01_CategoryUnits.DataSource =  Enum.GetValues(TypeOf(K8_CL02category.ValueUnit))

    End Sub

    Private Sub AddModifyDeleteCategory(sender As Object, e As RoutedEventArgs)

        Dim TempYear As UInt16 = CUShort(Val(TXTBX01_CategoryYear.Text.Trim))
        Dim TempName As String = CMBBX01_CategoryNames.Text '   TXTBX01_CategoryName.Text.Trim
        Dim TempCatID As String = K8_CL98functions.GetCategoryID(TempName, TempYear)

        If TempCatID = String.Empty Then Exit Sub

        If sender Is BTN01_CategoryAdd Then

            For Each TempCategory In KiebitzCategories
                If TempCatID = TempCategory.CategoryInternalID Then
                    Exit Sub
                End If
            Next

            KiebitzCategories.Add(New K8_CL02category With {
                            .CategoryStatus = K8ENUMS.CollectionItemStatus.add,
                            .CategoryIsEnabled = CBool(CHKBX01_CategoryEnabled.IsChecked),
                            .CategoryValuesRelativeTo1st = CBool(CHKBX01_CategoryRelativeTo1st.IsChecked),
                            .CategoryMeasValues = Nothing,
                            .CategoryName = TempName,
                            .CategoryUnit = CType([Enum].Parse(GetType(K8ENUMS.ValueUnits), CMBBX01_CategoryUnits.Text), K8ENUMS.ValueUnits),
                            .CategoryYear = TempYear,
                            .CategoryChartColor = TXTBX01_CategoryColor.Text,
                            .CategoryChartYMin = CInt(Val(TXTBX01_ChartMin.Text)),
                            .CategoryChartYMax = CInt(Val(TXTBX01_ChartMax.Text))})

            LSTVW01_Categories.Items.SortDescriptions.Clear()
            LSTVW01_Categories.Items.SortDescriptions.Add(New SortDescription("CategoryInternalID", ListSortDirection.Ascending))
            LSTVW01_Categories.Items.Refresh()



        ElseIf sender Is BTN01_CategoryModify Then
            For Each TempCategory In KiebitzCategories
                If TempCatID = TempCategory.CategoryInternalID Then
                    With TempCategory
                        .CategoryStatus = K8ENUMS.CollectionItemStatus.modify
                        .CategoryIsEnabled = CBool(CHKBX01_CategoryEnabled.IsChecked)
                        .CategoryValuesRelativeTo1st = CBool(CHKBX01_CategoryRelativeTo1st.IsChecked)
                        .CategoryMeasValues = Nothing
                        .CategoryName = TempName
                        .CategoryUnit = CType([Enum].Parse(GetType(K8ENUMS.ValueUnits), CMBBX01_CategoryUnits.Text), K8ENUMS.ValueUnits)
                        .CategoryYear = TempYear
                        .CategoryChartColor = TXTBX01_CategoryColor.Text
                        .CategoryChartYMin = CInt(Val(TXTBX01_ChartMin.Text))
                        .CategoryChartYMax = CInt(Val(TXTBX01_ChartMax.Text))
                    End With
                    Exit For
                End If
            Next
        ElseIf sender Is BTN01_CategoryDelete Then
            For Each TempCategory In KiebitzCategories
                If TempCatID = TempCategory.CategoryInternalID Then
                    If TempCategory.CategoryStatus = K8ENUMS.CollectionItemStatus.delete Then
                        TempCategory.CategoryStatus = K8ENUMS.CollectionItemStatus.none
                    Else
                        TempCategory.CategoryStatus = K8ENUMS.CollectionItemStatus.delete
                    End If
                    Exit For
                End If
            Next
        End If


    End Sub



    Private Sub SaveSettings(sender As Object, e As RoutedEventArgs)

        If sender Is BTN01_SAVEsettings Then
            K8_CL02category.SaveCollectionAsXML()
        ElseIf sender Is BTN01_EXITnosave Then
            K8_CL02category.LoadXMLIntoCollection()
            LSTVW01_Categories.DataContext = KiebitzCategories
        End If

    End Sub

    Public Sub ShowMyMRLPicker(sender As Object, e As RoutedEventArgs)

        ShowMRLPicker(Me)

    End Sub

    Public Sub ShowMRLPicker(dependencyObject As DependencyObject)

        Dim SelectedColor As String = "White"

        Dim MyForm As New MRLColorPicker.UCcolors
        MyForm.Owner = Window.GetWindow(dependencyObject)
        If MyForm.ShowDialog = True Then
            TXTBX01_CategoryColor.Text = MyForm.SelectedRectangle.SelectedFillColor.ToString
        Else
            Exit Sub
        End If

        RCTNGL01_Color.Fill = MyForm.SelectedRectangle.SelectedFillColor

    End Sub

    Private Sub ClearCategory(sender As Object, e As RoutedEventArgs)

        LSTVW01_Categories.SelectedIndex = -1

    End Sub
End Class
