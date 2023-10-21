Imports log4net
Imports Microsoft.VisualBasic.Logging


Public Class WindowSetup
    Private Shared ReadOnly log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)


    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Me.Title = System.Reflection.Assembly.GetExecutingAssembly.GetName().Name
        Me.Title = Me.Title & " - Setup"
        log.Info("Start " & Me.Title)
    End Sub

    Private Sub tb_intestazione1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_intestazione1.TextChanged
        My.Settings.intestazione1 = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_intestazione2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_intestazione2.TextChanged
        My.Settings.intestazione2 = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_intestazione3_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_intestazione3.TextChanged
        My.Settings.intestazione3 = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_autorefascicolo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_autorefascicolo.TextChanged
        My.Settings.autore = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_gruppofirmariga1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_gruppofirmariga1.TextChanged
        My.Settings.gruppoFirmaIRiga = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_gruppofirmariga2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_gruppofirmariga2.TextChanged
        My.Settings.gradoCognomeNome = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_luogo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_luogo.TextChanged
        My.Settings.luogo = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_titoloimmagine_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_titoloimmagine.TextChanged
        My.Settings.titoloFoto = sender.text
        My.Settings.Save()
    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)
        log.Info("Closed " & Me.Title)
    End Sub
End Class
