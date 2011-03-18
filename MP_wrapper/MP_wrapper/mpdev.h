/*! \file mpdev.h
	\author BIOPAC Systems, Inc.
	\version 2.1
    \brief  The BIOPAC Hardware API allows third-party software programs to communicate with BIOPAC MP devices

	The Hardware API is compatible with the MP150 communicating via Ethernet using UDP and the MP36R communicating via USB.

	The Hardware API allows developers to create software programs that communicate directly to the MP device.

	Using the Hardware API functions, software developers can:
  
	- Acquire data from the analog and digital channels
	- Acquire at different sample rates
	- Set triggers
	- Get the MP device status
	- Use the Analog output channels 

	New Features:
	- Added MP36R support
	- connection to MP150 device on Vista-based computers with multi-NICs configuration
	- redirecting of input channel signal to output channel for MP36R devices
*/

#ifndef _MPDEV_H_
#define _MPDEV_H_

#include <windows.h>

/** Enumerated Values for MP Devices
 * 
 *	Represents supported MP devices.
 *
 */
typedef enum MP_TYPE
{
	MP150 = 101,	/**< represents the MP150 device */
//	MP35,			/**< represents the MP35 device */
	MP36			/**< represents the MP36R device */
} MPTYPE;

/** Enumerated Values for the MP Device Communication Types
 *
 *  Represents supported MP communication types.
 *
 */
typedef enum MP_COM_TYPE
{
	MPUSB = 10, /**< represents communication via USB */
	MPUDP		/**< represents communication via Ethernet using UDP */
} MPCOMTYPE;

/** Enumerated Values For MP Device Triggering Options
 *
 *  Represents supported MP triggering options. 
 *
 */
typedef enum TRIGGER_OPT
{
	MPTRIGOFF = 1, /**< represents triggering off */
	MPTRIGEXT,	/**< represents external triggering */
	MPTRIGACH,	/**< represents analog channel triggering */
	MPTRIGDCH	/**< represents digital channel triggering (MP36R only)*/
} TRIGGEROPT;

/** Enumerated Values For the MP Device Digital I/O Options
 *
 *  Represents supported MP digital line reading or writing options. 
 *
 */
typedef enum DIGITAL_OPT
{
	SET_LOW_BITS = 1, /**< set only digital I/O lines 0 through 7 */
	SET_HIGH_BITS, /**< set only digital I/O lines 8 through 15 */
	READ_LOW_BITS, /**< read only digital I/O lines 0 through 7 */
	READ_HIGH_BITS /**< read only digital I/O lines 8 through 15 */
} DIGITALOPT;

/** Enumerated Return Code Values
 *
 *  Return codes that are generated by Hardware API functions. 
 *
 */
typedef enum MP_RETURN_CODE
{
	MPSUCCESS = 1, /**< = successful execution */
	MPDRVERR,	/**< = error communicating with the device drivers*/
	MPDLLBUSY,	/**< = a process is attached to the DLL, only one process may use the DLL */
	MPINVPARA,	/**< = invalid parameter(s)*/
	MPNOTCON,	/**< = MP device is not connected */
	MPREADY,	/**< = MP device is ready */
	MPWPRETRIG, /**< = MP device is waiting for pre-trigger (pre-triggering is not implemented) */
	MPWTRIG,	/**< = MP device is waiting for trigger */
	MPBUSY,		/**< = MP device is busy */
	MPNOACTCH,	/**< = there are no active channels, in order to acquire data at least one analog channel must be active */
	MPCOMERR,	/**< = generic communication error */
	MPINVTYPE,	/**< = the function is incompatible with the selected MP device or communication method */
	MPNOTINNET,	/**< = the specified MP150 is not in the network  */
	MPSMPLDLERR,/**< = MP device overwrote samples that had not been transferred from the device (buffer overflow) */
	MPMEMALLOCERR, /**< = error allocating memory */
	MPSOCKERR,	/**< = internal socket error */
	MPUNDRFLOW, /**< = MP device returned a data pointer that is less than the last data pointer **/
	MPPRESETERR, /**< = error with the specified preset file **/
	MPPARSERERR	/**< = preset file parsing error, the XML file must be valid according to the schema **/
} MPRETURNCODE;

/** Enumerated Values For MP36R Device Ouput modes
 *
 *  Represents supported MP36R output modes. 
 *
 */
