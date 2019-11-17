#pragma once


// CPosMode dialog

class CPosMode : public CDialog
{
	DECLARE_DYNAMIC(CPosMode)

public:
	CPosMode(CWnd* pParent = NULL);   // standard constructor
	virtual ~CPosMode();

// Dialog Data
	enum { IDD = IDD_DIALOG_POSMODE };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()

public:
	double GetPosModeVelocity();
	double GetPosModeAccel();
	double GetPosModeDecel();
	int GetPosModeAbsPos();
	int GetPosModeRelPos();
	BOOL GetRelMoveDir();
	afx_msg void OnBnClickedBtnPosmodeAbsmoveStart();
	afx_msg void OnBnClickedBtnPosmodeAbsmoveStop();
	afx_msg void OnBnClickedBtnPosmodeRelmoveStart();
	afx_msg void OnBnClickedBtnPosmodeRelmoveStop();
	double m_PosMode_Velocity;
	double m_PosMode_Accel;
	double m_PosMode_Decel;
	int m_PosMode_AbsMove_Pos;
	int m_PosMode_RelMove_Pos;

	void SetButtonEnable(int nID, BOOL bEnable);
	CComboBox m_PosMove_Dir;
	virtual BOOL OnInitDialog();
	virtual void OnOK();
	virtual void OnCancel();
};
