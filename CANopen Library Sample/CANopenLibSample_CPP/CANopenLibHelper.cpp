// ************************************************************************************************
// File Name        : CANopenLibHelper.cpp
// Copyright		: 2017, Shanghai AMP & MOONS' Automation Co., Ltd., All rights reserved.
// Module Name		: CANopen Library C++ helper implementation file
// Author           : Lei Youbing
// Created          : 2016-11-03
//
// Revision History
// No	Version		Date		Revised By		Description
// 1	1.0.17.0223	2017-02-23	Lei Youbing		First released.
// 2	1.0.17.1009	2017-10-09	Lei Youbing		Added canlib32.dll to the included files.
//
// ************************************************************************************************

#include "StdAfx.h"
#include "CANopenLibHelper.h"


EventCallback OnHeartBeatReceive()
{
	if (AfxGetApp() != NULL)
	{
		::SendMessage(AfxGetApp()->m_pMainWnd->m_hWnd, WM_HEARTBEATRECEIVED_MESSAGE, 0, 0);
	}
	return 0;
}

EventCallback OnDataSend()
{
	if (AfxGetApp() != NULL)
	{
		::SendMessage(AfxGetApp()->m_pMainWnd->m_hWnd, WM_DATASEND_MESSAGE, 0, 0);
	}
	return 0;
}

EventCallback OnDataReceive()
{
	if (AfxGetApp() != NULL)
	{
		::SendMessage(AfxGetApp()->m_pMainWnd->m_hWnd, WM_DATARECEIVED_MESSAGE, 0, 0);
	}
	return 0;
}

EventCallback OnTPDOReceived()
{
	if (AfxGetApp() != NULL)
	{
		::SendMessage(AfxGetApp()->m_pMainWnd->m_hWnd, WM_TPDORECEIVED_MESSAGE, 0, 0);
	}
	return 0;
}

