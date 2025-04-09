Imports System.IO

'This is a seperate class so the other thread can send variables to it
'as a thread.  Otherwise the function could not have variables passed to it.
'This way, I can issue a stop command by changing a the "Com" variable and
'having a handler "handle" it.  The reason this needed to be threaded is that
'if it were on a single thread, any major activity would crash the playback
'(and the rest of the program).
Public Class AudPlayer
    Public Sound As System.Media.SoundPlayer
    Public AudioStream As MemoryStream
    Public WithEvents Com As Command 'true is play, false is stop
    Public Looped As Boolean

    Public Sub Run()
        If Looped Then
            Sound.PlayLooping()
        Else
            Sound.PlaySync()
        End If
    End Sub

    Public Sub d() Handles Com.VariableChanged
        If Com.Command = False Then
            Sound.Stop()
        End If
    End Sub

    Public Sub New()
        Com = New Command
        Looped = False
    End Sub
End Class