typedef enum MP3XOUTPUT_OPT
{
	OUTPUTVOLTAGELEVEL		= 2, /**< = Setup ouptput mode to CONSTANT VOLTAGE LEVEL (MP36R does not support this mode) */
	OUTPUTCHANNEL3			= 3, /**< = Redirect input channel #3 to output  */
//	OUTPUTSTIMULATOR		4,	// Not supported with BHAPI 2.1
	OUTPUTCHANNEL1			= 5, /**< = Redirect input channel #1 to output  */
	OUTPUTCHANNEL2			= 6, /**< = Redirect input channel #2 to output  */
	OUTPUTCHANNEL4			= 7, /**< = Redirect input channel #4 to output  */
	OUTPUTGROUND			= 0x7f /**< = Ground all output signal - setup output sugnal to zero level */
} MP3XOUTMODE;


/** Attribute that enables the export of API functions via a DLL  **/
#define MPDLL_EXPORT __declspec(dllexport) 

#ifdef __cplusplus
extern "C"
{
#endif

/** Configure a Channel by Preset ID
 *
 *	This function will configure an analog input channel based on the preset element with the specified unique ID (uid) from the XML preset file loaded into memory using the loadXMLPresetFile() function.
 *
 *	Please see \ref chpresetxml_page.
 *
 *	\note
 *		- For the MP36R, the Scaling and the Hardware Configuration elements are applied 
 *		- For the MP150, only the Scaling element is applied
 *
 *	\param n the analog input channel where the preset configuration will be applied and 0 &lt;= <em>n</em> &lt; number of analog input channels of the MP device
 *	\param uid the unique identifier of the preset to be applied to the specified channels
 *  
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall configChannelByPresetID(const DWORD n, const char * uid);

/** Connect to the Specified MP Device
 *
 *	This function MUST be called first when using the Hardware API.
 *
 *	\note
 *		- This function will connnect to the first responding MP150, when "AUTO" or "auto" is specified for the <em>MP150SN</em> parameter
 *		- The first responding MP150 is usually the closest MP150 but it is not guaranteed
 *		- The MP150 serial number can be obtained using findAllMP150() and readAvailableMP150SN()
 *
 *	\param type the type of device to connect with, must use <em>MPTYPE</em> enumerated values
 *	\param method the type of communication method, must use <em>MPCOMTYPE</em> enumerated values
 *	\param MP150SN can be set to any string if not using MP150 via Ethernet using UDP, otherwise it's the serial number of the MP150. "AUTO" or "auto" can be used to automatically connect to first MP150 to respond
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall connectMPDev(const MPTYPE type, const MPCOMTYPE method, const char * MP150SN);

/** Disconnect from the MP Device
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall disconnectMPDev();

/** Find All MP150s
 *
 *	Finds all the MP150s routable in the current network configuration and caches their serial numbers which can be read by calling readAvailableMP150SN().
 *
 *	\note
 *		- connectMPDev() must be called first
 *		- the call to connectMPDev() must FAIL because of an invalid serial number
 *		- only works with device type <em>MP150</em> and communication type <em>MPUDP</em>
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall findAllMP150();

/** Get a Channel's Scaled Input Range
 *
 *	This function calculates the scaled input range of a particular channel.
 *  The Scaled Input Range is the scaled range of double values where the MP device can successfully read a scaled sample value without clipping.
 *	It is depedent upon scaling, gain, and offset values of the specified analog input channel.
 *	If the scaled sample value is out of the range, it will be clipped to the range's maximum or minimum value.  
 *
 *	\param n the analog input channel where the scaled channel input range will be calculated and 0 &lt;= <em>n</em> &lt; number of analog input channels of the MP device
 *	\param minRange an out variable where the minimum value of the scaled input range is stored
 *	\param maxRange an out variable where the maximum value of the scaled input range is stored
 *  
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall getChScaledInputRange(const DWORD n, double * minRange, double * maxRange);

/** Get the State of the Specified Digital I/O Line
 *
 *	<em>true</em> = "high" = 1 = 5.0 volts (TTL high)
 *	\n
 *	<em>false</em> = "low" = 0 = 0.0 volts (TTL low)
 *
 *	\note	
 *		- The MP36R has dedicated input and output lines while the MP150 has auto-switching digital input and output lines
 *		- For the MP150, this function automatically switches the direction of all the Digital I/O lines to input
 *		- The MP36R has 8 digital input lines (digital lines 0 through 7)
 *		- The MP150 has 16 digital input lines (digital lines 0 through 15)
 *
 *	\param n the Digital Input channel to get  where 0 &lt;= <em>n</em> &lt; number of digital input channels of the MP device
 *	\param state an out variable where the state of Digital Input line <em>n</em> is stored
 *	\param opt
 *	    if <em>opt</em> == <em>READ_LOW_BITS</em> and 0 &lt;= <em>n</em> &lt; 8 only digital lines 0-7 are read\n
 *	    if <em>opt</em> == <em>READ_HIGH_BITS</em> and 8 &lt;= <em>n</em> &lt; 16 only digital lines 8-15 are read\n
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall getDigitalIO(const DWORD n, BOOL * state, const DIGITALOPT opt);

/** Get the Most Recent Sample
 *
 *	This function blocks until the MP device acquires the most recent sample. Once startAcquisition() is called, the MP device continuously acquires data at the specified sample rate. Use this function to get the latest sample collected at the time it was called. This function is useful in monitoring purposes at low sample rates where previous data does not change rapidly.
 *
 *	\note 
 *		- If acquiring only analog input channels:
 *			- <em>data</em> array must be able to hold double values equivalent to number of analog input channels of the MP device
 *			- After this function is succesfully invoked, data[<em>i</em>] = the most recent sample of the Analog Input Channel <em>i</em>+1, where 0 &lt;= <em>i</em> &lt; the number of analog input channels of the MP device
 *			- If a channel is not enabled for acquisition the corresponding array element will not be set to a default value
 *		- If acquiring analog and digital channels:
 *			- <em>data</em> array must be able to hold double values equivalent to number of analog input channels plus the number of digital input channels of the MP device
 *			- After this function is succesfully invoked, data[<em>i</em>] = the most recent sample of the Analog Input Channel <em>i</em>+1 and data[<em>a+d</em>] = the most recent sample of the Digital Input Channel <em>a+d</em> where:
 *				 - <em>a</em> is the number of analog input channels of the MP device
 *				 - 0 &lt;= <em>i</em> &lt; <em>a</em> 
 *				 - 0 &lt;= <em>d</em> &lt; the number of digital input channels of the MP device
 *			- If a channel is not enabled for acquisition the corresponding array element will not be set to a default value
 *	
 *	\param data an out double array variable where the most recent sample for input channels is stored by the function
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall getMostRecentSample(double data[]);

/** Get the Acquisition Buffer
 *  
 *	This function blocks until the MP device acquires the number of samples requested. The values will be interleaved, which means that the value for each active channel will be adjacent to each other and in increasing order by channel number.
 *	If acquiring with analog and digital channels, data from active digitals channels will follow the analog channels in the same interleaved manner. 
 *
 *  This function should only be used for acquisitions lasting less than a minute.  <b>It must only be called once per acquisition.</b>
 *	
 *	\par
 *  \htmlinclude buffer.txt
 *
 *	\note
 *		- <em>buff</em> array must be able to hold at least <em>numSamples*(number of active analog channels + number of active digital channels )</em> double values
 *
 *	\param numSamples the number of samples to acquire
 *	\param buff an out double array variable where values from the acquisition are stored
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall getMPBuffer(const DWORD numSamples, double buff[]);

/** Get the MP Daemon Last Error
 *
 *	This function returns the exit value or the status of the MP Acquisition Daemon that was created by a call to startMPAcqDaemon().
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall getMPDaemonLastError();

/** Get the Status of the MP Device
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall getStatusMPDev();

/** Load an XML Preset File
 *
 *	This function loads an XML preset file into memory.  The XML document must be properly formatted. 
 *
 *	Please see \ref chpresetxml_page.
 *
 *	\param filename the complete filename of the XML file using absolute or relative path
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall loadXMLPresetFile(const char * filename);

/** Read the Serial Numbers of Available MP150s
 *
 *	Reads the serial numbers of MP150s routable in the current network configuration.
 *
 *	\note
 *		- requires a successful call to findAllMP150()
 *		- MP150 serial number normally consist of 12 characters
 *		- each line represents the serial number of a MP150
 *		- each line will normally consist of 13 characters (the serial number and the newline character, '\\n')
 *		- returns <em>MPSUCCESS</em> if it reads one or more characters
 *		- <em>buff</em> array must be able to hold at least <em>numchartoread</em> character values
 *
 *	\param buff an out array variable where the number of requested character to read will be stored 
 *	\param numchartoread the number of characters requested to be read
 *	\param numcharread an out variable where the number of characters actually read is stored
 *
 *	\return <em>MPRETURNCODE</em>
 *
 *	\include readsn.txt
 */
MPDLL_EXPORT MPRETURNCODE _stdcall readAvailableMP150SN(char buff[], const DWORD numchartoread, DWORD * numcharread);

/** Receive the MP Data
 *
 *	Receives the MP data from the MP Acquisition Daemon. This is recommended for continuous acquisitions. The values will be interleaved, which means that the value for each active channel will be adjacent to each other and in increasing order by channel number. If acquiring with analog and digital channels, data from active digitals channels will follow the analog channels in the same interleaved manner.
 *
 *	\htmlinclude stream.txt
 *
 *	\note
 *		- requires a successful call to startMPAcqDaemon()
 *		- the data will be a stream of double values
 *		- returns <em>MPSUCCESS</em> if it receives one or more double values
 *		- <em>buff</em> array must be able to hold at least <em>numdatapoints</em> double values
 *
 *  \param buff an out array variable where the number of requested double values to receive will be stored
 *	\param numdatapoints the number of values requested to be received from the acquisition daemon
 *	\param numreceived an out variable where the number of double values actually received from the acquisition daemon is stored
 *
 *	\return <em>MPRETURNCODE</em>
 *
 *	\include daemon.txt
 */
MPDLL_EXPORT MPRETURNCODE _stdcall receiveMPData(double buff[], const DWORD numdatapoints, DWORD * numreceived);

/** Set the Analog Channels to Acquire
 *
 *	In order for data acquisition to start, at least one analog channel must be enabled for acquisition.
 *
 *	analogCH[<em>i</em>] = <em>true</em> implies that the MP device is set to acquire from Analog Channel <em>i</em>+1.
 *	\n
 *	analogCH[<em>i</em>] = <em>false</em> implies that the MP device is set to acquire from Analog Channel <em>i</em>+1.
 *	\n
 *	Where 0 &lt;= <em>i</em> &lt; number of analog input channels of the MP device.
 *
 *	\note
 *		- The length of <em>analgoCH</em> array must equal to the number of analog input channels of the MP device
 *
 *	\param analogCH an array of booleans, which enables/disables acquisition on the corresponding analog input channel
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall setAcqChannels(const BOOL analogCH[]);

/** Set the Analog Channel Scaling
 *
 *	This function configures the linear scaling of the specified analog input channel. It determines the linear scaling and offset values using the two pairs of unscaled and scaled values.
 *	The linear scaling and offset are then applied to unscaled values read from the MP device internally. These scaled values are what is returned by the getMostRecentSample(), getMPBuffer(), and receiveMPData() functions.
 *
 *	\note
 *		- The usnscaled values are in the units of voltage, the native units of the MP device
 *		- The default scaling factor for analog input channels is one with an offset of zero
 *
 *	\param n the analog input channel where the scaling configuration will be applied and 0 &lt;= <em>n</em> &lt; number of analog input channels of the MP device
 *	\param unscaled1 the first unscaled voltage value
 *	\param scaled1 the first scaled value
 *	\param unscaled2 the second unscaled voltage value
 *	\param scaled2 the second scaled value
 *	
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall setAnalogChScale(const DWORD n, const double unscaled1, const double scaled1, const double unscaled2, const double scaled2);

/** Set the Voltage of the Specified Analog Output Channel
 *
 *	\note
 *		- For the MP150, <em>maxvolt</em> = 10.0 and <em>minvolt</em> = -10.0
 *		- For the MP36R, constant voltage level output is not supported
 *
 *	\param value voltage to set the specific analog output channel, <em>minvolt</em> &lt;= <em>value</em> &lt;=  <em>maxvolt</em>
 *	\param outchan specify the output channel number, <em>outchan</em> = 1 or <em>outchan</em> = 0
 *	
 *	\return <em>MPRETURNCODE</em>
 *
 *	\warning
 *		For safety, always set analog output channels back to zero volts before the client program exits or physically power cycle the MP device.  If you do not set the voltage level back to zero the analog output channels may continue to output a non-zero voltage.
 */
MPDLL_EXPORT MPRETURNCODE _stdcall setAnalogOut(const double value, const int outchan);

/** Setup output mode for MP device
 *
 *	\note
 *		- Allow switching to different output modes of MP devices
 *		- One of MP36R Input channels may be redirected to Output Channel 0
 *		- to reset output signal to zero user should issue this command with parameter of OUTPUTGROUND
 *
 *	\param mode specify the output mode of MP36R device\n
 *	    if <em>mode</em> == <em>OUTPUTCHANNEL1</em> Redirect signal of Input channel #1 to Analog Output Channel 0\n
 *	    if <em>mode</em> == <em>OUTPUTCHANNEL2</em> Redirect signal of Input channel #2 to Analog Output Channel 0\n
 *	    if <em>mode</em> == <em>OUTPUTCHANNEL3</em> Redirect signal of Input channel #3 to Analog Output Channel 0\n
 *	    if <em>mode</em> == <em>OUTPUTCHANNEL4</em> Redirect signal of Input channel #4 to Analog Output Channel 0\n
 *	    if <em>mode</em> == <em>OUTPUTGROUND</em> Ground output signal\n
 *	
 *	\return <em>MPRETURNCODE</em>
 *
 *	\warning
 *		For safety, always set analog output channels back to zero volts before the client program exits or physically power cycle the MP device.  If you do not set the voltage level back to zero the analog output channels may continue to output a non-zero voltage.
 */
MPDLL_EXPORT MPRETURNCODE _stdcall setAnalogOutputMode (const MP3XOUTMODE mode);


/** Set the Digital Channels to Acquire
 *
 *	digitalCH[<em>i</em>] = <em>true</em> implies that the MP device is set to acquire from Digital Channel <em>i</em>.
 *	\n
 *	digitalCH[<em>i</em>] = <em>false</em> implies that the MP device is set to acquire from Digital Channel <em>i</em>.
 *	\n
 *	Where 0 &lt;= <em>i</em> &lt; number of digital input channels of the MP device.
 *
 *	\note
 *		- The length of <em>digitalCH</em> array must equal to the number of digital input channels of the MP device
 *
 *	\param digitalCH an array of booleans, enables/disables acquisition on the corresponding digital input channel 
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall setDigitalAcqChannels(const BOOL digitalCH[]);

/** Set the State of the Specified Digital I/O Line
 *
 *	<em>true</em> = "high" = 1 = 5.0 volts (TTL high)
 *	\n
 *  <em>false</em> = "low" = 0 = 0.0 volts (TTL low)
 *
 *	\note	
 *		- The MP36R has dedicated input and output lines while the MP150 has auto-switching digital input and output lines
 *		- For the MP150, this function automatically switches the direction of all the Digital I/O lines to output
 *		- The MP36R has 8 digital output lines (digital lines 0 through 7)
 *		- The MP150 has 16 digital output lines (digital lines 0 through 15)
 *
 *	\param n the Digital I/O line to set  where 0 &lt;= <em>n</em> &lt; number of digital I/O channels of the MP device
 *	\param state if <em>true</em> the Digital I/O line will be set high, if <em>false</em> it will be set low
 *	\param setnow if <em>true</em> the function will send the setting to the MP device, if <em>false</em> it will not send settings until the function is called with <em>setnow</em> set to <em>true</em>.  This allows for multiple Digital I/O lines to be configured before they are sent to the MP device.
 *	\param opt this parameter is ignored unless <em>setnow</em> = <em>true</em>\n
 *	    if <em>opt</em> == <em>SET_LOW_BITS</em> only digital lines 0-7 are set.\n
 *	    if <em>opt</em> == <em>SET_HIGH_BITS</em> only digital lines 8-15 are set.\n
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall setDigitalIO(const DWORD n, const BOOL state, const BOOL setnow, const DIGITALOPT opt);

/** Set the Triggering Configuration
 *
 *	\note
 *		- By default the triggering is set to off
 *		- For the MP36R, the external triggering (<em>MPTRIGEXT</em>) can only be triggered via negative edge (<em>posEdge</em>=<em>false</em>)
 *		- For the MP150, digital input channel triggering (<em>MPTRIGDCH</em>) is not possible
 *		- Trigger <em>level</em> is in the scaled units
 *		- If the scaling, gain, or offset of the analog input channel trigger (<em>MPTRIGACH</em>) is modified after configuring the trigger settings, the trigger <em>level</em> is recalculated as follows:
 *			- the previous level is used, regardless of the previous scaling units
 *			- if the previous level is out range of the new Scaled Input Range (see getChScaledInputRange()), the previous level will be clipped to the new range's maximum or minimum value
 *
 *	\param option different ways to set up triggering must use <em>TRIGGEROPT</em> enumerated values.\n
		if <em>option</em> == <em>MPTRIGOFF</em> the rest of the parameters are ignored by the function. \n
	    if <em>option</em> == <em>MPTRIGEXT</em> the TTL external trigger will be used, parameter <em>posEdge</em> is required and the rest of the parameters are ignored by the function.\n
	    if <em>option</em> == <em>MPTRIGACH</em> one of the analog channel inputs will be used and all the parameters are required by the function.\n
		if <em>option</em> == <em>MPTRIGDCH</em> one of the digital channel inputs will be used, all the parameters are required by the function except <em>level</em>. The <em>level</em> parameter is ignored.  It is automatically set to TTL.\n
 *	\param posEdge if <em>true</em> the triggering is set for positive edge trigger, if <em>false</em> it is set for negative edge trigger. This parameter is ignored by the function if <em>option</em> is set to <em>MPTRIGOFF</em>.
 *	\param level the scaled units level of the trigger, minRange &lt;= <em>level</em> &lt;= maxRange (see getChScaledInputRange()). This parameter is ignored by the function if <em>option</em> is set to <em>MPTRIGOFF</em>, <em>MPTRIGEXT</em> or <em>MPTRIGDCH</em>.
 *	\param chNum the  channel where the MP device waits for the trigger. This parameter is ignored by the function if <em>option</em> is set to <em>MPTRIGOFF</em> or <em>MPTRIGEXT</em>.\n
		if <em>option</em> == <em>MPTRIGACH</em>  0 &lt;= <em>chNum</em> &lt; the number of analog channels of the MP device.\n
		if <em>option</em> == <em>MPTRIGDCH</em>  0 &lt;= <em>chNum</em> &lt; the number of digital channels of the MP device.\n
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall setMPTrigger(const TRIGGEROPT option, const BOOL posEdge, const double level, const DWORD chNum);

/** Set the MP Device Sample Rate
 *
 *	\htmlinclude samplerates.txt
 *
 *	\param rate MP device sampling rate in msec/sample
 *
 *	\return <em>MPRETURNCODE</em>
 *
 *	\warning
 *		 - Only use the sample rates listed above
 *		 - The number of active acquisition channels should not exceed the Maximum Recommended Number of Active Acquisition Channels, the acquisition may fail or not even start otherwise
 *		 - Unexpected results may occur if the incoming data are not transferred and processed as quickly and efficiently as possible
 */
MPDLL_EXPORT MPRETURNCODE _stdcall setSampleRate(const double rate);

/** Start the Acquisition
 *
 *	\note
 *		- At least one analog input channel must be enabled for acquisition
 *		- If a the MP device is configured to trigger, this function will not return until the trigger is received by the MP device
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall startAcquisition();

/** Start the Acquisition Daemon
 *
 *  Starts a thread (daemon) that downloads the data from the MP device and caches it once acquisition starts.  The cache can be retrieved by calling receiveMPData().
 *
 *	\note
 *		- call this function first before calling startAcquisition()
 *		- when using this function, you must <b>NOT</b> use any data transfer methods such as, getMPBuffer() and getMostRecentSample() functions, during the same acquisition
 *		- the daemon will not exit until the stopAcquisition() or disconnectMPDev() function is called or an error occurs within the thread
 *		- the thread is spawned from the calling process
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall startMPAcqDaemon();

/** Stop the Acquisition
 *
 *	\return <em>MPRETURNCODE</em>
 */
MPDLL_EXPORT MPRETURNCODE _stdcall stopAcquisition();

#ifdef __cplusplus
}
#endif

#endif
