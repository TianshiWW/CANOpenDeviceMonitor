#pragma once
#include "afxwin.h"

class CHexEditCtrl : public CEdit  
{  
    DECLARE_DYNAMIC(CHexEditCtrl)  
  
public:  
    CHexEditCtrl();  
    virtual ~CHexEditCtrl();  
  
protected:  
    afx_msg void OnEnSetfocus();  
    afx_msg void OnEnKillfocus();  
    DECLARE_MESSAGE_MAP()  
    virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);  
  
// ‰»Î∑®  
protected:    
    HKL m_DefaultHKL;    
    HKL m_Saved_KbdLayout;    
    HKL GetDefault_KbdLayout();    
  
};  