Imports System.ComponentModel

Namespace K8ENUMS

    Public Enum CollectionItemStatus

        none = 0
        add = 1
        modify = 2
        delete = 3

    End Enum

    Public Enum ValueUnits

        <Description("ohne Einheit")>
        none = 0
        <Description("°C ")>
        Temperature = 1
        <Description("kWh")>
        KilowattHour = 2
        <Description("qm")>
        Kubikmeter = 3
        <Description("km")>
        Kilometer = 4
        <Description("kg")>
        Kilogramm = 5

    End Enum

    Public Enum ChartType

        none = 0
        ChartPoint = 1
        ChartLine = 2
        ChartColumn = 3
        ChartArea = 4

    End Enum

End Namespace
