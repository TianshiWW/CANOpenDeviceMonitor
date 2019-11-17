#include "StdAfx.h"
#include "HexEditCtrl.h"

// CHexEditCtrl  
IMPLEMENT_DYNAMIC(CHexEditCtrl, CEdit)  
  
CHexEditCtrl::CHexEditCtrl()  
{  
    m_DefaultHKL = GetDefault_KbdLayout();    
    m_Saved_KbdLayout = 0;
}  
  
CHexEditCtrl::~CHexEditCtrl()  
{  
}  
  
BEGIN_MESSAGE_MAP(CHexEditCtrl, CEdit)  
    ON_CONTROL_REFLECT(EN_SETFOCUS, &CHexEditCtrl::OnEnSetfocus)  
    ON_CONTROL_REFLECT(EN_KILLFOCUS, &CHexEditCtrl::OnEnKillfocus)  
END_MESSAGE_MAP()  
  
// CHexEditCtrl message handlers  
  
LRESULT CHexEditCtrl::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)  
{  
    // TODO: Add your specialized code here and/or call the base class  
  
    //���������ַ�  
    //���� �ո� 0-F Backspace  
    if(message == WM_CHAR)  
    {  
        WCHAR szInput[2] = {(WCHAR)wParam, 0};  
        _wcsupr_s(szInput);  
        CStringW szValidItem=L" 0123456789ABCDEF\b";   
        if(szValidItem.Find(szInput) == -1)  
        {  
            MessageBeep(-1);//��ʾ��  
            return 0;  
        }  
        wParam = szInput[0];  
    }  
  
    if(WM_INPUTLANGCHANGEREQUEST == message) //�л����뷨��Ϣ     
    {    
        return 0;  //�������뷨    
        //m_Saved_KbdLayout = 0; //���ܸñ��Ҳ���Ҫ�ָ����뷨     
    }    
  
    return CEdit::WindowProc(message, wParam, lParam);  
}  
  
void CHexEditCtrl::OnEnSetfocus()  
{  
    // TODO: Add your control notification handler code here  
    if(m_DefaultHKL)    
    {    
        HKL CurKbdLayout = GetKeyboardLayout(0);    
        if(CurKbdLayout != m_DefaultHKL)    
        {    
            m_Saved_KbdLayout = CurKbdLayout; //�������뷨     
            if(ActivateKeyboardLayout(m_DefaultHKL, 0) == 0) //����ΪĬ�����뷨     
            {    
                TRACE( _T("Set ActivateKeyboardLayout Err=%d"), GetLastError());    
            }    
        }    
    }   
}  
  
void CHexEditCtrl::OnEnKillfocus()  
{  
    // TODO: Add your control notification handler code here  
    if(m_Saved_KbdLayout) //�ָ����뷨     
    {    
        if(ActivateKeyboardLayout(m_Saved_KbdLayout, 0) == 0)    
            TRACE( _T("Restore ActivateKeyboardLayout Err=%d"), GetLastError());    
        m_Saved_KbdLayout = 0;    
    }   
}  
  
//��ȡӢ�ļ���  
HKL CHexEditCtrl::GetDefault_KbdLayout()    
{    
  HKL HKL_Ret= 0;    
  int nSize = GetKeyboardLayoutList(0 , 0);    
  if(nSize != 0)    
  {    
    HKL FAR * lpList = (HKL FAR *)new HKL[nSize];    
    ZeroMemory(lpList, sizeof(HKL) * nSize);    
    if(GetKeyboardLayoutList(nSize, lpList) == nSize)    
    {    
      for(int i=0; i<nSize; i++)    
      {    
        HKL Val = lpList[i];    
        if(HIWORD(Val) == LOWORD(Val))    
          HKL_Ret = Val;    
        TRACE( _T("0x%08X/n"), Val);    
      }    
    }    
    delete []lpList;    
  }    
  return HKL_Ret;    
}   