Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security

Partial Class MainForm
    'Saves a WAV to a file directly, no DMC conversion.  This is for SSRC/resampling use.
    Private Sub SaveOrig(ByVal FilePath As String, ByRef Wave As WAV, ByVal Rate As Integer, ByVal Conv As Boolean)    'writes a wave file straight up
        'creates a filestream and returns if no file selected.
        Dim fs As FileStream
        Try
            fs = New FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite)
        Catch ex As Exception
            MessageBox.Show("Could not find file, or no file was selected.")
            Return
        End Try
        'Sets the rate, and exports the wave to a list of bytes
        Wave.Format.SampleRate = Rate
        Dim OutputWave As List(Of Byte) = Wave.ExportWave(Conv, 0)

        'Writes the wave to the file.
        For Each PCM As Byte In OutputWave
            fs.WriteByte(PCM)
        Next

        'Cleanup
        fs.Close()
        fs.Dispose()
    End Sub

    Private Sub Save(ByVal FilePath As String, ByRef Wave As WAV, ByVal Rate As Integer)    'writes a dpcm approximation of a wave
        Dim z As New WAV(Rate, NESResample(WAVtoDMCWAV(Wave)), 8)

        'Makes a filestream of filepath (read-write)
        Dim fs As FileStream
        Try
            fs = New FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite)
        Catch ex As Exception
            MessageBox.Show("Could not find file, or no file was selected.")
            Return
        End Try

        'OutputWave / List of Byte is our file, stored in Memory
        Dim OutputWave As List(Of Byte) = z.ExportWave(False, 0)

        'Writes the list of bytes to the file
        For Each dc As Byte In OutputWave
            fs.WriteByte(dc)
        Next

        'Cleanup
        fs.Close()
        fs.Dispose()
    End Sub

    Private Sub SaveDMC(ByVal FilePath As String)
        Dim tmpData As New List(Of Integer)
        For Each item In curWav.Data.Samples
            tmpData.Add(item)
        Next
        Volumize(tmpData, numVolume.Value)
        Pitchize(tmpData)
        Dim mwav As New WAV(33144, tmpData, 6)
        '"Super Crush" - takes the volumes and inverts it around the middle line (31.5)
        'This must be done in this phase, because it is a modification to the
        ' actual wave file, not the DMC data.
        If CrushAmount = 20 Then

            For d = 0 To mwav.Data.Samples.Count - 1
                If mwav.Data.Samples(d) > 31 Then
                    mwav.Data.Samples(d) -= 32
                Else
                    mwav.Data.Samples(d) += 32
                End If
            Next
        End If
        curDMC = New DMC(mwav)          'convert WAV to new DMC
        curDMC.Crush(CrushAmount)       'Bitcrush the data (for values 1-19)

        'Write the file
        Dim fs As FileStream
        Try
            fs = New FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite)
        Catch ex As Exception
            MessageBox.Show("Could not find file, or no file was selected.")
            Return
        End Try

        'Curdmc.data is the HEX representation of the ones and zeros
        ' which is what the DMC format is.
        For Each tmpByte In curDMC.Data
            fs.WriteByte(tmpByte)
        Next

        'Cleanup
        fs.Close()
        fs.Dispose()
    End Sub

    Private Sub SavedmcToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavedmcToolStripMenuItem.Click, tsbSaveDMC.Click
        'If there is no curwav, nothing has been loaded--short circuit
        If curWav Is Nothing Then
            MessageBox.Show("No .wav has been loaded!")
            Return
        End If
        'Set up sfd (Save File Dialog)
        sfd.Filter = "Delta PCM File(*.dmc)|*.dmc"
        sfd.FileName = lblName.Text.Substring(6, lblName.Text.Length - 10) & ".dmc"
        sfd.AddExtension = True
        sfd.RestoreDirectory = True
        sfd.FilterIndex = 0
        If sfd.ShowDialog() = 1 Then
            SaveDMC(sfd.FileName)   'Save the DMC to a file
        End If
    End Sub

    Private Sub SavewavToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavewavToolStripMenuItem.Click, tsbSaveWav.Click
        'Short circuit if there is no curWav (no file has been loaded)
        If curWav Is Nothing Then
            MessageBox.Show("No .wav has been loaded!")
            Return
        End If
        'Set up sfd (Save File Dialog)
        sfd.Filter = "Wave Soundfile(*.wav)|*.wav"
        sfd.FileName = lblName.Text.Substring(6, lblName.Text.Length - 10) & ".dmc.wav"
        sfd.AddExtension = True
        sfd.FilterIndex = 0
        sfd.RestoreDirectory = True
        If sfd.ShowDialog() = 1 Then
            Save(sfd.FileName, curWav, 33144)   'Save a wav @ filename
        End If


    End Sub

    'Makes a copy of the incoming WAV, then creates a WAV recording of a DMC
    Function WAVtoDMCWAV(ByVal c As WAV) As List(Of Integer)
        Dim tmpData As New List(Of Integer)
        For Each item In c.Data.Samples
            tmpData.Add(item)
        Next
        Volumize(tmpData, numVolume.Value)  'Change the volume of the data
        Pitchize(tmpData)                   'Change the quality/rate of the data
        Dim mwav As New WAV(QualRate, tmpData, 6)
        If CrushAmount = 20 Then
            'super crusher

            For d = 0 To mwav.Data.Samples.Count - 1
                If mwav.Data.Samples(d) > 31 Then
                    mwav.Data.Samples(d) -= 32
                Else
                    mwav.Data.Samples(d) += 32
                End If
            Next
        End If
        'Make a DMC of the WAV, then crush (1-19)   
        curDMC = New DMC(mwav)
        curDMC.Crush(CrushAmount)
        'This code converts the DMC data to 8-bit PCM data.
        ' Even though the format is 8-bit, the actual volume
        ' steps are all 6-bit, hence the *=4.
        Dim g As New List(Of Integer)
        g = curDMC.ToWaveData(numDelta.Value)
        For x = 0 To g.Count - 1
            g(x) *= 4
        Next
        Return g    '"g" is our final wave data.
    End Function

End Class
