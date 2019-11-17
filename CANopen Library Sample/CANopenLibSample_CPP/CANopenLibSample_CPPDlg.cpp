
// CANopenLibSample_CPPDlg.cpp : implementation file
//

#include "stdafx.h"
#include "CANopenLibSample_CPP.h"
#include "CANopenLibSample_CPPDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CCANopenLibSample_CPPDlg dialog

CANopenLibHelper m_CANopenLibHelper;


CCANopenLibSample_CPPDlg::CCANopenLibSample_CPPDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CCANopenLibSample_CPPDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	chk_ShowPCtoDrive = true;
	chk_ShowDrivetoPC = true;

	m_nSingleCommandLen = 0x08;
	m_nSingleCommandFlag = 0x00;
	m_sSingleCommandData = _T("00 00 00 00 00 00 00 00");
	txt_CANRev = _T("");
	txt_DSPRev = _T("");
}

void CCANopenLibSample_CPPDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LST_HEARTBEAT, m_HeartBeatList);
	DDX_Control(pDX, IDC_MOONSLOGO, m_Logo);
	DDX_Control(pDX, IDC_CMB_ADAPTER, cmb_Adapter);
	DDX_Control(pDX, IDC_CMB_BAUDRATE, cmb_BaudRate);
	DDX_Control(pDX, IDC_BTN_OPEN, btn_Open);
	DDX_Control(pDX, IDC_BTN_CLOSE, btn_Close);
	DDX_Control(pDX, IDC_BTN_ENABLE, btn_Enable);
	DDX_Control(pDX, IDC_BTN_DISABLE, btn_Disable);
	DDX_Control(pDX, IDC_BTN_PREOPMODE, btn_PreOpMode);
	DDX_Control(pDX, IDC_BTN_OPMODE, btn_OpMode);
	DDX_Control(pDX, IDC_BTN_ALARMRESET, btn_AlarmReset);
	DDX_Control(pDX, IDC_BTN_STOP, btn_Stop);
	DDX_Text(pDX, IDC_TXT_COMMANDHISTORY, m_strCommandHistory);
	DDX_Check(pDX, IDC_CHK_DRIVETOPC, chk_ShowDrivetoPC);
	DDX_Check(pDX, IDC_CHK_PCTODRIVE, chk_ShowPCtoDrive);
	DDX_Control(pDX, IDC_TXT_NODEID, txt_NodeID);
	DDX_Control(pDX, IDC_TXT_SINGLECOMMANDID, txt_SingleCommandID);
	DDX_Text(pDX, IDC_TXT_SINGLECOMMANDLEN, m_nSingleCommandLen);
	DDX_Text(pDX, IDC_TXT_SINGLECOMMANDFLAG, m_nSingleCommandFlag);
	DDX_Text(pDX, IDC_TXT_SINGLECOMMANDDATA, m_sSingleCommandData);
	DDX_Control(pDX, IDC_TAB_MOTION, tab_Motion);
	DDX_Control(pDX, IDC_BTN_EXECUTESINGLECOMMAND, btn_ExecuteSingleCommand);
	DDX_Text(pDX, IDC_TXT_CANREV, txt_CANRev);
	DDX_Text(pDX, IDC_TXT_DSPREV, txt_DSPRev);
}

BEGIN_MESSAGE_MAP(CCANopenLibSample_CPPDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()

	ON_MESSAGE(WM_HEARTBEATRECEIVED_MESSAGE, &CCANopenLibSample_CPPDlg::OnHeartBeatReceive)
	ON_MESSAGE(WM_DATASEND_MESSAGE, &CCANopenLibSample_CPPDlg::OnDataSend)
	ON_MESSAGE(WM_DATARECEIVED_MESSAGE, &CCANopenLibSample_CPPDlg::OnDataReceive)
	ON_MESSAGE(WM_TPDORECEIVED_MESSAGE, &CCANopenLibSample_CPPDlg::OnTPDOReceived)
	ON_MESSAGE(WM_BUTTONCLICK_MESSAGE, &CCANopenLibSample_CPPDlg::OnButtonClick)

	ON_WM_TIMER()
	
	ON_BN_CLICKED(IDC_BTN_OPEN, &CCANopenLibSample_CPPDlg::OnBnClickedBtnOpen)
	ON_BN_CLICKED(IDC_BTN_CLOSE, &CCANopenLibSample_CPPDlg::OnBnClickedBtnClose)
	ON_BN_CLICKED(IDC_BTN_ENABLE, &CCANopenLibSample_CPPDlg::OnBnClickedBtnEnable)
	ON_BN_CLICKED(IDC_BTN_DISABLE, &CCANopenLibSample_CPPDlg::OnBnClickedBtnDisable)
	ON_BN_CLICKED(IDC_BTN_PREOPMODE, &CCANopenLibSample_CPPDlg::OnBnClickedBtnPreopmode)
	ON_BN_CLICKED(IDC_BTN_OPMODE, &CCANopenLibSample_CPPDlg::OnBnClickedBtnOpmode)
	ON_BN_CLICKED(IDC_BTN_EXECUTESINGLECOMMAND, &CCANopenLibSample_CPPDlg::OnBnClickedBtnExecutesinglecommand)
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB_MOTION, &CCANopenLibSample_CPPDlg::OnTcnSelchangeTabMotion)
	ON_BN_CLICKED(IDC_BTN_CLEAR, &CCANopenLibSample_CPPDlg::OnBnClickedBtnClear)
	ON_BN_CLICKED(IDC_BTN_STOP, &CCANopenLibSample_CPPDlg::OnBnClickedBtnStop)
