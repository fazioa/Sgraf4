Imports System.Data
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
            Dim p = document.InsertParagraph(" ")
            p.Alignment = Alignment.center

            p.InsertTableAfterSelf(t)
            'document.InsertTable(t)

        End If
    End Sub

    Private Sub inserisciImmagineInCella(ByRef document As DocX, ByRef t As Xceed.Document.NET.Table, iRig As Integer, iCol As Integer, usrCtrlImg As UserControlImg)

        'inserisce numerazione immagine
        Dim p = t.Rows(iRig).Cells(iCol).Paragraphs(0).Append(My.Settings.titoloFoto & " " & usrCtrlImg.LabelNumeroFoto.Content.ToString)
        t.Rows(iRig).Cells(iCol).Paragraphs(0).Alignment = Alignment.center


        p.Font(My.Settings.carattereFont)

        'p.Font(New Xceed.Document.NET.Font("Arial"))
        p.FontSize(My.Settings.carattereDimensioneTitoloImmagine)
        '  p.Bold()
        ' p.Alignment = Alignment.center



        'inserisce immagine
        Dim Image = document.AddImage(usrCtrlImg.sNomeFile)
        Dim picture = Image.CreatePicture(150, 150)

        p = p.InsertParagraphAfterSelf(" ")
        p.Alignment = Alignment.center
        p.InsertPicture(picture)

        'solo se il fascicolo è descrittivo
        Dim sEXIF As String = ""
        If My.Settings.tipoFascicolo = tipofascicolo.descrittivo Then
            'costruisce la stringa dei dati EXIF
            Dim bFlag As Boolean = False
            If My.Settings.bEXIFMarca Then
                sEXIF += Trim(usrCtrlImg.marca)
                bFlag = True
            End If
            If My.Settings.bEXIFModello Then
                If sEXIF <> "" And usrCtrlImg.modello IsNot Nothing Then sEXIF += ", "
                sEXIF += Trim(usrCtrlImg.modello)
                bFlag = True
            End If
            If My.Settings.bEXIFDataOra Then
                If sEXIF <> "" And usrCtrlImg.dataScatto IsNot Nothing Then sEXIF += ", "
                sEXIF += Trim(usrCtrlImg.dataScatto)
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

            'inserisce il nome file
            p = p.InsertParagraphAfterSelf(System.IO.Path.GetFileName(usrCtrlImg.sNomeFile))
            p.Alignment = Alignment.center
            p.Font(My.Settings.carattereFont)
            p.FontSize(My.Settings.carattereDimensioneNomeFile)


            'inserisce i dati EXIF
            p = p.InsertParagraphAfterSelf(sEXIF)
            p.Alignment = Alignment.center
            p.Font(My.Settings.carattereFont)
            p.FontSize(My.Settings.carattereDimensioneDatiEXIF)


            'inserisce la didascalia
            p = p.InsertParagraphAfterSelf(usrCtrlImg.TextBoxTag.Text)
            p.Alignment = Alignment.center
            p.Font(My.Settings.carattereFont)
            p.FontSize(My.Settings.carattereDimensioneDidascalia)


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
End Class
