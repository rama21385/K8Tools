Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports K8Tools

Public Class K8_CL04analyse
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private _AnalyseCategoryName As String

    Public Property AnalyseCategoryName As String
        Get
            Return _AnalyseCategoryName
        End Get
        Set(value As String)
            _AnalyseCategoryName = value
            NotifyPropertyChanged()
        End Set
    End Property
End Class
