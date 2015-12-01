namespace Quail
{
  partial class OptionsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDefaultLogLocation = new System.Windows.Forms.TextBox();
            this.cbAutoLoadLog = new System.Windows.Forms.CheckBox();
            this.tbMaxBytesToLoad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTailTimeout = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLogEditCommand = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbTimeFormat = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbAutoPause = new System.Windows.Forms.CheckBox();
            this.cbQueryLogHandling = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGetColor = new System.Windows.Forms.Button();
            this.btnLightTheme = new System.Windows.Forms.Button();
            this.btnDarkTheme = new System.Windows.Forms.Button();
            this.tbFilterColor = new System.Windows.Forms.TextBox();
            this.tbDefaultColor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBackgroundColor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(289, 317);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(370, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Default Log File";
            // 
            // tbDefaultLogLocation
            // 
            this.tbDefaultLogLocation.Location = new System.Drawing.Point(130, 19);
            this.tbDefaultLogLocation.Name = "tbDefaultLogLocation";
            this.tbDefaultLogLocation.Size = new System.Drawing.Size(276, 20);
            this.tbDefaultLogLocation.TabIndex = 1;
            // 
            // cbAutoLoadLog
            // 
            this.cbAutoLoadLog.AutoSize = true;
            this.cbAutoLoadLog.Location = new System.Drawing.Point(130, 42);
            this.cbAutoLoadLog.Name = "cbAutoLoadLog";
            this.cbAutoLoadLog.Size = new System.Drawing.Size(96, 17);
            this.cbAutoLoadLog.TabIndex = 2;
            this.cbAutoLoadLog.Text = "Auto Load Log";
            this.cbAutoLoadLog.UseVisualStyleBackColor = true;
            // 
            // tbMaxBytesToLoad
            // 
            this.tbMaxBytesToLoad.Location = new System.Drawing.Point(130, 117);
            this.tbMaxBytesToLoad.Name = "tbMaxBytesToLoad";
            this.tbMaxBytesToLoad.Size = new System.Drawing.Size(73, 20);
            this.tbMaxBytesToLoad.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Bytes to Load";
            // 
            // tbTailTimeout
            // 
            this.tbTailTimeout.Location = new System.Drawing.Point(130, 91);
            this.tbTailTimeout.Name = "tbTailTimeout";
            this.tbTailTimeout.Size = new System.Drawing.Size(73, 20);
            this.tbTailTimeout.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Tail Timeout";
            // 
            // tbLogEditCommand
            // 
            this.tbLogEditCommand.Location = new System.Drawing.Point(130, 65);
            this.tbLogEditCommand.Name = "tbLogEditCommand";
            this.tbLogEditCommand.Size = new System.Drawing.Size(276, 20);
            this.tbLogEditCommand.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Edit Command";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(442, 304);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbTimeFormat);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.cbAutoPause);
            this.tabPage1.Controls.Add(this.cbQueryLogHandling);
            this.tabPage1.Controls.Add(this.tbLogEditCommand);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tbDefaultLogLocation);
            this.tabPage1.Controls.Add(this.tbTailTimeout);
            this.tabPage1.Controls.Add(this.cbAutoLoadLog);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tbMaxBytesToLoad);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(434, 278);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbTimeFormat
            // 
            this.tbTimeFormat.Location = new System.Drawing.Point(130, 189);
            this.tbTimeFormat.Name = "tbTimeFormat";
            this.tbTimeFormat.Size = new System.Drawing.Size(276, 20);
            this.tbTimeFormat.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(59, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Time Format";
            // 
            // cbAutoPause
            // 
            this.cbAutoPause.AutoSize = true;
            this.cbAutoPause.Location = new System.Drawing.Point(130, 143);
            this.cbAutoPause.Name = "cbAutoPause";
            this.cbAutoPause.Size = new System.Drawing.Size(81, 17);
            this.cbAutoPause.TabIndex = 23;
            this.cbAutoPause.Text = "Auto Pause";
            this.cbAutoPause.UseVisualStyleBackColor = true;
            // 
            // cbQueryLogHandling
            // 
            this.cbQueryLogHandling.AutoSize = true;
            this.cbQueryLogHandling.Location = new System.Drawing.Point(130, 166);
            this.cbQueryLogHandling.Name = "cbQueryLogHandling";
            this.cbQueryLogHandling.Size = new System.Drawing.Size(120, 17);
            this.cbQueryLogHandling.TabIndex = 22;
            this.cbQueryLogHandling.Text = "Query Log Handling";
            this.cbQueryLogHandling.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btnGetColor);
            this.tabPage2.Controls.Add(this.btnLightTheme);
            this.tabPage2.Controls.Add(this.btnDarkTheme);
            this.tabPage2.Controls.Add(this.tbFilterColor);
            this.tabPage2.Controls.Add(this.tbDefaultColor);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tbBackgroundColor);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(434, 278);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Colors";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(205, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 23);
            this.button2.TabIndex = 34;
            this.button2.Tag = "tbDefaultColor";
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnGetColor_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 33;
            this.button1.Tag = "tbFilterColor";
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnGetColor_Click);
            // 
            // btnGetColor
            // 
            this.btnGetColor.Location = new System.Drawing.Point(205, 6);
            this.btnGetColor.Name = "btnGetColor";
            this.btnGetColor.Size = new System.Drawing.Size(35, 23);
            this.btnGetColor.TabIndex = 32;
            this.btnGetColor.Tag = "tbBackgroundColor";
            this.btnGetColor.Text = "...";
            this.btnGetColor.UseVisualStyleBackColor = true;
            this.btnGetColor.Click += new System.EventHandler(this.btnGetColor_Click);
            // 
            // btnLightTheme
            // 
            this.btnLightTheme.Location = new System.Drawing.Point(246, 54);
            this.btnLightTheme.Name = "btnLightTheme";
            this.btnLightTheme.Size = new System.Drawing.Size(75, 23);
            this.btnLightTheme.TabIndex = 31;
            this.btnLightTheme.Text = "Light Theme";
            this.btnLightTheme.UseVisualStyleBackColor = true;
            this.btnLightTheme.Click += new System.EventHandler(this.btnLightTheme_Click);
            // 
            // btnDarkTheme
            // 
            this.btnDarkTheme.Location = new System.Drawing.Point(246, 24);
            this.btnDarkTheme.Name = "btnDarkTheme";
            this.btnDarkTheme.Size = new System.Drawing.Size(75, 23);
            this.btnDarkTheme.TabIndex = 30;
            this.btnDarkTheme.Text = "Dark Theme";
            this.btnDarkTheme.UseVisualStyleBackColor = true;
            this.btnDarkTheme.Click += new System.EventHandler(this.btnDarkTheme_Click);
            // 
            // tbFilterColor
            // 
            this.tbFilterColor.Location = new System.Drawing.Point(109, 32);
            this.tbFilterColor.Name = "tbFilterColor";
            this.tbFilterColor.Size = new System.Drawing.Size(90, 20);
            this.tbFilterColor.TabIndex = 2;
            // 
            // tbDefaultColor
            // 
            this.tbDefaultColor.Location = new System.Drawing.Point(109, 58);
            this.tbDefaultColor.Name = "tbDefaultColor";
            this.tbDefaultColor.Size = new System.Drawing.Size(90, 20);
            this.tbDefaultColor.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Background";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Default Text";
            // 
            // tbBackgroundColor
            // 
            this.tbBackgroundColor.Location = new System.Drawing.Point(109, 6);
            this.tbBackgroundColor.Name = "tbBackgroundColor";
            this.tbBackgroundColor.Size = new System.Drawing.Size(90, 20);
            this.tbBackgroundColor.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Filter Text";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.66474F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.33526F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(26, 84);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(214, 187);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 342);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.Text = "Quail Options";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.TextBox tbDefaultLogLocation;
    public System.Windows.Forms.CheckBox cbAutoLoadLog;
    public System.Windows.Forms.TextBox tbMaxBytesToLoad;
    private System.Windows.Forms.Label label5;
    public System.Windows.Forms.TextBox tbTailTimeout;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.TextBox tbLogEditCommand;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    public System.Windows.Forms.TextBox tbFilterColor;
    public System.Windows.Forms.TextBox tbDefaultColor;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    public System.Windows.Forms.TextBox tbBackgroundColor;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Button btnLightTheme;
    private System.Windows.Forms.Button btnDarkTheme;
    public System.Windows.Forms.CheckBox cbAutoPause;
    public System.Windows.Forms.CheckBox cbQueryLogHandling;
    private System.Windows.Forms.ColorDialog colorDialog1;
    private System.Windows.Forms.Button btnGetColor;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    public System.Windows.Forms.TextBox tbTimeFormat;
    private System.Windows.Forms.Label label10;

  }
}