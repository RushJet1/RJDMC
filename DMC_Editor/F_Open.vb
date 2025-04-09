Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security

Partial Class MainForm
    'Opens a wave file and goes into manual mode if that is
    'checked (or if the user did not check anything, since it
    'is the default).  If other modes are checked, it reacts
    'accordingly and opens multiple files at once.

    Private Sub OpenWAVE_Event(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenwavToolStripMenuItem.Click, tsbOpenWave.Click
        ofd.Filter = "Wave Soundfile(*.wav)|*.wav"
        ofd.AddExtension = True
        ofd.RestoreDirectory = True
        ofd.FilterIndex = 0
        If ofd.ShowDialog() = 1 Then
            Dim z() As String = ofd.FileNames
            If Pref.radManual.Checked = True Then
                If z.Count > 1 Then
                    MessageBox.Show("Opening multiple files is not supported in ""Manual"" save mode.")
                    Return
                End If
            End If
            For Each FilePath As String In z
                OpenWav(FilePath)
                If Pref.radWav.Checked = True Then
                    Dim loc As String = FilePath.Remove(FilePath.Length - 4)
                    Save(loc + "_DMC.wav", curWav, 33144)
                ElseIf Pref.radDmc.Checked = True Then
                    SaveDMC(FilePath.Remove(FilePath.Length - 4) + ".dmc")
                End If
            Next
            If Not Pref.radManual.Checked Then
                CloseToolStripMenuItem_Click(sender, e)
                MessageBox.Show("Successfully converted files.")
            End If
        End If
    End Sub


    'Same as OpenWAV_Event except that it opens a DMC file.
    Private Sub OpenDMC_Event(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOpenDMC.Click, OpendmcToolStripMenuItem.Click
        ofd.Filter = "DMC Soundfile(*.dmc)|*.dmc"
        ofd.AddExtension = True
        ofd.RestoreDirectory = True
        ofd.FilterIndex = 0
        If ofd.ShowDialog() = 1 Then
            Dim z() As String = ofd.FileNames
            If Pref.radManual.Checked = True Then
                If z.Count > 1 Then
                    MessageBox.Show("Opening multiple files is not supported in ""Manual"" save mode.")
                    Return
                End If
            End If
            For Each FilePath As String In z
                OpenDMC(FilePath)
                If Pref.radWav.Checked = True Then
                    Dim loc As String = FilePath.Remove(FilePath.Length - 4)
                    Save(loc + "_DMC.wav", curWav, 33144)
                ElseIf Pref.radDmc.Checked = True Then
                    SaveDMC(FilePath.Remove(FilePath.Length - 4) + "_copy.dmc")
                End If
            Next
        End If
    End Sub

    'This is the drag & drop functionality of the program.
    'it is functionally the same as the openDMC_Event and openWAVE_Event
    Private Sub Drop(ByVal sender As System.Object, ByVal e As DragEventArgs) Handles Me.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim z() As String = e.Data.GetData(DataFormats.FileDrop)
            If Pref.radManual.Checked = True Then
                If z.Count > 1 Then
                    MessageBox.Show("Dragging multiple files is not supported in ""Manual"" save mode.")
                    Return
                End If
            End If
            For Each FilePath As String In z
                If Path.GetExtension(FilePath) = ".dmc" Then
                    OpenDMC(FilePath)
                ElseIf Path.GetExtension(FilePath) = ".wav" Then
                    OpenWav(FilePath)
                Else
                    Return
                End If
                If Pref.radWav.Checked = True Then
                    Dim loc As String = FilePath.Remove(FilePath.Length - 4)
                    Save(loc + "_DMC.wav", curWav, 33144)
                ElseIf Pref.radDmc.Checked = True Then
                    If Path.GetExtension(FilePath) = ".dmc" Then
                        SaveDMC(FilePath.Remove(FilePath.Length - 4) + "_copy.dmc")
                    Else
                        SaveDMC(FilePath.Remove(FilePath.Length - 4) + ".dmc")
                    End If
                End If
            Next
            If Not Pref.radManual.Checked Then
                CloseToolStripMenuItem_Click(sender, e)
                MessageBox.Show("Successfully converted files.")
            End If
        End If
    End Sub

    'Opens any file and returns it as a list of bytes.
    Private Function OpenFile(ByVal FilePath As String) As BinaryReader        'opens ANY file and returns it as a byte list.
        Dim br As BinaryReader
        Dim fs As FileStream
        Try
            fs = New FileStream(FilePath, FileMode.Open, FileAccess.Read)
            br = New BinaryReader(fs)
        Catch ex As Exception
            MessageBox.Show("Could not find file, or no file was selected." & vbNewLine & vbNewLine & ex.ToString)
            Return Nothing
        End Try
        'Dim wavebytearr(fs.Length) As Byte

        'fs.Read(wavebytearr, 0, fs.Length)
        'fs.Close()
        'fs.Dispose()
        'Return wavebytearr.ToList
        Return br
    End Function

    'Specifically opens a wave file. 
    'INPUT: File Path
    'OUTPUT: WAV Class instance of that wave file.
    Private Function WOpen(ByVal FilePath As String) As WAV


        Dim tmpWav As WAV
        Try
            tmpWav = New WAV(OpenFile(FilePath))
        Catch
            MessageBox.Show("This is not a wave file or file is corrupt!")
            DelBox()
            Return Nothing
        End Try
        If tmpWav.IsCorrupted Then
            MessageBox.Show("This is not a wave file or file is corrupt!")
            Return Nothing
        End If
        Return tmpWav
    End Function


    'This is called when a new file is opened or all of the files
    'are closed.  Saves memory.
    Private Sub GarbageCollect()
        tmpWavData = Nothing
        curWav = Nothing
        BckupWav = Nothing
        curDMC = Nothing
        CurPreview = Nothing
        GC.Collect()
    End Sub

    'Opens a DMC file and creates CURWAV from the DMC class.
    Private Sub OpenDMC(ByVal FilePath As String)
        GarbageCollect()
        Dim tmpDMC As New DMC(OpenFile(FilePath))
        DisplayBox("Processing DMC data...")
        curWav = New WAV(33144, tmpDMC.ToWaveData(numDelta.Value), 8)
        DelBox()
        BckupWav = curWav.Copy
        BlankLabels()
        lblBits.Text += " " & curWav.Format.BitsPerSample.ToString
        lblChannels.Text += " Mono"
        lblFormat.Text += " DMC"
        lblRate.Text += " 33144"
        lblSize.Text += " " & tmpDMC.Samples.Count
        lblName.Text += " " & Path.GetFileName(FilePath)

        counter = 1     '"3" is the highest depth
        VisualizeDMC()
        Redraw()
        StopSound()
        'Dim flag As Boolean = False
        'For Each sample In curWav.Data.Samples
        ' If sample = 127 Then
        'flag = True
        'End If
        'Next
        RevForm()
    End Sub

    'Returns the least common multiple of two numbers.
    Function GCD(ByVal n1 As Long, ByVal n2 As Long) As Long
        Dim tmp, product As Long
        product = n1 * n2

        'the following block evaluates the GCD of the two numbers
        Do
            'swap the items so that n1 >= n2
            If n1 < n2 Then
                tmp = n1
                n1 = n2
                n2 = tmp
            End If
            'take the modulo
            n1 = n1 Mod n2
        Loop While n1

        'now n2 contains the GCD of the two numbers
        Return n2
    End Function

    'Overwrites the current size of the file and adds more zeros to the end.
    'Also changes to a SSRC-compatible rate and deletes INFO blocks.
    Private Sub OverwriteWave(ByVal FileName As String, ByVal FS As Integer, ByVal CurSize As Integer, ByVal Rate As Integer,
                              ByVal Bits As Integer, ByVal NumChannels As Integer)
        Dim qp As New FileStream(FileName, FileMode.Open, FileAccess.ReadWrite)

        Dim Is16Bit As Boolean = (Bits = 16)

        'For stupid INFO blocks
        Dim p As Integer = 0
        Dim TmpArr(4) As Byte
        Dim x As Integer = 0

        If FS <> 0 Then
            'This block seeks to an INFO block and writes zeros over it.
            For x = 0 To qp.Length - 1
                qp.Seek(x, 0)
                qp.Read(TmpArr, 0, 4)
                If TmpArr(0) = &H4C And TmpArr(1) = &H49 And TmpArr(2) = &H53 And TmpArr(3) = &H54 Then
                    qp.Seek(x, 0)
                    For z = x To qp.Length - 1
                        If Is16Bit Then
                            qp.WriteByte(0)
                        Else
                            qp.WriteByte(127)
                        End If
                    Next
                    Exit For
                End If
            Next
        End If

        'This block seeks to the fmt  block
        For x = 0 To qp.Length - 1
            qp.Seek(x, 0)
            qp.Read(TmpArr, 0, 4)
            If TmpArr(0) = &H66 And TmpArr(1) = &H6D And TmpArr(2) = &H74 And TmpArr(3) = &H20 Then
                Exit For
            End If
        Next

        'SAMPLERATE
        qp.Seek(12 + x, 0)

        qp.WriteByte(Rate And &HFF)
        qp.WriteByte((Rate >> 8) And &HFF)
        qp.WriteByte((Rate >> 16) And &HFF)
        qp.WriteByte((Rate >> 24) And &HFF)

        'BYTERATE
        If Is16Bit Then
            Rate *= 2
        End If

        Rate *= NumChannels

        qp.Seek(16 + x, 0)

        qp.WriteByte(Rate And &HFF)
        qp.WriteByte((Rate >> 8) And &HFF)
        qp.WriteByte((Rate >> 16) And &HFF)
        qp.WriteByte((Rate >> 24) And &HFF)

        If Not CurSize = 0 Then

            'CHUNKSIZE
            qp.Seek(4, 0)
            qp.WriteByte((FS + 36) And &HFF)
            qp.WriteByte(((FS + 36) >> 8) And &HFF)
            qp.WriteByte(((FS + 36) >> 16) And &HFF)
            qp.WriteByte(((FS + 36) >> 24) And &HFF)

            'This block seeks to the data block
            For x = 0 To qp.Length - 1
                qp.Seek(x, 0)
                qp.Read(TmpArr, 0, 4)
                If TmpArr(0) = &H64 And TmpArr(1) = &H61 And TmpArr(2) = &H74 And TmpArr(3) = &H61 Then
                    Exit For
                End If
            Next

            'SUBCHUNK2SIZE
            qp.Seek(4 + x, 0)
            qp.WriteByte(FS And &HFF)
            qp.WriteByte((FS >> 8) And &HFF)
            qp.WriteByte((FS >> 16) And &HFF)
            qp.WriteByte((FS >> 24) And &HFF)
            qp.Close()
            qp.Dispose()


            qp = New FileStream("~tmpx.wav", FileMode.Append, FileAccess.Write)
            For x = CurSize To FS
                If Is16Bit Then
                    qp.WriteByte(0)
                Else
                    qp.WriteByte(127)
                End If
            Next
        End If
        qp.Close()
        qp.Dispose()
    End Sub

    'The control function for opening a wave file.  This sets
    'all of the variables necessary and handles everything around
    'opening a wave file.

    Private Sub OpenWav(ByVal FilePath As String)
        GarbageCollect()
        Try
            File.Delete("~tmp.wav")
        Catch
        End Try
        StopSound()
        Dim isSmall As Boolean = False
        Dim SmallCutoff As Integer = 0
        Dim tmpHead As New WAVHeader(OpenFile(FilePath))
        If tmpHead.Header.IsWave = False Then
            MessageBox.Show("This is not a wave file.")
            Return
        End If
        If tmpHead.Format.AudioFormat = False Then
            MessageBox.Show("This is not a PCM-encoded file.")
            Return
        End If
        Dim rate As Integer = tmpHead.Format.SampleRate
        Dim s As Integer = 0
        If 33144 > rate Then
            s = rate
        Else
            s = 33144
        End If
        Dim ModNumber As Long = s / GCD(rate, 33144)
        While 1
            If ModNumber Mod 2 = 0 Or ModNumber Mod 3 = 0 Or rate = 33144 Then
                Exit While
            End If
            rate -= 1
            If 33144 > rate Then
                s = rate
            End If
            ModNumber = s / GCD(rate, 33144)
        End While

        Try
            File.Delete("~tmpx.wav")
        Catch
        End Try
        File.Copy(FilePath, "~tmpx.wav")

        'populates labels on the form

        BlankLabels()
        lblBits.Text += " " & tmpHead.Format.BitsPerSample.ToString
        If tmpHead.Format.NumChannels = 1 Then
            lblChannels.Text += " Mono"
        ElseIf tmpHead.Format.NumChannels = 2 Then
            lblChannels.Text += " Stereo"
        End If
        If tmpHead.Format.AudioFormat = True Then
            lblFormat.Text += " PCM"
        Else
            lblFormat.Text += " Compressed type"
        End If
        lblRate.Text += " " & tmpHead.Format.SampleRate.ToString
        lblSize.Text += " " & tmpHead.Header.FileSize.ToString
        lblName.Text += " " & Path.GetFileName(FilePath)




        Dim FS As Integer = 50000
        If tmpHead.Format.BitsPerSample = 16 Then
            FS *= 2
        End If
        If tmpHead.Format.NumChannels = 2 Then
            FS *= 2
        End If

        'if the file is so small that ssrc will crash or duplicate 
        'its contents, make silence/padding.  We need to keep the
        'wave the same for the padding, else SSRC will corrupt the
        'volumes slightly because the wave is only 6-bit.

        If tmpHead.Format.BitsPerSample = 24 Then
            MessageBox.Show("24-bit WAV files are unsupported.")
            Return
        End If
        Dim eefs As FileStream = File.Create("~tmp.wav")
        eefs.Close()
        eefs.Dispose()
        'If tmpHead.Header.FileSize < FS Then                   'we gots to modify it all regardless *sigh
        Dim tmpwav As New WAV(OpenFile("~tmpx.wav"))
        'isSmall = True
        'SmallCutoff = tmpwav.Data.Samples.Count
        'If tmpwav.Format.SampleRate = 48000 Then               'what the hell was this
        '    tmpwav.Format.SampleRate = 47999
        'End If

        'ok so ssrc sucks and will only convert stuff if it follows rate/(gcd(rate,33144) Mod 2 or 3
        'oh wow so it's actually <the lower of rate or 33144> / (gcd(rate, 33144) Mod 2 or 3
        'i hate this program and the fact that it doesn't say anything

        Dim tabl As New List(Of Integer)({1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6, 7, -7, 8, -8})
        Dim cx = 0
        Dim Rate2 As Integer = tmpwav.Format.SampleRate
        While isAMatch(Rate2) <> True And cx < 16
            Rate2 = tmpwav.Format.SampleRate + tabl(cx)
            cx += 1
        End While
        If cx = 16 Then
            MessageBox.Show("Please resample your .wav file to 33144hz in another program- not compatible with SSRC")
            Me.Close()
        End If
        tmpwav.Format.SampleRate = Rate2


        'SaveOrig("~tmpx.wav", tmpwav, tmpwav.Format.SampleRate, True)
        OverwriteWave("~tmpx.wav", 0, 0, tmpwav.Format.SampleRate, tmpHead.Format.BitsPerSample, tmpHead.Format.NumChannels)
        'Now that SSRC's twopass option is used, it's better quality for 6-bit waves... but still not good enough
        'to skip this step.  This keeps the wave in its original format while changing its data.
        'OverwriteWave("~tmpx.wav", FS, tmpwav.Data.Samples.Count * tmpwav.Format.NumChannels * _
        '(tmpwav.Format.BitsPerSample / 8), rate, tmpwav.Format.BitsPerSample, tmpwav.Format.NumChannels)

        Try
            SSRC("~tmpx.wav", "~tmp.wav", 33144, False)
        Catch
            MessageBox.Show("ssrc was not found.  Ssrc.exe should be in the ssrc directory along with this program.")
            Return
        End Try
        Try
            File.Delete("~tmp1.wav")
        Catch
        End Try
        'Else  'the wave is not too small, no modification necessary
        '    OverwriteWave("~tmpx.wav", 0, 0, rate, tmpHead.Format.BitsPerSample, tmpHead.Format.NumChannels)
        '    Try
        '        SSRC("~tmpx.wav", "~tmp.wav", 33144, True)
        '    Catch
        '        MessageBox.Show("ssrc was not found.  Ssrc.exe should be in the ssrc directory along with this program.")
        '        Return
        '    End Try
        '    Try
        '        File.Delete("~tmp1.wav")
        '    Catch
        '    End Try
        'End If
        Try
            File.Delete("~tmpx.wav")
        Catch
        End Try

        DisplayBox("Processing audio data...")
        curWav = WOpen("~tmp.wav") 'curwav is the open wave


        If curWav Is Nothing Then
            Return
        End If
        'If isSmall Then
        '    Dim p As New List(Of Integer)
        '    For x = 0 To (SmallCutoff / (rate / 33144)) - 1
        '        p.Add(curWav.Data.Samples(x))
        '    Next
        '    curWav.Data.Replace(p)
        'End If

        BckupWav = curWav.Copy      'the wave that is loaded on "revert"
        File.Delete("~tmp.wav")


        counter = 1     '"3" is the highest depth  .. this is for zoom
        curDMC = Nothing
        CurPreview = Nothing
        VisualizeDMC()
        Redraw()
        StopSound()
        RevForm()
        DelBox()
    End Sub

End Class
