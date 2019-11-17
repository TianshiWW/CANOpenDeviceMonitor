// ************************************************************************************************
// File Name        : CANopenLibHelper.h
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

#pragma once

#define MAX_REGISTERCOUNT 100
#define MAX_BYTES_COUNT 1024

typedef struct _CANMESSAGE{
private:
protected:
public:
    long            id;
    UINT    dlc;
    BYTE            msg[8];
    UINT    flag;
    ULONG	timeStamp;
} CANMESSAGE, *PCANMESSAGE;


typedef struct _PDOMESSAGE{
public:
	BYTE NodeID;
	BYTE No;
	BYTE Len;
    BYTE            msg[8];
} PDOMESSAGE, *PPDOMESSAGE;

typedef struct _PDOMAPPING
{
	int Index;
	BYTE SubIndex;
	BYTE BitCounts;
} PDOMAPPING, *PPDOMAPPING;

typedef struct _ERROR_INFO
{
	int ErrorCode;
	char* Command;
	char* ErrorMessage;
} ERROR_INFO, *PERROR_INFO;


typedef   void   (CALLBACK   *EventCallback)();
typedef   void   (CALLBACK   *TPDOCallback)();

typedef enum EnumAdapter
{
	Kvaser,
	PEAK,
	ZLG,
};

typedef enum EnumBaudRate
{
	BitRate1Mbps,
	BitRate800kbps,
	BitRate500kbps,
	BitRate250kbps,
	BitRate125kbps,
	BitRate50kbps,
	BitRate20kbps,
	BitRate12D5kbps,
};

/// <summary>
/// PEAKDeviceType
/// </summary>
typedef enum KvaserDeviceType
{
	/// <summary>
	/// Unknown or undefined
	/// </summary>
	HWTYPE_NONE = 0,
	/// <summary>
	/// The virtual CAN bus
	/// </summary>
	HWTYPE_VIRTUAL = 1,
	/// <summary>
	/// LAPcan Family
	/// </summary>
	HWTYPE_LAPCAN = 2,
	/// <summary>
	/// CANpari (obsolete).
	/// </summary>
	HWTYPE_CANPARI = 3,
	/// <summary>
	/// PCcan Family
	/// </summary>
	HWTYPE_PCCAN = 8,
	/// <summary>
	/// PCIcan Family
	/// </summary>
	HWTYPE_PCICAN = 9,
	/// <summary>
	/// USBcan (obsolete).
	/// </summary>
	HWTYPE_USBCAN = 11,
	/// <summary>
	/// PCIcan II family
	/// </summary>
	HWTYPE_PCICAN_II = 40,
	/// <summary>
	/// USBcan II, USBcan Rugged, Kvaser Memorator
	/// </summary>
	HWTYPE_USBCAN_II = 42,
	/// <summary>
	/// Simulated CAN bus for Kvaser Creator (obsolete).
	/// </summary>
	HWTYPE_SIMULATED = 44,
	/// <summary>
	/// Kvaser Acquisitor (obsolete).
	/// </summary>
	HWTYPE_ACQUISITOR = 46,
	/// <summary>
	/// Kvaser Leaf Family
	/// </summary>
	HWTYPE_LEAF = 48,
	/// <summary>
	/// Kvaser PC104+
	/// </summary>
	HWTYPE_PC104_PLUS = 50,
	/// <summary>
	/// Kvaser PCIcanx II
	/// </summary>
	HWTYPE_PCICANX_II = 52,
	/// <summary>
	/// Kvaser Memorator Professional family
	/// </summary>
	HWTYPE_MEMORATOR_II = 54,
	/// <summary>
	/// Kvaser Memorator Professional family
	/// </summary>
	HWTYPE_MEMORATOR_PRO = 54,
	/// <summary>
	/// Kvaser USBcan Professional
	/// </summary>
	HWTYPE_USBCAN_PRO = 56,
	/// <summary>
	/// Obsolete name, use canHWTYPE_BLACKBIRD instead
	/// </summary>
	HWTYPE_IRIS = 58,
	/// <summary>
	/// Kvaser BlackBird
	/// </summary>
	HWTYPE_BLACKBIRD = 58,
	/// <summary>
	/// Kvaser Memorator Light
	/// </summary>
	HWTYPE_MEMORATOR_LIGHT = 60,
	/// <summary>
	/// Obsolete name, use canHWTYPE_EAGLE instead
	/// </summary>
	HWTYPE_MINIHYDRA = 62,
	/// <summary>
	/// Kvaser Eagle family
	/// </summary>
	HWTYPE_EAGLE = 62,
	/// <summary>
	/// Obsolete name, use canHWTYPE_BLACKBIRD_V2 instead
	/// </summary>
	HWTYPE_BAGEL = 64,
	/// <summary>
	/// Kvaser BlackBird v2
	/// </summary>
	HWTYPE_BLACKBIRD_V2 = 64,
	/// <summary>
	/// Kvaser Mini PCI Express
	/// </summary>
	HWTYPE_MINIPCIE = 66,
	/// <summary>
	/// USBcan Pro HS/K-Line
	/// </summary>
	HWTYPE_USBCAN_KLINE = 68,
	/// <summary>
	/// Kvaser Ethercan
	/// </summary>
	HWTYPE_ETHERCAN = 70,
	/// <summary>
	/// Kvaser USBcan Light
	/// </summary>
	HWTYPE_USBCAN_LIGHT = 72,
	/// <summary>
	/// Kvaser USBcan Pro 5xHS and variants
	/// </summary>
	HWTYPE_USBCAN_PRO2 = 74,
	/// <summary>
	/// Kvaser PCIEcan 4xHS and variants
	/// </summary>
	HWTYPE_PCIE_V2 = 76,
	/// <summary>
	/// Kvaser Memorator Pro 5xHS and variants
	/// </summary>
	HWTYPE_MEMORATOR_PRO2 = 78,
	/// <summary>
	/// Kvaser Leaf Pro HS v2 and variants
	/// </summary>
	HWTYPE_LEAF2 = 80,
	/// <summary>
	/// Kvaser Memorator (2nd generation)
	/// </summary>
	HWTYPE_MEMORATOR_V2 = 82,
};

