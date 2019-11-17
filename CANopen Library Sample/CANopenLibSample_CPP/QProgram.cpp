// QProgram.cpp : implementation file
//

#include "stdafx.h"
#include "CANopenLibSample_CPP.h"
#include "QProgram.h"
#include "afxdialogex.h"


// CQProgram dialog

IMPLEMENT_DYNAMIC(CQProgram, CDialog)

CQProgram::CQProgram(CWnd* pParent /*=NULL*/)
	: CDialog(CQProgram::IDD, pParent)
{
	m_QSegment = 1;
	m_NormalMode = 0;
}

CQProgram::~CQProgram()
{
}

void CQProgram::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDV_MinMaxInt(pDX, m_QSegment, 1, 12);
	//  DDX_Control(pDX, IDC_TXT_QPROGRAM_SEGMENT, m_QSegment);
	DDX_Text(pDX, IDC_TXT_QPROGRAM_SEGMENT, m_QSegment);
	DDX_Radio(pDX, IDC_RB_NORMAL, m_NormalMode);

}


BEGIN_MESSAGE_MAP(CQProgram, CDialog)
	ON_BN_CLICKED(IDC_BTN_QPROGRAM_START, &CQProgram::OnClickedBtnQprogramStart)
	ON_BN_CLICKED(IDC_BTN_QPROGRAM_STOP, &CQProgram::OnClickedBtnQprogramStop)
END_MESSAGE_MAP()


// CQProgram message handlers

void CQProgram::SetButtonEnable(int nID, BOOL bEnable)
{
	((CButton *)GetDlgItem(nID))->EnableWindow(bEnable);
}

void CQProgram:: SetQProgramSegment(BYTE nSegment)
{
	m_QSegment = nSegment;
	UpdateData(TRUE);
}

int CQProgram::GetQProgramSegment()
{
	UpdateData(TRUE);
	if (m_QSegment < 1)
	{
		m_QSegment = 1;
	}
	else if (m_QSegment > 12)
	{
		m_QSegment = 12;
	}
	return m_QSegment;
}

BOOL CQProgram::GetModeIsSyncMode()
{
	//if (((CRadio*)GetDlgItem(IDD_DIALOG_QPROGRAM))->
	UpdateData(TRUE);
	if (m_NormalMode == 0)
	{
		return FALSE;
	}
	else
	{
		return TRUE;
	}
}

void CQProgram::OnClickedBtnQprogramStart()
{
	// TODO: Add your control notification handler code here
	int id = IDC_BTN_QPROGRAM_START;
	::SendMessage(this->GetParent()->GetParent()->m_hWnd, WM_BUTTONCLICK_MESSAGE, 0, (LPARAM)&id);
}


void CQProgram::OnClickedBtnQprogramStop()
{
	// TODO: Add your control notification handler code here
	int id = IDC_BTN_QPROGRAM_STOP;
	::SendMessage(this->GetParent()->GetParent()->m_hWnd, WM_BUTTONCLICK_MESSAGE, 0, (LPARAM)&id);
}


void CQProgram::OnOK()
{
	// TODO: Add your specialized code here and/or call the base class

	//CDialog::OnOK();
}


void CQProgram::OnCancel()
{
	// TODO: Add your specialized code here and/or call the base class

	//CDialog::OnCancel();
}

BOOL CQProgram::OnInitDialog()
{
	CDialog::OnInitDialog();

	((CEdit*)GetDlgItem(IDC_TXT_QPROGRAM_SEGMENT))->SetLimitText(2);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}
