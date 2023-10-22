

Imports System.Drawing
Imports System.IO
Imports log4net
Imports Microsoft.VisualBasic.ApplicationServices

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


        'Dim img As Image = New Image()
        Dim sPath As String = AppDomain.CurrentDomain.BaseDirectory & "test"


        ' For a = 0 To 50
        ' Dim usr As UserControlImg = New UserControlImg(img, sPath & "\dsc_0181.jpg", 250, 100)
        ' WrapPanelImmagini.Children.Add(usr)
        '    Next


    End Sub

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

                'BitmapImage è la miniatura
                Dim b_image As BitmapImage = New BitmapImage()
                b_image.BeginInit()
                b_image.UriSource = New Uri(sFile, UriKind.RelativeOrAbsolute)
                b_image.DecodePixelHeight = 200
                b_image.EndInit()

                imageItem = New UserControlImg(b_image, sFile, My.Settings.fotoLarghezzaThumb, My.Settings.fotoAltezzaThumb)
                WrapPanelImmagini.Children.Add(imageItem)

            Catch ex As Exception
                log.Info("Inserimento immagine fallito - " & ex.Message)
            End Try
            i = i + 1
        Next

        For Each child As UserControlImg In WrapPanelImmagini.Children
            RemoveHandler child.PictureBox1.MouseDown, AddressOf childs_MouseDown
            AddHandler child.PictureBox1.MouseDown, AddressOf childs_MouseDown

            child.LabelNumeroFoto.Content = WrapPanelImmagini.Children.IndexOf(child).ToString + 1

        Next
    End Sub

    Private Sub childs_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs)
        'occorre distinguere tra drag&drop e click
        Dim source As UserControlImg = CType(sender.parent, UserControlImg)
        ' If e.Button = MouseButtons.Left Then
        ' Me.dragtype = source.GetType
        ' Me.DoDragDrop(source, DragDropEffects.Move)
        ' Else

        '  End If
    End Sub


End Class

