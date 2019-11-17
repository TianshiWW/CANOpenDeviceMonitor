namespace CANopenLib
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_Open = new System.Windows.Forms.Button();
            this.txt_CommandHistory = new System.Windows.Forms.TextBox();
            this.grp_CANBusMonitor = new System.Windows.Forms.GroupBox();
            this.chk_ShowDrivetoPC = new System.Windows.Forms.CheckBox();
            this.chk_ShowPCtoDrive = new System.Windows.Forms.CheckBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.cmb_Adapter = new System.Windows.Forms.ComboBox();
            this.btn_Disable = new System.Windows.Forms.Button();
            this.btn_Enable = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.grp_Adapter = new System.Windows.Forms.GroupBox();
            this.grp_BitRate = new System.Windows.Forms.GroupBox();
            this.cmb_BitRate = new System.Windows.Forms.ComboBox();
            this.tmr_Monitor = new System.Windows.Forms.Timer(this.components);
            this.write_SDO_Btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Node_Id_Input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Function_ID = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.text_Data_7 = new System.Windows.Forms.TextBox();
            this.text_Data_1 = new System.Windows.Forms.TextBox();
            this.text_Data_6 = new System.Windows.Forms.TextBox();
            this.text_Data_0 = new System.Windows.Forms.TextBox();
            this.text_Data_5 = new System.Windows.Forms.TextBox();
            this.text_Data_2 = new System.Windows.Forms.TextBox();
            this.text_Data_4 = new System.Windows.Forms.TextBox();
            this.text_Data_3 = new System.Windows.Forms.TextBox();
            this.Clear_Bnt = new System.Windows.Forms.Button();
            this.Scan_Btn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Index_Input_1 = new System.Windows.Forms.TextBox();
            this.Sub_Index_Input = new System.Windows.Forms.TextBox();
            this.Index_Input = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Node_ID_Block = new System.Windows.Forms.TextBox();
            this.SDO_Block_Send_Btn = new System.Windows.Forms.Button();
            this.SDO_Block_Init_Btn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_StopAll = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.StartUp_TextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Read_File = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grp_CANBusMonitor.SuspendLayout();
            this.grp_Adapter.SuspendLayout();
            this.grp_BitRate.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(203, 21);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(60, 30);
            this.btn_Open.TabIndex = 0;
            this.btn_Open.Text = "Open";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // txt_CommandHistory
            // 
            this.txt_CommandHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CommandHistory.Location = new System.Drawing.Point(6, 46);
            this.txt_CommandHistory.Multiline = true;
            this.txt_CommandHistory.Name = "txt_CommandHistory";
            this.txt_CommandHistory.ReadOnly = true;
            this.txt_CommandHistory.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_CommandHistory.Size = new System.Drawing.Size(825, 268);
            this.txt_CommandHistory.TabIndex = 8;
            // 
            // grp_CANBusMonitor
            // 
            this.grp_CANBusMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_CANBusMonitor.Controls.Add(this.chk_ShowDrivetoPC);
            this.grp_CANBusMonitor.Controls.Add(this.chk_ShowPCtoDrive);
            this.grp_CANBusMonitor.Controls.Add(this.txt_CommandHistory);
            this.grp_CANBusMonitor.Controls.Add(this.btn_Clear);
            this.grp_CANBusMonitor.Location = new System.Drawing.Point(31, 298);
            this.grp_CANBusMonitor.Name = "grp_CANBusMonitor";
            this.grp_CANBusMonitor.Size = new System.Drawing.Size(838, 299);
            this.grp_CANBusMonitor.TabIndex = 9;
            this.grp_CANBusMonitor.TabStop = false;
            this.grp_CANBusMonitor.Text = "CAN Bus Monitor";
            // 
            // chk_ShowDrivetoPC
            // 
            this.chk_ShowDrivetoPC.AutoSize = true;
            this.chk_ShowDrivetoPC.Checked = true;
            this.chk_ShowDrivetoPC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ShowDrivetoPC.Location = new System.Drawing.Point(116, 19);
            this.chk_ShowDrivetoPC.Name = "chk_ShowDrivetoPC";
            this.chk_ShowDrivetoPC.Size = new System.Drawing.Size(132, 21);
            this.chk_ShowDrivetoPC.TabIndex = 78;
            this.chk_ShowDrivetoPC.Text = "Show Drive->PC";
            this.chk_ShowDrivetoPC.UseVisualStyleBackColor = true;
            // 
            // chk_ShowPCtoDrive
            // 
            this.chk_ShowPCtoDrive.AutoSize = true;
            this.chk_ShowPCtoDrive.Checked = true;
            this.chk_ShowPCtoDrive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ShowPCtoDrive.Location = new System.Drawing.Point(6, 19);
            this.chk_ShowPCtoDrive.Name = "chk_ShowPCtoDrive";
            this.chk_ShowPCtoDrive.Size = new System.Drawing.Size(132, 21);
            this.chk_ShowPCtoDrive.TabIndex = 77;
            this.chk_ShowPCtoDrive.Text = "Show PC->Drive";
            this.chk_ShowPCtoDrive.UseVisualStyleBackColor = true;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Clear.Location = new System.Drawing.Point(757, 19);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 25);
            this.btn_Clear.TabIndex = 10;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // cmb_Adapter
            // 
            this.cmb_Adapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Adapter.FormattingEnabled = true;
            this.cmb_Adapter.Items.AddRange(new object[] {
            "USBCAN1"});
            this.cmb_Adapter.Location = new System.Drawing.Point(6, 22);
            this.cmb_Adapter.Name = "cmb_Adapter";
            this.cmb_Adapter.Size = new System.Drawing.Size(124, 25);
            this.cmb_Adapter.TabIndex = 13;
            // 
            // btn_Disable
            // 
            this.btn_Disable.Enabled = false;
            this.btn_Disable.Location = new System.Drawing.Point(181, 31);
            this.btn_Disable.Name = "btn_Disable";
            this.btn_Disable.Size = new System.Drawing.Size(50, 41);
            this.btn_Disable.TabIndex = 108;
            this.btn_Disable.Text = "Disable";
            this.btn_Disable.UseVisualStyleBackColor = true;
            this.btn_Disable.Click += new System.EventHandler(this.btn_Disable_Click);
            // 
            // btn_Enable
            // 
            this.btn_Enable.Enabled = false;
            this.btn_Enable.Location = new System.Drawing.Point(237, 30);
            this.btn_Enable.Name = "btn_Enable";
            this.btn_Enable.Size = new System.Drawing.Size(50, 41);
            this.btn_Enable.TabIndex = 106;
            this.btn_Enable.Text = "Enable";
            this.btn_Enable.UseVisualStyleBackColor = true;
            this.btn_Enable.Click += new System.EventHandler(this.btn_Enable_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Enabled = false;
            this.btn_Close.Location = new System.Drawing.Point(203, 74);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(60, 30);
            this.btn_Close.TabIndex = 104;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // grp_Adapter
            // 
            this.grp_Adapter.Controls.Add(this.cmb_Adapter);
            this.grp_Adapter.Location = new System.Drawing.Point(21, 55);
            this.grp_Adapter.Name = "grp_Adapter";
            this.grp_Adapter.Size = new System.Drawing.Size(138, 49);
            this.grp_Adapter.TabIndex = 101;
            this.grp_Adapter.TabStop = false;
            this.grp_Adapter.Text = "Adapter";
            // 
            // grp_BitRate
            // 
            this.grp_BitRate.Controls.Add(this.cmb_BitRate);
            this.grp_BitRate.Location = new System.Drawing.Point(21, 4);
            this.grp_BitRate.Name = "grp_BitRate";
            this.grp_BitRate.Size = new System.Drawing.Size(138, 47);
            this.grp_BitRate.TabIndex = 115;
            this.grp_BitRate.TabStop = false;
            this.grp_BitRate.Text = "Bit Rate";
            // 
            // cmb_BitRate
            // 
            this.cmb_BitRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_BitRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_BitRate.FormattingEnabled = true;
            this.cmb_BitRate.Items.AddRange(new object[] {
            "1 Mbps",
            "800 kbps",
            "500 kbps",
            "250 kbps",
            "125 kbps",
            "50 kbps",
            "20 kbps",
            "12.5 kbps"});
            this.cmb_BitRate.Location = new System.Drawing.Point(6, 22);
            this.cmb_BitRate.Name = "cmb_BitRate";
            this.cmb_BitRate.Size = new System.Drawing.Size(126, 25);
            this.cmb_BitRate.TabIndex = 3;
            // 
            // write_SDO_Btn
            // 
            this.write_SDO_Btn.Enabled = false;
            this.write_SDO_Btn.Location = new System.Drawing.Point(324, 114);
            this.write_SDO_Btn.Name = "write_SDO_Btn";
            this.write_SDO_Btn.Size = new System.Drawing.Size(66, 61);
            this.write_SDO_Btn.TabIndex = 116;
            this.write_SDO_Btn.Text = "Write";
            this.write_SDO_Btn.UseVisualStyleBackColor = true;
            this.write_SDO_Btn.Click += new System.EventHandler(this.write_SDO_Btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 114);
            this.textBox1.MaxLength = 356950000;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(298, 122);
            this.textBox1.TabIndex = 117;
            // 
            // Node_Id_Input
            // 
            this.Node_Id_Input.Enabled = false;
            this.Node_Id_Input.Location = new System.Drawing.Point(20, 24);
            this.Node_Id_Input.Name = "Node_Id_Input";
            this.Node_Id_Input.Size = new System.Drawing.Size(124, 23);
            this.Node_Id_Input.TabIndex = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 121;
            this.label1.Text = "NodeID";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Function_ID);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.Clear_Bnt);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Node_Id_Input);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.write_SDO_Btn);
            this.panel1.Location = new System.Drawing.Point(31, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 252);
            this.panel1.TabIndex = 124;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 134;
            this.label2.Text = "Function_ID";
            // 
            // Function_ID
            // 
            this.Function_ID.Enabled = false;
            this.Function_ID.Location = new System.Drawing.Point(165, 24);
            this.Function_ID.Name = "Function_ID";
            this.Function_ID.Size = new System.Drawing.Size(124, 23);
            this.Function_ID.TabIndex = 133;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.text_Data_7);
            this.panel3.Controls.Add(this.text_Data_1);
            this.panel3.Controls.Add(this.text_Data_6);
            this.panel3.Controls.Add(this.text_Data_0);
            this.panel3.Controls.Add(this.text_Data_5);
            this.panel3.Controls.Add(this.text_Data_2);
            this.panel3.Controls.Add(this.text_Data_4);
            this.panel3.Controls.Add(this.text_Data_3);
            this.panel3.Location = new System.Drawing.Point(20, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(323, 55);
            this.panel3.TabIndex = 132;
            // 
            // text_Data_7
            // 
            this.text_Data_7.Location = new System.Drawing.Point(247, 29);
            this.text_Data_7.Name = "text_Data_7";
            this.text_Data_7.Size = new System.Drawing.Size(28, 23);
            this.text_Data_7.TabIndex = 132;
            // 
            // text_Data_1
            // 
            this.text_Data_1.Location = new System.Drawing.Point(42, 29);
            this.text_Data_1.Name = "text_Data_1";
            this.text_Data_1.Size = new System.Drawing.Size(28, 23);
            this.text_Data_1.TabIndex = 126;
            // 
            // text_Data_6
            // 
            this.text_Data_6.Location = new System.Drawing.Point(213, 29);
            this.text_Data_6.Name = "text_Data_6";
            this.text_Data_6.Size = new System.Drawing.Size(28, 23);
            this.text_Data_6.TabIndex = 131;
            // 
            // text_Data_0
            // 
            this.text_Data_0.Location = new System.Drawing.Point(8, 29);
            this.text_Data_0.Name = "text_Data_0";
            this.text_Data_0.Size = new System.Drawing.Size(28, 23);
            this.text_Data_0.TabIndex = 125;
            // 
            // text_Data_5
            // 
            this.text_Data_5.Location = new System.Drawing.Point(179, 29);
            this.text_Data_5.Name = "text_Data_5";
            this.text_Data_5.Size = new System.Drawing.Size(28, 23);
            this.text_Data_5.TabIndex = 130;
            // 
            // text_Data_2
            // 
            this.text_Data_2.Location = new System.Drawing.Point(76, 29);
            this.text_Data_2.Name = "text_Data_2";
            this.text_Data_2.Size = new System.Drawing.Size(28, 23);
            this.text_Data_2.TabIndex = 127;
            // 
            // text_Data_4
            // 
            this.text_Data_4.Location = new System.Drawing.Point(145, 29);
            this.text_Data_4.Name = "text_Data_4";
            this.text_Data_4.Size = new System.Drawing.Size(28, 23);
            this.text_Data_4.TabIndex = 129;
            // 
            // text_Data_3
            // 
            this.text_Data_3.Location = new System.Drawing.Point(110, 29);
            this.text_Data_3.Name = "text_Data_3";
            this.text_Data_3.Size = new System.Drawing.Size(28, 23);
            this.text_Data_3.TabIndex = 128;
            // 
            // Clear_Bnt
            // 
            this.Clear_Bnt.Enabled = false;
            this.Clear_Bnt.Location = new System.Drawing.Point(324, 181);
            this.Clear_Bnt.Name = "Clear_Bnt";
            this.Clear_Bnt.Size = new System.Drawing.Size(66, 55);
            this.Clear_Bnt.TabIndex = 124;
            this.Clear_Bnt.Text = "Clear";
            this.Clear_Bnt.UseVisualStyleBackColor = true;
            this.Clear_Bnt.Click += new System.EventHandler(this.Clear_Bnt_Click);
            // 
            // Scan_Btn
            // 
            this.Scan_Btn.Location = new System.Drawing.Point(21, 20);
            this.Scan_Btn.Name = "Scan_Btn";
            this.Scan_Btn.Size = new System.Drawing.Size(131, 63);
            this.Scan_Btn.TabIndex = 126;
            this.Scan_Btn.Text = "Scan";
            this.Scan_Btn.UseVisualStyleBackColor = true;
            this.Scan_Btn.Click += new System.EventHandler(this.Scan_Btn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Read_File);
            this.panel2.Controls.Add(this.Index_Input_1);
            this.panel2.Controls.Add(this.Sub_Index_Input);
            this.panel2.Controls.Add(this.Index_Input);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.Node_ID_Block);
            this.panel2.Controls.Add(this.SDO_Block_Send_Btn);
            this.panel2.Controls.Add(this.SDO_Block_Init_Btn);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel2.Location = new System.Drawing.Point(496, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(373, 252);
            this.panel2.TabIndex = 127;
            // 
            // Index_Input_1
            // 
            this.Index_Input_1.Enabled = false;
            this.Index_Input_1.Location = new System.Drawing.Point(260, 81);
            this.Index_Input_1.Name = "Index_Input_1";
            this.Index_Input_1.Size = new System.Drawing.Size(38, 23);
            this.Index_Input_1.TabIndex = 131;
            // 
            // Sub_Index_Input
            // 
            this.Sub_Index_Input.Enabled = false;
            this.Sub_Index_Input.Location = new System.Drawing.Point(304, 81);
            this.Sub_Index_Input.Name = "Sub_Index_Input";
            this.Sub_Index_Input.Size = new System.Drawing.Size(52, 23);
            this.Sub_Index_Input.TabIndex = 130;
            // 
            // Index_Input
            // 
            this.Index_Input.Enabled = false;
            this.Index_Input.Location = new System.Drawing.Point(217, 81);
            this.Index_Input.Name = "Index_Input";
            this.Index_Input.Size = new System.Drawing.Size(38, 23);
            this.Index_Input.TabIndex = 129;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(301, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 17);
            this.label5.TabIndex = 128;
            this.label5.Text = "Sub Index";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 127;
            this.label4.Text = "Index";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 123;
            this.label3.Text = "NodeID";
            // 
            // Node_ID_Block
            // 
            this.Node_ID_Block.Enabled = false;
            this.Node_ID_Block.Location = new System.Drawing.Point(217, 24);
            this.Node_ID_Block.Name = "Node_ID_Block";
            this.Node_ID_Block.Size = new System.Drawing.Size(139, 23);
            this.Node_ID_Block.TabIndex = 122;
            // 
            // SDO_Block_Send_Btn
            // 
            this.SDO_Block_Send_Btn.Enabled = false;
            this.SDO_Block_Send_Btn.Location = new System.Drawing.Point(217, 197);
            this.SDO_Block_Send_Btn.Name = "SDO_Block_Send_Btn";
            this.SDO_Block_Send_Btn.Size = new System.Drawing.Size(139, 39);
            this.SDO_Block_Send_Btn.TabIndex = 2;
            this.SDO_Block_Send_Btn.Text = "SDO Block Send";
            this.SDO_Block_Send_Btn.UseVisualStyleBackColor = true;
            this.SDO_Block_Send_Btn.Click += new System.EventHandler(this.SDO_Block_Send_Btn_Click);
            // 
            // SDO_Block_Init_Btn
            // 
            this.SDO_Block_Init_Btn.Enabled = false;
            this.SDO_Block_Init_Btn.Location = new System.Drawing.Point(217, 153);
            this.SDO_Block_Init_Btn.Name = "SDO_Block_Init_Btn";
            this.SDO_Block_Init_Btn.Size = new System.Drawing.Size(139, 35);
            this.SDO_Block_Init_Btn.TabIndex = 1;
            this.SDO_Block_Init_Btn.Text = "SDO Block Init";
            this.SDO_Block_Init_Btn.UseVisualStyleBackColor = true;
            this.SDO_Block_Init_Btn.Click += new System.EventHandler(this.SDO_Block_Init_Btn_Click);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(17, 21);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(191, 215);
            this.textBox2.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CANopenLib.Properties.Resources.施耐德;
            this.pictureBox1.Location = new System.Drawing.Point(764, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 77);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 125;
            this.pictureBox1.TabStop = false;
            // 
            // btn_StopAll
            // 
            this.btn_StopAll.Enabled = false;
            this.btn_StopAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StopAll.Image = global::CANopenLib.Properties.Resources.Stop;
            this.btn_StopAll.Location = new System.Drawing.Point(302, 20);
            this.btn_StopAll.Name = "btn_StopAll";
            this.btn_StopAll.Size = new System.Drawing.Size(95, 63);
            this.btn_StopAll.TabIndex = 111;
            this.btn_StopAll.Text = "STOP";
            this.btn_StopAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_StopAll.UseVisualStyleBackColor = true;
            this.btn_StopAll.Click += new System.EventHandler(this.btn_StopAll_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.Scan_Btn);
            this.panel4.Controls.Add(this.btn_StopAll);
            this.panel4.Controls.Add(this.btn_Enable);
            this.panel4.Controls.Add(this.btn_Disable);
            this.panel4.Location = new System.Drawing.Point(265, 155);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(421, 118);
            this.panel4.TabIndex = 128;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Location = new System.Drawing.Point(15, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(896, 121);
            this.panel5.TabIndex = 129;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btn_Open);
            this.panel6.Controls.Add(this.btn_Close);
            this.panel6.Controls.Add(this.grp_BitRate);
            this.panel6.Controls.Add(this.grp_Adapter);
            this.panel6.Location = new System.Drawing.Point(322, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(278, 115);
            this.panel6.TabIndex = 126;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(19, 21);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(925, 648);
            this.TabControl.TabIndex = 130;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.StartUp_TextBox);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(917, 618);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "StartUp";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(779, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 48);
            this.button1.TabIndex = 131;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StartUp_TextBox
            // 
            this.StartUp_TextBox.Location = new System.Drawing.Point(265, 303);
            this.StartUp_TextBox.Multiline = true;
            this.StartUp_TextBox.Name = "StartUp_TextBox";
            this.StartUp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StartUp_TextBox.Size = new System.Drawing.Size(427, 309);
            this.StartUp_TextBox.TabIndex = 130;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.grp_CANBusMonitor);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(917, 618);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SDO";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Read_File
            // 
            this.Read_File.Enabled = false;
            this.Read_File.Location = new System.Drawing.Point(217, 114);
            this.Read_File.Name = "Read_File";
            this.Read_File.Size = new System.Drawing.Size(139, 33);
            this.Read_File.TabIndex = 128;
            this.Read_File.Text = "Read File";
            this.Read_File.UseVisualStyleBackColor = true;
            this.Read_File.Click += new System.EventHandler(this.Read_File_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "二进制文件|*.bin|文本文件|*.txt\"";
            this.openFileDialog1.InitialDirectory = "c:\\Users\\wang\\Desktop\\test";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 17);
            this.label6.TabIndex = 128;
            this.label6.Text = "CanMessage Send";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(543, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 17);
            this.label7.TabIndex = 132;
            this.label7.Text = "SDO Block Transfer";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(974, 689);
            this.Controls.Add(this.TabControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Canopen_Upgrader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.CANopenLibTestForm_Load);
            this.grp_CANBusMonitor.ResumeLayout(false);
            this.grp_CANBusMonitor.PerformLayout();
            this.grp_Adapter.ResumeLayout(false);
            this.grp_BitRate.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.TextBox txt_CommandHistory;
        private System.Windows.Forms.GroupBox grp_CANBusMonitor;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.ComboBox cmb_Adapter;
        private System.Windows.Forms.Button btn_StopAll;
        private System.Windows.Forms.Button btn_Disable;
        private System.Windows.Forms.Button btn_Enable;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.GroupBox grp_Adapter;
        private System.Windows.Forms.CheckBox chk_ShowDrivetoPC;
        private System.Windows.Forms.CheckBox chk_ShowPCtoDrive;
        private System.Windows.Forms.GroupBox grp_BitRate;
        private System.Windows.Forms.ComboBox cmb_BitRate;
        private System.Windows.Forms.Timer tmr_Monitor;
        private System.Windows.Forms.Button write_SDO_Btn;            
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox Node_Id_Input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Clear_Bnt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Scan_Btn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button SDO_Block_Init_Btn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button SDO_Block_Send_Btn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox text_Data_7;
        private System.Windows.Forms.TextBox text_Data_1;
        private System.Windows.Forms.TextBox text_Data_6;
        private System.Windows.Forms.TextBox text_Data_0;
        private System.Windows.Forms.TextBox text_Data_5;
        private System.Windows.Forms.TextBox text_Data_2;
        private System.Windows.Forms.TextBox text_Data_4;
        private System.Windows.Forms.TextBox text_Data_3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Function_ID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Node_ID_Block;
        private System.Windows.Forms.TextBox Sub_Index_Input;
        private System.Windows.Forms.TextBox Index_Input;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Index_Input_1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox StartUp_TextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Read_File;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}