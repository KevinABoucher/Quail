using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using QuailControls;
using Timer = System.Windows.Forms.Timer;

namespace Quail
{
    internal class Form1 : Form
  {
    private static bool mFullscreen;
    private const string mruRegKey = "SOFTWARE\\Quail\\MruLists";
    private readonly string mBadFile = "";
    private readonly StringCollection mColBuffer = new StringCollection();
    private readonly Hashtable mColorCache = new Hashtable();
    private readonly string mConfigLocation = "";
    private readonly string mGoodFile = "";
    private readonly char[] SPACE_SEPARATOR = new char[] { ' ' };

    private readonly Hashtable mHighlightColors = new Hashtable();
    private readonly Mutex mLogMutex = new Mutex();
    private readonly MyOptions mOptions = new MyOptions();
    private readonly Regex mRegLines = new Regex(@"\n", RegexOptions.Compiled);
    private ToolStripPanel BottomToolStripPanel;
    private Button btnClear;
    private Button btnCloseFind;
    private Button btnExecuteQuery;
    private Button btnFind;
    private Button btnFindAll;
    private Button btnFindPrevious;
    private Button btnOpenSQL;
    private Button btnSaveGrid;
    private Button btnSaveSQL;
    private CheckBox cbBayesian;
    private ComboBox cbQueryType;
    private CheckBox cbShowSelected;
    private CheckBox cbTimeSlice;
    private ToolStripMenuItem clearToolStripMenuItem;
    private IContainer components;
    private ToolStripContentPanel ContentPanel;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem copyToolStripMenuItem;
    private ToolStripMenuItem copyToolStripMenuItem1;
    private ToolStripMenuItem ctxMenuItemCopy;
    private ContextMenuStrip ctxMenuLog;
    private ContextMenuStrip ctxMenuSelected;
    private DataGridView dgvResults;
    private DateTimePicker dtTimeSliceEnd;
    private DateTimePicker dtTimeSliceStart;
    private ToolStripMenuItem editFileToolStripMenuItem;
    private ToolStripMenuItem editFileToolStripMenuItem1;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem findAllToolStripMenuItem;
    private ToolStripMenuItem fullscreenToolStripMenuItem;
    private ToolStripMenuItem insertMarkToolStripMenuItem;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label lblBayesionScore;
    private Label lblClear;
    private Label lblError;
    private Label lblLoop;
    private Label lblNotes;
    public ColorListBox lbLog;
    private Label lblPause;
    private Label lblShowErrors;
    private Label lblShowLastError;
    private ToolStripPanel LeftToolStripPanel;
    private ListBox lstResults;
    private ToolStripMenuItem markToolStripMenuItem;
    private bool mAutoPause = true;
    private Color mBackgroundColor = Color.WhiteSmoke;
    private bool mBayesian;
    private Color mDefaultColor = Color.Black;
    private string mDefaultLogLocation = @"c:\temp\mtlog.txt";
    private DelegateAddString mDelegateAddString;
    private MenuStrip menuStrip1;
    private string mFileName = "";
    private SpamFilter mFilter;
    private Color mFilterColor = Color.Teal;
    private bool mIsNewSelection;
    public ArrayList mItems = new ArrayList();
    private string mLogEditCommand = @"notepad.exe {0}";
    private long mMaxBytesToLoad = 100000000;
    private TextFileMonitor mMonitor;
    private ToolStripMenuItem mnuRecentFiles;
    private ToolStripMenuItem mnuTrainHide;
    private ToolStripMenuItem mnuTrainShow;
    private bool mOpaque;
    private bool mPause;
    private bool mQueryLogHandling = true;
    protected MruStripMenuInline mruMenu;
    private int mSearchLocation;
    private bool mShowSelected;
    private int mTailTimeout = 1000;
    private Thread mThread;
    private ToolStripMenuItem openDefaultToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem optionsToolStripMenuItem;
    private Panel panel1;
    private Panel panel2;
    private Panel panel3;
    private Panel panel4;
    private Panel panel5;
    private Panel panel6;
    private Panel panel7;
    private Panel panelBayesianFilter;
    private Panel panelQueryResults;
    private Panel panelSlider;
    private Panel panelTransparency;
    private ToolStripMenuItem pauseToolStripMenuItem;
    private ToolStripPanel RightToolStripPanel;
    private Timer SelectionTimer;
    private ToolStripMenuItem showAllErrorsToolStripMenuItem;
    private SplitContainer splitContainerBottom;
    private SplitContainer splitContainerMain;
    private Splitter splitterQueryResults;
    private TabPage tabAdvancedOptions;
    private TabControl tabControl1;
    private TabPage tabPageHelp;
    private TabPage tabPageLog;
    private TabPage tabPageQuery;
    private TextBox tbFind;
    private TextBox tbQuery;
    public TextBox tbSelected;
    private TrackBar tbTransparency;
    private ToolStripMenuItem tileBottom3rdToolStripMenuItem;
    private ToolStripMenuItem tileBottomHalfToolStripMenuItem;
    private ToolStripMenuItem tileToolStripMenuItem;
    private ToolStripMenuItem tileTop3rdToolStripMenuItem;
    private ToolStripMenuItem toolsToolStripMenuItem;
    private ToolStripContainer toolStripContainer1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItem12;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
    private ToolStripMenuItem toolStripMenuItem5;
    private ToolStripMenuItem toolStripMenuItem6;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator10;
    private ToolStripSeparator toolStripSeparator11;
    private ToolStripSeparator toolStripSeparator13;
    private ToolStripSeparator toolStripSeparator14;
    private ToolStripSeparator toolStripSeparator15;
    private ToolStripSeparator toolStripSeparator16;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripSeparator toolStripSeparator20;
    private ToolStripSeparator toolStripSeparator21;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripPanel TopToolStripPanel;
    private ToolStripMenuItem transparencyToolStripMenuItem;
    private ToolStripMenuItem viewLastErrorToolStripMenuItem;
    private WebBrowser webBrowser1;
    public CheckBox cbFilterOn;
    public TextBox tbFilterString;
    private Button btnApplyFilter;
    private ToolTip toolTip1;
    private ToolStripMenuItem windowToolStripMenuItem;