END_MESSAGE_MAP()


// CCANopenLibSample_CPPDlg message handlers

BOOL CCANopenLibSample_CPPDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	this->cmb_Adapter.AddString(_T("Kvaser"));
	this->cmb_Adapter.AddString(_T("PEAK"));
	this->cmb_Adapter.AddString(_T("ZLG"));
	this->cmb_Adapter.SetCurSel(0);

	this->cmb_BaudRate.AddString(_T("1M bps"));
	this->cmb_BaudRate.AddString(_T("800k bps"));
	this->cmb_BaudRate.AddString(_T("500k bps"));
	this->cmb_BaudRate.AddString(_T("250k bps"));
	this->cmb_BaudRate.AddString(_T("125k bps"));
	this->cmb_BaudRate.AddString(_T("50k bps"));
	this->cmb_BaudRate.AddString(_T("20k bps"));
	this->cmb_BaudRate.AddString(_T("12.5 bps"));
	this->cmb_BaudRate.SetCurSel(0);

	m_Logo.SetTransparentColor(RGB(255, 255, 255));
	m_Logo.SetBitmapIndex(IDB_LOGO);

	DWORD _ExtendedStyle = m_HeartBeatList.GetExtendedStyle();

	_ExtendedStyle |= LVS_EX_FULLROWSELECT;
	_ExtendedStyle |= LVS_EX_GRIDLINES;
	m_HeartBeatList.SetExtendedStyle(_ExtendedStyle);

	m_HeartBeatList.InsertColumn(0, _T("SEQ"), LVCFMT_LEFT, 35);
	m_HeartBeatList.InsertColumn(1, _T("NodeID(H)"), LVCFMT_LEFT, 65);
	m_HeartBeatList.InsertColumn(2, _T("Code(H)"), LVCFMT_LEFT, 55);
	m_HeartBeatList.InsertColumn(3, _T("Time Stamp"), LVCFMT_LEFT, 75);

	txt_NodeID.SetWindowText(_T("1"));	

	txt_SingleCommandID.SetWindowText(_T("601"));

	((CEdit*)GetDlgItem(IDC_TXT_NODEID))->SetLimitText(2);
	((CEdit*)GetDlgItem(IDC_TXT_SINGLECOMMANDID))->SetLimitText(3);
	((CEdit*)GetDlgItem(IDC_TXT_SINGLECOMMANDLEN))->SetLimitText(1);
	((CEdit*)GetDlgItem(IDC_TXT_SINGLECOMMANDFLAG))->SetLimitText(3);
	((CEdit*)GetDlgItem(IDC_TXT_SINGLECOMMANDDATA))->SetLimitText(23);

	tab_Motion.InsertItem(0, _T("Position Mode"));
	tab_Motion.InsertItem(1, _T("Homing"));
	tab_Motion.InsertItem(2, _T("Q Program"));

	m_PosMode.Create(IDD_DIALOG_POSMODE, GetDlgItem(IDC_TAB_MOTION));
	m_Homing.Create(IDD_DIALOG_HOMING, GetDlgItem(IDC_TAB_MOTION));
	m_QProgram.Create(IDD_DIALOG_QPROGRAM, GetDlgItem(IDC_TAB_MOTION));	

	CRect rect;
	tab_Motion.GetClientRect(&rect);
	rect.top += 25;
	rect.bottom -= 10;
	rect.left += 1;
	rect.right -= 2;

	m_PosMode.MoveWindow(&rect);
	m_QProgram.MoveWindow(&rect);
	m_Homing.MoveWindow(&rect);

	m_PosMode.ShowWindow(1);
	m_QProgram.ShowWindow(0);
	m_Homing.ShowWindow(0);

	tab_Motion.SetCurSel(0);

	SetActionEnable();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CCANopenLibSample_CPPDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

