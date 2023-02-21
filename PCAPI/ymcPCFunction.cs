//===========================================================
// C#
//
// API declaration module for ymcPCAPI.Dll ＜ymcPCFunction.cs＞
//
// Version (date)		：  Ver.1.0.0.0 (12/02/27)
//
// Remarks              ：	
//
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MotionAPI
{
    public partial class CMotionAPI
    {
#if WIN32
        const string DllName = "ymcPCAPI.dll";
#elif WIN64
        const string DllName = "ymcPCAPI_x64.dll";
#else

#endif
        const string DllName = "ymcPCAPI.dll";
        /////////////////////////////////////////////////////////////
        // ymcPCAPI Declare List
        /////////////////////////////////////////////////////////////

        //***********************************************************
        // Sequential/event, common motion APIs
        //***********************************************************
        //===========================================================
        // device
        //===========================================================
        // Function name: ymcClearAllAxes (Deleting axis all definition)
        [DllImport(DllName)]
        public static extern UInt32 ymcClearAllAxes();

        // Function name: ymcDeclareAxis (Declaring axis handle)
        [DllImport(DllName)]
        public static extern UInt32 ymcDeclareAxis(
            UInt16 RackNo,
            UInt16 SlotNo,
            UInt16 SubslotNo,
            UInt16 AxisNo,
            UInt16 LogicalAxisNo,
            UInt16 AxisType,
            String pAxisName,
            ref UInt32 phAxis);

        // Function name: ymcClearAxis (Deleting axis definition)
        [DllImport(DllName)]
        public static extern UInt32 ymcClearAxis(
            UInt32 hAxis);


        // Function name: ymcDeclareDevice (Declaring device)
        [DllImport(DllName)]
        public static extern UInt32 ymcDeclareDevice(
            UInt16 AxisNum,
            [In] UInt32[] pAxis,
            ref UInt32 pDevice);

        // Function name: ymcClearDevice (Deleting device)
        [DllImport(DllName)]
        public static extern UInt32 ymcClearDevice(
            UInt32 hDevice);


        // Function name: ymcGetAxisHandle (Getting axis handle information)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetAxisHandle(
            UInt16 SpecifyType,
            UInt16 RackNo,
            UInt16 SlotNo,
            UInt16 SubslotNo,
            UInt16 AxisNo,
            UInt16 LogicalAxisNo,
            String pAxisName,
            ref UInt32 pAxis);
        //===========================================================
        // Unit conversion function
        //===========================================================
        // Function name: ymcConvertFloat2Fix (Converting floating-point data to fixed-point data)
        [DllImport(DllName)]
        public static extern UInt32 ymcConvertFloat2Fix(
            UInt16 digit,
            Double FloatValue,
            ref Int32 pFixValue);

        // Function name: ymcConvertFix2Float (Converting fixed-point data to floating-point data)
        [DllImport(DllName)]
        public static extern UInt32 ymcConvertFix2Float(
            UInt16 digit,
            Int32 FixValue,
            ref Double pFloatValue);


        //===========================================================
        // axis Information
        //===========================================================
        // Function name: ymcMoveTorque (Torque reference function)
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveTorque(
            UInt32 hDevice,
            [In] POSITION_DATA[] pSpeedLimit,
            [In] POSITION_DATA[] pTorqueData,
            UInt32 hMoveIO,
            String pObjectName,
            UInt32 SystemOption);
        
        //===========================================================
        // parameter operation
        //===========================================================
        // Function name: ymcSetMotionParameterValue (Motion parameter setting)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetMotionParameterValue(
            UInt32 hAxis,
            UInt16 ParameterType,
            UInt32 ParameterOffset,
            UInt32 Value);


        // Function name: ymcGetMotionParameterValue (Motion parameter got)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetMotionParameterValue(
            UInt32 hAxis,
            UInt16 ParameterType,
            UInt32 ParameterOffset,
            ref UInt32 pStoredValue);


        // Function name: ymcDefinePosition (Setting current position)
        [DllImport(DllName)]
        public static extern UInt32 ymcDefinePosition(
            UInt32 hDevice,
            [In] POSITION_DATA[] pPos);

        //***********************************************************
        // Sequential motion APIs
        //***********************************************************
        //===========================================================
        // Positioning function
        //===========================================================
        // Function name: ymcMovePositioning (Positioning: sequential type)
        [DllImport(DllName)]
        public static extern UInt32 ymcMovePositioning(
            UInt32 hDevice,
            [In] MOTION_DATA[] pMotionData,
            [In] POSITION_DATA[] pPos,
            UInt32 hMoveIO,
            String pObjectName,
            UInt16 WaitForCompletion,
            UInt32 SystemOption);

        // Function name: ymcMoveExternalPositioning (External positioning)
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveExternalPositioning(
            UInt32 hDevice,
            [In] MOTION_DATA[] pMotionData,
            [In] POSITION_DATA[] pPositionData,
            [In] POSITION_DATA[] pMaxLatchPos,
            [In] POSITION_DATA[] pMinLatchPos,
            [In] POSITION_DATA[] pDistance,
            UInt32 hMoveIO,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);

        // Function name: ymcMoveIntimePositioning (Positioning with time specification function)
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveIntimePositioning(
            UInt32 hDevice,
            [In] MOTION_DATA[] pMotionData,
            [In] POSITION_DATA[] pPos,
            [In] DWORD_DATA[] pPositioningTime,
            UInt32 hMoveIO,
            String pObjectName,
            UInt16 WaitForCompletion,
            UInt32 SystemOption);

        // Function name: ymcMoveHomePosition (Home position return)
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveHomePosition(
            UInt32 hDevice,
            [In] MOTION_DATA[] pMotionData,
            [In] POSITION_DATA[] pOffsetPosition,
            [In] UInt16[] pHomingMethod,
            [In] UInt16[] pDirection,
            UInt32 hMoveIO,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);

        // Function name: ymcMoveDriverPositioning (Driver positioning)
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveDriverPositioning(
            UInt32 hDevice,
            [In] MOTION_DATA[] pMotionData,
            [In] POSITION_DATA[] pPos,
            UInt32 hMoveIO,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);

        // Function name: ymcStopMotion (Stopping axis execution)
        [DllImport(DllName)]
        public static extern UInt32 ymcStopMotion(
            UInt32 hDevice,
            [In] MOTION_DATA[] pMotionData,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);

        // Function name: ymcMoveJOG (Executing JOG operation)
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveJOG(
            UInt32 hDevice,
            [In] MOTION_DATA[] pMotionData,
            [In] Int16[] pDirection,
            [In] UInt16[] pTimeout,
            UInt32 hMoveIO,
            String pObjectName,
            UInt32 SystemOption);

        // Function name: ymcStopJOG (Stopping JOG operation)
        [DllImport(DllName)]
        public static extern UInt32 ymcStopJOG(
            UInt32 hDevice,
            UInt32 hMoveIO,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);


        //===========================================================
        // interpolation
        //===========================================================
        // Function name: ymcMoveLinear (Linear interpolation)
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveLinear(
            UInt32 hDevice,
            ref MOTION_DATA pMotionData,
            [In] POSITION_DATA[] pPos,
            UInt32 hMoveIO,
            String pObjectName,
            UInt16 WaitForCompletion,
            UInt32 SystemOption);
        
        // Function name(): ymcMoveCircularRadius [Circular interpolation (radius specified)]
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveCircularRadius(
            UInt32 hDevice,
            ref MOTION_DATA pMotionData,
            [In] POSITION_DATA[] pEndPoint,
            ref DWORD_DATA pRadius,
            ref DWORD_DATA pTurnNumber,
            UInt16 Direction,
            UInt16 AngleType,
            UInt32 hMoveIO,
            String pObjectName,
            UInt16 WaitForCompletion,
            UInt32 SystemOption);
        // Function name(): ymcMoveCircularCenter [Circular interpolation (center point coordinate specified)]
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveCircularCenter(
            UInt32 hDevice,
            ref MOTION_DATA pMotionData,
            [In] POSITION_DATA[] pEndPoint,
            [In] POSITION_DATA[] pCenter,
            ref DWORD_DATA pTurnNumber,
            UInt16 Direction,
            UInt32 hMoveIO,
            String pObjectName,
            UInt16 WaitForCompletion,
            UInt32 SystemOption);
        
        // Function name(): ymcMoveHelicalRadius [Helical interpolation (radius specified)]
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveHelicalRadius(
            UInt32 hDevice,
            ref MOTION_DATA pMotionData,
            [In] POSITION_DATA[] pEndPoint,
            ref DWORD_DATA pRadius,
            ref DWORD_DATA pTurnNumber,
            UInt16 Direction,
            UInt16 AngleType,
            UInt32 hMoveIO,
            String pObjectName,
            UInt16 WaitForCompletion,
            UInt32 SystemOption);

        // Function name(): ymcMoveHelicalCenter [Helical interpolation (center point coordinate specified)]
        [DllImport(DllName)]
        public static extern UInt32 ymcMoveHelicalCenter(
            UInt32 hDevice,
            ref MOTION_DATA pMotionData,
            [In] POSITION_DATA[] pEndPoint,
            [In] POSITION_DATA[] pCenter,
            ref DWORD_DATA pTurnNumber,
            UInt16 Direction,
            UInt32 hMoveIO,
            String pObjectName,
            UInt16 WaitForCompletion,
            UInt32 SystemOption);

        //===========================================================
        // Synchronization (Gear)
        //===========================================================
        // Function name: ymcEnableGear (Starting gear control)
        [DllImport(DllName)]
        public static extern UInt32 ymcEnableGear(
            UInt32 hAxis,
            UInt32 hDevice,
            UInt32 MasterType,
            [In] SYNC_DISTANCE[] pSyncDistance,
            [In] UInt32[] pStatus,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);
        
        // Function name: ymcDisableGear (Stopping gear control)
        [DllImport(DllName)]
        public static extern UInt32 ymcDisableGear(
            UInt32 hAxis,
            UInt32 hDevice,
            UInt32 MasterType,
            [In] SYNC_DISTANCE[] pSyncDistance,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);

        // Function name: ymcSetGearRatio (Setting gear ratio)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetGearRatio(
            UInt32 hDevice,
            [In] GEAR_RATIO_DATA[] pGearRatioData,
            UInt32 SystemOption);


        //===========================================================
        // compensation
        //===========================================================
        // Function name: ymcPositionOffset (Position compensation)
        [DllImport(DllName)]
        public static extern UInt32 ymcPositionOffset(
            UInt32 hDevice,
            [In] POSITION_OFFSET_DATA[] pPositionOffsetData,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);
        
        //===========================================================
        // Motion operation
        //===========================================================
        // Function name: ymcChangeDynamics (Changing motion data)
        [DllImport(DllName)]
        public static extern UInt32 ymcChangeDynamics(
            UInt32 hDevice,
            [In] MOTION_DATA[] pMotionData,
            [In] POSITION_DATA[] pPos,
            UInt32 Subject,
            String pObjectName,
            UInt32 SystemOption);
        
        //===========================================================
        // Driver operation
        //===========================================================
        // Function name: ymcServoControl (Servo ON or OFF)
        [DllImport(DllName)]
        public static extern UInt32 ymcServoControl(
            UInt32 hDevice,
            UInt16 ControlType,
            UInt16 TimeOut);
        
        //===========================================================
        // others
        //===========================================================
        // Function name: ymcEnableLatch (Starting latch)
        [DllImport(DllName)]
        public static extern UInt32 ymcEnableLatch(
            UInt32 hDevice,
            String pObjectName,
            [In] UInt16[] pWaitForCompletion,
            UInt32 SystemOption);

        // Function name: ymcDisableLatch (Stopping latch)
        [DllImport(DllName)]
        public static extern UInt32 ymcDisableLatch(
            UInt32 hDevice,
            String pObjectName,
            UInt32 SystemOption);


        //***********************************************************
        // System APIs
        //***********************************************************
        //===========================================================
        // Data operation
        //===========================================================
        // Function name: ymcSetIoDataBit (Setting bit)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetIoDataBit(
            ref IO_DATA pIoData,
            UInt32 pStoredBitValue);
        
        // Function name: ymcGetIoDataBit (Getting bit)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetIoDataBit(
            ref IO_DATA pIoData,
            ref UInt32 pStoredBitValue);

        // Function name: ymcSetIoDataValue (Setting data)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetIoDataValue(
            ref IO_DATA pIoData,
            UInt32 Value);
        
        // Function name: ymcGetIoDataValue (Getting data)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetIoDataValue(
            ref IO_DATA pIoData,
            ref UInt32 pStoredValue);
            
        // Function name: ymcSetRegisterData (Setting value to register)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetRegisterData(
            UInt32 hRegisterData,
            UInt32 RegisterDataNumber,
            [In] Int16[] pRegisterData);
        [DllImport(DllName)]
        public static extern UInt32 ymcSetRegisterData(
            UInt32 hRegisterData,
            UInt32 RegisterDataNumber,
            [In] UInt16[] pRegisterData);
        [DllImport(DllName)]
        public static extern UInt32 ymcSetRegisterData(
            UInt32 hRegisterData,
            UInt32 RegisterDataNumber,
            [In] Int32[] pRegisterData);
        [DllImport(DllName)]
        public static extern UInt32 ymcSetRegisterData(
            UInt32 hRegisterData,
            UInt32 RegisterDataNumber,
            [In] UInt32[] pRegisterData);

        // Function name: ymcSetRegisterDataEx (Setting value to extended register)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetRegisterDataEx(
            UInt64 hRegisterDataEx,
            UInt32 RegisterDataNumber,
            [In] Int16[] pRegisterData);
        [DllImport(DllName)]
        public static extern UInt32 ymcSetRegisterDataEx(
            UInt64 hRegisterDataEx,
            UInt32 RegisterDataNumber,
            [In] UInt16[] pRegisterData);
        [DllImport(DllName)]
        public static extern UInt32 ymcSetRegisterDataEx(
            UInt64 hRegisterDataEx,
            UInt32 RegisterDataNumber,
            [In] Int32[] pRegisterData);
        [DllImport(DllName)]
        public static extern UInt32 ymcSetRegisterDataEx(
            UInt64 hRegisterDataEx,
            UInt32 RegisterDataNumber,
            [In] UInt32[] pRegisterData);

        // Function name: ymcGetRegisterData (Reading value from register)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterData(
            UInt32 hRegisterData,
            UInt32 RegisterDataNumber,
            [In] Int16[] pRegisterData,
            ref UInt32 pReadDataNumber);
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterData(
            UInt32 hRegisterData,
            UInt32 RegisterDataNumber,
            [In] UInt16[] pRegisterData,
            ref UInt32 pReadDataNumber);
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterData(
            UInt32 hRegisterData,
            UInt32 RegisterDataNumber,
            [In] Int32[] pRegisterData,
            ref UInt32 pReadDataNumber);
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterData(
            UInt32 hRegisterData,
            UInt32 RegisterDataNumber,
            [In] UInt32[] pRegisterData,
            ref UInt32 pReadDataNumber);

        // Function name: ymcGetRegisterDataEx (Reading value from extended register)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterDataEx(
            UInt64 hRegisterDataEx,
            UInt32 RegisterDataNumber,
            [In] Int16[] pRegisterData,
            ref UInt32 pReadDataNumber);
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterDataEx(
            UInt64 hRegisterDataEx,
            UInt32 RegisterDataNumber,
            [In] UInt16[] pRegisterData,
            ref UInt32 pReadDataNumber);
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterDataEx(
            UInt64 hRegisterDataEx,
            UInt32 RegisterDataNumber,
            [In] Int32[] pRegisterData,
            ref UInt32 pReadDataNumber);
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterDataEx(
            UInt64 hRegisterDataEx,
            UInt32 RegisterDataNumber,
            [In] UInt32[] pRegisterData,
            ref UInt32 pReadDataNumber);

        // Function name: ymcGetRegisterDataHandle (Getting register handle)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterDataHandle(
            String pRegisterName,
            ref UInt32 hRegisterData);

        // Function name: ymcGetRegisterDataHandleEx (Getting extended register handle)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetRegisterDataHandleEx(
            String pRegisterName,
            ref UInt64 hRegisterDataEx);

        // Function name: ymcSetGroupRegisterData (Setting value to register (Multi))
        [DllImport(DllName)]
        public static extern UInt32 ymcSetGroupRegisterData(
            UInt32 GroupNumber,
            [In] REGISTER_INFO[] pRegisterInfo);

        // Function name: ymcSetGroupRegisterDataEx (Setting value to register (Multi))
        [DllImport(DllName)]
        public static extern UInt32 ymcSetGroupRegisterDataEx(
            UInt32 GroupNumber,
            [In] REGISTER_INFO_EX[] pRegisterInfoEx);
        
        // Function name: ymcGetGroupRegisterData (Reading value from register (Multi))
        [DllImport(DllName)]
        public static extern UInt32 ymcGetGroupRegisterData(
            UInt32 GroupNumber,
            [In] REGISTER_INFO[] pRegisterInfo);

        // Function name: ymcGetGroupRegisterDataEx (Reading value from extended register (Multi))
        [DllImport(DllName)]
        public static extern UInt32 ymcGetGroupRegisterDataEx(
            UInt32 GroupNumber,
            [In] REGISTER_INFO_EX[] pRegisterInfoEx);

        //===========================================================
        // System Information
        //==========================================================
        // Function name: ymcClearAlarm (Clearing alarm)
        [DllImport(DllName)]
        public static extern UInt32 ymcClearAlarm(
            UInt32 hAlarm);

        // Function name: ymcClearServoAlarm (Clearing alarm)
        [DllImport(DllName)]
        public static extern UInt32 ymcClearServoAlarm(
            UInt32 hAxis);
        
        // Function name: ymcGetAlarm (Getting current alarm)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetAlarm(
            UInt32 Number,
            [Out] UInt32[] pAlarm,
            [Out] ALARM_INFO[] pAlarmInfo,
            ref UInt32 pAlarmNumber);
        
        //============================================================
        // System operation
        //============================================================
        // Function name: ymcSetController (Changing target MP2100)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetController(
            UInt32 hController);
        
        // Function name: ymcGetController (Getting the current target Controller handle)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetController(
            ref UInt32 hController);

        // Function name: ymcResetController (reset target MP2100)
        [DllImport(DllName)]
        public static extern UInt32 ymcResetController(
            ref UInt32 hController);

        // Function name: ymcGetBusIFData (Getting value to BusIF)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetBusIFData(
            UInt32 Offset,
            UInt32 Size,
            [Out] UInt16[] pBusIFData);
        
        // Function name: ymcSetBusIFData (Setting value to BusIF)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetBusIFData(
            UInt32 Offset,
            UInt32 Size,
            [In] UInt16[] pBusIFData);

        // Function name: ymcGetBusIFInfo (Setting value to BusIF Information)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetBusIFInfo(
            ref BUSIF_INFO pBusIFInfo);

        //============================================================
        // Declaring or deleting ComDevice
        //============================================================
        // Function name: ymcOpenController (Declaring ComDevice)
        [DllImport(DllName)]
        public static extern UInt32 ymcOpenController(
            ref COM_DEVICE pComDevice,
            ref UInt32 hController);

        // Function name: ymcCloseController (Deleting ComDevice)
        [DllImport(DllName)]
        public static extern UInt32 ymcCloseController(
            UInt32 hController);

        //============================================================
        // Calendar operation
        //============================================================
        // Function name: ymcSetCalendar (Setting calendar)
        [DllImport(DllName)]
        public static extern UInt32 ymcSetCalendar(
            ref CALENDAR_DATA pCalendarData);
        
        // Function name: ymcGetCalendar (Getting calendar)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetCalendar(
            ref CALENDAR_DATA pCalendarData);

        //============================================================
        // Last error
        //============================================================
        // Function name: ymcGetLastError (Reading out last error)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetLastError();

        //============================================================
        // Timer
        //============================================================
        // Function name: ymcSetAPITimeoutValue
        [DllImport(DllName)]
        public static extern UInt32 ymcSetAPITimeoutValue(
            UInt32 TimeoutValue);

        // Funciton name: ymcWaitTime
        [DllImport(DllName)]
        public static extern UInt32 ymcWaitTime(
            UInt32 WaitTime,
            String pObjectName);

        //===========================================================
        // Logging
        //===========================================================
        // Function name： ymcGetLoggingPath (Getting logfile's file path)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetLoggingPath(
            String pMPLogPathName,
            UInt32 pMPStatus);

        //関数名： ymcGetLoggingFile (Storing logfile)
        [DllImport(DllName)]
        public static extern UInt32 ymcGetLoggingFile(
            String pMPLogPathName,
            String pWriteFileName);
    }
}