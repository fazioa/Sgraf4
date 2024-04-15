Public Class AboutBoxWindow
    Private Sub Window_Activated(sender As Object, e As EventArgs)
        labelVersion.Text = "Versione " + My.Application.Info.Version.ToString
    End Sub
End Class
