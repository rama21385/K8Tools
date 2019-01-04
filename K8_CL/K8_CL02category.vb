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

    Public ReadOnly Property CategoryInternalID As String
        Get
            Return K8_CL98functions.GetCategoryID(_CategoryName, _CategoryYear)
        End Get
    End Property

    Private _CategoryName As String
    Private _CategoryUnit As K8ENUMS.ValueUnits
    Private _CategoryYear As UInt16
    Private _CategoryMeasValues As New ObservableCollection(Of K8_CL03measurement)
    Private _CategoryIsEnabled As Boolean
    Private _CategoryValuesRelativeTo1st As Boolean
    Private _CategoryStatus As K8ENUMS.CollectionItemStatus
    Private _CategoryChartColor As String
    Private _CategoryChartType As ChartType
    Private _CategoryChartYMin As Int32
    Private _CategoryChartYMax As Int32

    Private _CurveMin As Decimal
    Private _CurveMax As Decimal
    Private _CurveAvgYear As Decimal
    Private _CurveAvgHJ1 As Decimal
    Private _CurveAvgHJ2 As Decimal
    Private _CurveAvgQ1 As Decimal
    Private _CurveAvgQ2 As Decimal
    Private _CurveAvgQ3 As Decimal
    Private _CurveAvgQ4 As Decimal
    Private _CurveAvgJan As Decimal
    Private _CurveAvgFeb As Decimal
    Private _CurveAvgMar As Decimal
    Private _CurveAvgApr As Decimal
    Private _CurveAvgMay As Decimal
    Private _CurveAvgJun As Decimal
    Private _CurveAvgJul As Decimal
    Private _CurveAvgAug As Decimal
    Private _CurveAvgSep As Decimal
    Private _CurveAvgOct As Decimal
    Private _CurveAvgNov As Decimal
    Private _CurveAvgDec As Decimal
    Private _CurveDeltaYear As Decimal
    Private _CurveDeltaHJ1 As Decimal
    Private _CurveDeltaHJ2 As Decimal
    Private _CurveDeltaQ1 As Decimal
    Private _CurveDeltaQ2 As Decimal
    Private _CurveDeltaQ3 As Decimal
    Private _CurveDeltaQ4 As Decimal
    Private _CurveDeltaJan As Decimal
    Private _CurveDeltaFeb As Decimal
    Private _CurveDeltaMar As Decimal
    Private _CurveDeltaApr As Decimal
    Private _CurveDeltaMay As Decimal
    Private _CurveDeltaJun As Decimal
    Private _CurveDeltaJul As Decimal
    Private _CurveDeltaAug As Decimal
    Private _CurveDeltaSep As Decimal
    Private _CurveDeltaOct As Decimal
    Private _CurveDeltaNov As Decimal
    Private _CurveDeltaDec As Decimal

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


    ''' <summary>
    ''' Is an item being added, modified or deleted
    ''' </summary>
    ''' <returns></returns>
    Public Property CategoryStatus As CollectionItemStatus
        Get
            Return _CategoryStatus
        End Get
        Set(value As CollectionItemStatus)
            _CategoryStatus = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CategoryChartColor As String
        Get
            Return _CategoryChartColor
        End Get
        Set(value As String)
            _CategoryChartColor = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CategoryChartYMin As Int32
        Get
            Return _CategoryChartYMin
        End Get
        Set(value As Int32)
            _CategoryChartYMin = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CategoryChartYMax As Int32
        Get
            Return _CategoryChartYMax
        End Get
        Set(value As Int32)
            _CategoryChartYMax = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CategoryValuesRelativeTo1st As Boolean
        Get
            Return _CategoryValuesRelativeTo1st
        End Get
        Set(value As Boolean)
            _CategoryValuesRelativeTo1st = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveMin As Decimal
        Get
            Return Math.Round(_CurveMin, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveMin = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveMax As Decimal
        Get
            Return Math.Round(_CurveMax, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveMax = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgYear As Decimal
        Get
            Return Math.Round(_CurveAvgYear, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgYear = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgHJ1 As Decimal
        Get
            Return Math.Round(_CurveAvgHJ1, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgHJ1 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgHJ2 As Decimal
        Get
            Return Math.Round(_CurveAvgHJ2, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgHJ2 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgQ1 As Decimal
        Get
            Return Math.Round(_CurveAvgQ1, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgQ1 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgQ2 As Decimal
        Get
            Return Math.Round(_CurveAvgQ2, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgQ2 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgQ3 As Decimal
        Get
            Return Math.Round(_CurveAvgQ3, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgQ3 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgQ4 As Decimal
        Get
            Return Math.Round(_CurveAvgQ4, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgQ4 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgJan As Decimal
        Get
            Return Math.Round(_CurveAvgJan, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgJan = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgFeb As Decimal
        Get
            Return Math.Round(_CurveAvgFeb, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgFeb = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgMar As Decimal
        Get
            Return Math.Round(_CurveAvgMar, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgMar = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgApr As Decimal
        Get
            Return Math.Round(_CurveAvgApr, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgApr = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgMay As Decimal
        Get
            Return Math.Round(_CurveAvgMay, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgMay = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgJun As Decimal
        Get
            Return Math.Round(_CurveAvgJun, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgJun = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgJul As Decimal
        Get
            Return Math.Round(_CurveAvgJul, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgJul = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgAug As Decimal
        Get
            Return Math.Round(_CurveAvgAug, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgAug = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgSep As Decimal
        Get
            Return Math.Round(_CurveAvgSep, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgSep = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgOct As Decimal
        Get
            Return Math.Round(_CurveAvgOct, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgOct = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgNov As Decimal
        Get
            Return Math.Round(_CurveAvgNov, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgNov = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveAvgDec As Decimal
        Get
            Return Math.Round(_CurveAvgDec, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveAvgDec = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaYear As Decimal
        Get
            Return Math.Round(_CurveDeltaYear, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveDeltaYear = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaHJ1 As Decimal
        Get
            Return Math.Round(_CurveDeltaHJ1, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveDeltaHJ1 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaHJ2 As Decimal
        Get
            Return Math.Round(_CurveDeltaHJ2, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveDeltaHJ2 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaQ1 As Decimal
        Get
            Return Math.Round(_CurveDeltaQ1, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveDeltaQ1 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaQ2 As Decimal
        Get
            Return Math.Round(_CurveDeltaQ2, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveDeltaQ2 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaQ3 As Decimal
        Get
            Return Math.Round(_CurveDeltaQ3, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveDeltaQ3 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaQ4 As Decimal
        Get
            Return Math.Round(_CurveDeltaQ4, 2, MidpointRounding.AwayFromZero)
        End Get
        Set(value As Decimal)
            _CurveDeltaQ4 = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaJan As Decimal
        Get
            Return _CurveDeltaJan
        End Get
        Set(value As Decimal)
            _CurveDeltaJan = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaFeb As Decimal
        Get
            Return _CurveDeltaFeb
        End Get
        Set(value As Decimal)
            _CurveDeltaFeb = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaMar As Decimal
        Get
            Return _CurveDeltaMar
        End Get
        Set(value As Decimal)
            _CurveDeltaMar = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaApr As Decimal
        Get
            Return _CurveDeltaApr
        End Get
        Set(value As Decimal)
            _CurveDeltaApr = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaMay As Decimal
        Get
            Return _CurveDeltaMay
        End Get
        Set(value As Decimal)
            _CurveDeltaMay = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaJun As Decimal
        Get
            Return _CurveDeltaJun
        End Get
        Set(value As Decimal)
            _CurveDeltaJun = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaJul As Decimal
        Get
            Return _CurveDeltaJul
        End Get
        Set(value As Decimal)
            _CurveDeltaJul = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaAug As Decimal
        Get
            Return _CurveDeltaAug
        End Get
        Set(value As Decimal)
            _CurveDeltaAug = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaSep As Decimal
        Get
            Return _CurveDeltaSep
        End Get
        Set(value As Decimal)
            _CurveDeltaSep = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaOct As Decimal
        Get
            Return _CurveDeltaOct
        End Get
        Set(value As Decimal)
            _CurveDeltaOct = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaNov As Decimal
        Get
            Return _CurveDeltaNov
        End Get
        Set(value As Decimal)
            _CurveDeltaNov = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property CurveDeltaDec As Decimal
        Get
            Return _CurveDeltaDec
        End Get
        Set(value As Decimal)
            _CurveDeltaDec = value
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