    public Form1()
    {
      InitializeComponent();

      CheckForIllegalCrossThreadCalls = false;

      // use double buffering
      SetStyle(ControlStyles.DoubleBuffer, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.UserPaint, true);

      // get MRU list
      mruMenu = new MruStripMenuInline(fileToolStripMenuItem, mnuRecentFiles, OnMruFile, mruRegKey + "\\MRU", 10);

      // load saved options
      try
      {
        mOptions = MyOptions.Load();

        // - uncomment to create intial options.xml
        //      mOptions = new MyOptions();
        //      HighlightItem itm = new HighlightItem("[ERROR", "Red");
        //      HighlightItem itm2 = new HighlightItem("[WARNING", "Orange");
        //      mOptions.AddItem(itm);
        //      mOptions.AddItem(itm2);
        //      mOptions.Save();
        //      return;

        mBackgroundColor = ColorTranslator.FromHtml(mOptions.option.BackgroundColor);
        mFilterColor = ColorTranslator.FromHtml(mOptions.option.FilterColor);
        mDefaultColor = ColorTranslator.FromHtml(mOptions.option.DefaultColor);
        mDefaultLogLocation = mOptions.option.DefaultLogLocation;
        mAutoPause = mOptions.option.AutoPause;
        mLogEditCommand = mOptions.option.LogEditCommand;
        mQueryLogHandling = mOptions.option.QueryLogHandling;
        mTailTimeout = mOptions.option.TailTimeout;
        mMaxBytesToLoad = mOptions.option.MaxBytesToLoad;
        dtTimeSliceStart.CustomFormat = mOptions.option.TimeFormat;
        dtTimeSliceEnd.CustomFormat = mOptions.option.TimeFormat;

        lbLog.BackColor = mBackgroundColor;

        // add highlight colors to a hash      
        foreach (HighlightItem item in mOptions.HighlightItems)
        {
          mHighlightColors.Add(item.ContainsString, ColorTranslator.FromHtml(item.HiglightColor));
        }

        SelectionTimer.Enabled = true;

        mConfigLocation = Assembly.GetExecutingAssembly().Location;
        if (mConfigLocation != null)
        {
          mConfigLocation = mConfigLocation.Substring(0, mConfigLocation.LastIndexOf(@"\")) + @"\config\";

          string sqlFile = mConfigLocation + "Samples.sql";

          tbQuery.Text = FileManager.ReadTextFile(sqlFile);
        }

        // Init Bayesian filter
        mGoodFile = mConfigLocation + "view.txt";
        mBadFile = mConfigLocation + "ignore.txt";
        LoadBayesianFilter();

        dtTimeSliceStart.Value = DateTime.Now.AddHours(-1);
        dtTimeSliceEnd.Value = dtTimeSliceStart.Value.AddDays(1);
      }
      catch (Exception exp)
      {
        //LoadBayesianFilter was modified to ignore missing mGoodFile and mBadFile files
        //Essentially we will no longer get here to display this message under those conditions
        //but will still get here if options.xml or possibly Samples.sql is missing
        MessageBox.Show(
          "Couldn't load configuration.\r\n\r\nMaybe you are trying to run Quail from a different directory?\r\nTry creating a shortcut instead.  For now, I'll use some default settings...\r\n\r\nDetails: " +
          exp.Message);
      }
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.Run(new Form1());
    }

    private void LoadBayesianFilter()
    {
      mFilter = new SpamFilter();
      Corpus bad = new Corpus();
      Corpus good = new Corpus();

      bad.LoadFromFile(mBadFile);
      good.LoadFromFile(mGoodFile);
      
      mFilter.Load(good, bad);

      /*
      // Just for grins, we'll dump out some statistics about the data we just loaded.
      lstResults.Items.Clear();
      lstResults.Items.Add(String.Format(@"Bayesian Filter Training Stats:  Good:{0} Bad:{1} Prob:{2}"
        , mFilter.Good.Tokens.Count
        , mFilter.Bad.Tokens.Count
        , mFilter.Prob.Count));

      // ... and some probabilities for keys
      foreach (string key in mFilter.Prob.Keys)
      {
        if (mFilter.Prob[key] > 0.02)
        {
          lstResults.Items.Add(String.Format("{0},{1}", mFilter.Prob[key].ToString(".0000"), key));
        }
      }
      */
    }

    private void AddToBayesianFilter(string good, string bad)
    {
      if (good != null)
        FileManager.AppendToFile(mGoodFile, good);

      if (bad != null)
        FileManager.AppendToFile(mBadFile, bad);

      LoadBayesianFilter();
    }

    // remove items based on bayesian filter
    private void RemoveItemsBasedOnBayesianFilter()
    {
      if (mBayesian)
      {
        ArrayList itemsToKeep = new ArrayList();
        foreach (string itm in mItems)
        {
          if (mFilter.Test(itm) < .99)
            itemsToKeep.Add(itm);
        }

        ClearLog();
        foreach (string itm in itemsToKeep)
        {
          AddString(itm);
        }
      }
    }

    // remove items based on time slice filter
    private void RemoveItemsBasedOnTimeSlice()
    {
      if (cbTimeSlice.Checked)
      {
        ArrayList itemsToKeep = new ArrayList();
        foreach (string itm in mItems)
        {
          string[] timeArray = itm.Split(SPACE_SEPARATOR);
          DateTime t;
          if (timeArray.Length <= 1) continue;
          if (DateTime.TryParse(timeArray[0] + " " + timeArray[1], out t))
          {
            if (t < dtTimeSliceStart.Value) continue;
            if (t > dtTimeSliceEnd.Value) continue;

            itemsToKeep.Add(itm);
          }
        }

        ClearLog();
        foreach (string itm in itemsToKeep)
        {
          AddString(itm);
        }
      }
    }

    private void OnMruFile(int number, String filename)
    {
      if (File.Exists(filename))
      {
        mruMenu.SetFirstFile(number);

        MonitorFile(filename);
      }
      else
      {
        mruMenu.RemoveFile(number);
        tbSelected.Text = "The file " + filename + " no longer exists.  It was removed from the MRU list.";
      }
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (mThread != null)
      {
        // Request that mThread be stopped
        mThread.Abort();

        // Wait until mThread finishes.
        mThread.Join(1000);
      }

      if (disposing && (components != null))
        components.Dispose();
      base.Dispose(disposing);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      // set window position first
      if (mOptions.window.Left >= 0 &&
          mOptions.window.Width >= 0 &&
          mOptions.window.Top >= 0 &&
          mOptions.window.Height >= 0)
      {
        Left = mOptions.window.Left;
        Width = mOptions.window.Width;
        Top = mOptions.window.Top;
        Height = mOptions.window.Height;
      }
      else
        WindowState = FormWindowState.Maximized;

      // Set the key GUI elements
      splitContainerMain.Dock = DockStyle.Fill;
      splitContainerBottom.Dock = DockStyle.Fill;
      lbLog.Dock = DockStyle.Fill;
      tbSelected.Dock = DockStyle.Fill;
      lstResults.Dock = DockStyle.Fill;

      splitContainerMain.SplitterDistance = splitContainerMain.Height - 60;
      splitContainerBottom.SplitterDistance = splitContainerBottom.Width - 400;


      // Load help file
      string helpFile = Assembly.GetExecutingAssembly().Location;
      if (helpFile != null)
      {
        helpFile = helpFile.Substring(0, helpFile.LastIndexOf(@"\")) + @"\config\QuailHelp.htm";
        helpFile = @"file:///" + helpFile;
        webBrowser1.Navigate(helpFile);
      }

      // Parse Command line file
      string filename;
      string[] args = Environment.GetCommandLineArgs();

      if (args.Length > 1)
        filename = args[1];
      else
      {
        if (mOptions.option.AutoLoadLog)
          filename = mOptions.option.DefaultLogLocation;
        else
        {
          DisplayHelp();
          return;
        }
      }

      // Load initial file
      MonitorFile(filename);
    }

    public void DisplayHelp()
    {
      tabControl1.SelectedIndex = 2;
    }

    /// <summary>
    /// AddString delegate method called from TextFileMonitor
    /// </summary>
    /// <param name="s"></param>
    public void AddString(string s)
    {
      if (mPause)
      {
        mColBuffer.Add(s);
        return;
      }

      mLogMutex.WaitOne();

      if (mColBuffer.Count > 0)
      {
        foreach (string s1 in mColBuffer)
        {
          if (!s1.Equals(Environment.NewLine))
            ParseString(s1);
        }
        mColBuffer.Clear();
      }
      else
      {
        if (!s.Equals(Environment.NewLine))
          ParseString(s);
      }

      mIsNewSelection = true;
      mLogMutex.ReleaseMutex();
    }

    // Split string into lines and add them to mItems
    public void ParseString(string s)
    {
      try
      {
        string[] lines = mRegLines.Split(s);
        Regex regFilter = new Regex(tbFilterString.Text);

        lbLog.BeginUpdate();
        lbLog.ClearSelected();
        int i = lbLog.Count;

        foreach (string line in lines)
        {
          if (line.Length != 0)
          {
            if (cbTimeSlice.Checked)
            {
              string[] timeArray = line.Split(SPACE_SEPARATOR);
              DateTime t;
              if (timeArray.Length <= 1) continue;
              if (DateTime.TryParse(timeArray[0] + " " + timeArray[1], out t))
              {
                if (t < dtTimeSliceStart.Value) continue;
                if (t > dtTimeSliceEnd.Value) continue;
              }
            }

            // Don't show SPAM
            if (mBayesian)
            {
              if (mFilter.Test(line) > .99)
                continue;
            }

            // Apply Filter
            if(cbFilterOn.Checked)
            {
              if (!regFilter.IsMatch(line))
                continue;
            }

            if (mQueryLogHandling)
            {
              // Ignore pop first message in QueryLog
              if (mFileName.Contains("querylog"))
              {
                if (line.Contains("PopFirstMessage"))
                  continue;

                if (line.Contains("p_pipelineid"))
                  continue;

                if (line.Contains("p_systemtime"))
                  continue;

                if (line.Contains("value: <"))
                  continue;

                if (line.Contains("P stored procedure"))
                  continue;

                if (line.Contains("p_messageid"))
                  continue;
              }
            }

            ++i;
            mItems.Add(line);
          }
        }
        lbLog.Count = i;

        lbLog.SelectedIndex = mItems.Count - 1;
        lbLog.EndUpdate();
      }
      catch (Exception exp)
      {
        MessageBox.Show(exp.ToString());
      }
    }

    public void ColorizeLine(string s, int index)
    {
      foreach (HighlightItem item in mOptions.HighlightItems)
      {
        if (s.IndexOf(item.ContainsString) > -1)
        {
          Color c = (Color) mHighlightColors[item.ContainsString];
          mColorCache.Add(index, c);
          return;
        }
      }
      mColorCache.Add(index, mDefaultColor);
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
      GlobalKeyHandler(sender, e);
    }

    private void btnFindAll_KeyDown(object sender, KeyEventArgs e)
    {
      GlobalKeyHandler(sender, e);
    }

    private void tabControl1_KeyDown(object sender, KeyEventArgs e)
    {
      GlobalKeyHandler(sender, e);
    }

    private void tbSelected_KeyDown(object sender, KeyEventArgs e)
    {
      GlobalKeyHandler(sender, e);
    }

    private void tbSelected_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.C && e.Control)
      {
        CopySelected();
        e.Handled = true;
        return;
      }
    }

    private void cbQueryType_KeyDown(object sender, KeyEventArgs e)
    {
      tbQuery_KeyDown(sender, e);
    }

    private void btnExecuteQuery_KeyDown(object sender, KeyEventArgs e)
    {
      tbQuery_KeyDown(sender, e);
    }

    private void tbQuery_KeyDown(object sender, KeyEventArgs e)
    {
      GlobalKeyHandler(sender, e);
    }

    private void lbLog_KeyDown(object sender, KeyEventArgs e)
    {
      GlobalKeyHandler(sender, e);
    }

    /// <summary>
    /// Position window on screen
    /// </summary>
    /// <param name="n"></param>
    /// <param name="d"></param>
    private void Tile(double n, double d)
    {
      WindowState = FormWindowState.Normal;
      Rectangle r = SystemInformation.WorkingArea;

      Left = 0;
      Width = r.Right;
      Top = (int) (r.Height*(n - 1)/d);
      Height = (int) (r.Bottom/d);
    }


    /// <summary>
    /// GlobalKeyHandler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void GlobalKeyHandler(object sender, KeyEventArgs e)
    {
      if (e.Control)
      {
        switch (e.KeyCode)
        {
          case Keys.D1:
            Tile(1, 3);
            break;

          case Keys.D2:
            Tile(1, 2);
            break;

          case Keys.D3:
            Tile(3, 3);
            break;

          case Keys.D4:
            Tile(2, 2);
            break;

          case Keys.T:
            ToggleTransparent();
            e.Handled = true;
            break;

          case Keys.H:
            DisplayHelp();
            e.Handled = true;
            break;

          case Keys.F:
            ShowFind();
            e.Handled = true;
            break;

          case Keys.P:
            Pause();
            e.Handled = true;
            break;

          case Keys.L:
            MonitorFile(mDefaultLogLocation);
            e.Handled = true;
            break;

          case Keys.O:
            SelectFile();
            e.Handled = true;
            break;

          case Keys.C:
            CopyIt();
            e.Handled = true;
            break;

          case Keys.Q:
            CopyQuery();
            e.Handled = true;
            break;

          case Keys.A:
            tbSelected.SelectAll();
            e.Handled = true;
            break;

          case Keys.M:
            Mark();
            e.Handled = true;
            break;
        }
      }

      if (e.KeyCode == Keys.F11)
        Fullscreen();


      if (e.KeyCode == Keys.F4)
        GotoLastError();

      if (e.KeyCode == Keys.F5)
        ShowAllErrors();

      if (e.KeyCode == Keys.F3)
      {
        if (e.Shift)
          DoFind(true, false);
        else
          DoFind(true, true);
        e.Handled = true;
      }

      if (e.KeyCode == Keys.F6)
      {
        ClearLog();
        e.Handled = true;
      }

      if (e.KeyCode == Keys.F9)
      {
        EditLog();
        e.Handled = true;
      }

      if (e.KeyCode == Keys.Escape)
      {
        panel1.Visible = false;
        lbLog.Focus();
        e.Handled = true;
      }

      if (e.KeyCode == Keys.F5)
      {
        ExecuteQuery();
        e.Handled = true;
      }
    }

    public void ToggleTransparent()
    {
      Opacity = mOpaque ? 1 : .75;
      tbTransparency.Value = (int) (Opacity*100);
      mOpaque = !mOpaque;
    }

    public void EditLog()
    {
      FileManager.EditFile(mFileName, mLogEditCommand);
    }

    public void Mark()
    {
      AddString("****************************************");
      AddString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
      AddString("****************************************");

      // unpause
      if (mAutoPause)
      {
        mPause = false;
        lblPause.Text = "Running...";
        AddString(Environment.NewLine);
      }
    }

    public void ClearLog()
    {
      mColorCache.Clear();
      lbLog.ClearSelected();
      lbLog.Count = 0;
      mItems.Clear();
      tbSelected.Text = "";
      lbLog.Refresh();
    }

    private void lbLog_KeyPress(object sender, KeyPressEventArgs e)
    {
      // Stop the character from being entered into the control
      e.Handled = true;
    }

    private void tbFind_KeyDown(object sender, KeyEventArgs e)
    {
      panel1.Visible = true;
      if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.F3))
      {
        if (e.Shift)
          DoFind(true, false);
        else
          DoFind(true, true);
        e.Handled = true;
        lbLog.Invalidate();
        return;
      }

      if (e.KeyCode == Keys.F4)
        GotoLastError();

      if (e.KeyCode == Keys.Escape)
      {
        panel1.Visible = false;
        lbLog.Focus();
        e.Handled = true;
        lbLog.Invalidate();
        return;
      }

      lbLog.Invalidate();
    }

