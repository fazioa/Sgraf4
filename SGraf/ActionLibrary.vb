Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports log4net
Imports Microsoft.VisualBasic.Logging
Imports MS.Internal
Imports Xceed
Imports Xceed.Document.NET
Imports Xceed.Words.NET

Public Class ActionLibrary
    Private Shared ReadOnly log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Dim document As DocX

    Public Function checkNumber(value As String)
        Dim iValue As Integer
        If Integer.TryParse(value, iValue) Then
            If iValue <= 0 Then
                iValue = 1
            End If
        Else
            iValue = 1
        End If
        Return iValue
    End Function

    'riceve in ingresso il documento del tipo DocX ed il contenitore degli oggetti UserControlImg del tipo userControl
    Public Sub wordInizializzaEcompila(ByRef document As DocX, wpanel As WrapPanel)
        Try
            log.Info("Compila intestazione")
            wordScriviSegnalibro(document, My.Settings.segnalibro_intestazione1, My.Settings.intestazione1)
            wordScriviSegnalibro(document, My.Settings.segnalibro_intestazione2, My.Settings.intestazione2)
            wordScriviSegnalibro(document, My.Settings.segnalibro_intestazione3, My.Settings.intestazione3)
        Catch ex As Exception
            log.Error("Errore Compila intestazione")
        End Try
        Try
            log.Info("Compila frontespizio")
            wordScriviSegnalibro(document, My.Settings.segnalibro_oggetto, My.Settings.oggetto)
            wordScriviSegnalibro(document, My.Settings.segnalibro_contenutoDettaglio, My.Settings.contenutoDettaglio)
            wordScriviSegnalibro(document, My.Settings.segnalibro_autore, My.Settings.autore)
            wordScriviSegnalibro(document, My.Settings.segnalibro_luogo_data, My.Settings.luogo & ", " & Date.Now().ToShortDateString)
        Catch ex As Exception
            log.Error("Errore Compila intestazione")
        End Try


        Try
            log.Info("Compila pagine immagini")
            wordscriviPagineImmagini(document, wpanel)
        Catch ex As Exception
            log.Error("Errore pagine immagini: " & ex.Message)
        End Try


    End Sub



    Private Sub wordScriviSegnalibro(ByRef document As DocX, sSegnalibro As String, sContenuto As String)
        document.InsertAtBookmark(sContenuto, sSegnalibro)
    End Sub
    Private Sub wordscriviPagineImmagini(document As DocX, wpanel As WrapPanel)

        If My.Settings.disposizioneColonne <= 0 Then
            My.Settings.disposizioneColonne = 1
            My.Settings.Save()
        End If
        If My.Settings.disposizioneRighe <= 0 Then
            My.Settings.disposizioneRighe = 1
            My.Settings.Save()
        End If


        Dim inrColonne = My.Settings.disposizioneColonne

        ' USA TABELLE SOLO SE RIGHE O COLONNE > 1 
        If (My.Settings.disposizioneColonne > 1 Or My.Settings.disposizioneRighe > 1) Then
            'CREAZIONE E RIEMPIMENTO TABELLA
            document.InsertSectionPageBreak()

            'crea tabella
            Dim t As Xceed.Document.NET.Table = creaTabella(document)
            t.Alignment = Alignment.center

            Dim iCountCella, iRig, iCol As Integer
            iRig = 0
            iCol = 0
            iCountCella = 0

            For Each element As UserControlImg In wpanel.Children
                If iCountCella < My.Settings.disposizioneRighe * My.Settings.disposizioneColonne Then
                    inserisciImmagineInCella(document, t, iRig, iCol, element)

                    iCountCella = iCountCella + 1
                    iCol = iCol + 1
                    If (iCountCella Mod inrColonne) = 0 Then
                        'se il modulo del contatore è 0 vuol dire che stiamo oltrepassando l'ultima colonna, allora incremento la riga e pongo la colonna a 0 (la prima)
                        iRig = iRig + 1
                        iCol = 0
                    End If
                Else
                    iCountCella = 0
                    iRig = 0
                    iCol = 0

                    'inserisce tabella nel documento
                    document.InsertTable(t)

                    'inserisco un'interruzione di pagina
                    document.InsertSectionPageBreak()

                    'crea tabella
                    t = creaTabella(document)

                    ''visto che in questo ciclo l'element è già stato estratto dalla lista, allora devo gestirlo, così lo inserisco nella prima cella, e faccio partire l'indice colonna e count da 2
                    inserisciImmagineInCella(document, t, 0, 0, element)
                    iCol = 1
                    iCountCella = 1
                    If (iCountCella Mod inrColonne) = 0 Then
                        'se il modulo del contatore è 0 vuol dire che stiamo oltrepassando l'ultima colonna, allora incremento la riga e pongo la colonna a 0 (la prima)
                        iRig = iRig + 1
                        iCol = 0
                    End If

                End If
            Next

            'inserisce tabella nel documento
            document.InsertTable(t)

        Else
            'CASO DI UNA SOLA IMMAGINE PER PAGINA
            'scansione di tutti gli elementi immagine
            For Each element As UserControlImg In wpanel.Children
                'inserisco un'interruzione di pagina
                document.InsertSectionPageBreak()
                inserisciImmagine(document, element)

            Next
        End If

    End Sub

    Private Sub insertVerticalImage(usrCtrlImg As UserControlImg, picture As Picture)
        'immagine in verticale
        'allora porto l'altezza al valore impostato nelle preferenze mentre la larghezza è un valore calcolato in base al rapporto larghezza / altezza 

        Dim larghezzaCm = My.Settings.fotoAltezzaCM * (usrCtrlImg.PictureBox1.Source.Width / usrCtrlImg.PictureBox1.Source.Height)

        'se la larghezza che risulta dal calcolo è maggiore di quella impostata nei parametri allora ridimensiono
        If (larghezzaCm < My.Settings.fotoLarghezzaCM) Then
            picture.HeightInches = My.Settings.fotoAltezzaCM * 0.39370078740157483
            picture.WidthInches = picture.HeightInches * (usrCtrlImg.PictureBox1.Source.Width / usrCtrlImg.PictureBox1.Source.Height)
        Else
            picture.WidthInches = My.Settings.fotoLarghezzaCM * 0.39370078740157483
            picture.HeightInches = picture.WidthInches * (usrCtrlImg.PictureBox1.Source.Height / usrCtrlImg.PictureBox1.Source.Width)
        End If

    End Sub

    Private Sub insertOrizzontalImage(usrCtrlImg As UserControlImg, picture As Picture)
        'immagine in orizzontale
        'allora porto la larghezza al valore impostato nelle preferenze mentre l'altezza è un valore calcolato in base al rapporto larghezza / altezza 
        Dim altezzaCm = My.Settings.fotoLarghezzaCM * (usrCtrlImg.PictureBox1.Source.Height / usrCtrlImg.PictureBox1.Source.Width)
        ' altezzaCm = My.Settings.fotoLarghezzaCM * (picture.Height / picture.Width)

        'se l'altezza che risulta dal calcolo è maggiore di quella impostata nei parametri allora ridimensiono
        If (altezzaCm < My.Settings.fotoAltezzaCM) Then
            picture.WidthInches = My.Settings.fotoLarghezzaCM * 0.39370078740157483
            picture.HeightInches = picture.WidthInches * (usrCtrlImg.PictureBox1.Source.Height / usrCtrlImg.PictureBox1.Source.Width)
        Else
            picture.HeightInches = My.Settings.fotoAltezzaCM * 0.39370078740157483
            picture.WidthInches = picture.HeightInches * (usrCtrlImg.PictureBox1.Source.Width / usrCtrlImg.PictureBox1.Source.Height)
        End If
    End Sub

    Private Sub inserisciImmagineInCella(ByRef document As DocX, ByRef t As Xceed.Document.NET.Table, iRig As Integer, iCol As Integer, usrCtrlImg As UserControlImg)


        'inserisce numerazione immagine
        Dim p = t.Rows(iRig).Cells(iCol).Paragraphs(0).Append(" ")
        t.Rows(iRig).Cells(iCol).Paragraphs(0).Alignment = Alignment.center

        p = p.InsertParagraphAfterSelf(My.Settings.titoloFoto & " " & usrCtrlImg.LabelNumeroFoto.Content.ToString)
        p.Alignment = Alignment.center

        p.Font(My.Settings.carattereFont)
        p.FontSize(My.Settings.carattereDimensioneTitoloImmagine)

        'inserisce immagine
        Dim image = document.AddImage(usrCtrlImg.sNomeFile)
        Dim picture = image.CreatePicture()

        picture.Rotation = usrCtrlImg.imgRotation.actualAngularRotation

        If (usrCtrlImg.PictureBox1.Source.Width > usrCtrlImg.PictureBox1.Source.Height) Then
            insertOrizzontalImage(usrCtrlImg, picture)
        Else
            insertVerticalImage(usrCtrlImg, picture)
            End If

        p = p.InsertParagraphAfterSelf(" ")
        p.Alignment = Alignment.center
        p.InsertPicture(picture)

        'solo se il fascicolo è descrittivo
        If My.Settings.tipoFascicolo = tipofascicolo.descrittivo Then

            Dim sEXIF = buildEXIFString(usrCtrlImg)
            inserisceNomeFile(p, System.IO.Path.GetFileName(usrCtrlImg.sNomeFile))
            inserisceDatiEXIF(p, sEXIF)
            inserisceDidascalia(p, usrCtrlImg.TextBoxTag.Text)

        End If
    End Sub

    Private Sub inserisceDidascalia(p As Paragraph, sDidascalia As String)
        'inserisce la didascalia
        p = p.InsertParagraphAfterSelf(sDidascalia)
        p.Alignment = Alignment.center
        p.Font(My.Settings.carattereFont)
        p.FontSize(My.Settings.carattereDimensioneDidascalia)
    End Sub

    Private Sub inserisceDatiEXIF(p As Paragraph, sEXIF As Object)
        'inserisce i dati EXIF
        p = p.InsertParagraphAfterSelf(sEXIF)
        p.Alignment = Alignment.center
        p.Font(My.Settings.carattereFont)
        p.FontSize(My.Settings.carattereDimensioneDatiEXIF)
    End Sub

    Private Sub inserisceNomeFile(ByRef p As Paragraph, sNomeFile As String)
        'inserisce il nome file
        If My.Settings.bNomeFile Then
            p = p.InsertParagraphAfterSelf(sNomeFile)
            p.Alignment = Alignment.center
            p.Font(My.Settings.carattereFont)
            p.FontSize(My.Settings.carattereDimensioneNomeFile)
        End If
    End Sub

    Private Function buildEXIFString(usrCtrlImg As UserControlImg) As Object
        'costruisce la stringa dei dati EXIF
        Dim sEXIF As String = ""
        Dim bFlag As Boolean = False
        If My.Settings.bEXIFDataOra Then
            sEXIF += Trim(usrCtrlImg.dataScatto)
            bFlag = True
        End If
        If My.Settings.bEXIFMarca Then
            If sEXIF <> "" And usrCtrlImg.marca IsNot Nothing Then sEXIF += ", "
            sEXIF += Trim(usrCtrlImg.marca)
            bFlag = True
        End If
        If My.Settings.bEXIFModello Then
            If sEXIF <> "" And usrCtrlImg.modello IsNot Nothing Then sEXIF += ", "
            sEXIF += Trim(usrCtrlImg.modello)
            bFlag = True
        End If

        If My.Settings.bEXIFEsposizione Then
            If sEXIF <> "" And usrCtrlImg.esposizione IsNot Nothing Then sEXIF += ", "
            sEXIF += Trim(usrCtrlImg.esposizione)
            bFlag = True
        End If
        If My.Settings.bEXIFDiaframma Then
            If sEXIF <> "" And usrCtrlImg.diaframma IsNot Nothing Then sEXIF += ", "
            sEXIF += Trim(usrCtrlImg.diaframma)
            bFlag = True
        End If
        If My.Settings.bEXIFISO Then
            If sEXIF <> "" And usrCtrlImg.ISO IsNot Nothing Then sEXIF += ", "
            sEXIF += Trim(usrCtrlImg.ISO)
            bFlag = True
        End If
        If My.Settings.bEXIFFlash Then
            If sEXIF <> "" And usrCtrlImg.flash IsNot Nothing Then sEXIF += ", "
            sEXIF += Trim(usrCtrlImg.flash)
            bFlag = True
        End If
        Return sEXIF
    End Function

    Private Sub inserisciImmagine(ByRef document As DocX, usrCtrlImg As UserControlImg)
        'inserisce numerazione immagine
        Dim p = document.InsertParagraph(My.Settings.titoloFoto & " " & usrCtrlImg.LabelNumeroFoto.Content.ToString)
        p.Alignment = Alignment.center
        p.Font(My.Settings.carattereFont)
        p.FontSize(My.Settings.carattereDimensioneTitoloImmagine)


        'inserisce immagine
        Dim image = document.AddImage(usrCtrlImg.sNomeFile)
        Dim picture = image.CreatePicture()

        picture.Rotation = usrCtrlImg.imgRotation.actualAngularRotation

        If (usrCtrlImg.PictureBox1.Source.Width > usrCtrlImg.PictureBox1.Source.Height) Then
            insertOrizzontalImage(usrCtrlImg, picture)
        Else
            insertVerticalImage(usrCtrlImg, picture)
        End If

        p = p.InsertParagraphAfterSelf(" ")
        p.Alignment = Alignment.center
        p.InsertPicture(picture)

        'solo se il fascicolo è descrittivo
        If My.Settings.tipoFascicolo = tipofascicolo.descrittivo Then

            Dim sEXIF = buildEXIFString(usrCtrlImg)
            inserisceNomeFile(p, System.IO.Path.GetFileName(usrCtrlImg.sNomeFile))
            inserisceDatiEXIF(p, sEXIF)
            inserisceDidascalia(p, usrCtrlImg.TextBoxTag.Text)
        End If

        'se si tratta di un fascicolo per l'identificazione aggiungo la legenda all'ultima pagina
        If My.Settings.tipoFascicolo = tipofascicolo.identificazione Then

        End If


    End Sub

    Private Function creaTabella(ByRef document As DocX)
        Dim t = document.AddTable(My.Settings.disposizioneRighe, My.Settings.disposizioneColonne)
        t.Alignment = Alignment.center
        Dim brd = New Border()
        brd.Tcbs = Xceed.Document.NET.BorderStyle.Tcbs_none
        t.SetBorder(TableBorderType.Bottom, brd)
        t.SetBorder(TableBorderType.InsideH, brd)
        t.SetBorder(TableBorderType.InsideV, brd)
        t.SetBorder(TableBorderType.Left, brd)
        t.SetBorder(TableBorderType.Right, brd)
        t.SetBorder(TableBorderType.Top, brd)
        Return t
    End Function


    Public Sub salvaTxtFile(sNomeFile As String, sValore As String)
        Dim file As New StreamWriter(sNomeFile)
        file.Write(sValore)
        file.Flush()
        file.Close()
    End Sub

    Friend Function openTxtFile(filename As String) As Object

        Dim sTxtFile As String = ""
        Try
            Dim file As New StreamReader(filename)
            sTxtFile = file.ReadToEnd
            file.Close()
        Catch ex As Exception
            log.Error("Errore di lettura del file pro """ & filename)
        End Try
        Return sTxtFile

    End Function

    'restituisce una stringa costituita da anno mese giorno ora minuti secondi
    Public Shared Function getTimeStamp() As String
        Dim d As Date = DateTime.Now
        getTimeStamp = d.Year & Format(d.Month, "d2") & Format(d.Day, "d2") & Format(d.Hour, "d2") & Format(d.Minute, "d2") & Format(d.Second, "d2")
    End Function
End Class


Public Class ImagesProjectClass
    Public Property sOggetto As String
    Public Property sTitoloFoto As String
    Public Property sTipoFascicolo As String
    Public Property sDettagliocontenuto As String
    Public Property iColonne As String
    Public Property iRighe As String
    Public Property iImmagineAltezzaCM As String
    Public Property iImmagineLarghezzaCM As String
    Public Property iDimensioneNomeFile As String
    Public Property iDimensioneDidascalia As String
    Public Property iDimensioneTitolo As String
    Public Property iDimensioneDatiExif As String
    Public Property sFileNames As List(Of String)
End Class