/// <summary>
/// PEAKDeviceType
/// </summary>
typedef enum PEAKDeviceType
{
	/// <summary>
	/// Undefined/default value for a PCAN bus
	/// </summary>
	PCAN_NONEBUS = 0x00,

	/// <summary>
	/// PCAN-ISA interface, channel 1
	/// </summary>
	PCAN_ISABUS1 = 0x21,
	/// <summary>
	/// PCAN-ISA interface, channel 2
	/// </summary>
	PCAN_ISABUS2 = 0x22,
	/// <summary>
	/// PCAN-ISA interface, channel 3
	/// </summary>
	PCAN_ISABUS3 = 0x23,
	/// <summary>
	/// PCAN-ISA interface, channel 4
	/// </summary>
	PCAN_ISABUS4 = 0x24,
	/// <summary>
	/// PCAN-ISA interface, channel 5
	/// </summary>
	PCAN_ISABUS5 = 0x25,
	/// <summary>
	/// PCAN-ISA interface, channel 6
	/// </summary>
	PCAN_ISABUS6 = 0x26,
	/// <summary>
	/// PCAN-ISA interface, channel 7
	/// </summary>
	PCAN_ISABUS7 = 0x27,
	/// <summary>
	/// PCAN-ISA interface, channel 8
	/// </summary>
	PCAN_ISABUS8 = 0x28,

	/// <summary>
	/// PPCAN-Dongle/LPT interface, channel 1 
	/// </summary>
	PCAN_DNGBUS1 = 0x31,

	/// <summary>
	/// PCAN-PCI interface, channel 1
	/// </summary>
	PCAN_PCIBUS1 = 0x41,
	/// <summary>
	/// PCAN-PCI interface, channel 2
	/// </summary>
	PCAN_PCIBUS2 = 0x42,
	/// <summary>
	/// PCAN-PCI interface, channel 3
	/// </summary>
	PCAN_PCIBUS3 = 0x43,
	/// <summary>
	/// PCAN-PCI interface, channel 4
	/// </summary>
	PCAN_PCIBUS4 = 0x44,
	/// <summary>
	/// PCAN-PCI interface, channel 5
	/// </summary>
	PCAN_PCIBUS5 = 0x45,
	/// <summary>
	/// PCAN-PCI interface, channel 6
	/// </summary>
	PCAN_PCIBUS6 = 0x46,
	/// <summary>
	/// PCAN-PCI interface, channel 7
	/// </summary>
	PCAN_PCIBUS7 = 0x47,
	/// <summary>
	/// PCAN-PCI interface, channel 8
	/// </summary>
	PCAN_PCIBUS8 = 0x48,

	/// <summary>
	/// PCAN-USB interface, channel 1
	/// </summary>
	PCAN_USBBUS1 = 0x51,
	/// <summary>
	/// PCAN-USB interface, channel 2
	/// </summary>
	PCAN_USBBUS2 = 0x52,
	/// <summary>
	/// PCAN-USB interface, channel 3
	/// </summary>
	PCAN_USBBUS3 = 0x53,
	/// <summary>
	/// PCAN-USB interface, channel 4
	/// </summary>
	PCAN_USBBUS4 = 0x54,
	/// <summary>
	/// PCAN-USB interface, channel 5
	/// </summary>
	PCAN_USBBUS5 = 0x55,
	/// <summary>
	/// PCAN-USB interface, channel 6
	/// </summary>
	PCAN_USBBUS6 = 0x56,
	/// <summary>
	/// PCAN-USB interface, channel 7
	/// </summary>
	PCAN_USBBUS7 = 0x57,
	/// <summary>
	/// PCAN-USB interface, channel 8
	/// </summary>
	PCAN_USBBUS8 = 0x58,

	/// <summary>
	/// PCAN-PC Card interface, channel 1
	/// </summary>
	PCAN_PCCBUS1 = 0x61,
	/// <summary>
	/// PCAN-PC Card interface, channel 2
	/// </summary>
	PCAN_PCCBUS2 = 0x62,
};

/// <summary>
/// ZLGDeviceType
/// </summary>
typedef enum ZLGDeviceType
{
	PCI5121 = 1,
	PCI9810 = 2,
	USBCAN1 = 3,
	USBCAN2 = 4,
	USBCAN2A = 4,
	PCI9820 = 5,
	CAN232 = 6,
	PCI5110 = 7,
	CANLITE = 8,
	ISA9620 = 9,
	ISA5420 = 10,
	PC104CAN = 11,
	CANETUDP = 12,
	CANETE = 12,
	DNP9810 = 13,
	PCI9840 = 14,
	PC104CAN2 = 15,
	PCI9820I = 16,
	CANETTCP = 17,
	PEC9920 = 18,
	PCIE_9220 = 18,
	PCI5010U = 19,
	USBCAN_E_U = 20,
	USBCAN_2E_U = 21,
	PCI5020U = 22,
	EG20T_CAN = 23,
	PCIE9221 = 24,
	WIFICAN_TCP = 25,
	WIFICAN_UDP = 26,
	PCIe9120 = 27,
	PCIe9110 = 28,
	PCIe9140 = 29,
};

typedef void (__stdcall *lpOnHeartBeatReceive)(EventCallback func);
typedef void (__stdcall *lpOnDataSend)(EventCallback func);
typedef void (__stdcall *lpOnDataReceive)(EventCallback func);
typedef void (__stdcall *lpOnTPDOReceived)(TPDOCallback func);
typedef void (__stdcall *lpGetLastHeartBeatMessage)(CANMESSAGE& CANMessage);
typedef void (__stdcall *lpGetLastSentMessage)(CANMESSAGE& CANMessage);
typedef void (__stdcall *lpGetLastReceivedMessage)(CANMESSAGE& CANMessage);
typedef void (__stdcall *lpGetLastTPDOMessage)(PDOMESSAGE& TPDOMessage);
typedef void (__stdcall *lpGetLastTPDOMessageByNodeID)(BYTE nNodeID, BYTE nPDONo, PDOMESSAGE& PDOMessage);

typedef BOOL (__stdcall *lpOpen)(int nAdapter, EnumBaudRate nBaudRate, BYTE nChannel);
typedef BOOL (__stdcall *lpIsOpen)();
typedef BOOL (__stdcall *lpClose)();

typedef void (__stdcall *lpSetTriggerHeartBeatEvent)(BOOL bTriggerHeartBeatEvent);
typedef void (__stdcall *lpSetTriggerDataReceivedEvent)(BOOL bTriggerDataReceivedEvent);
typedef void (__stdcall *lpSetTriggerDataSentEvent)(BOOL bTriggerDataSentEvent);
typedef void (__stdcall *lpSetTriggerTPDOReceivedEvent)(BOOL bTriggerTPDOReceivedEvent);

