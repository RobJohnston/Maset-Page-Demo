Public Class Home
    Inherits BasePage

    Private _lang As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _lang = Threading.Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName

        'NOTE:  These such things could easily be put into resource files.
        If _lang = "fra" Then
            Page.Title = "[FR] My page title"
            Page.MetaDescription = "[FR] My page description"
        Else
            Page.Title = "[EN] My page title"
            Page.MetaDescription = "[EN] My page description"
        End If
    End Sub

End Class