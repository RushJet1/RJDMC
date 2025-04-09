<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fade
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
        Me.radFadeIn = New System.Windows.Forms.RadioButton
        Me.radFadeOut = New System.Windows.Forms.RadioButton
        Me.numMinVol = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnOk = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.numMinVol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'radFadeIn
        '
        Me.radFadeIn.AutoSize = True
        Me.radFadeIn.Location = New System.Drawing.Point(12, 41)
        Me.radFadeIn.Name = "radFadeIn"
        Me.radFadeIn.Size = New System.Drawing.Size(60, 17)
        Me.radFadeIn.TabIndex = 0
        Me.radFadeIn.TabStop = True
        Me.radFadeIn.Text = "Fade in"
        Me.radFadeIn.UseVisualStyleBackColor = True
        '
        'radFadeOut
        '
        Me.radFadeOut.AutoSize = True
        Me.radFadeOut.Location = New System.Drawing.Point(12, 64)
        Me.radFadeOut.Name = "radFadeOut"
        Me.radFadeOut.Size = New System.Drawing.Size(67, 17)
        Me.radFadeOut.TabIndex = 1
        Me.radFadeOut.TabStop = True
        Me.radFadeOut.Text = "Fade out"
        Me.radFadeOut.UseVisualStyleBackColor = True
        '
        'numMinVol
        '
        Me.numMinVol.Location = New System.Drawing.Point(94, 15)
        Me.numMinVol.Name = "numMinVol"
        Me.numMinVol.Size = New System.Drawing.Size(57, 20)
        Me.numMinVol.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(151, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "%"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(91, 52)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 4
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Minimum Volume"
        '
        'Fade
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(175, 89)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.numMinVol)
        Me.Controls.Add(Me.radFadeOut)
        Me.Controls.Add(Me.radFadeIn)
        Me.Name = "Fade"
        Me.Text = "Fade Volume"
        CType(Me.numMinVol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents radFadeIn As System.Windows.Forms.RadioButton
    Friend WithEvents radFadeOut As System.Windows.Forms.RadioButton
    Friend WithEvents numMinVol As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
