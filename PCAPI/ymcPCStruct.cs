//===========================================================
// C#
//
// Structure difinition module for ymcPCAPI.Dll ÅÉymcPCStruct.csÅÑ
//
// Version (date)		ÅF  Ver.1.0.0.0 (12/02/27)
//
// Remarks              ÅF	
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
        // Structure to store communication settings. 
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct COM_DEVICE
        {
            public UInt16 		ComDeviceType;             	// Communication type
            public UInt16 		PortNumber;                	// Port number
            public UInt16 		CpuNumber;                 	// CPU number
            public UInt16 		NetworkNumber;				// Network number
            public UInt16 		StationNumber;             	// Station number
            public UInt16 		UnitNumber;                	// Unit number
            public string 		IPAddress;                 	// IP address (Ethernet)
            public UInt32 		Timeout;                   	// Communication timeout time
        };

    	// I/O data structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct IO_DATA
        {
            public UInt16     	DeviceType;             	// I/O, Direct I/O, Variable, Timer, Motion, etc. Applied to packet access type
            public UInt16     	DataUnitSize;           	// Unit data size BIT 1, BYTE 8, WORD 16, Etc.
            public UInt16     	RackNo;                 	// Rack number (1, 2, ....)
            public UInt16     	SlotNo;                 	// Slot number (0, 1, ....)
            public UInt16     	SubslotNo;              	// Subslot number (1, 2, ....)
            public UInt16     	StationNo;              	// Station number (1, 2, ....)
            public UInt32     	hData;                  	// Data Handle. Global data handle or register data handle can be specified.
            public UInt32     	DeviceWordOffset;           // Offset address (in the units of words). Motion parameter number
            public UInt16     	DeviceBitOffse;          	// Bit Offset. BYTE 0 1(Hi,Lo), WORD,DWORD   0(reserved)
            public UInt16     	Reserve;                	// Reserved
            public UInt32		DataUnitCount;          	// used for multiple bit quantities or multiple byte arrays, normally 1
        };                                                  

    	// Structure to specify position data
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct POSITION_DATA
        {
	        public Int32 		DataType;                   // Data type (0: immediate, 1: indirect designation)
	        public Int32 		PositionData;               // Position data: hGlobalData is stored in case of indirect designation.
		};
		
    	// Structure of data used for gear ratio setting
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GEAR_RATIO_DATA
        {
	        public UInt16    	Master;                     // Gear ratio (numerator) specified
	        public UInt16    	Slave;                      // Gear ratio (denominator) specified
		}                                                   
		                                                    
    	// Structure of synchronized position data used for Gear
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYNC_DISTANCE
       	{
            public UInt16     	SyncType;                   // Synchronization type
            public UInt16     	DataType;                   // Master axis moving distance data type
            public UInt32    	DistanceData;               // Master axis moving distance
		};                                                  
		
    	// Structure of data used for phase compensation
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct POSITION_OFFSET_DATA
       	{
            public UInt32     	ShiftType;                  // Speed pattern selection (TRIANGLE, TRAPEZOIDE)
            public double   	Offset;                     // Position offset value (UserUnit)
            public double   	Duration;                   // Position compensation time (0 to 65536)
        };                                                  

    	// Structure to specify common motion data
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MOTION_DATA
       	{
            public Int16 		CoordinateSystem;           // Coordinate system specified
            public Int16 		MoveType;                   // Motion type
            public Int16 		VelocityType;               // Speed type
            public Int16 		AccDecType;                 // Acceleration type
            public Int16 		FilterType;                 // Filter type
            public Int16 		DataType;                   // Data type (0: immediate, 1: indirect designation)
            public Int32 		MaxVelocity;                // Maximum feeding speed [reference unit/s]
            public Int32 		Acceleration;               // Acceleration [reference unit/s2], acceleration time constant [ms]
            public Int32 		Deceleration;               // Deceleration [reference unit/s2], deceleration time constant [ms]
            public Int32 		FilterTime;                 // Filter time [ms]
            public Int32 		Velocity;                   // Feeding speed [reference unit/s], Offset speed
            public Int32 		ApproachVelocity;           // Approach speed [reference unit/s]
            public Int32 		CreepVelocity;              // Creep speed [reference unit/s]
        };

    	// Calendar data structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CALENDAR_DATA
       	{
            public UInt16    	Year;                     	// Year
            public UInt16    	Month;                    	// Month
            public UInt16    	DayOfWeek;                	// Day of week
            public UInt16    	Day;                    	// Day
            public UInt16    	Hour;                     	// Hour
            public UInt16    	Minutes;                    // Minute
            public UInt16    	Second;                    	// Second
            public UInt16    	Milliseconds;               // Millisecond
        };                                                  
                                                            
        // Structure to specify alarm information. 
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ALARM_INFO
       	{
		    public UInt32        ErrorCode;					// Error code
		    public UInt32        ErrorLocation;             // Occurring position
		    public UInt32        DetectTask;                // Detector
		    public UInt32        hDevice;                   // Device handle
		    public UInt32        TaskID;                    // Task ID
            [ MarshalAs(UnmanagedType.ByValTStr, SizeConst=8)]
		    public String        TaskName;                  // Task name   8
		    public UInt32        ObjectHandle;              // Object handle
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=8)]
		    public String        ObjectName;                // Object name 8
            public CALENDAR_DATA Calendar;                  // Calendar
		    public UInt32        hAxis;                     // AXIS handle
		    public UInt32        DetailError;               // Detailed error (any error code)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
		    public String        Comment;                   // Comment   32
		};

        // Structure to specify 2-word data.
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DWORD_DATA
       	{
            public Int32    	DataType;                   
            public Int32    	Data;						
        };
        
    	// Register information structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct REGISTER_INFO
       	{
            public UInt32     	hRegisterData;              // handle to RegisterData
            public UInt32     	RegisterDataNumber;         // Number of RegisterData
            public IntPtr    	pRegisterData;              // pointer of Stored RegisterData
            public UInt32     	ReadDataNumber;             // Stored RegisterData Number
        };

        // Extended register information structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct REGISTER_INFO_EX
        {
            public UInt64 hRegisterDataEx;            // handle to ExtendedRegisterData
            public UInt32 RegisterDataNumber;         // Number of RegisterData
            public IntPtr pRegisterData;              // pointer of Stored RegisterData
            public UInt32 ReadDataNumber;             // Stored RegisterData Number
        };

    	// BUSIF information structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BUSIF_INFO
       	{
            public UInt16     	InputDataMaxSize;        	// InputDataMaxSize
            public UInt16     	InputDataAvailableSize;		// InputDataAvailableSize
            public UInt16     	OutputDataMaxSize;       	// OutputDataMaxSize
            public UInt16     	OutputDataAvailableSize;	// OutputDataAvailableSize
        };
    }
}