CANopenLibHelper::CANopenLibHelper(void)
{
	m_bWasLoaded = FALSE;
	m_hDll = NULL;

	m_hDll = LoadLibrary(_T("CANopenLib_x86.dll"));  // for 64-bit windows, please comment this line and uncomment next line
	//m_hDll = LoadLibrary(_T("CANopenLib_x64.dll"));  // for 32-bit windows, please comment this line and uncomment previous line

	if (m_hDll != NULL)
	{
		m_OnHeartBeatReceive = (lpOnHeartBeatReceive)GetProcAddress(m_hDll, "OnHeartBeatReceive");
		m_OnDataSend = (lpOnDataSend)GetProcAddress(m_hDll, "OnDataSend");
		m_OnDataReceive = (lpOnDataReceive)GetProcAddress(m_hDll, "OnDataReceive");
		m_OnTPDOReceived = (lpOnTPDOReceived)GetProcAddress(m_hDll, "OnTPDOReceived");

		m_OnHeartBeatReceive((EventCallback)OnHeartBeatReceive);
		m_OnDataSend((EventCallback)OnDataSend);
		m_OnDataReceive((EventCallback)OnDataReceive);
		m_OnTPDOReceived((EventCallback)OnTPDOReceived);

		m_GetLastHeartBeatMessage = (lpGetLastHeartBeatMessage)GetProcAddress(m_hDll, "GetLastHeartBeatMessage");
		m_GetLastSentMessage = (lpGetLastSentMessage)GetProcAddress(m_hDll, "GetLastSentMessage");
		m_GetLastReceivedMessage = (lpGetLastReceivedMessage)GetProcAddress(m_hDll, "GetLastReceivedMessage");
		m_GetLastTPDOMessage = (lpGetLastTPDOMessage)GetProcAddress(m_hDll, "GetLastTPDOMessage");
		m_GetLastTPDOMessageByNodeID = (lpGetLastTPDOMessageByNodeID)GetProcAddress(m_hDll, "GetLastTPDOMessageByNodeID");
				
		m_Open = (lpOpen)GetProcAddress(m_hDll, "Open");
		m_Close = (lpClose)GetProcAddress(m_hDll, "Close");
		m_IsOpen = (lpIsOpen)GetProcAddress(m_hDll, "IsOpen");
		
		m_SetExecuteTimeOut = (lpSetExecuteTimeOut)GetProcAddress(m_hDll, "SetExecuteTimeOut");
		m_GetExecuteTimeOut = (lpGetExecuteTimeOut)GetProcAddress(m_hDll, "GetExecuteTimeOut");
		m_SetExecuteRetryTimes = (lpSetExecuteRetryTimes)GetProcAddress(m_hDll, "SetExecuteRetryTimes");
		m_GetExecuteRetryTimes = (lpGetExecuteRetryTimes)GetProcAddress(m_hDll, "GetExecuteRetryTimes");
		m_ResetBuffer = (lpResetBuffer)GetProcAddress(m_hDll, "ResetBuffer");
		m_GetLastErrorInfo = (lpGetLastErrorInfo)GetProcAddress(m_hDll, "GetLastErrorInfo");
		m_Write = (lpWrite)GetProcAddress(m_hDll, "Write");
		m_ExecuteCommand = (lpExecuteCommand)GetProcAddress(m_hDll, "ExecuteCommand");
		m_ReadSDOInt8 = (lpReadSDOInt8)GetProcAddress(m_hDll, "ReadSDOInt8");
		m_ReadSDOUInt8 = (lpReadSDOUInt8)GetProcAddress(m_hDll, "ReadSDOUInt8");
		m_ReadSDOInt16 = (lpReadSDOInt16)GetProcAddress(m_hDll, "ReadSDOInt16");
		m_ReadSDOUInt16 = (lpReadSDOUInt16)GetProcAddress(m_hDll, "ReadSDOUInt16");
		m_ReadSDOInt32 = (lpReadSDOInt32)GetProcAddress(m_hDll, "ReadSDOInt32");
		m_ReadSDOUInt32 = (lpReadSDOUInt32)GetProcAddress(m_hDll, "ReadSDOUInt32");
		m_WriteSDOInt8 = (lpWriteSDOInt8)GetProcAddress(m_hDll, "WriteSDOInt8");
		m_WriteSDOUInt8 = (lpWriteSDOUInt8)GetProcAddress(m_hDll, "WriteSDOUInt8");
		m_WriteSDOInt16 = (lpWriteSDOInt16)GetProcAddress(m_hDll, "WriteSDOInt16");
		m_WriteSDOUInt16 = (lpWriteSDOUInt16)GetProcAddress(m_hDll, "WriteSDOUInt16");
		m_WriteSDOInt32 = (lpWriteSDOInt32)GetProcAddress(m_hDll, "WriteSDOInt32");
		m_WriteSDOUInt32 = (lpWriteSDOUInt32)GetProcAddress(m_hDll, "WriteSDOUInt32");
		m_SetToPreoperationalMode = (lpSetToPreoperationalMode)GetProcAddress(m_hDll, "SetToPreoperationalMode");
		m_SetToOperationalMode = (lpSetToOperationalMode)GetProcAddress(m_hDll, "SetToOperationalMode");

		m_SetRPDOMapping = (lpSetRPDOMapping)GetProcAddress(m_hDll, "SetRPDOMapping");
		m_SetTPDOMapping = (lpSetTPDOMapping)GetProcAddress(m_hDll, "SetTPDOMapping");
		m_WriteRPDO = (lpWriteRPDO)GetProcAddress(m_hDll, "WriteRPDO");
		m_RestorePDOMappingSettings = (lpRestorePDOMappingSettings)GetProcAddress(m_hDll, "RestorePDOMappingSettings");
		m_SaveParameters = (lpSaveParameters)GetProcAddress(m_hDll, "SaveParameters");

		m_ReadPositionGain = (lpReadPositionGain)GetProcAddress(m_hDll, "ReadPositionGain");
		m_WritePositionGain = (lpWritePositionGain)GetProcAddress(m_hDll, "WritePositionGain");
		m_ReadPositionDeriGain = (lpReadPositionDeriGain)GetProcAddress(m_hDll, "ReadPositionDeriGain");
		m_WritePositionDeriGain = (lpWritePositionDeriGain)GetProcAddress(m_hDll, "WritePositionDeriGain");
		m_ReadPositionDeriFilter = (lpReadPositionDeriFilter)GetProcAddress(m_hDll, "ReadPositionDeriFilter");
		m_WritePositionDeriFilter = (lpWritePositionDeriFilter)GetProcAddress(m_hDll, "WritePositionDeriFilter");
		m_ReadVelocityGain = (lpReadVelocityGain)GetProcAddress(m_hDll, "ReadVelocityGain");
		m_WriteVelocityGain = (lpWriteVelocityGain)GetProcAddress(m_hDll, "WriteVelocityGain");
		m_ReadVelocityIntegGain = (lpReadVelocityIntegGain)GetProcAddress(m_hDll, "ReadVelocityIntegGain");
		m_WriteVelocityIntegGain = (lpWriteVelocityIntegGain)GetProcAddress(m_hDll, "WriteVelocityIntegGain");
		m_ReadAccFeedForward = (lpReadAccFeedForward)GetProcAddress(m_hDll, "ReadAccFeedForward");
		m_WriteAccFeedForward = (lpWriteAccFeedForward)GetProcAddress(m_hDll, "WriteAccFeedForward");
		m_ReadPIDFilter = (lpReadPIDFilter)GetProcAddress(m_hDll, "ReadPIDFilter");
		m_WritePIDFilter = (lpWritePIDFilter)GetProcAddress(m_hDll, "WritePIDFilter");
		m_ReadNotchFilter = (lpReadNotchFilter)GetProcAddress(m_hDll, "ReadNotchFilter");
		m_WriteNotchFilter = (lpWriteNotchFilter)GetProcAddress(m_hDll, "WriteNotchFilter");
		m_ReadPositionError = (lpReadPositionError)GetProcAddress(m_hDll, "ReadPositionError");
		m_WritePositionError = (lpWritePositionError)GetProcAddress(m_hDll, "WritePositionError");
		m_ReadVelocityMax = (lpReadVelocityMax)GetProcAddress(m_hDll, "ReadVelocityMax");
		m_WriteVelocityMax = (lpWriteVelocityMax)GetProcAddress(m_hDll, "WriteVelocityMax");
		m_ReadSmoothFilter = (lpReadSmoothFilter)GetProcAddress(m_hDll, "ReadSmoothFilter");
		m_WriteSmoothFilter = (lpWriteSmoothFilter)GetProcAddress(m_hDll, "WriteSmoothFilter");
		m_ReadDriverTemp = (lpReadDriverTemp)GetProcAddress(m_hDll, "ReadDriverTemp");
		m_ReadErrorCode = (lpReadErrorCode)GetProcAddress(m_hDll, "ReadErrorCode");
		m_ReadErrorCodeUpper = (lpReadErrorCodeUpper)GetProcAddress(m_hDll, "ReadErrorCodeUpper");
		m_WriteControlWord = (lpWriteControlWord)GetProcAddress(m_hDll, "WriteControlWord");
		m_ReadStatusWord = (lpReadStatusWord)GetProcAddress(m_hDll, "ReadStatusWord");
		m_ReadQuickStopOptionCode = (lpReadQuickStopOptionCode)GetProcAddress(m_hDll, "ReadQuickStopOptionCode");
		m_WriteQuickStopOptionCode = (lpWriteQuickStopOptionCode)GetProcAddress(m_hDll, "WriteQuickStopOptionCode");
		m_WriteModesofOperation = (lpWriteModesofOperation)GetProcAddress(m_hDll, "WriteModesofOperation");
		m_ReadModesofOperation = (lpReadModesofOperation)GetProcAddress(m_hDll, "ReadModesofOperation");
		m_ReadPositionTargetValueCalculated = (lpReadPositionTargetValueCalculated)GetProcAddress(m_hDll, "ReadPositionTargetValueCalculated");
		m_ReadFollowingErrorWindow = (lpReadFollowingErrorWindow)GetProcAddress(m_hDll, "ReadFollowingErrorWindow");
		m_WriteFollowingErrorWindow = (lpWriteFollowingErrorWindow)GetProcAddress(m_hDll, "WriteFollowingErrorWindow");
		m_ReadVelocityTargetValueCalculated = (lpReadVelocityTargetValueCalculated)GetProcAddress(m_hDll, "ReadVelocityTargetValueCalculated");
		m_ReadTargetTorque = (lpReadTargetTorque)GetProcAddress(m_hDll, "ReadTargetTorque");
		m_WriteTargetTorque = (lpWriteTargetTorque)GetProcAddress(m_hDll, "WriteTargetTorque");
		m_ReadMaxRunningCurrent = (lpReadMaxRunningCurrent)GetProcAddress(m_hDll, "ReadMaxRunningCurrent");
		m_WriteMaxRunningCurrent = (lpWriteMaxRunningCurrent)GetProcAddress(m_hDll, "WriteMaxRunningCurrent");
		m_ReadTorqueDemandValue = (lpReadTorqueDemandValue)GetProcAddress(m_hDll, "ReadTorqueDemandValue");
		m_ReadCurrentActualValue = (lpReadCurrentActualValue)GetProcAddress(m_hDll, "ReadCurrentActualValue");
		m_ReadTargetPosition = (lpReadTargetPosition)GetProcAddress(m_hDll, "ReadTargetPosition");
		m_WriteTargetPosition = (lpWriteTargetPosition)GetProcAddress(m_hDll, "WriteTargetPosition");
		m_ReadHomingOffset = (lpReadHomingOffset)GetProcAddress(m_hDll, "ReadHomingOffset");
		m_WriteHomingOffset = (lpWriteHomingOffset)GetProcAddress(m_hDll, "WriteHomingOffset");
		m_ReadPolarity = (lpReadPolarity)GetProcAddress(m_hDll, "ReadPolarity");
		m_WritePolarity = (lpWritePolarity)GetProcAddress(m_hDll, "WritePolarity");
		m_ReadMaxProfileSpeed = (lpReadMaxProfileSpeed)GetProcAddress(m_hDll, "ReadMaxProfileSpeed");
		m_WriteMaxProfileSpeed = (lpWriteMaxProfileSpeed)GetProcAddress(m_hDll, "WriteMaxProfileSpeed");
		m_ReadProfileVelocity = (lpReadProfileVelocity)GetProcAddress(m_hDll, "ReadProfileVelocity");
		m_WriteProfileVelocity = (lpWriteProfileVelocity)GetProcAddress(m_hDll, "WriteProfileVelocity");
		m_ReadProfileAcceleration = (lpReadProfileAcceleration)GetProcAddress(m_hDll, "ReadProfileAcceleration");
		m_WriteProfileAcceleration = (lpWriteProfileAcceleration)GetProcAddress(m_hDll, "WriteProfileAcceleration");
		m_ReadProfileDeceleration = (lpReadProfileDeceleration)GetProcAddress(m_hDll, "ReadProfileDeceleration");
		m_WriteProfileDeceleration = (lpWriteProfileDeceleration)GetProcAddress(m_hDll, "WriteProfileDeceleration");
		m_ReadQuickStopDeceleration = (lpReadQuickStopDeceleration)GetProcAddress(m_hDll, "ReadQuickStopDeceleration");
		m_WriteQuickStopDeceleration = (lpWriteQuickStopDeceleration)GetProcAddress(m_hDll, "WriteQuickStopDeceleration");
		m_ReadTorqueSlop = (lpReadTorqueSlop)GetProcAddress(m_hDll, "ReadTorqueSlop");
		m_WriteTorqueSlop = (lpWriteTorqueSlop)GetProcAddress(m_hDll, "WriteTorqueSlop");
		m_ReadHomingMethod = (lpReadHomingMethod)GetProcAddress(m_hDll, "ReadHomingMethod");
		m_WriteHomingMethod = (lpWriteHomingMethod)GetProcAddress(m_hDll, "WriteHomingMethod");
		m_ReadHomingSpeedSearchSwitch = (lpReadHomingSpeedSearchSwitch)GetProcAddress(m_hDll, "ReadHomingSpeedSearchSwitch");
		m_WriteHomingSpeedSearchSwitch = (lpWriteHomingSpeedSearchSwitch)GetProcAddress(m_hDll, "WriteHomingSpeedSearchSwitch");
		m_ReadHomingSpeedSearchIndex = (lpReadHomingSpeedSearchIndex)GetProcAddress(m_hDll, "ReadHomingSpeedSearchIndex");
		m_WriteHomingSpeedSearchIndex = (lpWriteHomingSpeedSearchIndex)GetProcAddress(m_hDll, "WriteHomingSpeedSearchIndex");
		m_ReadHomingAcceleration = (lpReadHomingAcceleration)GetProcAddress(m_hDll, "ReadHomingAcceleration");
		m_WriteHomingAcceleration = (lpWriteHomingAcceleration)GetProcAddress(m_hDll, "WriteHomingAcceleration");
		m_ReadDriveOutputs = (lpReadDriveOutputs)GetProcAddress(m_hDll, "ReadDriveOutputs");
		m_WriteDriveOutputs = (lpWriteDriveOutputs)GetProcAddress(m_hDll, "WriteDriveOutputs");
		m_ReadTargetVelocity = (lpReadTargetVelocity)GetProcAddress(m_hDll, "ReadTargetVelocity");
		m_WriteTargetVelocity = (lpWriteTargetVelocity)GetProcAddress(m_hDll, "WriteTargetVelocity");
		m_ReadSupportedDriveModes = (lpReadSupportedDriveModes)GetProcAddress(m_hDll, "ReadSupportedDriveModes");
		m_ReadHomingSwitch = (lpReadHomingSwitch)GetProcAddress(m_hDll, "ReadHomingSwitch");
		m_WriteHomingSwitch = (lpWriteHomingSwitch)GetProcAddress(m_hDll, "WriteHomingSwitch");
		m_ReadIdleCurrent = (lpReadIdleCurrent)GetProcAddress(m_hDll, "ReadIdleCurrent");
		m_WriteIdleCurrent = (lpWriteIdleCurrent)GetProcAddress(m_hDll, "WriteIdleCurrent");
		m_ReadDisplayDriveInputs = (lpReadDisplayDriveInputs)GetProcAddress(m_hDll, "ReadDisplayDriveInputs");
		m_ReadTorqueConstant = (lpReadTorqueConstant)GetProcAddress(m_hDll, "ReadTorqueConstant");
		m_WriteTorqueConstant = (lpWriteTorqueConstant)GetProcAddress(m_hDll, "WriteTorqueConstant");
		m_WriteDSPClearAlarm = (lpWriteDSPClearAlarm)GetProcAddress(m_hDll, "WriteDSPClearAlarm");
		m_ReadQSegment = (lpReadQSegment)GetProcAddress(m_hDll, "ReadQSegment");
		m_WriteQSegment = (lpWriteQSegment)GetProcAddress(m_hDll, "WriteQSegment");
		m_ReadActualVelocity = (lpReadActualVelocity)GetProcAddress(m_hDll, "ReadActualVelocity");
		m_ReadActualPosition = (lpReadActualPosition)GetProcAddress(m_hDll, "ReadActualPosition");
		m_ReadDSPStatusCode = (lpReadDSPStatusCode)GetProcAddress(m_hDll, "ReadDSPStatusCode");
		m_WriteClearPosition = (lpWriteClearPosition)GetProcAddress(m_hDll, "WriteClearPosition");
		m_ReadAccelerationCurrent = (lpReadAccelerationCurrent)GetProcAddress(m_hDll, "ReadAccelerationCurrent");
		m_WriteAccelerationCurrent = (lpWriteAccelerationCurrent)GetProcAddress(m_hDll, "WriteAccelerationCurrent");
		m_ReadAnalogInput1 = (lpReadAnalogInput1)GetProcAddress(m_hDll, "ReadAnalogInput1");
		m_WriteProfileParam = (lpWriteProfileParam)GetProcAddress(m_hDll, "WriteProfileParam");
		m_SwitchControlWord = (lpSwitchControlWord)GetProcAddress(m_hDll, "SwitchControlWord");
		m_DriveEnable = (lpDriveEnable)GetProcAddress(m_hDll, "DriveEnable");
		m_Stop = (lpStop)GetProcAddress(m_hDll, "Stop");
		m_AlarmReset = (lpAlarmReset)GetProcAddress(m_hDll, "AlarmReset");
		m_RelMove = (lpRelMove)GetProcAddress(m_hDll, "RelMove");
		m_AbsMove = (lpAbsMove)GetProcAddress(m_hDll, "AbsMove");
		m_MultipleAbsMoveWithStopping = (lpMultipleAbsMoveWithStopping)GetProcAddress(m_hDll, "MultipleAbsMoveWithStopping");
		m_MultipleAbsMoveContinuous = (lpMultipleAbsMoveContinuous)GetProcAddress(m_hDll, "MultipleAbsMoveContinuous");
		m_ExecuteNormalQProgram = (lpExecuteNormalQProgram)GetProcAddress(m_hDll, "ExecuteNormalQProgram");
		m_ExecuteSyncQProgram = (lpExecuteSyncQProgram)GetProcAddress(m_hDll, "ExecuteSyncQProgram");
		m_Homing = (lpHoming)GetProcAddress(m_hDll, "Homing");
		m_LaunchVelocityMode = (lpLaunchVelocityMode)GetProcAddress(m_hDll, "LaunchVelocityMode");

		m_bWasLoaded = TRUE;
	}
	else
	{
		//AfxMessageBox("Fail to load CANopen library.");
		m_bWasLoaded = FALSE;
	}
	
}


