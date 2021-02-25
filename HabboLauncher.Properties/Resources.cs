using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace HabboLauncher.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					resourceMan = new ResourceManager("HabboLauncher.Properties.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static Bitmap bg_box_600 => (Bitmap)ResourceManager.GetObject("bg_box_600", resourceCulture);

		internal static Bitmap bg_box_630b => (Bitmap)ResourceManager.GetObject("bg_box_630b", resourceCulture);

		internal static Bitmap bg_box_launcher_window => (Bitmap)ResourceManager.GetObject("bg_box_launcher_window", resourceCulture);

		internal static Bitmap bg_box_launcher_window_490 => (Bitmap)ResourceManager.GetObject("bg_box_launcher_window_490", resourceCulture);

		internal static Bitmap bg_box_warning => (Bitmap)ResourceManager.GetObject("bg_box_warning", resourceCulture);

		internal static Bitmap launcher_button_classic => (Bitmap)ResourceManager.GetObject("launcher_button_classic", resourceCulture);

		internal static Bitmap launcher_button_modern_disabled => (Bitmap)ResourceManager.GetObject("launcher_button_modern_disabled", resourceCulture);

		internal static Bitmap launcher_close => (Bitmap)ResourceManager.GetObject("launcher_close", resourceCulture);

		internal static Bitmap launcher_codefield => (Bitmap)ResourceManager.GetObject("launcher_codefield", resourceCulture);

		internal static Bitmap launcher_codefield_clear => (Bitmap)ResourceManager.GetObject("launcher_codefield_clear", resourceCulture);

		internal static Bitmap launcher_header_bar => (Bitmap)ResourceManager.GetObject("launcher_header_bar", resourceCulture);

		internal static Bitmap launcher_logincode_bg => (Bitmap)ResourceManager.GetObject("launcher_logincode_bg", resourceCulture);

		internal static Bitmap launcher_logincode_bg_fill => (Bitmap)ResourceManager.GetObject("launcher_logincode_bg_fill", resourceCulture);

		internal static Bitmap launcher_logincode_bg1 => (Bitmap)ResourceManager.GetObject("launcher_logincode_bg1", resourceCulture);

		internal static Bitmap launcher_modern => (Bitmap)ResourceManager.GetObject("launcher_modern", resourceCulture);

		internal static Bitmap launcher_warning_header_bar => (Bitmap)ResourceManager.GetObject("launcher_warning_header_bar", resourceCulture);

		internal static Bitmap launcher_window_bg => (Bitmap)ResourceManager.GetObject("launcher_window_bg", resourceCulture);

		internal static Bitmap launcher_window_bg_big => (Bitmap)ResourceManager.GetObject("launcher_window_bg_big", resourceCulture);

		internal static Bitmap primary_btn => (Bitmap)ResourceManager.GetObject("primary_btn", resourceCulture);

		internal static Bitmap primary_btn_hover => (Bitmap)ResourceManager.GetObject("primary_btn_hover", resourceCulture);

		internal static Bitmap secondary_btn => (Bitmap)ResourceManager.GetObject("secondary_btn", resourceCulture);

		internal static Bitmap secondary_btn_hover => (Bitmap)ResourceManager.GetObject("secondary_btn_hover", resourceCulture);

		internal static byte[] UbuntuCondensed_Regular => (byte[])ResourceManager.GetObject("UbuntuCondensed_Regular", resourceCulture);

		internal static Bitmap update_icon => (Bitmap)ResourceManager.GetObject("update_icon", resourceCulture);

		internal static Bitmap view_us_wide => (Bitmap)ResourceManager.GetObject("view_us_wide", resourceCulture);

		internal static Bitmap warning_bg => (Bitmap)ResourceManager.GetObject("warning_bg", resourceCulture);

		internal Resources()
		{
		}
	}
}
