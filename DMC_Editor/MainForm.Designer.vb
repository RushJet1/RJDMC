<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenwavToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpendmcToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SavedmcToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SavewavToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveSplitdmcToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrimToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomToSelectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VolumeToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FadeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreferencesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssociateDMCFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblChannels = New System.Windows.Forms.Label()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.lblBits = New System.Windows.Forms.Label()
        Me.lblFormat = New System.Windows.Forms.Label()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbOpenWave = New System.Windows.Forms.ToolStripButton()
        Me.tsbOpenDMC = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveWav = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveDMC = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tsbPreview = New System.Windows.Forms.ToolStripButton()
        Me.tsbSelPreview = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnTilt = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.numTilt = New System.Windows.Forms.NumericUpDown()
        Me.trkCrushMask = New System.Windows.Forms.TrackBar()
        Me.trkBitCrush = New System.Windows.Forms.TrackBar()
        Me.lblBitCrush = New System.Windows.Forms.Label()
        Me.trkQualityMask = New System.Windows.Forms.TrackBar()
        Me.trkPitchMask = New System.Windows.Forms.TrackBar()
        Me.lblQuality = New System.Windows.Forms.Label()
        Me.lblPitch = New System.Windows.Forms.Label()
        Me.trkPitch = New System.Windows.Forms.TrackBar()
        Me.trkQuality = New System.Windows.Forms.TrackBar()
        Me.numVolume = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnRevert = New System.Windows.Forms.Button()
        Me.chkLoop = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrimToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReverseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectALlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomIntoSelectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VolumeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeVolumeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FadeVolumeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.numDelta = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numTilt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trkCrushMask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trkBitCrush, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trkQualityMask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trkPitchMask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trkPitch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trkQuality, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.numDelta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.ActionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(0)
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip1.Size = New System.Drawing.Size(822, 24)
        Me.MenuStrip1.TabIndex = 0
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.FileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenwavToolStripMenuItem, Me.OpendmcToolStripMenuItem, Me.SavedmcToolStripMenuItem, Me.SavewavToolStripMenuItem, Me.SaveSplitdmcToolStripMenuItem, Me.CloseToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(33, 24)
        Me.FileToolStripMenuItem.Text = "File"
        Me.FileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.FileToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'OpenwavToolStripMenuItem
        '
        Me.OpenwavToolStripMenuItem.AutoSize = False
        Me.OpenwavToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.OpenwavToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.OpenwavToolStripMenuItem.Name = "OpenwavToolStripMenuItem"
        Me.OpenwavToolStripMenuItem.ShortcutKeyDisplayString = ""
        Me.OpenwavToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.OpenwavToolStripMenuItem.Text = "Open .wav"
        Me.OpenwavToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OpenwavToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'OpendmcToolStripMenuItem
        '
        Me.OpendmcToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.OpendmcToolStripMenuItem.Name = "OpendmcToolStripMenuItem"
        Me.OpendmcToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.OpendmcToolStripMenuItem.Text = "Open .dmc"
        Me.OpendmcToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OpendmcToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'SavedmcToolStripMenuItem
        '
        Me.SavedmcToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.SavedmcToolStripMenuItem.Name = "SavedmcToolStripMenuItem"
        Me.SavedmcToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.SavedmcToolStripMenuItem.Text = "Save .dmc"
        Me.SavedmcToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SavedmcToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'SavewavToolStripMenuItem
        '
        Me.SavewavToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.SavewavToolStripMenuItem.Name = "SavewavToolStripMenuItem"
        Me.SavewavToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.SavewavToolStripMenuItem.Text = "Save .dmc as .wav"
        Me.SavewavToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SavewavToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'SaveSplitdmcToolStripMenuItem
        '
        Me.SaveSplitdmcToolStripMenuItem.Name = "SaveSplitdmcToolStripMenuItem"
        Me.SaveSplitdmcToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.SaveSplitdmcToolStripMenuItem.Text = "Save Split .dmc"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        Me.CloseToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CloseToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        Me.ExitToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem1, Me.CopyToolStripMenuItem1, Me.PasteToolStripMenuItem1, Me.DeleteToolStripMenuItem1, Me.TrimToolStripMenuItem1, Me.ZoomToSelectionToolStripMenuItem, Me.VolumeToolStripMenuItem1})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(35, 24)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'CutToolStripMenuItem1
        '
        Me.CutToolStripMenuItem1.Name = "CutToolStripMenuItem1"
        Me.CutToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl + X"
        Me.CutToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
        Me.CutToolStripMenuItem1.Text = "Cut"
        '
        'CopyToolStripMenuItem1
        '
        Me.CopyToolStripMenuItem1.Name = "CopyToolStripMenuItem1"
        Me.CopyToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl + C"
        Me.CopyToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
        Me.CopyToolStripMenuItem1.Text = "Copy"
        '
        'PasteToolStripMenuItem1
        '
        Me.PasteToolStripMenuItem1.Name = "PasteToolStripMenuItem1"
        Me.PasteToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl + V"
        Me.PasteToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
        Me.PasteToolStripMenuItem1.Text = "Paste"
        '
        'DeleteToolStripMenuItem1
        '
        Me.DeleteToolStripMenuItem1.Name = "DeleteToolStripMenuItem1"
        Me.DeleteToolStripMenuItem1.ShortcutKeyDisplayString = "Del"
        Me.DeleteToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
        Me.DeleteToolStripMenuItem1.Text = "Delete"
        '
        'TrimToolStripMenuItem1
        '
        Me.TrimToolStripMenuItem1.Name = "TrimToolStripMenuItem1"
        Me.TrimToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl + T"
        Me.TrimToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
        Me.TrimToolStripMenuItem1.Text = "Trim"
        '
        'ZoomToSelectionToolStripMenuItem
        '
        Me.ZoomToSelectionToolStripMenuItem.Name = "ZoomToSelectionToolStripMenuItem"
        Me.ZoomToSelectionToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ZoomToSelectionToolStripMenuItem.Text = "Zoom to Selection"
        '
        'VolumeToolStripMenuItem1
        '
        Me.VolumeToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeToolStripMenuItem, Me.FadeToolStripMenuItem})
        Me.VolumeToolStripMenuItem1.Name = "VolumeToolStripMenuItem1"
        Me.VolumeToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
        Me.VolumeToolStripMenuItem1.Text = "Volume"
        '
        'ChangeToolStripMenuItem
        '
        Me.ChangeToolStripMenuItem.Name = "ChangeToolStripMenuItem"
        Me.ChangeToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.ChangeToolStripMenuItem.Text = "Change..."
        '
        'FadeToolStripMenuItem
        '
        Me.FadeToolStripMenuItem.Name = "FadeToolStripMenuItem"
        Me.FadeToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.FadeToolStripMenuItem.Text = "Fade..."
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PreferencesToolStripMenuItem, Me.AssociateDMCFilesToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'PreferencesToolStripMenuItem
        '
        Me.PreferencesToolStripMenuItem.Name = "PreferencesToolStripMenuItem"
        Me.PreferencesToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.PreferencesToolStripMenuItem.Text = "Batch Export Options..."
        '
        'AssociateDMCFilesToolStripMenuItem
        '
        Me.AssociateDMCFilesToolStripMenuItem.Name = "AssociateDMCFilesToolStripMenuItem"
        Me.AssociateDMCFilesToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.AssociateDMCFilesToolStripMenuItem.Text = "Associate .DMC files"
        '
        'ActionsToolStripMenuItem
        '
        Me.ActionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem"
        Me.ActionsToolStripMenuItem.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ActionsToolStripMenuItem.Size = New System.Drawing.Size(40, 24)
        Me.ActionsToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'lblName
        '
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblName.Location = New System.Drawing.Point(12, 52)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(373, 15)
        Me.lblName.TabIndex = 23
        Me.lblName.Text = "Name:"
        '
        'lblChannels
        '
        Me.lblChannels.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblChannels.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblChannels.Location = New System.Drawing.Point(324, 244)
        Me.lblChannels.Name = "lblChannels"
        Me.lblChannels.Size = New System.Drawing.Size(100, 15)
        Me.lblChannels.TabIndex = 17
        Me.lblChannels.Text = "Channels:"
        '
        'lblSize
        '
        Me.lblSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize.Location = New System.Drawing.Point(231, 244)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(89, 15)
        Me.lblSize.TabIndex = 16
        Me.lblSize.Text = "Size:"
        '
        'lblRate
        '
        Me.lblRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRate.Location = New System.Drawing.Point(144, 244)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(83, 15)
        Me.lblRate.TabIndex = 15
        Me.lblRate.Text = "Rate:"
        '
        'lblBits
        '
        Me.lblBits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBits.Location = New System.Drawing.Point(90, 244)
        Me.lblBits.Name = "lblBits"
        Me.lblBits.Size = New System.Drawing.Size(50, 15)
        Me.lblBits.TabIndex = 14
        Me.lblBits.Text = "Bits:"
        '
        'lblFormat
        '
        Me.lblFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFormat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFormat.Location = New System.Drawing.Point(12, 244)
        Me.lblFormat.Name = "lblFormat"
        Me.lblFormat.Size = New System.Drawing.Size(75, 15)
        Me.lblFormat.TabIndex = 13
        Me.lblFormat.Text = "Format:"
        '
        'ofd
        '
        Me.ofd.Filter = "Wave Soundfile|*.wav"
        Me.ofd.Multiselect = True
        Me.ofd.Title = "Open File..."
        '
        'sfd
        '
        Me.sfd.Filter = "Delta Modulated Code|*.dmc"
        Me.sfd.Title = "Save File As..."
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenWave, Me.tsbOpenDMC, Me.tsbSaveWav, Me.tsbSaveDMC, Me.ToolStripButton1, Me.tsbPreview, Me.tsbSelPreview, Me.ToolStripButton2})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(822, 28)
        Me.ToolStrip1.TabIndex = 2
        '
        'tsbOpenWave
        '
        Me.tsbOpenWave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbOpenWave.Image = CType(resources.GetObject("tsbOpenWave.Image"), System.Drawing.Image)
        Me.tsbOpenWave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpenWave.Name = "tsbOpenWave"
        Me.tsbOpenWave.Size = New System.Drawing.Size(23, 25)
        Me.tsbOpenWave.Text = "Open .WAV"
        '
        'tsbOpenDMC
        '
        Me.tsbOpenDMC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbOpenDMC.Image = CType(resources.GetObject("tsbOpenDMC.Image"), System.Drawing.Image)
        Me.tsbOpenDMC.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpenDMC.Name = "tsbOpenDMC"
        Me.tsbOpenDMC.Size = New System.Drawing.Size(23, 25)
        Me.tsbOpenDMC.Text = "Open .DMC"
        '
        'tsbSaveWav
        '
        Me.tsbSaveWav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSaveWav.Image = CType(resources.GetObject("tsbSaveWav.Image"), System.Drawing.Image)
        Me.tsbSaveWav.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveWav.Name = "tsbSaveWav"
        Me.tsbSaveWav.Size = New System.Drawing.Size(23, 25)
        Me.tsbSaveWav.Text = "Save .WAV"
        '
        'tsbSaveDMC
        '
        Me.tsbSaveDMC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSaveDMC.Image = CType(resources.GetObject("tsbSaveDMC.Image"), System.Drawing.Image)
        Me.tsbSaveDMC.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveDMC.Name = "tsbSaveDMC"
        Me.tsbSaveDMC.Size = New System.Drawing.Size(23, 25)
        Me.tsbSaveDMC.Text = "Save .DMC"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 25)
        Me.ToolStripButton1.Text = "Stop"
        '
        'tsbPreview
        '
        Me.tsbPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPreview.Image = CType(resources.GetObject("tsbPreview.Image"), System.Drawing.Image)
        Me.tsbPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPreview.Name = "tsbPreview"
        Me.tsbPreview.Size = New System.Drawing.Size(23, 25)
        Me.tsbPreview.Text = "Preview"
        '
        'tsbSelPreview
        '
        Me.tsbSelPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbSelPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSelPreview.Image = CType(resources.GetObject("tsbSelPreview.Image"), System.Drawing.Image)
        Me.tsbSelPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSelPreview.Name = "tsbSelPreview"
        Me.tsbSelPreview.Size = New System.Drawing.Size(23, 25)
        Me.tsbSelPreview.Text = "Preview Selection"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 25)
        Me.ToolStripButton2.Text = "Save Split .dmc"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Location = New System.Drawing.Point(12, 68)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(599, 172)
        Me.PictureBox1.TabIndex = 25
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnTilt)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.numTilt)
        Me.GroupBox1.Controls.Add(Me.trkCrushMask)
        Me.GroupBox1.Controls.Add(Me.trkBitCrush)
        Me.GroupBox1.Controls.Add(Me.lblBitCrush)
        Me.GroupBox1.Controls.Add(Me.trkQualityMask)
        Me.GroupBox1.Controls.Add(Me.trkPitchMask)
        Me.GroupBox1.Controls.Add(Me.lblQuality)
        Me.GroupBox1.Controls.Add(Me.lblPitch)
        Me.GroupBox1.Controls.Add(Me.trkPitch)
        Me.GroupBox1.Controls.Add(Me.trkQuality)
        Me.GroupBox1.Location = New System.Drawing.Point(618, 68)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 191)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'btnTilt
        '
        Me.btnTilt.Location = New System.Drawing.Point(9, 162)
        Me.btnTilt.Name = "btnTilt"
        Me.btnTilt.Size = New System.Drawing.Size(51, 23)
        Me.btnTilt.TabIndex = 39
        Me.btnTilt.Text = "Tilt"
        Me.btnTilt.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(77, 167)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "strength:"
        '
        'numTilt
        '
        Me.numTilt.Location = New System.Drawing.Point(127, 164)
        Me.numTilt.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.numTilt.Minimum = New Decimal(New Integer() {500, 0, 0, -2147483648})
        Me.numTilt.Name = "numTilt"
        Me.numTilt.Size = New System.Drawing.Size(39, 20)
        Me.numTilt.TabIndex = 40
        '
        'trkCrushMask
        '
        Me.trkCrushMask.Location = New System.Drawing.Point(88, 98)
        Me.trkCrushMask.Maximum = 20
        Me.trkCrushMask.Name = "trkCrushMask"
        Me.trkCrushMask.Size = New System.Drawing.Size(104, 45)
        Me.trkCrushMask.TabIndex = 38
        '
        'trkBitCrush
        '
        Me.trkBitCrush.Location = New System.Drawing.Point(88, 98)
        Me.trkBitCrush.Maximum = 20
        Me.trkBitCrush.Name = "trkBitCrush"
        Me.trkBitCrush.Size = New System.Drawing.Size(104, 45)
        Me.trkBitCrush.TabIndex = 37
        '
        'lblBitCrush
        '
        Me.lblBitCrush.AutoSize = True
        Me.lblBitCrush.Location = New System.Drawing.Point(6, 105)
        Me.lblBitCrush.Name = "lblBitCrush"
        Me.lblBitCrush.Size = New System.Drawing.Size(58, 13)
        Me.lblBitCrush.TabIndex = 36
        Me.lblBitCrush.Text = "BitCrush: 0"
        '
        'trkQualityMask
        '
        Me.trkQualityMask.Location = New System.Drawing.Point(88, 58)
        Me.trkQualityMask.Maximum = 15
        Me.trkQualityMask.Name = "trkQualityMask"
        Me.trkQualityMask.Size = New System.Drawing.Size(104, 45)
        Me.trkQualityMask.TabIndex = 35
        '
        'trkPitchMask
        '
        Me.trkPitchMask.Location = New System.Drawing.Point(88, 19)
        Me.trkPitchMask.Maximum = 15
        Me.trkPitchMask.Name = "trkPitchMask"
        Me.trkPitchMask.Size = New System.Drawing.Size(104, 45)
        Me.trkPitchMask.TabIndex = 34
        '
        'lblQuality
        '
        Me.lblQuality.AutoSize = True
        Me.lblQuality.Location = New System.Drawing.Point(6, 67)
        Me.lblQuality.Name = "lblQuality"
        Me.lblQuality.Size = New System.Drawing.Size(51, 13)
        Me.lblQuality.TabIndex = 30
        Me.lblQuality.Text = "Quality: F"
        '
        'lblPitch
        '
        Me.lblPitch.AutoSize = True
        Me.lblPitch.Location = New System.Drawing.Point(6, 30)
        Me.lblPitch.Name = "lblPitch"
        Me.lblPitch.Size = New System.Drawing.Size(43, 13)
        Me.lblPitch.TabIndex = 29
        Me.lblPitch.Text = "Pitch: F"
        '
        'trkPitch
        '
        Me.trkPitch.Location = New System.Drawing.Point(88, 19)
        Me.trkPitch.Maximum = 15
        Me.trkPitch.Name = "trkPitch"
        Me.trkPitch.Size = New System.Drawing.Size(104, 45)
        Me.trkPitch.TabIndex = 28
        '
        'trkQuality
        '
        Me.trkQuality.Location = New System.Drawing.Point(88, 58)
        Me.trkQuality.Maximum = 15
        Me.trkQuality.Name = "trkQuality"
        Me.trkQuality.Size = New System.Drawing.Size(104, 45)
        Me.trkQuality.TabIndex = 27
        '
        'numVolume
        '
        Me.numVolume.Location = New System.Drawing.Point(446, 27)
        Me.numVolume.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.numVolume.Name = "numVolume"
        Me.numVolume.Size = New System.Drawing.Size(62, 20)
        Me.numVolume.TabIndex = 27
        Me.numVolume.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(339, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Input Wave Volume:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(509, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "%"
        '
        'btnRevert
        '
        Me.btnRevert.Location = New System.Drawing.Point(530, 26)
        Me.btnRevert.Name = "btnRevert"
        Me.btnRevert.Size = New System.Drawing.Size(51, 23)
        Me.btnRevert.TabIndex = 30
        Me.btnRevert.Text = "Revert"
        Me.btnRevert.UseVisualStyleBackColor = True
        '
        'chkLoop
        '
        Me.chkLoop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkLoop.AutoSize = True
        Me.chkLoop.Location = New System.Drawing.Point(696, 28)
        Me.chkLoop.Name = "chkLoop"
        Me.chkLoop.Size = New System.Drawing.Size(50, 17)
        Me.chkLoop.TabIndex = 31
        Me.chkLoop.Text = "Loop"
        Me.chkLoop.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.TrimToolStripMenuItem, Me.ReverseToolStripMenuItem, Me.SelectALlToolStripMenuItem, Me.RevertToolStripMenuItem, Me.ZoomIntoSelectionToolStripMenuItem, Me.VolumeToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(182, 224)
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.ShortcutKeyDisplayString = "CTRL-X"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CutToolStripMenuItem.Text = "Cu&t"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.ShortcutKeyDisplayString = "CTRL-C"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.ShortcutKeyDisplayString = "CTRL-V"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.PasteToolStripMenuItem.Text = "&Paste"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.ShortcutKeyDisplayString = "Del"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.DeleteToolStripMenuItem.Text = "&Delete"
        '
        'TrimToolStripMenuItem
        '
        Me.TrimToolStripMenuItem.Name = "TrimToolStripMenuItem"
        Me.TrimToolStripMenuItem.ShortcutKeyDisplayString = "CTRL-T"
        Me.TrimToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.TrimToolStripMenuItem.Text = "&Trim"
        '
        'ReverseToolStripMenuItem
        '
        Me.ReverseToolStripMenuItem.Name = "ReverseToolStripMenuItem"
        Me.ReverseToolStripMenuItem.ShortcutKeyDisplayString = "CTRL-I"
        Me.ReverseToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ReverseToolStripMenuItem.Text = "&Reverse"
        '
        'SelectALlToolStripMenuItem
        '
        Me.SelectALlToolStripMenuItem.Name = "SelectALlToolStripMenuItem"
        Me.SelectALlToolStripMenuItem.ShortcutKeyDisplayString = "CTRL-A"
        Me.SelectALlToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.SelectALlToolStripMenuItem.Text = "&Select All"
        '
        'RevertToolStripMenuItem
        '
        Me.RevertToolStripMenuItem.Name = "RevertToolStripMenuItem"
        Me.RevertToolStripMenuItem.ShortcutKeyDisplayString = "CTRL-Z"
        Me.RevertToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.RevertToolStripMenuItem.Text = "R&evert"
        '
        'ZoomIntoSelectionToolStripMenuItem
        '
        Me.ZoomIntoSelectionToolStripMenuItem.Name = "ZoomIntoSelectionToolStripMenuItem"
        Me.ZoomIntoSelectionToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ZoomIntoSelectionToolStripMenuItem.Text = "&Zoom Into Selection"
        '
        'VolumeToolStripMenuItem
        '
        Me.VolumeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeVolumeToolStripMenuItem, Me.FadeVolumeToolStripMenuItem})
        Me.VolumeToolStripMenuItem.Name = "VolumeToolStripMenuItem"
        Me.VolumeToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.VolumeToolStripMenuItem.Text = "Selection &Volume..."
        '
        'ChangeVolumeToolStripMenuItem
        '
        Me.ChangeVolumeToolStripMenuItem.Name = "ChangeVolumeToolStripMenuItem"
        Me.ChangeVolumeToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.ChangeVolumeToolStripMenuItem.Text = "&Change Volume"
        '
        'FadeVolumeToolStripMenuItem
        '
        Me.FadeVolumeToolStripMenuItem.Name = "FadeVolumeToolStripMenuItem"
        Me.FadeVolumeToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.FadeVolumeToolStripMenuItem.Text = "&Fade Volume"
        '
        'numDelta
        '
        Me.numDelta.Location = New System.Drawing.Point(281, 27)
        Me.numDelta.Maximum = New Decimal(New Integer() {63, 0, 0, 0})
        Me.numDelta.Name = "numDelta"
        Me.numDelta.Size = New System.Drawing.Size(39, 20)
        Me.numDelta.TabIndex = 32
        Me.numDelta.Value = New Decimal(New Integer() {31, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(178, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 13)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Delta Counter Start:"
        '
        'MainForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 265)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.numDelta)
        Me.Controls.Add(Me.btnRevert)
        Me.Controls.Add(Me.chkLoop)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.numVolume)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.lblChannels)
        Me.Controls.Add(Me.lblFormat)
        Me.Controls.Add(Me.lblSize)
        Me.Controls.Add(Me.lblBits)
        Me.Controls.Add(Me.lblRate)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(450, 250)
        Me.Name = "MainForm"
        Me.Text = "RJDMC"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numTilt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trkCrushMask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trkBitCrush, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trkQualityMask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trkPitchMask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trkPitch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trkQuality, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numVolume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.numDelta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenwavToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblChannels As System.Windows.Forms.Label
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents lblRate As System.Windows.Forms.Label
    Friend WithEvents lblBits As System.Windows.Forms.Label
    Friend WithEvents lblFormat As System.Windows.Forms.Label
    Friend WithEvents SavedmcToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sfd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SavewavToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpendmcToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbOpenWave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbOpenDMC As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSaveWav As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSaveDMC As System.Windows.Forms.ToolStripButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents tsbPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblQuality As System.Windows.Forms.Label
    Friend WithEvents lblPitch As System.Windows.Forms.Label
    Friend WithEvents trkPitch As System.Windows.Forms.TrackBar
    Friend WithEvents trkQuality As System.Windows.Forms.TrackBar
    Friend WithEvents trkPitchMask As System.Windows.Forms.TrackBar
    Friend WithEvents trkQualityMask As System.Windows.Forms.TrackBar
    Friend WithEvents trkBitCrush As System.Windows.Forms.TrackBar
    Friend WithEvents lblBitCrush As System.Windows.Forms.Label
    Friend WithEvents numVolume As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnRevert As System.Windows.Forms.Button
    Friend WithEvents chkLoop As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrimToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectALlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents numDelta As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents trkCrushMask As System.Windows.Forms.TrackBar
    Friend WithEvents tsbSelPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents ZoomIntoSelectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VolumeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeVolumeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FadeVolumeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReverseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnTilt As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents numTilt As System.Windows.Forms.NumericUpDown
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrimToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VolumeToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZoomToSelectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FadeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreferencesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveSplitdmcToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssociateDMCFilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