BOOL CCANopenLibSample_CPPDlg::DestroyWindow()
{
	// TODO: Add your specialized code here and/or call the base class
	m_CANopenLibHelper.Close();

	return CDialogEx::DestroyWindow();
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CCANopenLibSample_CPPDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CCANopenLibSample_CPPDlg::SetActionEnable()
{
	BOOL _IsOpen = m_CANopenLibHelper.IsOpen();
	this->btn_Open.EnableWindow(!_IsOpen);
	this->btn_Close.EnableWindow(!_IsOpen);
	this->cmb_Adapter.EnableWindow(!_IsOpen);
	this->cmb_BaudRate.EnableWindow(!_IsOpen);

	this->btn_Enable.EnableWindow(_IsOpen);
	this->btn_Disable.EnableWindow(_IsOpen);
	this->btn_Close.EnableWindow(_IsOpen);
	this->btn_PreOpMode.EnableWindow(_IsOpen);
	this->btn_OpMode.EnableWindow(_IsOpen);
	this->btn_AlarmReset.EnableWindow(_IsOpen);
	this->btn_Stop.EnableWindow(_IsOpen);

	this->btn_ExecuteSingleCommand.EnableWindow(_IsOpen);

	this->m_PosMode.SetButtonEnable(IDC_BTN_POSMODE_ABSMOVE_START, _IsOpen);
	this->m_PosMode.SetButtonEnable(IDC_BTN_POSMODE_ABSMOVE_STOP, _IsOpen);
	this->m_PosMode.SetButtonEnable(IDC_BTN_POSMODE_RELMOVE_START, _IsOpen);
	this->m_PosMode.SetButtonEnable(IDC_BTN_POSMODE_RELMOVE_STOP, _IsOpen);

	this->m_Homing.SetButtonEnable(IDC_BTN_HOMING_START, _IsOpen);
	this->m_Homing.SetButtonEnable(IDC_BTN_HOMING_STOP, _IsOpen);

	this->m_QProgram.SetButtonEnable(IDC_BTN_QPROGRAM_START, _IsOpen);
	this->m_QProgram.SetButtonEnable(IDC_BTN_QPROGRAM_STOP, _IsOpen);
}

void CCANopenLibSample_CPPDlg::AppendControlText(CString text)
{
	UpdateData(TRUE);

	CEdit* txt_CommandHistory = ((CEdit*)GetDlgItem(IDC_TXT_COMMANDHISTORY));

	m_strCommandHistory += text;

	UpdateData(FALSE);
	int lineCount = txt_CommandHistory->GetLineCount();
	txt_CommandHistory->LineScroll(lineCount, 0);

}

void CCANopenLibSample_CPPDlg::DisplayCanMessage(BOOL bDirIsToDrive, CANMESSAGE canMessage)
{
	CString text;
	if (bDirIsToDrive)
	{
		text.Format(_T("PC->Drive: Msg=%.3X Node ID=%.2X Len=%.2X Data=%.2X %.2X %.2X %.2X %.2X %.2X %.2X %.2X Time Stamp=%d\r\n"),
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
		text.Format(_T("Drive->PC: Msg=%.3X Node ID=%.2X Len=%.2X Data=%.2X %.2X %.2X %.2X %.2X %.2X %.2X %.2X Time Stamp=%d\r\n"),
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
	AppendControlText(text);
}

BOOL CCANopenLibSample_CPPDlg::SendCanMessageList(CANMESSAGE* canMessage, int len)
{
	for (int i = 0; i < len ; i++)
	{
		BOOL ret = m_CANopenLibHelper.Write(*canMessage);
		canMessage++;
		if (!ret)
		{
			return FALSE;
		}
		Sleep(1);
	}
	return TRUE;
}

afx_msg LRESULT CCANopenLibSample_CPPDlg::OnHeartBeatReceive(WPARAM wParam, LPARAM lParam)
{
	CANMESSAGE canMessage;
	m_CANopenLibHelper.GetLastHeartBeatMessage(canMessage);

	UpdateData(TRUE);

	CString strText;
	int nItemCount = m_HeartBeatList.GetItemCount();


	CString strNodeID;

	BYTE nNodeID;

	BOOL found = FALSE;
	int index = -1;

	for (int i = 0; i < nItemCount; i++)
	{
		strNodeID = m_HeartBeatList.GetItemText(i, 1);

		nNodeID = _tcstoul(strNodeID, NULL, 10);

		if (nNodeID == (canMessage.id & 0x7F))
		{
			index = i;
			found = TRUE;
			break;
		}
	}

	if (!found)
	{
		index = nItemCount;
		strText.Format(_T("%d"), nItemCount + 1);
		m_HeartBeatList.InsertItem(nItemCount, strText);
	}

	strNodeID.Format(_T("%.2X"), (canMessage.id & 0x7F));
	m_HeartBeatList.SetItemText(index, 1, strNodeID);
	CString strCode;
	strCode.Format(_T("%.2X"), canMessage.msg[0]);
	m_HeartBeatList.SetItemText(index, 2, strCode);
	CString strTimeStamp;
	strTimeStamp.Format(_T("%.8d"), canMessage.timeStamp);
	m_HeartBeatList.SetItemText(index, 3, strTimeStamp);

	return 0;
}

afx_msg LRESULT CCANopenLibSample_CPPDlg::OnDataSend(WPARAM wParam, LPARAM lParam)
{
	if (chk_ShowPCtoDrive)
	{
		CANMESSAGE canMessage;
		m_CANopenLibHelper.GetLastSentMessage(canMessage);

		UpdateData(TRUE);
		DisplayCanMessage(TRUE, canMessage);
	}

	return 0;
}

afx_msg LRESULT CCANopenLibSample_CPPDlg::OnDataReceive(WPARAM wParam, LPARAM lParam)
{
	if (chk_ShowDrivetoPC)
	{
		CANMESSAGE canMessage;
		m_CANopenLibHelper.GetLastReceivedMessage(canMessage);

		UpdateData(TRUE);
		DisplayCanMessage(FALSE, canMessage);
	}

	return 0;
}

afx_msg LRESULT CCANopenLibSample_CPPDlg::OnTPDOReceived(WPARAM wParam, LPARAM lParam)
{
	PDOMESSAGE TPDOMessage;
	m_CANopenLibHelper.GetLastTPDOMessage(TPDOMessage);

	return 0;
}

void CCANopenLibSample_CPPDlg::GetFirmwareRevision()
{
	UpdateData(TRUE);
	BYTE nNodeID;
	CString strNodeID;
	this->txt_NodeID.GetWindowText(strNodeID);
	nNodeID = _tcstoul(strNodeID, NULL, 16);

	CANMESSAGE getDSPRevMessage = { 0x600 + nNodeID, 8, { 0x40, 0x18, 0x10, 0x02, 0x00, 0x00, 0x00, 0x00 } };
	CANMESSAGE receivedCanMessage1;

	CANMESSAGE getCANRevMessage = { 0x600 + nNodeID, 8, { 0x40, 0x0A, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00 } };
	CANMESSAGE receivedCanMessage2;

	int OKTimes = 0;
	int TotalTimes = 0;

	BOOL ret1 = TRUE;
	BOOL ret2 = TRUE;

	CString str;

	ret1 = m_CANopenLibHelper.ExecuteCommand(getDSPRevMessage, &receivedCanMessage1, 0x580, TRUE, nNodeID);
	ret2 = m_CANopenLibHelper.ExecuteCommand(getCANRevMessage, &receivedCanMessage2, 0x580, TRUE, nNodeID);

	TotalTimes++;
	if (ret1 && ret2)
	{
		str.Format(_T("1.0%d%c"), receivedCanMessage1.msg[6], receivedCanMessage1.msg[4]);
		this->txt_DSPRev = str;

		str.Format(_T("%c.%c%c%c"), receivedCanMessage2.msg[4], receivedCanMessage2.msg[5], receivedCanMessage2.msg[6], receivedCanMessage2.msg[7]);
		this->txt_CANRev = str;

		OKTimes++;
	}
	else
	{
		this->txt_CANRev = "";
		this->txt_CANRev = "";
	}

	UpdateData(FALSE);
}

void CCANopenLibSample_CPPDlg::OnBnClickedBtnOpen()
{
	EnumAdapter nAdapter = (EnumAdapter)this->cmb_Adapter.GetCurSel();
	EnumBaudRate nBaudRate = (EnumBaudRate)this->cmb_BaudRate.GetCurSel();
	int nChannel = 0;

	if (nAdapter == Kvaser)
	{
		nChannel = (int)HWTYPE_NONE;
	}
	else if (nAdapter == PEAK)
	{
		nChannel = (int)PCAN_USBBUS1;
	}
	else if (nAdapter == ZLG)
	{
		nChannel = (int)USBCAN2A;
	}
	BOOL ret = m_CANopenLibHelper.Open(nAdapter, nBaudRate, nChannel);

	if (ret == FALSE)
	{
		AfxMessageBox(_T("Fail to open CANopen adapter."));
	}

	SetActionEnable();
	
	GetFirmwareRevision();
}


void CCANopenLibSample_CPPDlg::OnBnClickedBtnClose()
{
	m_CANopenLibHelper.Close();

	this->txt_DSPRev = "";
	this->txt_CANRev = "";
	UpdateData(FALSE);

	SetActionEnable();
}

void CCANopenLibSample_CPPDlg::OnBnClickedBtnEnable()
{
	BOOL ret;

	UpdateData(TRUE);
	int nNodeID;
	CString strNodeID;
	this->txt_NodeID.GetWindowText(strNodeID);
	nNodeID = _tcstoul(strNodeID, NULL, 16);

	ret = m_CANopenLibHelper.DriveEnable(nNodeID, true);
}


void CCANopenLibSample_CPPDlg::OnBnClickedBtnDisable()
{
	BOOL ret;

	UpdateData(TRUE);
	int nNodeID;
	CString strNodeID;
	this->txt_NodeID.GetWindowText(strNodeID);
	nNodeID = _tcstoul(strNodeID, NULL, 16);

	ret = m_CANopenLibHelper.DriveEnable(nNodeID, false);
}


void CCANopenLibSample_CPPDlg::OnBnClickedBtnPreopmode()
{
	BOOL ret;

	UpdateData(TRUE);
	BYTE nNodeID;
	CString strNodeID;
	this->txt_NodeID.GetWindowText(strNodeID);
	nNodeID = _tcstoul(strNodeID, NULL, 16);

	ret = m_CANopenLibHelper.SetToPreoperationalMode(nNodeID);
}


void CCANopenLibSample_CPPDlg::OnBnClickedBtnOpmode()
{
	BOOL ret;

	UpdateData(TRUE);
	int nNodeID;
	CString strNodeID;
	this->txt_NodeID.GetWindowText(strNodeID);
	nNodeID = _tcstoul(strNodeID, NULL, 16);

	ret = m_CANopenLibHelper.SetToOperationalMode(nNodeID);
}


void CCANopenLibSample_CPPDlg::OnBnClickedBtnExecutesinglecommand()
{
	BOOL ret;
	UpdateData(TRUE);

	CString strSingleCommandID;
	this->txt_SingleCommandID.GetWindowText(strSingleCommandID);
	int nCobID = _tcstoul(strSingleCommandID, NULL, 16);

	CANMESSAGE canMessage = { nCobID, m_nSingleCommandLen, { 0, 0, 0, 0, 0, 0, 0, 0 }, m_nSingleCommandFlag };

	CString strData = m_sSingleCommandData;

	CStringArray strArr;
	strArr.SetSize(0, 1);
	int index = 0;
	while (true)
	{
		index = strData.Find(_T(" "));
		if (index == -1)
		{
			strArr.Add(strData);
			break;
		}
		strArr.Add(strData.Left(index));
		strData = strData.Mid(index+1, strData.GetLength() - index);
	}

	if (strArr.GetSize() > 8)
	{
		AfxMessageBox(_T("Error Single Command Data."));
		return;
	}
	int* valueList = new int[strArr.GetSize()];
	for (int i = 0; i < strArr.GetSize(); i++)
	{
		canMessage.msg[i] = _tcstoul(strArr[i], NULL, 16);
	}
	ret = m_CANopenLibHelper.Write(canMessage);
}


void CCANopenLibSample_CPPDlg::OnTcnSelchangeTabMotion(NMHDR *pNMHDR, LRESULT *pResult)
{

	int CurSel = tab_Motion.GetCurSel();
	switch(CurSel)
	{
	case 0:
		m_PosMode.ShowWindow(TRUE);
		m_QProgram.ShowWindow(FALSE);
		m_Homing.ShowWindow(FALSE);
		break;
	case 1:
		m_PosMode.ShowWindow(FALSE);
		m_QProgram.ShowWindow(FALSE);
		m_Homing.ShowWindow(TRUE);
		break;
	case 2:
		m_PosMode.ShowWindow(FALSE);
		m_QProgram.ShowWindow(TRUE);
		m_Homing.ShowWindow(FALSE);
		break;
	default:
		;
	}

	*pResult = 0;
}


afx_msg LRESULT CCANopenLibSample_CPPDlg::OnButtonClick(WPARAM wParam, LPARAM lParam)
{
	BOOL ret;
	int * id;
	id = (int *)lParam;

	UpdateData(TRUE);
	int nNodeID;
	CString strNodeID;
	this->txt_NodeID.GetWindowText(strNodeID);
	nNodeID = _tcstoul(strNodeID, NULL, 16);

	double dPosModeVelocity = m_PosMode.GetPosModeVelocity();
	double dPosModeAccel = m_PosMode.GetPosModeAccel();
	double dPosModeDecel = m_PosMode.GetPosModeDecel();

	int nAbsDI = m_PosMode.GetPosModeAbsPos();
	int nRelDI = m_PosMode.GetPosModeRelPos();

	if (nAbsDI < 0)
	{
		nAbsDI = 4294967296 + nAbsDI;
	}
	if (m_PosMode.GetRelMoveDir() == FALSE)
	{
		nRelDI = 4294967296 - nRelDI;
	}

	double dHomingVelocity = m_Homing.GetHomingVelocity();
	double dHomingAccel = m_Homing.GetHomingAccel();
	double dIndexVelocity = m_Homing.GetIndexVelocity();

	int nHomingOffset = m_Homing.GetHomingOffset();
	int nHomingMethod = m_Homing.GetHomingMethod();
	int nHomingSwitch = m_Homing.GetHomingSwitch();

	if (nHomingOffset < 0)
	{
		nHomingOffset = 4294967296 + nAbsDI;
	}

	int len = 0;

	switch (*id)
	{
	case IDC_BTN_POSMODE_ABSMOVE_START:
		{
			OnBnClickedBtnEnable();

			ret = m_CANopenLibHelper.AbsMove(nNodeID, nAbsDI, &dPosModeVelocity, &dPosModeAccel, &dPosModeDecel);

			break;
		}
	case IDC_BTN_POSMODE_ABSMOVE_STOP:
		OnBnClickedBtnStop();
		break;
	case IDC_BTN_POSMODE_RELMOVE_START:
		{
			OnBnClickedBtnEnable();

			ret = m_CANopenLibHelper.RelMove(nNodeID, nRelDI, &dPosModeVelocity, &dPosModeAccel, &dPosModeDecel);

			break;
		}
	case IDC_BTN_POSMODE_RELMOVE_STOP:
		OnBnClickedBtnStop();
		break;
	case IDC_BTN_HOMING_START:
		{
			OnBnClickedBtnEnable();

			ret = m_CANopenLibHelper.Homing(nNodeID, nHomingMethod, &dHomingVelocity, &dIndexVelocity, &dHomingAccel, &nHomingOffset, &nHomingSwitch);

			break;
		}
	case IDC_BTN_HOMING_STOP:
		OnBnClickedBtnStop();
		break;
	case IDC_BTN_QPROGRAM_START:
		{
			int qSegment = m_QProgram.GetQProgramSegment();
			OnBnClickedBtnEnable();

			if (m_QProgram.GetModeIsSyncMode())
			{
				ret = m_CANopenLibHelper.ExecuteSyncQProgram(nNodeID, qSegment, 0x80);
			}
			else
			{
				ret = m_CANopenLibHelper.ExecuteNormalQProgram(nNodeID, qSegment);
			}

			break;
		}
	case IDC_BTN_QPROGRAM_STOP:
		OnBnClickedBtnStop();
		break;
	}
	return 0;
}

void CCANopenLibSample_CPPDlg::OnBnClickedBtnClear()
{
	m_strCommandHistory = "";
	UpdateData(FALSE);
}


void CCANopenLibSample_CPPDlg::OnBnClickedBtnStop()
{
	UpdateData(TRUE);
	int nNodeID;
	CString strNodeID;
	this->txt_NodeID.GetWindowText(strNodeID);
	nNodeID = _tcstoul(strNodeID, NULL, 16);

	BOOL ret = m_CANopenLibHelper.Stop(nNodeID);
}


