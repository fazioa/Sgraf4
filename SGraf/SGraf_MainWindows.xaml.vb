

Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports log4net
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32

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

    Private Sub PopolaImmagini(arrayFileNames As String())
        Dim i As Integer = 0
        Dim sFile As String
        Dim imageItem As UserControlImg
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

                imageItem = New UserControlImg(b_image, sFile, My.Settings.fotoLarghezzaThumb, My.Settings.fotoAltezzaThumb)
                WrapPanelImmagini.Children.Add(imageItem)

            Catch ex As Exception
                log.Error("Inserimento immagine fallito - " & ex.Message)
            End Try
        Next

        For Each child As UserControlImg In WrapPanelImmagini.Children
            RemoveHandler child.MouseDown, AddressOf childs_MouseDown
            AddHandler child.MouseDown, AddressOf childs_MouseDown

            'RemoveHandler child.MouseMove, AddressOf root_MouseMove
            'AddHandler child.MouseMove, AddressOf root_MouseMove
            'RemoveHandler child.MouseLeftButtonDown, AddressOf root_MouseLeftButtonDown
            'AddHandler child.MouseLeftButtonDown, AddressOf root_MouseLeftButtonDown
            'RemoveHandler child.MouseLeftButtonUp, AddressOf root_MouseLeftButtonUp
            'AddHandler child.MouseLeftButtonUp, AddressOf root_MouseLeftButtonUp

            RemoveHandler child.Drop, AddressOf childs_Drop
            AddHandler child.Drop, AddressOf childs_Drop


            child.LabelNumeroFoto.Content = WrapPanelImmagini.Children.IndexOf(child).ToString + 1

        Next
    End Sub

    Private Sub childs_Drop(sender As Object, e As DragEventArgs)

        If (e.Data.GetDataPresent(dragtype)) Then
            Dim source As UserControlImg = CType(e.Data.GetData(dragtype), UserControlImg)

            Dim final As UserControlImg = sender

            Dim wpanel As WrapPanel = source.Parent

            'estrare il numero di posizione finale
            Dim i_final = wpanel.Children.IndexOf(final)
            Dim i_source = wpanel.Children.IndexOf(source)

            If (i_final <> i_source) Then
                'se l'immagine non è stata spostata
                'visto che si tratta di un drag&drop e l'operazione di selezione immagine è stata stata eseguita, inverto per ripristinare lo stato
                source.toggleSelected()
            End If


            log.Info("Sposta immagine alla posizione " & i_final)
                'inserisce l'elelento nella nuova posizione
                wpanel.Children.Remove(source)
                wpanel.Children.Insert(i_final, source)

                'rinumera
                log.Info("Aggiorna la numerazione delle immagini")
                renumber(wpanel)

            End If

    End Sub

    Private Sub renumber(wpanel As WrapPanel)
        For Each child As UserControlImg In wpanel.Children
            child.LabelNumeroFoto.Content = wpanel.Children.IndexOf(child) + 1
        Next
    End Sub



    Private Sub childs_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs)

        If e.LeftButton = MouseButtonState.Pressed Then

            Me.dragtype = sender.GetType
            'Package the data.
            Dim Data As DataObject = New DataObject()
            Data.SetData(sender)
            ' Initiate the drag-And-drop operation.
            log.Info("Avvia spostamento immagine")
            DragDrop.DoDragDrop(Me, Data, DragDropEffects.Move)

            'tratta l'evento come un click e selezione l'immagine
            sender.toggleSelected()
        End If
    End Sub




    Dim anchorPoint As System.Windows.Point
    Dim currentPoint As System.Windows.Point
    Dim isInDrag As Boolean = False
    '   Dim Transform = New TranslateTransform()
    'Private Sub root_MouseMove(sender As Object, e As MouseEventArgs)
    '    If isInDrag = True Then
    '        Dim element As FrameworkElement = sender
    '        currentPoint = e.GetPosition(Nothing)

    '        Transform.X += currentPoint.X - anchorPoint.X
    '        Transform.Y += currentPoint.Y - anchorPoint.Y

    '        CType(sender, UserControlImg).RenderTransform = Transform
    '        anchorPoint = currentPoint
    '    End If


    'End Sub

    'Private Sub root_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
    '    Dim element As FrameworkElement = sender
    '    anchorPoint = e.GetPosition(Nothing)
    '        element.CaptureMouse()

    '        isInDrag = True
    '        e.Handled = True
    'End Sub
    'Private Sub root_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
    '    If isInDrag = True Then
    '        Dim element As FrameworkElement = sender
    '        element.ReleaseMouseCapture()

    '        isInDrag = False
    '        e.Handled = False
    '    End If
    'End Sub


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
            Dim index As Integer
            Dim usrCtrl As UserControlImg

            For index = WrapPanelImmagini.Children.Count - 1 To 0 Step -1
                usrCtrl = WrapPanelImmagini.Children.Item(index)
                If usrCtrl.isSelected Then
                    WrapPanelImmagini.Children.Remove(usrCtrl)
                End If
            Next
            renumber(WrapPanelImmagini)
        End If
    End Sub
End Class

