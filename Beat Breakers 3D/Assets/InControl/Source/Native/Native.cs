namespace InControl
{
	using System;
	using System.Runtime.InteropServices;
	using UnityEngine;
	using DeviceHandle = System.UInt32;

	// @cond nodoc
	internal static class Native
	{
		const string LibraryName = "InControlNative";

		public static ThreadSafeQueue<DeviceHandle> AttachedDeviceQueue = new ThreadSafeQueue<DeviceHandle>();
		public static ThreadSafeQueue<DeviceHandle> DetachedDeviceQueue = new ThreadSafeQueue<DeviceHandle>();


		[UnmanagedFunctionPointer( CallingConvention.Cdecl )]
		public delegate void DeviceAttachedCallback( DeviceHandle handle );


		[UnmanagedFunctionPointer( CallingConvention.Cdecl )]
		public delegate void DeviceDetachedCallback( DeviceHandle handle );


		[UnmanagedFunctionPointer( CallingConvention.Cdecl )]
		public delegate void DebugPrintCallback( string text );


#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

		[DllImport( LibraryName, EntryPoint = "InControl_SetDebugPrintFunc" )]
		static extern void SetDebugPrintFunc( DebugPrintCallback debugPrintFunc );


		[DllImport( LibraryName, EntryPoint = "InControl_Init" )]
		static extern void Init( DeviceAttachedCallback deviceAttached, DeviceDetachedCallback deviceDetached, NativeInputOptions options );
		public static void Init( NativeInputOptions options )
		{
			SetDebugPrintFunc( OnDebugPrint );
			Init( OnDeviceAttached, OnDeviceDetached, options );
		}


		[DllImport( LibraryName, EntryPoint = "InControl_Stop" )]
		public static extern void Stop();


		[DllImport( LibraryName, EntryPoint = "InControl_GetVersionInfo" )]
		public static extern void GetVersionInfo( out NativeVersionInfo versionInfo );


		[DllImport( LibraryName, EntryPoint = "InControl_GetDeviceInfo" )]
		public static extern bool GetDeviceInfo( DeviceHandle handle, out NativeDeviceInfo deviceInfo );


		[DllImport( LibraryName, EntryPoint = "InControl_GetDeviceState" )]
		public static extern bool GetDeviceState( DeviceHandle handle, out IntPtr deviceState );


		[DllImport( LibraryName, EntryPoint = "InControl_SetHapticState" )]
		public static extern void SetHapticState( UInt32 handle, Byte motor0, Byte motor1 );


		[DllImport( LibraryName, EntryPoint = "InControl_SetLightColor" )]
		public static extern void SetLightColor( UInt32 handle, Byte red, Byte green, Byte blue );


		[DllImport( LibraryName, EntryPoint = "InControl_SetLightFlash" )]
		public static extern void SetLightFlash( UInt32 handle, Byte flashOnDuration, Byte flashOffDuration );

#else

		public static void Init( NativeInputOptions options ) { }
		public static void Stop() { }
		public static void GetVersionInfo( out NativeVersionInfo versionInfo ) { versionInfo = new NativeVersionInfo(); }
		public static bool GetDeviceInfo( DeviceHandle handle, out NativeDeviceInfo deviceInfo ) { deviceInfo = new NativeDeviceInfo(); return false; }
		public static bool GetDeviceState( DeviceHandle handle, out IntPtr deviceState ) { deviceState = IntPtr.Zero; return false; }
		public static void SetHapticState( UInt32 handle, Byte motor0, Byte motor1 ) { }
		public static void SetLightColor( UInt32 handle, Byte red, Byte green, Byte blue ) { }
		public static void SetLightFlash( UInt32 handle, Byte flashOnDuration, Byte flashOffDuration ) { }

#endif


		static void OnDeviceAttached( DeviceHandle handle )
		{
			AttachedDeviceQueue.Enqueue( handle );
		}


		static void OnDeviceDetached( DeviceHandle handle )
		{
			DetachedDeviceQueue.Enqueue( handle );
		}


		static void OnDebugPrint( string text )
		{
			Debug.Log( "[InControl.Native] " + text );
		}
	}
	//@endcond
}

