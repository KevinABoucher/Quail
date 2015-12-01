using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace QuailControls
{
    /// <summary>
    /// Virtual Color ListBox - This is what makes Quail's scrolling so fast.
    /// </summary>
    public class ColorListBox : System.Windows.Forms.ListBox
	{
    // Listbox Styles
    private const int LBS_NOTIFY            = 0x0001;
    private const int LBS_SORT              = 0x0002;
    private const int LBS_NOREDRAW          = 0x0004;
    private const int LBS_MULTIPLESEL       = 0x0008;
    private const int LBS_OWNERDRAWFIXED    = 0x0010;
    private const int LBS_OWNERDRAWVARIABLE = 0x0020;
    private const int LBS_HASSTRINGS        = 0x0040;
    private const int LBS_USETABSTOPS       = 0x0080;
    private const int LBS_NOINTEGRALHEIGHT  = 0x0100;
    private const int LBS_MULTICOLUMN       = 0x0200;
    private const int LBS_WANTKEYBOARDINPUT = 0x0400;
    private const int LBS_EXTENDEDSEL       = 0x0800;
    private const int LBS_DISABLENOSCROLL   = 0x1000;
    private const int LBS_NODATA            = 0x2000;

    private const int LB_GETCOUNT           = 0x018B;
    private const int LB_SETCOUNT           = 0x01A7;

    private const int LB_SETSEL             = 0x0185;
    private const int LB_SETCURSEL          = 0x0186;
    private const int LB_GETSEL             = 0x0187;
    private const int LB_GETCURSEL          = 0x0188;
    private const int LB_GETSELCOUNT        = 0x0190;
    private const int LB_GETSELITEMS        = 0x0191;

    private const int SB_LINEUP		      	= 0; // Scrolls one line up
    private const int SB_LINELEFT			= 0; // Scrolls one cell left
    private const int SB_LINEDOWN			= 1; // Scrolls one line down
    private const int WM_ERASEBKGND			= 0x0014;
    private const int WM_MOUSEWHEEL         = 0x020A;
    private const int WM_VSCROLL            = 0x0115;
    private const int WM_KEYDOWN            = 0x0100;
    
    [DllImport("user32.dll",CharSet=CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int wMsg,IntPtr wParam, IntPtr lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private extern static int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

    private int mSelectedIndex = -1;
    public ColorListBox.SelectedIndexCollection mSelectedIndices = null;

	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.Container components = null;

    public string Line = null;
    public bool mIsHighlighted = false;
    public bool IsHighlighted
    {
      set { mIsHighlighted = value; }
      get { return mIsHighlighted; }
    }

    private Color mTextColor = Color.Black;
    public Color TextColor
    {
      set { mTextColor = value; }
      get { return mTextColor; }
    }

    public ColorListBox()
    {
        // This call is required by the Windows.Forms Form Designer.
        InitializeComponent();

        // Enable Double Buffering to remove flicker
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.UserPaint, true);

        mSelectedIndices = new ColorListBox.SelectedIndexCollection(this);

        this.DrawMode = DrawMode.OwnerDrawFixed;
        this.Font = new Font("Courier New", 10);
        this.ItemHeight = 16;
    }

    private void GetXY(IntPtr Param, out int X, out int Y)
    {
      byte[] byts = System.BitConverter.GetBytes((int)Param);
      X = BitConverter.ToInt16(byts, 0);
      Y = BitConverter.ToInt16(byts, 2);
    }
	
    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case (int)WM_MOUSEWHEEL:

          int X;
          int Y;

          GetXY(m.WParam, out X, out Y);

          if (Y >0)
          {
            SendMessage(this.Handle, (int)WM_VSCROLL,(IntPtr)SB_LINEUP,IntPtr.Zero);
          }
          else
          {
            SendMessage(this.Handle, (int)WM_VSCROLL,(IntPtr)SB_LINEDOWN,IntPtr.Zero);
          }
          return;
      }

      base.WndProc (ref m);
    }

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if( components != null )
				components.Dispose();
		}
		base.Dispose( disposing );
	}

	#region Component Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		components = new System.ComponentModel.Container();
	}
    #endregion

    /// <summary>
    /// Draw visible list items
    /// </summary>
    /// <param name="pe"></param>
    protected override void OnPaint(PaintEventArgs pe)
    {
        int curCount = Count; // send message call in Count property is expensive, so do it once

        if (curCount == 0)
        {
            // Loading message
            pe.Graphics.DrawString("Loading / waiting for data... ", new Font("Courier New", 12), new SolidBrush(this.ForeColor), ClientRectangle); // Draw text using mTextColor
            return;
        }

        int curSelectedIndex = SelectedIndex; // send message call is expensive,  so do it once

        Rectangle r = new Rectangle(ClientRectangle.Left, 0, ClientRectangle.Width, 18);
        DrawItemState state = DrawItemState.None;

        int visibleItems = ClientRectangle.Height / 16;
        for (int i = 0; i < visibleItems; i++)
        {
            int curIndex = TopIndex + i;

            r.Y = i * 16;

            if (curIndex >= curCount)
            {
                break;
            }

            if (ItemSelected(curIndex))
            {
                state = DrawItemState.Selected;
            }
            else
            {
                state = DrawItemState.None;
            }

            DrawItemEventArgs di = new DrawItemEventArgs(pe.Graphics, Font, r, curIndex, state, ForeColor, BackColor);
            OnDrawItem(di);
        }
    }

    /// <summary>
    /// OnPaintBackground - Draw the background so we don't see holes
    /// </summary>
    /// <param name="pevent"></param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      pevent.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
    }

    /// <summary>
    /// List Item drawing
    /// </summary>
    /// <param name="e"></param>
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      if(e.Index == -1)
        return;

      if ((e.State & DrawItemState.Selected ) == DrawItemState.Selected )
      {
        mSelectedIndex = e.Index;
      }
      base.OnDrawItem(e); // Line and colors are setup by user in DrawItem event
      
      e.DrawBackground();

      if(mIsHighlighted)
      {
        e.Graphics.FillRectangle(new SolidBrush(Color.FromName("yellow")), e.Bounds.X, e.Bounds.Y, 2, e.Bounds.Height);
      }
      try
      {
          e.Graphics.DrawString(Line, e.Font, new SolidBrush(mTextColor), e.Bounds); // Draw text using mTextColor
      }
      catch
      {
          e.Graphics.DrawString(string.Format("got humangus line, length {0}. Clean up the log file.",Line.Length), e.Font, new SolidBrush(mTextColor), e.Bounds); // Draw text using mTextColor
      }
    }

    /// <summary>
    /// Gets or sets the number of virtual items in the ListBox.
    /// </summary>
    public int Count
    {
      get
      {
        return SendMessage(this.Handle, LB_GETCOUNT, 0, IntPtr.Zero);
      }
      set
      {
        SendMessage(this.Handle, LB_SETCOUNT, value, IntPtr.Zero);
      }
    }

    /// <summary>
    /// Throws an exception.  All the items for a Virtual ListBox are externally managed.
    /// </summary>
    /// <remarks>The selected index can be obtained using the <see cref="SelectedIndex"/> and
    /// <see cref="SelectedIndices"/> properties.
    /// </remarks>
    [BrowsableAttribute(false)]
    public new SelectedObjectCollection SelectedItems
    {
      get
      {
        throw new InvalidOperationException("A Virtual ListBox does not have a SelectedObject collection");
      }
    }

    /// <summary>
    /// Hides the Sorted property of the ListBox control.  Any attempt to set this property
    /// to true will result in an exception.
    /// </summary>
    [BrowsableAttribute(false)]
    public new bool Sorted
    {
      get
      {
        return false;
      }
      set
      {
        if (value)
        {
          throw new InvalidOperationException("A Virtual ListBox cannot be sorted.");
        }
      }
    }

    /// <summary>
    /// Returns the selected index in the control.  If the control has the multi-select
    /// style, then the first selected item is returned.
    /// </summary>
    public new int SelectedIndex
    {
      get
      {
        int selIndex = -1;
        if (SelectionMode == System.Windows.Forms.SelectionMode.One)
        {
          selIndex = SendMessage(this.Handle, LB_GETCURSEL, 0, IntPtr.Zero);
        }			
        else if ((SelectionMode == System.Windows.Forms.SelectionMode.MultiExtended) || 
          (SelectionMode == System.Windows.Forms.SelectionMode.MultiSimple))
        {
          int selCount = SendMessage(this.Handle, LB_GETSELCOUNT, 0, IntPtr.Zero);
          if (selCount > 0)
          {
            IntPtr buf = Marshal.AllocCoTaskMem(4);
            SendMessage(this.Handle, LB_GETSELITEMS, 1, buf);
            selIndex = Marshal.ReadInt32(buf);
            Marshal.FreeCoTaskMem(buf);
          }
        }
        return selIndex;
      }
      set
      {
        if (SelectionMode == SelectionMode.One)
        {
          SendMessage(this.Handle, LB_SETSEL, 0, new IntPtr(-1));
          SendMessage(this.Handle, LB_SETCURSEL, value, IntPtr.Zero);
        }
        else if ((SelectionMode == SelectionMode.MultiExtended) || 
          (SelectionMode == SelectionMode.MultiSimple))
        {
          // clear any existing selection:
          //SendMessage(this.Handle, LB_SETSEL, 0, new IntPtr(-1));
          // set the requested selection
          SendMessage(this.Handle, LB_SETSEL, 1, (IntPtr) value);
        }
      }
    }		

    public void ClearExistingSelection()
    {
      // clear any existing selection:
      SendMessage(this.Handle, LB_SETSEL, 0, new IntPtr(-1));
    }

    /// <summary>
    /// Gets the selection state for an item.
    /// </summary>
    /// <param name="index">Index of the item.</param>
    /// <returns><c>true</c> if selected, <c>false</c> otherwise.</returns>
    public bool ItemSelected(int index)
    {
      bool state = false;
      if (SelectionMode == System.Windows.Forms.SelectionMode.One)
      {
        state = (SelectedIndex == index);
      }			
      else if ((SelectionMode == System.Windows.Forms.SelectionMode.MultiExtended) || 
        (SelectionMode == System.Windows.Forms.SelectionMode.MultiSimple))
      {
        state = (SendMessage(this.Handle, LB_GETSEL, index, IntPtr.Zero) != 0);
      }
      return state;
    }

    /// <summary>
    /// Sets the selection state for an item.
    /// </summary>
    /// <param name="index">Index of the item.</param>
    /// <param name="state">New selection state for the item.</param>
    public void ItemSelected(int index, bool state)
    {
      if (SelectionMode == System.Windows.Forms.SelectionMode.One)
      {
        if (state)
        {
          SelectedIndex = index;
        }
      }			
      else if ((SelectionMode == System.Windows.Forms.SelectionMode.MultiExtended) || 
        (SelectionMode == System.Windows.Forms.SelectionMode.MultiSimple))
      {
        SendMessage(this.Handle, LB_SETSEL, (state ? 1 : 0), (IntPtr) index);
      }
    }

    /// <summary>
    /// Sets up the <see cref="CreateParams"/> object to tell Windows
    /// how the ListBox control should be created.  In this instance
    /// the default configuration is modified to remove <c>LBS_HASSTRINGS</c>
    /// and <c>LBS_SORT</c> styles and to add <c>LBS_NODATA</c>
    /// and <c>LBS_OWNERDRAWFIXED</c> styles. This converts the ListBox
    /// into a Virtual ListBox.
    /// </summary>
    protected override System.Windows.Forms.CreateParams CreateParams
    {
      get
      {
        CreateParams defParams = base.CreateParams;
        //Console.WriteLine("In Param style: {0:X8}", defParams.Style);
        defParams.Style = defParams.Style & ~LBS_HASSTRINGS;
        defParams.Style = defParams.Style & ~LBS_SORT;
        defParams.Style = defParams.Style | LBS_OWNERDRAWFIXED | LBS_NODATA;
        //Console.WriteLine("Out Param style: {0:X8}", defParams.Style);
        return defParams;
      }
    }

    /// <summary>
    /// Called when the ListBox handle is destroyed.  
    /// </summary>
    /// <param name="e">Not used</param>
    protected override void OnHandleDestroyed(EventArgs e)
    {
      // Nasty. The problem is with the call to NativeUpdateSelection,
      // which calls the EnsureUpToDate on the SelectedObjectCollection method, 
      // and that is broken.
      try
      {
        base.OnHandleDestroyed(e);
      }
      catch (Exception)
      {
      }
    }

    /// <summary>
    /// Implements a read-only collection of selected items in the
    /// VListBox.
    /// </summary>
    public new class SelectedIndexCollection : ICollection, IEnumerable
    {
      private ColorListBox owner = null;

      /// <summary>
      /// Creates a new instance of this class
      /// </summary>
      /// <param name="owner">The VListBox which owns the collection</param>
      public SelectedIndexCollection(ColorListBox owner)
      {
        this.owner = owner;
      }

      /// <summary>
      /// Returns an enumerator which allows iteration through the selected items
      /// collection.
      /// </summary>
      /// <returns></returns>
      public IEnumerator GetEnumerator()
      {
        return new SelectedIndexCollectionEnumerator(this.owner);
      }

      /// <summary>
      /// Not implemented. Throws an exception.
      /// </summary>
      /// <param name="dest">Array to copy items to</param>
      /// <param name="startIndex">First index in array to put items in.</param>
      public void CopyTo(Array dest, int startIndex)
      {
        throw new InvalidOperationException("Not implemented");
      }

      /// <summary>
      /// Returns the number of items in the collection.
      /// </summary>
      public int Count
      {
        get
        {
          return SendMessage(owner.Handle, LB_GETSELCOUNT, 0, IntPtr.Zero);
        }
      }

      /// <summary>
      /// Returns the selected item with the specified 0-based index in the collection
      /// of selected items.  
      /// </summary>
      /// <remarks>
      /// Do not use this method to enumerate through all selected
      /// items as it gets the collection of selected items each item it 
      /// is called.  The <c>foreach</c> enumerator only gets the collection
      /// of items once when it is constructed and is therefore quicker.
      /// </remarks>
      public int this[int index]
      {
        get
        {
          int selIndex = -1;
          int selCount = SendMessage(owner.Handle, LB_GETSELCOUNT, 0, IntPtr.Zero);
          if ((index < selCount) && (index >= 0))
          {
            IntPtr buf = Marshal.AllocCoTaskMem(4 * (selCount));
            SendMessage(owner.Handle, LB_GETSELITEMS, selCount, buf);
            selIndex = Marshal.ReadInt32(buf, index * 4);
            Marshal.FreeCoTaskMem(buf);
          }
          else
          {
            throw new ArgumentException("Index out of bounds", "index");
          }
          return selIndex;
					
        }
      }

      /// <summary>
      /// Returns <c>false</c>.  This collection is not synchronized for
      /// concurrent access from multiple threads.
      /// </summary>
      public bool IsSynchronized
      {
        get
        {
          return false;
        }
      }

      /// <summary>
      /// Not implemented. Throws an exception.
      /// </summary>
      public object SyncRoot
      {
        get
        {
          throw new InvalidOperationException("Synchronization not supported.");
        }
      }


    }

    /// <summary>
    /// Implements the <see cref="IEnumerator"/> interface for the selected indexes
    /// within a <see cref="VListBox"/> control.
    /// </summary>
    public class SelectedIndexCollectionEnumerator : IEnumerator, IDisposable
    {
      private IntPtr buf = IntPtr.Zero;
      private int size = 0;
      private int offset = 0;
		
      /// <summary>
      /// Constructs a new instance of this class.
      /// </summary>
      /// <param name="owner">The <see cref="VListBox"/> which owns the collection.</param>
      public SelectedIndexCollectionEnumerator(ColorListBox owner)
      {
        int selCount = SendMessage(owner.Handle, LB_GETSELCOUNT, 0, IntPtr.Zero);
        if (selCount > 0)
        {
          buf = Marshal.AllocCoTaskMem(4 * selCount);
          SendMessage(owner.Handle, LB_GETSELITEMS, selCount, buf);
        }
      }

      /// <summary>
      /// Clears up any resources associated with this enumerator.
      /// </summary>
      public void Dispose()
      {
        if (!buf.Equals(IntPtr.Zero))
        {
          Marshal.FreeCoTaskMem(buf);
          buf = IntPtr.Zero;
        }
      }

      /// <summary>
      /// Resets the enumerator to the start of the list.
      /// </summary>
      public void Reset()
      {
        offset = 0;
      }

      /// <summary>
      /// Returns the current object.
      /// </summary>
      public object Current
      {
        get
        {
          if (offset >= size)
          {
            throw new Exception("Collection is exhausted.");
          }
          else
          {
            int index = Marshal.ReadInt32(buf, offset * 4);
            return (object) index;
          }

        }
      }

      /// <summary>
      /// Advances the enumerator to the next element of the collection.
      /// </summary>
      /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; 
      /// <c>false</c> if the enumerator has passed the end of the collection.</returns>
      public bool MoveNext()
      {
        bool success = false;
        offset++;
        if (offset < size)
        {
          success = true;
        }
        return success;
      }
    }


  }
}
