using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MotionAPI;

namespace CouplingTestStand
{
    public class Yaskawa
    {
        UInt32 g_hController = 0;    // Controller handle

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //	btn_Open_Click	
        //		MotionAPI Open
        //////////////////////////////////////////////////////////////////////////////////////////////////
        public Yaskawa()
        {
            UInt32 rc;
            CMotionAPI.COM_DEVICE ComDevice;

            //============================================================================ To Contents of Processing
            // Sets the ymcOpenController parameters.		
            //============================================================================
            ComDevice.ComDeviceType = (UInt16)CMotionAPI.ApiDefs.COMDEVICETYPE_PCI_MODE;
            ComDevice.PortNumber = 1;
            ComDevice.CpuNumber = (UInt16)1;  //cpuno;
            ComDevice.NetworkNumber = 0;
            ComDevice.StationNumber = 0;
            ComDevice.UnitNumber = 0;
            ComDevice.IPAddress = "";
            ComDevice.Timeout = 10000;

            rc = CMotionAPI.ymcOpenController(ref ComDevice, ref g_hController);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcOpenController Board 1 \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }

            //============================================================================ To Contents of Processing
            // Sets the motion API timeout. 		
            //============================================================================
            rc = CMotionAPI.ymcSetAPITimeoutValue(30000);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcSetAPITimeoutValue \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }
        }

        public void writeML(string cRegisterName, string dataML)
        {
            UInt32 hRegister_ML;                   // Register data handle for ML register
            String cRegisterName_ML;               // ML register name storage variable
            Int32[] Reg_LongData = new Int32[3];   // L size register data storage variable
            UInt32 rc;                             // Motion API return value

            hRegister_ML = 0x00000000;

            //============================================================================
            // Gets the register name.
            //============================================================================
            // ML Register
            cRegisterName_ML = cRegisterName;

            //============================================================================ To Contents of Processing
            // Gets the register data handle.	
            // The obtained register number can be used in other threads.
            //============================================================================
            // ML Register
            rc = CMotionAPI.ymcGetRegisterDataHandle(cRegisterName_ML, ref hRegister_ML);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcGetRegisterDataHandle ML \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }

            //============================================================================ To Contents of Processing
            // Writes the set data into the set register.
            //============================================================================
            // ML Register
            Reg_LongData[0] = Int32.Parse(dataML);

            rc = CMotionAPI.ymcSetRegisterData(hRegister_ML, 1, Reg_LongData);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcSetRegisterData ML \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////
        //       btn_Read_Click	
        //               Processing to read in the register
        //////////////////////////////////////////////////////////////////////////////////////////////////
        public void readML(string cRegisterName, ref Int32 readData)
        {
            // Definition of motion API variables
            UInt32 hRegister_ML;                   // Register data handle for ML register
            String cRegisterName_ML;               // ML register name storage variable
            UInt32 ReadDataNumber;                 // Number of obtained registers
            Int32[] Reg_LongData = new Int32[3];   // L size register data storage variable
            UInt32 rc;                             // Motion API return value

            hRegister_ML = 0x00000000;
            ReadDataNumber = 00000000;

            //============================================================================
            // Gets the register name.
            //============================================================================
            // ML Register
            cRegisterName_ML = cRegisterName;

            //============================================================================ To Contents of Processing
            // Gets the register data handle.
            // The obtained register number can be used in other threads.
            //============================================================================
            // ML Register
            rc = CMotionAPI.ymcGetRegisterDataHandle(cRegisterName_ML, ref hRegister_ML);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcGetRegisterDataHandle ML \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }

            //============================================================================ To Contents of Processing
            // Reads in the set register and displays the read-in data. 
            //============================================================================
            rc = CMotionAPI.ymcGetRegisterData(hRegister_ML, 1, Reg_LongData, ref ReadDataNumber);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcGetRegisterData ML \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }
            readData = Reg_LongData[0];
        }

        public void writeMB(string cRegisterName, string dataMB)
        {
            UInt32 hRegister_MB;                   // Register data handle for ML register
            String cRegisterName_MB;               // ML register name storage variable
            UInt16[] Reg_ShortData = new UInt16[3];  // W or B size register data storage variable
            UInt32 rc;                             // Motion API return value

            hRegister_MB = 0x00000000;

            //============================================================================
            // Gets the register name.
            //============================================================================
            // MB Register
            cRegisterName_MB = cRegisterName;

            //============================================================================ To Contents of Processing
            // Gets the register data handle.	
            // The obtained register number can be used in other threads.
            //============================================================================
            // MB Register
            rc = CMotionAPI.ymcGetRegisterDataHandle(cRegisterName_MB, ref hRegister_MB);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcGetRegisterDataHandle MB \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }

            //============================================================================ To Contents of Processing
            // Writes the set data into the set register.
            //============================================================================
            // MB Register
            Reg_ShortData[0] = UInt16.Parse(dataMB);
            rc = CMotionAPI.ymcSetRegisterData(hRegister_MB, 1, Reg_ShortData);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcSetRegisterData MB \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////
        //       btn_Read_Click	
        //               Processing to read in the register
        //////////////////////////////////////////////////////////////////////////////////////////////////
        public void readMB(string cRegisterName,ref UInt16 readData)
        {
            // Definition of motion API variables
            UInt32 hRegister_MB;                   // Register data handle for ML register
            String cRegisterName_MB;               // ML register name storage variable
            UInt32 ReadDataNumber;                 // Number of obtained registers
            UInt16[] Reg_ShortData = new UInt16[3];  // W or B size register data storage variable
            UInt32 rc;                             // Motion API return value

            hRegister_MB = 0x00000000;
            ReadDataNumber = 00000000;

            //============================================================================
            // Gets the register name.
            //============================================================================
            // MB Register
            cRegisterName_MB = cRegisterName;

            //============================================================================ To Contents of Processing
            // Gets the register data handle.
            // The obtained register number can be used in other threads.
            //============================================================================
            // MB Register
            rc = CMotionAPI.ymcGetRegisterDataHandle(cRegisterName_MB, ref hRegister_MB);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcGetRegisterDataHandle  MB \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }

            //============================================================================ To Contents of Processing
            // Reads in the set register and displays the read-in data. 
            //============================================================================
            // MB Register
            rc = CMotionAPI.ymcGetRegisterData(hRegister_MB, 1, Reg_ShortData, ref ReadDataNumber);
            if (rc != CMotionAPI.MP_SUCCESS)
            {
                MessageBox.Show(String.Format("Error ymcGetRegisterData MB \nErrorCode [ 0x{0} ]", rc.ToString("X")));
                return;
            }
            readData = Reg_ShortData[0];
        }
    }
}
