'Palette 2.1.7

Imports System.IO

Public Class frmPalette
    Const PALETTE_EXT As String = ".plt"                    'Extension of palette files
    Dim activeColour As Color = Color.Black                 'Value of last colour selected
    Dim tickNumber As Integer = 0                           'Delay before zooming a swatch
    Dim swatchSize As Integer = 0                           'Static value for swatch size
    Dim rowSize As Integer = 0                              'Number of swatches per row
    Dim drawingPoint As New Point(0, 0)                     'Starting point for drawing swatches
    Dim highlightPoint As New Point(0, 0)                   'Swatch coordinates when activeColour changes
    Dim modCoordinates As New Point(0, 0)                   'Mouse coordinates over swatch grid
    Dim swatchHover As Boolean = False                      'True if cursor is over swatch grid
    Dim activeHover As Boolean = False                      'True if cursor is over active colour
    Dim colourList As ArrayList = Nothing                   'List of hex values from active palette file
    Dim hoverDelay As Integer = 0                           'Number of times timer has ticked
    Dim oldIndex As Integer = 0                             'Previous swatch grid position for zoom sensitivity

    Private Sub frmPalette_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadPreferences()
        loadPalettes()
        setPalette("Web Safe" & PALETTE_EXT)
    End Sub

    Private Sub frmPalette_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        If swatchHover = True Then
            If ((modCoordinates.Y * rowSize) + modCoordinates.X) < colourList.Count() Then
                activeColour = ColorTranslator.FromHtml(colourList(((modCoordinates.Y * rowSize) + modCoordinates.X)))
                lblColour.BackColor = activeColour
                highlightPoint = New Point((modCoordinates.X * swatchSize) + drawingPoint.X, (modCoordinates.Y * swatchSize) + drawingPoint.Y)
            End If
        End If
    End Sub

    Private Sub frmPalette_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Const CONTROL_BOX_HEIGHT As Integer = 21
        Dim zoomSize As Integer = swatchSize * 1.6
        Dim zoomCoordinates As New Point(0, 0)
        Dim drawingCoordinates As New Point(drawingPoint.X, drawingPoint.Y)
        Dim hexIndex As Integer = 0

        For i As Integer = 0 To colourList.Count - 1 Step 1
            e.Graphics.FillRectangle(Brushes.Black, drawingCoordinates.X, drawingCoordinates.Y, swatchSize + 1, swatchSize + 1)
            e.Graphics.FillRectangle(New SolidBrush(ColorTranslator.FromHtml(colourList(i))), drawingCoordinates.X + 1, drawingCoordinates.Y + 1, swatchSize - 1, swatchSize - 1)
            drawingCoordinates.X = drawingCoordinates.X + swatchSize
            If (drawingCoordinates.X - drawingPoint.X) Mod (swatchSize * rowSize) = 0 Then
                drawingCoordinates.Y = drawingCoordinates.Y + swatchSize
                drawingCoordinates.X = drawingPoint.X
            End If
        Next
        If swatchHover = True Then
            If ((modCoordinates.X * (swatchSize)) Mod swatchSize = 0) And ((modCoordinates.Y * (swatchSize)) Mod swatchSize = 0) Then
                zoomCoordinates.X = ((modCoordinates.X * swatchSize) - Math.Floor(swatchSize / 3)) + drawingPoint.X
                zoomCoordinates.Y = ((modCoordinates.Y * swatchSize) - Math.Floor(swatchSize / 3)) + drawingPoint.Y
                If zoomCoordinates.Y < 0 Then
                    zoomCoordinates.Y = 0
                ElseIf zoomCoordinates.Y + zoomSize > Me.ClientRectangle.Height() - CONTROL_BOX_HEIGHT Then
                    zoomCoordinates.Y = (Me.ClientRectangle.Height - CONTROL_BOX_HEIGHT - 1) - zoomSize
                End If
                If zoomCoordinates.X < 0 Then
                    zoomCoordinates.X = 0
                ElseIf zoomCoordinates.X + zoomSize > Me.ClientRectangle.Width() Then
                    zoomCoordinates.X = (Me.ClientRectangle.Width - 1) - zoomSize
                End If
                hexIndex = (modCoordinates.Y * rowSize) + modCoordinates.X
                If hexIndex < colourList.Count() Then
                    If hoverDelay >= tickNumber Then
                        e.Graphics.FillRectangle(Brushes.Black, zoomCoordinates.X, zoomCoordinates.Y, zoomSize + 1, zoomSize + 1)
                        e.Graphics.FillRectangle(New SolidBrush(ColorTranslator.FromHtml(colourList(hexIndex))), zoomCoordinates.X + 1, zoomCoordinates.Y + 1, zoomSize - 1, zoomSize - 1)
                        hoverDelay = 0
                    End If
                    txtHexValue.Text = colourList(hexIndex)
                End If
            End If
        End If
        If activeHover = True Then
            If highlightPoint.X >= 0 And highlightPoint.Y >= 0 Then
                e.Graphics.DrawRectangle(Pens.White, highlightPoint.X, highlightPoint.Y, swatchSize, swatchSize)
            End If
        End If
        oldIndex = hexIndex
    End Sub

    Private Sub frmPalette_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If (e.X >= drawingPoint.X And e.X < (drawingPoint.X + (swatchSize * rowSize))) And (e.Y >= drawingPoint.Y And e.Y < (drawingPoint.Y + (colourList.Count \ rowSize) * swatchSize) + ((colourList.Count Mod rowSize) * swatchSize)) Then
            swatchHover = True
        Else
            swatchHover = False
            txtHexValue.Text = "#" & Microsoft.VisualBasic.Conversion.Hex(Convert.ToInt32(activeColour.ToArgb())).Substring(2)
        End If
        modCoordinates = New Point(CInt(((e.X - drawingPoint.X) - (swatchSize / 2)) / (swatchSize)), CInt(((e.Y - drawingPoint.Y) - (swatchSize / 2)) / (swatchSize)))
        hoverDelay = 0
        If oldIndex <> ((modCoordinates.Y * rowSize) + modCoordinates.X) Then
            Me.Invalidate()
        End If
    End Sub

    Private Sub frmPalette_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseHover
        tmrHover.Enabled = True
    End Sub

    Private Sub frmPalette_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        swatchHover = False
        tmrHover.Enabled = False
        hoverDelay = 0
        txtHexValue.Text = "#" & Microsoft.VisualBasic.Conversion.Hex(Convert.ToInt32(activeColour.ToArgb())).Substring(2)
        Me.Invalidate()
    End Sub

    Private Sub lblColour_MouseMove(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblColour.MouseMove
        txtHexValue.Text = ColorTranslator.ToHtml(activeColour)
        activeHover = True
        Me.Invalidate()
    End Sub

    Private Sub lblColour_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblColour.MouseLeave
        activeHover = False
        Me.Invalidate()
    End Sub

    Private Sub btnColourDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColourDialog.Click
        clrWindowsColour.Color = activeColour
        clrWindowsColour.ShowDialog()
        activeColour = clrWindowsColour.Color()
        highlightPoint = New Point(-1, -1)
        txtHexValue.Text = ColorTranslator.ToHtml(activeColour)
        lblColour.BackColor = activeColour
    End Sub

    Private Sub cntPaletteList_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles cntPaletteList.DropDownItemClicked
        setPalette(e.ClickedItem.Text & ".plt")
        Me.Invalidate()
    End Sub

    Private Sub tmrHover_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrHover.Tick
        hoverDelay = hoverDelay + 1
        If hoverDelay = tickNumber Then
            Me.Invalidate()
        End If
    End Sub

    Private Sub loadPreferences()
        Const PREFERENCES_EXT As String = ".cfg"
        Const DEFAULT_SWATCH_SIZE As Integer = 10
        Const DEFAULT_ROW_SIZE As Integer = 18
        Const DEFAULT_TICK_NUMBER As Integer = 2
        Const DEFAULT_DRAWING_POINT_X As Integer = 0
        Const DEFAULT_DRAWING_POINT_Y As Integer = 0
        Const DEFAULT_WIDTH As Integer = 187
        Const DEFAULT_HEIGHT As Integer = 166
        Dim preferences() As String = Nothing
        Dim fileReader As New StreamReader(Application.StartupPath & "\preferences" & PREFERENCES_EXT)

        Try
            preferences = fileReader.ReadToEnd.Split(vbLf)
            For i As Integer = 0 To preferences.Length - 1 Step 1
                preferences(i).Trim()
                preferences(i) = preferences(i).Substring(preferences(i).LastIndexOf("=") + 1)
            Next
            swatchSize = Convert.ToInt32(preferences(0))
            rowSize = Convert.ToInt32(preferences(1))
            tickNumber = Convert.ToInt32(preferences(2))
            drawingPoint = New Point(preferences(3).Substring(0, preferences(3).IndexOf(" ")), preferences(3).Substring(preferences(3).LastIndexOf(" ")))
            Me.Width = Convert.ToInt32(preferences(4))
            Me.Height = Convert.ToInt32(preferences(5))
        Catch ex As Exception
            swatchSize = DEFAULT_SWATCH_SIZE
            rowSize = DEFAULT_ROW_SIZE
            tickNumber = DEFAULT_TICK_NUMBER
            drawingPoint = New Point(DEFAULT_DRAWING_POINT_X, DEFAULT_DRAWING_POINT_Y)
            Me.Width = DEFAULT_WIDTH
            Me.Height = DEFAULT_HEIGHT
            MessageBox.Show("Preferences failed to load. Default preferences used.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub loadPalettes()
        Dim pltFiles As New ArrayList(Directory.GetFiles(Application.StartupPath & "\palettes\"))

        If pltFiles.Count() > 0 Then
            For i As Integer = 0 To pltFiles.Count() - 1 Step 1
                If pltFiles(i).ToString.Length >= PALETTE_EXT.Length() Then
                    If (pltFiles(i).ToString.Substring(pltFiles(i).length - PALETTE_EXT.Length())).ToLower() = PALETTE_EXT Then
                        cntPaletteList.DropDownItems.Add(pltFiles(i).ToString.Substring(pltFiles(i).ToString.LastIndexOf("\") + 1, pltFiles(i).ToString.Length - (pltFiles(i).ToString.LastIndexOf("\") + 1) - PALETTE_EXT.Length()))
                    End If
                End If
            Next
        Else
            Me.Close()
        End If
    End Sub

    Private Sub setPalette(ByVal paletteFile As String)
        Dim currentPalette As ToolStripMenuItem = Nothing
        Dim fileReader As New StreamReader(Application.StartupPath & "\palettes\" & paletteFile)
        colourList = ArrayList.Adapter(fileReader.ReadToEnd.Trim.Split(vbLf))
        fileReader.Dispose()
        For i As Integer = 0 To cntPaletteList.DropDownItems.Count() - 1 Step 1
            currentPalette = cntPaletteList.DropDownItems(i)
            cntPaletteList.DropDownItems.RemoveAt(i)
            If currentPalette.Text = paletteFile.Substring(0, paletteFile.Length - 4) Then
                currentPalette.Checked = True
            Else
                currentPalette.Checked = False
            End If
            cntPaletteList.DropDownItems.Insert(i, currentPalette)
        Next
        highlightPoint = New Point(0 + drawingPoint.X, 0 + drawingPoint.Y)
        activeColour = ColorTranslator.FromHtml(colourList(0))
        txtHexValue.Text = "#" & Microsoft.VisualBasic.Conversion.Hex(Convert.ToInt32(activeColour.ToArgb())).Substring(2)
        lblColour.BackColor = activeColour
    End Sub

    Private Sub cntPalette_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles cntPalette.Opened
        cntPaletteList.DropDownItems.Clear()
        loadPalettes()
    End Sub
End Class

