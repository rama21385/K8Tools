Imports System.Globalization

Namespace KIEBITZ8

    Public Class K8_CON02visibility
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            'Throw New NotImplementedException()

            If CType(value, Boolean) = True Then
                If CType(parameter, Boolean) = False Then
                    Return Visibility.Visible
                Else
                    Return Visibility.Collapsed
                End If
            Else
                If CType(parameter, Boolean) = False Then
                    Return Visibility.Collapsed
                Else
                    Return Visibility.Visible
                End If
            End If

        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            'Throw New NotImplementedException()

            Return Binding.DoNothing
        End Function

    End Class

End Namespace