typedef void (__stdcall *lpSetExecuteTimeOut)(BYTE nExecuteTimeOutinMs);
typedef UINT (__stdcall *lpGetExecuteTimeOut)();
typedef void (__stdcall *lpSetExecuteRetryTimes)(BYTE nExecuteRetryTimesinMs);
typedef UINT (__stdcall *lpGetExecuteRetryTimes)();
typedef BOOL (__stdcall *lpResetBuffer)();
typedef void (__stdcall *lpGetLastErrorInfo)(ERROR_INFO& pError_Info);
typedef BOOL (__stdcall *lpWrite)(CANMESSAGE pSendCanMessage);
typedef BOOL (__stdcall *lpExecuteCommand)(CANMESSAGE sendCanMessage, CANMESSAGE* pReceivedCanMessage, int nCanFunction, BOOL bMatchNodeID, BYTE nNodeID, BOOL bMatchIndex, int nIndex, BOOL bMatchFirstByte, BYTE nFirstByte);

typedef BOOL (__stdcall *lpReadSDOInt8)(BYTE nNodeID, int nIndex, BYTE nSubIndex, char* nData);

typedef BOOL (__stdcall *lpReadSDOUInt8)(BYTE nNodeID, int nIndex, BYTE nSubIndex, BYTE* nData);

typedef BOOL (__stdcall *lpReadSDOInt16)(BYTE nNodeID, int nIndex, BYTE nSubIndex, short* nData);

typedef BOOL (__stdcall *lpReadSDOUInt16)(BYTE nNodeID, int nIndex, BYTE nSubIndex, USHORT* nData);

typedef BOOL (__stdcall *lpReadSDOInt32)(BYTE nNodeID, int nIndex, BYTE nSubIndex, int* nData);

typedef BOOL (__stdcall *lpReadSDOUInt32)(BYTE nNodeID, int nIndex, BYTE nSubIndex, UINT* nData);

typedef BOOL (__stdcall *lpWriteSDOInt8)(BYTE nNodeID, int nIndex, BYTE nSubIndex, char nData);

typedef BOOL (__stdcall *lpWriteSDOUInt8)(BYTE nNodeID, int nIndex, BYTE nSubIndex, BYTE nData);

typedef BOOL (__stdcall *lpWriteSDOInt16)(BYTE nNodeID, int nIndex, BYTE nSubIndex, short nData);

typedef BOOL (__stdcall *lpWriteSDOUInt16)(BYTE nNodeID, int nIndex, BYTE nSubIndex, USHORT nData);

typedef BOOL (__stdcall *lpWriteSDOInt32)(BYTE nNodeID, int nIndex, BYTE nSubIndex, int nData);

typedef BOOL (__stdcall *lpWriteSDOUInt32)(BYTE nNodeID, int nIndex, BYTE nSubIndex, UINT nData);

typedef BOOL (__stdcall *lpSetToPreoperationalMode)(BYTE nNodeID);
typedef BOOL (__stdcall *lpSetToOperationalMode)(BYTE nNodeID);

typedef BOOL (__stdcall *lpSetRPDOMapping)(BYTE nNodeID, BYTE nRPDONo, int nLen, PDOMAPPING* pPDOMapping);
typedef BOOL (__stdcall *lpSetTPDOMapping)(BYTE nNodeID, BYTE nTPDONo, int nLen, PDOMAPPING* pPDOMapping);
typedef BOOL (__stdcall *lpRestorePDOMappingSettings)(BYTE nNodeID);
typedef BOOL (__stdcall *lpWriteRPDO)(BYTE nNodeID, BYTE nRPDONo, int nLen, BYTE* pData);
typedef BOOL (__stdcall *lpSaveParameters)(BYTE nNodeID);

