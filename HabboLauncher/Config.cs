using System;
using System.Runtime.Serialization;

namespace HabboLauncher
{
	[DataContract]
	public class Config
	{
		[DataMember(Name = "flash-windows")]
		public string AIRURL;

		[DataMember(Name = "unity-windows")]
		public string UnityURL;

		[DataMember(Name = "unity-osx")]
		public string UnityURLOSX;

		[DataMember(Name = "flash-osx")]
		public string AIRURLOSX;

		[DataMember(Name = "windows")]
		public string LauncherURL;

		[DataMember(Name = "osx")]
		public string LauncherURLOSX;

		[DataMember(Name = "flash-windows-version")]
		public string wfv;

		[DataMember(Name = "unity-windows-version")]
		public string uwv;

		[DataMember(Name = "flash-osx-version")]
		public string ofv;

		[DataMember(Name = "unity-osx-version")]
		public string ouv;

		[DataMember(Name = "windows-version")]
		public string wlv;

		[DataMember(Name = "osx-version")]
		public string olv;

		public int WindowsFlashVersion => Convert.ToInt32(wfv);

		public int WindowsUnityVersion => Convert.ToInt32(uwv);

		public int OSXFlashVersion => Convert.ToInt32(ofv);

		public int OSXUnityVersion => Convert.ToInt32(ouv);

		public int WindowsLauncherVersion => Convert.ToInt32(wlv);

		public int OSXLauncherVersion => Convert.ToInt32("5");
	}
}
