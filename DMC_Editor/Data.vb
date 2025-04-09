Imports System.IO

Public Class Data
    Private SampleSize As Integer
    Private Data As List(Of Integer)
    Private Subchunk2ID As String
    Private Subchunk2Size As Integer

    Public ReadOnly Property IsCorrupted() As Boolean
        Get
            If Not Subchunk2ID = "data" Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property DataSize() As Integer
        Get
            Return Subchunk2Size
        End Get
    End Property

    Public ReadOnly Property Samples() As List(Of Integer)
        Get
            Return Data
        End Get
    End Property

    'Replaces the data with new data
    Public Sub Replace(ByVal p As List(Of Integer))
        Data = p
    End Sub

    'This is for new data we need to convert to 8-bit
    Public Sub New(ByRef Wave As BinaryReader, ByVal Bits As Integer, ByVal Stereo As Boolean) 'we need to know how to parse the data!
        Dim Left As Integer = 0
        Dim Right As Integer = 0

        Data = New List(Of Integer)

        Subchunk2ID = "data"        'to get to this point, this MUST be true

        Dim tmp1 As String = ""

        Dim tmpList As New List(Of Byte)
        tmpList.Add(Wave.ReadByte)
        tmpList.Add(Wave.ReadByte)
        tmpList.Add(Wave.ReadByte)
        tmpList.Add(Wave.ReadByte)

        For x = 3 To 0 Step -1                                    'Subchunk2Size

            If Hex(tmpList(x)).ToString.Length = 0 Then
                tmp1 += "00"
            ElseIf Hex(tmpList(x)).ToString.Length = 1 Then
                tmp1 += "0" + Hex(tmpList(x))
            Else
                tmp1 += Hex(tmpList(x))
            End If
        Next
        Subchunk2Size = CInt("&H" + tmp1)


        While 1
            If Bits = 16 Then                                   'In this block of code, we convert 16-bit to 6-bit.
                Try
                    Data.Add((Wave.ReadInt16) >> 10)           'Shift all over 10, making 6-bit (0-63 unsigned)
                    If Data.Last > 31 Then
                        Data(Data.Count - 1) -= 32
                    Else
                        Data(Data.Count - 1) += 32
                    End If
                Catch ex As EndOfStreamException                'If the end of the stream is reached, exit
                    Exit While
                End Try
            ElseIf Bits = 8 Then
                Try
                    Data.Add(Wave.ReadByte >> 2)                '8-bit is already unsigned, so just shift right twice.
                Catch ex As EndOfStreamException                'If the end of the stream is reached, exit
                    Exit While
                End Try
            End If

            If Stereo Then                                      'file is stereo
                If Left = 0 Then                                'left is not set
                    Left = Data.Last                            'left = last data point
                ElseIf Right = 0 Then                           'right is not set
                    Right = Data.Last                           'right = last data point
                End If
                If Not Left = 0 And Not Right = 0 Then          'if both are set (after right is set)
                    Data.RemoveAt(Data.Count - 1)               'remove last element of data (old right)
                    Data(Data.Count - 1) = (Left + Right) >> 1  'change only element at the end to mono mix
                    Left = 0
                    Right = 0                                   'reset variables
                End If
            End If
        End While
    End Sub

    'This is just for already-existing data
    Public Sub New(ByRef IncomingData As List(Of Integer), ByVal Bits As Integer) 'we need to know how to parse the data!
        Data = New List(Of Integer)
        SampleSize = 1
        If Bits = 8 Then
            For Each x As Integer In IncomingData
                Data.Add(x >> 2)
            Next
        ElseIf Bits = 6 Then
            Data.AddRange(IncomingData)
        End If
        Subchunk2ID = "data"
        Subchunk2Size = IncomingData.Count
    End Sub


End Class
