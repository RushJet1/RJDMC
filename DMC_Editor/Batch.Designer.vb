<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Batch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.radManual = New System.Windows.Forms.RadioButton
        Me.radWav = New System.Windows.Forms.RadioButton
        Me.radDmc = New System.Windows.Forms.RadioButton
        Me.btnOK = New System.Windows.Forms.Button
        Me.grpWavRate = New System.Windows.Forms.GroupBox
        Me.cmbRate = New System.Windows.Forms.ComboBox
        Me.GroupBox1.SuspendLayout()
        Me.grpWavRate.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radManual)
        Me.GroupBox1.Controls.Add(Me.radWav)
        Me.GroupBox1.Controls.Add(Me.radDmc)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(178, 91)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Action on Open"
        '
        'radManual
        '
        Me.radManual.AutoSize = True
        Me.radManual.Location = New System.Drawing.Point(6, 19)
        Me.radManual.Name = "radManual"
        Me.radManual.Size = New System.Drawing.Size(115, 17)
        Me.radManual.TabIndex = 2
        Me.radManual.TabStop = True
        Me.radManual.Text = "Manual conversion"
        Me.radManual.UseVisualStyleBackColor = True
        '
        'radWav
        '
        Me.radWav.AutoSize = True
        Me.radWav.Location = New System.Drawing.Point(6, 42)
        Me.radWav.Name = "radWav"
        Me.radWav.Size = New System.Drawing.Size(166, 17)
        Me.radWav.TabIndex = 1
        Me.radWav.TabStop = True
        Me.radWav.Text = "Automatically convert to WAV"
        Me.radWav.UseVisualStyleBackColor = True
        '
        'radDmc
        '
        Me.radDmc.AutoSize = True
        Me.radDmc.Location = New System.Drawing.Point(6, 65)
        Me.radDmc.Name = "radDmc"
        Me.radDmc.Size = New System.Drawing.Size(165, 17)
        Me.radDmc.TabIndex = 0
        Me.radDmc.TabStop = True
        Me.radDmc.Text = "Automatically convert to DMC"
        Me.radDmc.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(110, 168)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'grpWavRate
        '
        Me.grpWavRate.Controls.Add(Me.cmbRate)
        Me.grpWavRate.Location = New System.Drawing.Point(7, 111)
        Me.grpWavRate.Name = "grpWavRate"
        Me.grpWavRate.Size = New System.Drawing.Size(178, 51)
        Me.grpWavRate.TabIndex = 2
        Me.grpWavRate.TabStop = False
        Me.grpWavRate.Text = "Wave Output Rate"
        '
        'cmbRate
        '
        Me.cmbRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRate.FormattingEnabled = True
        Me.cmbRate.Items.AddRange(New Object() {"11025", "16000", "22050", "32000", "33144", "44100"})
        Me.cmbRate.Location = New System.Drawing.Point(28, 19)
        Me.cmbRate.Name = "cmbRate"
        Me.cmbRate.Size = New System.Drawing.Size(121, 21)
        Me.cmbRate.TabIndex = 0
        '
        'frmPreferences
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(194, 198)
        Me.Controls.Add(Me.grpWavRate)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmPreferences"
        Me.Text = "Batch Export"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpWavRate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radManual As System.Windows.Forms.RadioButton
    Friend WithEvents radWav As System.Windows.Forms.RadioButton
    Friend WithEvents radDmc As System.Windows.Forms.RadioButton
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents grpWavRate As System.Windows.Forms.GroupBox
    Friend WithEvents cmbRate As System.Windows.Forms.ComboBox
End Class
