Imports System.IO

Public Class DMC
    Private d_data As BitArray              'the ones and zeros as represented by bytes (expanded data)
    Private d_1bit As List(Of Byte)         'the ones and zeros as bits (actual data)
    Private b_data As BitArray
    Private b_1bit As List(Of Byte)


    'Returns a copy of the DMC
    Public ReadOnly Property Copy() As DMC
        Get
            Dim z As New MemoryStream(d_1bit.ToArray)
            Dim br As New BinaryReader(z)
            Dim p As New DMC(br)
            Return p
        End Get
    End Property

    'Outputs the DPCM values as a list of integers that correspond to PCM wave bytes (8-bit)
    Public ReadOnly Property ToWaveData(ByVal DeltaStart As Integer) As List(Of Integer)
        Get
            Dim z As New List(Of Integer)
            Dim nug As Integer = DeltaStart * 4
            For Each dc In d_data 'curdmc.samples
                If dc = 0 Then
                    nug -= 4
                Else
                    nug += 4
                End If
                If nug > 255 Then
                    nug = 255
                ElseIf nug < 0 Then
                    nug = 0
                End If
                z.Add(nug)
            Next
            Return z
        End Get
    End Property

    Public ReadOnly Property Data() As List(Of Byte)
        Get
            Return d_1bit
        End Get
    End Property

    Public ReadOnly Property Samples() As BitArray
        Get
            Return d_data
        End Get
    End Property

    Public Sub Filter()
        Dim flag As Integer = 0
        Dim counter As Integer = 0
        For x = 1 To d_data.Length - 1
            If d_data(x) = True And counter < 64 Then
                counter += 1
            ElseIf counter > 0 Then
                counter -= 1
            End If
            If d_data(x) <> d_data(x - 1) Then
                flag += 1
                If flag = 5 Then
                    If counter > 32 Then
                        d_data(x - 1) = 1
                        d_data(x - 2) = 1
                        d_data(x - 3) = 1
                        d_data(x - 4) = 1
                        If counter > 3 Then
                            counter -= 4
                        Else
                            counter = 0
                        End If
                    Else
                        d_data(x - 1) = 0
                        d_data(x - 2) = 0
                        d_data(x - 3) = 0
                        d_data(x - 4) = 0
                        If counter < 60 Then
                            counter += 4
                        Else
                            counter = 63
                        End If
                    End If
                    flag = 0
                End If
                Else
                    flag = 0
                End If
        Next

        ConvToByte()
    End Sub

    Public Sub Revert()
        d_data = New BitArray(b_data.Length)
        d_1bit = New List(Of Byte)

        d_data = b_data.Clone
        For Each item In b_1bit
            d_1bit.Add(item)
        Next
    End Sub

    Public Sub Tilt(ByVal start As Integer, ByVal xend As Integer, ByVal str As Integer)
        Dim r As New Random
        If str < 0 Then
            For x = start To xend
                'true goes down (quiet) false goes up (loud)
                If d_data(x) = False Then
                    If r.Next(1, 500 / -str) = 1 Then
                        d_data(x) = True
                    End If
                End If

            Next
        ElseIf str = 0 Then
            Return
        Else
            For x = start To xend
                'true goes down (quiet) false goes up (loud)
                If d_data(x) = True Then
                    If r.Next(1, 501 / str) = 1 Then
                        d_data(x) = False
                    End If
                End If

            Next
        End If
    End Sub

    'This works for values 1-19 of the Bit Crush slider.
    'It takes the number in the slider, subtracts from 21, then uses that as the interval
    ' to flip bits.
    Public Sub Crush(ByVal Divisor As Integer)
        Revert()
        If Divisor = 0 Or Divisor = 20 Then
            Return
        Else
            For d = 0 To d_data.Count - 1 Step (21 - Divisor) * (21 - Divisor)
                If d_data(d) = False Then
                    d_data(d) = True
                Else
                    d_data(d) = False
                End If
            Next
        End If
        Dim z As Integer = d_data.Count
        While 1
            If z Mod 8 = 0 Then
                Exit While
            Else
                z -= 1
            End If
        End While
        Dim tmpData As New BitArray(z)
        For x = 0 To z - 1
            tmpData(x) = d_data(x)
        Next
        d_data = New BitArray(tmpData)
        ConvToByte()
    End Sub

    'IN A NUTSHELL, this function takes a WAV and makes a DMC approximation of it.
    'DMCs are stored in arrays of bits (for easy access. this might change), and arrays of bytes.

    'This has the deltacounter follow the PCM wave's values as closely as possible.
    Public Sub New(ByRef Wave As WAV)

        'The code below subtracts however many bits it needs to get to a round "8" number.
        'It can be as few as 0 or as many as 7.
        Dim z As Integer = Wave.Data.Samples.Count
        While 1
            If z Mod 8 = 0 Then
                Exit While
            Else
                z -= 1
            End If
        End While

        d_data = New BitArray(z)
        b_data = New BitArray(z)
        Dim DeltaCounter As Integer = Wave.Data.Samples(0) - 1      'Delta Counter start position
        Dim Change As Integer = 0                                   'The "current" 1-bit value per loop
        Dim x As Integer = 0                                        'Count for bit arrays

        For Each sample In Wave.Data.Samples
            If sample > DeltaCounter Then                           'Sample is higher than current DC value
                DeltaCounter += 1                                   ' so add one to DC and write "1" as the change bit
                Change = 1
            ElseIf sample < DeltaCounter Then                       'Sample is lower than current DC value
                Change = 0                                          ' so sub one from DC and write "0" as the change bit
                DeltaCounter -= 1
            Else                                                    'Sample equals Delta Counter---
                If DeltaCounter = 63 Then                           'Maximum value (63) reached, keep hitting 63 for "silence."
                    Change = 1
                ElseIf DeltaCounter > 0 Then                        'Equal DC and current value but not at an edge.. so we just
                    DeltaCounter += 1                               ' increase by one to keep moving.
                    Change = 1
                Else                                                'Minimum value (0) reached, keep hitting zero for "silence"
                    Change = 0
                End If

            End If
            Try
                d_data(x) = Change                                  'set d_data(x) and b_data(x) to the current change
                b_data(x) = Change
            Catch ex As Exception                                   'Size limit(z) reached.
                Exit For
            End Try
            x += 1
        Next

        ConvToByte()                                                'Convert to Byte/BIT format
        Mod16Plus1()                                                'Convert to mod16+1 format
    End Sub

    'This converts the data (ones and zeros) to hexidecimal values seen in files
    Private Sub ConvToByte()
        d_1bit = New List(Of Byte)
        Dim backup_flag As Boolean = False
        If b_1bit Is Nothing Then
            b_1bit = New List(Of Byte)
            backup_flag = True
        End If

        Dim gg As New BitArray(d_data.Count)
        For x = 0 To d_data.Count - 1
            gg(x) = d_data(x)
        Next
        Dim ge((d_data.Count / 8) - 1) As Byte
        gg.CopyTo(ge, 0)
        For Each tmpByte In ge
            d_1bit.Add(tmpByte)
            If backup_flag = True Then
                b_1bit.Add(tmpByte)
            End If
        Next
    End Sub

    Public Sub New(ByRef DpcmData As binaryReader)       'for opening DMC files directly
        d_1bit = New List(Of Byte)
        b_1bit = New List(Of Byte)
        While 1
            Try
                d_1bit.Add(DpcmData.ReadByte)
                b_1bit.Add(d_1bit.Last)
            Catch ex As EndOfStreamException            'This is the only way to get out of this while loop
                Exit While                              ' as binaryReaders do not have a flag for it.
            End Try
        End While

        'We have read in a list of bytes from the file.  These are not ones and zeros but hex values.

        Dim tmpBitarr As New BitArray(d_1bit.ToArray)   'Converts d_1bit to a bitarray (1s and 0s)
        d_data = tmpBitarr.Clone                        'Copies the new values to d_data and b_data
        b_data = tmpBitarr.Clone

        Mod16Plus1()        'make it mod16+1 bytes
    End Sub

    'Makes sure the file size (mod 16) equals 1.  This is how the NES does it.
    Private Sub Mod16Plus1()
        Return

        'Dim addbytes As Integer = 0
        'While 1
        '    If (d_1bit.Count Mod 16) = 1 Then
        '        Exit While
        '    Else
        '        d_1bit.Add(&H55)
        '        b_1bit.Add(&H55)
        '        addbytes += 1
        '    End If
        'End While
        'Dim p As New BitArray(((addbytes * 8) + d_data.Count))
        'For x = 0 To d_data.Count - 1
        '    p.Set(x, d_data(x))
        'Next
        'For x = d_data.Count To p.Count - 1 Step 2
        '    p.Set(x, 0)
        '    p.Set(x + 1, 1)
        'Next
        'd_data = p
        'b_data = p.Clone
    End Sub


    Public Sub WriteSegmentedTxt(strFile As String, length As Integer)
        Dim fileExists As Boolean = File.Exists(strFile)
        Using sw As New StreamWriter(File.Open(strFile, FileMode.Create))
            sw.WriteLine("# FamiTracker text export 0.4.2" & vbNewLine & "# Song information" & vbNewLine & "TITLE           """ & vbNewLine & "AUTHOR          """ & vbNewLine & "COPYRIGHT       """ & vbNewLine & "" & vbNewLine & "# Song comment" & vbNewLine & "COMMENT """ & vbNewLine & "" & vbNewLine & "# Global settings" & vbNewLine & "MACHINE         0" & vbNewLine & "FRAMERATE       0" & vbNewLine & "EXPANSION       0" & vbNewLine & "VIBRATO         1" & vbNewLine & "SPLIT           32" & vbNewLine & "# Macros" & vbNewLine & "" & vbNewLine & "" & vbNewLine & "# DPCM samples")
            Dim z As Integer = 0 ' our counter for the whole dmc
            Dim dmcNum As Integer = 0 'how many dmcs we've done so far
            For z = 0 To d_1bit.Count - 1 - length
                Dim blockCounter As Integer = 0
                sw.Write("DPCMDEF   " & dmcNum & "  " & length & " """ & dmcNum & ".dmc""" & vbNewLine)
                sw.Write("DPCM : ")
                For x = z To z + length - 1
                    Dim hexValue As String = Hex(d_1bit(x))
                    If hexValue.Length = 1 Then
                        hexValue = "0" & hexValue
                    End If
                    sw.Write(hexValue & " ")
                    blockCounter += 1
                    If blockCounter = 31 Then
                        sw.Write(vbNewLine & "DPCM : ")
                        blockCounter = 0
                    End If
                Next
                dmcNum += 1
                sw.Write(vbNewLine)
                z += length
            Next
            Dim blockCountre As Integer = 0
            sw.Write("DPCMDEF   " & dmcNum & "  " & d_1bit.Count - z & " """ & dmcNum & ".dmc""" & vbNewLine)
            sw.Write("DPCM : ")
            For x = z To d_1bit.Count - 1     'one last loop for remaining data
                Dim hexValue As String = Hex(d_1bit(x))
                If hexValue.Length = 1 Then
                    hexValue = "0" & hexValue
                End If
                sw.Write(hexValue & " ")
                blockCountre += 1
                If blockCountre = 31 Then
                    sw.Write(vbNewLine & "DPCM : ")
                    blockCountre = 0
                End If
            Next
            dmcNum += 1
            sw.Write(vbNewLine & vbNewLine)
            'done with DMC data. Now to write instrument tables and such
            sw.Write("# Instruments" & vbNewLine & "INST2A03   0    -1  -1  -1  -1  -1 ""New instrument""" & vbNewLine)

            Dim octave As Integer = 0
            Dim keynum As Integer = 0

            For x = 0 To dmcNum
                sw.Write("KEYDPCM   0   " & octave & "   " & keynum & "     " & x & "  15   0     0  -1" & vbNewLine)
                keynum += 1
                If keynum = 12 Then
                    keynum = 0
                    octave += 1
                End If
            Next

            sw.Write(vbNewLine & "# Tracks" & vbNewLine & vbNewLine & "TRACK  64  10 150 ""New song""" & vbNewLine & "COLUMNS : 1 1 1 1 1" & _
                     vbNewLine & vbNewLine & "ORDER 00 : 00 00 00 00 00" & vbNewLine & vbNewLine & "PATTERN 00" & vbNewLine)

            For x = 0 To 63
                Dim hexText As String = Hex(x)
                If hexText.Length = 1 Then
                    hexText = "0" + hexText
                End If
                sw.Write("ROW " & hexText & " : ... .. . ... : ... .. . ... : ... .. . ... : ... .. . ... : ... .. . ..." & vbNewLine)
            Next

        End Using
    End Sub
End Class
