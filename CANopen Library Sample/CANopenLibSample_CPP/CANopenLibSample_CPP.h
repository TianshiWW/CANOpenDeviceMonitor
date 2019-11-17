
// CANopenLibSample_CPP.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CCANopenLibSample_CPPApp:
// See CANopenLibSample_CPP.cpp for the implementation of this class
//

class CCANopenLibSample_CPPApp : public CWinApp
{
public:
	CCANopenLibSample_CPPApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CCANopenLibSample_CPPApp theApp;