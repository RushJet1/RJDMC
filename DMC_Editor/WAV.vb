Imports System.IO

Public Class WAV
    Private w_Data As Data
    Private w_Format As Format
    Private w_Header As Header

    Public Sub WriteHeader(ByVal hdr As WAVHeader)
        w_Header = hdr.Header
        w_Format = hdr.Format
    End Sub

    Public Function Copy() As WAV
        Dim mcopy As New WAV(33144, w_Data.Samples, 6)
        Return mcopy
    End Function


    'Makes a list of bytes ready for writing to a .wav file.
    '"Bitconv" is passed to specify if 6-bit to 8-bit conversion should be done.

    'Many of these values are hard coded (such as "RIFF" "WAVE" "fmt " "data")
    'This constructs the header and puts the data after it, then returns a list of bytes
    ' ready to write.
    Public ReadOnly Property ExportWave(ByVal BitConv As Boolean, ByVal diff As Integer) As List(Of Byte)
        Get

            Dim i, j, k, l, m, n, o, p, q, r, s, t As Integer

            Dim c1size As Integer = w_Data.Samples.Count + 36 + diff
            i = c1size >> 24 And &HFF
            j = c1size >> 16 And &HFF
            k = c1size >> 8 And &HFF
            l = c1size And &HFF

            c1size = w_Data.Samples.Count + diff

            m = c1size >> 24 And &HFF
            n = c1size >> 16 And &HFF
            o = c1size >> 8 And &HFF
            p = c1size And &HFF

            Dim Rate As Integer = w_Format.SampleRate

            q = Rate >> 24 And &HFF
            r = Rate >> 16 And &HFF
            s = Rate >> 8 And &HFF
            t = Rate And &HFF

            'This block is the header, format, and first part of the Data sections.
            Dim g() As Byte = {&H52, &H49, &H46, &H46,
                                  l, k, j, i,
                                  &H57, &H41, &H56, &H45,
                                  &H66, &H6D, &H74, &H20,
                                  16, 0, 0, 0,
                                  1, 0, 1, 0,
                                  t, s, r, q,
                                  t, s, r, q,
                                  1, 0, 8, 0,
                                  &H64, &H61, &H74, &H61,
                                  p, o, n, m}
            Dim g2 As List(Of Byte) = g.ToList
            g = Nothing
            For Each tmp As Integer In w_Data.Samples
                If BitConv Then
                    g2.Add(tmp << 2)
                Else
                    g2.Add(tmp)
                End If
            Next
            Return g2
        End Get
    End Property

    Public ReadOnly Property Data() As Data
        Get
            Return w_Data
        End Get
    End Property

    Public ReadOnly Property Format() As Format
        Get
            Return w_Format
        End Get
    End Property

    Public ReadOnly Property Header() As Header
        Get
            Return w_Header
        End Get
    End Property

    Public ReadOnly Property IsCorrupted() As Boolean
        Get
            If w_Data.IsCorrupted Or w_Format.IsCorrupted Or w_Data.IsCorrupted Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public Sub New(ByRef Wave As BinaryReader)
        Dim p As New List(Of Byte)   'rather than rewriting header and format to use binaryreader,
                                        'this is only 38-50 bytes or so anyway.
        p.Add(Wave.ReadByte)
        p.Add(Wave.ReadByte)            'reads the first four bytes.
        p.Add(Wave.ReadByte)
        p.Add(Wave.ReadByte)
        While Not (p(p.Count - 1) = &H61 And p(p.Count - 2) = &H74 And p(p.Count - 3) = &H61 And p(p.Count - 4) = &H64)
            Try
                p.Add(Wave.ReadByte)
            Catch ex As EndOfStreamException    '"data" does not exist in stream, return
                w_Header = New Header
                Return
            End Try
        End While

        w_Header = New Header(p)
        w_Format = New Format(p)
        w_Data = New Data(Wave, w_Format.BitsPerSample, (w_Format.NumChannels = 2))
        Wave.Close()
        Wave = Nothing
    End Sub

    'This definition of "new" makes a new .WAV with the header info filled in.

    Public Sub New(ByVal Rate As Integer, ByRef WaveData As List(Of Integer), ByVal Bits As Integer)
        w_Header = New Header(WaveData.Count + 36)
        w_Format = New Format(Rate)
        w_Data = New Data(WaveData, Bits)
    End Sub

End Class
