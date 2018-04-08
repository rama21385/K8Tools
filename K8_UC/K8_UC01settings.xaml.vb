Public Class K8_UC01settings



    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        LSTVW01_Categories.DataContext = KiebitzCats

        'K8_CL02category.LoadXMLIntoCollection()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'CMBBX01_CategoryUnits.DataSource =  Enum.GetValues(TypeOf(K8_CL02category.ValueUnit))

    End Sub

    Private Sub AddModifyDeleteCategory(sender As Object, e As RoutedEventArgs)

        Dim TempYear As UInt16 = CUInt(Val(TXTBX01_CategoryYear.Text.Trim))
        Dim TempName As String = TXTBX01_CategoryName.Text.Trim
        Dim TempCatID As String = K8_CL98functions.GetCategoryID(TempName, TempYear)

        If TempCatID = String.Empty Then Exit Sub

        If sender Is BTN01_CategoryAdd Then

            For Each TempCategory In KiebitzCats
                If TempCatID = TempCategory.CategoryInternalID Then
                    Exit Sub
                End If
            Next

            KiebitzCats.Add(New K8_CL02category With {
                            .CategoryStatus = K8ENUMS.CollectionItemStatus.add,
                            .CategoryIsEnabled = CBool(CHKBX01_CategoryEnabled.IsChecked),
                            .CategoryMeasValues = Nothing,
                            .CategoryName = TempName,
                            .CategoryUnit = CType([Enum].Parse(GetType(K8ENUMS.ValueUnits), CMBBX01_CategoryUnits.Text), K8ENUMS.ValueUnits),
                            .CategoryYear = TempYear})

        ElseIf sender Is BTN01_CategoryModify Then

        ElseIf sender Is BTN01_CategoryDelete Then
            For Each TempCategory In KiebitzCats
                If TempCatID = TempCategory.CategoryInternalID Then
                    If TempCategory.CategoryStatus = K8ENUMS.CollectionItemStatus.delete Then
                        TempCategory.CategoryStatus = K8ENUMS.CollectionItemStatus.none
                    Else
                        TempCategory.CategoryStatus = K8ENUMS.CollectionItemStatus.delete
                    End If
                    Exit Sub
                End If
            Next
        End If

    End Sub

    Private Sub ClearCategory(sender As Object, e As RoutedEventArgs)

        TXTBX01_CategoryName.Text = ""
        TXTBX01_CategoryName.IsEnabled = True
        TXTBX01_CategoryYear.Text = ""
        TXTBX01_CategoryYear.IsEnabled = True
        CMBBX01_CategoryUnits.SelectedIndex = -1
        CMBBX01_CategoryUnits.IsEnabled = True
        CHKBX01_CategoryEnabled.IsChecked = True
        CHKBX01_CategoryEnabled.IsEnabled = True

    End Sub


    Private Sub SaveSettings(sender As Object, e As RoutedEventArgs)

        If sender Is BTN01_SAVEsettings Then
            K8_CL02category.SaveCollectionAsXML()
        ElseIf sender Is BTN01_EXITnosave Then
            K8_CL02category.LoadXMLIntoCollection()
            LSTVW01_Categories.DataContext = KiebitzCats
        End If

    End Sub

End Class
