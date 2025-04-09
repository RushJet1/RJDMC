Public Class Form1
    Public Choice As Boolean
    Public curSize As Integer
    Private iTriggeredThis As Boolean = False
    Private hznumber As Integer = 60
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            Return
        ElseIf (curSize / 8) / NumericUpDown1.Value > 64 Then
            MessageBox.Show("These settings will exceed 64 samples.  Famitracker currently supports only 64 samples in use.")
        End If
        Choice = True
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Choice = False
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sfd As New SaveFileDialog

        'Set up sfd (Save File Dialog)
        sfd.Filter = "Text File (.txt)|*.txt"
        sfd.FileName = ".txt"
        sfd.AddExtension = True
        sfd.RestoreDirectory = True
        sfd.FilterIndex = 0
        If sfd.ShowDialog() = 1 Then
            TextBox1.Text = sfd.FileName
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If iTriggeredThis = True Or NumericUpDown3.IsHandleCreated = False Then
            Exit Sub
        End If
        iTriggeredThis = True
        NumericUpDown1.Value = Math.Floor(NumericUpDown2.Value * ((33144 / hznumber) / 8))
        iTriggeredThis = False
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If iTriggeredThis = True Or NumericUpDown3.IsHandleCreated = False Then
            Exit Sub
        End If
        iTriggeredThis = True
        NumericUpDown2.Value = Math.Floor(NumericUpDown1.Value / ((33144 / hznumber) / 8))
        iTriggeredThis = False
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown3.ValueChanged
        If NumericUpDown3.IsHandleCreated = False Then
            Exit Sub
        End If
        hznumber = NumericUpDown3.Value
        NumericUpDown2.Maximum = Math.Floor(4081 / ((33144 / hznumber) / 8))
        NumericUpDown2.Value = NumericUpDown1.Value / ((33144 / hznumber) / 8)
    End Sub
End Class