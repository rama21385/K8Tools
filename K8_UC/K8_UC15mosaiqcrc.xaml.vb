Public Class K8_UC15mosaiqcrc

    Dim CRC_TAB(255) As Integer

    Public Sub New()

        InitializeComponent()

        Call ReadCRCTable()

    End Sub

    Private Sub BerechneCRC(sender As Object, e As RoutedEventArgs)

        Me.TXTBX15_CRC.Text = MOSAIQCRC(Me.TXTBX15_TextSource.Text).ToString

    End Sub

    Function MOSAIQCRC(CRCzeile As String) As Long

        Dim I As Integer
        Dim Zeichen As String
        Dim Seed As Integer

        Seed = &H521
        For I = 1 To Len(CRCzeile)
            Zeichen = Mid(CRCzeile, I, 1)
            Seed = CRC_TAB(Asc(Zeichen) Xor Int((Seed And 65535) Mod 256)) Xor Int((Seed And 65535) \ 256)
        Next I
        MOSAIQCRC = Int(Seed And 65535)

    End Function


    Public Sub ReadCRCTable()

        Dim I As Integer

        'Die Checksummentabelle
        I = 0 : CRC_TAB(I) = &H0 : CRC_TAB(I + 1) = &HC0C1 : CRC_TAB(I + 2) = &HC181 : CRC_TAB(I + 3) = &H140 : CRC_TAB(I + 4) = &HC301 : CRC_TAB(I + 5) = &H3C0 : CRC_TAB(I + 6) = &H280 : CRC_TAB(I + 7) = &HC241
        I = 8 : CRC_TAB(I) = &HC601 : CRC_TAB(I + 1) = &H6C0 : CRC_TAB(I + 2) = &H780 : CRC_TAB(I + 3) = &HC741 : CRC_TAB(I + 4) = &H500 : CRC_TAB(I + 5) = &HC5C1 : CRC_TAB(I + 6) = &HC481 : CRC_TAB(I + 7) = &H440
        I = 16 : CRC_TAB(I) = &HCC01 : CRC_TAB(I + 1) = &HCC0 : CRC_TAB(I + 2) = &HD80 : CRC_TAB(I + 3) = &HCD41 : CRC_TAB(I + 4) = &HF00 : CRC_TAB(I + 5) = &HCFC1 : CRC_TAB(I + 6) = &HCE81 : CRC_TAB(I + 7) = &HE40
        I = 24 : CRC_TAB(I) = &HA00 : CRC_TAB(I + 1) = &HCAC1 : CRC_TAB(I + 2) = &HCB81 : CRC_TAB(I + 3) = &HB40 : CRC_TAB(I + 4) = &HC901 : CRC_TAB(I + 5) = &H9C0 : CRC_TAB(I + 6) = &H880 : CRC_TAB(I + 7) = &HC841
        I = 32 : CRC_TAB(I) = &HD801 : CRC_TAB(I + 1) = &H18C0 : CRC_TAB(I + 2) = &H1980 : CRC_TAB(I + 3) = &HD941 : CRC_TAB(I + 4) = &H1B00 : CRC_TAB(I + 5) = &HDBC1 : CRC_TAB(I + 6) = &HDA81 : CRC_TAB(I + 7) = &H1A40
        I = 40 : CRC_TAB(I) = &H1E00 : CRC_TAB(I + 1) = &HDEC1 : CRC_TAB(I + 2) = &HDF81 : CRC_TAB(I + 3) = &H1F40 : CRC_TAB(I + 4) = &HDD01 : CRC_TAB(I + 5) = &H1DC0 : CRC_TAB(I + 6) = &H1C80 : CRC_TAB(I + 7) = &HDC41
        I = 48 : CRC_TAB(I) = &H1400 : CRC_TAB(I + 1) = &HD4C1 : CRC_TAB(I + 2) = &HD581 : CRC_TAB(I + 3) = &H1540 : CRC_TAB(I + 4) = &HD701 : CRC_TAB(I + 5) = &H17C0 : CRC_TAB(I + 6) = &H1680 : CRC_TAB(I + 7) = &HD641
        I = 56 : CRC_TAB(I) = &HD201 : CRC_TAB(I + 1) = &H12C0 : CRC_TAB(I + 2) = &H1380 : CRC_TAB(I + 3) = &HD341 : CRC_TAB(I + 4) = &H1100 : CRC_TAB(I + 5) = &HD1C1 : CRC_TAB(I + 6) = &HD081 : CRC_TAB(I + 7) = &H1040
        I = 64 : CRC_TAB(I) = &HF001 : CRC_TAB(I + 1) = &H30C0 : CRC_TAB(I + 2) = &H3180 : CRC_TAB(I + 3) = &HF141 : CRC_TAB(I + 4) = &H3300 : CRC_TAB(I + 5) = &HF3C1 : CRC_TAB(I + 6) = &HF281 : CRC_TAB(I + 7) = &H3240
        I = 72 : CRC_TAB(I) = &H3600 : CRC_TAB(I + 1) = &HF6C1 : CRC_TAB(I + 2) = &HF781 : CRC_TAB(I + 3) = &H3740 : CRC_TAB(I + 4) = &HF501 : CRC_TAB(I + 5) = &H35C0 : CRC_TAB(I + 6) = &H3480 : CRC_TAB(I + 7) = &HF441
        I = 80 : CRC_TAB(I) = &H3C00 : CRC_TAB(I + 1) = &HFCC1 : CRC_TAB(I + 2) = &HFD81 : CRC_TAB(I + 3) = &H3D40 : CRC_TAB(I + 4) = &HFF01 : CRC_TAB(I + 5) = &H3FC0 : CRC_TAB(I + 6) = &H3E80 : CRC_TAB(I + 7) = &HFE41
        I = 88 : CRC_TAB(I) = &HFA01 : CRC_TAB(I + 1) = &H3AC0 : CRC_TAB(I + 2) = &H3B80 : CRC_TAB(I + 3) = &HFB41 : CRC_TAB(I + 4) = &H3900 : CRC_TAB(I + 5) = &HF9C1 : CRC_TAB(I + 6) = &HF881 : CRC_TAB(I + 7) = &H3840
        I = 96 : CRC_TAB(I) = &H2800 : CRC_TAB(I + 1) = &HE8C1 : CRC_TAB(I + 2) = &HE981 : CRC_TAB(I + 3) = &H2940 : CRC_TAB(I + 4) = &HEB01 : CRC_TAB(I + 5) = &H2BC0 : CRC_TAB(I + 6) = &H2A80 : CRC_TAB(I + 7) = &HEA41
        I = 104 : CRC_TAB(I) = &HEE01 : CRC_TAB(I + 1) = &H2EC0 : CRC_TAB(I + 2) = &H2F80 : CRC_TAB(I + 3) = &HEF41 : CRC_TAB(I + 4) = &H2D00 : CRC_TAB(I + 5) = &HEDC1 : CRC_TAB(I + 6) = &HEC81 : CRC_TAB(I + 7) = &H2C40
        I = 112 : CRC_TAB(I) = &HE401 : CRC_TAB(I + 1) = &H24C0 : CRC_TAB(I + 2) = &H2580 : CRC_TAB(I + 3) = &HE541 : CRC_TAB(I + 4) = &H2700 : CRC_TAB(I + 5) = &HE7C1 : CRC_TAB(I + 6) = &HE681 : CRC_TAB(I + 7) = &H2640
        I = 120 : CRC_TAB(I) = &H2200 : CRC_TAB(I + 1) = &HE2C1 : CRC_TAB(I + 2) = &HE381 : CRC_TAB(I + 3) = &H2340 : CRC_TAB(I + 4) = &HE101 : CRC_TAB(I + 5) = &H21C0 : CRC_TAB(I + 6) = &H2080 : CRC_TAB(I + 7) = &HE041
        I = 128 : CRC_TAB(I) = &HA001 : CRC_TAB(I + 1) = &H60C0 : CRC_TAB(I + 2) = &H6180 : CRC_TAB(I + 3) = &HA141 : CRC_TAB(I + 4) = &H6300 : CRC_TAB(I + 5) = &HA3C1 : CRC_TAB(I + 6) = &HA281 : CRC_TAB(I + 7) = &H6240
        I = 136 : CRC_TAB(I) = &H6600 : CRC_TAB(I + 1) = &HA6C1 : CRC_TAB(I + 2) = &HA781 : CRC_TAB(I + 3) = &H6740 : CRC_TAB(I + 4) = &HA501 : CRC_TAB(I + 5) = &H65C0 : CRC_TAB(I + 6) = &H6480 : CRC_TAB(I + 7) = &HA441
        I = 144 : CRC_TAB(I) = &H6C00 : CRC_TAB(I + 1) = &HACC1 : CRC_TAB(I + 2) = &HAD81 : CRC_TAB(I + 3) = &H6D40 : CRC_TAB(I + 4) = &HAF01 : CRC_TAB(I + 5) = &H6FC0 : CRC_TAB(I + 6) = &H6E80 : CRC_TAB(I + 7) = &HAE41
        I = 152 : CRC_TAB(I) = &HAA01 : CRC_TAB(I + 1) = &H6AC0 : CRC_TAB(I + 2) = &H6B80 : CRC_TAB(I + 3) = &HAB41 : CRC_TAB(I + 4) = &H6900 : CRC_TAB(I + 5) = &HA9C1 : CRC_TAB(I + 6) = &HA881 : CRC_TAB(I + 7) = &H6840
        I = 160 : CRC_TAB(I) = &H7800 : CRC_TAB(I + 1) = &HB8C1 : CRC_TAB(I + 2) = &HB981 : CRC_TAB(I + 3) = &H7940 : CRC_TAB(I + 4) = &HBB01 : CRC_TAB(I + 5) = &H7BC0 : CRC_TAB(I + 6) = &H7A80 : CRC_TAB(I + 7) = &HBA41
        I = 168 : CRC_TAB(I) = &HBE01 : CRC_TAB(I + 1) = &H7EC0 : CRC_TAB(I + 2) = &H7F80 : CRC_TAB(I + 3) = &HBF41 : CRC_TAB(I + 4) = &H7D00 : CRC_TAB(I + 5) = &HBDC1 : CRC_TAB(I + 6) = &HBC81 : CRC_TAB(I + 7) = &H7C40
        I = 176 : CRC_TAB(I) = &HB401 : CRC_TAB(I + 1) = &H74C0 : CRC_TAB(I + 2) = &H7580 : CRC_TAB(I + 3) = &HB541 : CRC_TAB(I + 4) = &H7700 : CRC_TAB(I + 5) = &HB7C1 : CRC_TAB(I + 6) = &HB681 : CRC_TAB(I + 7) = &H7640
        I = 184 : CRC_TAB(I) = &H7200 : CRC_TAB(I + 1) = &HB2C1 : CRC_TAB(I + 2) = &HB381 : CRC_TAB(I + 3) = &H7340 : CRC_TAB(I + 4) = &HB101 : CRC_TAB(I + 5) = &H71C0 : CRC_TAB(I + 6) = &H7080 : CRC_TAB(I + 7) = &HB041
        I = 192 : CRC_TAB(I) = &H5000 : CRC_TAB(I + 1) = &H90C1 : CRC_TAB(I + 2) = &H9181 : CRC_TAB(I + 3) = &H5140 : CRC_TAB(I + 4) = &H9301 : CRC_TAB(I + 5) = &H53C0 : CRC_TAB(I + 6) = &H5280 : CRC_TAB(I + 7) = &H9241
        I = 200 : CRC_TAB(I) = &H9601 : CRC_TAB(I + 1) = &H56C0 : CRC_TAB(I + 2) = &H5780 : CRC_TAB(I + 3) = &H9741 : CRC_TAB(I + 4) = &H5500 : CRC_TAB(I + 5) = &H95C1 : CRC_TAB(I + 6) = &H9481 : CRC_TAB(I + 7) = &H5440
        I = 208 : CRC_TAB(I) = &H9C01 : CRC_TAB(I + 1) = &H5CC0 : CRC_TAB(I + 2) = &H5D80 : CRC_TAB(I + 3) = &H9D41 : CRC_TAB(I + 4) = &H5F00 : CRC_TAB(I + 5) = &H9FC1 : CRC_TAB(I + 6) = &H9E81 : CRC_TAB(I + 7) = &H5E40
        I = 216 : CRC_TAB(I) = &H5A00 : CRC_TAB(I + 1) = &H9AC1 : CRC_TAB(I + 2) = &H9B81 : CRC_TAB(I + 3) = &H5B40 : CRC_TAB(I + 4) = &H9901 : CRC_TAB(I + 5) = &H59C0 : CRC_TAB(I + 6) = &H5880 : CRC_TAB(I + 7) = &H9841
        I = 224 : CRC_TAB(I) = &H8801 : CRC_TAB(I + 1) = &H48C0 : CRC_TAB(I + 2) = &H4980 : CRC_TAB(I + 3) = &H8941 : CRC_TAB(I + 4) = &H4B00 : CRC_TAB(I + 5) = &H8BC1 : CRC_TAB(I + 6) = &H8A81 : CRC_TAB(I + 7) = &H4A40
        I = 232 : CRC_TAB(I) = &H4E00 : CRC_TAB(I + 1) = &H8EC1 : CRC_TAB(I + 2) = &H8F81 : CRC_TAB(I + 3) = &H4F40 : CRC_TAB(I + 4) = &H8D01 : CRC_TAB(I + 5) = &H4DC0 : CRC_TAB(I + 6) = &H4C80 : CRC_TAB(I + 7) = &H8C41
        I = 240 : CRC_TAB(I) = &H4400 : CRC_TAB(I + 1) = &H84C1 : CRC_TAB(I + 2) = &H8581 : CRC_TAB(I + 3) = &H4540 : CRC_TAB(I + 4) = &H8701 : CRC_TAB(I + 5) = &H47C0 : CRC_TAB(I + 6) = &H4680 : CRC_TAB(I + 7) = &H8641
        I = 248 : CRC_TAB(I) = &H8201 : CRC_TAB(I + 1) = &H42C0 : CRC_TAB(I + 2) = &H4380 : CRC_TAB(I + 3) = &H8341 : CRC_TAB(I + 4) = &H4100 : CRC_TAB(I + 5) = &H81C1 : CRC_TAB(I + 6) = &H8081 : CRC_TAB(I + 7) = &H4040

    End Sub

End Class
