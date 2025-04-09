Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security

Partial Class MainForm

    'This code exists to make it so that the Trackbars all
    'scroll by one when the user uses the scrollwheel instead of 3,
    'and it also makes the bars remain stationary when the user is
    'using the scroll wheel to zoom in on the waveform, regardless
    'of whether or not it has focus.
    Sub Scroller() Handles trkQuality.Scroll, trkPitch.Scroll, trkBitCrush.Scroll
        If ChangeScroll Then
            If Not trkQuality.Value = Scroll1Prev Then
                If trkQuality.Value < Scroll1Prev Then
                    trkQuality.Value = Scroll1Prev - 1
                    trkQualityMask.Value = Scroll1Prev - 1
                Else
                    trkQuality.Value = Scroll1Prev + 1
                    trkQualityMask.Value = Scroll1Prev + 1
                End If
            ElseIf Not trkPitch.Value = Scroll2Prev Then
                If trkPitch.Value < Scroll2Prev Then
                    trkPitch.Value = Scroll2Prev - 1
                    trkPitchMask.Value = Scroll2Prev - 1
                Else
                    trkPitch.Value = Scroll2Prev + 1
                    trkPitchMask.Value = Scroll2Prev + 1
                End If
            ElseIf Not trkBitCrush.Value = Scroll3Prev Then
                If trkBitCrush.Value < Scroll3Prev Then
                    trkBitCrush.Value = Scroll3Prev - 1
                    trkCrushMask.Value = Scroll3Prev - 1
                Else
                    trkBitCrush.Value = Scroll3Prev + 1
                    trkCrushMask.Value = Scroll3Prev + 1
                End If
            End If
        Else
            trkQuality.Value = Scroll1Prev
            trkPitch.Value = Scroll2Prev
            trkBitCrush.Value = Scroll3Prev
            trkQualityMask.Value = Scroll1Prev
            trkPitchMask.Value = Scroll2Prev
            trkCrushMask.Value = Scroll3Prev
        End If
        ChangeScroll = True
        Scroll1Prev = trkQuality.Value
        Scroll2Prev = trkPitch.Value
        Scroll3Prev = trkBitCrush.Value

        PitchView()
        QualView()
    End Sub

    'Quality/Pitch/Crush-Mask functions give focus to the hidden
    'slider bar to avoid flickering.  When the hidden one changes
    'its scroll position, it is changed to the proper position.
    Sub QualityMask() Handles trkQualityMask.GotFocus
        trkQuality.Focus()
    End Sub

    Sub PitchMask() Handles trkPitchMask.GotFocus
        trkPitch.Focus()
    End Sub

    Sub CrushMask() Handles trkCrushMask.GotFocus
        trkBitCrush.Focus()
    End Sub

    Sub QualityMaskChanged() Handles trkQualityMask.Scroll
        ChangeScroll = True
        trkQuality.Value = trkQualityMask.Value
        Scroll1Prev = trkQuality.Value
        QualView()
    End Sub

    Sub PitchMaskChanged() Handles trkPitchMask.Scroll
        ChangeScroll = True
        trkPitch.Value = trkPitchMask.Value
        Scroll2Prev = trkPitch.Value
        PitchView()
    End Sub

    Sub CrushMaskChanged() Handles trkCrushMask.Scroll
        ChangeScroll = True
        trkBitCrush.Value = trkCrushMask.Value
        Scroll3Prev = trkBitCrush.Value
    End Sub


    Private Sub PitchView()         'sets the pitch number
        If trkPitch.Value = 15 Then
            PrevRate = 33144
        ElseIf trkPitch.Value = 14 Then
            PrevRate = 24856
        ElseIf trkPitch.Value = 13 Then
            PrevRate = 21304
        ElseIf trkPitch.Value = 12 Then
            PrevRate = 16880
        ElseIf trkPitch.Value = 11 Then
            PrevRate = 13984
        ElseIf trkPitch.Value = 10 Then
            PrevRate = 12600
        ElseIf trkPitch.Value = 9 Then
            PrevRate = 11184
        ElseIf trkPitch.Value = 8 Then
            PrevRate = 9416
        ElseIf trkPitch.Value = 7 Then
            PrevRate = 8360
        ElseIf trkPitch.Value = 6 Then
            PrevRate = 7920
        ElseIf trkPitch.Value = 5 Then
            PrevRate = 7048
        ElseIf trkPitch.Value = 4 Then
            PrevRate = 6256
        ElseIf trkPitch.Value = 3 Then
            PrevRate = 5592
        ElseIf trkPitch.Value = 2 Then
            PrevRate = 5264
        ElseIf trkPitch.Value = 1 Then
            PrevRate = 4712
        ElseIf trkPitch.Value = 0 Then
            PrevRate = 4184
        End If
        PrevChange = True
        lblPitch.Text = "Pitch: " & Hex(trkPitch.Value)
    End Sub

    Private Sub QualView()         'sets the pitch number
        lblQuality.Text = "Quality: " & Hex(trkQuality.Value)
        If trkQuality.Value = 15 Then
            QualRate = 33144
        ElseIf trkQuality.Value = 14 Then
            QualRate = 24856
        ElseIf trkQuality.Value = 13 Then
            QualRate = 21304
        ElseIf trkQuality.Value = 12 Then
            QualRate = 16880
        ElseIf trkQuality.Value = 11 Then
            QualRate = 13984
        ElseIf trkQuality.Value = 10 Then
            QualRate = 12600
        ElseIf trkQuality.Value = 9 Then
            QualRate = 11184
        ElseIf trkQuality.Value = 8 Then
            QualRate = 9416
        ElseIf trkQuality.Value = 7 Then
            QualRate = 8360
        ElseIf trkQuality.Value = 6 Then
            QualRate = 7920
        ElseIf trkQuality.Value = 5 Then
            QualRate = 7048
        ElseIf trkQuality.Value = 4 Then
            QualRate = 6256
        ElseIf trkQuality.Value = 3 Then
            QualRate = 5592
        ElseIf trkQuality.Value = 2 Then
            QualRate = 5264
        ElseIf trkQuality.Value = 1 Then
            QualRate = 4712
        ElseIf trkQuality.Value = 0 Then
            QualRate = 4184
        End If
        PrevChange = True
        lblQuality.Text = "Quality: " & Hex(trkQuality.Value)
    End Sub



End Class
