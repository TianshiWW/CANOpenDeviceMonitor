
// CANopenLibSample_CPPDlg.h : header file
//

#pragma once
#include "afxcmn.h"

#include "TransparentPic.h"
#include "CANopenLibHelper.h"
#include "HexEditCtrl.h"

#include "PosMode.h"
#include "Homing.h"
#include "QProgram.h"

// CCANopenLibSample_CPPDlg dialog
class CCANopenLibSample_CPPDlg : public CDialogEx
{
// Construction
public:
	CCANopenLibSample_CPPDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_CANOPENLIBSAMPLECPP_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	CPosMode m_PosMode;
	CQProgram m_QProgram;
	CHoming m_Homing;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()

	afx_msg LRESULT OnHeartBeatReceive(WPARAM wParam, LPARAM lParam);
	afx_msg LRESULT OnDataSend(WPARAM wParam, LPARAM lParam);
	afx_msg LRESULT OnDataReceive(WPARAM wParam, LPARAM lParam);
	afx_msg LRESULT OnTPDOReceived(WPARAM wParam, LPARAM lParam);
public:
	
	void SetActionEnable();
	void AppendControlText(CString text);
	void DisplayCanMessage(BOOL bDirIsToDrive, CANMESSAGE canMessage);
	BOOL SendCanMessageList(CANMESSAGE* canMessage, int len);

	CListCtrl m_HeartBeatList;
	CTransparentPic m_Logo;
	CComboBox cmb_Adapter;
	CComboBox cmb_BaudRate;
	afx_msg void OnBnClickedBtnExit();
	afx_msg void OnBnClickedBtnOpen();
	afx_msg void OnBnClickedBtnClose();
	afx_msg void OnBnClickedBtnEnable();
	CButton btn_Open;
	CButton btn_Close;
	CButton btn_Enable;
	CButton btn_Disable;
	CButton btn_PreOpMode;
	CButton btn_OpMode;
	CButton btn_AlarmReset;
	CMFCButton btn_Stop;
	CString m_strCommandHistory;
	BOOL chk_ShowDrivetoPC;
	BOOL chk_ShowPCtoDrive;
	afx_msg void OnBnClickedBtnDisable();
	afx_msg void OnBnClickedBtnPreopmode();
	afx_msg void OnBnClickedBtnOpmode();
//	BYTE m_NodeID;
	CHexEditCtrl txt_NodeID;
	afx_msg void OnBnClickedBtnExecutesinglecommand();
//	CString txt_SingleCommandData;
//	CString txt_SingleCommandFlag;
//	CString txt_SingleCommandID;
	CHexEditCtrl txt_SingleCommandID;
	BYTE m_nSingleCommandLen;
	UINT m_nSingleCommandFlag;
	CString m_sSingleCommandData;
	CTabCtrl tab_Motion;
	afx_msg void OnTcnSelchangeTabMotion(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg LRESULT OnButtonClick(WPARAM wParam, LPARAM lParam);
	afx_msg void OnBnClickedBtnClear();
	afx_msg void OnBnClickedBtnStop();
	CButton btn_ExecuteSingleCommand;
	CString txt_CANRev;
	CString txt_DSPRev;
	void GetFirmwareRevision();
	virtual BOOL DestroyWindow();
};
