Public Class K8_UC02input

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        CMBBX02_Category.DataContext = Kiebitz
        DTGRD02_Measurements.DataContext = Kiebitz
    End Sub

    Private Sub AddNewMeasValue(sender As Object, e As RoutedEventArgs)

        MyCurve1.Add(New K8_CL03measurement With {
                    .MeasComment = TXTBX0_MeasComment.Text,
                    .MeasValue = TXTBX0_MeasValue.Text,
                    .MeasDateDay = TXTBX0_MeasDay.Text,
                    .MeasDateMonth = TXTBX0_MeasMonth.Text,
                    .MeasDateYear = TXTBX0_MeasYear.Text})
    End Sub

    Private Sub SortTheList(sender As Object, e As SelectionChangedEventArgs)

    End Sub
End Class
