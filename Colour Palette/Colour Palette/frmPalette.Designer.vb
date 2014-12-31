<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPalette
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPalette))
        Me.btnColourDialog = New System.Windows.Forms.Button
        Me.txtHexValue = New System.Windows.Forms.TextBox
        Me.lblColour = New System.Windows.Forms.Label
        Me.clrWindowsColour = New System.Windows.Forms.ColorDialog
        Me.cntPalette = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cntPaletteList = New System.Windows.Forms.ToolStripMenuItem
        Me.tmrHover = New System.Windows.Forms.Timer(Me.components)
        Me.cntPalette.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnColourDialog
        '
        Me.btnColourDialog.BackgroundImage = CType(resources.GetObject("btnColourDialog.BackgroundImage"), System.Drawing.Image)
        Me.btnColourDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnColourDialog.FlatAppearance.BorderSize = 10
        Me.btnColourDialog.Location = New System.Drawing.Point(162, 122)
        Me.btnColourDialog.Name = "btnColourDialog"
        Me.btnColourDialog.Size = New System.Drawing.Size(20, 20)
        Me.btnColourDialog.TabIndex = 1
        Me.btnColourDialog.UseVisualStyleBackColor = True
        '
        'txtHexValue
        '
        Me.txtHexValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHexValue.Location = New System.Drawing.Point(0, 122)
        Me.txtHexValue.Name = "txtHexValue"
        Me.txtHexValue.Size = New System.Drawing.Size(65, 20)
        Me.txtHexValue.TabIndex = 3
        '
        'lblColour
        '
        Me.lblColour.BackColor = System.Drawing.Color.White
        Me.lblColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColour.Location = New System.Drawing.Point(67, 123)
        Me.lblColour.Name = "lblColour"
        Me.lblColour.Size = New System.Drawing.Size(25, 18)
        Me.lblColour.TabIndex = 4
        '
        'clrWindowsColour
        '
        Me.clrWindowsColour.AnyColor = True
        Me.clrWindowsColour.FullOpen = True
        '
        'cntPalette
        '
        Me.cntPalette.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cntPaletteList})
        Me.cntPalette.Name = "cntPalette"
        Me.cntPalette.Size = New System.Drawing.Size(116, 26)
        '
        'cntPaletteList
        '
        Me.cntPaletteList.Image = CType(resources.GetObject("cntPaletteList.Image"), System.Drawing.Image)
        Me.cntPaletteList.Name = "cntPaletteList"
        Me.cntPaletteList.Size = New System.Drawing.Size(115, 22)
        Me.cntPaletteList.Text = "Palettes"
        '
        'tmrHover
        '
        '
        'frmPalette
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(181, 142)
        Me.ContextMenuStrip = Me.cntPalette
        Me.Controls.Add(Me.txtHexValue)
        Me.Controls.Add(Me.lblColour)
        Me.Controls.Add(Me.btnColourDialog)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "frmPalette"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Palette"
        Me.cntPalette.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnColourDialog As System.Windows.Forms.Button
    Friend WithEvents txtHexValue As System.Windows.Forms.TextBox
    Friend WithEvents lblColour As System.Windows.Forms.Label
    Friend WithEvents clrWindowsColour As System.Windows.Forms.ColorDialog
    Friend WithEvents cntPalette As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cntPaletteList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrHover As System.Windows.Forms.Timer

End Class
