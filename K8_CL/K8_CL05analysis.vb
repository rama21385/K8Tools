Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Xml
Imports System.Xml.Serialization

Public Class K8_CL05analysis
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private _AnalysisCollectionName As String
    Private _AnalysisCollectionItems As New ObservableCollection(Of K8_CL04analyse)

    Public Property AnalysisCollectionName As String
        Get
            Return _AnalysisCollectionName
        End Get
        Set(value As String)
            _AnalysisCollectionName = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property AnalysisCollectionItems As ObservableCollection(Of K8_CL04analyse)
        Get
            Return _AnalysisCollectionItems
        End Get
        Set(value As ObservableCollection(Of K8_CL04analyse))
            _AnalysisCollectionItems = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Shared Sub SaveCollectionAsXML()

        If KiebitzAllAnalysis.Count = 0 Then Exit Sub

        Dim xs As New XmlSerializer(KiebitzAllAnalysis.GetType, "AllAnalysis")
        Dim tw As TextWriter = New StreamWriter(K8_DirSettings & "AllAnalysis.xml", False)
        xs.Serialize(tw, KiebitzAllAnalysis)
        tw.Close()

    End Sub


    Public Shared Sub LoadXMLIntoCollection()

        KiebitzAllAnalysis.Clear()

        If File.Exists(K8_DirSettings & "AllAnalysis.xml") = True Then

            Dim document As New XmlDocument
            document.Load(K8_DirSettings & "AllAnalysis.xml")
            Dim LoadedNamespace As String = document.DocumentElement.NamespaceURI

            Dim xs As New XmlSerializer(KiebitzAllAnalysis.GetType, "AllAnalysis")
            Dim stream As New MemoryStream
            document.Save(stream)
            stream.Flush()
            stream.Position = 0

            If LoadedNamespace = "AllAnalysis" Then
                KiebitzAllAnalysis = CType(xs.Deserialize(stream), ObservableCollection(Of K8_CL05analysis))
                'eventually run updates
            Else
                Beep()
            End If
        End If
    End Sub

End Class
