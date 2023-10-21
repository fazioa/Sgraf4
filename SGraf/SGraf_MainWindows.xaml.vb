

Imports log4net

Class MainWindow
    Private Shared ReadOnly log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)



    Private Sub tb_oggetto_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_oggetto.TextChanged
        My.Settings.oggetto = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_contenuto_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_contenuto.TextChanged
        My.Settings.contenutoDettaglio = sender.text
        My.Settings.Save()
    End Sub


    Private Sub sl_colonne_LostFocus(sender As Object, e As RoutedEventArgs) Handles sl_colonne.LostFocus
        My.Settings.disposizioneColonne = sender.Value
        My.Settings.Save()
    End Sub

    Private Sub sl_righe_LostFocus(sender As Object, e As RoutedEventArgs) Handles sl_righe.LostFocus
        My.Settings.disposizioneRighe = sender.Value
        My.Settings.Save()
    End Sub

    Private Sub cb_tipofascicolo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cb_tipofascicolo.SelectionChanged
        My.Settings.tipoFascicolo = sender.SelectedIndex
        My.Settings.Save()
    End Sub

    Private Sub MenuSetupItem_Click(sender As Object, e As RoutedEventArgs)

        Dim w As WindowSetup = New WindowSetup()
        w.Show()

    End Sub

    Private Sub window_Loaded(sender As Object, e As RoutedEventArgs) Handles window.Loaded
        Me.Title = System.Reflection.Assembly.GetExecutingAssembly.GetName().Name


        ' Get the Launch mode
        Dim isDevelopment As Boolean = String.Equals(Environment.GetEnvironmentVariable("DOTNET_MODIFIABLE_ASSEMBLIES"), "debug",
                                                     StringComparison.InvariantCultureIgnoreCase)

        log4net.Config.XmlConfigurator.Configure()


        log.Info("Start")


        Dim img As Image = New Image()
        Dim sPath As String = AppDomain.CurrentDomain.BaseDirectory & "test"




        For a = 0 To 100
            Dim usr As UserControlImg = New UserControlImg(img, sPath & "\dsc_0181.jpg", 100, 100)
            WrapPanelImmagini.Children.Add(usr)
        Next






    End Sub
End Class

