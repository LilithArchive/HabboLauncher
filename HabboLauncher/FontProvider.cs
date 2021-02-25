using System.Drawing;

namespace HabboLauncher
{
	public static class FontProvider
	{
		private static FontFamily ubuntuCondensed;

		public static FontFamily UbuntuCondensed => ubuntuCondensed;

		public static void SetFont(FontFamily font)
		{
			ubuntuCondensed = font;
		}
	}
}
