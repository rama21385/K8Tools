Imports System.Globalization

Namespace KIEBITZ8

    Public Class K8_CON01boolean
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            'Throw New NotImplementedException()

            If CType(value, Boolean) = False Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            'Throw New NotImplementedException()

            Return Binding.DoNothing
        End Function

    End Class

End Namespace
