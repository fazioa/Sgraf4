﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Il codice è stato generato da uno strumento.
'     Versione runtime:4.0.30319.42000
'
'     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
'     il codice viene rigenerato.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.7.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "Funzionalità di salvataggio automatico My.Settings"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("prima riga intestazione")>  _
        Public Property intestazione1() As String
            Get
                Return CType(Me("intestazione1"),String)
            End Get
            Set
                Me("intestazione1") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("seconda riga intestazione")>  _
        Public Property intestazione2() As String
            Get
                Return CType(Me("intestazione2"),String)
            End Get
            Set
                Me("intestazione2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("terza riga intestazione")>  _
        Public Property intestazione3() As String
            Get
                Return CType(Me("intestazione3"),String)
            End Get
            Set
                Me("intestazione3") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Indagini relative a....")>  _
        Public Property oggetto() As String
            Get
                Return CType(Me("oggetto"),String)
            End Get
            Set
                Me("oggetto") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Contiene fotografie di....")>  _
        Public Property contenutoDettaglio() As String
            Get
                Return CType(Me("contenutoDettaglio"),String)
            End Get
            Set
                Me("contenutoDettaglio") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("nome e cognome autore")>  _
        Public Property autore() As String
            Get
                Return CType(Me("autore"),String)
            End Get
            Set
                Me("autore") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("comune")>  _
        Public Property luogo() As String
            Get
                Return CType(Me("luogo"),String)
            End Get
            Set
                Me("luogo") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property gradoCognomeNome() As String
            Get
                Return CType(Me("gradoCognomeNome"),String)
            End Get
            Set
                Me("gradoCognomeNome") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("20")>  _
        Public Property carattereDimensioneTitoloImmagine() As String
            Get
                Return CType(Me("carattereDimensioneTitoloImmagine"),String)
            End Get
            Set
                Me("carattereDimensioneTitoloImmagine") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Immagine nr.")>  _
        Public Property titoloFoto() As String
            Get
                Return CType(Me("titoloFoto"),String)
            End Get
            Set
                Me("titoloFoto") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bIncorporaImmagini() As String
            Get
                Return CType(Me("bIncorporaImmagini"),String)
            End Get
            Set
                Me("bIncorporaImmagini") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property disposizioneColonne() As String
            Get
                Return CType(Me("disposizioneColonne"),String)
            End Get
            Set
                Me("disposizioneColonne") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2")>  _
        Public Property disposizioneRighe() As String
            Get
                Return CType(Me("disposizioneRighe"),String)
            End Get
            Set
                Me("disposizioneRighe") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("9")>  _
        Public Property carattereDimensioneDidascalia() As String
            Get
                Return CType(Me("carattereDimensioneDidascalia"),String)
            End Get
            Set
                Me("carattereDimensioneDidascalia") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("15")>  _
        Public Property fotoLarghezzaCM() As String
            Get
                Return CType(Me("fotoLarghezzaCM"),String)
            End Get
            Set
                Me("fotoLarghezzaCM") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("10")>  _
        Public Property fotoAltezzaCM() As String
            Get
                Return CType(Me("fotoAltezzaCM"),String)
            End Get
            Set
                Me("fotoAltezzaCM") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property tipoFascicolo() As String
            Get
                Return CType(Me("tipoFascicolo"),String)
            End Get
            Set
                Me("tipoFascicolo") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property newVersion() As String
            Get
                Return CType(Me("newVersion"),String)
            End Get
            Set
                Me("newVersion") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bMemorizzaDidascalia() As String
            Get
                Return CType(Me("bMemorizzaDidascalia"),String)
            End Get
            Set
                Me("bMemorizzaDidascalia") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("151")>  _
        Public Property fotoAltezzaThumb() As String
            Get
                Return CType(Me("fotoAltezzaThumb"),String)
            End Get
            Set
                Me("fotoAltezzaThumb") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bEXIFMarca() As String
            Get
                Return CType(Me("bEXIFMarca"),String)
            End Get
            Set
                Me("bEXIFMarca") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bEXIFModello() As String
            Get
                Return CType(Me("bEXIFModello"),String)
            End Get
            Set
                Me("bEXIFModello") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bEXIFEsposizione() As String
            Get
                Return CType(Me("bEXIFEsposizione"),String)
            End Get
            Set
                Me("bEXIFEsposizione") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bEXIFDiaframma() As String
            Get
                Return CType(Me("bEXIFDiaframma"),String)
            End Get
            Set
                Me("bEXIFDiaframma") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bEXIFDataOra() As String
            Get
                Return CType(Me("bEXIFDataOra"),String)
            End Get
            Set
                Me("bEXIFDataOra") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bEXIFFlash() As String
            Get
                Return CType(Me("bEXIFFlash"),String)
            End Get
            Set
                Me("bEXIFFlash") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bEXIFISO() As String
            Get
                Return CType(Me("bEXIFISO"),String)
            End Get
            Set
                Me("bEXIFISO") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("192, 255, 255")>  _
        Public Property coloreSelezione() As String
            Get
                Return CType(Me("coloreSelezione"),String)
            End Get
            Set
                Me("coloreSelezione") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-40")>  _
        Public Property trasparenzaImmagine() As String
            Get
                Return CType(Me("trasparenzaImmagine"),String)
            End Get
            Set
                Me("trasparenzaImmagine") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property bNomeFile() As String
            Get
                Return CType(Me("bNomeFile"),String)
            End Get
            Set
                Me("bNomeFile") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("30")>  _
        Public Property zoomMiniaturaDefaultMenu() As String
            Get
                Return CType(Me("zoomMiniaturaDefaultMenu"),String)
            End Get
            Set
                Me("zoomMiniaturaDefaultMenu") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("3")>  _
        Public Property zoomMiniaturaDefaultMouse() As String
            Get
                Return CType(Me("zoomMiniaturaDefaultMouse"),String)
            End Get
            Set
                Me("zoomMiniaturaDefaultMouse") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("200")>  _
        Public Property thumbnailDisplayResolution() As String
            Get
                Return CType(Me("thumbnailDisplayResolution"),String)
            End Get
            Set
                Me("thumbnailDisplayResolution") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("fascicolo fotografico.dotx")>  _
        Public Property doc_nomeModello() As String
            Get
                Return CType(Me("doc_nomeModello"),String)
            End Get
            Set
                Me("doc_nomeModello") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("autore")>  _
        Public Property segnalibro_autore() As String
            Get
                Return CType(Me("segnalibro_autore"),String)
            End Get
            Set
                Me("segnalibro_autore") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("intestazione1")>  _
        Public Property segnalibro_intestazione1() As String
            Get
                Return CType(Me("segnalibro_intestazione1"),String)
            End Get
            Set
                Me("segnalibro_intestazione1") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("intestazione2")>  _
        Public Property segnalibro_intestazione2() As String
            Get
                Return CType(Me("segnalibro_intestazione2"),String)
            End Get
            Set
                Me("segnalibro_intestazione2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("intestazione3")>  _
        Public Property segnalibro_intestazione3() As String
            Get
                Return CType(Me("segnalibro_intestazione3"),String)
            End Get
            Set
                Me("segnalibro_intestazione3") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("oggetto")>  _
        Public Property segnalibro_oggetto() As String
            Get
                Return CType(Me("segnalibro_oggetto"),String)
            End Get
            Set
                Me("segnalibro_oggetto") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("contenutoDettaglio")>  _
        Public Property segnalibro_contenutoDettaglio() As String
            Get
                Return CType(Me("segnalibro_contenutoDettaglio"),String)
            End Get
            Set
                Me("segnalibro_contenutoDettaglio") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("luogo_data")>  _
        Public Property segnalibro_luogo_data() As String
            Get
                Return CType(Me("segnalibro_luogo_data"),String)
            End Get
            Set
                Me("segnalibro_luogo_data") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1")>  _
        Public Property carattereFont() As String
            Get
                Return CType(Me("carattereFont"),String)
            End Get
            Set
                Me("carattereFont") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("12")>  _
        Public Property carattereDimensioneNomeFile() As String
            Get
                Return CType(Me("carattereDimensioneNomeFile"),String)
            End Get
            Set
                Me("carattereDimensioneNomeFile") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8")>  _
        Public Property carattereDimensioneDatiEXIF() As String
            Get
                Return CType(Me("carattereDimensioneDatiEXIF"),String)
            End Get
            Set
                Me("carattereDimensioneDatiEXIF") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("400")>  _
        Public Property fotoLarghezzaThumb() As String
            Get
                Return CType(Me("fotoLarghezzaThumb"),String)
            End Get
            Set
                Me("fotoLarghezzaThumb") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("180")>  _
        Public Property fotoLarghezzaThumbMin() As String
            Get
                Return CType(Me("fotoLarghezzaThumbMin"),String)
            End Get
            Set
                Me("fotoLarghezzaThumbMin") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("fascicolo.doc")>  _
        Public Property doc_savedDocumentName() As String
            Get
                Return CType(Me("doc_savedDocumentName"),String)
            End Get
            Set
                Me("doc_savedDocumentName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Legenda")>  _
        Public Property titoloLegenda() As String
            Get
                Return CType(Me("titoloLegenda"),String)
            End Get
            Set
                Me("titoloLegenda") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("12")>  _
        Public Property carattereDimensioneLegenda() As String
            Get
                Return CType(Me("carattereDimensioneLegenda"),String)
            End Get
            Set
                Me("carattereDimensioneLegenda") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("modello")>  _
        Public Property doc_modelPath() As String
            Get
                Return CType(Me("doc_modelPath"),String)
            End Get
            Set
                Me("doc_modelPath") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("result")>  _
        Public Property doc_resultPath() As String
            Get
                Return CType(Me("doc_resultPath"),String)
            End Get
            Set
                Me("doc_resultPath") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("150")>  _
        Public Property print_dpi() As String
            Get
                Return CType(Me("print_dpi"),String)
            End Get
            Set
                Me("print_dpi") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property bHashSHA1() As Boolean
            Get
                Return CType(Me("bHashSHA1"),Boolean)
            End Get
            Set
                Me("bHashSHA1") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property bHashSHA256() As Boolean
            Get
                Return CType(Me("bHashSHA256"),Boolean)
            End Get
            Set
                Me("bHashSHA256") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property bHashMD5() As Boolean
            Get
                Return CType(Me("bHashMD5"),Boolean)
            End Get
            Set
                Me("bHashMD5") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("180")>  _
        Public Property fotoAltezzaThumbMin() As String
            Get
                Return CType(Me("fotoAltezzaThumbMin"),String)
            End Get
            Set
                Me("fotoAltezzaThumbMin") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Hash SHA1:")>  _
        Public Property bHashSHA1_name() As String
            Get
                Return CType(Me("bHashSHA1_name"),String)
            End Get
            Set
                Me("bHashSHA1_name") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Hash SHA256:")>  _
        Public Property bHashSHA256_name() As String
            Get
                Return CType(Me("bHashSHA256_name"),String)
            End Get
            Set
                Me("bHashSHA256_name") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Hash MD5:")>  _
        Public Property bHashMD5_name() As String
            Get
                Return CType(Me("bHashMD5_name"),String)
            End Get
            Set
                Me("bHashMD5_name") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.SGraf.My.MySettings
            Get
                Return Global.SGraf.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
