using HabboLauncher.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HabboLauncher
{
	public class WarningBox : Form
	{
		private Action onClose;

		private Action okButtonClick;

		private Action cancelButtonClick;

		private IContainer components;

		private Label messageText;

		private PictureBox pictureBox1;

		private PictureBox CloseButton;

		private Label warningLabel;

		private Button okButton;

		private Button cancelButton;

		public WarningBox()
		{
			InitializeComponent();
			messageText.Font = new Font(FontProvider.UbuntuCondensed, 32f, GraphicsUnit.Pixel);
			okButton.Font = new Font(FontProvider.UbuntuCondensed, 32f, GraphicsUnit.Pixel);
			cancelButton.Font = new Font(FontProvider.UbuntuCondensed, 32f, GraphicsUnit.Pixel);
			warningLabel.Font = new Font(FontProvider.UbuntuCondensed, 42f, GraphicsUnit.Pixel);
		}

		public void AddOnCloseEvent(Action onClose)
		{
			this.onClose = (Action)Delegate.Combine(this.onClose, onClose);
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Close();
			onClose?.Invoke();
		}

		public void Show(string msg)
		{
			Show();
			messageText.Text = msg;
			messageText.Size = new Size(560, 258);
		}

		public void Show(string msg, int messageFontSize = 32, Action okButtonEvent = null, Action cancelButtonEvent = null)
		{
			Show();
			messageText.Text = msg;
			Font font = new Font("Ubuntu Condensed", messageFontSize, FontStyle.Regular, GraphicsUnit.Pixel);
			messageText.Font = font;
			if (okButtonEvent != null)
			{
				okButtonClick = (Action)Delegate.Combine(okButtonClick, okButtonEvent);
				okButton.Visible = true;
				messageText.Size = new Size(560, 170);
			}
			if (cancelButtonEvent != null)
			{
				cancelButtonClick = (Action)Delegate.Combine(cancelButtonClick, cancelButtonEvent);
				cancelButton.Visible = true;
				messageText.Size = new Size(560, 170);
			}
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			okButtonClick?.Invoke();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			cancelButtonClick?.Invoke();
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
			messageText = new System.Windows.Forms.Label();
			warningLabel = new System.Windows.Forms.Label();
			CloseButton = new System.Windows.Forms.PictureBox();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			okButton = new System.Windows.Forms.Button();
			cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)CloseButton).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			messageText.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			messageText.BackColor = System.Drawing.Color.White;
			messageText.Cursor = System.Windows.Forms.Cursors.Default;
			messageText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			messageText.Font = new System.Drawing.Font("Ubuntu Condensed", 32.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, 162);
			messageText.Location = new System.Drawing.Point(20, 85);
			messageText.Name = "messageText";
			messageText.Size = new System.Drawing.Size(560, 170);
			messageText.TabIndex = 6;
			messageText.Text = "Invalid token";
			messageText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			messageText.UseCompatibleTextRendering = true;
			warningLabel.BackColor = System.Drawing.Color.FromArgb(14, 57, 85);
			warningLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			warningLabel.Font = new System.Drawing.Font("Ubuntu Condensed", 42f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			warningLabel.ForeColor = System.Drawing.Color.White;
			warningLabel.Image = HabboLauncher.Properties.Resources.launcher_warning_header_bar;
			warningLabel.Location = new System.Drawing.Point(15, 16);
			warningLabel.Name = "warningLabel";
			warningLabel.Size = new System.Drawing.Size(570, 50);
			warningLabel.TabIndex = 14;
			warningLabel.Text = "Warning";
			warningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			warningLabel.UseCompatibleTextRendering = true;
			CloseButton.BackColor = System.Drawing.Color.FromArgb(104, 17, 17);
			CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
			CloseButton.Image = HabboLauncher.Properties.Resources.launcher_close;
			CloseButton.Location = new System.Drawing.Point(534, 14);
			CloseButton.Name = "CloseButton";
			CloseButton.Size = new System.Drawing.Size(52, 52);
			CloseButton.TabIndex = 13;
			CloseButton.TabStop = false;
			CloseButton.Click += new System.EventHandler(CloseButton_Click);
			pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			pictureBox1.BackColor = System.Drawing.Color.Transparent;
			pictureBox1.Image = HabboLauncher.Properties.Resources.launcher_warning_header_bar;
			pictureBox1.Location = new System.Drawing.Point(10, 10);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(580, 60);
			pictureBox1.TabIndex = 12;
			pictureBox1.TabStop = false;
			okButton.BackColor = System.Drawing.Color.Transparent;
			okButton.BackgroundImage = HabboLauncher.Properties.Resources.primary_btn;
			okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			okButton.Cursor = System.Windows.Forms.Cursors.Hand;
			okButton.FlatAppearance.BorderSize = 0;
			okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			okButton.Font = new System.Drawing.Font("Ubuntu Condensed", 32.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, 162);
			okButton.ForeColor = System.Drawing.Color.Black;
			okButton.Location = new System.Drawing.Point(320, 265);
			okButton.Name = "okButton";
			okButton.Size = new System.Drawing.Size(260, 78);
			okButton.TabIndex = 15;
			okButton.Text = "OK";
			okButton.UseVisualStyleBackColor = false;
			okButton.Visible = false;
			okButton.Click += new System.EventHandler(okButton_Click);
			cancelButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cancelButton.BackColor = System.Drawing.Color.Transparent;
			cancelButton.BackgroundImage = HabboLauncher.Properties.Resources.secondary_btn;
			cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
			cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelButton.FlatAppearance.BorderSize = 0;
			cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cancelButton.Font = new System.Drawing.Font("Ubuntu Condensed", 32.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, 162);
			cancelButton.Location = new System.Drawing.Point(20, 265);
			cancelButton.Name = "cancelButton";
			cancelButton.Size = new System.Drawing.Size(260, 78);
			cancelButton.TabIndex = 16;
			cancelButton.Text = "Cancel";
			cancelButton.UseVisualStyleBackColor = false;
			cancelButton.Visible = false;
			cancelButton.Click += new System.EventHandler(cancelButton_Click);
			base.AcceptButton = okButton;
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			BackColor = System.Drawing.Color.Fuchsia;
			BackgroundImage = HabboLauncher.Properties.Resources.bg_box_warning;
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			base.CancelButton = cancelButton;
			base.ClientSize = new System.Drawing.Size(600, 361);
			base.Controls.Add(cancelButton);
			base.Controls.Add(okButton);
			base.Controls.Add(CloseButton);
			base.Controls.Add(messageText);
			base.Controls.Add(warningLabel);
			base.Controls.Add(pictureBox1);
			DoubleBuffered = true;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "WarningBox";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Habbo";
			base.TransparencyKey = System.Drawing.Color.Fuchsia;
			((System.ComponentModel.ISupportInitialize)CloseButton).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}
	}
}
