#pragma once
#include "afxwin.h"

class CTransparentPic : public CStatic
{
	DECLARE_DYNAMIC(CTransparentPic)

public:
	CTransparentPic();
	virtual ~CTransparentPic();

	void SetTransparentColor(UINT color);
	void SetBitmapIndex(UINT nBitmapIndex);

protected:
	DECLARE_MESSAGE_MAP()

public:
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	afx_msg void OnPaint();

private:
	UINT m_nBitmapIndex; // Bitmap index of resource
	UINT m_nTransparentColor;
};