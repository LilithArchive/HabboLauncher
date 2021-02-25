using HabboLauncher.Properties;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace HabboLauncher
{
	internal static class Program
	{
		private static string[] args;

		private static int flashVersion = 0;

		private static int unityVersion = 0;

		private static int launcherVersion = 1007;

		private static readonly string userAgent = "HabboLauncher/1.0";

		private static readonly string serverKey = "-server";

		private static readonly string ticketKey = "-ticket";

		private static readonly string tokenRegex = "^([\\w]+)\\.([\\w-]+\\.V4)$";

		private static string token;

		private static readonly string appPath = Application.StartupPath;

		private static readonly string downloadedFolderName = "HabboFlash";

		private static readonly string downloadedFileName = "HabboFlash";

		private static readonly string downloadedFileFormat = "zip";

		private static readonly string versionFileName = "Version";

		private static readonly string versionFileFormat = "txt";

		private static string flashFolderPath = $"{Application.StartupPath}\\{downloadedFolderName}";

		private static string unityFolderPath = string.Format("{0}\\{1}", Application.StartupPath, "HabboClient");

		private static string downloadedFilePath = $"{Application.StartupPath}\\{downloadedFileName}.{downloadedFileFormat}";

		private static string downloadedFilePathUnity = string.Format("{0}\\{1}.{2}", Application.StartupPath, "unity", downloadedFileFormat);

		private static string versionFilePath = $"{Application.StartupPath}\\{versionFileName}.{versionFileFormat}";

		private static readonly string hashURL = "https://www.habbo.com/gamedata/clienturls";

		private static string configJson;

		private static Form1 form1;

		private static Config config = new Config();

		private static bool isFlashReady = false;

		private static bool isUnityReady = false;

		private static bool downloadingFlash = false;

		private static bool downloadingUnity = false;

		private static WarningBox loadingDialog;

		private static AppStatus status = new AppStatus(isFlashReady: false, isUnityReady: false, downloadingFlash: false, downloadingUnity: false);

		[DllImport("gdi32.dll")]
		private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

		[STAThread]
		private static void Main()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			AddFontForPublicUsage();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			if (!File.Exists(versionFilePath))
			{
				Version graph = new Version(flashVersion, unityVersion, launcherVersion);
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Version));
				using (MemoryStream memoryStream = new MemoryStream())
				{
					dataContractJsonSerializer.WriteObject((Stream)memoryStream, (object)graph);
					memoryStream.Position = 0L;
					string value = new StreamReader(memoryStream).ReadToEnd();
					using (StreamWriter streamWriter = File.CreateText(versionFilePath))
					{
						streamWriter.Write(value);
					}
				}
			}
			else
			{
				string s = File.ReadAllText(versionFilePath);
				DataContractJsonSerializer dataContractJsonSerializer2 = new DataContractJsonSerializer(typeof(Version));
				using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
				{
					Version obj = dataContractJsonSerializer2.ReadObject((Stream)stream) as Version;
					flashVersion = obj.FlashVersionNumber;
					unityVersion = obj.UnityVersionNumber;
					launcherVersion = obj.LauncherVersionNumber;
				}
			}
			GetConfig();
			loadingDialog = new WarningBox();
			loadingDialog.Show("Loading");
			Application.Run(loadingDialog);
		}

		private static void AddFontForPublicUsage()
		{
			PrivateFontCollection privateFontCollection = new PrivateFontCollection();
			try
			{
				Stream stream = new MemoryStream(HabboLauncher.Properties.Resources.UbuntuCondensed_Regular);
				IntPtr intPtr = Marshal.AllocCoTaskMem((int)stream.Length);
				byte[] ubuntuCondensed_Regular = HabboLauncher.Properties.Resources.UbuntuCondensed_Regular;
				stream.Read(ubuntuCondensed_Regular, 0, (int)stream.Length);
				Marshal.Copy(ubuntuCondensed_Regular, 0, intPtr, (int)stream.Length);
				uint pcFonts = 0u;
				AddFontMemResourceEx(intPtr, (uint)ubuntuCondensed_Regular.Length, IntPtr.Zero, ref pcFonts);
				privateFontCollection.AddMemoryFont(intPtr, (int)stream.Length);
				stream.Close();
				Marshal.FreeCoTaskMem(intPtr);
			}
			catch (Exception)
			{
			}
			if (privateFontCollection != null && privateFontCollection.Families.Length != 0)
			{
				FontProvider.SetFont(privateFontCollection.Families[0]);
			}
		}

		private static void InitForm(Action actionBeforeRun = null)
		{
			form1 = new Form1();
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				form1.AddUnityButtonClickAction(GetArgumentsAndOpenUnityClient);
				form1.AddFlashButtonClickAction(GetArgumentsAndOpenFlashClient);
				form1.SetLayout(isArgsNull: false);
			}
			else
			{
				form1.AddTokenInputListener(OnTokenInputChanged);
				form1.AddUnityButtonClickAction(GetInputAndOpenUnityClient);
				form1.AddFlashButtonClickAction(GetInputAndOpenFlashClient);
				form1.SetLayout(isArgsNull: true);
			}
			actionBeforeRun?.Invoke();
			Application.Run(form1);
		}

		private static void GetConfig()
		{
			CustomWebClient customWebClient = new CustomWebClient();
			customWebClient.Headers["User-Agent"] = userAgent;
			customWebClient.Timeout = 15000;
			customWebClient.DownloadStringAsync(new Uri(hashURL));
			customWebClient.DownloadStringCompleted += OnJsonDl;
		}

		private static void OnJsonDl(object sender, DownloadStringCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				return;
			}
			if (e.Error == null)
			{
				configJson = e.Result;
				Console.WriteLine(e.Result);
				if (!string.IsNullOrEmpty(configJson) && !string.IsNullOrWhiteSpace(configJson))
				{
					OnConfigLoaded(configJson);
					return;
				}
				WarningBox warningBox = new WarningBox();
				warningBox.Show("Network error - check your connection and try again!");
				Application.Run(warningBox);
				isFlashReady = true;
				isUnityReady = true;
				InitForm(delegate
				{
					SetStatus(isFlashReady, isUnityReady, downloadingFlash, downloadingUnity);
				});
			}
			else
			{
				WarningBox warningBox2 = new WarningBox();
				warningBox2.Show("Network error - check your connection and try again!");
				Application.Run(warningBox2);
				isFlashReady = true;
				isUnityReady = true;
				InitForm(delegate
				{
					SetStatus(isFlashReady, isUnityReady, downloadingFlash, downloadingUnity);
				});
			}
		}

		private static void OnConfigLoaded(Config config)
		{
			if (config.WindowsLauncherVersion <= launcherVersion)
			{
				if (config.WindowsFlashVersion <= flashVersion)
				{
					isFlashReady = true;
				}
				else
				{
					isFlashReady = false;
				}
				if (config.WindowsUnityVersion <= unityVersion)
				{
					isUnityReady = true;
				}
				else
				{
					isUnityReady = false;
				}
				InitForm(delegate
				{
					SetStatus(isFlashReady, isUnityReady, downloadingFlash, downloadingUnity);
				});
			}
			else
			{
				WarningBox warningBox = new WarningBox();
				warningBox.Show("Your launcher is out of date!");
				warningBox.AddOnCloseEvent(delegate
				{
					Application.Exit();
				});
				Process.Start("https://www.habbo.com/");
				Application.Run(warningBox);
			}
		}

		private static void GetArgumentsAndOpenUnityClient()
		{
			if (isUnityReady)
			{
				if (Environment.GetCommandLineArgs().Length > 1)
				{
					if (Directory.Exists(Application.StartupPath + "\\HabboClient"))
					{
						args = Environment.GetCommandLineArgs();
						Uri uri = new Uri(args[1]);
						string text = HttpUtility.ParseQueryString(uri.Query).Get("server");
						string text2 = HttpUtility.ParseQueryString(uri.Query).Get("token");
						ProcessStartInfo processStartInfo = new ProcessStartInfo(Application.StartupPath + "\\HabboClient\\StandaloneWindows\\habbo2020-global-prod.exe");
						processStartInfo.Arguments = $"{serverKey} {text} {ticketKey} {text2}";
						Process.Start(processStartInfo);
						Application.Exit();
					}
					else
					{
						new WarningBox().Show("Modern client is not available.");
					}
				}
				else
				{
					new WarningBox().Show("No args!");
				}
			}
			else
			{
				DownloadUnityClient(config.UnityURL);
			}
		}

		private static void GetInputAndOpenUnityClient()
		{
			if (isUnityReady)
			{
				if (!string.IsNullOrEmpty(token) && !string.IsNullOrWhiteSpace(token))
				{
					if (IsTokenValid(token))
					{
						if (Directory.Exists(Application.StartupPath + "\\HabboClient"))
						{
							MatchCollection matchCollection = Regex.Matches(token, tokenRegex, RegexOptions.None);
							ProcessStartInfo processStartInfo = new ProcessStartInfo(Application.StartupPath + "\\HabboClient\\StandaloneWindows\\habbo2020-global-prod.exe");
							processStartInfo.Arguments = $"{serverKey} {matchCollection[0].Groups[1].Value} {ticketKey} {matchCollection[0].Groups[2].Value}";
							Process.Start(processStartInfo);
							Application.Exit();
						}
						else
						{
							new WarningBox().Show("Modern client is not available.");
						}
					}
					else
					{
						new WarningBox().Show("Invalid token.");
					}
				}
				else
				{
					new WarningBox().Show("Invalid token.");
				}
			}
			else
			{
				DownloadUnityClient(config.UnityURL);
			}
		}

		private static void GetArgumentsAndOpenFlashClient()
		{
			if (isFlashReady)
			{
				CheckTLS(OnTLSReady);
			}
			else
			{
				DownloadFlashClient(config.AIRURL);
			}
			void OnTLSReady()
			{
				if (Environment.GetCommandLineArgs().Length > 1)
				{
					args = Environment.GetCommandLineArgs();
					Uri uri = new Uri(args[1]);
					string text = HttpUtility.ParseQueryString(uri.Query).Get("server");
					string text2 = HttpUtility.ParseQueryString(uri.Query).Get("token");
					string text3 = text.Split(new string[1]
					{
						"hh"
					}, StringSplitOptions.None)[1];
					Process.Start(new ProcessStartInfo(Application.StartupPath + "\\HabboFlash\\Habbo.exe")
					{
						Arguments = $"{serverKey} {text3} {ticketKey} {text2}"
					});
					Application.Exit();
				}
				else
				{
					new WarningBox().Show("Invalid token.");
				}
			}
		}

		private static void GetInputAndOpenFlashClient()
		{
			if (isFlashReady)
			{
				CheckTLS(OnTLSReady);
			}
			else
			{
				DownloadFlashClient(config.AIRURL);
			}
			void OnTLSReady()
			{
				if (!string.IsNullOrEmpty(token) && !string.IsNullOrWhiteSpace(token))
				{
					if (IsTokenValid(token))
					{
						MatchCollection matchCollection = Regex.Matches(token, tokenRegex, RegexOptions.None);
						Process.Start(new ProcessStartInfo(Application.StartupPath + "\\HabboFlash\\Habbo.exe")
						{
							Arguments = $"{serverKey} {matchCollection[0].Groups[1].Value} {ticketKey} {matchCollection[0].Groups[2].Value}"
						});
						Application.Exit();
					}
					else
					{
						new WarningBox().Show("Invalid token.");
					}
				}
				else
				{
					new WarningBox().Show("Invalid token.");
				}
			}
		}

		private static void DownloadFlashClient(string url)
		{
			downloadingFlash = true;
			SetStatus(isFlashReady, isUnityReady, downloadingFlash, downloadingUnity);
			using (WebClient webClient = new WebClient())
			{
				webClient.Headers["User-Agent"] = userAgent;
				webClient.DownloadProgressChanged += OnDownloadProgress;
				webClient.DownloadFileCompleted += OnDownloadComplete;
				webClient.DownloadFileAsync(new Uri(url), downloadedFilePath);
			}
		}

		private static void OnDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
		{
			form1.OnDownloadProgress((int)((double)e.BytesReceived / (double)e.TotalBytesToReceive * 100.0), status);
		}

		private static void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				if (e.Error != null)
				{
					new WarningBox().Show("Download failed");
				}
				else
				{
					if (Directory.Exists(flashFolderPath))
					{
						Directory.Delete(flashFolderPath, recursive: true);
					}
					Directory.CreateDirectory(flashFolderPath);
					try
					{
						ExtractZip(downloadedFilePath, flashFolderPath, ClientType.Flash);
					}
					catch (IOException)
					{
						new WarningBox().Show("Update failed");
						isFlashReady = false;
					}
				}
			}
			downloadingFlash = false;
			SetStatus(isFlashReady, isUnityReady, downloadingFlash, downloadingUnity);
		}

		private static void DownloadUnityClient(string url)
		{
			downloadingUnity = true;
			SetStatus(isFlashReady, isUnityReady, downloadingFlash, downloadingUnity);
			using (WebClient webClient = new WebClient())
			{
				webClient.Headers["User-Agent"] = userAgent;
				webClient.DownloadProgressChanged += OnDownloadProgressUnity;
				webClient.DownloadFileCompleted += OnDownloadCompleteUnity;
				webClient.DownloadFileAsync(new Uri(url), downloadedFilePathUnity);
			}
		}

		private static void OnDownloadProgressUnity(object sender, DownloadProgressChangedEventArgs e)
		{
			form1.OnDownloadProgress(e.ProgressPercentage, status);
		}

		private static void OnDownloadCompleteUnity(object sender, AsyncCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				if (e.Error != null)
				{
					new WarningBox().Show("Download failed");
				}
				else
				{
					if (Directory.Exists(unityFolderPath))
					{
						Directory.Delete(unityFolderPath, recursive: true);
					}
					try
					{
						ExtractZip(downloadedFilePathUnity, unityFolderPath, ClientType.Unity);
					}
					catch (IOException)
					{
						new WarningBox().Show("Update failed");
						isUnityReady = false;
					}
				}
			}
			downloadingUnity = false;
			SetStatus(isFlashReady, isUnityReady, downloadingFlash, downloadingUnity);
		}

		private static void OnConfigLoaded(string data)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Config));
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
			{
				config = (dataContractJsonSerializer.ReadObject((Stream)stream) as Config);
				OnConfigLoaded(config);
			}
		}

		private static void SetStatus(bool isFlashReady, bool isUnityReady, bool downloadingFlash, bool downloadingUnity)
		{
			status.isFlashReady = isFlashReady;
			status.isUnityReady = isUnityReady;
			status.downloadingFlash = downloadingFlash;
			status.downloadingUnity = downloadingUnity;
			form1.SetAppStatus(status);
		}

		private static void ExtractZip(string zipFileName, string targetDir, ClientType clientType)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			FastZip val = new FastZip();
			string text = null;
			val.ExtractZip(zipFileName, targetDir, text);
			OnZipExtracted(clientType);
		}

		private static void OnZipExtracted(ClientType clientType)
		{
			File.Delete(downloadedFilePath);
			switch (clientType)
			{
			case ClientType.Flash:
				flashVersion = config.WindowsFlashVersion;
				isFlashReady = true;
				break;
			case ClientType.Unity:
				unityVersion = config.WindowsUnityVersion;
				isUnityReady = true;
				break;
			}
			Version graph = new Version(flashVersion, unityVersion, launcherVersion);
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Version));
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject((Stream)memoryStream, (object)graph);
				memoryStream.Position = 0L;
				string value = new StreamReader(memoryStream).ReadToEnd();
				using (StreamWriter streamWriter = File.CreateText(versionFilePath))
				{
					streamWriter.Write(value);
				}
			}
		}

		private static void OnTarExtracted()
		{
			File.Delete(downloadedFilePath);
			flashVersion = config.WindowsFlashVersion;
			unityVersion = config.WindowsUnityVersion;
			Version graph = new Version(flashVersion, unityVersion, launcherVersion);
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Version));
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject((Stream)memoryStream, (object)graph);
				memoryStream.Position = 0L;
				string value = new StreamReader(memoryStream).ReadToEnd();
				using (StreamWriter streamWriter = File.CreateText(versionFilePath))
				{
					streamWriter.Write(value);
				}
			}
		}

		private static void OnTokenInputChanged(string newToken)
		{
			token = newToken;
		}

		private static bool IsTokenValid(string input)
		{
			int num = 0;
			foreach (Match item in Regex.Matches(input, tokenRegex, RegexOptions.None))
			{
				if (item.Success)
				{
					num++;
				}
			}
			return num > 0;
		}

		private static void CheckTLS(Action onAccept = null)
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\", writable: true);
			RegistryKey subkey = registryKey.CreateSubKey("Internet Settings");
			int val = (int)subkey.GetValue("SecureProtocols");
			WarningBox warningBox = new WarningBox();
			if (val < 2048)
			{
				warningBox.Show("TLS 1.2 must be enabled in order for the Habbo application to run.", 28, AddKey, delegate
				{
					warningBox.Close();
				});
				return;
			}
			subkey.Close();
			onAccept?.Invoke();
			void AddKey()
			{
				val += 2048;
				subkey.SetValue("SecureProtocols", val, RegistryValueKind.DWord);
				subkey.Close();
				warningBox.Close();
				onAccept?.Invoke();
			}
		}
	}
}
