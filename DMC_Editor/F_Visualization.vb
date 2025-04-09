Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security

Partial Class MainForm
    'Mouse has no intercept functionality built-in.
    'Takes mousewheel arguments from any origin, then suppresses them if the mouse is over the display.
    Private Sub MW_Change_Zoom(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles Me.MouseWheel, trkQuality.MouseWheel, trkPitch.MouseWheel, trkBitCrush.MouseWheel
        'If the mouse has scrolled in a direction and any of the trackbars' values do not match their counterpart's value, 
        ' then bring the counterpart to the front to hide the changes that will go on.
        If trkQuality.Focused And ((e.Delta > 119 And trkQuality.Value < trkQuality.Maximum) Or (e.Delta < -119 And trkQuality.Value > trkQuality.Minimum)) Then
            trkQualityMask.BringToFront()
        End If
        If trkPitch.Focused And ((e.Delta > 119 And trkPitch.Value < trkPitch.Maximum) Or (e.Delta < -119 And trkPitch.Value > trkPitch.Minimum)) Then
            trkPitchMask.BringToFront()
        End If
        If trkBitCrush.Focused And ((e.Delta > 119 And trkBitCrush.Value < trkBitCrush.Maximum) Or (e.Delta < -119 And trkBitCrush.Value > trkBitCrush.Minimum)) Then
            trkCrushMask.BringToFront()
        End If
        If CurDMCWav Is Nothing Then
            Return
        End If
        Dim ex As Integer = 3           'x and y location offsets for window
        Dim why As Integer = 20

        'If the mouse is over the picturebox window
        If MousePosition.X > PictureBox1.Location.X + Me.Location.X + ex And MousePosition.X < PictureBox1.Location.X + PictureBox1.Size.Width + Me.Location.X + ex _
        And MousePosition.Y > PictureBox1.Location.Y + Me.Location.Y + why And MousePosition.Y < PictureBox1.Location.Y + PictureBox1.Size.Height + Me.Location.Y + why Then
            ChangeScroll = False                                'set flag to revert scrolling the trackbars
            If e.Delta < -119 Then                              '-120 is a "scroll out/down" mousewheel change
                If counter > 1 Then                             'if User is zoomed in
                    counter /= 1.2
                    Rect = Nothing                              'Get rid of selection area
                    TmpWavFix(PictureBox1.PointToClient(Cursor.Position).X, True)
                Else
                    Return
                End If
            ElseIf e.Delta > 119 Then                           '120 = scroll up
                If counter < CurDMCWav.Count / 10 Then          'if User is not zoomed all the way in
                    counter *= 1.2
                    Rect = Nothing                              'Get rid of selection area
                    TmpWavFix(PictureBox1.PointToClient(Cursor.Position).X, False)
                Else
                    Return
                End If
            End If
            Redraw()
        End If
    End Sub

    'This function sets tmpWavData, which is the set of values used to draw the screen.  It locates the correct values
    ' based on the previous zoom window.  vBegin and vEnd are changed to reflect these new values.
    Private Sub TmpWavFix(ByVal MouseX As Integer, ByVal Back As Boolean)
        '"MouseX" is the mouse's x-position.  "Back" Is whether or not the mouse is scrolling "out."
        If vBegin = 0 And vEnd = 0 Then
            vEnd = CurDMCWav.Count
        End If
        If MouseX <> -1 Then        'If mouseX was -1, we don't need to do this
            Dim ratio As Double = (MouseX / PictureBox1.Width)
            If vEnd = CurDMCWav.Count Then
                MouseX = ratio * (vEnd - vBegin)
                vBegin = MouseX - (MouseX / counter)
                vEnd = MouseX + ((CurDMCWav.Count - MouseX) / counter)
            Else
                'With my current way of doing this being (file size / 10), 1+ sqrt(counter) / sqrt(counter) * 5 shows the best results with any file size.
                'The "5" was found by trial and error.
                Dim Constant As Double = (1 + Math.Sqrt(counter) / (Math.Sqrt(counter) * 5)) 'This is way more efficient than doing this 4 times.
                MouseX = vBegin + (ratio * (vEnd - vBegin))
                If Back = True Then
                    vBegin = MouseX - ((MouseX - vBegin) * Constant)
                    vEnd = MouseX + ((vEnd - MouseX) * Constant)
                Else
                    vBegin = MouseX - ((MouseX - vBegin) / Constant)
                    vEnd = MouseX + ((vEnd - MouseX) / Constant)
                End If

            End If

        End If

        'Necessary for boundary catches!

        If vBegin < 0 Then
            vBegin = 0
        End If
        If vEnd > CurDMCWav.Count - 1 Then
            vEnd = CurDMCWav.Count - 1
        End If

        tmpWavData = Nothing
        GC.Collect()

        'The actual population of tmpWavData

        If counter = 1 Then
            Dim g(CurDMCWav.Count) As Integer
            CurDMCWav.CopyTo(g)
            tmpWavData = g.ToList
            vBegin = 0
            vEnd = CurDMCWav.Count
        Else
            tmpWavData = New List(Of Integer)
            For x = vBegin To vEnd Step 1 + (CurDMCWav.Count / 100000 / counter)    'Samples from the current wave chunk to save time
                'As counter gets higher (more zoomed) this step's denominator gets smaller, resulting in higher accuracy.
                tmpWavData.Add(CurDMCWav(x))
            Next x
        End If
    End Sub

    'Redraw redraws the view.  This is called any time the wave is updated or the box is moved over, resized, zoomed, etc.
    Private Sub Redraw() Handles Me.Resize, Me.DragOver
        If mySize.Width < 1 Or mySize.Height < 1 Or Me.WindowState = FormWindowState.Minimized Then       'initialize code (don't resize on form load)
            Return
        End If

        Dim Oldsize As Size = PictureBox1.Size
        PictureBox1.Size = New Size(PictureBox1.Size.Width + (Me.Size.Width - mySize.Width), PictureBox1.Size.Height + (Me.Size.Height - mySize.Height))
        Dim xChange As Double = PictureBox1.Size.Width / Oldsize.Width

        mySize = Me.Size
        Rect = New Rectangle(Rect.Location.X * xChange, 0, Rect.Size.Width * xChange, PictureBox1.Size.Height)  'Draws the selection if it exists
        Line *= xChange
        graphic(PictureBox1)
    End Sub

    Private Sub graphic(ByRef tmpnl As PictureBox)     '"tmpnl" was going to be used for multiple wave boxes.  This could still be done.

        Dim b As New Bitmap(tmpnl.Width, tmpnl.Height)              'We're going to store the picture here temporarily for speed

        Dim pt1 As New Point(0, 0)
        Dim sz1 As New Size(tmpnl.Size.Width, tmpnl.Size.Height)    'Picturebox's size

        Dim brsh As New SolidBrush(Color.Black)

        g = Graphics.FromImage(b)                                   'This sets a drawing ability to the bitmap we created
        pn = New Pen(Color.Green)                                   'pn = the pen with which the wave is drawn
        Dim Background As New Rectangle(pt1, sz1)
        g.FillRectangle(brsh, Background)                           'This is to create the black background box

        If curWav Is Nothing Then                                   'If there is no wave, just display the black box
            tmpnl.Image = b
            Return
        End If

        Dim br2 As New SolidBrush(Color.Green)
        Dim StartX As Integer = 0                                   'the dimensions of the black box
        'Dim StartY As Integer = 0
        Dim multiplier As Double = tmpWavData.Count / (tmpnl.Size.Width - StartX)
        Dim x As Double = 0
        Dim tmp As Integer = 0

        'Draws the background stripes showing the 6-bit wave

        Dim clr As Color
        clr = Color.FromArgb(25, 25, 25)
        Dim br3 As New SolidBrush(clr)
        For x = 0 To tmpnl.Size.Height Step tmpnl.Size.Height / 32  'every other one
            Dim bgRect As New Rectangle(0, x, tmpnl.Size.Width, tmpnl.Size.Height / 64)
            g.FillRectangle(br3, bgRect)
        Next x
        Dim old_max As Integer = 0   'last m and q. these are useful for filling gaps.
        Dim old_min As Integer = 0
        Dim max As Integer = 0
        Dim min As Integer = 0



        'This loop draws the wave and samples it with Multiplier (wave size / width of image)

        For x = 0 To tmpWavData.Count - multiplier Step multiplier  'splits the wavdata by increments appropriate for box size
            old_max = max
            old_min = min
            max = 0
            min = 10000

            'This is determining the maximum and minimum values for the samples covered by one pixel

            For z = x To x + multiplier Step tmpWavData.Count / 10000       'We sample this too to save time
                tmp = tmpWavData(CInt(Int(z)))
                If max < tmp Then
                    max = tmp
                End If
                If min > tmp Then
                    min = tmp
                End If
            Next



            If max = min Then   'GDI+ does not draw single points as lines.  You must explicitly tell it to draw a point or you get nothing
                g.FillRectangle(br2, StartX, (CInt(min / (256 / (tmpnl.Size.Height)))), 1, 1)
            End If

            'If the old max is smaller than our current min, draw a line from (startx, max fitted to window) to (startX, old_max fitted)
            'If the old min is bigger than the current max, same deal but in reverse (startX, min) to (startX, old_min)
            'This makes the wave look more continuous - there would be small gaps otherwise.

            If old_max < min Then
                g.DrawLine(pn, New Point(StartX, ((max / (256 / (tmpnl.Size.Height))))), New Point(StartX, ((old_max / (256 / (tmpnl.Size.Height))))))  'draws from maximum value to minimum
            ElseIf old_min > max Then
                g.DrawLine(pn, New Point(StartX, ((old_min / (256 / (tmpnl.Size.Height))))), New Point(StartX, ((min / (256 / (tmpnl.Size.Height))))))  'draws from maximum value to minimum
            Else
                g.DrawLine(pn, New Point(StartX, ((max / (256 / (tmpnl.Size.Height))))), New Point(StartX, ((min / (256 / (tmpnl.Size.Height))))))  'draws from maximum value to minimum
            End If
            StartX += 1

        Next

        BackupImage = b.Clone                                               'Set the backup image used for editing (would be super slow to redraw each time
        If Not Rect.Size.Width = 0 Then                                     ' the selection box changed).
            Dim brsh3 As New SolidBrush(Color.FromArgb(50, 255, 255, 255))  'This color is a semi transparent white for the selctions
            g.FillRectangle(brsh3, Rect)                                    'Selection box is filled
        Else
            Dim tmpPen As New Pen(Color.FromArgb(50, 255, 255, 255))        'This color is a semi transparent white for the selctions
            g.DrawLine(tmpPen, Line, 0, Line, PictureBox1.Height)           'Insertion line is set
        End If
        If Not tmpnl.Image Is Nothing Then
            tmpnl.Image.Dispose()                                           'Data cleanup
        End If
        tmpnl.Image = b                                                     'Set the image to the Picturebox

    End Sub

    'This simply creates a new preview if there is not one and 
    Sub VisualizeDMC()
        If curWav Is Nothing Then
            tmpWavData = Nothing
            Return
        End If
        If CurPreview Is Nothing Then
            CurPreview = New WAV(33144, NESResample(WAVtoDMCWAV(curWav)), 8)
        End If
        CurDMCWav = CurPreview.Data.Samples 'just a rename of cpreview.d.s
        'TmpWavFix(-1, Nothing)              'Value is -1 because we are not zooming, just redrawing
        TmpWavFix(PictureBox1.PointToClient(Cursor.Position).X, True)

    End Sub

    'This triggers when the mouse moves over the window.  It creates the selection rectangle 
    ' because the mouse must be down /clicked to continue.
    Private Sub Editor(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseMove
        If (Not MouseIsDown) Or PictureBox1.Image Is Nothing Or curWav Is Nothing Then
            Return
        End If
        PictureBox1.Image = BackupImage.Clone
        Moved = True
        'this creates graphics for the selections
        Dim brsh As New SolidBrush(Color.FromArgb(50, 255, 255, 255))

        g = Graphics.FromImage(PictureBox1.Image)
        Dim d As Integer = e.X
        If e.X > BoxStart Then
            If d > PictureBox1.Size.Width Then
                d = PictureBox1.Size.Width
            End If
            Rect = New Rectangle(BoxStart, 0, d - BoxStart, PictureBox1.Height)
        Else
            If d < 0 Then
                d = 0
            End If
            Rect = New Rectangle(d, 0, BoxStart - d, PictureBox1.Height)
        End If
        g.FillRectangle(brsh, Rect)
        PictureBox1.Refresh()

    End Sub

    'Sets the "MouseIsDown" flag for the "Editor" function to receive.
    Private Sub Down(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Or curWav Is Nothing Then
            Return
        End If
        BoxStart = e.X                  'Start of the selection
        MouseIsDown = True
    End Sub

    'Mouse button was unclicked.  This sets boxEnd and makes sure BoxEnd > BoxStart
    Private Sub Up(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Or MouseIsDown = False Or curWav Is Nothing Then
            Return
        End If
        MouseIsDown = False
        BoxEnd = e.X
        If BoxEnd < 0 Then
            BoxEnd = 0
        End If
        If BoxEnd > PictureBox1.Size.Width Then
            BoxEnd = PictureBox1.Size.Width
        End If
        If BoxEnd < BoxStart Then
            Dim p As Integer = BoxEnd
            BoxEnd = BoxStart
            BoxStart = p
        End If

    End Sub

    'Cancels the current selection and draws a line where the user has clicked.
    Private Sub Cancel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseClick
        If curWav Is Nothing Then
            Return
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip1.Show(PictureBox1, e.X, e.Y)
            Return
        End If
        If Moved = True Then
            Moved = False
            Return
        End If
        Rect = Nothing
        BoxStart = -1
        BoxEnd = 0
        PictureBox1.Image = BackupImage.Clone
        Dim tmpPen As New Pen(Color.FromArgb(50, 255, 255, 255))
        g = Graphics.FromImage(PictureBox1.Image)
        Line = e.X
        g.DrawLine(tmpPen, Line, 0, Line, PictureBox1.Height)
    End Sub

End Class