typedef BOOL (__stdcall *lpReadPositionGain)(BYTE nNodeID, USHORT* nPositionGain);
typedef BOOL (__stdcall *lpWritePositionGain)(BYTE nNodeID, USHORT nPositionGain);
typedef BOOL (__stdcall *lpReadPositionDeriGain)(BYTE nNodeID, USHORT* nPositionDeriGain);
typedef BOOL (__stdcall *lpWritePositionDeriGain)(BYTE nNodeID, USHORT nPositionDeriGain);
typedef BOOL (__stdcall *lpReadPositionDeriFilter)(BYTE nNodeID, USHORT* nPositionDeriFilter);
typedef BOOL (__stdcall *lpWritePositionDeriFilter)(BYTE nNodeID, USHORT nPositionDeriFilter);
typedef BOOL (__stdcall *lpReadVelocityGain)(BYTE nNodeID, USHORT* nVelocityGain);
typedef BOOL (__stdcall *lpWriteVelocityGain)(BYTE nNodeID, USHORT nVelocityGain);
typedef BOOL (__stdcall *lpReadVelocityIntegGain)(BYTE nNodeID, USHORT* nVelocityIntegGain);
typedef BOOL (__stdcall *lpWriteVelocityIntegGain)(BYTE nNodeID, USHORT nVelocityIntegGain);
typedef BOOL (__stdcall *lpReadAccFeedForward)(BYTE nNodeID, USHORT* nAccFeedForward);
typedef BOOL (__stdcall *lpWriteAccFeedForward)(BYTE nNodeID, USHORT nAccFeedForward);
typedef BOOL (__stdcall *lpReadPIDFilter)(BYTE nNodeID, USHORT* nPIDFilter);
typedef BOOL (__stdcall *lpWritePIDFilter)(BYTE nNodeID, USHORT nPIDFilter);
typedef BOOL (__stdcall *lpReadNotchFilter)(BYTE nNodeID, int nIndex, USHORT* nNotchFilter);
typedef BOOL (__stdcall *lpWriteNotchFilter)(BYTE nNodeID, int nIndex, USHORT nNotchFilter);
typedef BOOL (__stdcall *lpReadPositionError)(BYTE nNodeID, USHORT* nPositionError);
typedef BOOL (__stdcall *lpWritePositionError)(BYTE nNodeID, USHORT nPositionError);
typedef BOOL (__stdcall *lpReadVelocityMax)(BYTE nNodeID, USHORT* nVelocityMax);
typedef BOOL (__stdcall *lpWriteVelocityMax)(BYTE nNodeID, USHORT nVelocityMax);
typedef BOOL (__stdcall *lpReadSmoothFilter)(BYTE nNodeID, USHORT* nSmoothFilter);
typedef BOOL (__stdcall *lpWriteSmoothFilter)(BYTE nNodeID, USHORT nSmoothFilter);
typedef BOOL (__stdcall *lpReadDriverTemp)(BYTE nNodeID, SHORT* nDriverTemp);
typedef BOOL (__stdcall *lpReadErrorCode)(BYTE nNodeID, USHORT* nErrorCode);
typedef BOOL (__stdcall *lpReadErrorCodeUpper)(BYTE nNodeID, USHORT* nErrorCode);
typedef BOOL (__stdcall *lpWriteControlWord)(BYTE nNodeID, USHORT nControlWord);
typedef BOOL (__stdcall *lpReadStatusWord)(BYTE nNodeID, USHORT* pStatusWord);
typedef BOOL (__stdcall *lpReadQuickStopOptionCode)(BYTE nNodeID, short* nOptionCode);
typedef BOOL (__stdcall *lpWriteQuickStopOptionCode)(BYTE nNodeID, short nOptionCode);
typedef BOOL (__stdcall *lpWriteModesofOperation)(BYTE nNodeID, char nModesofOperation);
typedef BOOL (__stdcall *lpReadModesofOperation)(BYTE nNodeID, char* nModesofOperation);
typedef BOOL (__stdcall *lpReadPositionTargetValueCalculated)(BYTE nNodeID, int* nPositionTargetValueCalculated);
typedef BOOL (__stdcall *lpReadFollowingErrorWindow)(BYTE nNodeID, UINT* pFollowingError);
typedef BOOL (__stdcall *lpWriteFollowingErrorWindow)(BYTE nNodeID, UINT nFollowingError);
typedef BOOL (__stdcall *lpReadVelocityTargetValueCalculated)(BYTE nNodeID, double* dVelocityTargetValueCalculated);
typedef BOOL (__stdcall *lpReadTargetTorque)(BYTE nNodeID, short* pTargetTorque);
typedef BOOL (__stdcall *lpWriteTargetTorque)(BYTE nNodeID, short nTargetTorque);
typedef BOOL (__stdcall *lpReadMaxRunningCurrent)(BYTE nNodeID, double* dRunningCurrent);
typedef BOOL (__stdcall *lpWriteMaxRunningCurrent)(BYTE nNodeID, double dRunningCurrent);
typedef BOOL (__stdcall *lpReadTorqueDemandValue)(BYTE nNodeID, double* dTorqueDemandValue);
typedef BOOL (__stdcall *lpReadCurrentActualValue)(BYTE nNodeID, double* dCurrentActualValue);
typedef BOOL (__stdcall *lpReadTargetPosition)(BYTE nNodeID, int* nTargetPositiont);
typedef BOOL (__stdcall *lpWriteTargetPosition)(BYTE nNodeID, int nTargetPositiont);
typedef BOOL (__stdcall *lpReadHomingOffset)(BYTE nNodeID, int* nHomingOffset);
typedef BOOL (__stdcall *lpWriteHomingOffset)(BYTE nNodeID, int nHomingOffset);
typedef BOOL (__stdcall *lpReadPolarity)(BYTE nNodeID, BYTE* pPolarity);
typedef BOOL (__stdcall *lpWritePolarity)(BYTE nNodeID, BYTE nPolarity);
typedef BOOL (__stdcall *lpReadMaxProfileSpeed)(BYTE nNodeID, double* dMaxProfileSpeed);
typedef BOOL (__stdcall *lpWriteMaxProfileSpeed)(BYTE nNodeID, double dMaxProfileSpeed);
typedef BOOL (__stdcall *lpReadProfileVelocity)(BYTE nNodeID, double* dProfileVelocity);
typedef BOOL (__stdcall *lpWriteProfileVelocity)(BYTE nNodeID, double dProfileVelocity);
typedef BOOL (__stdcall *lpReadProfileAcceleration)(BYTE nNodeID, double* dProfileAcceleration);
typedef BOOL (__stdcall *lpWriteProfileAcceleration)(BYTE nNodeID, double dProfileAcceleration);
typedef BOOL (__stdcall *lpReadProfileDeceleration)(BYTE nNodeID, double* dProfileAcceleration);
typedef BOOL (__stdcall *lpWriteProfileDeceleration)(BYTE nNodeID, double dProfileAcceleration);
typedef BOOL (__stdcall *lpReadQuickStopDeceleration)(BYTE nNodeID, double* dQuickStopDeceleration);
typedef BOOL (__stdcall *lpWriteQuickStopDeceleration)(BYTE nNodeID, double dQuickStopDeceleration);
typedef BOOL (__stdcall *lpReadTorqueSlop)(BYTE nNodeID, UINT* pTorqueSlop);
typedef BOOL (__stdcall *lpWriteTorqueSlop)(BYTE nNodeID, UINT nTorqueSlop);
typedef BOOL (__stdcall *lpReadHomingMethod)(BYTE nNodeID, BYTE* pHomingMethod);
typedef BOOL (__stdcall *lpWriteHomingMethod)(BYTE nNodeID, BYTE nHomingMethod);
typedef BOOL (__stdcall *lpReadHomingSpeedSearchSwitch)(BYTE nNodeID, double* dSpeed);
typedef BOOL (__stdcall *lpWriteHomingSpeedSearchSwitch)(BYTE nNodeID, double dSpeed);
typedef BOOL (__stdcall *lpReadHomingSpeedSearchIndex)(BYTE nNodeID, double* dSpeed);
typedef BOOL (__stdcall *lpWriteHomingSpeedSearchIndex)(BYTE nNodeID, double dSpeed);
typedef BOOL (__stdcall *lpReadHomingAcceleration)(BYTE nNodeID, double* dHomingAcceleration);
typedef BOOL (__stdcall *lpWriteHomingAcceleration)(BYTE nNodeID, double dHomingAcceleration);
typedef BOOL (__stdcall *lpReadDriveOutputs)(BYTE nNodeID, UINT* pDriveOutputs);
typedef BOOL (__stdcall *lpWriteDriveOutputs)(BYTE nNodeID, UINT nDriveOutputs);
typedef BOOL (__stdcall *lpReadTargetVelocity)(BYTE nNodeID, double* dTargetVelocity);
typedef BOOL (__stdcall *lpWriteTargetVelocity)(BYTE nNodeID, double dTargetVelocity);
typedef BOOL (__stdcall *lpReadSupportedDriveModes)(BYTE nNodeID, UINT* pModes);
typedef BOOL (__stdcall *lpReadHomingSwitch)(BYTE nNodeID, BYTE* pHomingSwitch);
typedef BOOL (__stdcall *lpWriteHomingSwitch)(BYTE nNodeID, BYTE nHomingSwitch);
typedef BOOL (__stdcall *lpReadIdleCurrent)(BYTE nNodeID, double* dIdleCurrent);
typedef BOOL (__stdcall *lpWriteIdleCurrent)(BYTE nNodeID, double dIdleCurrent);
typedef BOOL (__stdcall *lpReadDisplayDriveInputs)(BYTE nNodeID, USHORT* pInputs);
typedef BOOL (__stdcall *lpReadTorqueConstant)(BYTE nNodeID, USHORT* pTorqueConstant);
typedef BOOL (__stdcall *lpWriteTorqueConstant)(BYTE nNodeID, USHORT nTorqueConstant);
typedef BOOL (__stdcall *lpWriteDSPClearAlarm)(BYTE nNodeID);
typedef BOOL (__stdcall *lpReadQSegment)(BYTE nNodeID, BYTE* pQSegment);
typedef BOOL (__stdcall *lpWriteQSegment)(BYTE nNodeID, BYTE nQSegment);
typedef BOOL (__stdcall *lpReadActualVelocity)(BYTE nNodeID, double* dActualVelocity);
typedef BOOL (__stdcall *lpReadActualPosition)(BYTE nNodeID, int* nActualPosition);
typedef BOOL (__stdcall *lpReadDSPStatusCode)(BYTE nNodeID, USHORT* pStatusCode);
typedef BOOL (__stdcall *lpWriteClearPosition)(BYTE nNodeID);
typedef BOOL (__stdcall *lpReadAccelerationCurrent)(BYTE nNodeID, double* dAccelerationCurrent);
typedef BOOL (__stdcall *lpWriteAccelerationCurrent)(BYTE nNodeID, double dAccelerationCurrent);
typedef BOOL (__stdcall *lpReadAnalogInput1)(BYTE nNodeID, USHORT* pAnalogInput1);
typedef BOOL (__stdcall *lpWriteProfileParam)(BYTE nNodeID, int* nModes, int* nDistance, double* dVelocity, double* dAccel, double* dDecel);
typedef BOOL (__stdcall *lpSwitchControlWord)(BYTE nNodeID, USHORT nControlWord1, USHORT nControlWord2);
typedef BOOL (__stdcall *lpDriveEnable)(BYTE nNodeID, BOOL bEnabled);
typedef BOOL (__stdcall *lpStop)(BYTE nNodeID);
typedef BOOL (__stdcall *lpAlarmReset)(BYTE nNodeID);
typedef BOOL (__stdcall *lpRelMove)(BYTE nNodeID, int nDistance, double* dVelocity, double* dAccel, double* dDecel);
typedef BOOL (__stdcall *lpAbsMove)(BYTE nNodeID, int nDistance, double* dVelocity, double* dAccel, double* dDecel);
typedef BOOL (__stdcall *lpMultipleAbsMoveWithStopping)(BYTE nNodeID, int nDistance1, int nDistance2, double* dVelocity1, double* dVelocity2, double* dAccel, double* dDecel);
typedef BOOL (__stdcall *lpMultipleAbsMoveContinuous)(BYTE nNodeID, int nDistance1, int nDistance2, double* dVelocity1, double* dVelocity2, double* dAccel, double* dDecel, BOOL bImmediateChange);
typedef BOOL (__stdcall *lpExecuteNormalQProgram)(BYTE nNodeID, BYTE nSegment);
typedef BOOL (__stdcall *lpExecuteSyncQProgram)(BYTE nNodeID, BYTE nSegment, UINT nSyncPulse);
typedef BOOL (__stdcall *lpHoming)(BYTE nNodeID, int nHomingMethod, double* dHomingVelocity, double* dIndexVelocity, double* dHomingAccel, int* nHomingOffset, int* nHomingSwitch);
typedef BOOL (__stdcall *lpLaunchVelocityMode)(BYTE nNodeID, double* dVelocity, double* dAccel, double* dDecel);

