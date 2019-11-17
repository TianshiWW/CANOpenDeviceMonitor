// PosMode.cpp : implementation file
//

#include "stdafx.h"
#include "CANopenLibSample_CPP.h"
#include "PosMode.h"
#include "afxdialogex.h"


// CPosMode dialog

IMPLEMENT_DYNAMIC(CPosMode, CDialog)

CPosMode::CPosMode(CWnd* pParent /*=NULL*/)
	: CDialog(CPosMode::IDD, pParent)
{

	//  m_PosMode_Velocity = 0.0;
	m_PosMode_Velocity = 10;
	m_PosMode_Accel = 100;
	m_PosMode_Decel = 100;
	m_PosMode_AbsMove_Pos = 20000;
	m_PosMode_RelMove_Pos = 20000;
}

CPosMode::~CPosMode()
{
}

BOOL CPosMode::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  Add extra initialization here	

	m_PosMove_Dir.AddString(_T("CW"));
	m_PosMove_Dir.AddString(_T("CCW"));
	m_PosMove_Dir.SetCurSel(0);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

void CPosMode::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//  DDX_Text(pDX, IDC_EDIT_POSMODE_VELOCITY, m_PosMode_Velocity);
	//  DDV_MinMaxDouble(pDX, m_PosMode_Velocity, 0.167, 5461.167);
	DDX_Text(pDX, IDC_EDIT_POSMODE_VELOCITY, m_PosMode_Velocity);
	DDV_MinMaxDouble(pDX, m_PosMode_Velocity, 0.025, 100);
	DDX_Text(pDX, IDC_EDIT_POSMODE_ACCEL, m_PosMode_Accel);
	DDV_MinMaxDouble(pDX, m_PosMode_Accel, 0.167, 5461.167);
	DDX_Text(pDX, IDC_EDIT_POSMODE_DECEL, m_PosMode_Decel);
	DDV_MinMaxDouble(pDX, m_PosMode_Decel, 0.167, 5461.167);
	DDX_Text(pDX, IDC_EDIT_POSMODE_ABSMOVE_POS, m_PosMode_AbsMove_Pos);
	DDV_MinMaxInt(pDX, m_PosMode_AbsMove_Pos, -2147483647, 2147483647);
	DDX_Text(pDX, IDC_EDIT_POSMODE_RELMOVE_POS, m_PosMode_RelMove_Pos);
	DDV_MinMaxInt(pDX, m_PosMode_RelMove_Pos, -2147483647, 2147483647);
	DDX_Control(pDX, IDC_CMB_POSMOVE_DIR, m_PosMove_Dir);
}


BEGIN_MESSAGE_MAP(CPosMode, CDialog)
	ON_BN_CLICKED(IDC_BTN_POSMODE_ABSMOVE_START, &CPosMode::OnBnClickedBtnPosmodeAbsmoveStart)
	ON_BN_CLICKED(IDC_BTN_POSMODE_ABSMOVE_STOP, &CPosMode::OnBnClickedBtnPosmodeAbsmoveStop)
	ON_BN_CLICKED(IDC_BTN_POSMODE_RELMOVE_START, &CPosMode::OnBnClickedBtnPosmodeRelmoveStart)
	ON_BN_CLICKED(IDC_BTN_POSMODE_RELMOVE_STOP, &CPosMode::OnBnClickedBtnPosmodeRelmoveStop)
END_MESSAGE_MAP()


// CPosMode message handlers

double CPosMode::GetPosModeVelocity()
{
	UpdateData(TRUE);
	return m_PosMode_Velocity;
}

double CPosMode::GetPosModeAccel()
{
	UpdateData(TRUE);
	return m_PosMode_Accel;
}

double CPosMode::GetPosModeDecel()
{
	UpdateData(TRUE);
	return m_PosMode_Decel;
}

int CPosMode::GetPosModeAbsPos()
{
	UpdateData(TRUE);
	return m_PosMode_AbsMove_Pos;
}

int CPosMode::GetPosModeRelPos()
{
	UpdateData(TRUE);
	return m_PosMode_RelMove_Pos;
}

void CPosMode::OnBnClickedBtnPosmodeAbsmoveStart()
{
	int id = IDC_BTN_POSMODE_ABSMOVE_START;
	::SendMessage(this->GetParent()->GetParent()->m_hWnd, WM_BUTTONCLICK_MESSAGE, 0, (LPARAM)&id);
}

void CPosMode::OnBnClickedBtnPosmodeAbsmoveStop()
{
	// TODO: Add your control notification handler code here
	int id = IDC_BTN_POSMODE_ABSMOVE_STOP;
	::SendMessage(this->GetParent()->GetParent()->m_hWnd, WM_BUTTONCLICK_MESSAGE, 0, (LPARAM)&id);
}


void CPosMode::OnBnClickedBtnPosmodeRelmoveStart()
{
	int id = IDC_BTN_POSMODE_RELMOVE_START;
	::SendMessage(this->GetParent()->GetParent()->m_hWnd, WM_BUTTONCLICK_MESSAGE, 0, (LPARAM)&id);
}

void CPosMode::OnBnClickedBtnPosmodeRelmoveStop()
{
	// TODO: Add your control notification handler code here
	int id = IDC_BTN_POSMODE_RELMOVE_STOP;
	::SendMessage(this->GetParent()->GetParent()->m_hWnd, WM_BUTTONCLICK_MESSAGE, 0, (LPARAM)&id);
}

void CPosMode::SetButtonEnable(int nID, BOOL bEnable)
{
	((CButton *)GetDlgItem(nID))->EnableWindow(bEnable);
}

BOOL CPosMode::GetRelMoveDir()
{
	if (m_PosMove_Dir.GetCurSel() == 0)
	{
		return TRUE;
	}
	else
	{
		return FALSE;
	}
}

void CPosMode::OnOK()
{
	// TODO: Add your specialized code here and/or call the base class

	//CDialog::OnOK();
}


void CPosMode::OnCancel()
{
	// TODO: Add your specialized code here and/or call the base class

	//CDialog::OnCancel();
}

