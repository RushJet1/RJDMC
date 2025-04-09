Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security

Partial Class MainForm

    Private Sub Handler() Handles tsbPreview.Click
        AudioPreview(curWav, True)
    End Sub

    'Runs VisualizeDMC if there is no current preview.  If there is, it just plays the current preview
    'on a separate thread.
    Private Sub AudioPreview(ByRef Wave As WAV, ByVal IsCurWav As Boolean)
        If Wave Is Nothing Then
            Return
        ElseIf CurPreview Is Nothing Or PrevChange Or IsCurWav = False Then     'no Cur Preview

            'This section displays a loading box for larger file operations
            If curWav.Data.Samples.Count < 400000 Then
                CurPreview = New WAV(33144, NESResample(WAVtoDMCWAV(Wave)), 8)   'Create new CurPreview
            Else
                Dim tmpLst As List(Of Integer) = WAVtoDMCWAV(Wave)
                DisplayBox("Generating audio data...")
                CurPreview = New WAV(33144, NESResample(tmpLst), 8)   'possibly resample (qualrate)
                DelBox()
            End If

            If IsCurWav = True Then     'This section basically asks "Should this redraw the window?"
                PrevChange = False
                VisualizeDMC()
                Redraw()
            Else
                PrevChange = True
            End If
        End If

        'fix pops at the end of files that are not aligned (16 bit rewrite) in future
        'CurPreview.ExportWave()


        'Creates a memorystream that is an array of the bytes that would make up a full .wav file.
        Dim str As New MemoryStream(CurPreview.ExportWave(False, 0).ToArray)


        'If the thread still exists, make "command" false to stop playback, and abort thread.
        If Not thr Is Nothing Then
            AP.Com.Command = False
            thr.Abort()
            thr.Join()
        End If

        'Creates a new instance of AudPlayer and plays the sound
        AP = New AudPlayer
        AP.AudioStream = str

        'Loops if necessary
        If chkLoop.Checked = True Then
            AP.Looped = True
        End If

        'Creates a new instance of a soundplayer with the memorystream housing the Wave info.
        AP.Sound = New System.Media.SoundPlayer(str)

        'Makes a new thread and runs it
        thr = New System.Threading.Thread(AddressOf AP.Run)
        thr.Start()

    End Sub

    'Simply changes the volume (in percent) by the number in the NumericUpDown box.
    '   Subtracts 31 from (sample) and multiplies by the percentage, then adds 31 and 
    '   normalizes to the 6-bit volume format (0-63)
    Private Sub Volumize(ByRef v As List(Of Integer), ByVal Vol As Integer)
        For x = 0 To v.Count - 1
            v(x) -= 31
            v(x) = v(x) * (Vol / 100)
            v(x) += 31
            If v(x) > 63 Then
                v(x) = 63
            ElseIf v(x) < 0 Then
                v(x) = 0
            End If
        Next
    End Sub

    'This changes the quality rate of the file.  If the user wants to have a sample
    ' at a lower rate to save space in their song, they can.  If the sample is small, adds
    ' padding at the back of it, resamples, then chops off padding.
    Private Sub Pitchize(ByRef v As List(Of Integer))
        If QualRate = 33144 Then
            Return
        End If
        Dim t As New WAV(QualRate, v, 6)
        Dim SmallCutoff As Integer = 0
        If t.Data.Samples.Count < 15000 Then      'if the file is so small that ssrc will crash or duplicate its contents, make silence/padding
            SmallCutoff = t.Data.Samples.Count
            For x = t.Data.Samples.Count To 15000
                t.Data.Samples.Add(0)
            Next
        End If

        Dim rate As Integer = 33144
        Dim ModNumber As Long = rate / GCD(rate, 33144)
        While 1
            If ModNumber Mod 2 = 0 Or ModNumber Mod 3 = 0 Or rate = 44100 Then
                Exit While
            End If
            rate -= 1
            ModNumber = rate / GCD(rate, QualRate)
        End While

        SaveOrig("~tmp2.wav", t, rate, True)     'saves the file to a temp file
        SSRC("~tmp2.wav", "~tmpP.wav", QualRate, v.Count > 400000)
        v = New WAV(OpenFile("~tmpP.wav")).Data.Samples
        Try
            File.Delete("~tmp2.wav")
        Catch
        End Try
        Try
            File.Delete("~tmpP.wav")
        Catch
        End Try
        If SmallCutoff > 0 Then
            Dim p As New List(Of Integer)
            For x = 0 To (SmallCutoff / (rate / QualRate)) - 1
                p.Add(v(x))
            Next
            v = p
        End If
    End Sub

    'Stops the current sound from playing.
    Private Sub StopSound() Handles ToolStripButton1.Click, Me.FormClosed
        If Not thr Is Nothing Then
            AP.Com.Command = False      'tells the thread's class to stop using
            thr.Abort()                 'the secondary class's eventhandler
            thr.Join()
        End If
    End Sub

    'Instead of using SSRC, this just stretches out the wave to the proper
    'rate.  This is how a NES does it (no interpolation) so this function
    'is definitely necessary.
    Function NESResample(ByVal z As List(Of Integer)) As List(Of Integer)
        If PrevRate = 33144 Then
            Return z
        End If
        Dim p As New List(Of Integer)
        Dim x As Double
        For x = 0 To z.Count - 1 Step PrevRate / 33144 'Prevrate = slider value
            p.Add(z(CInt(x)))                          'this adds the value (prevrate/33144) times
        Next                                           'per step, so smaller values add more and are longer
        Return p
    End Function

End Class