class CANopenLibHelper
{
private:
	HINSTANCE m_hDll; //DLL Handle
	BOOL m_bWasLoaded;
protected:
	lpOnHeartBeatReceive m_OnHeartBeatReceive;
	lpOnDataSend m_OnDataSend;
	lpOnDataReceive m_OnDataReceive;
	lpOnTPDOReceived m_OnTPDOReceived;

	lpGetLastHeartBeatMessage m_GetLastHeartBeatMessage;
	lpGetLastSentMessage m_GetLastSentMessage;
	lpGetLastReceivedMessage m_GetLastReceivedMessage;
	lpGetLastTPDOMessage m_GetLastTPDOMessage;
	lpGetLastTPDOMessageByNodeID m_GetLastTPDOMessageByNodeID;

	lpOpen m_Open;
	lpClose m_Close;
	lpIsOpen m_IsOpen;

	lpSetTriggerHeartBeatEvent m_SetTriggerHeartBeatEvent;
	lpSetTriggerDataReceivedEvent m_SetTriggerDataReceivedEvent;
	lpSetTriggerDataSentEvent m_SetTriggerDataSentEvent;
	lpSetTriggerTPDOReceivedEvent m_SetTriggerTPDOReceivedEvent;

	lpSetExecuteTimeOut m_SetExecuteTimeOut;
	lpGetExecuteTimeOut m_GetExecuteTimeOut;
	lpSetExecuteRetryTimes m_SetExecuteRetryTimes;
	lpGetExecuteRetryTimes m_GetExecuteRetryTimes;
	lpResetBuffer m_ResetBuffer;
	lpGetLastErrorInfo m_GetLastErrorInfo;
	lpWrite m_Write;
	lpExecuteCommand m_ExecuteCommand;
	lpReadSDOInt8 m_ReadSDOInt8;
	lpReadSDOUInt8 m_ReadSDOUInt8;
	lpReadSDOInt16 m_ReadSDOInt16;
	lpReadSDOUInt16 m_ReadSDOUInt16;
	lpReadSDOInt32 m_ReadSDOInt32;
	lpReadSDOUInt32 m_ReadSDOUInt32;
	lpWriteSDOInt8 m_WriteSDOInt8;
	lpWriteSDOUInt8 m_WriteSDOUInt8;
	lpWriteSDOInt16 m_WriteSDOInt16;
	lpWriteSDOUInt16 m_WriteSDOUInt16;
	lpWriteSDOInt32 m_WriteSDOInt32;
	lpWriteSDOUInt32 m_WriteSDOUInt32;
	lpSetToPreoperationalMode m_SetToPreoperationalMode;
	lpSetToOperationalMode m_SetToOperationalMode;

	lpSetRPDOMapping m_SetRPDOMapping;
	lpSetTPDOMapping m_SetTPDOMapping;
	lpRestorePDOMappingSettings m_RestorePDOMappingSettings;
	lpWriteRPDO m_WriteRPDO;
	lpSaveParameters m_SaveParameters;