CANopenLibHelper::~CANopenLibHelper(void)
{
	m_bWasLoaded = false;
	if (m_hDll != NULL)
	{
		m_Close();
		FreeLibrary(m_hDll);
	}	
	m_hDll = NULL;
}

void CANopenLibHelper::GetLastHeartBeatMessage(CANMESSAGE& CANMessage)
{
	m_GetLastHeartBeatMessage(CANMessage);
}

void CANopenLibHelper::GetLastSentMessage(CANMESSAGE& CANMessage)
{
	m_GetLastSentMessage(CANMessage);
}

void CANopenLibHelper::GetLastReceivedMessage(CANMESSAGE& CANMessage)
{
	m_GetLastReceivedMessage(CANMessage);
}

void CANopenLibHelper::GetLastTPDOMessage(PDOMESSAGE& PDOMessage)
{
	m_GetLastTPDOMessage(PDOMessage);
}

void CANopenLibHelper::GetLastTPDOMessageByNodeID(BYTE nNodeID, BYTE nPDONo, PDOMESSAGE& PDOMessage)
{
	m_GetLastTPDOMessageByNodeID(nNodeID, nPDONo, PDOMessage);
}

BOOL CANopenLibHelper::Open(EnumAdapter nAdapter, EnumBaudRate nBaudRate, BYTE nChannel)
{
	return m_Open(nAdapter, nBaudRate, nChannel);
}

