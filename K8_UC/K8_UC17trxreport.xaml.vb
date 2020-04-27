Public Class K8_UC17trxreport

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        K8_CL17txreport.LoadXMLIntoCollection()

        Me.Content = MyTRXreporter.Result.UnitTestResult(0).ComputerName

    End Sub
End Class