	lpReadPositionGain m_ReadPositionGain;
	lpWritePositionGain m_WritePositionGain;
	lpReadPositionDeriGain m_ReadPositionDeriGain;
	lpWritePositionDeriGain m_WritePositionDeriGain;
	lpReadPositionDeriFilter m_ReadPositionDeriFilter;
	lpWritePositionDeriFilter m_WritePositionDeriFilter;
	lpReadVelocityGain m_ReadVelocityGain;
	lpWriteVelocityGain m_WriteVelocityGain;
	lpReadVelocityIntegGain m_ReadVelocityIntegGain;
	lpWriteVelocityIntegGain m_WriteVelocityIntegGain;
	lpReadAccFeedForward m_ReadAccFeedForward;
	lpWriteAccFeedForward m_WriteAccFeedForward;
	lpReadPIDFilter m_ReadPIDFilter;
	lpWritePIDFilter m_WritePIDFilter;
	lpReadNotchFilter m_ReadNotchFilter;
	lpWriteNotchFilter m_WriteNotchFilter;
	lpReadPositionError m_ReadPositionError;
	lpWritePositionError m_WritePositionError;
	lpReadVelocityMax m_ReadVelocityMax;
	lpWriteVelocityMax m_WriteVelocityMax;
	lpReadSmoothFilter m_ReadSmoothFilter;
	lpWriteSmoothFilter m_WriteSmoothFilter;
	lpReadDriverTemp m_ReadDriverTemp;
	lpReadErrorCode m_ReadErrorCode;
	lpReadErrorCodeUpper m_ReadErrorCodeUpper;
	lpWriteControlWord m_WriteControlWord;
	lpReadStatusWord m_ReadStatusWord;
	lpReadQuickStopOptionCode m_ReadQuickStopOptionCode;
	lpWriteQuickStopOptionCode m_WriteQuickStopOptionCode;
	lpWriteModesofOperation m_WriteModesofOperation;
	lpReadModesofOperation m_ReadModesofOperation;
	lpReadPositionTargetValueCalculated m_ReadPositionTargetValueCalculated;
	lpReadFollowingErrorWindow m_ReadFollowingErrorWindow;
	lpWriteFollowingErrorWindow m_WriteFollowingErrorWindow;
	lpReadVelocityTargetValueCalculated m_ReadVelocityTargetValueCalculated;
	lpReadTargetTorque m_ReadTargetTorque;
	lpWriteTargetTorque m_WriteTargetTorque;
	lpReadMaxRunningCurrent m_ReadMaxRunningCurrent;
	lpWriteMaxRunningCurrent m_WriteMaxRunningCurrent;
	lpReadTorqueDemandValue m_ReadTorqueDemandValue;
	lpReadCurrentActualValue m_ReadCurrentActualValue;
	lpReadTargetPosition m_ReadTargetPosition;
	lpWriteTargetPosition m_WriteTargetPosition;
	lpReadHomingOffset m_ReadHomingOffset;
	lpWriteHomingOffset m_WriteHomingOffset;
	lpReadPolarity m_ReadPolarity;
	lpWritePolarity m_WritePolarity;
	lpReadMaxProfileSpeed m_ReadMaxProfileSpeed;
	lpWriteMaxProfileSpeed m_WriteMaxProfileSpeed;
	lpReadProfileVelocity m_ReadProfileVelocity;
	lpWriteProfileVelocity m_WriteProfileVelocity;
	lpReadProfileAcceleration m_ReadProfileAcceleration;
	lpWriteProfileAcceleration m_WriteProfileAcceleration;
	lpReadProfileDeceleration m_ReadProfileDeceleration;
	lpWriteProfileDeceleration m_WriteProfileDeceleration;
	lpReadQuickStopDeceleration m_ReadQuickStopDeceleration;
	lpWriteQuickStopDeceleration m_WriteQuickStopDeceleration;
	lpReadTorqueSlop m_ReadTorqueSlop;
	lpWriteTorqueSlop m_WriteTorqueSlop;
	lpReadHomingMethod m_ReadHomingMethod;
	lpWriteHomingMethod m_WriteHomingMethod;
	lpReadHomingSpeedSearchSwitch m_ReadHomingSpeedSearchSwitch;
	lpWriteHomingSpeedSearchSwitch m_WriteHomingSpeedSearchSwitch;
	lpReadHomingSpeedSearchIndex m_ReadHomingSpeedSearchIndex;
	lpWriteHomingSpeedSearchIndex m_WriteHomingSpeedSearchIndex;
	lpReadHomingAcceleration m_ReadHomingAcceleration;
	lpWriteHomingAcceleration m_WriteHomingAcceleration;
	lpReadDriveOutputs m_ReadDriveOutputs;
	lpWriteDriveOutputs m_WriteDriveOutputs;
	lpReadTargetVelocity m_ReadTargetVelocity;
	lpWriteTargetVelocity m_WriteTargetVelocity;
	lpReadSupportedDriveModes m_ReadSupportedDriveModes;
	lpReadHomingSwitch m_ReadHomingSwitch;
	lpWriteHomingSwitch m_WriteHomingSwitch;
	lpReadIdleCurrent m_ReadIdleCurrent;
	lpWriteIdleCurrent m_WriteIdleCurrent;
	lpReadDisplayDriveInputs m_ReadDisplayDriveInputs;
	lpReadTorqueConstant m_ReadTorqueConstant;
	lpWriteTorqueConstant m_WriteTorqueConstant;
	lpWriteDSPClearAlarm m_WriteDSPClearAlarm;
	lpReadQSegment m_ReadQSegment;
	lpWriteQSegment m_WriteQSegment;
	lpReadActualVelocity m_ReadActualVelocity;
	lpReadActualPosition m_ReadActualPosition;
	lpReadDSPStatusCode m_ReadDSPStatusCode;
	lpWriteClearPosition m_WriteClearPosition;
	lpReadAccelerationCurrent m_ReadAccelerationCurrent;
	lpWriteAccelerationCurrent m_WriteAccelerationCurrent;
	lpReadAnalogInput1 m_ReadAnalogInput1;
	lpWriteProfileParam m_WriteProfileParam;
	lpSwitchControlWord m_SwitchControlWord;
	lpDriveEnable m_DriveEnable;
	lpStop m_Stop;
	lpAlarmReset m_AlarmReset;
	lpRelMove m_RelMove;
	lpAbsMove m_AbsMove;
	lpMultipleAbsMoveWithStopping m_MultipleAbsMoveWithStopping;
	lpMultipleAbsMoveContinuous m_MultipleAbsMoveContinuous;
	lpExecuteNormalQProgram m_ExecuteNormalQProgram;
	lpExecuteSyncQProgram m_ExecuteSyncQProgram;
	lpHoming m_Homing;
	lpLaunchVelocityMode m_LaunchVelocityMode;

public:
	CANopenLibHelper(void);
	virtual ~CANopenLibHelper(void);

	void GetLastHeartBeatMessage(CANMESSAGE& CANMessage);
	void GetLastSentMessage(CANMESSAGE& CANMessage);
	void GetLastReceivedMessage(CANMESSAGE& CANMessage);
	void GetLastTPDOMessage(PDOMESSAGE& TPDOMessage);
	void GetLastTPDOMessageByNodeID(BYTE nNodeID, BYTE nPDONo, PDOMESSAGE& PDOMessage);

