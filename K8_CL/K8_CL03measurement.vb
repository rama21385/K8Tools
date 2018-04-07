Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class K8_CL03measurement
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private _MeasDateDay As UInt16
    Private _MeasDateMonth As UInt16
    Private _MeasDateYear As UInt16
    Private ReadOnly _MeasDate As DateTime
    Private ReadOnly _MeasDayOfYear As UInt16
    Private _MeasComment As String
    Private _MeasValue As Decimal

    Public Property MeasDateDay As UInt16
        Get
            Return _MeasDateDay
        End Get
        Set(value As UInt16)
            _MeasDateDay = value
        End Set
    End Property

    Public Property MeasDateMonth As UInt16
        Get
            Return _MeasDateMonth
        End Get
        Set(value As UInt16)
            _MeasDateMonth = value
        End Set
    End Property

    Public Property MeasDateYear As UInt16
        Get
            Return _MeasDateYear
        End Get
        Set(value As UInt16)
            _MeasDateYear = value
        End Set
    End Property

    Public ReadOnly Property MeasDate As DateTime
        Get
            Return New DateTime(_MeasDateYear, _MeasDateMonth, _MeasDateDay, 0, 0, 0)
        End Get
    End Property

    Public Property MeasComment As String
        Get
            Return _MeasComment
        End Get
        Set(value As String)
            _MeasComment = value
        End Set
    End Property

    Public Property MeasValue As Decimal
        Get
            Return _MeasValue
        End Get
        Set(value As Decimal)
            _MeasValue = value
        End Set
    End Property

    Public ReadOnly Property MeasDayOfYear As UInt16
        Get
            Return _MeasDayOfYear
        End Get
    End Property
End Class
