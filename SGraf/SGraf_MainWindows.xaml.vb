
Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports System.Web.UI.WebControls
Imports log4net
Imports Microsoft.Win32
Imports Newtonsoft.Json
Imports Xceed.Document.NET
Imports Xceed.Words.NET
Imports Xceed.Wpf.Toolkit

Class MainWindow
    Private Shared ReadOnly log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Private Shared feAction As New ActionLibrary

    Property isBusy As Boolean

    Private Sub tb_oggetto_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_oggetto.TextChanged
        My.Settings.oggetto = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_contenuto_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_contenuto.TextChanged
        My.Settings.contenutoDettaglio = sender.text
        My.Settings.Save()
    End Sub


    Private Sub cb_tipofascicolo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cb_tipofascicolo.SelectionChanged
        My.Settings.tipoFascicolo = sender.SelectedIndex
        My.Settings.Save()
    End Sub

    Private Sub MenuSetupItem_Click(sender As Object, e As RoutedEventArgs)
        Dim w As New WindowSetup(Me)
        w.Show()
    End Sub

    Private Sub AboutBoxWindow_Click(sender As Object, e As RoutedEventArgs)
        Dim w As New AboutBoxWindow()
        w.Show()
    End Sub

    Private Sub window_Loaded(sender As Object, e As RoutedEventArgs) Handles window.Loaded
        Me.Title = System.Reflection.Assembly.GetExecutingAssembly.GetName().Name

        ' Get the Launch mode
        Dim isDevelopment As Boolean = String.Equals(Environment.GetEnvironmentVariable("DOTNET_MODIFIABLE_ASSEMBLIES"), "debug",
                                                     StringComparison.InvariantCultureIgnoreCase)

        log4net.Config.XmlConfigurator.Configure()

        log.Info("Start")

        '   Dim sPath As String = AppDomain.CurrentDomain.BaseDirectory & "test"


    End Sub


    Dim dragtype As Type
    Private Sub WrapPanelImmagini_Drop(sender As Object, e As DragEventArgs) Handles WrapPanelImmagini.Drop
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then

            ' Note that you can have more than one file.
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            PopolaImmagini(files)
        End If
    End Sub


    Private Sub ScrollViewer_Drop(sender As Object, e As DragEventArgs)
        WrapPanelImmagini_Drop(sender, e)
    End Sub

    Private Async Sub PopolaImmagini(arrayFileNames As String())
        Dim i As Integer = 0
        Dim sFile As String
        Dim imageItem As UserControlImg

        ''faccio partire la misura del tempo
        Dim sw As Stopwatch = Stopwatch.StartNew()

        Dim iCount = arrayFileNames.Count
        Dim iRate As Double = 100 / iCount
        ptest.Value = 0
        ' Visualizza la ProgressBar :
        ptest.Visibility = Visibility.Visible

        log.Info("Start - Caricamento immagini")

        For Each sFile In arrayFileNames
            Try
                log.Info("Inserimento immagine " & sFile)
                'BitmapImage è la miniatura
                Dim b_image As BitmapImage = New BitmapImage()
                b_image.BeginInit()
                b_image.UriSource = New Uri(sFile, UriKind.RelativeOrAbsolute)

                ' la risoluzione della mininiatura può essere impostata nella configurazione
                b_image.DecodePixelHeight = My.Settings.thumbnailDisplayResolution

                b_image.EndInit()

                'la dimensione dell'oggetto viene impostata in funzione dell'altezza 
                imageItem = New UserControlImg(b_image, sFile, My.Settings.fotoLarghezzaThumb)
                WrapPanelImmagini.Children.Add(imageItem)
                ptest.Value += iRate
                ' Attendi un breve intervallo per consentire all'interfaccia utente di aggiornarsi:
                Await Task.Delay(1)

            Catch ex As Exception
                log.Error("Inserimento immagine fallito - " & ex.Message)
            End Try


        Next
        ' Nasconde la ProgressBar :
        ptest.Visibility = Visibility.Hidden


        For Each child As UserControlImg In WrapPanelImmagini.Children
            RemoveHandler child.MouseDown, AddressOf childs_MouseDown
            AddHandler child.MouseDown, AddressOf childs_MouseDown

            RemoveHandler child.DragOver, AddressOf childs_DragOver
            AddHandler child.DragOver, AddressOf childs_DragOver

            RemoveHandler child.Drop, AddressOf childs_Drop
            AddHandler child.Drop, AddressOf childs_Drop

            child.LabelNumeroFoto.Content = WrapPanelImmagini.Children.IndexOf(child).ToString + 1

        Next

        'fermo la misura del tempo
        sw.Stop()
        'visualizza il tempo di esecuzione
        log.Info("End - Caricamento immagini - Tempo " & sw.Elapsed.ToString)
        Console.WriteLine($"Elapsed: {sw.Elapsed}")
    End Sub

    Private Sub childs_MouseUp(sender As Object, e As MouseButtonEventArgs)
        Console.WriteLine("Mouse up")

    End Sub

    Private Sub childs_Drop(sender As Object, e As DragEventArgs)
        Dim final As UserControlImg = sender
        Dim wpanel As WrapPanel = final.Parent
        'estrare il numero di posizione finale
        i_final = wpanel.Children.IndexOf(final)

        'se l'indice iniziale e quello finale coincidono dopo il drag&drop allora si tratta di un click, quindi seleziono/deseleziono l'immagine
        If i_final = i_source Then
            final.toggleSelected()
        End If

        'rinumera
        log.Info("Aggiorna la numerazione delle immagini")
        renumber(wpanel)
    End Sub

    Dim objPrecFinal As UserControlImg = Nothing
    Private Sub childs_DragOver(sender As Object, e As DragEventArgs)
        'sposta visivamente l'immagine durante il drag
        If (e.Data.GetDataPresent(dragtype)) Then
            Dim source As UserControlImg = CType(e.Data.GetData(dragtype), UserControlImg)

            Dim final As UserControlImg = sender

            Dim wpanel As WrapPanel = source.Parent

            Dim i_source = wpanel.Children.IndexOf(source)

            'estrare il numero di posizione finale
            Dim i_final = wpanel.Children.IndexOf(final)
            'salva l'indice dell'elemento di destinazione finale precedente
            Dim i_PrecFinal = wpanel.Children.IndexOf(objPrecFinal)

            If Not IsNothing(objPrecFinal) Then
                If (i_PrecFinal <> i_final) Then
                    'inserisce l'elelento nella nuova posizione
                    wpanel.Children.Remove(source)
                    wpanel.Children.Insert(i_final, source)
                End If
            End If
            'aggiorna l'elemento di destinazione finale precedente
            objPrecFinal = final
        End If

    End Sub


    Private Sub renumber(wpanel As WrapPanel)
        log.Info("Assegnazione numeri immagini")
        For Each child As UserControlImg In wpanel.Children
            child.LabelNumeroFoto.Content = wpanel.Children.IndexOf(child) + 1
        Next
    End Sub

    Public Sub rewriteEXIF_allImages()
        log.Info("Aggiornamento dati EXIF")
        For Each child As UserControlImg In WrapPanelImmagini.Children
            child.update_label_from_EXIF()
        Next

    End Sub

    Dim i_source, i_final

    Private Sub childs_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs)

        If e.LeftButton = MouseButtonState.Pressed Then
            Me.dragtype = sender.GetType
            'Package the data.
            Dim Data As DataObject = New DataObject()


            'TO DO
            'spostamento di più immagini in un colpo solo.


            'Dim index As Integer
            'Dim usrCtrl As UserControlImg
            'For index = WrapPanelImmagini.Children.Count - 1 To 0 Step -1
            '    usrCtrl = WrapPanelImmagini.Children.Item(index)

            '    If usrCtrl.isSelected Then
            '        Data.SetData(usrCtrl)
            '    End If
            'Next

            Dim source As UserControlImg = sender

            Dim wpanel As WrapPanel = source.Parent

            'estrae e salva il numero di posizione finale. Servirà al momento del drop, per capire  se si tratta di un click oppure di un trascinamento.
            i_source = wpanel.Children.IndexOf(source)

            Data.SetData(sender)
            ' Initiate the drag-And-drop operation.
            log.Info("Avvia spostamento immagine")
            DragDrop.DoDragDrop(Me, Data, DragDropEffects.Move)
        End If
    End Sub

    Private Sub MenuItem_Click(sender As Object, e As RoutedEventArgs)
        Dim o As OpenFileDialog = New OpenFileDialog
        o.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.bmp|All files (*.*)|*.*"
        o.Multiselect = True
        log.Info("Finestra Apri file")
        Dim result = o.ShowDialog()
        PopolaImmagini(o.FileNames)
    End Sub



    Private Sub window_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles window.PreviewKeyDown
        If e.Key = Key.Delete Then
            log.Info("Cancellazione")
            Dim index As Integer
            Dim usrCtrl As UserControlImg

            For index = WrapPanelImmagini.Children.Count - 1 To 0 Step -1
                usrCtrl = WrapPanelImmagini.Children.Item(index)
                If usrCtrl.isSelected Then
                    log.Info("Cancellazione " & index)
                    WrapPanelImmagini.Children.Remove(usrCtrl)
                End If
            Next
            renumber(WrapPanelImmagini)
        End If
    End Sub

    Private Sub RotateDx_Click(sender As Object, e As RoutedEventArgs)
        log.Info("Rotazione selezionate Dx ")
        Dim index As Integer
        Dim usrCtrl As UserControlImg
        For index = WrapPanelImmagini.Children.Count - 1 To 0 Step -1
            usrCtrl = WrapPanelImmagini.Children.Item(index)
            If usrCtrl.isSelected Then
                log.Info("Ruota Dx " & index)
                usrCtrl.rotateDx()
            End If
        Next
    End Sub

    Private Sub RotateSx_Click(sender As Object, e As RoutedEventArgs)
        log.Info("Rotazione selezionate Dx ")
        Dim index As Integer
        Dim usrCtrl As UserControlImg
        For index = WrapPanelImmagini.Children.Count - 1 To 0 Step -1
            usrCtrl = WrapPanelImmagini.Children.Item(index)
            If usrCtrl.isSelected Then
                log.Info("Ruota Sx " & index)
                usrCtrl.rotateSx()
            End If
        Next
    End Sub

    Private Sub Genera_fascicolo_Click(sender As Object, e As RoutedEventArgs)

        Dim sPath As String = AppDomain.CurrentDomain.BaseDirectory & My.MySettings.Default.doc_modelPath & "\"
        Dim sResultPath As String = AppDomain.CurrentDomain.BaseDirectory & My.MySettings.Default.doc_resultPath & "\"
        Dim sFilename As String = sPath & My.MySettings.Default.doc_nomeModello

        log.Info("Generazione fascicolo")
        Dim a = "Intestazione1 "
        Dim b = "nuovo testo"
        Dim document As DocX

        Try
            log.Info("Apertura modello ")
            document = DocX.Load(sFilename)

        Catch ex As Exception
            log.Info("Errore apertura modello " & sFilename)
        End Try

        If document IsNot Nothing Then
            log.Info("Compilazione")
            'passa alla funzione il contenitore delle immagini ed anche un riferimento alla progress bar
            feAction.wordInizializzaEcompila(document, WrapPanelImmagini, ptest)

            'rimuove il contenuto della cartella dei risultati
            Try
                log.Info("Pulizia cartella " & sResultPath)
                Dim di As DirectoryInfo = New DirectoryInfo(sResultPath)
                For Each f In di.GetFiles
                    f.Delete()
                Next
            Catch ex As Exception
                log.Info("Errore " & ex.Message)
            End Try

            'salva il nuovo documento
            log.Info("Salva documento " & sResultPath & ActionLibrary.getTimeStamp() & " " & My.Settings.doc_savedDocumentName)
            Dim sDocPath As String = sResultPath & ActionLibrary.getTimeStamp() & " " & My.Settings.doc_savedDocumentName
            document.SaveAs(sDocPath)

            'apre il nuovo documento
            Try
                log.Info("Apertura documento " & sDocPath)
                Process.Start(sDocPath)

            Catch ex As Exception
                log.Error("Errore" & ex.Message)
            End Try

        End If

    End Sub

    Private Sub tb_altezzaimmagine_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_altezzaimmagine.TextChanged
        'verifica che venga inserito un numero valido
        Dim iValue As Integer = feAction.checkNumber(sender.text)
        sender.text = iValue
        My.Settings.fotoAltezzaCM = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_larghezzaimmagine_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_larghezzaimmagine.TextChanged
        'verifica che venga inserito un numero valido
        Dim iValue As Integer = feAction.checkNumber(sender.text)
        sender.text = iValue
        My.Settings.fotoLarghezzaCM = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_colonne_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_colonne.TextChanged
        'verifica che venga inserito un numero valido
        Dim iValue As Integer = feAction.checkNumber(sender.text)
        sender.text = iValue
        My.Settings.disposizioneColonne = sender.text
        My.Settings.Save()
    End Sub

    Private Sub tb_righe_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tb_righe.TextChanged
        'verifica che venga inserito un numero valido
        Dim iValue As Integer = feAction.checkNumber(sender.text)
        sender.text = iValue
        My.Settings.disposizioneRighe = sender.text
        My.Settings.Save()
    End Sub

    Private Sub menu_svuota_Click(sender As Object, e As RoutedEventArgs) Handles menu_svuota.Click
        WrapPanelImmagini.Children.Clear()
    End Sub

    Dim ZoomValue As Integer = My.Settings.zoomMiniaturaDefaultMouse
    Dim CtrlIsDown As Boolean

    'modifica dimensione miniature (solo quelle mostrate a video)
    Dim intNuovaAltezzaThumb As Integer
    Dim intNuovaLarghezzaThumb As Integer
    Private Sub imgRedrawZoom(zoomPercent As Double)
        intNuovaLarghezzaThumb = CInt(My.Settings.fotoLarghezzaThumb * (1 + zoomPercent / 100))
        Dim ratio As Double = 0
        If (intNuovaLarghezzaThumb > My.Settings.fotoLarghezzaThumbMin) Then
            'se le nuove dimensioni rispettano i requisiti allora vengono salvate come predefinite
            If (My.Settings.fotoLarghezzaThumb <> intNuovaLarghezzaThumb) Then
                My.Settings.fotoLarghezzaThumb = intNuovaLarghezzaThumb
                My.Settings.Save()
            End If
            If Not IsNothing(WrapPanelImmagini) Then
                For Each child As UserControlImg In WrapPanelImmagini.Children
                    '   child.Height = CInt(child.Height * (1 + zoomPercent / 100))
                    child.Width = CInt(child.Width * (1 + zoomPercent / 100))

                Next
            End If
        End If
    End Sub

    'procedure temporanee

    Private Sub imgRedraw()
        Dim ratio As Double = 0
        If Not IsNothing(WrapPanelImmagini) Then
            For Each child As UserControlImg In WrapPanelImmagini.Children
                '   child.Height = My.Settings.fotoAltezzaThumb
                child.Width = My.Settings.fotoLarghezzaThumb
            Next
        End If
    End Sub

    Private Sub menu_salvaprogetto_Click(sender As Object, e As RoutedEventArgs) Handles menu_salvaprogetto.Click
        log.Info("Salvataggio progetto")
        Dim progetto As New ImagesProjectClass

        'leggo le preferenze
        progetto.sOggetto = My.Settings.oggetto
        progetto.sTitoloFoto = My.Settings.titoloFoto
        progetto.sDettagliocontenuto = My.Settings.contenutoDettaglio
        progetto.sTipoFascicolo = My.Settings.tipoFascicolo

        progetto.iColonne = My.Settings.disposizioneColonne
        progetto.iRighe = My.Settings.disposizioneRighe

        progetto.iImmagineAltezzaCM = My.Settings.fotoAltezzaCM
        progetto.iImmagineLarghezzaCM = My.Settings.fotoLarghezzaCM

        progetto.iDimensioneNomeFile = My.Settings.carattereDimensioneNomeFile
        progetto.iDimensioneDidascalia = My.Settings.carattereDimensioneDidascalia
        progetto.iDimensioneTitolo = My.Settings.carattereDimensioneTitoloImmagine
        progetto.iDimensioneDatiExif = My.Settings.carattereDimensioneDatiEXIF

        progetto.sFileNames = New List(Of String)()
        For Each child As UserControlImg In WrapPanelImmagini.Children
            progetto.sFileNames.Add(child.sNomeFile)
        Next

        'serializza
        Dim jsonTxt As String = JsonConvert.SerializeObject(progetto, Newtonsoft.Json.Formatting.Indented)

        ' Configure save file dialog box
        Dim dialog As New Microsoft.Win32.SaveFileDialog()
        dialog.FileName = "project" ' Default file name
        dialog.DefaultExt = ".sgraf" ' Default file extension
        dialog.Filter = "Text documents (.sgraf)|*.sgraf" ' Filter files by extension

        ' Show save file dialog box
        Dim result As Boolean? = dialog.ShowDialog()

        ' Process save file dialog box results
        If result = True Then
            ' Save document
            Dim filename As String = dialog.FileName
            'salva file testo
            feAction.salvaTxtFile(filename, jsonTxt)
            log.Info("Progetto Salvato")
        End If

    End Sub



    Private Sub WrapPanelImmagini_MouseWheel(sender As Object, e As MouseWheelEventArgs) Handles WrapPanelImmagini.MouseWheel
        'gestione mouse scroll - rotella mouse per zoom miniature

        'check if control is being held down
        If My.Computer.Keyboard.CtrlKeyDown Then
            'disabilita momentaneamente lo scroll
            e.Handled = True
            scrollWrapPanel.IsEnabled = False

            CtrlIsDown = True
            'evaluate the delta's sign and call the appropriate zoom command
            Select Case Math.Sign(e.Delta)
                Case Is < 0
                    ZoomValue = -2
                Case Is > 0
                    ZoomValue = 2
            End Select
            'ridimensiona i controlli immagine. Lo scorrimento automatico viene momentaneamente disabilitato
            log.Info("Zoom " & ZoomValue & "%")
            imgRedrawZoom(ZoomValue)

            scrollWrapPanel.IsEnabled = True
        End If
        CtrlIsDown = False
    End Sub

    Private Sub DockPanel_MouseWheel(sender As Object, e As MouseWheelEventArgs)
        WrapPanelImmagini_MouseWheel(sender, e)
    End Sub

    Private Sub window_MouseWheel(sender As Object, e As MouseWheelEventArgs) Handles window.MouseWheel
        WrapPanelImmagini_MouseWheel(sender, e)
    End Sub

    Private Sub menu_apriprogetto_Click(sender As Object, e As RoutedEventArgs) Handles menu_apriprogetto.Click
        log.Info("Apertura progetto")

        ' Configure open file dialog box
        Dim dialog As New Microsoft.Win32.OpenFileDialog()
        dialog.FileName = "project" ' Default file name
        dialog.DefaultExt = ".sgraf" ' Default file extension
        dialog.Filter = "Text documents (.sgraf)|*.sgraf" ' Filter files by extension

        ' Show open file dialog box
        Dim result As Boolean = dialog.ShowDialog()

        ' Process open file dialog box results
        If result = True Then
            ' Open document
            Dim filename As String = dialog.FileName

            'dialog apertura file
            Dim sTxtResult = feAction.openTxtFile(filename)
            'procede solo se il file non è vuoto
            If sTxtResult.Length <= 0 Then
                MsgBox("File vuoto", MsgBoxStyle.DefaultButton1, "Apertura progetto")
                log.Warn("File non valido: " & filename)
            Else
                Dim progetto As New ImagesProjectClass

                Try
                    'deserializza
                    progetto = JsonConvert.DeserializeObject(Of ImagesProjectClass)(sTxtResult)

                    'leggo le preferenze
                    My.Settings.oggetto = progetto.sOggetto
                    My.Settings.titoloFoto = progetto.sTitoloFoto
                    My.Settings.contenutoDettaglio = progetto.sDettagliocontenuto
                    My.Settings.tipoFascicolo = progetto.sTipoFascicolo

                    My.Settings.disposizioneColonne = progetto.iColonne
                    My.Settings.disposizioneRighe = progetto.iRighe

                    My.Settings.fotoAltezzaCM = progetto.iImmagineAltezzaCM
                    My.Settings.fotoLarghezzaCM = progetto.iImmagineLarghezzaCM

                    My.Settings.carattereDimensioneNomeFile = progetto.iDimensioneNomeFile
                    My.Settings.carattereDimensioneDidascalia = progetto.iDimensioneDidascalia
                    My.Settings.carattereDimensioneTitoloImmagine = progetto.iDimensioneTitolo
                    My.Settings.carattereDimensioneDatiEXIF = progetto.iDimensioneDatiExif

                    'elimina tutte le immagini presenti
                    WrapPanelImmagini.Children.Clear()

                    'ripopola la lista immagini
                    PopolaImmagini(progetto.sFileNames.ToArray)
                Catch ex As Exception
                    MsgBox("File non valido - " & ex.Message, MsgBoxStyle.DefaultButton1, "Apertura progetto")
                    log.Error("Errore di apertura file: " & ex.Message)
                End Try

            End If
        End If
    End Sub


End Class


