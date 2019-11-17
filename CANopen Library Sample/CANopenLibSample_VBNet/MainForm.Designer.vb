Partial Public Class MainForm
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.btn_Open = New System.Windows.Forms.Button()
        Me.txt_CommandHistory = New System.Windows.Forms.TextBox()
        Me.grp_CANBusMonitor = New System.Windows.Forms.GroupBox()
        Me.chk_ShowDrivetoPC = New System.Windows.Forms.CheckBox()
        Me.chk_ShowPCtoDrive = New System.Windows.Forms.CheckBox()
        Me.btn_Clear = New System.Windows.Forms.Button()
        Me.cmb_Adapter = New System.Windows.Forms.ComboBox()
        Me.btn_Disable = New System.Windows.Forms.Button()
        Me.btn_Enable = New System.Windows.Forms.Button()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.grp_Adapter = New System.Windows.Forms.GroupBox()
        Me.grp_BitRate = New System.Windows.Forms.GroupBox()
        Me.cmb_BitRate = New System.Windows.Forms.ComboBox()
        Me.btn_StopAll = New System.Windows.Forms.Button()
        Me.grp_CANBusMonitor.SuspendLayout()
        Me.grp_Adapter.SuspendLayout()
        Me.grp_BitRate.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_Open
        '
        Me.btn_Open.Location = New System.Drawing.Point(12, 19)
        Me.btn_Open.Name = "btn_Open"
        Me.btn_Open.Size = New System.Drawing.Size(60, 30)
        Me.btn_Open.TabIndex = 0
        Me.btn_Open.Text = "Open"
        Me.btn_Open.UseVisualStyleBackColor = True
        '
        'txt_CommandHistory
        '
        Me.txt_CommandHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_CommandHistory.Location = New System.Drawing.Point(6, 53)
        Me.txt_CommandHistory.Multiline = True
        Me.txt_CommandHistory.Name = "txt_CommandHistory"
        Me.txt_CommandHistory.ReadOnly = True
        Me.txt_CommandHistory.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txt_CommandHistory.Size = New System.Drawing.Size(557, 130)
        Me.txt_CommandHistory.TabIndex = 8
        '
        'grp_CANBusMonitor
        '
        Me.grp_CANBusMonitor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grp_CANBusMonitor.Controls.Add(Me.chk_ShowDrivetoPC)
        Me.grp_CANBusMonitor.Controls.Add(Me.chk_ShowPCtoDrive)
        Me.grp_CANBusMonitor.Controls.Add(Me.txt_CommandHistory)
        Me.grp_CANBusMonitor.Controls.Add(Me.btn_Clear)
        Me.grp_CANBusMonitor.Location = New System.Drawing.Point(12, 466)
        Me.grp_CANBusMonitor.Name = "grp_CANBusMonitor"
        Me.grp_CANBusMonitor.Size = New System.Drawing.Size(570, 190)
        Me.grp_CANBusMonitor.TabIndex = 9
        Me.grp_CANBusMonitor.TabStop = False
        Me.grp_CANBusMonitor.Text = "CAN Bus Monitor"
        '
        'chk_ShowDrivetoPC
        '
        Me.chk_ShowDrivetoPC.AutoSize = True
        Me.chk_ShowDrivetoPC.Checked = True
        Me.chk_ShowDrivetoPC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_ShowDrivetoPC.Location = New System.Drawing.Point(116, 19)
        Me.chk_ShowDrivetoPC.Name = "chk_ShowDrivetoPC"
        Me.chk_ShowDrivetoPC.Size = New System.Drawing.Size(132, 21)
        Me.chk_ShowDrivetoPC.TabIndex = 78
        Me.chk_ShowDrivetoPC.Text = "Show Drive->PC"
        Me.chk_ShowDrivetoPC.UseVisualStyleBackColor = True
        '
        'chk_ShowPCtoDrive
        '
        Me.chk_ShowPCtoDrive.AutoSize = True
        Me.chk_ShowPCtoDrive.Checked = True
        Me.chk_ShowPCtoDrive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_ShowPCtoDrive.Location = New System.Drawing.Point(6, 19)
        Me.chk_ShowPCtoDrive.Name = "chk_ShowPCtoDrive"
        Me.chk_ShowPCtoDrive.Size = New System.Drawing.Size(132, 21)
        Me.chk_ShowPCtoDrive.TabIndex = 77
        Me.chk_ShowPCtoDrive.Text = "Show PC->Drive"
        Me.chk_ShowPCtoDrive.UseVisualStyleBackColor = True
        '
        'btn_Clear
        '
        Me.btn_Clear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Clear.Location = New System.Drawing.Point(489, 19)
        Me.btn_Clear.Name = "btn_Clear"
        Me.btn_Clear.Size = New System.Drawing.Size(75, 25)
        Me.btn_Clear.TabIndex = 10
        Me.btn_Clear.Text = "Clear"
        Me.btn_Clear.UseVisualStyleBackColor = True
        '
        'cmb_Adapter
        '
        Me.cmb_Adapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Adapter.FormattingEnabled = True
        Me.cmb_Adapter.Items.AddRange(New Object() {"GC_USBCAN1"})
        Me.cmb_Adapter.Location = New System.Drawing.Point(7, 22)
        Me.cmb_Adapter.Name = "cmb_Adapter"
        Me.cmb_Adapter.Size = New System.Drawing.Size(116, 25)
        Me.cmb_Adapter.TabIndex = 13
        '
        'btn_Disable
        '
        Me.btn_Disable.Enabled = False
        Me.btn_Disable.Location = New System.Drawing.Point(348, 14)
        Me.btn_Disable.Name = "btn_Disable"
        Me.btn_Disable.Size = New System.Drawing.Size(50, 41)
        Me.btn_Disable.TabIndex = 108
        Me.btn_Disable.Text = "Disable"
        Me.btn_Disable.UseVisualStyleBackColor = True
        '
        'btn_Enable
        '
        Me.btn_Enable.Enabled = False
        Me.btn_Enable.Location = New System.Drawing.Point(290, 14)
        Me.btn_Enable.Name = "btn_Enable"
        Me.btn_Enable.Size = New System.Drawing.Size(50, 41)
        Me.btn_Enable.TabIndex = 106
        Me.btn_Enable.Text = "Enable"
        Me.btn_Enable.UseVisualStyleBackColor = True
        '
        'btn_Close
        '
        Me.btn_Close.Enabled = False
        Me.btn_Close.Location = New System.Drawing.Point(82, 20)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(60, 30)
        Me.btn_Close.TabIndex = 104
        Me.btn_Close.Text = "Close"
        Me.btn_Close.UseVisualStyleBackColor = True
        '
        'grp_Adapter
        '
        Me.grp_Adapter.Controls.Add(Me.cmb_Adapter)
        Me.grp_Adapter.Location = New System.Drawing.Point(148, 28)
        Me.grp_Adapter.Name = "grp_Adapter"
        Me.grp_Adapter.Size = New System.Drawing.Size(136, 49)
        Me.grp_Adapter.TabIndex = 101
        Me.grp_Adapter.TabStop = False
        Me.grp_Adapter.Text = "Adapter"
        '
        'grp_BitRate
        '
        Me.grp_BitRate.Controls.Add(Me.cmb_BitRate)
        Me.grp_BitRate.Location = New System.Drawing.Point(12, 56)
        Me.grp_BitRate.Name = "grp_BitRate"
        Me.grp_BitRate.Size = New System.Drawing.Size(119, 44)
        Me.grp_BitRate.TabIndex = 115
        Me.grp_BitRate.TabStop = False
        Me.grp_BitRate.Text = "Bit Rate"
        '
        'cmb_BitRate
        '
        Me.cmb_BitRate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_BitRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_BitRate.FormattingEnabled = True
        Me.cmb_BitRate.Items.AddRange(New Object() {"1 Mbps", "800 kbps", "500 kbps", "250 kbps", "125 kbps", "50 kbps", "20 kbps", "12.5 kbps"})
        Me.cmb_BitRate.Location = New System.Drawing.Point(10, 16)
        Me.cmb_BitRate.Name = "cmb_BitRate"
        Me.cmb_BitRate.Size = New System.Drawing.Size(107, 25)
        Me.cmb_BitRate.TabIndex = 3
        '
        'btn_StopAll
        '
        Me.btn_StopAll.Enabled = False
        Me.btn_StopAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_StopAll.Image = Global.MOONS.CANopenLibSample_VB.My.Resources.Resources._Stop
        Me.btn_StopAll.Location = New System.Drawing.Point(486, 14)
        Me.btn_StopAll.Name = "btn_StopAll"
        Me.btn_StopAll.Size = New System.Drawing.Size(95, 63)
        Me.btn_StopAll.TabIndex = 111
        Me.btn_StopAll.Text = "STOP"
        Me.btn_StopAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_StopAll.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(594, 668)
        Me.Controls.Add(Me.grp_BitRate)
        Me.Controls.Add(Me.btn_Close)
        Me.Controls.Add(Me.btn_Open)
        Me.Controls.Add(Me.btn_StopAll)
        Me.Controls.Add(Me.grp_Adapter)
        Me.Controls.Add(Me.btn_Enable)
        Me.Controls.Add(Me.btn_Disable)
        Me.Controls.Add(Me.grp_CANBusMonitor)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CANopenLib Sample(VB.NET)"
        Me.grp_CANBusMonitor.ResumeLayout(False)
        Me.grp_CANBusMonitor.PerformLayout()
        Me.grp_Adapter.ResumeLayout(False)
        Me.grp_BitRate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents btn_Open As System.Windows.Forms.Button
	Private txt_CommandHistory As System.Windows.Forms.TextBox
	Private grp_CANBusMonitor As System.Windows.Forms.GroupBox
	Private WithEvents btn_Clear As System.Windows.Forms.Button
	Private cmb_Adapter As System.Windows.Forms.ComboBox
    Private WithEvents btn_StopAll As System.Windows.Forms.Button
    Private WithEvents btn_Disable As System.Windows.Forms.Button
    Private WithEvents btn_Enable As System.Windows.Forms.Button
    Private WithEvents btn_Close As System.Windows.Forms.Button
    Private grp_Adapter As System.Windows.Forms.GroupBox
    Private chk_ShowDrivetoPC As System.Windows.Forms.CheckBox
    Private chk_ShowPCtoDrive As System.Windows.Forms.CheckBox
    Private grp_BitRate As System.Windows.Forms.GroupBox
    Private cmb_BitRate As System.Windows.Forms.ComboBox
End Class
