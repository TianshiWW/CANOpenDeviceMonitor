
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace CANopenLib
{
    public partial class MainForm : Form
    {
        private CANopenLibHelper m_CANopenLibHelper = new CANopenLibHelper();

        private byte m_NodeID = 0x01;
        private List<byte> data = new List<byte>();
        /// <summary>
        /// Set Action Enable
        /// </summary>
        private void SetActionEnable()
        {
            bool _IsOpen = m_CANopenLibHelper.IsOpen();
            this.btn_Open.Enabled = !_IsOpen;
            this.btn_Close.Enabled = !_IsOpen;
            this.cmb_Adapter.Enabled = !_IsOpen;
            this.cmb_BitRate.Enabled = !_IsOpen;
            this.Clear_Bnt.Enabled = _IsOpen;
            //this.Index_Input.Enabled = _IsOpen;
            //this.SubIndex_Input.Enabled = _IsOpen;
            this.Read_File.Enabled = _IsOpen;
            this.SDO_Block_Init_Btn.Enabled = _IsOpen;
            this.SDO_Block_Send_Btn.Enabled = _IsOpen;
            this.textBox2.Enabled = _IsOpen;
            this.Function_ID.Enabled = _IsOpen;
            this.Node_Id_Input.Enabled = _IsOpen;
            this.Node_ID_Block.Enabled = _IsOpen;
            this.Index_Input.Enabled = _IsOpen;
            this.Index_Input_1.Enabled = _IsOpen;
            this.Sub_Index_Input.Enabled = _IsOpen;
            this.write_SDO_Btn.Enabled = _IsOpen;
            this.btn_Enable.Enabled = _IsOpen;
            this.btn_Disable.Enabled = _IsOpen;
            this.btn_Close.Enabled = _IsOpen;
            //this.btn_PreOperational.Enabled = _IsOpen;
            //this.btn_Operational.Enabled = _IsOpen;
            //this.btn_AlarmReset.Enabled = _IsOpen;
            this.btn_StopAll.Enabled = _IsOpen;



        }

        public void DisplayCanMessage(bool bDirIsToDrive, CanMessage canMessage)
        {
            string text = string.Empty;
            if (bDirIsToDrive)
            {
                if (this.chk_ShowPCtoDrive.Checked == false)
                {
                    return;
                }
                text = string.Format("PC->Drive: Msg={0:X3} Node ID={1:X2} Len={2:X} Data={3:X2} {4:X2} {5:X2} {6:X2} {7:X2} {8:X2} {9:X2} {10:X2} Time Stamp={11:D8}\r\n",
                    (canMessage.id & 0x780),
                    (canMessage.id & 0x7F),
                    canMessage.dlc,
                    canMessage.msg[0],
                    canMessage.msg[1],
                    canMessage.msg[2],
                    canMessage.msg[3],
                    canMessage.msg[4],
                    canMessage.msg[5],
                    canMessage.msg[6],
                    canMessage.msg[7],
                    canMessage.timeStamp);
            }
            else
            {
                if (this.chk_ShowDrivetoPC.Checked == false)
                {
                    return;
                }
                text = string.Format("Drive->PC: Msg={0:X3} Node ID={1:X2} Len={2:X} Data={3:X2} {4:X2} {5:X2} {6:X2} {7:X2} {8:X2} {9:X2} {10:X2} Time Stamp={11:D8}\r\n",
                    (canMessage.id & 0x780),
                    (canMessage.id & 0x7F),
                    canMessage.dlc,
                    canMessage.msg[0],
                    canMessage.msg[1],
                    canMessage.msg[2],
                    canMessage.msg[3],
                    canMessage.msg[4],
                    canMessage.msg[5],
                    canMessage.msg[6],
                    canMessage.msg[7],
                    canMessage.timeStamp);
            }
            AppendControlText(this.txt_CommandHistory, text);
        }

        /// <summary>
        /// SendCanMessage
        /// </summary>
        /// <param name="CanMessage">canMessage</param>
        /// <returns>Return true if write command successfully, otherwise return false</returns>
        public bool SendCanMessage(CanMessage canMessage)
        {
            bool ret = m_CANopenLibHelper.Write(canMessage);

            if (!ret)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// SendCanMessage
        /// </summary>
        /// <param name="CanMessage">canMessage</param>
        /// <returns>Return true if write command successfully, otherwise return false</returns>
        public bool ExecuteCanMessage(CanMessage sSendCanMessage, int canFunction, ref CanMessage sReceivedCanMessage, bool bMatchNodeID, byte nNodeID,
            bool bMatchIndex = false,
            int nIndex = 0,
            bool bMatchFirstByte = false,
            byte nFirstByte = 0,
            int nTimeOut = 30)
        {
            bool ret = m_CANopenLibHelper.ExecuteCommand(sSendCanMessage, ref sReceivedCanMessage, canFunction, bMatchNodeID, nNodeID, bMatchIndex, nIndex, bMatchFirstByte, nFirstByte);

            if (!ret)
            {
                return false;
            }
            return true;
        }




        public MainForm()
        {
            InitializeComponent();
        }

        private void CANopenLibTestForm_Load(object sender, EventArgs e)
        {
            this.cmb_Adapter.SelectedIndex = 0;
            this.cmb_BitRate.SelectedIndex = 0;

            m_CANopenLibHelper.DataSent += new CANopenLibHelper.OnCanMessageEventHandler(m_CANopenLibHelper_DataSent);
            m_CANopenLibHelper.DataReceived += new CANopenLibHelper.OnCanMessageEventHandler(m_CANopenLibHelper_DataReceived);

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
        }

        private delegate void m_dSetControlText(Control control, string text);

        private void ThreadSetControlText(Control control, string text)
        {
            control.Text = text;
        }

        private void SetControlText(Control control, string text)
        {
            if (control.InvokeRequired == false)
            {
                ThreadSetControlText(control, text);
            }
            else
            {
                m_dSetControlText d = new m_dSetControlText(ThreadSetControlText);
                control.BeginInvoke(d, new object[] { control, text });
            }
        }

        private delegate void m_dAppendControlText(TextBox textBox, string text);

        private void ThreadAppendControlText(TextBox textBox, string text)
        {
            textBox.AppendText(text);
        }

        private void AppendControlText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired == false)
            {
                ThreadAppendControlText(textBox, text);
            }
            else
            {
                m_dAppendControlText d = new m_dAppendControlText(ThreadAppendControlText);
                textBox.BeginInvoke(d, new object[] { textBox, text });
            }
        }


        void m_CANopenLibHelper_DataSent(CanMessageEventHandle e)
        {
            DisplayCanMessage(true, e.CanMessage);
        }

        void m_CANopenLibHelper_DataReceived(CanMessageEventHandle e)
        {
            DisplayCanMessage(false, e.CanMessage);
        }

        public List<byte> nNodeIDList = new List<byte>();
        void m_CANopenLibHelper_TPDOReceived(TPDOReceivedEventHandle e)
        {

        }

        private void GetFirmwareRevision()
        {
            m_NodeID = 1;
            //m_NodeID = 126;
            CanMessage sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, 0x0A, 0x10, 0, 0, 0, 0, 0 } };
            CanMessage receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };

            int n = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CanMessage));

            bool ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);

            if (ret == true)
            {
                string m_ARMRevision = System.Text.ASCIIEncoding.Default.GetString(new byte[] { receivedCanMessage.msg[4], receivedCanMessage.msg[5], receivedCanMessage.msg[6], receivedCanMessage.msg[7] }).Insert(1, ".");

                //SetControlText(this.lbl_ARMRev, m_ARMRevision);
            }
            else
            {
                //SetControlText(this.lbl_ARMRev, string.Empty);
            }

            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, 0x18, 0x10, 0x02, 0, 0, 0, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);

            if (ret == true)
            {
                string m_DSPRevision = string.Format("1.0{0}{1}", (char)(receivedCanMessage.msg[6] + 48), (char)(receivedCanMessage.msg[4]));

                //SetControlText(this.lbl_DSPRev, m_DSPRevision);
            }
            else
            {
                //SetControlText(this.lbl_DSPRev, string.Empty);
            }
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            bool ret = true;
            //textBox1.Text = this.cmb_Adapter.SelectedIndex.ToString();
            if (this.cmb_Adapter.SelectedIndex == 0)
            {
                textBox1.Text = this.cmb_Adapter.SelectedIndex.ToString();
                //ret = m_CANopenLibHelper.Open(EnumAdapter.ZLG, EnumBaudRate.BitRate1Mbps, (int)ZLGDeviceType.USBCAN1);
                ret = m_CANopenLibHelper.Open(EnumAdapter.ZLG, EnumBaudRate.BitRate1Mbps, (int)ZLGDeviceType.USBCAN1);
            }

            //textBox1.Text = ret.ToString();
            if (ret == false)
            {
                MessageBox.Show("Fail to open CANopen adapter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            StartUp_TextBox.Text = StartUp_TextBox.Text + "Open Device Successully!\r\n";

            SetActionEnable();

            //GetFirmwareRevision();

            this.default_settings();
        }
        private void default_settings()
        {
            string node_ID_Intialize = "126";
            string func_ID = "600";
            string text_Data_0_init = "00";
            string text_Data_1_init = "00";
            string text_Data_2_init = "00";
            string text_Data_3_init = "00";
            string text_Data_4_init = "00";
            string text_Data_5_init = "00";
            string text_Data_6_init = "00";
            string text_Data_7_init = "00";
            string index_Input_0 = "0x10";
            string index_Input_1 = "0x18";
            string subIndex_Input = "1";

            this.Node_Id_Input.Text = node_ID_Intialize;
            this.Node_ID_Block.Text = node_ID_Intialize;
            this.Function_ID.Text = func_ID;
            this.text_Data_0.Text = text_Data_0_init;
            this.text_Data_1.Text = text_Data_1_init;
            this.text_Data_2.Text = text_Data_2_init;
            this.text_Data_3.Text = text_Data_3_init;
            this.text_Data_4.Text = text_Data_4_init;
            this.text_Data_5.Text = text_Data_5_init;
            this.text_Data_6.Text = text_Data_6_init;
            this.text_Data_7.Text = text_Data_7_init;
            this.Index_Input.Text = index_Input_0;
            this.Index_Input_1.Text = index_Input_1;
            this.Sub_Index_Input.Text = subIndex_Input;
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            bool ret = m_CANopenLibHelper.Close();

            this.tmr_Monitor.Stop();
            if (ret)
            {
                StartUp_TextBox.Text = StartUp_TextBox.Text + "Device Closed. \r\n";
            }
            SetActionEnable();



        }

        private void btn_Enable_Click(object sender, EventArgs e)
        {
            m_NodeID = 126;
            bool ret = m_CANopenLibHelper.DriveEnable(m_NodeID, true);
            if (ret == false)
            {
                ErrorInfo errorInfo = new ErrorInfo();
                m_CANopenLibHelper.GetLastErrorInfo(ref errorInfo);
            }
        }

        private void btn_Disable_Click(object sender, EventArgs e)
        {
            bool ret = m_CANopenLibHelper.DriveEnable(m_NodeID, false);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            this.txt_CommandHistory.Clear();
        }






        private void btn_StopAll_Click(object sender, EventArgs e)
        {
            bool ret = m_CANopenLibHelper.Stop(m_NodeID);
        }






        private void nud_NodeID_ValueChanged(object sender, EventArgs e)
        {

        }

        private void write_SDO_Btn_Click(object sender, EventArgs e)
        {
            bool ret;
            CanMessage sendCanMessage;
            CanMessage receivedCanMessage;

            byte byt0 = (byte)Convert.ToInt32(this.text_Data_0.Text, 16);
            byte byt1 = (byte)Convert.ToInt32(this.text_Data_1.Text, 16);
            byte byt2 = (byte)Convert.ToInt32(this.text_Data_2.Text, 16);
            byte byt3 = (byte)Convert.ToInt32(this.text_Data_3.Text, 16);
            byte byt4 = (byte)Convert.ToInt32(this.text_Data_4.Text, 16);
            byte byt5 = (byte)Convert.ToInt32(this.text_Data_5.Text, 16);
            byte byt6 = (byte)Convert.ToInt32(this.text_Data_6.Text, 16);
            byte byt7 = (byte)Convert.ToInt32(this.text_Data_7.Text, 16);
            int nNodeID = Convert.ToInt32(this.Node_Id_Input.Text);
            int Func_ID = Convert.ToInt32(this.Function_ID.Text, 16);

            sendCanMessage = new CanMessage { id = Func_ID + nNodeID, dlc = 8, msg = new byte[8] { byt0, byt1, byt2, byt3, byt4, byt5, byt6, byt7 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = m_CANopenLibHelper.Write(sendCanMessage);

            if (ret)
            {
                this.textBox1.Text = textBox1.Text + "Successfully Written!\r\n";
            }
            else
            {
                this.textBox1.Text = textBox1.Text + "Written failed\r\n";
            }

        }

        private void Clear_Bnt_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void Scan_Btn_Click(object sender, EventArgs e)
        { int i;
            for (i = 1; i <= 128; i++)
            {
                byte nNodeID = (byte)i;
                int nIndex = Convert.ToInt32("0x1018", 16);
                byte nSubIndex = 1;
                short nData = 0;

                bool ret = CANopenLibHelper._ReadSDOInt16(nNodeID, nIndex, nSubIndex, ref nData);
                if (ret)
                {
                    textBox1.Text = textBox1.Text + "Node Id: " + nNodeID.ToString() + "\r\n";
                }
            }
        }
        /// <summary>
        ///  Initialize SDO_BLOCK UPLOAD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void SDO_Block_Init_Btn_Click(object sender, EventArgs e)
        {

            m_NodeID = (byte)Convert.ToInt32(this.Node_ID_Block.Text);
            byte nIndex_0 = (byte)Convert.ToInt32(this.Index_Input.Text, 16);
            byte nIndex_1 = (byte)Convert.ToInt32(this.Index_Input_1.Text, 16);
            byte nSubIndex = (byte)Convert.ToInt32(this.Index_Input_1.Text, 16);
            //textBox2.Text = textBox2.Text + nIndex_0.ToString() + "  ";
            //textBox2.Text = textBox2.Text + nIndex_1.ToString();


            //int SubIndex = 0;
            SDO_Init(m_NodeID, nIndex_0, nIndex_1, nSubIndex);

        }

        /// <summary>
        /// Initiate SDO_Block Transfer
        /// </summary>
        /// <param name="m_NodeID"></param>
        private void SDO_Init(byte m_NodeID, byte index_0, byte index_1, byte sub_Index)
        {


            CanMessage sendCanMessage_Test = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            CanMessage receivedCanMessage_Test = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            bool ret;
            CanMessage sendCanMessage;
            CanMessage receivedCanMessage;



            //rb check
            //sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, 0x00, 0x10, 0x00, 0, 0, 0, 0 } };
            //receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            //ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);
            //rb check
            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, index_1, index_0, 0x01, 0, 0, 0, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);

            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, index_1, index_0, 0x02, 0, 0, 0, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);


            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, index_1, index_0, 0x03, 0, 0, 0, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);


            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, index_1, index_0, 0x04, 0x00, 0, 0, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);
            //rb ok

            ///Erase Flash 
            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x2f, 0x51, 0x1f, 0x01, 0x03, 0, 0, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);

            // check flash
            int i = 0;
            while (i <= 10)
            {
                sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, 0x57, 0x1f, 0x01, 0, 0, 0, 0 } };
                receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
                ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);
                m_CANopenLibHelper.GetLastReceivedMessage(ref receivedCanMessage);
                if (receivedCanMessage.msg[4] == 0x02)
                {
                    break;
                }
                Thread.Sleep(50);
            }
            // flash ok

            // initialize sdo block
            i = 0;
            while (i <= 10)
            {
                ///INIT  length
                sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x21, 0x50, 0x1f, 0x01, 0x0E, 0x00, 0, 0 } };
                receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
                ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);
                m_CANopenLibHelper.GetLastReceivedMessage(ref receivedCanMessage);
                if (ret)
                {
                    textBox2.Text = textBox2.Text + "SDO Block Initialized. \r\n";
                    break;

                }
                Thread.Sleep(100);
            }
            // sdo block init ok

            ///send sdo block message
            ///
            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x00, 0x88, 0x40, 0x00, 0x20, 0x51, 0x2f, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);


            //Thread.Sleep(1000);
            /// STAT CHECK 





        }
        private void SDO_Block_Send_Btn_Click(object sender, EventArgs e)
        {
            bool ret;
            CanMessage sendCanMessage;
            CanMessage receivedCanMessage;


            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x11, 0xbf, 0, 0, 0, 0, 0, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);


            /// STAT CHECK 
            sendCanMessage = new CanMessage { id = 0x600 + m_NodeID, dlc = 8, msg = new byte[8] { 0x40, 0x56, 0x1f, 0x01, 0, 0, 0, 0 } };
            receivedCanMessage = new CanMessage { msg = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 } };
            ret = ExecuteCanMessage(sendCanMessage, 0x580, ref receivedCanMessage, true, m_NodeID);


        }
        private void DataProcess(List<byte> data) {
            //StartUp_TextBox.Text = StartUp_TextBox.Text + ((data.Count +6) / 7).ToString() +"\r\n";
            // get data length and length of the last message
            int dataLength = data.Count;
            int data_Len_Last = data.Count % 7;

            if (dataLength == 0)
            {
                return;
            }
            if (dataLength % 7 != 0)
            {
               //StartUp_TextBox.Text = StartUp_TextBox.Text + (data.Count%7) + "\r\n";
                
                for (int i = 0;i< (7 - dataLength % 7); i++)
                {
                    byte data_0 = 0;
                    data.Add(data_0);
                   
                }
                StartUp_TextBox.Text = StartUp_TextBox.Text + "Original Data : ";
                foreach (byte n in data)
                {
                   StartUp_TextBox.Text = StartUp_TextBox.Text + n + " ";
                }
                StartUp_TextBox.Text = StartUp_TextBox.Text + "\r\n";
            }

            //StartUp_TextBox.Text = StartUp_TextBox.Text + data.Count/7 + " count \r\n";
            Dictionary<int,List<byte>> data_Dic = new Dictionary<int, List<byte>>();
            //for (int i = 0; i < data.Count / 7 ; i++){
            
            int data_len = data.Count / 7;

            //StartUp_TextBox.Text = StartUp_TextBox.Text + data_len + " i \r\n";
            for (int i = 0; i < data_len; i++)
            {

                List<byte> can_data = new List<byte>();

                for (int j = 0;j <7; j++)
                {
                    can_data.Add(data[j]);
                    
                }
              
                data.RemoveRange(0, 7);
         
                data_Dic.Add(i, can_data);  
            }
     
            foreach (int key in data_Dic.Keys)
            {
               // StartUp_TextBox.Text = StartUp_TextBox.Text + key + " keys \r\n";
                List<byte> value = new List<byte>();
                data_Dic.TryGetValue(key, out value);
                StartUp_TextBox.Text = StartUp_TextBox.Text + "CanMessage Data : key " + key + " data: ";
                foreach (byte n in value)
                {   
                    StartUp_TextBox.Text = StartUp_TextBox.Text + n +" ";
                }
                StartUp_TextBox.Text = StartUp_TextBox.Text  + "\r\n";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //byte[] data1 = new byte[] { 0x02, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,0x08,0x01,0x01 };
            byte[] data1 = new byte[] { 0x01 };
            List<byte> data = new List<byte>();
            
            data.AddRange(data1);

            //foreach (byte n in data)
            //{
            //    StartUp_TextBox.Text = StartUp_TextBox.Text + data[0];
            //}
            //data.Add(0);   
            DataProcess(data);
        }

        private void Read_File_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            Stream myStream = null;
            int file_len;
            int read_len;
            string filePath;
            byte[] binchar = new byte[] { };
            //List<byte> data = new List<byte>();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {   
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        // get file path
                        filePath = openFileDialog1.FileName;
                        textBox2.AppendText("File Path Get: " + filePath + "\r\n");
                        FileStream Myfile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        BinaryReader binreader = new BinaryReader(Myfile);
                        //string Mytext = "";
                        file_len = (int)Myfile.Length;
                        textBox2.AppendText("文件长度: ");
                        textBox2.AppendText(file_len.ToString()+"\r\n");
                        //int z=0;
                        while (file_len > 0)
                        {
                            //textBox2.Text = textBox2.Text + z.ToString() + "\r\n";
                           //z++;
                       
                            //file_len = 512;
                            if (file_len / 256 > 0)
                            {
                                //一次读取256字节
                                read_len = 256;
                             
                            }
                            else                   //不足256字节按实际长度读取
                            {
                                read_len = file_len % 256;
                                
                            }
                            binchar = binreader.ReadBytes(read_len);
                            this.data.AddRange(binchar);
                            //foreach (byte n in binchar)
                            //{
                            //    textBox1.AppendText(n.ToString("X")); //大写 16位显示 增加前导0

                            //    textBox1.Text = Mytext;
                            //}
                            file_len -= read_len;
                        }
                        textBox1.AppendText("\r\n" + data.Count.ToString() + " ");
                        binreader.Close();
                        foreach(byte n in data)
                        {
                            textBox1.AppendText(n.ToString("X") + " ");

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开文件出错：" + ex.Message);
                }

            }

        }
    }
}
