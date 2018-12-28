Imports K8Tools.K8ENUMS

Class MainWindow

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        K8_CL02category.LoadXMLIntoCollection()

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
        ElseIf sender Is BTN_03analyse Then
            NumberOfActiveMenu = UserControlItems.UC03analyse
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
            Case UserControlItems.UC03analyse
                K8UC = New K8_UC03analyse
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
