Public Class Batch

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub radWav_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radWav.CheckedChanged
        If radWav.Checked Then
            grpWavRate.Enabled = True
        Else
            grpWavRate.Enabled = False
        End If
    End Sub
End Class