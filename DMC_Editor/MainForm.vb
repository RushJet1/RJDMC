Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security

Public Class MainForm
    Public Declare Function SetForegroundWindow Lib "user32.dll" (ByVal hwnd As Integer) As Integer
    Public Declare Auto Function FindWindow Lib "user32.dll" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
#Region "Form Variables"
    Private curWav As WAV                   'The current editable WAV in memory
    Private BckupWav As WAV                 'The current WAV in memory, backed up for reverting edits
    Private curDMC As DMC                   'The current DMC in memory, represented as bits and bytes
    Private Pref As Batch                   'The Preferences box that appears when the user goes to edit->preferences
    Private ar As AboutWrapper              '"ar" is the AboutBox wrapper class (pop up box notifications)
    Private tr As Threading.Thread          '"tr" is the thread the AboutBox wrapper class is run on
    Private curDirectory As String
#End Region

#Region "Graphics Variables"
    Private g As Graphics                   '"g" is the actual graphics hook for the program
    Private pn As Pen
    Private tmpWavData As List(Of Integer)
    Private counter As Double
    Private mySize As Size
#End Region

#Region "ScrollVariables"
    'ScrollXPrev variables show where the sliders SHOULD be if they only increased or decreased by one value.
    ' This prevents the system setting from taking control and only moves them one value per scroll of the mouse
    ' wheel.
    Private ChangeScroll As Boolean         'This is for the sliders - determines if it should be scrolled or not depending
    ' on the mouse's postion.
    Private Scroll1Prev As Integer
    Private Scroll2Prev As Integer
    Private Scroll3Prev As Integer
#End Region

#Region "Preview Variables"
    Private PrevRate As Integer             'The rate set by the slider
    Private PrevChange As Boolean         'This is a flag saying the pitch changed.  It's used to re-render
    ' the sample in several places.
    Private CrushAmount As Integer          'The amount to crush the data (as set by the slider)
    Private QualRate As Integer             'The Rate / Quality of the file (as set by the slider)
    Private CurPreview As WAV
    Private CurDMCWav As List(Of Integer)
    Private thr As System.Threading.Thread
    Private AP As AudPlayer
#End Region

#Region "Edit Variables"
    Private MouseIsDown As Boolean          'This is set by a mousedown event and enables dragging the mouse to make selections
    Private BoxStart As Integer             'Beginning point of the drawn box for selections
    Private BoxEnd As Integer               'End point of the drawn box for selections
    Private Rect As Rectangle               'The actual rectangle that is drawn for selections
    Private Line As Integer                 'The actual line drawn for insertion points
    Private BackupImage As Bitmap           'The "backup" image created for when the user deselects an area
    Private Moved As Boolean                'This is set when the mouse has moved over the box
    Private vBegin As Integer               'Beginning of selection
    Private vEnd As Integer                 'End of selection
    Private CopyPasta As List(Of Integer)   'The data to be pasted
