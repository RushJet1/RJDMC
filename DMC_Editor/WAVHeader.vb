Imports System.IO

Public Class WAVHeader
    Private w_Format As Format
    Private w_Header As Header

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

    Public Sub New(ByRef Wave As BinaryReader)
        Dim p As New List(Of Byte)   'rather than rewriting header and format to use binaryreader,
        'this is only 38-50 bytes or so anyway.
        p.Add(Wave.ReadByte)
        p.Add(Wave.ReadByte)            'reads the first four bytes.
        p.Add(Wave.ReadByte)
        p.Add(Wave.ReadByte)
        While 1
            If (p(p.Count - 1) = &H61 And p(p.Count - 2) = &H74 And p(p.Count - 3) = &H61 And p(p.Count - 4) = &H64) Then
                Exit While
            End If
            Try
                p.Add(Wave.ReadByte)
            Catch ex As EndOfStreamException
                w_Header = New Header
                Return
            End Try
        End While

        w_Header = New Header(p)
        w_Format = New Format(p)
        Wave.Close()
        Wave = Nothing
    End Sub

    'This definition of "new" makes a new .WAV with the header info filled in.

    Public Sub New(ByVal Rate As Integer, ByRef WaveData As List(Of Integer), ByVal Bits As Integer)
        w_Header = New Header(WaveData.Count + 36)
        w_Format = New Format(Rate)
    End Sub

End Class
