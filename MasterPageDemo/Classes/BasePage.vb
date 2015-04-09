Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading

''' <summary>
''' This class will be inherited by all ASPX pages. This provides us a "hook" 
''' to apply global functionality from a central location.
''' </summary>
''' <remarks></remarks>
Public Class BasePage
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Determine the culture based on the page name.
    ''' </summary>
    ''' <remarks>
    ''' It is assumed that the pages are named like "somepage-eng.aspx".
    ''' </remarks>
    Protected Overrides Sub InitializeCulture()
        Dim userCulture As String
        Dim pattern As String = "^\S+-(\S+).aspx\S*$"
        Dim m As Match

        m = Regex.Match(Request.RawUrl, pattern, RegexOptions.IgnoreCase)

        'Given an arbitrary page such as "/SomeDir/SomePage-fra.aspx?id=123",
        'the above pattern should find 1 group: "fra".

        If m.Success Then
            Select Case m.Groups.Item(1).Value
                Case "fra"
                    userCulture = "fr-ca"
                Case Else
                    userCulture = "en-ca"
            End Select

            Thread.CurrentThread.CurrentUICulture = New CultureInfo(userCulture)
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(userCulture)
        End If
    End Sub

End Class