BOOL CANopenLibHelper::Close()
{
	return m_Close();
}

BOOL CANopenLibHelper::IsOpen()
{
	return m_IsOpen();
}

void CANopenLibHelper::SetExecuteTimeOut(BYTE nExecuteTimeOutinMs)
{
	m_SetExecuteTimeOut(nExecuteTimeOutinMs);
}

BYTE CANopenLibHelper::GetExecuteTimeOut()
{
	return m_GetExecuteTimeOut();
}

void CANopenLibHelper::SetExecuteRetryTimes(BYTE nExecuteRetryTimesinMs)
{
	m_SetExecuteRetryTimes(nExecuteRetryTimesinMs);
}

BYTE CANopenLibHelper::GetExecuteRetryTimes()
{
	return m_GetExecuteRetryTimes();
}

BOOL CANopenLibHelper::ResetBuffer()
{
	return m_ResetBuffer();
}

void CANopenLibHelper::GetLastErrorInfo(ERROR_INFO& errorInfo)
{
	m_GetLastErrorInfo(errorInfo);
}

BOOL CANopenLibHelper::Write(CANMESSAGE pSendCanMessage)
{
	return m_Write(pSendCanMessage);
}

BOOL CANopenLibHelper::ExecuteCommand(CANMESSAGE sendCanMessage, CANMESSAGE* pReceivedCanMessage, int nCanFunction, BOOL bMatchNodeID, BYTE nNodeID, BOOL bMatchIndex, int nIndex, BOOL bMatchFirstByte, BYTE nFirstByte)
{
	return m_ExecuteCommand(sendCanMessage, pReceivedCanMessage, nCanFunction, bMatchNodeID, nNodeID, bMatchIndex, nIndex, bMatchFirstByte, nFirstByte);
}

