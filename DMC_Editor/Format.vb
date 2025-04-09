Public Class Format
    Dim fSubchunk1ID As String
    Dim fSubchunk1Size As Integer
    Dim fAudioFormat As Boolean 'PCM = 1, other = 0
    Dim fNumChannels As Integer
    Dim fSampleRate As Integer
    Dim fByteRate As Integer
    Dim fBlockAlign As Integer
    Dim fBitsPerSample As Integer

    Public ReadOnly Property IsCorrupted() As Boolean
        Get
            If fSubchunk1ID = "fmt " Then
                Return False
            End If
            Return True
        End Get
    End Property

    Public ReadOnly Property AudioFormat() As Boolean
        Get
            Return fAudioFormat
        End Get
    End Property

    Public Property NumChannels() As Integer
        Get
            Return fNumChannels
        End Get
        Set(ByVal value As Integer)
            fNumChannels = value
        End Set
    End Property

    Public Property SampleRate() As Integer
        Get
            Return fSampleRate
        End Get
        Set(ByVal value As Integer)
            fSampleRate = value
        End Set
    End Property

    Public Property ByteRate() As Integer
        Get
            Return fByteRate
        End Get
        Set(ByVal value As Integer)
            fByteRate = value
        End Set
    End Property

    Public Property BitsPerSample() As Integer
        Get
            Return fBitsPerSample
        End Get
        Set(ByVal value As Integer)
            fBitsPerSample = value
        End Set
    End Property

    Public Sub New(ByRef Wave As List(Of Byte))
        fSubchunk1ID = ""
        For x = 12 To 15
            fSubchunk1ID += Chr(CInt(Wave(x)))
        Next
        Dim tmp1 As String = ""     'Subchunk1Size
        Dim tmp2 As String = ""     'AudioFormat & NumChannels
        Dim tmp3 As String = ""     'SampleRate
        Dim tmp4 As String = ""     'ByteRate

        For x = 19 To 16 Step -1
            If (Hex(Wave(x))).Length = 1 Then
                tmp1 += "0"
            End If
            tmp1 += Hex(Wave(x))
            If (Hex(Wave(x + 8))).Length = 1 Then
                tmp3 += "0"
            End If
            tmp3 += Hex(Wave(x + 8))
            If (Hex(Wave(x + 12))).Length = 1 Then
                tmp4 += "0"
            End If
            tmp4 += Hex(Wave(x + 12))
        Next
        fSubchunk1Size = CInt("&H" + tmp1)
        fSampleRate = CInt("&H" + tmp3)
        fByteRate = CInt("&H" + tmp4)

        tmp1 = ""
        tmp3 = ""
        tmp4 = ""
        For x = 21 To 20 Step -1
            tmp1 += Hex(Wave(x))
            tmp2 += Hex(Wave(x + 2))
            tmp3 += Hex(Wave(x + 12))
            tmp4 += Hex(Wave(x + 14))
        Next

        fNumChannels = CInt("&H" + tmp2)
        If CInt("&H" + tmp1) = 1 Then
            fAudioFormat = 1
        End If
        fBlockAlign = CInt("&H" + tmp3)
        fBitsPerSample = CInt("&H" + tmp4)

    End Sub

    Public Sub New(ByVal Rate As Integer)
        fSubchunk1ID = "fmt "
        fSubchunk1Size = 16
        fAudioFormat = 1
        fNumChannels = 1
        fSampleRate = Rate
        fByteRate = 8
        fBlockAlign = 1
        fBitsPerSample = 8
    End Sub

End Class
