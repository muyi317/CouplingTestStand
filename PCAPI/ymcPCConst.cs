//===========================================================
// C#
//
// Const difinition module for ymcPCAPI.Dll ÅÉymcPCConst.csÅÑ
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
        //======================================================
        //                                                      
        //  MotionAPI Difinition                                
        //                                                      
        //======================================================
        public enum ApiDefs : ushort
        {
			// AxisType
			 REAL_AXIS = 1                             	// Actual servo axis
			,VIRTUAL_AXIS = 2                          	// Virtual servo axis
			,EXTERNAL_ENCODER = 3                      	// External encoder

			// SpecifyType
			,PHYSICALAXIS = 1                          	// Physical axis specified
			,AxisName = 2                              	// Axis name specified
			,LOGICALAXIS = 3                           	// Logical axis specified

			,HMETHOD_DEC1_C = 0                        	// 0: DEC1 + phase-C pulse method
			,HMETHOD_ZERO = 1                          	// 1: ZERO signal method
			,HMETHOD_DEC1_ZERO = 2                     	// 2: DEC1 + ZERO signal method
			,HMETHOD_C = 3                             	// 3: Phase-C pulse method
			,HMETHOD_DEC2_ZERO = 4                     	// 4: DEC 2 + ZERO signal method
			,HMETHOD_DEC1_L_ZERO = 5                   	// 5: DEC1 + LMT + ZERO signal method
			,HMETHOD_DEC2_C = 6                        	// 6: DEC2 + phase-C pulse method
			,HMETHOD_DEC1_L_C = 7                      	// 7: DEC 1 + LMT + phase-C pulse method
			,HMETHOD_C_ONLY = 11                       	// 11: C Pulse Only
			,HMETHOD_POT_C = 12                        	// 12: POT & C Pulse
			,HMETHOD_POT_ONLY = 13                     	// 13: POT Only
			,HMETHOD_HOMELS_C = 14                     	// 14: Home LS C Pulse
			,HMETHOD_HOMELS_ONLY = 15                  	// 15: Home LS Only
			,HMETHOD_NOT_C = 16                        	// 16: NOT & C Pulse
			,HMETHOD_NOT_ONLY = 17                     	// 17: NOT Only
			,HMETHOD_INPUT_C = 18                      	// 18: Input & C Pulse
			,HMETHOD_INPUT_ONLY = 19                   	// 19: Input Only

			// Direction
			,DIRECTION_POSITIVE = 0                    	// Positive Direction
			,DIRECTION_NEGATIVE = 1                    	// Negative Direction

			// Coordinate system specified
			,WORK_SYSTEM = 0                           	// 0: Workpiece coordinate system
			,MACHINE_SYSTEM = 1                        	// 1: Machine coordinate system

			// Motion type
			,MTYPE_RELATIVE = 0                        	// 0: Incremental value specified, common for linear and rotary axes
			,MTYPE_ABSOLUTE = 1                        	// 1: Absolute position specified, for linear axis
			,MTYPE_R_SHORTEST = 2                      	// 2: Absolute position specified, for rotary axis (Rotates to the closer direction.)
			,MTYPE_R_POSITIVE = 3                      	// 3: Absolute position specified, for rotary axis (forward run)
			,MTYPE_R_NEGATIVE = 4                      	// 4: Absolute position specified, for rotary axis (reverse run)

			// Data type
			,DTYPE_IMMEDIATE = 0                       	// Direct designation
			,DTYPE_INDIRECT = 1                        	// Indirect designation
			,DTYPE_MAX_VELOCITY = 0x1                  	// bit0: Designation for Max. Velocity
			,DTYPE_ACCELERATION = 0x2                  	// bit1: Designation for Acceleration
			,DTYPE_DECELERATION = 0x4                  	// bit2: Designation for Deceleration
			,DTYPE_FILTER_TIME = 0x8                   	// bit3: Designation for FilterTime
			,DTYPE_VELOCITY = 0x10                     	// bit4: Designation for Velocity
			,DTYPE_APPROCH = 0x20                      	// bit5: Designation for ApproachVelocity
			,DTYPE_CREEP = 0x40                        	// bit6: Designation for CreepVelocity

			// Feeding speed type
			,VTYPE_UNIT_PAR = 0                        	// speed [Reference unit/s]
			,VTYPE_PERCENT = 1                         	// Rated speed percentage (%) specified

			// Acceleration/deceleration type
			,ATYPE_UNIT_PAR = 0                        	// Acceleration [reference unit/s2]
			,ATYPE_TIME = 1                            	// Time constant [ms]
			,ATYPE_KEEP = 2                            	// Current setting held

			// Filter type
			,FTYPE_S_CURVE = 0                         	// 0: Move average filter (simple S-curve)
			,FTYPE_EXP = 1                             	// 1: Exponential Filter
			,FTYPE_NOTIONG = 2                         	// 2: WITHOUT Filter
			,FTYPE_KEEP = 3                            	// 3: Current setting held

			// WaitForCompletion Constant definition
			,DISTRIBUTION_COMPLETED = 0                	// Distribution completed
			,POSITIONING_COMPLETED = 1                 	// Positioning completed
			,COMMAND_STARTED = 2                       	// Command completed
			,LATCH_COMMAND_STARTED = 0                 	// Latch command started
			,LATCH_COMPLETED = 1                       	// Latch completed

			// SystemOption Constant definition
			,OP_BIT_ALARM_CONTINUE = 0x1               	// bit0: Normal axis operation continued at alarm occurrence

			// Target for ChangeDynamics (1: Changed, 	0: Not changed)
			,SUBJECT_ACC = 0x1                         	// bit0: Acceleration
			,SUBJECT_DEC = 0x2                         	// bit1: Deceleration
			,SUBJECT_POS = 0x8                         	// bit3: Position
			,SUBJECT_VEL = 0x10                        	// bit4: Velocity

			,SERVO_OFF = 0x0                           	// servo OFF
			,SERVO_ON = 0x1                            	// Servo ON

			// Device type
			,DEVICETYPE_IO = 1                         	// I/O
			,DEVICETYPE_DIRECTIO = 2                   	// Direct i / O
			,DEVICETYPE_GLOBALDATA = 3                 	// Global Data
			,DEVICETYPE_REGISTER = 4                   	// Register

			// Unit data size (number of bits)
			,DATAUNITSIZE_BIT = 1                      	// 1 bit
			,DATAUNITSIZE_BYTE = 8                     	// BYTE = 8 bits
			,DATAUNITSIZE_WORD = 16                    	// WORD = 16 bits
			,DATAUNITSIZE_LONG = 32                    	// LONG = 32 bits
			,DATAUNITSIZE_FLOAT = 32                   	// FLOAT= 32 bits
			,DATAUNITSIZE_DOUBLE = 64                  	// DOUBLE= 64 bits

			// Semaphore type
			,SEMAPHORE_NOUSE = 0                       	// Semaphore Not Used
			,SEMAPHORE_USE = 1                         	// Semaphore Used

			// ComDevice type
			,RS232C_MODE = 1                           	// RS232C
			,MODEM_MODE = 2                            	// Modem
			,ETHERNET_MODE = 3                         	// Ethernet
			,PCI_MODE = 4                              	// PCI bus(910)
			,CONTROLLER_MODE = 5                       	// Interior of Controller

			,MAX_CURRENT_ALARM = 32                    	// Maximum number of alarm information that can be obtained at a time
			,MAX_DEVICE_AXIS = 32                      	// Maximum number of devices that can be defined at a time
			,MAX_REGISTER_BLOCK = 499                  	// Maximum number of register blocks that can be operated at a time

			// BitEvent type
			,OFF_TO_ON = 0                             	// Rising edge detection
			,ON_TO_OFF = 1                             	// Falling edge detection
			,LEVEL_ON = 3                              	// Level signal ON (Event only once at level ON detection)
			,LEVEL_OFF = 4                             	// Level signal OFF (Event only once at level OFF detection)

			// Gear
			,MASTER_AXIS_FEEDBACK = 0                  	// Feedback Value
			,MASTER_AXIS_COMMAND = 1                   	// Command Value

			// Gear synchronization type
			,SYNCH_DISTANCE = 0                        	// distance synchronization
			,SYNCH_TIME = 1                            	// Time sychronization

			// Attribute of Gear command
			,GEAR_ENGAGE_COMPLETED = 0                 	// Gear control started (Engage completed)
			,GEAR_COMMAND_STARTED = 1                  	// Command started

			// Attribute of Gear status
			,GEAR_NOT_ENGAGED = 0                      	// During GEAR stop
			,GEAR_WAITING_ENGAGED = 1                  	// Waiting for GEAR motion
			,GEAR_ENGAGED = 2                          	// During GEAR motion
			,GEAR_WAITING_DISENGAGED = 4               	// Waiting for GEAR to stop

			// Attribute of CAM command
			,CAM_ENGAGE_COMPLETED = 0                  	// CAM control started
			,CAM_SHIFT_COMPLETED = 0                   	// CAM phase compensation completed

			,CAM_DISENGAGE_COMPLETED = 0               	// CAM control stopped
			,CAM_COMMAND_STARTED = 1                   	// Command started

			// Attribute of Cam status
			,CAM_NOT_ENGAGED = 0                       	// During CAM stop
			,CAM_WAITING_ENGAGED = 1                   	// Waiting for CAM motion
			,CAM_ENGAGED = 2                           	// During CAM motion
			,CAM_WAITING_DISENGAGED = 4                	// Waiting for CAM to stop

			// Shift type
			,SHIFT_TIME = 0                            	// Shift by time
			,SHIFT_POSITION = 1                        	// Shift by position

			// Attribute of POSITION command
			,POSITION_OFFSET_COMPLETED = 0             	// Position compensation completed
			,POSITION_OFFSET_COMMAND_STARTED = 1       	// Command started

			// Table type
			,CAM_TABLE = 2                             	// CAM table file name
			,INTERPOLATION_TABLE = 3                   	// Interpolation table file name
			,REGISTERHANDLE = 4                        	// Register Handle
			,USER_FUNCTION = 5                         	// User function
			,IK_FUNCTION = 6                           	// IK function

			// Motion parameter type
			,SETTING_PARAMETER = 0                     	// setting parameter
			,MONITOR_PARAMETER = 1                     	// Monitor parameter

			// Cyclic event
			,HIGH_SCAN = 1                             	// High-speed scan
			,MIDDLE_SCAN = 2                           	// Medium-speed scan
			,LOW_SCAN = 3                              	// Low-speed scan
			,SCAN_OCCURED = 1                          	// Minimum

			// Other program (task) action
			,START_PROGRAM = 1                         	// Other program (task) start

			// Move control action
			,START_MOVE = 1                            	// Start
			,HOLD_MOVE = 2                             	// Hold
			,RELEASE_HOLD = 3                          	// Hold released
			,ABORT_MOVE = 4                            	// Abort
			,SKIP_MOVE = 5                             	// Skip

			// Segment type
			,SEGMENT_TYPE_EMPTY = 0                    	// Not used
			,SEGMENT_TYPE_ARC = 1                      	//
			,SEGMENT_TYPE_HELIX = 2                    	//
			,SEGMENT_TYPE_LINEAR_ABS = 3               	//
			,SEGMENT_TYPE_LINEAR_INC = 4               	//
			,SEGMENT_TYPE_CONTOUR = 5                  	//


			,MAX_DEVICE_AXIS_NUM = 16                  	// Maximum number of device axes

			// Circular arc type for circular and helical interpolation
			,LESS_180DEGREE = 0x1                      	//
			,GREATER_180DEGREE = 0x2                   	//

			// coordinate System
			,COORDINATE_SYSTEM_DEFAULT = 0             	//
			,COORDINATE_SYSTEM_MACHINE = 1             	// Machine coordinate system
			
			// mode
			,MODE_INCREMENTAL = 0                      	// INC
			,MODE_ABSOLUTE = 1                         	// ABS

			// Feeding speed type
			,F_TYPE_COMMAND_UNIT = 0                   	// reference unit / Min
			,F_TYPE_PARCENT = 1                        	// % specified

			// Acceleration/deceleration type
			,ACCEL_TYPE_ACCERALATION = 0               	// Acceleration
			,ACCEL_TYPE_TIME_CONSTANT = 1              	// Time Constant
			,ACCEL_TYPE_NO_SPECIFY = 2                 	// Not specified

			// Move event
			,MOVE_EVENT_DISTRIBUTION_START = 0x1       	// Distribution starting event
			,MOVE_EVENT_DISTRIBUTION_COMPLETED = 0x2   	// Distribution completion event
			,MOVE_EVENT_POSITION_COMPLETED = 0x3       	// Positioning completion event
			,MOVE_EVENT_POSITION_COINCIDED = 0x4       	// Specified position passing event
			,MOVE_EVENT_VELOCITY_COINCIDED = 0x5       	// Speed coincidence event
			,MOVE_EVENT_TORQUE_COINCIDED = 0x6         	// Torque coincidence event
			,MOVE_EVENT_ACCELERATION_COMPLETED = 0x7   	// Acceleration completion event
			,MOVE_EVENT_DECELERATION_START = 0x8       	// Deceleration starting event
			,MOVE_EVENT_LATCH_COMPLETED = 0x9          	// Latch completion event
			,MOVE_EVENT_ALARM_OCCURRED = 0xA           	// Alarm occurrence event
			,MOVE_EVENT_ABORT_OCCURRED = 0xB           	// Abort occurrence event

			,MOVE_EVENT_SPECIFIED_DISTRIBUTION_START = 0x10            // Specified record distribution started
			,MOVE_EVENT_SPECIFIED_DISTRIBUTION_COMPLETED = 0x11        // Specified record distribution completed

			// Data comparison event
			,EQUAL = 0x10                              	// EQUAL
			,NOT_EQUAL = 0x11                          	// Not equal
			,GREATER = 0x12                            	// GREATER than
			,LESS = 0x13                               	// smaller than
			,GREATER_EQUAL = 0x14                      	// Equal or greater than
			,LESS_EQUAL = 0x15                         	// Equal or smaller than

			// Evevt within the data range
			,WITHIN = 0x20                             	// Within the range
			,WITHOUT = 0x21                            	// Out of the range

			// Message event
			,MESSAGE_RECIEVED = 1                      	// Message received

			// Timer event
			,TIMEUP = 1                                	// Time up
			
			// Move Action
			,MOVE_ACTION_START_MOVE = 0x1              	// Start Action
			,MOVE_ACTION_HOLD_MOVE = 0x2               	// Hold Action
			,MOVE_ACTION_RELESE_HOLD = 0x3             	// Hold release action
			,MOVE_ACTION_ABORT_MOVE = 0x4              	// Abort Action
			,MOVE_ACTION_SKIP_MOVE = 0x5               	// Skip Action
			,MOVE_ACTION_POSITION_VALUE = 0x6          	// Target position change action
			,MOVE_ACTION_SPEED_VALUE = 0x7             	// Reference speed change action
			,MOVE_ACTION_TORQUE_VALUE = 0x8            	// Reference torque change action
			,MOVE_ACTION_OVERRIDE = 0x9                	// Override change action
			,MOVE_ACTION_ACCELTIME_VALUE = 0xA         	// Acceleration time change action
			,MOVE_ACTION_DECELTIME_VALUE = 0xB         	// Deceleration time change action

			// Bit setting action
			,SET_BIT_OFF = 0x0                         	// Bit OFF
			,SET_BIT_ON = 0x1                          	// Bit ON
			
			// Bit getting action
			,GET_IO = 1                                	// I/O getting

			// Data setting action
			,SET_VALUE = 0x10                          	// Data setting

			// Data getting action
			,GET_VALUE = 0x10                          	// Data getting

			// Message Action
			,SEND_MESSAGE = 1                          	// Message sent
			,RECEIVE_MESSAGE = 2                       	// Message received
			
			// Timer Action
			,START_TIMER = 1                           	// Start
			,STOP_TIMER = 2                            	// Stop
			,CONTINUE_TIMER = 3                        	// Continuous Start
			,RESET_TIMER = 4                           	// Reset

			// User function action
			,START_USERFUNCTION = 1                    	// Start
			,ABORT_USERFUNCTION = 2                    	// Abort

			// Log Read - out
			,SEND_LOG = 1                              	// Sending Log
			,RECV_LOG = 2                              	// Receiving Log

			// Attribute of move
			,MOVE_TYPE_DISTRIBUTION_COMPLETED = 0x0    	// Distribution completed
			,MOVE_TYPE_POSITIONING_COMPLETED = 0x1     	// Positioning completed
			,MOVE_TYPE_POSITIONING_NEIGHBORHOOD = 0x2  	// Second INP completed

			// Related to event log
			,EVENTLOG_BUF_TYPE_LINEAR = 0          		// Linear Buffer
			,EVENTLOG_BUF_TYPE_RING = 1            		// Ring Buffer
			,EVENTLOG_DATA_TYPE_LOGDATA = 1        		// Log Data
			,EVENTLOG_DATA_TYPE_STARTTIME = 2      		// Starting Time
			,EVENTLOG_DATA_TYPE_STOPTIME = 3       		// Stopping Time

			// Axis type
			,AXISTYPE_USE = 1                      		// Actual servo axis used
			,AXISTYPE_VIRTUAL = 2                  		// Virtual servo axis used
			,AXISTYPE_EXTERNAL_ENCODER = 3         		// External encoder used

			// Specified data type when getting axis handle
			,GETAXISHANDLE_PHYSICAL_NO_TYPE = 1    		// Physical axis specified
			,GETAXISHANDLE_NAME_TYPE = 2           		// Name specified

			// UnitType definition
			,UNITTYPE_PULSE = 0                    		// Pulse
			,UNITTYPE_MM = 1                       		// mm
			,UNITTYPE_INCH = 2                     		// inch
			,UNITTYPE_DEGREE = 3                   		// degree

			// Data type
			,DATATYPE_IMMEDIATE = 0                		// Direct designation
			,DATATYPE_INDIRECT = 1                 		// Indirect designation

			// ComDevice type
			,COMDEVICETYPE_RS232C_MODE = 1         		// RS232C
			,COMDEVICETYPE_MODEM_MODE = 2          		// Modem
			,COMDEVICETYPE_ETHERNET_MODE = 3       		// Ethernet
			,COMDEVICETYPE_PCI_MODE = 4            		// PCI bus(910 or 2100)
            ,COMDEVICETYPE_PCIe_MODE = 6            	// PCI bus(3100)
			,COMDEVICETYPE_CONTROLLER_MODE = 5     		// Interior of Controller
        }

        //======================================================
        //                                                      
        //  Motion Parameter( Setting )                         
        //                                                      
        //======================================================
	    
        public enum ApiDefs_SetPrm
        {
			 SER_RUNMOD = 1                        		// OWXX00 : Motion mode setting
			,SER_SVRUNCMD = 2                      		// OWXX01 : Servo drive operation command setting
			,SER_TLIMP = 3                         		// OWXX02 : TORQUE LIMIT PLUS SIDE
			,SER_TLIMN = 4                         		// OWXX03 : TORQUE LIMIT MINUS SIDE
			,SER_NLIMP = 5                         		// OWXX04 : SPEED LIMIT PLUS SIDE
			,SER_NLIMN = 6                         		// OWXX05 : SPEED LIMIT MINUS SIDE
			,SER_ABSOFF = 7                        		// OLXX06 : ABS. ORG. OFFSET
			,SER_COINDAT = 9                       		// OLXX08 : COIN DATA               <YC_7>
			,SER_NAPR = 11                         		// OWXX0A : APROACH SPEED           <YC_7>
			,SER_NCLP = 12                         		// OWXX0B : CLEEP SPEED             <YC_7>
			,SER_NACC = 13                         		// OWXX0C : ACCELARATING TIME       <YC_7>
			,SER_NDCC = 14                         		// OWXX0D : DECREACING TIME         <YC_7>
			,SER_PEXT = 15                         		// OWXX0E : POSITION EXTENT         <YC_7>
			,SER_EOV = 16                          		// OWXX0F : DEVIATION OVER RANGE    <YC_7>
			,SER_KP = 17                           		// OWXX10 : SOFT FEED BACK RATE     <YC_7>
			,SER_KF = 18                           		// OWXX11 : SOFT FEED FOWARD RATE   <YC_7>
			,SER_XREF = 19                         		// OLXX12 : ABS. POSITION REF       <YC_7>
			,SER_NNUM = 21                         		// OWXX14 : AVERAGING TIMES         <YC_7>
			,SER_NREF = 22                         		// OWXX15 : speed reference
			,SER_PHBIAS = 23                       		// OLXX16 : PHASE OFFSET            <YC_7>
			,SER_NCOM = 25                         		// OWXX18 : SPEED BIAS REF          <YC_7>
			,SER_PV = 26                           		// OWXX19 : PROPORTIONAL_GAIN       <YC_7>
			,SER_TI = 27                           		// OWXX1A : TI                      <YC_7>
			,SER_TREF = 28                         		// OWXX1B : TORQUE REFERNCE         <YC_7>
			,SER_NLIM = 29                         		// OWXX1C : SPEED LIMIT             <YC_7>
			,SER_KV = 30                           		// OWXX1D : VELOCITY_GAIN           <YC_7>
			,SER_PULBIAS = 31                      		// OLXX1E : PULSE OFFSET            <YC_7>
			,SER_MCMDCODE = 33                     		// OWXX20 : Motion command code
			,SER_MCMDCTRL = 34                     		// OWXX21 : Motion command control flag
			,SER_RV = 35                           		// OLXX22 : Rapid feeding speed
			,SER_EXMDIST = 37                      		// OLXX24 : External positioning travel distance
			,SER_STOPDIST = 39                     		// OLXX26 : Stopping distance
			,SER_STEP = 41                         		// OLXX28 : STEP moving amount
			,SER_ZRNDIST = 43                      		// OLXX2A : Home position return final travel distance
			,SER_OV = 45                           		// OWXX2C : Override
			,SER_POSCTRL = 46                      		// OWXX2D : Position management control flag
			,SER_OFFSET = 47                       		// OLXX2E : Workpiece coordinate system offset
			,SER_POSMXTRN = 49                     		// OLXX30 : Preset data for number of POSMAX turns
			,SER_INPWIDTH = 51                     		// OWXX32 : Second in-position width
			,SER_PSETWIDTH = 52                    		// OWXX33 : Home position output width
			,SER_PSETTIME = 53                     		// OWXX34 : Positioning completion checking time
			,SER_YENTCN = 54                       		// OWXX35 : YENET servo parameter number
			,SER_CNDAT = 55                        		// OLXX36 : YENET servo parameter set value
			,SER_EPOSL = 57                        		// OLXX38 : Lower digit 2 words of encoder position at power OFF
			,SER_EPOSH = 59                        		// OLXX3A : Upper digit 2 words of encoder position at power OFF
			,SER_APOSL = 61                        		// OLXX3C : Lower digit 2 words of PULSE absolute position at power OFF
        }

        //======================================================
        //                                                      
        //  Motion Parameter( Monitor )                         
        //                                                      
        //======================================================
        public enum ApiDefs_MonPrm
        {
			 SER_RUNSTS = 0                        		// IWxx00 : operation Status
			,SER_ERNO = 1                          		// IWxx01 : Out-of-range occurring parameter number
			,SER_WARNING = 2                       		// ILxx02 : Warning
			,SER_ALARM = 4                         		// ILxx04 : ALARM
			,SER_MCMDRCODE = 8                     		// IWxx08 : Motion command response code
			,SER_MCMDSTS = 9                       		// IWxx09 : Motion command status
			,SER_SUBCMD = 10                       		// IWxx0A : Subcommand response
			,SER_SUBSTS = 11                       		// IWxx0B : Subcommand Status
			,SER_POSSTS = 12                       		// IWxx0C : Position management status
			,SER_TPOS = 14                         		// ILxx0E : Machine coordinate system target position (POS)
			,SER_CPOS = 16                         		// ILxx10 : Machine coordinate system position calculation (CPOS)
			,SER_MPOS = 18                         		// ILxx12 : Machine coordinate system reference position (MPOS)
			,SER_DPOS = 20                         		// ILxx14 : 32-bit coordinate system reference position (DPOS)
			,SER_APOS = 22                         		// ILxx16 : Machine coordinate system feedback position (APOS)
			,SER_LPOS = 24                         		// ILxx18 : Machine coordinate system latch position (LPOS)
			,SER_PERR = 26                         		// ILxx1A : Position deviation
			,SER_PDV = 28                          		// ILxx1C : Target position incremental value monitor
			,SER_PMAXTURN = 30                     		// ILxx1E : Number of POSMAX turns
			,SER_SPDREF = 32                       		// ILxx20 : Speed reference output value monitor
			,SER_XREFMON = 34                      		// ILxx22 : Position reference output monitor
			,SER_YIMON = 36                        		// IWxx24 : Integral value output value monitor
			,SER_LAGMON = 38                       		// ILxx26 : Primary delay monitor
			,SER_PIMON = 40                        		// ILxx28 : Position loop output value monitor
			,SER_SVSTS = 44                        		// IWxx2C : Servo driver status (depending on model)
			,SER_SVALARM = 45                      		// IWxx2D : Servo driver ALARM code
			,SER_SVIOMON = 46                      		// IWxx2E : Servo driver I/O monitor
			,SER_MUSRMONSEL = 47                   		// IWxx2F : Servo driver user monitor information
			,SER_USRMON2 = 48                      		// ILxx30 : Servo driver user monitor 2
			,SER_USRMON3 = 50                      		// ILxx32 : Servo driver user monitor 3
			,SER_USRMON4 = 52                      		// ILxx34 : Servo driver user monitor 4
			,SER_MCNNO = 54                        		// IWxx36 : Servo driver parameter number
			,SER_MSUBCNNO = 55                     		// IWxx37 : Auxiliary servo driver parameter number
			,SER_MCNDAT = 56                       		// ILxx38 : Servo driver parameter read-out data
			,SER_MSUBCNDAT = 58                    		// ILxx3A : Auxiliary servo driver parameter read-out data
			,SER_SANS = 60                         		// IWxx3C : Serial command answer monitor
			,SER_MSADR = 61                        		// IWxx3D : Serial command address monitor
			,SER_MSDAT = 62                        		// IWxx3E : Serial command data monitor
			,SER_MOTERTYPE = 63                    		// IWxx3F : Serial command data monitor
			,SER_FSPD = 64                         		// ILxx40 : Feedback speed                   [Reference unit/s], [10^n reference unit/min], [0.01%]
			,SER_TRQ = 66                          		// ILxx42 : Torque reference monitor
			,SER_ABSREV = 74                       		// ILxx4A : Number of accumulated rotating speed received from absolute encoder
			,SER_IPULSE = 76                       		// ILxx4C : Number of initial incremental pulses [pulses]
			,SER_FIXPRMMON = 86                    		// ILxx56 : Fixed parameter monitor
			,SER_DI = 88                           		// IWxx58 : General-purpose DI monitor
			,SER_AI1 = 89                          		// IWxx59 : General-purpose AI monitor
			,SER_AI2 = 90                          		// IWxx5A : General-purpose AI monitor
			,SER_MEPOSML = 94                      		// ILxx5E : Lower digit 2 words of encoder position at power OFF
			,SER_MEPOSMH = 96                      		// ILxx60 : Upper digit 2 words of encoder position at power OFF
			,SER_MAPOSML = 98                      		// ILxx62 : Lower digit 2 words of PULSE absolute position at power OFF
			,SER_MAPOSMH = 100                     		 // ILxx64 : Upper digit 2 words of PULSE absolute position at power OFF
			,SER_MONSTS = 102                      		 // ILxx66 : Monitor data status
			,SER_MONDATA = 104                     		 // ILxx68 : Read-out data
        }
    }
}