	BOOL Open(EnumAdapter nAdapter, EnumBaudRate nBaudRate, BYTE nChannel);
	BOOL IsOpen();
	BOOL Close();
	
	void SetExecuteTimeOut(BYTE nExecuteTimeOutinMs);
	BYTE GetExecuteTimeOut();
	void SetExecuteRetryTimes(BYTE nExecuteRetryTimesinMs);
	BYTE GetExecuteRetryTimes();
	BOOL ResetBuffer();
	void GetLastErrorInfo(ERROR_INFO& pError_Info);
	
	BOOL Write(CANMESSAGE pSendCanMessage);
	BOOL ExecuteCommand(CANMESSAGE sendCanMessage, CANMESSAGE* pReceivedCanMessage, int nCanFunction, BOOL bMatchNodeID = FALSE, BYTE nNodeID = 0, BOOL bMatchIndex = FALSE, int nIndex = 0, BOOL bMatchFirstByte = FALSE, BYTE nFirstByte = 0);
	
	BOOL ReadSDOInt8(BYTE nNodeID, int nIndex, BYTE nSubIndex, char* nData);

	BOOL ReadSDOUInt8(BYTE nNodeID, int nIndex, BYTE nSubIndex, BYTE* nData);

	BOOL ReadSDOInt16(BYTE nNodeID, int nIndex, BYTE nSubIndex, short* nData);

	BOOL ReadSDOUInt16(BYTE nNodeID, int nIndex, BYTE nSubIndex, USHORT* nData);

	BOOL ReadSDOInt32(BYTE nNodeID, int nIndex, BYTE nSubIndex, int* nData);

	BOOL ReadSDOUInt32(BYTE nNodeID, int nIndex, BYTE nSubIndex, UINT* nData);

	BOOL WriteSDOInt8(BYTE nNodeID, int nIndex, BYTE nSubIndex, char nData);

	BOOL WriteSDOUInt8(BYTE nNodeID, int nIndex, BYTE nSubIndex, BYTE nData);

	BOOL WriteSDOInt16(BYTE nNodeID, int nIndex, BYTE nSubIndex, short nData);

	BOOL WriteSDOUInt16(BYTE nNodeID, int nIndex, BYTE nSubIndex, USHORT nData);

	BOOL WriteSDOInt32(BYTE nNodeID, int nIndex, BYTE nSubIndex, int nData);

	BOOL WriteSDOUInt32(BYTE nNodeID, int nIndex, BYTE nSubIndex, UINT nData);

	BOOL SetToPreoperationalMode(BYTE nNodeID);
	BOOL SetToOperationalMode(BYTE nNodeID);

	BOOL SetRPDOMapping(BYTE nNodeID, BYTE nRPDONo, int nLen, PDOMAPPING* pPDOMapping);
	BOOL SetTPDOMapping(BYTE nNodeID, BYTE nTPDONo, int nLen, PDOMAPPING* pPDOMapping);
	BOOL RestorePDOMappingSettings(BYTE nNodeID);
	BOOL WriteRPDO(BYTE nNodeID, BYTE nRPDONo, int nLen, BYTE* pData);
	BOOL SaveParameters(BYTE nNodeID);

