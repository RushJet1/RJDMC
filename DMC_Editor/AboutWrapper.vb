Imports System.IO
Imports System.Text.RegularExpressions
Public Class AboutWrapper
    Public Box As AboutBoxNotify
    Public Text As String
    Public Loc As Point

    Public Sub Execute()
        Box = New AboutBoxNotify()
        Box.lblProgress.Text = Text
        Box.StartPosition = FormStartPosition.Manual
        Box.Location = New Point(Loc.X, Loc.Y)
        Box.AllowDrop = False
        Box.ShowDialog(MainForm)
    End Sub


End Class
