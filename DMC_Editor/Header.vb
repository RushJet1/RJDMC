Public Class Header
    Private ChunkID As String
    Private ChunkSize As Integer
    Private Format As String

    Public ReadOnly Property FileSize() As Integer
        Get
            Return ChunkSize + 8
        End Get
    End Property

    Public ReadOnly Property IsWave() As Boolean
        Get
            If ChunkID = "RIFF" And Format = "WAVE" Then
                Return True
            End If
            Return False
        End Get
    End Property

    'New input that must be read
    Public Sub New(ByRef Wave As List(Of Byte))
        'Initialize variables
        ChunkID = ""
        ChunkSize = 0
        Format = ""

        'Populate ChunkID and Format
        For x = 0 To 3
            ChunkID += Chr(CInt(Wave(x)))
            Format += Chr(CInt(Wave(x + 8)))
        Next

        'Convert bytes 4-7 to ChunkSize (little-endian)
        Dim tmpStr As String = ""
        For x = 7 To 4 Step -1              'little endian reordering
            If (Hex(Wave(x))).Length = 1 Then
                tmpStr += "0"
            End If
            tmpStr += Hex(Wave(x)).ToString
        Next

        ChunkSize = CInt("&H" + tmpStr)
    End Sub

    'Creating an already-existing header
    Public Sub New(ByVal Size As Integer)
        ChunkID = "RIFF"
        ChunkSize = Size
        Format = "WAVE"
    End Sub

    'This is for the functions that need a header to be there to avoid crashes but do not use it
    Public Sub New()
        ChunkID = "NULL"
        Format = "NULL"
    End Sub

End Class
