Imports K8Tools.K8ENUMS

Class MainWindow

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        K8_CL02category.LoadXMLIntoCollection()

        'MyCurve1.Add(New K8_CL03measurement With {
        '    .MeasDateDay = 1,
        '    .MeasDateMonth = 1,
        '    .MeasDateYear = 2018,
        '    .MeasValue = 42.1,
        '    .MeasComment = "42.1c"})
        'MyCurve1.Add(New K8_CL03measurement With {
        '    .MeasDateDay = 2,
        '    .MeasDateMonth = 1,
        '    .MeasDateYear = 2018,
        '    .MeasValue = 42.2,
        '    .MeasComment = "42.2c"})
        'MyCurve1.Add(New K8_CL03measurement With {
        '    .MeasDateDay = 3,
        '    .MeasDateMonth = 1,
        '    .MeasDateYear = 2018,
        '    .MeasValue = 42.3,
        '    .MeasComment = "42.3c"})

        'MyCurve2.Add(New K8_CL03measurement With {
        '    .MeasDateDay = 3,
        '    .MeasDateMonth = 2,
        '    .MeasDateYear = 2017,
        '    .MeasValue = 1.14,
        '    .MeasComment = "1.14c"})
        'MyCurve2.Add(New K8_CL03measurement With {
        '    .MeasDateDay = 2,
        '    .MeasDateMonth = 2,
        '    .MeasDateYear = 2017,
        '    .MeasValue = 2.71,
        '    .MeasComment = "2.71c"})
        'MyCurve2.Add(New K8_CL03measurement With {
        '    .MeasDateDay = 1,
        '    .MeasDateMonth = 2,
        '    .MeasDateYear = 2017,
        '    .MeasValue = 3.14,
        '    .MeasComment = "3.14c"})

        '' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'KiebitzCats.Add(New K8_CL02category With {
        '            .CategoryName = "Strom",
        '            .CategoryYear = 2018,
        '            .CategoryIsEnabled = True,
        '            .CategoryUnit = ValueUnits.Kilogramm,
        '            .CategoryMeasValues = MyCurve1})
        'KiebitzCats.Add(New K8_CL02category With {
        '            .CategoryName = "Wasser",
        '            .CategoryYear = 2017,
        '            .CategoryIsEnabled = False,
        '            .CategoryUnit = ValueUnits.Kubikmeter,
        '            .CategoryMeasValues = MyCurve2})

    End Sub

    Private Sub K8Exit()

        Me.Close() 'this will call MainWindow_Closing. If it is cancelled there it is cancelled here as well

    End Sub

    Private Sub OpenMyUserControl(sender As Object, e As RoutedEventArgs)

        If RemoveChildWindow() = False Then Exit Sub

        If sender Is BTN_01settings Then
            NumberOfActiveMenu = UserControlItems.UC01settings
        ElseIf sender Is BTN_02input Then
            NumberOfActiveMenu = UserControlItems.UC02input
        End If

        If NumberOfActiveMenu > UserControlItems.none Then
            Call AddChildWindow()
        End If

    End Sub

    Private Sub AddChildWindow()

        Dim K8UC As New UserControl

        Select Case NumberOfActiveMenu
            Case UserControlItems.UC01settings
                K8UC = New K8_UC01settings
            Case UserControlItems.UC02input
                K8UC = New K8_UC02input
        End Select

        If IsNothing(K8UC) = True Then Exit Sub

        PositionUConGrid(MainGrid, 1, 1, 1, 1, K8UC, NumberOfActiveMenu.ToString)

    End Sub

    Private Sub PositionUConGrid(XLoopGrid As Grid, GridRow As Int32, GridRowSpan As Int32, GridCol As Int32, GridColSpan As Int32, K8UC As UserControl, K8UCname As String)


        XLoopGrid.RegisterName(K8UCname, K8UC)
        XLoopGrid.Children.Add(K8UC)

        Grid.SetRow(K8UC, GridRow)
        Grid.SetRowSpan(K8UC, GridRowSpan)
        Grid.SetColumn(K8UC, GridCol)
        Grid.SetColumnSpan(K8UC, GridColSpan)

    End Sub

    Private Function RemoveChildWindow(Optional XLoopClosing As Boolean = False) As Boolean

        If NumberOfActiveMenu = UserControlItems.none Then Return True 'Nothing to do

        Dim K8UserControls As Array = System.Enum.GetValues(GetType(K8_MD01main.UserControlItems))

        For Each K8UC In K8UserControls
            If NumberOfActiveMenu.ToString = K8UC.ToString Then
                MainGrid.Children.Remove(CType(MainGrid.FindName(K8UC.ToString), UIElement))
                MainGrid.UnregisterName(K8UC.ToString)
                NumberOfActiveMenu = UserControlItems.none
                Return True
            End If
        Next

        Return False

    End Function
End Class
