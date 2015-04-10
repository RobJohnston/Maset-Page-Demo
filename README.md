# Master-Page-Demo
A VB.Net WebForms project showing one approach to using master pages with wet-boew.

This is using the 
[Government of Canada (GC) Intranet theme for the Web Experience Toolkit](https://github.com/wet-boew/theme-gc-intranet).

The start page of the project should be set to home-accueil-eng.aspx.  The urlMappings section of the web.config file will 
translate this to Home.aspx.  One should then be able to cycle between English and French by using the language toggle on the top-right.

Any new page should inherit from BasePage instead of System.Web.UI.Page.

Another option is to use multiple language-specific master pages instead of a single master page with resource files.  In this case, a BaseMaster.vb file should be created in the Classes folder to hold common functionality and each master page would then inherit from BaseMaster instead of MasterPage.  The following function would be placed in BasePage.vb:

```vb
    ''' <summary>
    ''' Load a specific master page based on the language of the current culture.
    ''' </summary>
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreInit

        Select Case Threading.Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName
            Case "fra"
                Me.MasterPageFile = "MasterPage-fra.Master"
            Case Else
                Me.MasterPageFile = "MasterPage-eng.Master"
        End Select
    End Sub
```
