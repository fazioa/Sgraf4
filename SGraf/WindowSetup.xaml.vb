Imports System.Drawing
Imports log4net
Imports Microsoft.VisualBasic.Logging


Public Class WindowSetup
    Private Shared ReadOnly log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Private Shared feAction As New ActionLibrary

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Me.Title = System.Reflection.Assembly.GetExecutingAssembly.GetName().Name
        Me.Title = Me.Title & " - Setup"
        log.Info("Start " & Me.Title)

        FillFontComboBox(ComboBoxFonts)
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

    Private Sub tb_carattereTitolo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_carattereTitolo.TextChanged
        'verifica che venga inserito un numero valido
        Dim iValue As Integer = feAction.checkNumber(sender.text)
        sender.text = iValue
        My.Settings.carattereDimensioneTitoloImmagine = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_carattereNomeImmagine_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_carattereNomeImmagine.TextChanged
        'verifica che venga inserito un numero valido
        Dim iValue As Integer = feAction.checkNumber(sender.text)
        sender.text = iValue
        My.Settings.carattereDimensioneDidascalia = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_carattereNomeFile_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_carattereNomeFile.TextChanged
        'verifica che venga inserito un numero valido
        Dim iValue As Integer = feAction.checkNumber(sender.text)
        sender.text = iValue
        My.Settings.carattereDimensioneNomeFile = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_carattereDatiEXIF_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_carattereDatiEXIF.TextChanged
        'verifica che venga inserito un numero valido
        Dim iValue As Integer = feAction.checkNumber(sender.text)
        sender.text = iValue
        My.Settings.carattereDimensioneDatiEXIF = sender.text
        My.Settings.Save()
    End Sub

    Public Sub FillFontComboBox(comboBoxFonts As ComboBox)

        ' Return the family typeface collection for the font family.
        Dim familyTypefaceCollection As FamilyTypefaceCollection = FontFamily.FamilyTypefaces

        For Each element In Fonts.SystemFontFamilies
            comboBoxFonts.Items.Add(element.ToString)
        Next

        'se il valore My.Settings.carattereFont è un numero allora vuol dire che si tratta del primo avvio, quindi imposta il carattere di default
        If IsNumeric(My.Settings.carattereFont) Then
            Try
                log.Info("Imposta il font di default")
                Dim defaultFont = New FontFamily("Times New Roman")
                comboBoxFonts.SelectedValue = defaultFont.Name

            Catch ex As Exception
                log.Error("Errore impostazione font di default")
            End Try

        End If


        comboBoxFonts.SelectedValue = My.Settings.carattereFont
    End Sub

    Private Sub ComboBoxFonts_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ComboBoxFonts.SelectionChanged
        Dim s As ComboBox = sender
        My.Settings.carattereFont = s.SelectedValue
        My.Settings.Save()
    End Sub


End Class
