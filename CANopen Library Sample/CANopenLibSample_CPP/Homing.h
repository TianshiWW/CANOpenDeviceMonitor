#pragma once


// CHoming dialog

class CHoming : public CDialogEx
{
	DECLARE_DYNAMIC(CHoming)

public:
	CHoming(CWnd* pParent = NULL);   // standard constructor
	virtual ~CHoming();

// Dialog Data
	enum { IDD = IDD_DIALOG_HOMING };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CComboBox cmb_HomingMethod;
	void SetButtonEnable(int nID, BOOL bEnable);
	int GetHomingMethod();
	double GetHomingVelocity();
	double GetHomingAccel();
	double GetIndexVelocity();
	int GetHomingOffset();
	int GetHomingSwitch();

	double m_HomingVelocity;
	double m_HomingAccel;
	int m_HomingOffset;
	double m_IndexVelocity;
	afx_msg void OnBnClickedBtnHomingStart();
	afx_msg void OnBnClickedBtnHomingStop();
	virtual BOOL OnInitDialog();
//	CEdit m_HomingSwitch;
	int m_HomingSwitch;
};
