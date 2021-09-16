/*
Copyright 2005-2010 BIOPAC Systems, Inc.

This software is provided 'as-is', without any express or implied warranty.
In no event will BIOPAC Systems, Inc. or BIOPAC Systems, Inc. employees be 
held liable for any damages arising from the use of this software.

Permission is granted to anyone to use this software for any purpose, 
including commercial applications, and to alter it and redistribute it 
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not 
claim that you wrote the original software. If you use this software in a 
product, an acknowledgment (see the following) in the product documentation
is required.

Portions Copyright 2005-2010 BIOPAC Systems, Inc.

2. Altered source versions must be plainly marked as such, and must not be 
misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/

using System.Runtime.InteropServices;

namespace Biopac.API.MPDevice
{
	/// <summary>
	/// C# translation of mpdev.h. A language binding for c:\documents and settings\administrator\my documents\BioPacVideo\BioPacVideo\mpdev.dll is created when compiled
	/// Supports BHAPI 2.1 for Windows
	/// See BHAPI documentation for full documentation
	/// </summary>
	public class MPDevImports
	{
		public enum MPTYPE 
		{
			MP150 = 101,
            MP160 = 103,
			//MP35,
			MP36
		}
		
		public enum MPCOMTYPE
		{
			MPUSB = 10,
			MPUDP
		}
		
		public enum TRIGGEROPT
		{ 
			MPTRIGOFF = 1,
			MPTRIGEXT,
			MPTRIGACH,
			MPTRIGDCH
		}
		
		public enum DIGITALOPT 
		{ 
			SET_LOW_BITS = 1,
			SET_HIGH_BITS,
			READ_LOW_BITS,
			READ_HIGH_BITS
		}
		
		public enum MPRETURNCODE 
		{ 
			MPSUCCESS = 1,
			MPDRVERR,
			MPDLLBUSY,
			MPINVPARA,
			MPNOTCON,
			MPREADY,
			MPWPRETRIG,
			MPWTRIG,
			MPBUSY,
			MPNOACTCH,
			MPCOMERR,
			MPINVTYPE,
			MPNOTINNET,
			MPSMPLDLERR,
			MPMEMALLOCERR,
			MPSOCKERR,
			MPUNDRFLOW,
			MPPRESETERR,
			MPPARSERERR
		}

		public enum MP3XOUTMODE
		{ 
			OUTPUTVOLTAGELEVEL		= 2, 
			OUTPUTCHANNEL3		= 3,
			OUTPUTCHANNEL1		= 5,
			OUTPUTCHANNEL2		= 6,
			OUTPUTCHANNEL4		= 7,
			OUTPUTGROUND			= 0x7f
		}



		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE configChannelByPresetID(uint n, string uid);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE connectMPDev(MPTYPE type, MPCOMTYPE  method, string MP150SN);
		
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE disconnectMPDev();

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE findAllMP150();
		
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE getChScaledInputRange (uint n, out double minRange, out double maxRange);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE getDigitalIO(uint n, out bool state, DIGITALOPT opt);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE getMostRecentSample(double[] data);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE getMPBuffer(uint numSamples, double[] buff);
		
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE getMPDaemonLastError();

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE getStatusMPDev();

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE loadXMLPresetFile(string path);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE readAvailableMP150SN(byte[] buff, uint numchartoread, out uint numcharread);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE receiveMPData(double[] buff, uint numdatapoints, out uint numreceived);
		
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE setAcqChannels(bool[] analogCH);
		
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE setAnalogChScale(uint n, double unscaled1, double scaled1, double unscaled2, double scaled2);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE setAnalogOut(double value, int outchan);
		
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE setAnalogOutputMode (MP3XOUTMODE mode);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE setDigitalAcqChannels(bool[] digitalCH);
		
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE setDigitalIO(uint n, bool state, bool setnow, DIGITALOPT opt);
	
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE setMPTrigger(TRIGGEROPT option, bool posEdge, double level, uint chNum);
		
		[DllImport(@".\mpdev.dll")] 
		public static extern MPRETURNCODE setSampleRate(double rate);

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE startAcquisition();

		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE startMPAcqDaemon();
		
		[DllImport(@".\mpdev.dll")]
		public static extern MPRETURNCODE stopAcquisition(); 
	}
}