#End Region

    'Help->About messagebox
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        MessageBox.Show("RJDMC" & vbNewLine & "v1.53 by Tadd Nuznov (RushJet1)" & vbNewLine & "Mar 5, 2023")
    End Sub

    'This allows drag 'n drop of files.
    Private Sub zDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    'Exits the program when the user clicks file->exit
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    'Closes the current wave file.  Garbage collects and blanks the labels.
    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        GarbageCollect()
        BlankLabels()
        Me.Refresh()
    End Sub

    'Blanks the labels on the form.
    Sub BlankLabels()
        lblBits.Text = "Bits:"
        lblChannels.Text = "Channels:"
        lblFormat.Text = "Format:"
        lblName.Text = "Name:"
        lblRate.Text = "Rate:"
        lblSize.Text = "Size:"
    End Sub

    'Form load events - all variables are initialized.
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        curDirectory = My.Application.Info.DirectoryPath
        Directory.SetCurrentDirectory(curDirectory)
        PrevRate = 33144                            '33144 is the default quality/rate of the NES
        QualRate = 33144
        PrevChange = False                        'Pitch has not changed, so don't redraw the DMC wave
        Scroll1Prev = trkQuality.Maximum            'Scroll variables are initialized here.  Quality, Pitch are
        Scroll2Prev = trkPitch.Maximum              ' set to their maximum values, wihle Crush is set to its
        trkQuality.Value = trkQuality.Maximum       ' minimum value (no crush).
        trkPitch.Value = trkPitch.Maximum
        trkQualityMask.Value = trkQuality.Maximum
        trkPitchMask.Value = trkPitch.Maximum
        trkQualityMask.BringToFront()
        trkPitchMask.BringToFront()
        ChangeScroll = True                         'This shows that the scrolling CAN change on the controls.
        mySize = Me.Size                            'Storing current form size to mySize
        Pref = New Batch                   'Preferences is another form that appears when it is selected
        Pref.radManual.Checked = True
        Pref.grpWavRate.Enabled = False
        Pref.cmbRate.SelectedIndex = Pref.cmbRate.Items.Count - 1
        MouseIsDown = False                         'On program start, mouse isn't being held down
        BoxStart = -1                                'No selection has been made.
        BoxEnd = -1
        Dim z As New Media.SoundPlayer(My.Resources.silz)
        z.Play()
        z.Play()
        z.Play()
        z.Play()
        z.Play()
        z.Dispose()
        numDelta.BringToFront()
        numTilt.BringToFront()
        numVolume.BringToFront()
    End Sub

    Private Sub Me_shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        If My.Application.CommandLineArgs.Count <> 0 Then
            Dim filpth As String = My.Application.CommandLineArgs(0)
            If Path.GetExtension(filpth).ToLower = ".wav" Then
                OpenWav(filpth)
            ElseIf Path.GetExtension(filpth).ToLower = ".dmc" Then
                OpenDMC(filpth)
            Else
                MessageBox.Show("Unsupported Format: " & Path.GetExtension(filpth))
            End If
        End If
    End Sub


    'Shows the preferences form when the toolstrip menu item is clicked
    Private Sub PreferencesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Pref.ShowDialog()
    End Sub

    'Changes the values for the label and Crush Amount when trkBitCrush scrolls
    Private Sub trkBitCrush_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trkBitCrush.ValueChanged
        If Not ChangeScroll Then
            Return
        End If
        CrushAmount = trkBitCrush.Value
        lblBitCrush.Text = "Bit Crush: " & trkBitCrush.Value
        UpdateViewQuick()
    End Sub

    'Updates the view if the sample count is less than 100,000
    Private Sub UpdateViewQuick()
        If curWav Is Nothing Then
            Return
        End If
        If curWav.Data.Samples.Count < 100000 Then
            CurPreview = Nothing
            VisualizeDMC()
            Redraw()
        Else
            PrevChange = True
        End If
    End Sub

    'Volume has changed, so it sets a flag telling it to re-render the preview.
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numVolume.ValueChanged
        PrevChange = True
    End Sub

    'This makes sure that numVolume keeps its focus on keyUp events
    Private Sub numVol_Focus() Handles numVolume.KeyUp
        numVolume.Focus()
    End Sub

    'Reverts the form's settings to when the file was loaded.
    Private Sub RevForm()
        numVolume.Value = 100
        trkCrushMask.Value = 0
        trkBitCrush.Value = 0
        trkPitchMask.Value = 15
        trkPitch.Value = 15
        trkQualityMask.Value = 15
        trkQuality.Value = 15
        Scroll1Prev = 15
        Scroll2Prev = 15
        Scroll3Prev = 0
        PitchView()
        QualView()
    End Sub

    'Reverts the wave by copying the backup wave to curWav, then revisualizing
    Private Sub Revert() Handles btnRevert.Click, RevertToolStripMenuItem.Click
        If curWav Is Nothing Then
            Return
        End If
        RevForm()
        curWav = BckupWav.Copy
        CurPreview = Nothing
        VisualizeDMC()
        Redraw()
    End Sub

    'This creates a display box for notifications.
    Sub DisplayBox(ByVal Text As String)
        Me.Refresh()            'refreshes the main form (avoid having garbage data on it)
        ar = New AboutWrapper   'aboutWrapper is a class for threading/variable passing
        ar.Text = Text
        'ar.loc will now equal the midle of the form ... regardless of form's location or size
        ar.Loc = New Point(Me.Location.X + (Me.Width / 2) - (130 / 2), Me.Location.Y + (Me.Height / 2) - (14 / 2))
        tr = New Threading.Thread(AddressOf ar.Execute)     'thread execution point
        tr.SetApartmentState(Threading.ApartmentState.STA)  'this is necessary to avoid an error (windows threading)
        tr.Start()              'start the thread
    End Sub

    'This deletes the threaded popup box.
    Sub DelBox()
        'If the box was created and we delete it too early, there is an error,
        ' so we need to sleep for 100ms for small files.
        System.Threading.Thread.Sleep(100)
        Invoker(ar.Box)     'This clears the box in a threaded-safe way.
        Me.Refresh()        'Refreshes the main form to avoid the box staying there
        'tr.Abort()          'Aborts the thread
        tr.Join()
    End Sub

    '"Invokr" is required for this to work
    Delegate Sub Invokr(ByVal Box As AboutBoxNotify)

    'Deletes the box if it is able to be deleted (is not being used by the other thread).
    'I specifically created the box IN the other thread so it wouldn't freeze on operations
    'during this main thread.
    Sub Invoker(ByVal Box As AboutBoxNotify)
        If Box.InvokeRequired Then
            Box.Invoke(New Invokr(AddressOf Invoker), Box)
        Else
            Box.Hide()          'Hide box
            Box.Dispose()       'Free memory taken by the box
        End If
    End Sub

    Private Function isAMatch(ByVal rate As Integer) As Boolean
        Dim ratex As Integer = 0
        If rate < 33144 Then
            ratex = rate
        Else
            ratex = 33144
        End If
        If (ratex / GCD(rate, 33144)) Mod 3 <> 0 And (ratex / GCD(rate, 33144)) Mod 2 <> 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    'Creates a new process and runs SSRC.
    Private Sub SSRC(ByVal FilePath As String, ByVal TmpPath As String, ByVal Rate As Integer, ByVal DispBox As Boolean)
        Directory.SetCurrentDirectory(curDirectory)
        If DispBox Then
            DisplayBox("Converting sample rate...")
        End If
        Dim SSRCProc As New Process
        'Sets up the arguments for SSRC
        '--twopass renders the numbers as floating point for greater accuracy
        '--profile fast means slightly worse quality but much better than one-pass
        '--rate (Rate) converts to the input rate for this function

        System.IO.File.WriteAllBytes("ssrc.exe", My.Resources.ssrc)

        SSRCProc.StartInfo.Arguments = "--twopass --rate " & Rate & " --tmpfile ""tt.wav"" """ & FilePath & """ """ & TmpPath & """"
        SSRCProc.StartInfo.FileName = "ssrc.exe"
        SSRCProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden  'Hide the console window
        SSRCProc.Start()        'Start the process
        SSRCProc.WaitForExit()  'Program waits until it's done
        If DispBox Then
            DelBox()
        End If
        SSRCProc.Dispose()          'garbage collection
        System.IO.File.Delete("ssrc.exe")
    End Sub

    'Intercepts all button presses before the forms do.  This is for space playback, delete, etc

    'This function intercepts all button presses for all controls
    Private Sub Intercepter(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Space Then
                If e.Control Then
                    StopSound()
                ElseIf e.Shift Then
                    tsbSelPreview_Click()
                Else
                    AudioPreview(curWav, True)
                    e.SuppressKeyPress = True
                End If
            ElseIf e.KeyCode = Keys.Delete Then             'Delete
                DeleteChunk()
                'Delete is not suppressed because it is useful for the numericupdown box
            ElseIf e.Control And e.KeyCode = Keys.C Then    'CTRL+C
                CopyChunk()
                e.SuppressKeyPress = True
            ElseIf e.Control And e.KeyCode = Keys.V Then    'CTRL+V
                PasteChunk(0)
                e.SuppressKeyPress = True
            ElseIf e.Control And e.KeyCode = Keys.X Then    'CTRL+X
                CutChunk()
                e.SuppressKeyPress = True
            ElseIf e.Control And e.KeyCode = Keys.T Then    'CTRL+T
                TrimChunk()
                e.SuppressKeyPress = True
            ElseIf e.Control And e.KeyCode = Keys.Z Then    'CTRL+Z
                Revert()
                e.SuppressKeyPress = True
            ElseIf e.Control And e.KeyCode = Keys.A Then    'CTRL+A
                SelectAll()
                e.SuppressKeyPress = True
            ElseIf e.Control And e.KeyCode = Keys.I Then    'CTRL+I
                Reverse()
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            MessageBox.Show("The sample is too small.")
        End Try
    End Sub

    'Deletes the selected area
    Private Sub DeleteChunk() Handles DeleteToolStripMenuItem.Click, DeleteToolStripMenuItem1.Click
        If (BoxStart = 0 And BoxEnd = 0) Or BoxStart = -1 Then
            Return
        End If
        Dim BeginDel As Integer = BoxStart
        Dim EndDel As Integer = BoxEnd
        SetSelection(BeginDel, EndDel)  'Sets BeginDel and EndDel to the proper sample values

        If BeginDel = EndDel Then
            Return
        End If

        If EndDel > curWav.Data.Samples.Count Then
            EndDel = curWav.Data.Samples.Count - 1
        End If

        curWav.Data.Samples.RemoveRange(BeginDel, EndDel - BeginDel)

        'There is no box now so set start to -1, end to 0
        Line = BoxStart
        BoxStart = -1
        BoxEnd = 0
        CurPreview = Nothing
        Rect = Nothing
        '(If everything was deleted)
        If curWav.Data.Samples.Count = 0 Then
            curWav = Nothing
        End If
        'Now re-draw/re-render the preview
        VisualizeDMC()
        Redraw()
        PrevChange = True
    End Sub

    'This will copy selection WITH effects intact.
    Private Sub CopyChunk() Handles CopyToolStripMenuItem.Click, CopyToolStripMenuItem1.Click
        If BoxStart = -1 Then
            Return
        End If
        CopyPasta = New List(Of Integer)
        Dim BeginCop As Integer = BoxStart
        Dim EndCop As Integer = BoxEnd
        SetSelection(BeginCop, EndCop)  'Set to begin/end values for this selection
        If BeginCop > EndCop Then
            For x = EndCop To BeginCop - 1
                CopyPasta.Add(curWav.Data.Samples(x))
            Next
        ElseIf BeginCop < EndCop Then
            For x = BeginCop To EndCop - 1
                CopyPasta.Add(curWav.Data.Samples(x))
            Next
        Else
            Return
        End If

    End Sub

    Private Sub PasteToolStripClick() Handles PasteToolStripMenuItem.Click, PasteToolStripMenuItem1.Click
        PasteChunk(0)
    End Sub

    'Pastes a chunk that was previously cut or copied at the current cursor point.
    Private Sub PasteChunk(ByVal z As Integer)
        If (Not BoxStart = -1) Or (CopyPasta Is Nothing) Then
            Return
        End If
        If curWav Is Nothing Then
            curWav = New WAV(33144, CopyPasta, 8)
            Return
        End If
        Dim SampStart As Integer = Line
        If z <> 0 Then
            SampStart = z
        Else
            SetSelection(SampStart, Nothing)
        End If
        curWav.Data.Samples.InsertRange(SampStart, CopyPasta)
        CurPreview = Nothing
        Rect = Nothing
        If counter = 1 Then
            Line = (SampStart / curWav.Data.Samples.Count) * PictureBox1.Size.Width
            Line += (CopyPasta.Count / curWav.Data.Samples.Count) * PictureBox1.Size.Width
        End If
        VisualizeDMC()
        Redraw()
        PrevChange = True
    End Sub

    'Cuts a chunk of the wave.  Copies then deletes, essentially.
    Private Sub CutChunk() Handles CutToolStripMenuItem.Click, CutToolStripMenuItem1.Click
        If (BoxStart = 0 And BoxEnd = 0) Or BoxStart = -1 Then
            Return
        End If
        CopyChunk()
        DeleteChunk()
    End Sub

    'Selects the entire wave.
    Private Sub SelectAll() Handles SelectALlToolStripMenuItem.Click
        If curWav Is Nothing Then
            Return
        End If
        PictureBox1.Image = BackupImage.Clone
        Rect = New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height)
        BoxStart = 0
        BoxEnd = PictureBox1.Width
        Dim brsh As New SolidBrush(Color.FromArgb(50, 255, 255, 255))
        g = Graphics.FromImage(PictureBox1.Image)
        g.FillRectangle(brsh, Rect)
        PictureBox1.Refresh()
    End Sub

    '"Trims" the wave so that only the selected area is left.
    Private Sub TrimChunk() Handles TrimToolStripMenuItem.Click, TrimToolStripMenuItem1.Click
        If BoxStart = -1 Then
            Return
        End If
        CopyChunk()
        curWav.Data.Replace(CopyPasta)
        CurPreview = Nothing
        Rect = Nothing
        VisualizeDMC()
        Redraw()
        PrevChange = True
    End Sub

    'Sets PrevChange to true so it will re-render the preview
    Private Sub numDelta_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numDelta.ValueChanged
        PrevChange = True
    End Sub

    'Reverses the waveform.  This is selection-dependent.
    Private Sub Reverse() Handles ReverseToolStripMenuItem.Click
        If BoxStart = -1 Then
            Return
        End If
        Dim p As New List(Of Integer)
        Line = BoxStart
        CutChunk()
        For x = CopyPasta.Count - 1 To 0 Step -1
            p.Add(CopyPasta(x))   'read samples backwards
        Next
        CopyPasta = p.ToList
        BoxStart = -1
        PasteChunk(0)

        'redraw wave and make new preview
        CurPreview = Nothing
        Rect = Nothing
        VisualizeDMC()
        Redraw()
        PrevChange = True
    End Sub

    'Previews the selection 
    Private Sub tsbSelPreview_Click() Handles tsbSelPreview.Click
        If (BoxStart = BoxEnd) Or (BoxStart = -1) Then
            Return
        End If
        Dim p As New List(Of Integer)
        Dim tmpStart As Integer = BoxStart
        Dim tmpEnd As Integer = BoxEnd
        SetSelection(tmpStart, tmpEnd)
        'Dim z As Double = curWav.Data.Samples.Count / PictureBox1.Size.Width
        For x = tmpStart To tmpEnd - 1
            p.Add(curWav.Data.Samples(x))
        Next
        AudioPreview(New WAV(33144, p, 6), False) 'false=do not update graphics
    End Sub

    Private Sub ZoomIntoSelectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoomIntoSelectionToolStripMenuItem.Click, ZoomToSelectionToolStripMenuItem.Click
        If (BoxStart = BoxEnd) Or (BoxStart = -1) Or counter >= CurDMCWav.Count / 10 Then
            Return
        End If

        Dim dmcSampleCount As Integer = curWav.Data.Samples.Count

        'If counter is 1, then the wave is zoomed all the way out.  If it's not, then 
        ' we need to calculate where this selection is taking place.
        If counter = 1 Then
            vBegin = BoxStart * dmcSampleCount / PictureBox1.Size.Width
            vEnd = BoxEnd * dmcSampleCount / PictureBox1.Size.Width
        Else
            Dim size As Integer = vEnd - vBegin
            Dim vb2 As Integer = vBegin
            vBegin = vb2 + (BoxStart * size / PictureBox1.Size.Width)
            vEnd = vb2 + (BoxEnd * size / PictureBox1.Size.Width)
        End If

        If vEnd >= dmcSampleCount Then
            vEnd = dmcSampleCount - 1
        End If

        'This makes counter a similar value to what it would be if the user had scrolled there manually.
        While 1
            counter *= 1.2
            'If the selected area is bigger than the area that (total samples) / counter yields, then stop!
            If vEnd - vBegin >= dmcSampleCount / counter Then
                Exit While
            End If
        End While

        tmpWavData = Nothing
        GC.Collect()
        If counter = 1 Then
            Dim g(CurDMCWav.Count) As Integer
            CurDMCWav.CopyTo(g)
            tmpWavData = g.ToList
            vBegin = 0
            vEnd = CurDMCWav.Count
        Else
            tmpWavData = New List(Of Integer)
            For x = vBegin To vEnd - 1 Step 1 + (30 / counter)
                tmpWavData.Add(CurDMCWav(x))
            Next x
        End If
        Rect = Nothing
        BoxStart = -1
        Redraw()
    End Sub

    'Writes corresponding Sample value ranges to the incoming integers
    ' based on the original box coordinates
    Private Sub SetSelection(ByRef tmpStart As Integer, ByRef tmpEnd As Integer)
        If counter = 1 Then
            tmpStart = 1 + (tmpStart / PictureBox1.Size.Width * curWav.Data.Samples.Count)
            tmpEnd = 1 + (tmpEnd / PictureBox1.Size.Width * curWav.Data.Samples.Count)
        Else
            Dim size As Integer = vEnd - vBegin
            tmpStart = vBegin + (tmpStart / PictureBox1.Size.Width * size)
            tmpEnd = vBegin + (tmpEnd / PictureBox1.Size.Width * size)
        End If
        If tmpEnd >= curWav.Data.Samples.Count Then
            tmpEnd = curWav.Data.Samples.Count - 1
        End If
    End Sub

    'Code used to cut, then operate, then paste.  However, that means copying and 
    ' deleting, then insertingAt a location, which is costly on slow systems.  Instead,
    ' this now copies, then operates, then assigns the new values back to the old list,
    ' bringing it down to 2 copies instead of 2 copies, a delete, and an insertAt.
    Private Sub ChangeVolumeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeVolumeToolStripMenuItem.Click, ChangeToolStripMenuItem.Click
        If BoxStart = -1 Then
            Return
        End If
        Dim z As Integer = BoxStart
        SetSelection(z, 0)
        VolBox.ShowDialog()
        CopyChunk()
        If VolBox.numVolume.Value = Nothing Then
            Return
        End If
        Volumize(CopyPasta, VolBox.numVolume.Value)
        For x = 0 To CopyPasta.Count - 1
            curWav.Data.Samples(z + x) = CopyPasta(x)
        Next
        CurPreview = Nothing
        VisualizeDMC()
        Redraw()
        PrevChange = True
    End Sub

    Private Sub FadeVolumeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FadeVolumeToolStripMenuItem.Click, FadeToolStripMenuItem.Click
        If BoxStart = -1 Then
            Return
        End If
        Dim z As Integer = BoxStart
        SetSelection(z, 0)          'Sets "z" to the proper sample location at the beginning of the rect
        Fade.ShowDialog()           'Show fade box dialog
        CopyChunk()                  'Cuts the highlighted area
        If VolBox.numVolume.Value = Nothing Then
            Return
        End If
        FadeOperation(CopyPasta, Fade.numMinVol.Value, Fade.radFadeOut.Checked) 'Fades the cut data
        For x = 0 To CopyPasta.Count - 1
            curWav.Data.Samples(z + x) = CopyPasta(x)
        Next
        CurPreview = Nothing
        'Re-draw data
        VisualizeDMC()
        Redraw()
        PrevChange = True
    End Sub

    Sub FadeOperation(ByRef v As List(Of Integer), ByVal vol As Double, ByVal fadeOut As Boolean)
        If BoxStart = -1 Then
            Return
        End If
        vol /= 100                  'more efficient to do this once instead of every loop
        Dim tmp As Double = 0.0     'more efficient to dim this before the loop

        For x = 0 To v.Count - 1
            tmp = x / v.Count       'for efficiency and visibility, not calculating this every time it's used
            v(x) -= 31
            'Basically, this works like the other volume function, except that the fraction (tmp)
            ' grows over the length of the list of integers.  Fading out means inverting this (1-tmp)
            ' so it shrinks instead.  Adding (vol*tmp) means that, as we reach the end of the list, 
            ' tmp grows closer to 1, and therefore vol gains more weight.  This makes the end volume
            ' be what the user selected with a smooth ramp from 100% volume down to or up from it.
            If fadeOut Then
                v(x) *= (1 - tmp) + (vol * tmp)
            Else
                v(x) *= tmp + ((vol * (1 - tmp)))
            End If
            v(x) += 31
            If v(x) > 63 Then
                v(x) = 63
            ElseIf v(x) < 0 Then
                v(x) = 0
            End If
        Next
    End Sub

    Private Sub btnTilt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTilt.Click
        If BoxStart = -1 Then
            Return
        End If
        Dim tStart As Integer = BoxStart
        Dim tEnd As Integer = BoxEnd
        SetSelection(tStart, tEnd)                                              'find beginning and end of selection




        Dim g As New List(Of Integer)
        If curWav.Data.Samples.Count > CurPreview.Data.Samples.Count Then       'a selection was previewed
            curDMC = New DMC(curWav)                                            're-build DMC from whole wave to avoid issues
        End If

        curDMC.Tilt(tStart, tEnd, numTilt.Value)
        g = curDMC.ToWaveData(numDelta.Value)

        curWav = New WAV(33144, g, 8)                              'this code basically creates a new curwav from our edited dmc data
        '2025 edit: since we are not resampling, 
        'we don't NESResample this curwav because it will
        'cause hilarious side-effects. Duh!

        For x = 0 To g.Count - 1
            g(x) *= 4
        Next
        CurPreview = New WAV(33144, NESResample(g), 8)
        PrevChange = True
        VisualizeDMC()
        Redraw()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Focus()
    End Sub

    Private Sub trkPitchMask_Scroll(sender As Object, e As EventArgs) Handles trkPitchMask.MouseEnter
        trkPitchMask.Focus()
    End Sub

    Private Sub trkQualityMask_Scroll(sender As Object, e As EventArgs) Handles trkQualityMask.MouseEnter
        trkQualityMask.Focus()
    End Sub

    Private Sub trkCrushMask_Scroll(sender As Object, e As EventArgs) Handles trkCrushMask.MouseEnter
        trkCrushMask.Focus()
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs)  '.  Sounds bad.
        If curWav Is Nothing Then
            Return
        End If

        curDMC.Filter()
        Dim g As New List(Of Integer)
        g = curDMC.ToWaveData(numDelta.Value)

        curWav = New WAV(33144, NESResample(g), 8)                              'this code basically creates a new curwav from our edited dmc data
        For x = 0 To g.Count - 1
            g(x) *= 4
        Next
        CurPreview = New WAV(33144, NESResample(g), 8)
        PrevChange = True
        VisualizeDMC()
        Redraw()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click, SaveSplitdmcToolStripMenuItem.Click
        If curDMC Is Nothing Then
            Return
        End If
        Form1.curSize = curDMC.Samples.Length
        Form1.ShowDialog()
        If Form1.Choice = True Then
            curDMC.WriteSegmentedTxt(Form1.TextBox1.Text, Form1.NumericUpDown1.Value)
        End If

    End Sub

    Private Sub AssociateDMCFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssociateDMCFilesToolStripMenuItem.Click
        Dim ans As DialogResult = MessageBox.Show("Right click a DMC file in Windows explorer and open with RJDMC.  If you want it to open DMC files by default, click ""Always use this app to open .dmc files.""")

        'If ans = DialogResult.Yes Then
        '    Dim appData As String = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)
        '    If (Not System.IO.Directory.Exists(appData & "\RJDMC")) Then
        '        System.IO.Directory.CreateDirectory(appData & "\RJDMC")
        '    End If
        '    File.WriteAllBytes(appData & "\RJDMC\dmcl.ico", My.Resources.dmcl)
        '    My.Computer.Registry.ClassesRoot.CreateSubKey(".dmc").SetValue("", ".dmc", Microsoft.Win32.RegistryValueKind.String)
        '    My.Computer.Registry.ClassesRoot.CreateSubKey(".dmc\shell\open\command").SetValue("", Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
        '    My.Computer.Registry.ClassesRoot.CreateSubKey(".dmc\DefaultIcon").SetValue("", appData & "\RJDMC\dmcl.ico", Microsoft.Win32.RegistryValueKind.ExpandString)
        '    Try
        '        My.Computer.Registry.ClassesRoot.DeleteSubKeyTree("dmc_auto_file")
        '    Catch ex As Exception

        '    End Try
        'End If
    End Sub

End Class
