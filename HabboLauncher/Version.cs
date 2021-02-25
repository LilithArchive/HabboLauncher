using System.Runtime.Serialization;

namespace HabboLauncher
{
	[DataContract]
	public class Version
	{
		[DataMember(Name = "FlashVersionNumber")]
		public int FlashVersionNumber;

		[DataMember(Name = "UnityVersionNumber")]
		public int UnityVersionNumber;

		[DataMember(Name = "LauncherVersionNumber")]
		public int LauncherVersionNumber;

		public Version(int flashV, int unityV, int launcherV)
		{
			FlashVersionNumber = flashV;
			UnityVersionNumber = unityV;
			LauncherVersionNumber = launcherV;
		}
	}
}