	BOOL ReadPositionGain(BYTE nNodeID, USHORT* nPositionGain);
	BOOL WritePositionGain(BYTE nNodeID, USHORT nPositionGain);
	BOOL ReadPositionDeriGain(BYTE nNodeID, USHORT* nPositionDeriGain);
	BOOL WritePositionDeriGain(BYTE nNodeID, USHORT nPositionDeriGain);
	BOOL ReadPositionDeriFilter(BYTE nNodeID, USHORT* nPositionDeriFilter);
	BOOL WritePositionDeriFilter(BYTE nNodeID, USHORT nPositionDeriFilter);
	BOOL ReadVelocityGain(BYTE nNodeID, USHORT* nVelocityGain);
	BOOL WriteVelocityGain(BYTE nNodeID, USHORT nVelocityGain);
	BOOL ReadVelocityIntegGain(BYTE nNodeID, USHORT* nVelocityIntegGain);
	BOOL WriteVelocityIntegGain(BYTE nNodeID, USHORT nVelocityIntegGain);
	BOOL ReadAccFeedForward(BYTE nNodeID, USHORT* nAccFeedForward);
	BOOL WriteAccFeedForward(BYTE nNodeID, USHORT nAccFeedForward);
	BOOL ReadPIDFilter(BYTE nNodeID, USHORT* nPIDFilter);
	BOOL WritePIDFilter(BYTE nNodeID, USHORT nPIDFilter);
	BOOL ReadNotchFilter(BYTE nNodeID, int nIndex, USHORT* nNotchFilter);
	BOOL WriteNotchFilter(BYTE nNodeID, int nIndex, USHORT nNotchFilter);
	BOOL ReadPositionError(BYTE nNodeID, USHORT* nPositionError);
	BOOL WritePositionError(BYTE nNodeID, USHORT nPositionError);
	BOOL ReadVelocityMax(BYTE nNodeID, USHORT* nVelocityMax);
	BOOL WriteVelocityMax(BYTE nNodeID, USHORT nVelocityMax);
	BOOL ReadSmoothFilter(BYTE nNodeID, USHORT* nSmoothFilter);
	BOOL WriteSmoothFilter(BYTE nNodeID, USHORT nSmoothFilter);
	BOOL ReadDriverTemp(BYTE nNodeID, short* nDriverTemp);
	BOOL ReadErrorCode(BYTE nNodeID, USHORT* nErrorCode);
	BOOL ReadErrorCodeUpper(BYTE nNodeID, USHORT* nErrorCode);
	BOOL WriteControlWord(BYTE nNodeID, USHORT nControlWord);
	BOOL ReadStatusWord(BYTE nNodeID, USHORT* pStatusWord);
	BOOL ReadQuickStopOptionCode(BYTE nNodeID, SHORT* nOptionCode);
	BOOL WriteQuickStopOptionCode(BYTE nNodeID, SHORT nOptionCode);
	BOOL WriteModesofOperation(BYTE nNodeID, char nModesofOperation);
	BOOL ReadModesofOperation(BYTE nNodeID, char* nModesofOperation);
	BOOL ReadPositionTargetValueCalculated(BYTE nNodeID, int* nPositionTargetValueCalculated);
	BOOL ReadFollowingErrorWindow(BYTE nNodeID, UINT* pFollowingError);
	BOOL WriteFollowingErrorWindow(BYTE nNodeID, UINT nFollowingError);
	BOOL ReadVelocityTargetValueCalculated(BYTE nNodeID, double* dVelocityTargetValueCalculated);
	BOOL ReadTargetTorque(BYTE nNodeID, short* pTargetTorque);
	BOOL WriteTargetTorque(BYTE nNodeID, short nTargetTorque);
	BOOL ReadMaxRunningCurrent(BYTE nNodeID, double* dRunningCurrent);
	BOOL WriteMaxRunningCurrent(BYTE nNodeID, double dRunningCurrent);
	BOOL ReadTorqueDemandValue(BYTE nNodeID, double* pTorqueDemandValue);
	BOOL ReadCurrentActualValue(BYTE nNodeID, double* pCurrentActualValue);
	BOOL ReadTargetPosition(BYTE nNodeID, int* nTargetPositiont);
	BOOL WriteTargetPosition(BYTE nNodeID, int nTargetPositiont);
	BOOL ReadHomingOffset(BYTE nNodeID, int* nHomingOffset);
	BOOL WriteHomingOffset(BYTE nNodeID, int nHomingOffset);
	BOOL ReadPolarity(BYTE nNodeID, BYTE* pPolarity);
	BOOL WritePolarity(BYTE nNodeID, BYTE nPolarity);
	BOOL ReadMaxProfileSpeed(BYTE nNodeID, double* dMaxProfileSpeed);
	BOOL WriteMaxProfileSpeed(BYTE nNodeID, double dMaxProfileSpeed);
	BOOL ReadProfileVelocity(BYTE nNodeID, double* dProfileVelocity);
	BOOL WriteProfileVelocity(BYTE nNodeID, double dProfileVelocity);
	BOOL ReadProfileAcceleration(BYTE nNodeID, double* dProfileAcceleration);
	BOOL WriteProfileAcceleration(BYTE nNodeID, double dProfileAcceleration);
	BOOL ReadProfileDeceleration(BYTE nNodeID, double* dProfileAcceleration);
	BOOL WriteProfileDeceleration(BYTE nNodeID, double dProfileAcceleration);
	BOOL ReadQuickStopDeceleration(BYTE nNodeID, double* dQuickStopDeceleration);
	BOOL WriteQuickStopDeceleration(BYTE nNodeID, double dQuickStopDeceleration);
	BOOL ReadTorqueSlop(BYTE nNodeID, UINT* pTorqueSlop);
	BOOL WriteTorqueSlop(BYTE nNodeID, UINT nTorqueSlop);
	BOOL ReadHomingMethod(BYTE nNodeID, BYTE* pHomingMethod);
	BOOL WriteHomingMethod(BYTE nNodeID, BYTE nHomingMethod);
	BOOL ReadHomingSpeedSearchSwitch(BYTE nNodeID, double* dSpeed);
	BOOL WriteHomingSpeedSearchSwitch(BYTE nNodeID, double dSpeed);
	BOOL ReadHomingSpeedSearchIndex(BYTE nNodeID, double* dSpeed);
	BOOL WriteHomingSpeedSearchIndex(BYTE nNodeID, double dSpeed);
	BOOL ReadHomingAcceleration(BYTE nNodeID, double* dHomingAcceleration);
	BOOL WriteHomingAcceleration(BYTE nNodeID, double dHomingAcceleration);
	BOOL ReadDriveOutputs(BYTE nNodeID, UINT* pDriveOutputs);
	BOOL WriteDriveOutputs(BYTE nNodeID, UINT nDriveOutputs);
	BOOL ReadTargetVelocity(BYTE nNodeID, double* dTargetVelocity);
	BOOL WriteTargetVelocity(BYTE nNodeID, double dTargetVelocity);
	BOOL ReadSupportedDriveModes(BYTE nNodeID, UINT* pModes);
	BOOL ReadHomingSwitch(BYTE nNodeID, BYTE* pHomingSwitch);
	BOOL WriteHomingSwitch(BYTE nNodeID, BYTE nHomingSwitch);
	BOOL ReadIdleCurrent(BYTE nNodeID, double* dIdleCurrent);
	BOOL WriteIdleCurrent(BYTE nNodeID, double dIdleCurrent);
	BOOL ReadDisplayDriveInputs(BYTE nNodeID, USHORT* pInputs);
	BOOL ReadTorqueConstant(BYTE nNodeID, USHORT* pTorqueConstant);
	BOOL WriteTorqueConstant(BYTE nNodeID, USHORT nTorqueConstant);
	BOOL WriteDSPClearAlarm(BYTE nNodeID);
	BOOL ReadQSegment(BYTE nNodeID, BYTE* pQSegment);
	BOOL WriteQSegment(BYTE nNodeID, BYTE nQSegment);
	BOOL ReadActualVelocity(BYTE nNodeID, double* dActualVelocity);
	BOOL ReadActualPosition(BYTE nNodeID, int* nActualPosition);
	BOOL ReadDSPStatusCode(BYTE nNodeID, USHORT* pStatusCode);
	BOOL WriteClearPosition(BYTE nNodeID);
	BOOL ReadAccelerationCurrent(BYTE nNodeID, double* dAccelerationCurrent);
	BOOL WriteAccelerationCurrent(BYTE nNodeID, double dAccelerationCurrent);
	BOOL ReadAnalogInput1(BYTE nNodeID, USHORT* pAnalogInput1);
	BOOL WriteProfileParam(BYTE nNodeID, int* nModes, int* nDistance, double* dVelocity, double* dAccel, double* dDecel);
	BOOL SwitchControlWord(BYTE nNodeID, USHORT nControlWord1, USHORT nControlWord2);
	BOOL DriveEnable(BYTE nNodeID, BOOL bEnabled);
	BOOL Stop(BYTE nNodeID);
	BOOL AlarmReset(BYTE nNodeID);
	BOOL RelMove(BYTE nNodeID, int nDistance, double* dVelocity, double* dAccel, double* dDecel);
	BOOL AbsMove(BYTE nNodeID, int nDistance, double* dVelocity, double* dAccel, double* dDecel);
	BOOL MultipleAbsMoveWithStopping(BYTE nNodeID, int nDistance1, int nDistance2, double* dVelocity1, double* dVelocity2, double* dAccel, double* dDecel);
	BOOL MultipleAbsMoveContinuous(BYTE nNodeID, int nDistance1, int nDistance2, double* dVelocity1, double* dVelocity2, double* dAccel, double* dDecel, BOOL bImmediateChange);
	BOOL ExecuteNormalQProgram(BYTE nNodeID, BYTE nSegment);
	BOOL ExecuteSyncQProgram(BYTE nNodeID, BYTE nSegment, UINT nSyncPulse);
	BOOL Homing(BYTE nNodeID, int nHomingMethod, double* dHomingVelocity, double* dIndexVelocity, double* dHomingAccel, int* nHomingOffset, int* nHomingSwitch);
	BOOL LaunchVelocityMode(BYTE nNodeID, double* dVelocity, double* dAccel, double* dDecel);

};

