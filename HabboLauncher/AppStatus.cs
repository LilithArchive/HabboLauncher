namespace HabboLauncher
{
	public class AppStatus
	{
		public bool isFlashReady;

		public bool isUnityReady;

		public bool downloadingFlash;

		public bool downloadingUnity;

		public AppStatus(bool isFlashReady, bool isUnityReady, bool downloadingFlash, bool downloadingUnity)
		{
			this.isFlashReady = isFlashReady;
			this.isUnityReady = isUnityReady;
			this.downloadingFlash = downloadingFlash;
			this.downloadingUnity = downloadingUnity;
		}
	}
}
