Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Xml
Imports System.Xml.Serialization
Imports K8Tools
Imports K8Tools.K8ENUMS

Public Class K8_CL02category
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private _CategoryName As String
    Private _CategoryUnit As K8ENUMS.ValueUnits
    Private _CategoryYear As UInt16
    Private _CategoryMeasValues As New ObservableCollection(Of K8_CL03measurement)
    Private _CategoryIsEnabled As Boolean
    Private _CategoryStatus As K8ENUMS.CollectionItemStatus
    Private _CategoryColor As Color
    Private _CategoryChartType As ChartType

    Public Property CategoryName As String
        Get
            Return _CategoryName
        End Get
        Set(value As String)
            _CategoryName = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CategoryUnit As K8ENUMS.ValueUnits
        Get
            Return _CategoryUnit
        End Get
        Set(value As K8ENUMS.ValueUnits)
            _CategoryUnit = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CategoryYear As UInt16
        Get
            Return _CategoryYear
        End Get
        Set(value As UInt16)
            _CategoryYear = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CategoryIsEnabled As Boolean
        Get
            Return _CategoryIsEnabled
        End Get
        Set(value As Boolean)
            _CategoryIsEnabled = value
            NotifyPropertyChanged()
        End Set
    End Property

    <XmlIgnore>
    Public Property CategoryMeasValues As ObservableCollection(Of K8_CL03measurement)
        Get
            Return _CategoryMeasValues
        End Get
        Set(value As ObservableCollection(Of K8_CL03measurement))
            _CategoryMeasValues = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public ReadOnly Property CategoryInternalID As String
        Get
            Return K8_CL98functions.GetCategoryID(_CategoryName, _CategoryYear)
        End Get
    End Property

    Public Property CategoryStatus As CollectionItemStatus
        Get
            Return _CategoryStatus
        End Get
        Set(value As CollectionItemStatus)
            _CategoryStatus = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CategoryColor As Color
        Get
            Return _CategoryColor
        End Get
        Set(value As Color)
            _CategoryColor = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Shared Sub SaveCollectionAsXML()

        For Each item In KiebitzCats.ToList 'otherwise error because removing an item from a list makes the list shorter than it was at the beginning of the loop
            If item.CategoryStatus = CollectionItemStatus.delete Then
                KiebitzCats.Remove(item)
            Else
                item.CategoryStatus = CollectionItemStatus.none
            End If
        Next

        Dim xs As New XmlSerializer(KiebitzCats.GetType, "K8_CL02category")
        Dim tw As TextWriter = New StreamWriter("K8_Categories.xml")
        xs.Serialize(tw, KiebitzCats)
        tw.Close()

    End Sub


    Public Shared Sub LoadXMLIntoCollection()

        KiebitzCats.Clear()

        If File.Exists("K8_Categories.xml") = True Then

            Dim document As New XmlDocument
            document.Load("K8_Categories.xml")
            Dim LoadedNamespace As String = document.DocumentElement.NamespaceURI

            Dim xs As New XmlSerializer(KiebitzCats.GetType, "K8_CL02category")
            Dim stream As New MemoryStream
            document.Save(stream)
            stream.Flush()
            stream.Position = 0

            If LoadedNamespace = "K8_CL02category" Then
                KiebitzCats = CType(xs.Deserialize(stream), ObservableCollection(Of K8_CL02category))
                'eventually run updates
            Else
                Beep()
            End If
        End If
    End Sub


End Class
