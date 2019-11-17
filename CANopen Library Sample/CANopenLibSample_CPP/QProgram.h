#pragma once


// CQProgram dialog

class CQProgram : public CDialog
{
	DECLARE_DYNAMIC(CQProgram)

public:
	CQProgram(CWnd* pParent = NULL);   // standard constructor
	virtual ~CQProgram();

// Dialog Data
	enum { IDD = IDD_DIALOG_QPROGRAM };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	int m_QSegment;
	int m_NormalMode;

	DECLARE_MESSAGE_MAP()
public:
	void SetButtonEnable(int nID, BOOL bEnable);
	void SetQProgramSegment(BYTE nSegemtn);
	int GetQProgramSegment();
	BOOL GetModeIsSyncMode();
	afx_msg void OnClickedBtnQprogramStart();
	afx_msg void OnClickedBtnQprogramStop();
	virtual void OnOK();
	virtual void OnCancel();
	virtual BOOL OnInitDialog();
};
