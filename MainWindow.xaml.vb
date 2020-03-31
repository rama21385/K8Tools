Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports K8Tools.K8ENUMS

Class MainWindow
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private _selectedTheme As ResourceDictionary

    Public Property SelectedTheme As ResourceDictionary
        Get
            Return _selectedTheme
        End Get
        Set(value As ResourceDictionary)
            Try
                Resources.MergedDictionaries.Remove(SelectedTheme)
            Catch ex As Exception
            End Try

            _selectedTheme = value
            Me.Resources.MergedDictionaries.Add(_selectedTheme)
            NotifyPropertyChanged()
        End Set
    End Property

    Public Sub New()

        Dim myResourceDictionary As New ResourceDictionary With {
            .Source = New Uri("/Resources/Style_Default.xaml", UriKind.RelativeOrAbsolute)
        }
        SelectedTheme = myResourceDictionary

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

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
        ElseIf sender Is BTN_21password Then
            NumberOfActiveMenu = UserControlItems.UC11password
        ElseIf sender Is BTN_22stringFormat Then
            NumberOfActiveMenu = UserControlItems.UC12stringformat
        ElseIf sender Is BTN_23colors Then
            NumberOfActiveMenu = UserControlItems.UC13colors
        ElseIf sender Is BTN_24ascii Then
            NumberOfActiveMenu = UserControlItems.UC14ascii
        ElseIf sender Is BTN_25mosaiqcrc Then
            NumberOfActiveMenu = UserControlItems.UC15mosaiqcrc
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

            Case UserControlItems.UC11password
                K8UC = New K8_UC11winpassword
            Case UserControlItems.UC12stringformat
                K8UC = New K8_UC12stringformat
            Case UserControlItems.UC13colors
                K8UC = New K8_UC13colors
            Case UserControlItems.UC14ascii
                K8UC = New K8_UC14ascii
            Case UserControlItems.UC15mosaiqcrc
                K8UC = New K8_UC15mosaiqcrc
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

    Private Sub K8IsLoaded(sender As Object, e As RoutedEventArgs)

        For Each K8Dir In {K8_DirHome, K8_DirCSV, K8_DirReport, K8_DirSettings}

            If System.IO.Directory.Exists(K8Dir) = False Then
                System.IO.Directory.CreateDirectory(K8Dir)
            End If

        Next

        K8_CL02category.LoadXMLIntoCollection()

        'Dim myResourceDictionary As New ResourceDictionary
        'myResourceDictionary.Source = New Uri("/Resources/Style_Dark.xaml", UriKind.RelativeOrAbsolute)
        'SelectedTheme = myResourceDictionary

    End Sub
End Class