BOOL CANopenLibHelper::ReadSDOInt8(BYTE nNodeID, int nIndex, BYTE nSubIndex, char* nData)
{
	return m_ReadSDOInt8(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::ReadSDOUInt8(BYTE nNodeID, int nIndex, BYTE nSubIndex, BYTE* nData)
{
	return m_ReadSDOUInt8(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::ReadSDOInt16(BYTE nNodeID, int nIndex, BYTE nSubIndex, short* nData)
{
	return m_ReadSDOInt16(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::ReadSDOUInt16(BYTE nNodeID, int nIndex, BYTE nSubIndex, USHORT* nData)
{
	return m_ReadSDOUInt16(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::ReadSDOInt32(BYTE nNodeID, int nIndex, BYTE nSubIndex, int* nData)
{
	return m_ReadSDOInt32(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::ReadSDOUInt32(BYTE nNodeID, int nIndex, BYTE nSubIndex, UINT* nData)
{
	return m_ReadSDOUInt32(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::WriteSDOInt8(BYTE nNodeID, int nIndex, BYTE nSubIndex, char nData)
{
	return m_WriteSDOInt8(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::WriteSDOUInt8(BYTE nNodeID, int nIndex, BYTE nSubIndex, BYTE nData)
{
	return m_WriteSDOUInt8(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::WriteSDOInt16(BYTE nNodeID, int nIndex, BYTE nSubIndex, short nData)
{
	return m_WriteSDOInt16(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::WriteSDOUInt16(BYTE nNodeID, int nIndex, BYTE nSubIndex, USHORT nData)
{
	return m_WriteSDOUInt16(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::WriteSDOInt32(BYTE nNodeID, int nIndex, BYTE nSubIndex, int nData)
{
	return m_WriteSDOInt32(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::WriteSDOUInt32(BYTE nNodeID, int nIndex, BYTE nSubIndex, UINT nData)
{
	return m_WriteSDOUInt32(nNodeID, nIndex, nSubIndex, nData);
}

BOOL CANopenLibHelper::SetToPreoperationalMode(BYTE nNodeID)
{
	return m_SetToPreoperationalMode(nNodeID);
}

BOOL CANopenLibHelper::SetToOperationalMode(BYTE nNodeID)
{
	return m_SetToOperationalMode(nNodeID);
}

BOOL CANopenLibHelper::SetRPDOMapping(BYTE nNodeID, BYTE nRPDONo, int nLen, PDOMAPPING* pPDOMapping)
{
	return m_SetRPDOMapping(nNodeID, nRPDONo, nLen, pPDOMapping);
}

BOOL CANopenLibHelper::SetTPDOMapping(BYTE nNodeID, BYTE nTPDONo, int nLen, PDOMAPPING* pPDOMapping)
{
	return m_SetTPDOMapping(nNodeID, nTPDONo, nLen, pPDOMapping);
}

BOOL CANopenLibHelper::RestorePDOMappingSettings(BYTE nNodeID)
{
	return m_RestorePDOMappingSettings(nNodeID);
}

BOOL CANopenLibHelper::WriteRPDO(BYTE nNodeID, BYTE nRPDONo, int nLen, BYTE* pData)
{
	return m_WriteRPDO(nNodeID, nRPDONo, nLen, pData);
}

BOOL CANopenLibHelper::SaveParameters(BYTE nNodeID)
{
	return m_SaveParameters(nNodeID);
}

BOOL CANopenLibHelper::ReadPositionGain(BYTE nNodeID, USHORT* nPositionGain)
{
	return m_ReadPositionGain(nNodeID, nPositionGain);
}

BOOL CANopenLibHelper::WritePositionGain(BYTE nNodeID, USHORT nPositionGain)
{
	return m_WritePositionGain(nNodeID, nPositionGain);
}

BOOL CANopenLibHelper::ReadPositionDeriGain(BYTE nNodeID, USHORT* nPositionDeriGain)
{
	return m_ReadPositionDeriGain(nNodeID, nPositionDeriGain);
}

BOOL CANopenLibHelper::WritePositionDeriGain(BYTE nNodeID, USHORT nPositionDeriGain)
{
	return m_WritePositionDeriGain(nNodeID, nPositionDeriGain);
}

BOOL CANopenLibHelper::ReadPositionDeriFilter(BYTE nNodeID, USHORT* nPositionDeriFilter)
{
	return m_ReadPositionDeriFilter(nNodeID, nPositionDeriFilter);
}

BOOL CANopenLibHelper::WritePositionDeriFilter(BYTE nNodeID, USHORT nPositionDeriFilter)
{
	return m_WritePositionDeriFilter(nNodeID, nPositionDeriFilter);
}

BOOL CANopenLibHelper::ReadVelocityGain(BYTE nNodeID, USHORT* nVelocityGain)
{
	return m_ReadVelocityGain(nNodeID, nVelocityGain);
}

BOOL CANopenLibHelper::WriteVelocityGain(BYTE nNodeID, USHORT nVelocityGain)
{
	return m_WriteVelocityGain(nNodeID, nVelocityGain);
}

BOOL CANopenLibHelper::ReadVelocityIntegGain(BYTE nNodeID, USHORT* nVelocityIntegGain)
{
	return m_ReadVelocityIntegGain(nNodeID, nVelocityIntegGain);
}

BOOL CANopenLibHelper::WriteVelocityIntegGain(BYTE nNodeID, USHORT nVelocityIntegGain)
{
	return m_WriteVelocityIntegGain(nNodeID, nVelocityIntegGain);
}

BOOL CANopenLibHelper::ReadAccFeedForward(BYTE nNodeID, USHORT* nAccFeedForward)
{
	return m_ReadAccFeedForward(nNodeID, nAccFeedForward);
}

BOOL CANopenLibHelper::WriteAccFeedForward(BYTE nNodeID, USHORT nAccFeedForward)
{
	return m_WriteAccFeedForward(nNodeID, nAccFeedForward);
}

BOOL CANopenLibHelper::ReadPIDFilter(BYTE nNodeID, USHORT* nPIDFilter)
{
	return m_ReadPIDFilter(nNodeID, nPIDFilter);
}

BOOL CANopenLibHelper::WritePIDFilter(BYTE nNodeID, USHORT nPIDFilter)
{
	return m_WritePIDFilter(nNodeID, nPIDFilter);
}

BOOL CANopenLibHelper::ReadNotchFilter(BYTE nNodeID, int nIndex, USHORT* nNotchFilter)
{
	return m_ReadNotchFilter(nNodeID, nIndex, nNotchFilter);
}

BOOL CANopenLibHelper::WriteNotchFilter(BYTE nNodeID, int nIndex, USHORT nNotchFilter)
{
	return m_WriteNotchFilter(nNodeID, nIndex, nNotchFilter);
}

BOOL CANopenLibHelper::ReadPositionError(BYTE nNodeID, USHORT* nPositionError)
{
	return m_ReadPositionError(nNodeID, nPositionError);
}

BOOL CANopenLibHelper::WritePositionError(BYTE nNodeID, USHORT nPositionError)
{
	return m_WritePositionError(nNodeID, nPositionError);
}

BOOL CANopenLibHelper::ReadVelocityMax(BYTE nNodeID, USHORT* nVelocityMax)
{
	return m_ReadVelocityMax(nNodeID, nVelocityMax);
}

BOOL CANopenLibHelper::WriteVelocityMax(BYTE nNodeID, USHORT nVelocityMax)
{
	return m_WriteVelocityMax(nNodeID, nVelocityMax);
}

BOOL CANopenLibHelper::ReadSmoothFilter(BYTE nNodeID, USHORT* nSmoothFilter)
{
	return m_ReadSmoothFilter(nNodeID, nSmoothFilter);
}

BOOL CANopenLibHelper::WriteSmoothFilter(BYTE nNodeID, USHORT nSmoothFilter)
{
	return m_WriteSmoothFilter(nNodeID, nSmoothFilter);
}

BOOL CANopenLibHelper::ReadDriverTemp(BYTE nNodeID, SHORT* nDriverTemp)
{
	return m_ReadDriverTemp(nNodeID, nDriverTemp);
}

BOOL CANopenLibHelper::ReadErrorCode(BYTE nNodeID, USHORT* nErrorCode)
{
	return m_ReadErrorCode(nNodeID, nErrorCode);
}

BOOL CANopenLibHelper::ReadErrorCodeUpper(BYTE nNodeID, USHORT* nErrorCode)
{
	return m_ReadErrorCodeUpper(nNodeID, nErrorCode);
}

BOOL CANopenLibHelper::WriteControlWord(BYTE nNodeID, USHORT nControlWord)
{
	return m_WriteControlWord(nNodeID, nControlWord);
}

BOOL CANopenLibHelper::ReadStatusWord(BYTE nNodeID, USHORT* nStatusWord)
{
	return m_ReadStatusWord(nNodeID, nStatusWord);
}

BOOL CANopenLibHelper::ReadQuickStopOptionCode(BYTE nNodeID, SHORT* nOptionCode)
{
	return m_ReadQuickStopOptionCode(nNodeID, nOptionCode);
}

BOOL CANopenLibHelper::WriteQuickStopOptionCode(BYTE nNodeID, SHORT nQuickStopOptionCode)
{
	return m_WriteQuickStopOptionCode(nNodeID, nQuickStopOptionCode);
}

BOOL CANopenLibHelper::WriteModesofOperation(BYTE nNodeID, char nModesofOperation)
{
	return m_WriteModesofOperation(nNodeID, nModesofOperation);
}

BOOL CANopenLibHelper::ReadModesofOperation(BYTE nNodeID, char* nModesofOperation)
{
	return m_ReadModesofOperation(nNodeID, nModesofOperation);
}

BOOL CANopenLibHelper::ReadPositionTargetValueCalculated(BYTE nNodeID, int* nPositionTargetValueCalculated)
{
	return m_ReadPositionTargetValueCalculated(nNodeID, nPositionTargetValueCalculated);
}

BOOL CANopenLibHelper::ReadFollowingErrorWindow(BYTE nNodeID, UINT* nFollowingError)
{
	return m_ReadFollowingErrorWindow(nNodeID, nFollowingError);
}

BOOL CANopenLibHelper::WriteFollowingErrorWindow(BYTE nNodeID, UINT nFollowingError)
{
	return m_WriteFollowingErrorWindow(nNodeID, nFollowingError);
}

BOOL CANopenLibHelper::ReadVelocityTargetValueCalculated(BYTE nNodeID, double* dVelocityTargetValueCalculated)
{
	return m_ReadVelocityTargetValueCalculated(nNodeID, dVelocityTargetValueCalculated);
}

BOOL CANopenLibHelper::ReadTargetTorque(BYTE nNodeID, short* nTargetTorque)
{
	return m_ReadTargetTorque(nNodeID, nTargetTorque);
}

BOOL CANopenLibHelper::WriteTargetTorque(BYTE nNodeID, short nTargetTorque)
{
	return m_WriteTargetTorque(nNodeID, nTargetTorque);
}

BOOL CANopenLibHelper::ReadMaxRunningCurrent(BYTE nNodeID, double* dRunningCurrent)
{
	return m_ReadMaxRunningCurrent(nNodeID, dRunningCurrent);
}

BOOL CANopenLibHelper::WriteMaxRunningCurrent(BYTE nNodeID, double dRunningCurrent)
{
	return m_WriteMaxRunningCurrent(nNodeID, dRunningCurrent);
}

BOOL CANopenLibHelper::ReadTorqueDemandValue(BYTE nNodeID, double* dTorqueDemandValue)
{
	return m_ReadTorqueDemandValue(nNodeID, dTorqueDemandValue);
}

BOOL CANopenLibHelper::ReadCurrentActualValue(BYTE nNodeID, double* dCurrentActualValue)
{
	return m_ReadCurrentActualValue(nNodeID, dCurrentActualValue);
}

BOOL CANopenLibHelper::ReadTargetPosition(BYTE nNodeID, int* nTargetPositiont)
{
	return m_ReadTargetPosition(nNodeID, nTargetPositiont);
}

BOOL CANopenLibHelper::WriteTargetPosition(BYTE nNodeID, int nTargetPositiont)
{
	return m_WriteTargetPosition(nNodeID, nTargetPositiont);
}

BOOL CANopenLibHelper::ReadHomingOffset(BYTE nNodeID, int* nHomingOffset)
{
	return m_ReadHomingOffset(nNodeID, nHomingOffset);
}

BOOL CANopenLibHelper::WriteHomingOffset(BYTE nNodeID, int nHomingOffset)
{
	return m_WriteHomingOffset(nNodeID, nHomingOffset);
}

BOOL CANopenLibHelper::ReadPolarity(BYTE nNodeID, BYTE* nPolarity)
{
	return m_ReadPolarity(nNodeID, nPolarity);
}

BOOL CANopenLibHelper::WritePolarity(BYTE nNodeID, BYTE nPolarity)
{
	return m_WritePolarity(nNodeID, nPolarity);
}

BOOL CANopenLibHelper::ReadMaxProfileSpeed(BYTE nNodeID, double* dMaxProfileSpeed)
{
	return m_ReadMaxProfileSpeed(nNodeID, dMaxProfileSpeed);
}

BOOL CANopenLibHelper::WriteMaxProfileSpeed(BYTE nNodeID, double dMaxProfileSpeed)
{
	return m_WriteMaxProfileSpeed(nNodeID, dMaxProfileSpeed);
}

BOOL CANopenLibHelper::ReadProfileVelocity(BYTE nNodeID, double* dProfileVelocity)
{
	return m_ReadProfileVelocity(nNodeID, dProfileVelocity);
}

BOOL CANopenLibHelper::WriteProfileVelocity(BYTE nNodeID, double dProfileVelocity)
{
	return m_WriteProfileVelocity(nNodeID, dProfileVelocity);
}

BOOL CANopenLibHelper::ReadProfileAcceleration(BYTE nNodeID, double* dProfileAcceleration)
{
	return m_ReadProfileAcceleration(nNodeID, dProfileAcceleration);
}

BOOL CANopenLibHelper::WriteProfileAcceleration(BYTE nNodeID, double dProfileAcceleration)
{
	return m_WriteProfileAcceleration(nNodeID, dProfileAcceleration);
}

BOOL CANopenLibHelper::ReadProfileDeceleration(BYTE nNodeID, double* dProfileAcceleration)
{
	return m_ReadProfileDeceleration(nNodeID, dProfileAcceleration);
}

BOOL CANopenLibHelper::WriteProfileDeceleration(BYTE nNodeID, double dProfileAcceleration)
{
	return m_WriteProfileDeceleration(nNodeID, dProfileAcceleration);
}

BOOL CANopenLibHelper::ReadQuickStopDeceleration(BYTE nNodeID, double* dQuickStopDeceleration)
{
	return m_ReadQuickStopDeceleration(nNodeID, dQuickStopDeceleration);
}

BOOL CANopenLibHelper::WriteQuickStopDeceleration(BYTE nNodeID, double dQuickStopDeceleration)
{
	return m_WriteQuickStopDeceleration(nNodeID, dQuickStopDeceleration);
}

BOOL CANopenLibHelper::ReadTorqueSlop(BYTE nNodeID, UINT* nTorqueSlop)
{
	return m_ReadTorqueSlop(nNodeID, nTorqueSlop);
}

BOOL CANopenLibHelper::WriteTorqueSlop(BYTE nNodeID, UINT nTorqueSlop)
{
	return m_WriteTorqueSlop(nNodeID, nTorqueSlop);
}

BOOL CANopenLibHelper::ReadHomingMethod(BYTE nNodeID, BYTE* nHomingMethod)
{
	return m_ReadHomingMethod(nNodeID, nHomingMethod);
}

BOOL CANopenLibHelper::WriteHomingMethod(BYTE nNodeID, BYTE nHomingMethod)
{
	return m_WriteHomingMethod(nNodeID, nHomingMethod);
}

BOOL CANopenLibHelper::ReadHomingSpeedSearchSwitch(BYTE nNodeID, double* dSpeed)
{
	return m_ReadHomingSpeedSearchSwitch(nNodeID, dSpeed);
}

BOOL CANopenLibHelper::WriteHomingSpeedSearchSwitch(BYTE nNodeID, double dSpeed)
{
	return m_WriteHomingSpeedSearchSwitch(nNodeID, dSpeed);
}

BOOL CANopenLibHelper::ReadHomingSpeedSearchIndex(BYTE nNodeID, double* dSpeed)
{
	return m_ReadHomingSpeedSearchIndex(nNodeID, dSpeed);
}

BOOL CANopenLibHelper::WriteHomingSpeedSearchIndex(BYTE nNodeID, double dSpeed)
{
	return m_WriteHomingSpeedSearchIndex(nNodeID, dSpeed);
}

BOOL CANopenLibHelper::ReadHomingAcceleration(BYTE nNodeID, double* dHomingAcceleration)
{
	return m_ReadHomingAcceleration(nNodeID, dHomingAcceleration);
}

BOOL CANopenLibHelper::WriteHomingAcceleration(BYTE nNodeID, double dHomingAcceleration)
{
	return m_WriteHomingAcceleration(nNodeID, dHomingAcceleration);
}

BOOL CANopenLibHelper::ReadDriveOutputs(BYTE nNodeID, UINT* nDriveOutputs)
{
	return m_ReadDriveOutputs(nNodeID, nDriveOutputs);
}

BOOL CANopenLibHelper::WriteDriveOutputs(BYTE nNodeID, UINT nDriveOutputs)
{
	return m_WriteDriveOutputs(nNodeID, nDriveOutputs);
}

BOOL CANopenLibHelper::ReadTargetVelocity(BYTE nNodeID, double* dTargetVelocity)
{
	return m_ReadTargetVelocity(nNodeID, dTargetVelocity);
}

BOOL CANopenLibHelper::WriteTargetVelocity(BYTE nNodeID, double dTargetVelocity)
{
	return m_WriteTargetVelocity(nNodeID, dTargetVelocity);
}

BOOL CANopenLibHelper::ReadSupportedDriveModes(BYTE nNodeID, UINT* nModes)
{
	return m_ReadSupportedDriveModes(nNodeID, nModes);
}

BOOL CANopenLibHelper::ReadHomingSwitch(BYTE nNodeID, BYTE* nHomingSwitch)
{
	return m_ReadHomingSwitch(nNodeID, nHomingSwitch);
}

BOOL CANopenLibHelper::WriteHomingSwitch(BYTE nNodeID, BYTE nHomingSwitch)
{
	return m_WriteHomingSwitch(nNodeID, nHomingSwitch);
}

BOOL CANopenLibHelper::ReadIdleCurrent(BYTE nNodeID, double* dIdleCurrent)
{
	return m_ReadIdleCurrent(nNodeID, dIdleCurrent);
}

BOOL CANopenLibHelper::WriteIdleCurrent(BYTE nNodeID, double dIdleCurrent)
{
	return m_WriteIdleCurrent(nNodeID, dIdleCurrent);
}

BOOL CANopenLibHelper::ReadDisplayDriveInputs(BYTE nNodeID, USHORT* nInputs)
{
	return m_ReadDisplayDriveInputs(nNodeID, nInputs);
}

BOOL CANopenLibHelper::ReadTorqueConstant(BYTE nNodeID, USHORT* nTorqueConstant)
{
	return m_ReadTorqueConstant(nNodeID, nTorqueConstant);
}

BOOL CANopenLibHelper::WriteTorqueConstant(BYTE nNodeID, USHORT nTorqueConstant)
{
	return m_WriteTorqueConstant(nNodeID, nTorqueConstant);
}

BOOL CANopenLibHelper::WriteDSPClearAlarm(BYTE nNodeID)
{
	return m_WriteDSPClearAlarm(nNodeID);
}

BOOL CANopenLibHelper::ReadQSegment(BYTE nNodeID, BYTE* nQSegment)
{
	return m_ReadQSegment(nNodeID, nQSegment);
}

BOOL CANopenLibHelper::WriteQSegment(BYTE nNodeID, BYTE nQSegment)
{
	return m_WriteQSegment(nNodeID, nQSegment);
}

BOOL CANopenLibHelper::ReadActualVelocity(BYTE nNodeID, double* dActualVelocity)
{
	return m_ReadActualVelocity(nNodeID, dActualVelocity);
}

BOOL CANopenLibHelper::ReadActualPosition(BYTE nNodeID, int* nActualPosition)
{
	return m_ReadActualPosition(nNodeID, nActualPosition);
}

BOOL CANopenLibHelper::ReadDSPStatusCode(BYTE nNodeID, USHORT* nStatusCode)
{
	return m_ReadDSPStatusCode(nNodeID, nStatusCode);
}

BOOL CANopenLibHelper::WriteClearPosition(BYTE nNodeID)
{
	return m_WriteClearPosition(nNodeID);
}

BOOL CANopenLibHelper::ReadAccelerationCurrent(BYTE nNodeID, double* dAccelerationCurrent)
{
	return m_ReadAccelerationCurrent(nNodeID, dAccelerationCurrent);
}

BOOL CANopenLibHelper::WriteAccelerationCurrent(BYTE nNodeID, double dAccelerationCurrent)
{
	return m_WriteAccelerationCurrent(nNodeID, dAccelerationCurrent);
}

BOOL CANopenLibHelper::ReadAnalogInput1(BYTE nNodeID, USHORT* nAnalogInput1)
{
	return m_ReadAnalogInput1(nNodeID, nAnalogInput1);
}

BOOL CANopenLibHelper::WriteProfileParam(BYTE nNodeID, int* nModes, int* nDistance, double* dVelocity, double* dAccel, double* dDecel)
{
	return m_WriteProfileParam(nNodeID, nModes, nDistance, dVelocity, dAccel, dDecel);
}

BOOL CANopenLibHelper::SwitchControlWord(BYTE nNodeID, USHORT nControlWord1, USHORT nControlWord2)
{
	return m_SwitchControlWord(nNodeID, nControlWord1, nControlWord2);
}

BOOL CANopenLibHelper::DriveEnable(BYTE nNodeID, BOOL bEnabled)
{
	return m_DriveEnable(nNodeID, bEnabled);
}

BOOL CANopenLibHelper::Stop(BYTE nNodeID)
{
	return m_Stop(nNodeID);
}

BOOL CANopenLibHelper::AlarmReset(BYTE nNodeID)
{
	return m_AlarmReset(nNodeID);
}

BOOL CANopenLibHelper::RelMove(BYTE nNodeID, int nDistance, double* dVelocity, double* dAccel, double* dDecel)
{
	return m_RelMove(nNodeID, nDistance, dVelocity, dAccel, dDecel);
}

BOOL CANopenLibHelper::AbsMove(BYTE nNodeID, int nDistance, double* dVelocity, double* dAccel, double* dDecel)
{
	return m_AbsMove(nNodeID, nDistance, dVelocity, dAccel, dDecel);
}

BOOL CANopenLibHelper::MultipleAbsMoveWithStopping(BYTE nNodeID, int nDistance1, int nDistance2, double* dVelocity1, double* dVelocity2, double* dAccel, double* dDecel)
{
	return m_MultipleAbsMoveWithStopping(nNodeID, nDistance1, nDistance2, dVelocity1, dVelocity2, dAccel, dDecel);
}

BOOL CANopenLibHelper::MultipleAbsMoveContinuous(BYTE nNodeID, int nDistance1, int nDistance2, double* dVelocity1, double* dVelocity2, double* dAccel, double* dDecel, BOOL bImmediateChange)
{
	return m_MultipleAbsMoveContinuous(nNodeID, nDistance1, nDistance2, dVelocity1, dVelocity2, dAccel, dDecel, bImmediateChange);
}

BOOL CANopenLibHelper::ExecuteNormalQProgram(BYTE nNodeID, BYTE nSegment)
{
	return m_ExecuteNormalQProgram(nNodeID, nSegment);
}

BOOL CANopenLibHelper::ExecuteSyncQProgram(BYTE nNodeID, BYTE nSegment, UINT nSyncPulse)
{
	return m_ExecuteSyncQProgram(nNodeID, nSegment, nSyncPulse);
}

BOOL CANopenLibHelper::Homing(BYTE nNodeID, int nHomingMethod, double* dHomingVelocity, double* dIndexVelocity, double* dHomingAccel, int* nHomingOffset, int* nHomingSwitch)
{
	return m_Homing(nNodeID, nHomingMethod, dHomingVelocity, dIndexVelocity, dHomingAccel, nHomingOffset, nHomingSwitch);
}

BOOL CANopenLibHelper::LaunchVelocityMode(BYTE nNodeID, double* dVelocity, double* dAccel, double* dDecel)
{
	return m_LaunchVelocityMode(nNodeID, dVelocity, dAccel, dDecel);
}

