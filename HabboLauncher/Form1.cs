using HabboLauncher.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HabboLauncher
{
	public class Form1 : Form
	{
		private Action onUnityButtonClick;

		private Action onFlashButtonClick;

		private Action<string> onTokenTextChanged;

		private bool isArgsNull;

		private IContainer components;

		private Button ClassicButton;

		private Button ModernButton;

		private Label chooseAppLabel;

		private TextBox tokenInputField;

		private PictureBox pictureBox1;

		private PictureBox CloseButton;

		private Label launcherTitle;

		private PictureBox inputBG;

		private Label inputFieldLabel;

		private PictureBox inputFieldBG;

		private PictureBox ClearTokenButton;

		private PictureBox flashDownloadIcon;

		private PictureBox flashProgressBarBG;

		private PictureBox flashProgressBarFore;

		private PictureBox unityDownloadIcon;

		private PictureBox unityProgressBarBG;

		private PictureBox unityProgressBarFore;

		public Form1()
		{
			InitializeComponent();
			chooseAppLabel.Font = new Font(FontProvider.UbuntuCondensed, 32f, GraphicsUnit.Pixel);
			launcherTitle.Font = new Font(FontProvider.UbuntuCondensed, 36f, GraphicsUnit.Pixel);
			inputFieldLabel.Font = new Font(FontProvider.UbuntuCondensed, 24f, GraphicsUnit.Pixel);
			tokenInputField.Font = new Font(FontProvider.UbuntuCondensed, 22f, GraphicsUnit.Pixel);
			ClassicButton.Font = new Font(FontProvider.UbuntuCondensed, 32f, GraphicsUnit.Pixel);
			ModernButton.Font = new Font(FontProvider.UbuntuCondensed, 32f, GraphicsUnit.Pixel);
		}

		public void AddUnityButtonClickAction(Action action)
		{
			onUnityButtonClick = (Action)Delegate.Combine(onUnityButtonClick, action);
		}

		public void AddFlashButtonClickAction(Action action)
		{
			onFlashButtonClick = (Action)Delegate.Combine(onFlashButtonClick, action);
		}

		public void AddTokenInputListener(Action<string> action)
		{
			onTokenTextChanged = (Action<string>)Delegate.Combine(onTokenTextChanged, action);
		}

		public void SetLayout(bool isArgsNull)
		{
			this.isArgsNull = isArgsNull;
			inputFieldLabel.Visible = isArgsNull;
			tokenInputField.Visible = isArgsNull;
			ClearTokenButton.Visible = isArgsNull;
			inputBG.Visible = isArgsNull;
			inputFieldBG.Visible = isArgsNull;
			base.Size = new Size(600, isArgsNull ? 630 : 460);
			BackgroundImage = (isArgsNull ? HabboLauncher.Properties.Resources.bg_box_630b : HabboLauncher.Properties.Resources.bg_box_launcher_window);
		}

		public void SetAppStatus(AppStatus status)
		{
			int num = isArgsNull ? 630 : 490;
			int num2 = isArgsNull ? 600 : 460;
			Bitmap bitmap = isArgsNull ? HabboLauncher.Properties.Resources.bg_box_630b : HabboLauncher.Properties.Resources.bg_box_launcher_window_490;
			Bitmap bitmap2 = isArgsNull ? HabboLauncher.Properties.Resources.bg_box_600 : HabboLauncher.Properties.Resources.bg_box_launcher_window;
			if (status.downloadingFlash || status.downloadingUnity)
			{
				ModernButton.Anchor = AnchorStyles.Top;
				ClassicButton.Anchor = AnchorStyles.Top;
				chooseAppLabel.Anchor = AnchorStyles.Top;
				flashDownloadIcon.Anchor = AnchorStyles.Top;
				unityDownloadIcon.Anchor = AnchorStyles.Top;
			}
			flashDownloadIcon.Visible = !status.isFlashReady;
			flashProgressBarBG.Visible = !status.isFlashReady;
			flashProgressBarFore.Visible = !status.isFlashReady;
			flashProgressBarBG.Visible = status.downloadingFlash;
			flashProgressBarFore.Visible = status.downloadingFlash;
			ClassicButton.Text = (status.downloadingFlash ? "Updating..." : "Classic");
			flashProgressBarFore.BringToFront();
			unityDownloadIcon.Visible = !status.isUnityReady;
			unityProgressBarBG.Visible = !status.isUnityReady;
			unityProgressBarFore.Visible = !status.isUnityReady;
			unityProgressBarFore.BringToFront();
			unityProgressBarBG.Visible = status.downloadingUnity;
			unityProgressBarFore.Visible = status.downloadingUnity;
			ModernButton.Text = (status.downloadingUnity ? "Updating..." : "Modern");
			base.Size = new Size(600, (status.downloadingFlash || status.downloadingUnity) ? num : num2);
			BackgroundImage = ((status.downloadingFlash || status.downloadingUnity) ? bitmap : bitmap2);
			if (!status.downloadingFlash && !status.downloadingUnity)
			{
				ModernButton.Anchor = AnchorStyles.Bottom;
				ClassicButton.Anchor = AnchorStyles.Bottom;
				chooseAppLabel.Anchor = AnchorStyles.Bottom;
				flashDownloadIcon.Anchor = AnchorStyles.Bottom;
				unityDownloadIcon.Anchor = AnchorStyles.Bottom;
			}
		}

		public void OnDownloadProgress(int val, AppStatus status)
		{
			if (status.downloadingUnity)
			{
				unityProgressBarFore.Size = new Size(val * 2, unityProgressBarFore.Size.Height);
			}
			if (status.downloadingFlash)
			{
				flashProgressBarFore.Size = new Size(val * 2, 40);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void ClassicButtonClick(object sender, EventArgs e)
		{
			onFlashButtonClick?.Invoke();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
		}

		private void ModernButtonClick(object sender, EventArgs e)
		{
			onUnityButtonClick?.Invoke();
		}

		private void ClearTokenButton_Click(object sender, EventArgs e)
		{
			tokenInputField.Text = string.Empty;
		}

		private void tokenInputField_TextChanged_1(object sender, EventArgs e)
		{
			onTokenTextChanged?.Invoke(tokenInputField.Text);
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			chooseAppLabel = new System.Windows.Forms.Label();
			tokenInputField = new System.Windows.Forms.TextBox();
			ModernButton = new System.Windows.Forms.Button();
			ClassicButton = new System.Windows.Forms.Button();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			CloseButton = new System.Windows.Forms.PictureBox();
			launcherTitle = new System.Windows.Forms.Label();
			inputBG = new System.Windows.Forms.PictureBox();
			inputFieldLabel = new System.Windows.Forms.Label();
			inputFieldBG = new System.Windows.Forms.PictureBox();
			ClearTokenButton = new System.Windows.Forms.PictureBox();
			flashDownloadIcon = new System.Windows.Forms.PictureBox();
			flashProgressBarBG = new System.Windows.Forms.PictureBox();
			flashProgressBarFore = new System.Windows.Forms.PictureBox();
			unityDownloadIcon = new System.Windows.Forms.PictureBox();
			unityProgressBarBG = new System.Windows.Forms.PictureBox();
			unityProgressBarFore = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)CloseButton).BeginInit();
			((System.ComponentModel.ISupportInitialize)inputBG).BeginInit();
			((System.ComponentModel.ISupportInitialize)inputFieldBG).BeginInit();
			((System.ComponentModel.ISupportInitialize)ClearTokenButton).BeginInit();
			((System.ComponentModel.ISupportInitialize)flashDownloadIcon).BeginInit();
			((System.ComponentModel.ISupportInitialize)flashProgressBarBG).BeginInit();
			((System.ComponentModel.ISupportInitialize)flashProgressBarFore).BeginInit();
			((System.ComponentModel.ISupportInitialize)unityDownloadIcon).BeginInit();
			((System.ComponentModel.ISupportInitialize)unityProgressBarBG).BeginInit();
			((System.ComponentModel.ISupportInitialize)unityProgressBarFore).BeginInit();
			SuspendLayout();
			chooseAppLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			chooseAppLabel.AutoSize = true;
			chooseAppLabel.BackColor = System.Drawing.Color.White;
			chooseAppLabel.Font = new System.Drawing.Font("Ubuntu Condensed", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
			chooseAppLabel.Location = new System.Drawing.Point(72, 245);
			chooseAppLabel.Name = "chooseAppLabel";
			chooseAppLabel.Size = new System.Drawing.Size(467, 43);
			chooseAppLabel.TabIndex = 6;
			chooseAppLabel.Text = "Please choose the Habbo app version:";
			chooseAppLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			chooseAppLabel.UseCompatibleTextRendering = true;
			tokenInputField.BackColor = System.Drawing.Color.FromArgb(247, 249, 250);
			tokenInputField.BorderStyle = System.Windows.Forms.BorderStyle.None;
			tokenInputField.Font = new System.Drawing.Font("Ubuntu Condensed", 22f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			tokenInputField.ForeColor = System.Drawing.Color.FromArgb(0, 107, 228);
			tokenInputField.Location = new System.Drawing.Point(48, 164);
			tokenInputField.Name = "tokenInputField";
			tokenInputField.Size = new System.Drawing.Size(470, 26);
			tokenInputField.TabIndex = 0;
			tokenInputField.TextChanged += new System.EventHandler(tokenInputField_TextChanged_1);
			ModernButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			ModernButton.BackColor = System.Drawing.Color.Transparent;
			ModernButton.BackgroundImage = HabboLauncher.Properties.Resources.launcher_modern;
			ModernButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			ModernButton.Cursor = System.Windows.Forms.Cursors.Hand;
			ModernButton.FlatAppearance.BorderSize = 0;
			ModernButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			ModernButton.Font = new System.Drawing.Font("Ubuntu Condensed", 27.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
			ModernButton.ForeColor = System.Drawing.Color.Transparent;
			ModernButton.Location = new System.Drawing.Point(320, 314);
			ModernButton.Margin = new System.Windows.Forms.Padding(0);
			ModernButton.Name = "ModernButton";
			ModernButton.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
			ModernButton.Size = new System.Drawing.Size(200, 240);
			ModernButton.TabIndex = 2;
			ModernButton.Text = "Modern";
			ModernButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			ModernButton.UseCompatibleTextRendering = true;
			ModernButton.UseVisualStyleBackColor = false;
			ModernButton.Click += new System.EventHandler(ModernButtonClick);
			ClassicButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			ClassicButton.BackColor = System.Drawing.Color.Transparent;
			ClassicButton.BackgroundImage = HabboLauncher.Properties.Resources.launcher_button_classic;
			ClassicButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			ClassicButton.Cursor = System.Windows.Forms.Cursors.Hand;
			ClassicButton.FlatAppearance.BorderSize = 0;
			ClassicButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			ClassicButton.Font = new System.Drawing.Font("Ubuntu Condensed", 32.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, 162);
			ClassicButton.ForeColor = System.Drawing.Color.White;
			ClassicButton.Location = new System.Drawing.Point(80, 314);
			ClassicButton.Margin = new System.Windows.Forms.Padding(0);
			ClassicButton.Name = "ClassicButton";
			ClassicButton.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
			ClassicButton.Size = new System.Drawing.Size(200, 240);
			ClassicButton.TabIndex = 1;
			ClassicButton.Text = "Classic";
			ClassicButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			ClassicButton.UseCompatibleTextRendering = true;
			ClassicButton.UseVisualStyleBackColor = false;
			ClassicButton.Click += new System.EventHandler(ClassicButtonClick);
			pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			pictureBox1.BackColor = System.Drawing.Color.Transparent;
			pictureBox1.Image = HabboLauncher.Properties.Resources.launcher_header_bar;
			pictureBox1.Location = new System.Drawing.Point(10, 10);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(580, 60);
			pictureBox1.TabIndex = 12;
			pictureBox1.TabStop = false;
			CloseButton.BackColor = System.Drawing.Color.FromArgb(14, 57, 85);
			CloseButton.BackgroundImage = HabboLauncher.Properties.Resources.launcher_close;
			CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
			CloseButton.Location = new System.Drawing.Point(534, 14);
			CloseButton.Name = "CloseButton";
			CloseButton.Size = new System.Drawing.Size(52, 52);
			CloseButton.TabIndex = 13;
			CloseButton.TabStop = false;
			CloseButton.Click += new System.EventHandler(pictureBox2_Click);
			launcherTitle.AutoSize = true;
			launcherTitle.BackColor = System.Drawing.Color.FromArgb(14, 57, 85);
			launcherTitle.Font = new System.Drawing.Font("Ubuntu Condensed", 36f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, 162);
			launcherTitle.ForeColor = System.Drawing.Color.White;
			launcherTitle.Location = new System.Drawing.Point(185, 20);
			launcherTitle.Name = "launcherTitle";
			launcherTitle.Size = new System.Drawing.Size(229, 48);
			launcherTitle.TabIndex = 14;
			launcherTitle.Text = "Habbo launcher";
			launcherTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			launcherTitle.UseCompatibleTextRendering = true;
			inputBG.BackColor = System.Drawing.Color.White;
			inputBG.Image = HabboLauncher.Properties.Resources.launcher_logincode_bg;
			inputBG.Location = new System.Drawing.Point(10, 82);
			inputBG.Name = "inputBG";
			inputBG.Size = new System.Drawing.Size(580, 140);
			inputBG.TabIndex = 15;
			inputBG.TabStop = false;
			inputFieldLabel.AutoSize = true;
			inputFieldLabel.BackColor = System.Drawing.Color.FromArgb(207, 215, 222);
			inputFieldLabel.Font = new System.Drawing.Font("Ubuntu Condensed", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, 162);
			inputFieldLabel.Location = new System.Drawing.Point(75, 100);
			inputFieldLabel.Name = "inputFieldLabel";
			inputFieldLabel.Size = new System.Drawing.Size(457, 33);
			inputFieldLabel.TabIndex = 16;
			inputFieldLabel.Text = "Please paste the login token from your clipboard:";
			inputFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			inputFieldLabel.UseCompatibleTextRendering = true;
			inputFieldBG.BackColor = System.Drawing.Color.FromArgb(207, 215, 221);
			inputFieldBG.Image = HabboLauncher.Properties.Resources.launcher_codefield;
			inputFieldBG.Location = new System.Drawing.Point(30, 145);
			inputFieldBG.Name = "inputFieldBG";
			inputFieldBG.Size = new System.Drawing.Size(540, 60);
			inputFieldBG.TabIndex = 17;
			inputFieldBG.TabStop = false;
			ClearTokenButton.BackColor = System.Drawing.Color.FromArgb(247, 249, 250);
			ClearTokenButton.Cursor = System.Windows.Forms.Cursors.Hand;
			ClearTokenButton.Image = HabboLauncher.Properties.Resources.launcher_codefield_clear;
			ClearTokenButton.Location = new System.Drawing.Point(525, 162);
			ClearTokenButton.Name = "ClearTokenButton";
			ClearTokenButton.Size = new System.Drawing.Size(30, 30);
			ClearTokenButton.TabIndex = 18;
			ClearTokenButton.TabStop = false;
			ClearTokenButton.Click += new System.EventHandler(ClearTokenButton_Click);
			flashDownloadIcon.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			flashDownloadIcon.BackColor = System.Drawing.Color.FromArgb(228, 234, 239);
			flashDownloadIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			flashDownloadIcon.Image = HabboLauncher.Properties.Resources.update_icon;
			flashDownloadIcon.Location = new System.Drawing.Point(230, 326);
			flashDownloadIcon.Name = "flashDownloadIcon";
			flashDownloadIcon.Size = new System.Drawing.Size(40, 40);
			flashDownloadIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			flashDownloadIcon.TabIndex = 21;
			flashDownloadIcon.TabStop = false;
			flashDownloadIcon.Visible = false;
			flashProgressBarBG.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			flashProgressBarBG.BackColor = System.Drawing.Color.FromArgb(40, 31, 44);
			flashProgressBarBG.Location = new System.Drawing.Point(80, 536);
			flashProgressBarBG.Name = "flashProgressBarBG";
			flashProgressBarBG.Size = new System.Drawing.Size(200, 40);
			flashProgressBarBG.TabIndex = 22;
			flashProgressBarBG.TabStop = false;
			flashProgressBarBG.Visible = false;
			flashProgressBarFore.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			flashProgressBarFore.BackColor = System.Drawing.Color.FromArgb(0, 173, 4);
			flashProgressBarFore.Location = new System.Drawing.Point(80, 536);
			flashProgressBarFore.Name = "flashProgressBarFore";
			flashProgressBarFore.Size = new System.Drawing.Size(0, 40);
			flashProgressBarFore.TabIndex = 23;
			flashProgressBarFore.TabStop = false;
			unityDownloadIcon.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			unityDownloadIcon.BackColor = System.Drawing.Color.FromArgb(228, 234, 239);
			unityDownloadIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			unityDownloadIcon.Image = HabboLauncher.Properties.Resources.update_icon;
			unityDownloadIcon.Location = new System.Drawing.Point(468, 326);
			unityDownloadIcon.Name = "unityDownloadIcon";
			unityDownloadIcon.Size = new System.Drawing.Size(40, 40);
			unityDownloadIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			unityDownloadIcon.TabIndex = 24;
			unityDownloadIcon.TabStop = false;
			unityDownloadIcon.Visible = false;
			unityProgressBarBG.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			unityProgressBarBG.BackColor = System.Drawing.Color.FromArgb(40, 31, 44);
			unityProgressBarBG.Location = new System.Drawing.Point(320, 536);
			unityProgressBarBG.Name = "unityProgressBarBG";
			unityProgressBarBG.Size = new System.Drawing.Size(200, 40);
			unityProgressBarBG.TabIndex = 25;
			unityProgressBarBG.TabStop = false;
			unityProgressBarBG.Visible = false;
			unityProgressBarFore.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			unityProgressBarFore.BackColor = System.Drawing.Color.FromArgb(0, 173, 4);
			unityProgressBarFore.Location = new System.Drawing.Point(320, 536);
			unityProgressBarFore.Name = "unityProgressBarFore";
			unityProgressBarFore.Size = new System.Drawing.Size(0, 40);
			unityProgressBarFore.TabIndex = 26;
			unityProgressBarFore.TabStop = false;
			unityProgressBarFore.Visible = false;
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			BackColor = System.Drawing.Color.Fuchsia;
			BackgroundImage = HabboLauncher.Properties.Resources.bg_box_launcher_window;
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			base.ClientSize = new System.Drawing.Size(600, 600);
			base.Controls.Add(unityProgressBarFore);
			base.Controls.Add(unityProgressBarBG);
			base.Controls.Add(unityDownloadIcon);
			base.Controls.Add(flashProgressBarFore);
			base.Controls.Add(flashProgressBarBG);
			base.Controls.Add(flashDownloadIcon);
			base.Controls.Add(ClearTokenButton);
			base.Controls.Add(inputFieldLabel);
			base.Controls.Add(launcherTitle);
			base.Controls.Add(CloseButton);
			base.Controls.Add(tokenInputField);
			base.Controls.Add(chooseAppLabel);
			base.Controls.Add(ModernButton);
			base.Controls.Add(ClassicButton);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(inputFieldBG);
			base.Controls.Add(inputBG);
			DoubleBuffered = true;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form1";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Habbo";
			base.TransparencyKey = System.Drawing.Color.Fuchsia;
			base.Load += new System.EventHandler(Form1_Load);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)CloseButton).EndInit();
			((System.ComponentModel.ISupportInitialize)inputBG).EndInit();
			((System.ComponentModel.ISupportInitialize)inputFieldBG).EndInit();
			((System.ComponentModel.ISupportInitialize)ClearTokenButton).EndInit();
			((System.ComponentModel.ISupportInitialize)flashDownloadIcon).EndInit();
			((System.ComponentModel.ISupportInitialize)flashProgressBarBG).EndInit();
			((System.ComponentModel.ISupportInitialize)flashProgressBarFore).EndInit();
			((System.ComponentModel.ISupportInitialize)unityDownloadIcon).EndInit();
			((System.ComponentModel.ISupportInitialize)unityProgressBarBG).EndInit();
			((System.ComponentModel.ISupportInitialize)unityProgressBarFore).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
