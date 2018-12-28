Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class K8_UC03analyse
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        K8_CL05analysis.LoadXMLIntoCollection()

        CMBBX03_Category.DataContext = KiebitzCats
        CMBBX03_AnalysisCollection.DataContext = KiebitzAllAnalysis

        CMBBX03_Category.SelectedIndex = -1

    End Sub

    Private Sub LoadAndSortTheList(sender As Object, e As SelectionChangedEventArgs)

        LSTBX03_Categories.DataContext = KiebitzAllAnalysis(CMBBX03_AnalysisCollection.SelectedIndex).AnalysisCollectionItems

    End Sub

    Private Sub SaveSettings(sender As Object, e As RoutedEventArgs)

        Dim SetFound As Boolean = False

        If sender Is BTN03_SAVEsettings Then
            KiebitzAnalyse.Clear()
            For Each CategoryItem In LSTBX03_Categories.Items
                KiebitzAnalyse.Add(New K8_CL04analyse With {.AnalyseCategoryName = CategoryItem.ToString})
            Next

            For Each AnalyseSet In KiebitzAllAnalysis
                If AnalyseSet.AnalysisCollectionName = TXTBX03_CollectionName.Text.Trim Then
                    AnalyseSet.AnalysisCollectionItems = KiebitzAnalyse
                    AnalyseSet.AnalysisCollectionName = TXTBX03_CollectionName.Text.Trim
                    SetFound = True
                    Exit For
                End If
            Next

            If SetFound = False Then

                KiebitzAllAnalysis.Add(New K8_CL05analysis With {
                    .AnalysisCollectionName = TXTBX03_CollectionName.Text.Trim,
                    .AnalysisCollectionItems = KiebitzAnalyse})

            End If

            K8_CL05analysis.SaveCollectionAsXML()

        ElseIf sender Is BTN03_EXITnosave Then
            CloseMe(Nothing, Nothing)
        End If

    End Sub

    Private Sub CloseMe(sender As Object, e As RoutedEventArgs)

        Dim ParentGrid As Grid = CType(Me.Parent, Grid)
        ParentGrid.Children.Remove(Me)

    End Sub

    Private Sub AddNewMeasValue(sender As Object, e As RoutedEventArgs)

        If LSTBX03_Categories.Items.Contains(CMBBX03_Category.SelectedValue) = False Then
            LSTBX03_Categories.Items.Add(CMBBX03_Category.SelectedValue)
        End If

    End Sub
End Class
