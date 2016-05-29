<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.txtConnectionString = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtQuery = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearQueryCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvSchemaColumns = New System.Windows.Forms.DataGridView()
        Me.rbCS = New System.Windows.Forms.RadioButton()
        Me.txtObjectName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbVB = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbStoredProcedure = New System.Windows.Forms.RadioButton()
        Me.rbQuery = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdCnnString = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgvSchemaColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtConnectionString
        '
        Me.txtConnectionString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtConnectionString.Location = New System.Drawing.Point(9, 44)
        Me.txtConnectionString.Margin = New System.Windows.Forms.Padding(2)
        Me.txtConnectionString.Multiline = True
        Me.txtConnectionString.Name = "txtConnectionString"
        Me.txtConnectionString.Size = New System.Drawing.Size(683, 40)
        Me.txtConnectionString.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Connection String"
        '
        'txtQuery
        '
        Me.txtQuery.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQuery.Location = New System.Drawing.Point(9, 114)
        Me.txtQuery.Margin = New System.Windows.Forms.Padding(2)
        Me.txtQuery.Multiline = True
        Me.txtQuery.Name = "txtQuery"
        Me.txtQuery.Size = New System.Drawing.Size(591, 71)
        Me.txtQuery.TabIndex = 5
        '
        'txtCode
        '
        Me.txtCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCode.Location = New System.Drawing.Point(9, 399)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(2)
        Me.txtCode.Multiline = True
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ReadOnly = True
        Me.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCode.Size = New System.Drawing.Size(710, 183)
        Me.txtCode.TabIndex = 12
        '
        'btnGenerate
        '
        Me.btnGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerate.Location = New System.Drawing.Point(634, 600)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(83, 24)
        Me.btnGenerate.TabIndex = 13
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ScreenToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(729, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.ToolStripMenuItem1.Text = "&File"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.FileToolStripMenuItem.Text = "E&xit"
        '
        'ScreenToolStripMenuItem
        '
        Me.ScreenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClearQueryCodeToolStripMenuItem, Me.ClearAllToolStripMenuItem})
        Me.ScreenToolStripMenuItem.Name = "ScreenToolStripMenuItem"
        Me.ScreenToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.ScreenToolStripMenuItem.Text = "Screen"
        '
        'ClearQueryCodeToolStripMenuItem
        '
        Me.ClearQueryCodeToolStripMenuItem.Name = "ClearQueryCodeToolStripMenuItem"
        Me.ClearQueryCodeToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ClearQueryCodeToolStripMenuItem.Text = "&Clear Query && Code"
        '
        'ClearAllToolStripMenuItem
        '
        Me.ClearAllToolStripMenuItem.Name = "ClearAllToolStripMenuItem"
        Me.ClearAllToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ClearAllToolStripMenuItem.Text = "Clear &All"
        '
        'dgvSchemaColumns
        '
        Me.dgvSchemaColumns.AllowUserToAddRows = False
        Me.dgvSchemaColumns.AllowUserToDeleteRows = False
        Me.dgvSchemaColumns.AllowUserToOrderColumns = True
        Me.dgvSchemaColumns.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSchemaColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSchemaColumns.Location = New System.Drawing.Point(9, 236)
        Me.dgvSchemaColumns.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvSchemaColumns.Name = "dgvSchemaColumns"
        Me.dgvSchemaColumns.ReadOnly = True
        Me.dgvSchemaColumns.RowTemplate.Height = 24
        Me.dgvSchemaColumns.Size = New System.Drawing.Size(709, 134)
        Me.dgvSchemaColumns.TabIndex = 10
        '
        'rbCS
        '
        Me.rbCS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbCS.AutoSize = True
        Me.rbCS.Location = New System.Drawing.Point(-4, 2)
        Me.rbCS.Margin = New System.Windows.Forms.Padding(2)
        Me.rbCS.Name = "rbCS"
        Me.rbCS.Size = New System.Drawing.Size(70, 17)
        Me.rbCS.TabIndex = 0
        Me.rbCS.Text = "Visual C#"
        Me.rbCS.UseVisualStyleBackColor = True
        '
        'txtObjectName
        '
        Me.txtObjectName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtObjectName.Location = New System.Drawing.Point(79, 191)
        Me.txtObjectName.Margin = New System.Windows.Forms.Padding(2)
        Me.txtObjectName.Name = "txtObjectName"
        Me.txtObjectName.Size = New System.Drawing.Size(521, 20)
        Me.txtObjectName.TabIndex = 8
        Me.txtObjectName.Text = "POCO"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 194)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Class Name:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.rbVB)
        Me.GroupBox1.Controls.Add(Me.rbCS)
        Me.GroupBox1.Location = New System.Drawing.Point(479, 602)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(150, 24)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'rbVB
        '
        Me.rbVB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbVB.AutoSize = True
        Me.rbVB.Checked = True
        Me.rbVB.Location = New System.Drawing.Point(70, 2)
        Me.rbVB.Margin = New System.Windows.Forms.Padding(2)
        Me.rbVB.Name = "rbVB"
        Me.rbVB.Size = New System.Drawing.Size(82, 17)
        Me.rbVB.TabIndex = 1
        Me.rbVB.TabStop = True
        Me.rbVB.Text = "Visual Basic"
        Me.rbVB.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.rbStoredProcedure)
        Me.GroupBox2.Controls.Add(Me.rbQuery)
        Me.GroupBox2.Location = New System.Drawing.Point(604, 114)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(113, 97)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ADO.NET Action"
        '
        'rbStoredProcedure
        '
        Me.rbStoredProcedure.AutoSize = True
        Me.rbStoredProcedure.Location = New System.Drawing.Point(6, 54)
        Me.rbStoredProcedure.Name = "rbStoredProcedure"
        Me.rbStoredProcedure.Size = New System.Drawing.Size(108, 17)
        Me.rbStoredProcedure.TabIndex = 1
        Me.rbStoredProcedure.Text = "Stored Procedure"
        Me.rbStoredProcedure.UseVisualStyleBackColor = True
        '
        'rbQuery
        '
        Me.rbQuery.AutoSize = True
        Me.rbQuery.Checked = True
        Me.rbQuery.Location = New System.Drawing.Point(7, 25)
        Me.rbQuery.Name = "rbQuery"
        Me.rbQuery.Size = New System.Drawing.Size(97, 17)
        Me.rbQuery.TabIndex = 0
        Me.rbQuery.TabStop = True
        Me.rbQuery.Text = "Dynamic Query"
        Me.rbQuery.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 220)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "SchemaTable:"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 383)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(201, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Generated Results Schema POCO Class:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 98)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Query Text:"
        '
        'cmdCnnString
        '
        Me.cmdCnnString.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCnnString.Location = New System.Drawing.Point(697, 44)
        Me.cmdCnnString.Name = "cmdCnnString"
        Me.cmdCnnString.Size = New System.Drawing.Size(25, 40)
        Me.cmdCnnString.TabIndex = 3
        Me.cmdCnnString.Text = "..."
        Me.cmdCnnString.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 630)
        Me.Controls.Add(Me.cmdCnnString)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtObjectName)
        Me.Controls.Add(Me.dgvSchemaColumns)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.txtQuery)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtConnectionString)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate Results Schema POCO Class - On VB - Visual Studio Magazine"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgvSchemaColumns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtConnectionString As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtQuery As System.Windows.Forms.TextBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ScreenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearQueryCodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvSchemaColumns As System.Windows.Forms.DataGridView
    Friend WithEvents rbCS As System.Windows.Forms.RadioButton
    Friend WithEvents txtObjectName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbVB As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbStoredProcedure As System.Windows.Forms.RadioButton
    Friend WithEvents rbQuery As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdCnnString As Button
End Class
