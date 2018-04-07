Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports K8Tools

Public Class K8_CL02category
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Enum ValueUnit

        none = 0
        Temperature = 1
        KilowattHour = 2
        Kubikmeter = 3
        Kilometer = 4
        Kilogramm = 5

    End Enum

    Private _CategoryName As String
    Private _CategoryUnit As ValueUnit
    Private _CategoryMeasValues As New ObservableCollection(Of K8_CL03measurement)

    Public Property CategoryName As String
        Get
            Return _CategoryName
        End Get
        Set(value As String)
            _CategoryName = value
        End Set
    End Property

    Public Property CategoryUnit As ValueUnit
        Get
            Return _CategoryUnit
        End Get
        Set(value As ValueUnit)
            _CategoryUnit = value
        End Set
    End Property

    Public Property CategoryMeasValues As ObservableCollection(Of K8_CL03measurement)
        Get
            Return _CategoryMeasValues
        End Get
        Set(value As ObservableCollection(Of K8_CL03measurement))
            _CategoryMeasValues = value
        End Set
    End Property
End Class
