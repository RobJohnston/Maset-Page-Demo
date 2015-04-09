Imports System.Globalization
Imports System.Threading

Public Class Site
    Inherits System.Web.UI.MasterPage

#Region "Private Variables"
    Private _metaCreator As String = "Government of Canada, Citizenship and Immigration Canada, Communications Branch"
    Private _metaIssuedDate As String
    Private _metaSubject As String = "None"
    Private _pageCreationDate As String
    Private _pageModifiedDate As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetDateCreated()
        GetDateModified()

        'If the issue date hasn't been set on the page then default it to the page creation date.
        If _metaIssuedDate = vbNullString Then _
            _metaIssuedDate = _pageCreationDate

        'Replace the meta tags in the template.
        AddMetaTag("dcterms.language", Me.ThreeLetterLangName, "ISO639-2")
        AddMetaTag("dcterms.subject", _metaSubject, "scheme")
        AddMetaTag("dcterms.modified", _pageModifiedDate, "W3CDTF")
        AddMetaTag("dcterms.issued", _metaIssuedDate, "W3CDTF")
        AddMetaTag("dcterms.creator", _metaCreator)
        AddMetaTag("dcterms.title", Page.Title)


        'Adjust the link on the language switcher to point to the page in the opposite language.
        Dim switchLanguageUrl As String

        If Me.TwoLetterLangName.ToLower = "fr" Then
            switchLanguageUrl = Request.RawUrl.Replace("-fra.", "-eng.")
            HyperLinkHome2.NavigateUrl = "Home-Accueil-fra.aspx"
        Else
            switchLanguageUrl = Request.RawUrl.Replace("-eng.", "-fra.")
            HyperLinkHome2.NavigateUrl = "Home-Accueil-eng.aspx"
        End If

        HyperLinkLanguage.NavigateUrl = switchLanguageUrl
    End Sub

#Region "Private Methods"

    ''' <summary>
    ''' Add meta tags to the head.
    ''' </summary>
    ''' <param name="name">The value for the name attribute of the meta tag.</param>
    ''' <param name="content">The value for the content attribute of the meta tag.</param>
    ''' <param name="title">The value for the title attribute of the meta tag</param>
    ''' <remarks>
    ''' If the master page is edited, the position where these tags are added 
    ''' may need to be adjusted, but adding the tags in this manner avoids 
    ''' having to set an ID parameter on the meta tags (not proper HTML5).
    ''' </remarks>
    Private Sub AddMetaTag(name As String, content As String, Optional title As String = vbNullString)
        Dim metaTag As New HtmlControls.HtmlMeta

        metaTag.Name = name
        metaTag.Content = content
        metaTag.Attributes.Add("title", title)

        Page.Header.Controls.RemoveAt(11)
        Page.Header.Controls.AddAt(6, metaTag)
    End Sub

    ''' <summary>
    ''' Get the creation date of the "slave" page (not this master page).
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDateCreated()
        If _pageCreationDate = String.Empty Then
            Dim objInfo As New System.IO.FileInfo(Server.MapPath(Request.ServerVariables.Get("SCRIPT_NAME")))
            Dim strDate As String

            strDate = Format(objInfo.CreationTime.Date, "yyyy-MM-dd")
            _pageCreationDate = strDate
        End If
    End Sub

    ''' <summary>
    ''' Get the modification date of the "slave" page (not this master page).
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDateModified()
        If _pageModifiedDate = String.Empty Then
            Dim objInfo As New System.IO.FileInfo(Server.MapPath(Request.ServerVariables.Get("SCRIPT_NAME")))
            Dim strDate As String

            strDate = Format(objInfo.LastWriteTime.Date, "yyyy-MM-dd")
            _pageModifiedDate = strDate
        End If
    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Get or set the content attribute of the dcterms.creator meta tag.
    ''' </summary>
    Public Property MetaCreator() As String
        Get
            Return _metaCreator
        End Get
        Set(value As String)
            _metaCreator = value
        End Set
    End Property

    ''' <summary>
    ''' Get or set the content attribute of the dcterms.issued meta tag.
    ''' </summary>
    ''' <remarks>Defaults to the creation time of the file on disk.</remarks>
    Public Property MetaIssued() As String
        Get
            Return _metaIssuedDate
        End Get
        Set(value As String)
            _metaIssuedDate = value
        End Set
    End Property

    ''' <summary>
    ''' Get or set the content attribute of the dcterms.subject meta tag.
    ''' </summary>
    Public Property MetaSubject() As String
        Get
            Return _metaSubject
        End Get
        Set(ByVal value As String)
            _metaSubject = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the date that the page was modified.
    ''' </summary>
    ''' <remarks>Defaults to the modification time of the file on disk.</remarks>
    Public Property PageModified As String
        Get
            Return _pageModifiedDate
        End Get
        Set(value As String)
            _pageModifiedDate = value

        End Set
    End Property

    ''' <summary>
    ''' Gets the ISO 639-2 three-letter code for the language of the current CultureInfo.
    ''' </summary>
    Public ReadOnly Property ThreeLetterLangName As String
        Get
            Return Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName
            'Return CultureInfo.CurrentCulture.ThreeLetterISOLanguageName
        End Get
    End Property

    ''' <summary>
    ''' Gets the ISO 639-1 two-letter code for the language of the current CultureInfo.
    ''' </summary>
    Public ReadOnly Property TwoLetterLangName As String
        Get
            Return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
            'Return CultureInfo.CurrentCulture.TwoLetterISOLanguageName
        End Get
    End Property
#End Region

End Class