Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.CompilerServices
Imports K8Tools.K8ENUMS

Public Class K8_CL03measurement
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private _CategoryInternalID As String
    Private _MeasDateDay As UInt16
    Private _MeasDateMonth As UInt16
    Private _MeasDateYear As UInt16
    Private ReadOnly _MeasDate As DateTime
    Private ReadOnly _MeasDayOfYear As UInt16
    Private _MeasComment As String
    Private _MeasValue As Decimal
    Private _MeasStatus As K8ENUMS.CollectionItemStatus


    <XmlIgnore>
    Public Property CategoryInternalID As String
        Get
            Return _CategoryInternalID
        End Get
        Set(value As String)
            _CategoryInternalID = value
        End Set
    End Property

    Public Property MeasDateDay As UInt16
        Get
            Return _MeasDateDay
        End Get
        Set(value As UInt16)
            _MeasDateDay = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property MeasDateMonth As UInt16
        Get
            Return _MeasDateMonth
        End Get
        Set(value As UInt16)
            _MeasDateMonth = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property MeasDateYear As UInt16
        Get
            Return _MeasDateYear
        End Get
        Set(value As UInt16)
            _MeasDateYear = value
            NotifyPropertyChanged()
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
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property MeasValue As Decimal
        Get
            Return _MeasValue
        End Get
        Set(value As Decimal)
            _MeasValue = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public ReadOnly Property MeasDayOfYear As UInt16
        Get
            Return CUShort(DateAndTime.DatePart(DateInterval.DayOfYear, MeasDate))
        End Get
    End Property

    Public Property MeasStatus As CollectionItemStatus
        Get
            Return _MeasStatus
        End Get
        Set(value As CollectionItemStatus)
            _MeasStatus = value
            NotifyPropertyChanged()
        End Set
    End Property


    Public Shared Sub SaveCollectionAsXML(CategoryName As String)

        If KiebitzMeasCurve.Count = 0 Then Exit Sub

        For Each item In KiebitzMeasCurve.ToList 'otherwise error because removing an item from a list makes the list shorter than it was at the beginning of the loop
            If item.MeasStatus = CollectionItemStatus.delete Then
                KiebitzMeasCurve.Remove(item)
            Else
                item.MeasStatus = CollectionItemStatus.none
            End If
        Next

        If System.IO.Directory.Exists(K8_DirHome & CategoryName) = False Then
            System.IO.Directory.CreateDirectory(K8_DirHome & CategoryName)
        End If

        Dim xs As New XmlSerializer(KiebitzMeasCurve.GetType, KiebitzMeasCurve.Item(0).CategoryInternalID) '"K8_CL03curve")
        Dim tw As TextWriter = New StreamWriter(K8_DirHome & CategoryName & "\" & KiebitzMeasCurve.Item(0).CategoryInternalID & ".xml", False)
        xs.Serialize(tw, KiebitzMeasCurve)
        tw.Close()

    End Sub


    Public Shared Sub LoadXMLIntoCollection(ByRef TempCategoryID As String, CategoryName As String)

        KiebitzMeasCurve.Clear()

        If File.Exists(K8_DirHome & CategoryName & "\" & TempCategoryID & ".xml") = True Then

            Dim document As New XmlDocument
            document.Load(K8_DirHome & CategoryName & "\" & TempCategoryID & ".xml")
            Dim LoadedNamespace As String = document.DocumentElement.NamespaceURI

            Dim xs As New XmlSerializer(KiebitzMeasCurve.GetType, TempCategoryID)
            Dim stream As New MemoryStream
            document.Save(stream)
            stream.Flush()
            stream.Position = 0

            If LoadedNamespace = TempCategoryID Then
                KiebitzMeasCurve = CType(xs.Deserialize(stream), ObservableCollection(Of K8_CL03measurement))
                'eventually run updates
            Else
                Beep()
            End If
        End If
    End Sub


End Class
