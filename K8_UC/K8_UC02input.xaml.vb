Imports System.ComponentModel

Public Class K8_UC02input
    Private SelectedMeasGroup As String

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        CMBBX02_Category.DataContext = KiebitzCats
        DTGRD02_Measurements.DataContext = KiebitzCurve

        CMBBX02_Category.SelectedIndex = -1

    End Sub


    Private Sub AddNewMeasValue(sender As Object, e As RoutedEventArgs)

        KiebitzCurve.Add(New K8_CL03measurement With {
            .CategoryInternalID = CMBBX02_Category.SelectedValue,
            .MeasComment = TXTBX0_MeasComment.Text,
            .MeasValue = CDec(Val(TXTBX0_MeasValue.Text)),
            .MeasDateDay = CInt(Val(TXTBX0_MeasDay.Text)),
            .MeasDateMonth = CInt(Val(TXTBX0_MeasMonth.Text)),
            .MeasDateYear = CInt((TXTBX0_MeasYear.Text)),
            .MeasStatus = K8ENUMS.CollectionItemStatus.add})

        DTGRD02_Measurements.DataContext = KiebitzCurve
        DTGRD02_Measurements.ScrollIntoView(KiebitzCurve.Item(KiebitzCurve.Count-1))

    End Sub

    Private Sub DeleteMeasValue(sender As Object, e As RoutedEventArgs)


    End Sub

    Private Sub ModifyMeasValue(sender As Object, e As RoutedEventArgs)


    End Sub

    Private Sub LoadAndSortTheList(sender As Object, e As SelectionChangedEventArgs)

        Dim TempCategoryID As String = CMBBX02_Category.Text = ""
        K8_CL03measurement.LoadXMLIntoCollection(TempCategoryID)
        DTGRD02_Measurements.DataContext = KiebitzCurve


        DTGRD02_Measurements.Items.SortDescriptions.Clear()
        DTGRD02_Measurements.Items.SortDescriptions.Add(New SortDescription("MeasDate", ListSortDirection.Ascending))
        DTGRD02_Measurements.Items.Refresh()
        SelectedMeasGroup = CType(CMBBX02_Category.SelectedItem, K8_CL02category).CategoryName.ToString

    End Sub

    Private Sub SaveSettings(sender As Object, e As RoutedEventArgs)

        If sender Is BTN01_SAVEsettings Then
            K8_CL03measurement.SaveCollectionAsXML()
        ElseIf sender Is BTN01_EXITnosave Then
            CloseMe(Nothing, Nothing)
        End If

    End Sub

    Private Sub CloseMe(sender As Object, e As RoutedEventArgs)

        Dim ParentGrid As Grid = CType(Me.Parent, Grid)
        ParentGrid.Children.Remove(Me)

    End Sub

End Class
