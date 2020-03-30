Imports System.DirectoryServices.AccountManagement
Imports System.Security.Principal
Imports Microsoft.Win32

Public Class K8_UC11winpassword

    Dim ret As New List(Of String)
    Dim myDomain As System.DirectoryServices.ActiveDirectory.Domain
    Dim myDomainName As String


    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        'Call VerifyPassword()
        GetUsers()
        CMBBX_WindowsUsers.ItemsSource = ret

        Try
            myDomain = System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain()
            myDomainName = myDomain.Name
        Catch ex As Exception
            myDomainName = "NO domain!"
        End Try
        LBL_Domain.Content = myDomainName

    End Sub

    Function GetUsers() As List(Of String)

        Dim userskey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList")
        For Each keyname As String In userskey.GetSubKeyNames()
            Using key As RegistryKey = userskey.OpenSubKey(keyname)
                Dim userpath As String = DirectCast(key.GetValue("ProfileImagePath"), String)
                Dim username As String = System.IO.Path.GetFileNameWithoutExtension(userpath)
                'Console.WriteLine("{0}", username)
                ret.Add(username)
            End Using
        Next
        If Not ret.Contains("Guest") Then ret.Add("Guest")
        ret.Sort()

        Return ret

    End Function

    Private Sub VerifyPassword()

        'https://stackoverflow.com/questions/34023992/find-group-membership-for-user-in-vb-net

        Dim MyPrincipalContext As PrincipalContext

        If myDomain Is Nothing Then
            MyPrincipalContext = New PrincipalContext(ContextType.Machine)
        Else
            MyPrincipalContext = New PrincipalContext(ContextType.Domain, myDomainName)
        End If

        Dim IsPasswordValid As Boolean
        IsPasswordValid = MyPrincipalContext.ValidateCredentials(CMBBX_WindowsUsers.Text, Me.PSSWRDBX_Password.Password)

        Dim userFullName As String = UserPrincipal.FindByIdentity(MyPrincipalContext, CMBBX_WindowsUsers.Text).DisplayName
        Dim userGroups As PrincipalSearchResult(Of Principal) = UserPrincipal.FindByIdentity(MyPrincipalContext, CMBBX_WindowsUsers.Text).GetGroups

        LBL_Password2.Content = "Principal Context Name: " & MyPrincipalContext.Name
        LBL_Password3.Content = "Principal Context Username: " & MyPrincipalContext.UserName
        LBL_Password4.Content = "Is administrator? " & IsAdministrator()
        LBL_Password5.Content = "User Full Name: " & userFullName

        If IsPasswordValid Then
            '--- Ok

            LBL_Password1.Content = "Password Validity: valid!"

            TXTBLCK_Gruppen.Text = ""
            For Each Ergebnis In userGroups
                TXTBLCK_Gruppen.Text &= Ergebnis.Name & " - " & Ergebnis.DisplayName & " - " & Ergebnis.Description & vbCrLf
            Next
        Else
            '--- Nicht Ok
            LBL_Password1.Content = "Password Validity: not valid!"

        End If



        'Using MyPrincipalContext As PrincipalContext = New PrincipalContext(ContextType.Machine) ', "Domäne")

        '    Dim IsPasswordValid As Boolean
        '    IsPasswordValid = MyPrincipalContext.ValidateCredentials(CMBBX_WindowsUsers.Text, Me.TXTBX_Password.Text)

        '    If IsPasswordValid Then
        '        '--- Ok

        '        'Dim userFullName As String = UserPrincipal.FindByIdentity(MyPrincipalContext, "rayv2").DisplayName
        '        Dim userFullName As String = UserPrincipal.FindByIdentity(MyPrincipalContext, CMBBX_WindowsUsers.Text).DisplayName
        '        Dim userGroups As PrincipalSearchResult(Of Principal) = UserPrincipal.FindByIdentity(MyPrincipalContext, CMBBX_WindowsUsers.Text).GetGroups

        '        LBL_Password.Content = "Password Validity: valid! - " & MyPrincipalContext.Name & " - " & MyPrincipalContext.UserName & " - " & IsAdministrator() & " - " & userFullName

        '        TXTBLCK_Gruppen.Text = ""
        '        For Each Ergebnis In userGroups
        '            TXTBLCK_Gruppen.Text &= Ergebnis.Name & " - " & Ergebnis.DisplayName & vbCrLf
        '        Next
        '    Else
        '        '--- Nicht Ok
        '        LBL_Password.Content = "Password Validity: not valid!"

        '    End If

        'End Using

    End Sub

    Public Shared Function IsAdministrator() As Boolean
        Dim isAdmin As Boolean = False
        Try
            Dim user As IIdentity = WindowsIdentity.GetCurrent()
            Dim principal As New WindowsPrincipal(CType(user, WindowsIdentity))
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator)
            isAdmin = principal.IsInRole(WindowsBuiltInRole.PowerUser)
            Return isAdmin
        Catch ex As Exception
            Return isAdmin
        End Try
        'Try
        '    Dim user As IIdentity = WindowsIdentity.GetCurrent()
        '    Dim principal As New WindowsPrincipal(CType(user, WindowsIdentity))
        '    isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator)
        '    Return isAdmin
        'Catch ex As Exception
        '    Return isAdmin
        'End Try




    End Function



End Class