    /// <summary>
    /// Put the selected items in the clipboard 
    /// </summary>
    private void CopyIt()
    {
      if (lbLog.SelectedIndex > -1 && mItems.Count > 0)
      {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < lbLog.mSelectedIndices.Count; i++)
        {
          sb.Append(mItems[lbLog.mSelectedIndices[i]].ToString());
          sb.Append(Environment.NewLine);
        }
        Clipboard.SetDataObject(sb.ToString());
      }
    }

    /// <summary>
    /// Copy selected text to the clipboard 
    /// </summary>
    private void CopySelected()
    {
      Clipboard.SetDataObject(tbSelected.SelectedText);
    }

    /// <summary>
    /// Put the selected items in the clipboard 
    /// </summary>
    private void CopyQuery()
    {
      if (lbLog.SelectedIndex > -1 && mItems.Count > 0)
      {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < lbLog.mSelectedIndices.Count; i++)
        {
          if (mItems[lbLog.mSelectedIndices[i]].ToString().Trim().Length > 0)
          {
            sb.Append(mItems[lbLog.mSelectedIndices[i]].ToString());
            sb.Append(Environment.NewLine);
          }
        }
        Clipboard.SetDataObject(sb.ToString());
      }
    }

    private void SelectFile()
    {
      string filename = FileManager.SelectFile("Select File For Monitoring",
                                               "All files (*.*)|*.*|txt files (*.txt)|*.txt", "c:\\");
      if (filename.Length > 0)
        MonitorFile(filename);
    }

    private void Pause()
    {
      mPause = !mPause;
      if (mPause)
        lblPause.Text = "Paused.";
      else
      {
        lblPause.Text = "Running...";
        AddString(Environment.NewLine);
      }
    }

    private void ShowFind()
    {
      tabControl1.SelectedIndex = 0;
      lblError.Text = "";
      lblError.Visible = false;
      panel1.Visible = true;
      tbFind.Focus();
    }

    private void ShowAllErrors()
    {
      ShowFind();
      tbFind.Text = "\"Error";
      DoFindAll();
    }

    private void DoFindAll()
    {
      lbLog.BeginUpdate();
      lbLog.ClearExistingSelection();
      mSearchLocation = 0;
      int savedFirstLocation = 0;
      lblError.Text = "";

      int i = 0;
      if (DoFind(false, true))
      {
        i++;
        savedFirstLocation = mSearchLocation;
      }

      while (DoFind(false, true))
      {
        i++;
        if (mSearchLocation == savedFirstLocation)
        {
          i--;
          break;
        }
        if (i > 999)
        {
          lblError.Text = "Done.  Found more than (" + i + ") matches.  Not all were highlighted.";
          lblError.Visible = true;
          lblLoop.Visible = false;
          lbLog.EndUpdate();
          return;
        }
      }
      lblError.Text = "Done.  Found (" + i + ") matches.";
      lblError.Visible = true;
      lblLoop.Visible = false;
      lbLog.EndUpdate();
    }

    private bool DoFind(bool clearSelected, bool isForward)
    {
      try
      {
        if (clearSelected)
        {
          mSearchLocation = lbLog.SelectedIndex;
          lbLog.ClearExistingSelection();
        }

        if (isForward)
        {
          if (mItems.Count == mSearchLocation)
            // handle case we we found the item on the last line - loop to top for next search
          {
            mSearchLocation = 0;
            lblLoop.Text = "Looped past end of file...";
            lblLoop.Visible = true;
          }
          else
            mSearchLocation++;
        }
        else
        {
          if (0 == mSearchLocation)
          {
            mSearchLocation = mItems.Count - 1;
            lblLoop.Text = "Looped past beginning of file...";
            lblLoop.Visible = true;
          }
          else
            mSearchLocation--;
        }

        mSearchLocation = CustomFind(tbFind.Text, mSearchLocation, isForward);

        if (mSearchLocation > -1)
        {
          lbLog.SelectedIndex = mSearchLocation;
          mIsNewSelection = true;

          lblError.Text = "";
          lblError.Visible = false;
          return true;
        }

        lblLoop.Visible = false;
        lblError.Visible = true;
        lblError.Text = "No match found.";
        return false;
      }
      catch (Exception exp)
      {
        lblLoop.Visible = false;
        lblError.Visible = true;
        lblError.Text = exp.Message;
        mSearchLocation = 0;
      }
      return false;
    }

    /// <summary>
    /// CustomFind
    /// </summary>
    /// <param name="s"></param>
    /// <param name="startIndex"></param>
    /// <param name="isForward"></param>
    /// <returns></returns>
    private int CustomFind(string s, int startIndex, bool isForward)
    {
      lblLoop.Visible = false;
      if (isForward)
      {
        for (int i = startIndex; i != startIndex - 1; i++)
        {
          if (i == -1)
            continue;
          if (i == mItems.Count)
          {
            lblLoop.Text = "Looped past end of file...";
            lblLoop.Visible = true;
            i = -2;
            continue;
          }

          string str = mItems[i].ToString().ToLower();
          if (str.IndexOf(s.ToLower()) > -1)
            return i;
        }
      }
      else
      {
        for (int i = startIndex; i != startIndex + 1; i--)
        {
          if (i == mItems.Count)
            continue;
          if (i == -1)
          {
            lblLoop.Text = "Looped past beginning of file...";
            lblLoop.Visible = true;
            i = mItems.Count;
            continue;
          }

          string str = mItems[i].ToString().ToLower();
          if (str.IndexOf(s.ToLower()) > -1)
            return i;
        }
      }
      return -1; // not found
    }

    private void MonitorFile(string fileName)
    {
      mFileName = fileName;

      if (File.Exists(mFileName))
      {
        mruMenu.AddFile(mFileName); // add file to MRU list
        mruMenu.SaveToRegistry();
      }

      if (mThread != null)
      {
        // Request that mThread be stopped
        mThread.Abort();

        // Wait until mThread finishes.
        mThread.Join(1000);
      }

      ClearLog();

      mDelegateAddString = AddString;
      Text = "Quail - " + fileName;

      mMonitor = new TextFileMonitor(this, fileName, mDelegateAddString, mTailTimeout, mMaxBytesToLoad);
      mThread = new Thread(mMonitor.DoTail);
      mThread.Start();
    }

    private void btnCloseFind_Click(object sender, EventArgs e)
    {
      panel1.Visible = false;
    }

    private void btnFindAll_Click(object sender, EventArgs e)
    {
      DoFindAll();
    }

    private void btnFind_Click(object sender, EventArgs e)
    {
      DoFind(true, true);
    }

    private void btnFindPrevious_Click(object sender, EventArgs e)
    {
      DoFind(true, false);
    }

    /*private void lbLog_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
    {
      if(e.Index >= mItems.Count)
        return;

      lbLog.Line = mItems[e.Index].ToString(); 
    }*/

    // Here's where I do the color highlighting
    private void lbLog_DrawItem(object sender, DrawItemEventArgs e)
    {
      if (e.Index >= mItems.Count)
        return;

      lbLog.Line = mItems[e.Index].ToString();

      if (!tbFind.Text.Equals(String.Empty))
      {
        if (lbLog.Line.ToLower().IndexOf(tbFind.Text.ToLower()) > -1)
        {
          lbLog.TextColor = mFilterColor;
          lbLog.IsHighlighted = true;
          return;
        }
      }
      lbLog.IsHighlighted = false;

      if (mColorCache.Contains(e.Index))
      {
        lbLog.TextColor = (Color) mColorCache[e.Index];
        return;
      }

      foreach (HighlightItem item in mOptions.HighlightItems)
      {
        if (lbLog.Line.IndexOf(item.ContainsString) > -1)
        {
          lbLog.TextColor = (Color) mHighlightColors[item.ContainsString];
          mColorCache.Add(e.Index, lbLog.TextColor);
          return;
        }
      }

      lbLog.TextColor = mDefaultColor;
      mColorCache.Add(e.Index, lbLog.TextColor);
    }

    private void lbLog_DragDrop(object sender, DragEventArgs e)
    {
      // Load file
      string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
      MonitorFile(files[0]);
    }

    private void lbLog_DragEnter(object sender, DragEventArgs e)
    {
      // Allow them to continue if it is a file
      if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
        e.Effect = DragDropEffects.All;
    }

    private void Form1_DragDrop(object sender, DragEventArgs e)
    {
      // Load file
      string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
      MonitorFile(files[0]);
    }

    private void Form1_DragEnter(object sender, DragEventArgs e)
    {
      // Allow them to continue if it is a file
      if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
        e.Effect = DragDropEffects.All;
    }

    private void lbLog_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (mAutoPause)
      {
        mIsNewSelection = true;

        // Auto Pause feature
        if (lbLog.SelectedIndex < mItems.Count - 1)
        {
          mPause = true;
          lblPause.Text = "Paused.";
        }
        else
        {
          mPause = false;
          lblPause.Text = "Running...";
          AddString(Environment.NewLine);
        }
      }
    }

    private void SelectionTimer_Tick(object sender, EventArgs e)
    {
      if (mIsNewSelection)
      {
        SelectionTimer.Enabled = false;
        tbSelected.Text = "Updating...";
        tbSelected.Refresh();

        // Put the selected items text in our textbox at the bottom 
        if (lbLog.SelectedIndex > -1 && mItems.Count > 0)
        {
          StringBuilder sb = new StringBuilder();
          for (int i = 0; i < lbLog.mSelectedIndices.Count; i++)
          {
            sb.Append(mItems[lbLog.mSelectedIndices[i]].ToString());
            sb.Append(Environment.NewLine);
          }
          tbSelected.Text = sb.ToString();

          // show bayesian score for selected
          if (mShowSelected)
            lblBayesionScore.Text = "Selected Score:  " + mFilter.Test(sb.ToString());
        }
        mIsNewSelection = false;
        SelectionTimer.Enabled = true;
      }
    }

    private void btnExecuteQuery_Click(object sender, EventArgs e)
    {
      ExecuteQuery();
    }

    // Execute Query
    private void ExecuteQuery()
    {
      if (cbQueryType.SelectedItem == null)
      {
        lblNotes.Text = "Please select a query type.";
        return;
      }

      lblNotes.Text = "Executing query, please wait...";
      lblNotes.Refresh();

      string Err = "";
      DataTable myRes = new DataTable();

      DateTime dtStart = DateTime.Now;
      switch (cbQueryType.SelectedItem.ToString().ToLower())
      {
        case "iis log":
          myRes = LogParser.IISquery(GetQueryText(), ref Err);
          break;
        case "w3c log":
          myRes = LogParser.W3Cquery(GetQueryText(), ref Err);
          break;
        case "event log":
          myRes = LogParser.GetData(GetQueryText(), ref Err);
          break;
        case "registry":
          myRes = LogParser.REGquery(GetQueryText(), ref Err);
          break;
        case "file system":
          myRes = LogParser.FSquery(GetQueryText(), ref Err);
          break;
        case "text file":
          myRes = LogParser.Textquery(GetQueryText(), ref Err);
          break;
        case "csv file":
          myRes = LogParser.CSVquery(GetQueryText(), ref Err);
          break;
        default:
          Err = "Please select a query type.";
          break;
      }
      TimeSpan dtEnd = DateTime.Now.Subtract(dtStart);
      lblNotes.Text = "Query returned (" + myRes.Rows.Count + ") rows.  In " + dtEnd.TotalSeconds + " seconds.";
      lblNotes.Refresh();

      if (Err == "")
        dgvResults.DataSource = myRes;
      else
        MessageBox.Show(Err);
    }

    private string GetQueryText()
    {
      string s = tbQuery.SelectedText.Length != 0 ? tbQuery.SelectedText : tbQuery.Text;
      s = s.Replace("[this]", mFileName);
      return s;
    }

    private void btnOpenSQL_Click(object sender, EventArgs e)
    {
      tbQuery.Text = FileManager.GetFileAndRead("sql");
    }

    private void btnSaveSQL_Click(object sender, EventArgs e)
    {
      FileManager.GetFileAndSave("sql", tbQuery.Text);
    }

    private void Form1_Closing(object sender, CancelEventArgs e)
    {
      mOptions.window.Left = Left;
      mOptions.window.Width = Width;
      mOptions.window.Top = Top;
      mOptions.window.Height = Height;

      mOptions.Save();
    }

    private void Fullscreen()
    {
      mFullscreen = !mFullscreen;

      if (mFullscreen)
      {
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        menuStrip1.Visible = false;
        Tile(1, 1);
      }
      else
      {
        WindowState = FormWindowState.Normal;
        FormBorderStyle = FormBorderStyle.Sizable;
        menuStrip1.Visible = true;
        Tile(1, 1);
      }
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SelectFile();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Environment.Exit(0);
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
      CopyIt();
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CopyQuery();
    }

    private void transparencyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ToggleTransparent();
    }

    private void findAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowFind();
    }

    private void tileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Tile(1, 2);
    }

    private void tileTop3rdToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Tile(1, 3);
    }

    private void tileBottom3rdToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Tile(3, 3);
    }

    private void tileBottomHalfToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Tile(2, 2);
    }

    private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Fullscreen();
    }

    private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Pause();
    }

    private void openDefaultToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MonitorFile(mDefaultLogLocation);
    }

    private void clearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ClearLog();
    }

    private void editFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EditLog();
    }

    private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OptionsForm dlg = new OptionsForm();

      // General options
      dlg.tbDefaultLogLocation.Text = mOptions.option.DefaultLogLocation;
      dlg.cbAutoLoadLog.Checked = mOptions.option.AutoLoadLog;
      dlg.cbAutoPause.Checked = mOptions.option.AutoPause;
      dlg.cbQueryLogHandling.Checked = mOptions.option.QueryLogHandling;
      dlg.tbLogEditCommand.Text = mOptions.option.LogEditCommand;
      dlg.tbMaxBytesToLoad.Text = mOptions.option.MaxBytesToLoad.ToString();
      dlg.tbTailTimeout.Text = mOptions.option.TailTimeout.ToString();
      dlg.tbTimeFormat.Text = mOptions.option.TimeFormat;

      // Colors
      dlg.tbBackgroundColor.Text = mOptions.option.BackgroundColor;
      dlg.tbFilterColor.Text = mOptions.option.FilterColor;
      dlg.tbDefaultColor.Text = mOptions.option.DefaultColor;

      // Higlight items
      dlg.HighlightItems = mOptions.HighlightItems;

      if (dlg.ShowDialog(this) == DialogResult.OK)
      {
        try
        {
          mOptions.option.DefaultLogLocation = dlg.tbDefaultLogLocation.Text;
          mOptions.option.AutoLoadLog = dlg.cbAutoLoadLog.Checked;
          mOptions.option.AutoPause = dlg.cbAutoPause.Checked;
          mOptions.option.QueryLogHandling = dlg.cbQueryLogHandling.Checked;

          mOptions.option.LogEditCommand = dlg.tbLogEditCommand.Text;
          mOptions.option.MaxBytesToLoad = long.Parse(dlg.tbMaxBytesToLoad.Text);
          mOptions.option.TailTimeout = int.Parse(dlg.tbTailTimeout.Text);
          mOptions.option.TimeFormat = dlg.tbTimeFormat.Text;

          mOptions.option.BackgroundColor = dlg.tbBackgroundColor.Text;
          mOptions.option.FilterColor = dlg.tbFilterColor.Text;
          mOptions.option.DefaultColor = dlg.tbDefaultColor.Text;

          dtTimeSliceStart.CustomFormat = mOptions.option.TimeFormat;
          dtTimeSliceEnd.CustomFormat = mOptions.option.TimeFormat;

          int j = 0;
          for (int i = 0; i < dlg.tableLayoutPanel1.Controls.Count; i++)
          {
            TextBox myTextBox = dlg.tableLayoutPanel1.Controls[++i] as TextBox;
            if (myTextBox != null) mOptions.HighlightItems[j].HiglightColor = myTextBox.Text;
            j++;
            i++; //skip button
          }

          // add highlight colors to a hash      
          foreach (HighlightItem item in mOptions.HighlightItems)
          {
            mHighlightColors[item.ContainsString] = ColorTranslator.FromHtml(item.HiglightColor);
          }

          mDefaultLogLocation = mOptions.option.DefaultLogLocation;
          mLogEditCommand = mOptions.option.LogEditCommand;

          mAutoPause = mOptions.option.AutoPause;
          mQueryLogHandling = mOptions.option.QueryLogHandling;
          mTailTimeout = mOptions.option.TailTimeout;
          mMaxBytesToLoad = mOptions.option.MaxBytesToLoad;

          mBackgroundColor = ColorTranslator.FromHtml(mOptions.option.BackgroundColor);
          mFilterColor = ColorTranslator.FromHtml(mOptions.option.FilterColor);
          mDefaultColor = ColorTranslator.FromHtml(mOptions.option.DefaultColor);

          mOptions.Save();

          // refresh
          lbLog.BackColor = mBackgroundColor;
          MonitorFile(mFileName);
        }
        catch (Exception exp)
        {
          MessageBox.Show("Error saving options: " + exp, "Quail Error", MessageBoxButtons.AbortRetryIgnore);
        }
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      tbFind.Text = "";
      lbLog.ClearSelected();
      lbLog.ClearExistingSelection();
      tbSelected.Text = "";
      lbLog.Invalidate();
    }

    private void GotoLastError()
    {
      if (mAutoPause)
      {
        mPause = true;
        lblPause.Text = "Paused.";
      }
      ShowFind();
      lbLog.ClearSelected();
      lbLog.ClearExistingSelection();
      lbLog.SelectedIndex = mItems.Count - 1;
      tbFind.Text = "\"ERROR";
      if (DoFind(true, false) == false)
      {
        if (mAutoPause)
        {
          mPause = false;
          lblPause.Text = "Running...";
        }
      }
    }

    private void viewLastErrorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GotoLastError();
    }

    private void lblPause_Click(object sender, EventArgs e)
    {
      Pause();
    }

    private void editFileToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      string filename = tbSelected.SelectedText.Trim();
      FileManager.EditFile(filename, mLogEditCommand);
    }

  /*  private void findInFilesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string look = tbSelected.SelectedText.Trim();
      FileManager.FindInFiles(mFindInFilesCommand, "*.*", look);
    }
    */
    /*private void findInFilesspecifyPathToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string look = tbSelected.SelectedText.Trim();
      FindParameters p = new FindParameters();
      p.tbFindWhat.Text = look;
      p.tbLookIn.Text = mFindInFilesCommand;
      p.tbExtensions.Text = "*.*";
      if (p.ShowDialog() == DialogResult.OK)
        FileManager.FindInFiles(p.tbLookIn.Text, p.tbExtensions.Text, p.tbFindWhat.Text);
    }
    */
    private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      CopySelected();
    }

    private void btnSaveGrid_Click(object sender, EventArgs e)
    {
      dgvResults.SelectAll();
      Clipboard.SetDataObject(dgvResults.GetClipboardContent());
      FileManager.GetFileAndSave("xls", Clipboard.GetText());
    }

    private void markToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Mark();
    }

    private void insertMarkToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Mark();
    }

    /// <summary>
    /// Shared method to retrieve integer from currently selected text in window
    /// </summary>
    /// <param name="selectedValue"></param>
    /// <returns></returns>
    private bool GetCurrentlySelectedTextAsInteger(out int selectedValue)
    {
      try
      {
        selectedValue = int.Parse(tbSelected.SelectedText);
        return true;
      }
      catch (Exception)
      {
        string message = string.IsNullOrEmpty(tbSelected.SelectedText) ? "No integer value was selected. Did you select the ID in the copy panel?" : string.Format("Unable to read '{0}' as an integer value.", tbSelected.SelectedText);

        MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

        selectedValue = -1;

        return false;
      }
    }

    private void tbTransparency_Scroll(object sender, EventArgs e)
    {
      Opacity = tbTransparency.Value*.01;
    }

    private void cbBayesian_CheckedChanged(object sender, EventArgs e)
    {
      mBayesian = cbBayesian.Checked;
      if (mBayesian)
      {
        if (DialogResult.Cancel ==
            MessageBox.Show(
              "Turning on the bayesian filter will clear the screen section of your log, and degrade performance.  Is this OK?",
              "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
        {
          cbBayesian.Checked = false;
          mBayesian = cbBayesian.Checked;
          return;
        }
        ClearLog();
        RemoveItemsBasedOnBayesianFilter();
      }
      else
      {
        // Load initial file
        MonitorFile(mFileName);
      }
      lbLog.Invalidate();
    }

    private void mnuTrainHide_Click(object sender, EventArgs e)
    {
      AddToBayesianFilter(null, tbSelected.Text);
    }

    private void mnuTrainShow_Click(object sender, EventArgs e)
    {
      AddToBayesianFilter(tbSelected.Text, null);
    }

    private void cbShowSelected_CheckedChanged(object sender, EventArgs e)
    {
      mShowSelected = cbShowSelected.Checked;
    }

    private void lblClear_Click(object sender, EventArgs e)
    {
      ClearLog();
    }

    private void lblShowErrors_Click(object sender, EventArgs e)
    {
      ShowAllErrors();
    }

    private void showAllErrorsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowAllErrors();
    }

    private void lblShowLastError_Click(object sender, EventArgs e)
    {
      GotoLastError();
    }

    private void cbTimeSlice_CheckedChanged(object sender, EventArgs e)
    {
      if (cbTimeSlice.Checked)
      {
        RemoveItemsBasedOnTimeSlice();
      }
      else
      {
        if (DialogResult.OK ==
            MessageBox.Show(
              "Do you want to reload data you previously filtered out?  Note:  Either way you will have to re-enable your time slice when loading is complete.",
              "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
        {
          MonitorFile(mFileName);
        }
      }

      lbLog.Invalidate();
    }

    private void dtTimeSliceStart_ValueChanged(object sender, EventArgs e)
    {
      cbTimeSlice.Checked = false;
    }

    private void dtTimeSliceEnd_ValueChanged(object sender, EventArgs e)
    {
      cbTimeSlice.Checked = false;
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ctxMenuLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.insertMarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTrainHide = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrainShow = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuSelected = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.editFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectionTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnFindPrevious = new System.Windows.Forms.Button();
            this.lblLoop = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnFindAll = new System.Windows.Forms.Button();
            this.btnCloseFind = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFilterString = new System.Windows.Forms.TextBox();
            this.cbFilterOn = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblShowLastError = new System.Windows.Forms.Label();
            this.lblShowErrors = new System.Windows.Forms.Label();
            this.lblClear = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.lblPause = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.lbLog = new QuailControls.ColorListBox();
            this.splitContainerBottom = new System.Windows.Forms.SplitContainer();
            this.tbSelected = new System.Windows.Forms.TextBox();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.tabPageQuery = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSaveGrid = new System.Windows.Forms.Button();
            this.btnSaveSQL = new System.Windows.Forms.Button();
            this.btnOpenSQL = new System.Windows.Forms.Button();
            this.lblNotes = new System.Windows.Forms.Label();
            this.cbQueryType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExecuteQuery = new System.Windows.Forms.Button();
            this.splitterQueryResults = new System.Windows.Forms.Splitter();
            this.panelQueryResults = new System.Windows.Forms.Panel();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.tabAdvancedOptions = new System.Windows.Forms.TabPage();
            this.panelBayesianFilter = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cbTimeSlice = new System.Windows.Forms.CheckBox();
            this.dtTimeSliceEnd = new System.Windows.Forms.DateTimePicker();
            this.dtTimeSliceStart = new System.Windows.Forms.DateTimePicker();
            this.cbShowSelected = new System.Windows.Forms.CheckBox();
            this.lblBayesionScore = new System.Windows.Forms.Label();
            this.cbBayesian = new System.Windows.Forms.CheckBox();
            this.panelTransparency = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panelSlider = new System.Windows.Forms.Panel();
            this.tbTransparency = new System.Windows.Forms.TrackBar();
            this.tabPageHelp = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.markToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.editFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.findAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLastErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.transparencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileTop3rdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileBottom3rdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileBottomHalfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctxMenuLog.SuspendLayout();
            this.ctxMenuSelected.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBottom)).BeginInit();
            this.splitContainerBottom.Panel1.SuspendLayout();
            this.splitContainerBottom.Panel2.SuspendLayout();
            this.splitContainerBottom.SuspendLayout();
            this.tabPageQuery.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelQueryResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.tabAdvancedOptions.SuspendLayout();
            this.panelBayesianFilter.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panelTransparency.SuspendLayout();
            this.panelSlider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparency)).BeginInit();
            this.tabPageHelp.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxMenuLog
            // 
            this.ctxMenuLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemCopy,
            this.toolStripMenuItem2,
            this.toolStripSeparator15,
            this.insertMarkToolStripMenuItem,
            this.toolStripSeparator9,
            this.toolStripMenuItem3,
            this.toolStripSeparator10,
            this.toolStripMenuItem4,
            this.toolStripSeparator11,
            this.toolStripMenuItem5,
            this.toolStripSeparator21,
            this.mnuTrainHide,
            this.mnuTrainShow});
            this.ctxMenuLog.Name = "ctxMenuLog";
            this.ctxMenuLog.Size = new System.Drawing.Size(304, 210);
            // 
            // ctxMenuItemCopy
            // 
            this.ctxMenuItemCopy.Name = "ctxMenuItemCopy";
            this.ctxMenuItemCopy.Size = new System.Drawing.Size(303, 22);
            this.ctxMenuItemCopy.Text = "Copy";
            this.ctxMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(303, 22);
            this.toolStripMenuItem2.Text = "Copy Without Blanks";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(300, 6);
            // 
            // insertMarkToolStripMenuItem
            // 
            this.insertMarkToolStripMenuItem.Name = "insertMarkToolStripMenuItem";
            this.insertMarkToolStripMenuItem.Size = new System.Drawing.Size(303, 22);
            this.insertMarkToolStripMenuItem.Text = "Insert &Mark";
            this.insertMarkToolStripMenuItem.Click += new System.EventHandler(this.insertMarkToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(300, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(303, 22);
            this.toolStripMenuItem3.Text = "Clear";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(300, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(303, 22);
            this.toolStripMenuItem4.Text = "Toggle Pause";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(300, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(303, 22);
            this.toolStripMenuItem5.Text = "View Last Error";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.viewLastErrorToolStripMenuItem_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(300, 6);
            // 
            // mnuTrainHide
            // 
            this.mnuTrainHide.Name = "mnuTrainHide";
            this.mnuTrainHide.Size = new System.Drawing.Size(303, 22);
            this.mnuTrainHide.Text = "Train to Hide Selected Text (Bayesian Filter)";
            this.mnuTrainHide.Click += new System.EventHandler(this.mnuTrainHide_Click);
            // 
            // mnuTrainShow
            // 
            this.mnuTrainShow.Name = "mnuTrainShow";
            this.mnuTrainShow.Size = new System.Drawing.Size(303, 22);
            this.mnuTrainShow.Text = "Train to Show Selected Text (Bayesian Filter)";
            this.mnuTrainShow.Click += new System.EventHandler(this.mnuTrainShow_Click);
            // 
            // ctxMenuSelected
            // 
            this.ctxMenuSelected.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1,
            this.toolStripSeparator8,
            this.editFileToolStripMenuItem1});
            this.ctxMenuSelected.Name = "ctxMenuSelected";
            this.ctxMenuSelected.Size = new System.Drawing.Size(116, 54);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.copyToolStripMenuItem1.Text = "Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(112, 6);
            // 
            // editFileToolStripMenuItem1
            // 
            this.editFileToolStripMenuItem1.Name = "editFileToolStripMenuItem1";
            this.editFileToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.editFileToolStripMenuItem1.Text = "Edit File";
            this.editFileToolStripMenuItem1.Click += new System.EventHandler(this.editFileToolStripMenuItem1_Click);
            // 
            // SelectionTimer
            // 
            this.SelectionTimer.Interval = 500;
            this.SelectionTimer.Tick += new System.EventHandler(this.SelectionTimer_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.toolStripSeparator16,
            this.toolStripMenuItem12});
            this.contextMenuStrip1.Name = "ctxMenuSelected";
            this.contextMenuStrip1.Size = new System.Drawing.Size(116, 54);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem6.Text = "Copy";
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(112, 6);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem12.Text = "Edit File";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.BackColor = System.Drawing.Color.Red;
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(727, 428);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnFindPrevious);
            this.panel1.Controls.Add(this.lblLoop);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.btnFindAll);
            this.panel1.Controls.Add(this.btnCloseFind);
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.tbFind);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 379);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1026, 28);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(358, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(41, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnFindPrevious
            // 
            this.btnFindPrevious.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindPrevious.Location = new System.Drawing.Point(214, 3);
            this.btnFindPrevious.Name = "btnFindPrevious";
            this.btnFindPrevious.Size = new System.Drawing.Size(57, 23);
            this.btnFindPrevious.TabIndex = 7;
            this.btnFindPrevious.Text = "Previous";
            this.btnFindPrevious.Click += new System.EventHandler(this.btnFindPrevious_Click);
            // 
            // lblLoop
            // 
            this.lblLoop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoop.ForeColor = System.Drawing.Color.Navy;
            this.lblLoop.Location = new System.Drawing.Point(595, 8);
            this.lblLoop.Name = "lblLoop";
            this.lblLoop.Size = new System.Drawing.Size(184, 23);
            this.lblLoop.TabIndex = 6;
            this.lblLoop.Text = "Loop";
            this.lblLoop.Visible = false;
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(170, 3);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(38, 23);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "Next";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnFindAll
            // 
            this.btnFindAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindAll.Location = new System.Drawing.Point(277, 3);
            this.btnFindAll.Name = "btnFindAll";
            this.btnFindAll.Size = new System.Drawing.Size(75, 23);
            this.btnFindAll.TabIndex = 4;
            this.btnFindAll.Text = "Highlight All";
            this.btnFindAll.Click += new System.EventHandler(this.btnFindAll_Click);
            this.btnFindAll.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnFindAll_KeyDown);
            // 
            // btnCloseFind
            // 
            this.btnCloseFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseFind.ForeColor = System.Drawing.Color.Red;
            this.btnCloseFind.Location = new System.Drawing.Point(3, 5);
            this.btnCloseFind.Name = "btnCloseFind";
            this.btnCloseFind.Size = new System.Drawing.Size(21, 20);
            this.btnCloseFind.TabIndex = 3;
            this.btnCloseFind.Text = "X";
            this.btnCloseFind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCloseFind.Click += new System.EventHandler(this.btnCloseFind_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(571, 8);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(67, 13);
            this.lblError.TabIndex = 2;
            this.lblError.Text = "Search Error";
            this.lblError.Visible = false;
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(64, 5);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(100, 20);
            this.tbFind.TabIndex = 1;
            this.tbFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFind_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find:";
            // 
            // tbFilterString
            // 
            this.tbFilterString.Location = new System.Drawing.Point(62, 32);
            this.tbFilterString.Name = "tbFilterString";
            this.tbFilterString.Size = new System.Drawing.Size(100, 20);
            this.tbFilterString.TabIndex = 10;
            this.toolTip1.SetToolTip(this.tbFilterString, "Regex can be used in filter...");
            this.tbFilterString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFilterString_KeyDown);
            // 
            // cbFilterOn
            // 
            this.cbFilterOn.AutoSize = true;
            this.cbFilterOn.Location = new System.Drawing.Point(12, 32);
            this.cbFilterOn.Name = "cbFilterOn";
            this.cbFilterOn.Size = new System.Drawing.Size(51, 17);
            this.cbFilterOn.TabIndex = 9;
            this.cbFilterOn.Text = "Filter:";
            this.cbFilterOn.UseVisualStyleBackColor = true;
            this.cbFilterOn.CheckedChanged += new System.EventHandler(this.cbFilterOn_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1026, 379);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblShowLastError);
            this.panel3.Controls.Add(this.lblShowErrors);
            this.panel3.Controls.Add(this.lblClear);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.lblPause);
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Controls.Add(this.menuStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1026, 379);
            this.panel3.TabIndex = 2;
            // 
            // lblShowLastError
            // 
            this.lblShowLastError.AutoSize = true;
            this.lblShowLastError.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblShowLastError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShowLastError.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowLastError.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblShowLastError.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowLastError.Location = new System.Drawing.Point(740, 24);
            this.lblShowLastError.MinimumSize = new System.Drawing.Size(60, 15);
            this.lblShowLastError.Name = "lblShowLastError";
            this.lblShowLastError.Size = new System.Drawing.Size(85, 15);
            this.lblShowLastError.TabIndex = 11;
            this.lblShowLastError.Text = "Show Last Error";
            this.lblShowLastError.Click += new System.EventHandler(this.lblShowLastError_Click);
            // 
            // lblShowErrors
            // 
            this.lblShowErrors.AutoSize = true;
            this.lblShowErrors.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblShowErrors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShowErrors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowErrors.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblShowErrors.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowErrors.Location = new System.Drawing.Point(825, 24);
            this.lblShowErrors.MinimumSize = new System.Drawing.Size(60, 15);
            this.lblShowErrors.Name = "lblShowErrors";
            this.lblShowErrors.Size = new System.Drawing.Size(81, 15);
            this.lblShowErrors.TabIndex = 10;
            this.lblShowErrors.Text = "Show All Errors";
            this.lblShowErrors.Click += new System.EventHandler(this.lblShowErrors_Click);
            // 
            // lblClear
            // 
            this.lblClear.AutoSize = true;
            this.lblClear.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblClear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblClear.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClear.Location = new System.Drawing.Point(906, 24);
            this.lblClear.MinimumSize = new System.Drawing.Size(60, 15);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(60, 15);
            this.lblClear.TabIndex = 9;
            this.lblClear.Text = "Clear Log";
            this.lblClear.Click += new System.EventHandler(this.lblClear_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnApplyFilter);
            this.panel6.Controls.Add(this.tbFilterString);
            this.panel6.Controls.Add(this.cbFilterOn);
            this.panel6.Location = new System.Drawing.Point(251, -8);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(483, 54);
            this.panel6.TabIndex = 8;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Location = new System.Drawing.Point(166, 31);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(29, 23);
            this.btnApplyFilter.TabIndex = 11;
            this.btnApplyFilter.Text = "Go";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // lblPause
            // 
            this.lblPause.AutoSize = true;
            this.lblPause.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblPause.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPause.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPause.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPause.Location = new System.Drawing.Point(966, 24);
            this.lblPause.MinimumSize = new System.Drawing.Size(60, 15);
            this.lblPause.Name = "lblPause";
            this.lblPause.Size = new System.Drawing.Size(60, 15);
            this.lblPause.TabIndex = 3;
            this.lblPause.Text = "Running...";
            this.lblPause.Click += new System.EventHandler(this.lblPause_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLog);
            this.tabControl1.Controls.Add(this.tabPageQuery);
            this.tabControl1.Controls.Add(this.tabAdvancedOptions);
            this.tabControl1.Controls.Add(this.tabPageHelp);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1026, 355);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.splitContainerMain);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(1018, 329);
            this.tabPageLog.TabIndex = 0;
            this.tabPageLog.Text = "Log View";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Location = new System.Drawing.Point(8, 31);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerMain.Panel1.Controls.Add(this.lbLog);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerBottom);
            this.splitContainerMain.Size = new System.Drawing.Size(248, 226);
            this.splitContainerMain.SplitterDistance = 142;
            this.splitContainerMain.TabIndex = 2;
            // 
            // lbLog
            // 
            this.lbLog.AllowDrop = true;
            this.lbLog.ContextMenuStrip = this.ctxMenuLog;
            this.lbLog.Count = 1;
            this.lbLog.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbLog.Font = new System.Drawing.Font("Courier New", 10F);
            this.lbLog.IsHighlighted = false;
            this.lbLog.ItemHeight = 16;
            this.lbLog.Location = new System.Drawing.Point(31, 26);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbLog.Size = new System.Drawing.Size(165, 52);
            this.lbLog.TabIndex = 0;
            this.lbLog.TextColor = System.Drawing.Color.Black;
            this.lbLog.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbLog_DrawItem);
            this.lbLog.SelectedIndexChanged += new System.EventHandler(this.lbLog_SelectedIndexChanged);
            this.lbLog.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbLog_DragDrop);
            this.lbLog.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbLog_DragEnter);
            this.lbLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbLog_KeyDown);
            this.lbLog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbLog_KeyPress);
            // 
            // splitContainerBottom
            // 
            this.splitContainerBottom.Location = new System.Drawing.Point(26, 14);
            this.splitContainerBottom.Name = "splitContainerBottom";
            // 
            // splitContainerBottom.Panel1
            // 
            this.splitContainerBottom.Panel1.Controls.Add(this.tbSelected);
            this.splitContainerBottom.Panel1MinSize = 50;
            // 
            // splitContainerBottom.Panel2
            // 
            this.splitContainerBottom.Panel2.Controls.Add(this.lstResults);
            this.splitContainerBottom.Panel2MinSize = 50;
            this.splitContainerBottom.Size = new System.Drawing.Size(208, 55);
            this.splitContainerBottom.SplitterDistance = 128;
            this.splitContainerBottom.TabIndex = 0;
            // 
            // tbSelected
            // 
            this.tbSelected.BackColor = System.Drawing.SystemColors.Control;
            this.tbSelected.ContextMenuStrip = this.ctxMenuSelected;
            this.tbSelected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbSelected.Location = new System.Drawing.Point(16, 13);
            this.tbSelected.Multiline = true;
            this.tbSelected.Name = "tbSelected";
            this.tbSelected.ReadOnly = true;
            this.tbSelected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSelected.Size = new System.Drawing.Size(107, 28);
            this.tbSelected.TabIndex = 1;
            this.tbSelected.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSelected_KeyDown);
            this.tbSelected.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSelected_KeyUp);
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Location = new System.Drawing.Point(25, 13);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(36, 30);
            this.lstResults.TabIndex = 0;
            // 
            // tabPageQuery
            // 
            this.tabPageQuery.Controls.Add(this.panel5);
            this.tabPageQuery.Controls.Add(this.panel4);
            this.tabPageQuery.Controls.Add(this.splitterQueryResults);
            this.tabPageQuery.Controls.Add(this.panelQueryResults);
            this.tabPageQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuery.Name = "tabPageQuery";
            this.tabPageQuery.Size = new System.Drawing.Size(1018, 329);
            this.tabPageQuery.TabIndex = 1;
            this.tabPageQuery.Text = "Query";
            this.tabPageQuery.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tbQuery);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(882, 206);
            this.panel5.TabIndex = 4;
            // 
            // tbQuery
            // 
            this.tbQuery.BackColor = System.Drawing.Color.Black;
            this.tbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQuery.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQuery.ForeColor = System.Drawing.Color.White;
            this.tbQuery.Location = new System.Drawing.Point(0, 0);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbQuery.Size = new System.Drawing.Size(882, 206);
            this.tbQuery.TabIndex = 0;
            this.tbQuery.WordWrap = false;
            this.tbQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbQuery_KeyDown);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSaveGrid);
            this.panel4.Controls.Add(this.btnSaveSQL);
            this.panel4.Controls.Add(this.btnOpenSQL);
            this.panel4.Controls.Add(this.lblNotes);
            this.panel4.Controls.Add(this.cbQueryType);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.btnExecuteQuery);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(882, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(136, 206);
            this.panel4.TabIndex = 3;
            // 
            // btnSaveGrid
            // 
            this.btnSaveGrid.Location = new System.Drawing.Point(8, 109);
            this.btnSaveGrid.Name = "btnSaveGrid";
            this.btnSaveGrid.Size = new System.Drawing.Size(120, 23);
            this.btnSaveGrid.TabIndex = 7;
            this.btnSaveGrid.Text = "Save Results";
            this.btnSaveGrid.UseVisualStyleBackColor = true;
            this.btnSaveGrid.Click += new System.EventHandler(this.btnSaveGrid_Click);
            // 
            // btnSaveSQL
            // 
            this.btnSaveSQL.Location = new System.Drawing.Point(80, 8);
            this.btnSaveSQL.Name = "btnSaveSQL";
            this.btnSaveSQL.Size = new System.Drawing.Size(48, 23);
            this.btnSaveSQL.TabIndex = 6;
            this.btnSaveSQL.Text = "Save";
            this.btnSaveSQL.Click += new System.EventHandler(this.btnSaveSQL_Click);
            // 
            // btnOpenSQL
            // 
            this.btnOpenSQL.Location = new System.Drawing.Point(8, 8);
            this.btnOpenSQL.Name = "btnOpenSQL";
            this.btnOpenSQL.Size = new System.Drawing.Size(48, 23);
            this.btnOpenSQL.TabIndex = 5;
            this.btnOpenSQL.Text = "Open";
            this.btnOpenSQL.Click += new System.EventHandler(this.btnOpenSQL_Click);
            // 
            // lblNotes
            // 
            this.lblNotes.ForeColor = System.Drawing.Color.Navy;
            this.lblNotes.Location = new System.Drawing.Point(9, 135);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(120, 106);
            this.lblNotes.TabIndex = 4;
            // 
            // cbQueryType
            // 
            this.cbQueryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueryType.Items.AddRange(new object[] {
            "IIS Log",
            "W3C Log",
            "Event Log",
            "Registry",
            "File System",
            "Text File",
            "CSV File"});
            this.cbQueryType.Location = new System.Drawing.Point(8, 56);
            this.cbQueryType.Name = "cbQueryType";
            this.cbQueryType.Size = new System.Drawing.Size(121, 21);
            this.cbQueryType.TabIndex = 3;
            this.cbQueryType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbQueryType_KeyDown);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Query Type:";
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Location = new System.Drawing.Point(8, 80);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new System.Drawing.Size(120, 23);
            this.btnExecuteQuery.TabIndex = 0;
            this.btnExecuteQuery.Text = "Execute Query (F5)";
            this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
            this.btnExecuteQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnExecuteQuery_KeyDown);
            // 
            // splitterQueryResults
            // 
            this.splitterQueryResults.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitterQueryResults.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterQueryResults.Location = new System.Drawing.Point(0, 206);
            this.splitterQueryResults.Name = "splitterQueryResults";
            this.splitterQueryResults.Size = new System.Drawing.Size(1018, 3);
            this.splitterQueryResults.TabIndex = 2;
            this.splitterQueryResults.TabStop = false;
            // 
            // panelQueryResults
            // 
            this.panelQueryResults.Controls.Add(this.dgvResults);
            this.panelQueryResults.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelQueryResults.Location = new System.Drawing.Point(0, 209);
            this.panelQueryResults.Name = "panelQueryResults";
            this.panelQueryResults.Size = new System.Drawing.Size(1018, 120);
            this.panelQueryResults.TabIndex = 1;
            // 
            // dgvResults
            // 
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(1018, 120);
            this.dgvResults.TabIndex = 1;
            // 
            // tabAdvancedOptions
            // 
            this.tabAdvancedOptions.Controls.Add(this.panelBayesianFilter);
            this.tabAdvancedOptions.Location = new System.Drawing.Point(4, 22);
            this.tabAdvancedOptions.Name = "tabAdvancedOptions";
            this.tabAdvancedOptions.Size = new System.Drawing.Size(1018, 329);
            this.tabAdvancedOptions.TabIndex = 3;
            this.tabAdvancedOptions.Text = "Advanced Options";
            this.tabAdvancedOptions.UseVisualStyleBackColor = true;
            // 
            // panelBayesianFilter
            // 
            this.panelBayesianFilter.Controls.Add(this.panel7);
            this.panelBayesianFilter.Controls.Add(this.cbShowSelected);
            this.panelBayesianFilter.Controls.Add(this.lblBayesionScore);
            this.panelBayesianFilter.Controls.Add(this.cbBayesian);
            this.panelBayesianFilter.Controls.Add(this.panelTransparency);
            this.panelBayesianFilter.Location = new System.Drawing.Point(21, 25);
            this.panelBayesianFilter.Name = "panelBayesianFilter";
            this.panelBayesianFilter.Size = new System.Drawing.Size(988, 302);
            this.panelBayesianFilter.TabIndex = 3;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cbTimeSlice);
            this.panel7.Controls.Add(this.dtTimeSliceEnd);
            this.panel7.Controls.Add(this.dtTimeSliceStart);
            this.panel7.Location = new System.Drawing.Point(-7, 31);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(501, 22);
            this.panel7.TabIndex = 4;
            // 
            // cbTimeSlice
            // 
            this.cbTimeSlice.AutoSize = true;
            this.cbTimeSlice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTimeSlice.Location = new System.Drawing.Point(10, 2);
            this.cbTimeSlice.Name = "cbTimeSlice";
            this.cbTimeSlice.Size = new System.Drawing.Size(89, 17);
            this.cbTimeSlice.TabIndex = 6;
            this.cbTimeSlice.Text = "Time Slice:";
            this.cbTimeSlice.UseVisualStyleBackColor = true;
            this.cbTimeSlice.CheckedChanged += new System.EventHandler(this.cbTimeSlice_CheckedChanged);
            // 
            // dtTimeSliceEnd
            // 
            this.dtTimeSliceEnd.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.dtTimeSliceEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTimeSliceEnd.Location = new System.Drawing.Point(240, 0);
            this.dtTimeSliceEnd.Name = "dtTimeSliceEnd";
            this.dtTimeSliceEnd.Size = new System.Drawing.Size(137, 21);
            this.dtTimeSliceEnd.TabIndex = 5;
            this.dtTimeSliceEnd.ValueChanged += new System.EventHandler(this.dtTimeSliceEnd_ValueChanged);
            // 
            // dtTimeSliceStart
            // 
            this.dtTimeSliceStart.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.dtTimeSliceStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTimeSliceStart.Location = new System.Drawing.Point(97, 0);
            this.dtTimeSliceStart.Name = "dtTimeSliceStart";
            this.dtTimeSliceStart.Size = new System.Drawing.Size(137, 21);
            this.dtTimeSliceStart.TabIndex = 4;
            this.dtTimeSliceStart.ValueChanged += new System.EventHandler(this.dtTimeSliceStart_ValueChanged);
            // 
            // cbShowSelected
            // 
            this.cbShowSelected.AutoSize = true;
            this.cbShowSelected.Location = new System.Drawing.Point(133, 4);
            this.cbShowSelected.Name = "cbShowSelected";
            this.cbShowSelected.Size = new System.Drawing.Size(15, 14);
            this.cbShowSelected.TabIndex = 8;
            this.cbShowSelected.UseVisualStyleBackColor = true;
            this.cbShowSelected.CheckedChanged += new System.EventHandler(this.cbShowSelected_CheckedChanged);
            // 
            // lblBayesionScore
            // 
            this.lblBayesionScore.AutoSize = true;
            this.lblBayesionScore.Location = new System.Drawing.Point(146, 4);
            this.lblBayesionScore.Name = "lblBayesionScore";
            this.lblBayesionScore.Size = new System.Drawing.Size(96, 13);
            this.lblBayesionScore.TabIndex = 7;
            this.lblBayesionScore.Text = "Selected Score:  --";
            // 
            // cbBayesian
            // 
            this.cbBayesian.AutoSize = true;
            this.cbBayesian.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBayesian.Location = new System.Drawing.Point(3, 3);
            this.cbBayesian.Name = "cbBayesian";
            this.cbBayesian.Size = new System.Drawing.Size(109, 17);
            this.cbBayesian.TabIndex = 0;
            this.cbBayesian.Text = "Bayesian Filter";
            this.cbBayesian.UseVisualStyleBackColor = true;
            this.cbBayesian.CheckedChanged += new System.EventHandler(this.cbBayesian_CheckedChanged);
            // 
            // panelTransparency
            // 
            this.panelTransparency.Controls.Add(this.label3);
            this.panelTransparency.Controls.Add(this.panelSlider);
            this.panelTransparency.Location = new System.Drawing.Point(3, 71);
            this.panelTransparency.Name = "panelTransparency";
            this.panelTransparency.Size = new System.Drawing.Size(227, 20);
            this.panelTransparency.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Transparency:";
            // 
            // panelSlider
            // 
            this.panelSlider.Controls.Add(this.tbTransparency);
            this.panelSlider.Location = new System.Drawing.Point(90, 3);
            this.panelSlider.Name = "panelSlider";
            this.panelSlider.Size = new System.Drawing.Size(133, 19);
            this.panelSlider.TabIndex = 3;
            // 
            // tbTransparency
            // 
            this.tbTransparency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTransparency.Location = new System.Drawing.Point(0, 0);
            this.tbTransparency.Maximum = 100;
            this.tbTransparency.Minimum = 15;
            this.tbTransparency.Name = "tbTransparency";
            this.tbTransparency.Size = new System.Drawing.Size(133, 19);
            this.tbTransparency.TabIndex = 0;
            this.tbTransparency.TickFrequency = 10;
            this.tbTransparency.Value = 100;
            this.tbTransparency.Scroll += new System.EventHandler(this.tbTransparency_Scroll);
            // 
            // tabPageHelp
            // 
            this.tabPageHelp.Controls.Add(this.webBrowser1);
            this.tabPageHelp.Location = new System.Drawing.Point(4, 22);
            this.tabPageHelp.Name = "tabPageHelp";
            this.tabPageHelp.Size = new System.Drawing.Size(1018, 329);
            this.tabPageHelp.TabIndex = 2;
            this.tabPageHelp.Text = "Help";
            this.tabPageHelp.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1018, 329);
            this.webBrowser1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1026, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openDefaultToolStripMenuItem,
            this.toolStripSeparator2,
            this.mnuRecentFiles,
            this.toolStripSeparator13,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openDefaultToolStripMenuItem
            // 
            this.openDefaultToolStripMenuItem.Name = "openDefaultToolStripMenuItem";
            this.openDefaultToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.openDefaultToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openDefaultToolStripMenuItem.Text = "Open &Default";
            this.openDefaultToolStripMenuItem.Click += new System.EventHandler(this.openDefaultToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuRecentFiles
            // 
            this.mnuRecentFiles.Name = "mnuRecentFiles";
            this.mnuRecentFiles.Size = new System.Drawing.Size(184, 22);
            this.mnuRecentFiles.Text = "Recent Files";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(181, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem,
            this.toolStripSeparator4,
            this.markToolStripMenuItem,
            this.toolStripSeparator14,
            this.editFileToolStripMenuItem,
            this.toolStripSeparator6,
            this.findAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem1.Text = "&Copy";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.copyToolStripMenuItem.Text = "Copy Without &Blanks";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(225, 6);
            // 
            // markToolStripMenuItem
            // 
            this.markToolStripMenuItem.Name = "markToolStripMenuItem";
            this.markToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.markToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.markToolStripMenuItem.Text = "Insert &Mark";
            this.markToolStripMenuItem.Click += new System.EventHandler(this.markToolStripMenuItem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(225, 6);
            // 
            // editFileToolStripMenuItem
            // 
            this.editFileToolStripMenuItem.Name = "editFileToolStripMenuItem";
            this.editFileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.editFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.editFileToolStripMenuItem.Text = "&Edit Log";
            this.editFileToolStripMenuItem.Click += new System.EventHandler(this.editFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(225, 6);
            // 
            // findAllToolStripMenuItem
            // 
            this.findAllToolStripMenuItem.Name = "findAllToolStripMenuItem";
            this.findAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findAllToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.findAllToolStripMenuItem.Text = "&Find";
            this.findAllToolStripMenuItem.Click += new System.EventHandler(this.findAllToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewLastErrorToolStripMenuItem,
            this.showAllErrorsToolStripMenuItem,
            this.toolStripSeparator5,
            this.pauseToolStripMenuItem,
            this.toolStripSeparator7,
            this.transparencyToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearToolStripMenuItem,
            this.toolStripSeparator20,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // viewLastErrorToolStripMenuItem
            // 
            this.viewLastErrorToolStripMenuItem.Name = "viewLastErrorToolStripMenuItem";
            this.viewLastErrorToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.viewLastErrorToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.viewLastErrorToolStripMenuItem.Text = "View Last &Error";
            this.viewLastErrorToolStripMenuItem.Click += new System.EventHandler(this.viewLastErrorToolStripMenuItem_Click);
            // 
            // showAllErrorsToolStripMenuItem
            // 
            this.showAllErrorsToolStripMenuItem.Name = "showAllErrorsToolStripMenuItem";
            this.showAllErrorsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.showAllErrorsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.showAllErrorsToolStripMenuItem.Text = "Show All Errors";
            this.showAllErrorsToolStripMenuItem.Click += new System.EventHandler(this.showAllErrorsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(221, 6);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.pauseToolStripMenuItem.Text = "Toggle &Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(221, 6);
            // 
            // transparencyToolStripMenuItem
            // 
            this.transparencyToolStripMenuItem.Name = "transparencyToolStripMenuItem";
            this.transparencyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.transparencyToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.transparencyToolStripMenuItem.Text = "&Toggle Transparency";
            this.transparencyToolStripMenuItem.Click += new System.EventHandler(this.transparencyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.clearToolStripMenuItem.Text = "&Clear (screen only)";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(221, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileTop3rdToolStripMenuItem,
            this.tileToolStripMenuItem,
            this.tileBottom3rdToolStripMenuItem,
            this.tileBottomHalfToolStripMenuItem,
            this.fullscreenToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // tileTop3rdToolStripMenuItem
            // 
            this.tileTop3rdToolStripMenuItem.Name = "tileTop3rdToolStripMenuItem";
            this.tileTop3rdToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.tileTop3rdToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.tileTop3rdToolStripMenuItem.Text = "Tile Top 3rd";
            this.tileTop3rdToolStripMenuItem.Click += new System.EventHandler(this.tileTop3rdToolStripMenuItem_Click);
            // 
            // tileToolStripMenuItem
            // 
            this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
            this.tileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.tileToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.tileToolStripMenuItem.Text = "Tile Top Half";
            this.tileToolStripMenuItem.Click += new System.EventHandler(this.tileToolStripMenuItem_Click);
            // 
            // tileBottom3rdToolStripMenuItem
            // 
            this.tileBottom3rdToolStripMenuItem.Name = "tileBottom3rdToolStripMenuItem";
            this.tileBottom3rdToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.tileBottom3rdToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.tileBottom3rdToolStripMenuItem.Text = "Tile Bottom 3rd";
            this.tileBottom3rdToolStripMenuItem.Click += new System.EventHandler(this.tileBottom3rdToolStripMenuItem_Click);
            // 
            // tileBottomHalfToolStripMenuItem
            // 
            this.tileBottomHalfToolStripMenuItem.Name = "tileBottomHalfToolStripMenuItem";
            this.tileBottomHalfToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.tileBottomHalfToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.tileBottomHalfToolStripMenuItem.Text = "Tile Bottom Half";
            this.tileBottomHalfToolStripMenuItem.Click += new System.EventHandler(this.tileBottomHalfToolStripMenuItem_Click);
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.fullscreenToolStripMenuItem.Text = "&Fullscreen";
            this.fullscreenToolStripMenuItem.Click += new System.EventHandler(this.fullscreenToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1026, 407);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1026, 407);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Tip:";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1026, 407);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Quail";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ctxMenuLog.ResumeLayout(false);
            this.ctxMenuSelected.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerBottom.Panel1.ResumeLayout(false);
            this.splitContainerBottom.Panel1.PerformLayout();
            this.splitContainerBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBottom)).EndInit();
            this.splitContainerBottom.ResumeLayout(false);
            this.tabPageQuery.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panelQueryResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.tabAdvancedOptions.ResumeLayout(false);
            this.panelBayesianFilter.ResumeLayout(false);
            this.panelBayesianFilter.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panelTransparency.ResumeLayout(false);
            this.panelTransparency.PerformLayout();
            this.panelSlider.ResumeLayout(false);
            this.panelSlider.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparency)).EndInit();
            this.tabPageHelp.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private void cbFilterOn_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void btnApplyFilter_Click(object sender, EventArgs e)
    {
      Cursor = Cursors.WaitCursor;
      cbFilterOn.Checked = true;
      MonitorFile(mFileName);
      Cursor = Cursors.Default;
    }

    private void tbFilterString_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        btnApplyFilter_Click(sender, e); 
        e.Handled = true;
      }  
    }

  }
}