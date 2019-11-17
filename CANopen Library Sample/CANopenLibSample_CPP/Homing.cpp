// Homing.cpp : implementation file
//

#include "stdafx.h"
#include "CANopenLibSample_CPP.h"
#include "Homing.h"
#include "afxdialogex.h"


// CHoming dialog

IMPLEMENT_DYNAMIC(CHoming, CDialogEx)

CHoming::CHoming(CWnd* pParent /*=NULL*/)
	: CDialogEx(CHoming::IDD, pParent)
{

	//  m_HomingAccel = 0.0;
	//  m_HomingDecel = 0.0;
	//  m_HomingVelocity = 0.0;
	//  m_HomingOffset = 0;
	//  m_IndexVelocity = 0.0;
	m_HomingVelocity = 2.0;
	m_HomingAccel = 10.0;
	m_HomingOffset = 0;
	m_IndexVelocity = 0.5;
	m_HomingSwitch = 1;
}

CHoming::~CHoming()
{
}

void CHoming::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_CMB_HOMINGMETHOD, cmb_HomingMethod);
	//  DDX_Text(pDX, IDC_LBL_HOMING_ACCEL, m_HomingAccel);
	//  DDV_MinMaxDouble(pDX, m_HomingAccel, 0.167, 5461.167);
	//  DDX_Text(pDX, IDC_LBL_HOMING_DECEL, m_HomingDecel);
	//  DDV_MinMaxDouble(pDX, m_HomingDecel, 0.167, 5461.167);
	//  DDX_Text(pDX, IDC_LBL_HOMING_VELOCITY, m_HomingVelocity);
	//  DDV_MinMaxDouble(pDX, m_HomingVelocity, 0.025, 60);
	//  DDX_Text(pDX, IDC_LBL_HOMINGOFFSET, m_HomingOffset);
	//  DDV_MinMaxInt(pDX, m_HomingOffset, -2147483647, 2147483647);
	//  DDX_Text(pDX, IDC_LBL_INDEXVELOCITY, m_IndexVelocity);
	//  DDV_MinMaxDouble(pDX, m_IndexVelocity, 0.025, 60);
	DDX_Text(pDX, IDC_TXT_HOMING_VELOCITY, m_HomingVelocity);
	DDV_MinMaxDouble(pDX, m_HomingVelocity, 0.025, 60);
	DDX_Text(pDX, IDC_TXT_HOMING_ACCEL, m_HomingAccel);
	DDV_MinMaxDouble(pDX, m_HomingAccel, 0.167, 5461.167);
	DDX_Text(pDX, IDC_TXT_HOMING_OFFSET, m_HomingOffset);
	DDV_MinMaxInt(pDX, m_HomingOffset, -2147483647, 2147483647);
	DDX_Text(pDX, IDC_TXT_HOMING_INDEXVELOCITY, m_IndexVelocity);
	DDV_MinMaxDouble(pDX, m_IndexVelocity, 0.025, 20);
	//  DDX_Control(pDX, IDC_TXT_HOMING_SWITCH, m_HomingSwitch);
	DDX_Text(pDX, IDC_TXT_HOMING_SWITCH, m_HomingSwitch);
	DDV_MinMaxInt(pDX, m_HomingSwitch, 1, 12);
}


BEGIN_MESSAGE_MAP(CHoming, CDialogEx)
	ON_BN_CLICKED(IDC_BTN_HOMING_START, &CHoming::OnBnClickedBtnHomingStart)
	ON_BN_CLICKED(IDC_BTN_HOMING_STOP, &CHoming::OnBnClickedBtnHomingStop)
END_MESSAGE_MAP()

BOOL CHoming::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	CString str;

	for (int i = 1; i <= 35; i++)
	{
		if (i == 31 || i == 32)
		{
			continue;
		}
		str.Format(_T("%d"), i);
		cmb_HomingMethod.AddString(str);
	}

	cmb_HomingMethod.SetCurSel(0);

	((CEdit*)GetDlgItem(IDC_TXT_HOMING_SWITCH))->SetLimitText(2);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

// CHoming message handlers

void CHoming::SetButtonEnable(int nID, BOOL bEnable)
{
	((CButton *)GetDlgItem(nID))->EnableWindow(bEnable);
}

int CHoming::GetHomingMethod()
{
	//UpdateData(TRUE);
	//return m_HomingOffset;
	return cmb_HomingMethod.GetCurSel() + 1;
}

double CHoming::GetHomingVelocity()
{
	UpdateData(TRUE);
	return m_HomingVelocity;
}

double CHoming::GetHomingAccel()
{
	UpdateData(TRUE);
	return m_HomingAccel;
}

double CHoming::GetIndexVelocity()
{
	UpdateData(TRUE);
	return m_IndexVelocity;
}

int CHoming::GetHomingOffset()
{
	UpdateData(TRUE);
	return m_HomingOffset;
}

int CHoming::GetHomingSwitch()
{
	UpdateData(TRUE);
	return m_HomingSwitch;
}

void CHoming::OnBnClickedBtnHomingStart()
{
	int id = IDC_BTN_HOMING_START;
	::SendMessage(this->GetParent()->GetParent()->m_hWnd, WM_BUTTONCLICK_MESSAGE, 0, (LPARAM)&id);
}


void CHoming::OnBnClickedBtnHomingStop()
{
	int id = IDC_BTN_HOMING_STOP;
	::SendMessage(this->GetParent()->GetParent()->m_hWnd, WM_BUTTONCLICK_MESSAGE, 0, (LPARAM)&id);
}


