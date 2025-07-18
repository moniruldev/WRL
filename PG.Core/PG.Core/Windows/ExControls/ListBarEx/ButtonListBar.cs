using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PG.Core.Windows.ExControls.ListBar
{
    #region ButtonListBar
    /// <summary>
    /// Summary description for ButtonListBar.
    /// </summary>
    public class ButtonListBar : System.Windows.Forms.UserControl
    {
        #region Member Variables
        private bool m_isXp = false;
        private ImageList m_imageList = null;
        private ToolTip m_toolTip = null;
        private ButtonListBarItems m_items = null;
        private int m_buttonWidth = 96;
        private int m_lastButtonWidth = 0;
        private bool m_showScroll = false;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion

        #region Events
        public event ItemClickEventHandler ItemClick;
        public event MouseEventHandler BarClick;
        public event SelectionChangedEventHandler SelectionChanged;
        #endregion

        #region Unmanaged Code
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public override string ToString()
            {
                return String.Format("({0},{1})-({2},{3})", left, top, right, bottom);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct SIZEAPI
        {
            public int cx;
            public int cy;

            public override string ToString()
            {
                return String.Format("{0} x {1}", cx, cy);
            }
        }

        [DllImport("gdi32")]
        private extern static IntPtr SelectObject(
            IntPtr hDC,
            IntPtr hObject);
        [DllImport("gdi32")]
        private extern static int DeleteObject(
            IntPtr hObject);
        [DllImport("user32")]
        private extern static int GetSystemMetrics(
            int nIndex);
        private const int SM_CXVSCROLL = 2;

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static IntPtr OpenThemeData(
            IntPtr hWnd,
            string pszClassList
            );
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int CloseThemeData(
            IntPtr hTheme);
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int DrawThemeBackground(
            IntPtr hTheme,
            IntPtr hDC,
            int iPartId,
            int iStateId,
            ref RECT pRect,
            ref RECT pClipRect
            );
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int DrawThemeParentBackground(
            IntPtr hTheme,
            IntPtr hDC,
            ref RECT prc
            );
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int GetThemeBackgroundContentRect(
            IntPtr hTheme,
            IntPtr hDC,
            int iPartId,
            int iStateId,
            ref RECT pBoundingRect,
            ref RECT pContentRect
            );
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int DrawThemeText(
            IntPtr hTheme,
            IntPtr hDC,
            int iPartId,
            int iStateId,
            string pszText,
            int iCharCount,
            int dwTextFlag,
            int dwTextFlags2,
            ref RECT pRect
            );
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int DrawThemeIcon(
            IntPtr hTheme,
            IntPtr hDC,
            int iPartId,
            int iStateId,
            ref RECT pRect,
            IntPtr hIml,
            int iImageIndex
            );
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int GetThemePartSize(
            IntPtr hTheme,
            IntPtr hDC,
            int iPartId,
            int iStateId,
            ref RECT pRect,
            int iSize,
            ref SIZEAPI pSz
            );
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int GetThemeTextExtent(
            IntPtr hTheme,
            IntPtr hDC,
            int iPartId,
            int iStateId,
            string pszText,
            int iCharCount,
            int dwTextFlags,
            ref RECT pBoundingRect,
            ref RECT pExtentRect
            );
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int DrawThemeBackground(
            IntPtr hTheme,
            IntPtr hDC,
            int iPartId,
            int iStateId,
            ref RECT pDestRect,
            int uEdge,
            int uFlags,
            ref RECT pContentRect
            );

        private const int S_OK = 0;
        private const int HWND_DESKTOP = 0;

        // THEMESIZE
        private const int TS_MIN = 0;            //// minimum size
        private const int TS_TRUE = 1;            //// size without stretching
        private const int TS_DRAW = 2;           //// size that theme mgr will use to draw part

        // Button class
        private const string UXTHEMEBUTTONCLASS = "Button";
        private const string UXTHEMETOOLBARCLASS = "Toolbar";
        // Button part
        private const int TP_BUTTON = 1;
        private const int BP_PUSHBUTTON = 1;
        // Button states
        private const int TS_NORMAL = 1;
        private const int TS_HOT = 2;
        private const int TS_PRESSED = 3;
        private const int TS_DISABLED = 4;
        private const int TS_CHECKED = 5;
        private const int TS_HOTCHECKED = 6;
        private const int PBS_DISABLED = 4;

        // DrawTextFlags
        private const int DT_TOP = 0x0;
        private const int DT_LEFT = 0x0;
        private const int DT_CENTER = 0x1;
        private const int DT_RIGHT = 0x2;
        private const int DT_VCENTER = 0x4;
        private const int DT_BOTTOM = 0x8;
        private const int DT_WORDBREAK = 0x10;
        private const int DT_SINGLELINE = 0x20;
        private const int DT_EXPANDTABS = 0x40;
        private const int DT_TABSTOP = 0x80;
        private const int DT_NOCLIP = 0x100;
        private const int DT_EXTERNALLEADING = 0x200;
        private const int DT_CALCRECT = 0x400;
        private const int DT_NOPREFIX = 0x800;
        private const int DT_INTERNAL = 0x1000;
        private const int DT_EDITCONTROL = 0x2000;
        private const int DT_PATH_ELLIPSIS = 0x4000;
        private const int DT_END_ELLIPSIS = 0x8000;
        private const int DT_MODIFYSTRING = 0x10000;
        private const int DT_RTLREADING = 0x20000;
        private const int DT_WORD_ELLIPSIS = 0x40000;
        private const int DT_NOFULLWIDTHCHARBREAK = 0x80000;
        private const int DT_HIDEPREFIX = 0x100000;
        private const int DT_PREFIXONLY = 0x200000;

        // UxTheme DrawText Additional Flag
        private const int DTT_GRAYED = 0x1;             //// draw a grayed-out string

        #endregion

        public ButtonListBar()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            base.TabStop = true;
            base.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
            m_items = CreateItemCollection();
            Version ver = System.Environment.OSVersion.Version;
            if (ver.Major > 5)
            {
                m_isXp = true;
            }
            else
            {
                if (ver.Major == 5)
                {
                    if (ver.Minor >= 1)
                    {
                        m_isXp = true;
                    }
                }
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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

        #region Properties and Methods
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                return m_imageList;
            }
            set
            {
                m_imageList = value;
            }
        }

        public System.Windows.Forms.ToolTip ToolTip
        {
            get
            {
                return m_toolTip;
            }
            set
            {
                m_toolTip = value;
            }
        }

        public ButtonListBarItems Items
        {
            get
            {
                return m_items;
            }
        }

        public int ButtonWidth
        {
            get
            {
                return m_buttonWidth;
            }
            set
            {
                m_buttonWidth = value;
                OnItemChanged(null);
            }
        }

        public ButtonListBarItem SelectedItem
        {
            get
            {
                ButtonListBarItem ret = null;
                foreach (ButtonListBarItem item in m_items)
                {
                    if (item.Selected)
                    {
                        ret = item;
                        break;
                    }
                }
                return ret;
            }
        }
        #endregion

        internal bool OnSelectItem(
            ButtonListBarItem item,
            bool value
            )
        {
            if (value)
            {
                for (int i = 0; i < m_items.Count; i++)
                {
                    if (m_items[i] != item)
                    {
                        if (m_items[i].Selected)
                        {
                            m_items[i].Selected = false;
                        }
                    }
                }
                SelectionChangedEventArgs e = new SelectionChangedEventArgs(item);
                OnSelectionChanged(e);
                return value;
            }
            else
            {
                return value;
            }
        }

        internal void OnItemChanged(
            ButtonListBarItem item
            )
        {
            int minHeight = 0;

            if (m_items != null)
            {
                if (m_items.Count > 0)
                {

                    // Measure all the items & evaluate the overall height
                    bool useXpStyles = m_isXp;
                    IntPtr hTheme = IntPtr.Zero;

                    if (useXpStyles)
                    {
                        hTheme = OpenThemeData(
                            this.Handle,
                            UXTHEMEBUTTONCLASS);
                        if (hTheme.Equals(IntPtr.Zero))
                        {
                            useXpStyles = false;
                        }
                    }

                    ButtonListBarItem lastBarItem = null;
                    int iconSize = 0;

                    Graphics gfx = Graphics.FromHwnd(this.Handle);

                    if (m_imageList != null)
                    {
                        iconSize = m_imageList.ImageSize.Height;
                    }

                    if (useXpStyles)
                    {
                        RECT tTextR = new RECT();
                        RECT tTextBoundR = new RECT();
                        int lR = 0;

                        IntPtr hdc = gfx.GetHdc();
                        IntPtr hFont = this.Font.ToHfont();
                        IntPtr hFontOld = SelectObject(hdc, hFont);

                        foreach (ButtonListBarItem barItem in m_items)
                        {
                            string itemText = barItem.Text;
                            tTextR.top = 0;
                            tTextR.bottom = 1280;
                            tTextR.left = 6;
                            tTextR.right = m_buttonWidth - 12;
                            tTextBoundR.top = 0;
                            tTextBoundR.bottom = 1280;
                            tTextBoundR.left = 3;
                            tTextBoundR.right = m_buttonWidth - 12;

                            lR = GetThemeTextExtent(
                                hTheme,
                                hdc,
                                TP_BUTTON,
                                TS_NORMAL,
                                itemText, -1,
                                DT_CENTER | DT_WORDBREAK,
                                ref tTextBoundR, ref tTextR);
                            //Console.WriteLine("GetThemeTextExtent {0}, {1}, {2}", lR, barItem.Text, tTextR.ToString())
                            if (lastBarItem == null)
                            {
                                barItem.Start = 4;
                            }
                            else
                            {
                                barItem.Start = lastBarItem.Start + lastBarItem.Extent + 4;
                            }
                            barItem.Extent = tTextR.bottom - tTextR.top + 4 + iconSize + 6;
                            minHeight = barItem.Start + barItem.Extent;
                            lastBarItem = barItem;
                        }

                        SelectObject(hdc, hFontOld);
                        DeleteObject(hFont);
                        gfx.ReleaseHdc(hdc);
                        CloseThemeData(hTheme);
                    }
                    else
                    {
                        StringFormat fmt = new StringFormat();
                        fmt.Alignment = StringAlignment.Center;
                        fmt.LineAlignment = StringAlignment.Near;
                        fmt.Trimming = StringTrimming.None;
                        fmt.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;

                        foreach (ButtonListBarItem barItem in m_items)
                        {
                            string itemText = barItem.Text;
                            int itemHeight = (int)gfx.MeasureString(
                                itemText,
                                this.Font,
                                m_buttonWidth - 12,
                                fmt).Height;
                            //Console.WriteLine("GDI+ String Height {0}, {1}", itemText, itemHeight)
                            if (lastBarItem == null)
                            {
                                barItem.Start = 4;
                            }
                            else
                            {
                                barItem.Start = lastBarItem.Start + lastBarItem.Extent + 4;
                            }
                            barItem.Extent = itemHeight + 4 + iconSize + 6;
                            minHeight = barItem.Start + barItem.Extent;
                            lastBarItem = barItem;
                        }
                        fmt.Dispose();

                    }

                    gfx.Dispose();

                    minHeight = minHeight + 3;
                }
            }

            bool showScroll = false;
            if (minHeight > this.Height)
            {
                showScroll = true;
            }
            this.AutoScrollMinSize = new Size(0, minHeight);

            if (
                ((m_showScroll != showScroll)) |
                ((m_lastButtonWidth != m_buttonWidth))
                )
            {
                if (showScroll)
                {
                    this.Width = m_buttonWidth + GetSystemMetrics(SM_CXVSCROLL);
                }
                else
                {
                    this.Width = m_buttonWidth;
                }
                m_showScroll = showScroll;
                m_lastButtonWidth = m_buttonWidth;
            }
            else
            {
                this.Invalidate();
            }

        }

        protected override void OnPaint(
            System.Windows.Forms.PaintEventArgs e)
        {

            // Clear the background
            Brush br = new SolidBrush(this.BackColor);
            e.Graphics.FillRectangle(br, e.ClipRectangle);
            br.Dispose();

            if ((m_items == null) || (m_items.Count == 0))
            {
                return;
            }

            // Paint the buttons:
            bool useXpStyles = m_isXp;
            IntPtr hTheme = IntPtr.Zero;

            if (useXpStyles)
            {
                hTheme = OpenThemeData(this.Handle, UXTHEMETOOLBARCLASS);
                if (hTheme.Equals(IntPtr.Zero))
                {
                    useXpStyles = false;
                }
            }

            int iconSize = 0;
            if (m_imageList != null)
            {
                iconSize = m_imageList.ImageSize.Height;
            }

            int iStateId = 0;
            string itemText = "";

            if (useXpStyles)
            {

                // Drawing using XP Styles
                IntPtr hDC = e.Graphics.GetHdc();

                RECT tR = new RECT();
                tR.left = 0;
                tR.right = m_buttonWidth;
                tR.top = 0;
                tR.bottom = this.Height;

                RECT tItemR = new RECT();
                RECT tContentR = new RECT();
                RECT tIconR = new RECT();

                IntPtr hFontOld = IntPtr.Zero;
                IntPtr hFont = this.Font.ToHfont();
                hFontOld = SelectObject(hDC, hFont);

                int textAlign = DT_CENTER | DT_WORDBREAK;

                foreach (ButtonListBarItem barItem in m_items)
                {

                    tItemR.left = tR.left + 3;
                    tItemR.right = tR.right - 3;
                    tItemR.top = barItem.Start + this.AutoScrollPosition.Y;
                    tItemR.bottom = barItem.Start + barItem.Extent + this.AutoScrollPosition.Y;

                    if (barItem.Enabled)
                    {
                        if (barItem.MouseDown)
                        {
                            if (barItem.MouseOver)
                            {
                                if (barItem.Selected)
                                {
                                    iStateId = TS_HOTCHECKED;
                                }
                                else
                                {
                                    iStateId = TS_PRESSED;
                                }
                            }
                            else
                            {
                                if (barItem.Selected)
                                {
                                    iStateId = TS_CHECKED;
                                }
                                else
                                {
                                    iStateId = TS_HOT;
                                }
                            }
                        }
                        else
                        {
                            if (barItem.MouseOver)
                            {
                                if (barItem.Selected)
                                {
                                    iStateId = TS_HOTCHECKED;
                                }
                                else
                                {
                                    iStateId = TS_HOT;
                                }
                            }
                            else
                            {
                                if (barItem.Selected)
                                {
                                    iStateId = TS_CHECKED;
                                }
                                else
                                {
                                    iStateId = TS_NORMAL;
                                }
                            }
                        }
                    }
                    else
                    {
                        iStateId = TS_DISABLED;
                    }

                    DrawThemeBackground(hTheme, hDC, TP_BUTTON, iStateId,
                        ref tItemR, ref tItemR);
                    GetThemeBackgroundContentRect(hTheme, hDC, TP_BUTTON, iStateId,
                        ref tItemR, ref tContentR);

                    if (iStateId == TS_DISABLED)
                    {
                        CloseThemeData(hTheme);
                        hTheme = OpenThemeData(this.Handle, UXTHEMEBUTTONCLASS);
                        iStateId = PBS_DISABLED;
                    }

                    tIconR = tContentR;
                    tIconR.left = tContentR.left + (tContentR.right - tContentR.left - iconSize) / 2;
                    tIconR.right = tIconR.left + iconSize;
                    tIconR.top += 4;
                    tIconR.bottom = tIconR.top + iconSize;

                    if (iStateId == TS_PRESSED)
                    {
                        tIconR.left += 1;
                        tIconR.top += 1;
                    }

                    if (m_imageList != null)
                    {
                        SelectObject(hDC, hFontOld);
                        DeleteObject(hFont);
                        e.Graphics.ReleaseHdc(hDC);
                        if (barItem.Enabled)
                        {
                            e.Graphics.DrawImage(
                                m_imageList.Images[barItem.IconIndex],
                                tIconR.left, tIconR.top);
                        }
                        else
                        {
                            ControlPaint.DrawImageDisabled(
                                e.Graphics,
                                m_imageList.Images[barItem.IconIndex],
                                tIconR.left,
                                tIconR.top,
                                this.BackColor);
                        }
                        hDC = e.Graphics.GetHdc();
                        hFont = this.Font.ToHfont();
                        hFontOld = SelectObject(hDC, hFont);
                    }

                    tContentR.top = tContentR.top + 4 + iconSize;
                    tContentR.left += 3;
                    tContentR.right -= 3;
                    if (iStateId == TS_PRESSED)
                    {
                        tContentR.left += 1;
                        tContentR.top += 1;
                        tContentR.right += 1;
                        tContentR.bottom += 1;
                    }

                    itemText = barItem.Text;
                    DrawThemeText(hTheme, hDC, BP_PUSHBUTTON, iStateId,
                        itemText, -1,
                        textAlign,
                        (barItem.Enabled ? 0 : DTT_GRAYED),
                        ref tContentR);

                    if (iStateId == TS_DISABLED)
                    {
                        CloseThemeData(hTheme);
                        hTheme = OpenThemeData(this.Handle, UXTHEMETOOLBARCLASS);
                    }
                    
                }

                CloseThemeData(hTheme);
                SelectObject(hDC, hFontOld);
                DeleteObject(hFont);
                e.Graphics.ReleaseHdc(hDC);

            }
            else
            {
                //  Drawing using .NET Framework

                StringFormat fmt = new StringFormat();
                fmt.Alignment = StringAlignment.Center;
                fmt.LineAlignment = StringAlignment.Near;
                fmt.Trimming = StringTrimming.None;
                fmt.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;

                Brush brText = new SolidBrush(this.ForeColor);
                foreach (ButtonListBarItem barItem in m_items)
                {
                    if (barItem.Enabled)
                    {
                        if (barItem.MouseDown)
                        {
                            if (barItem.MouseOver)
                            {
                                if (barItem.Selected)
                                {
                                    iStateId = TS_HOTCHECKED;
                                }
                                else
                                {
                                    iStateId = TS_PRESSED;
                                }
                            }
                            else
                            {
                                if (barItem.Selected)
                                {
                                    iStateId = TS_CHECKED;
                                }
                                else
                                {
                                    iStateId = TS_HOT;
                                }
                            }
                        }
                        else
                        {
                            if (barItem.MouseOver)
                            {
                                if (barItem.Selected)
                                {
                                    iStateId = TS_HOTCHECKED;
                                }
                                else
                                {
                                    iStateId = TS_HOT;
                                }
                            }
                            else
                            {
                                if (barItem.Selected)
                                {
                                    iStateId = TS_CHECKED;
                                }
                                else
                                {
                                    iStateId = TS_NORMAL;
                                }
                            }
                        }
                    }
                    else
                    {
                        iStateId = TS_DISABLED;
                    }

                    Rectangle itemRect = new Rectangle(3, barItem.Start, m_buttonWidth - 6, barItem.Extent);
                    itemRect.Offset(0, this.AutoScrollPosition.Y);

                    //  Draw background to item (if necessary);
                    if (iStateId == TS_CHECKED)
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Control, itemRect);
                    }

                    //  Draw border:
                    if ((iStateId == TS_HOTCHECKED) || (iStateId == TS_CHECKED))
                    {
                        // DrawEdgeAPI(m_cMemDC.hDC, tItemR, BDR_SUNKEN, BF_RECT Or BF_SOFT)
                        ControlPaint.DrawBorder3D(e.Graphics, itemRect, Border3DStyle.Sunken, Border3DSide.All);
                    }
                    else if (iStateId == TS_HOT)
                    {
                        // DrawEdgeAPI(m_cMemDC.hDC, tItemR, BDR_RAISED, BF_RECT Or BF_SOFT)
                        ControlPaint.DrawBorder3D(e.Graphics, itemRect, Border3DStyle.Raised, Border3DSide.All);
                    }
                    else if (iStateId == TS_PRESSED)
                    {
                        // DrawEdgeAPI(m_cMemDC.hDC, tItemR, BDR_SUNKEN, BF_RECT Or BF_SOFT)
                        ControlPaint.DrawBorder3D(e.Graphics, itemRect, Border3DStyle.Sunken, Border3DSide.All);
                    }
                    
                    itemRect.Inflate(-2, -2);
                    Rectangle iconRect = new Rectangle(
                        itemRect.X + (itemRect.Width - iconSize) / 2,
                        itemRect.Y + 4,
                        iconSize,
                        iconSize);
                    if (iStateId == TS_PRESSED)
                    {
                        iconRect.Offset(1, 1);
                    }

                    if (m_imageList != null)
                    {
                        if (barItem.Enabled)
                        {
                            e.Graphics.DrawImage(
                                m_imageList.Images[barItem.IconIndex],
                                iconRect.X, iconRect.Y);
                        }
                        else
                        {                            
                            ControlPaint.DrawImageDisabled(
                                e.Graphics,
                                m_imageList.Images[barItem.IconIndex],
                                iconRect.X,
                                iconRect.Y,
                                this.BackColor);
                        }
                    }

                    itemRect.Y = itemRect.Y + iconSize + 4;
                    // itemRect.Inflate(-6, 0)
                    if (iStateId == TS_PRESSED)
                    {
                        itemRect.Offset(1, 1);
                    }

                    RectangleF textRect = new RectangleF(itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height);

                    itemText = barItem.Text;
                    if (iStateId == TS_DISABLED)
                    {
                        //ControlPaint.DrawStringDisabled(e.Graphics,
                        //    itemText, this.Font, Color.FromKnownColor(KnownColor.ControlDark),
                        //    textRect, fmt);
                            ControlPaint.DrawStringDisabled(e.Graphics,
                                    itemText, this.Font, Color.FromKnownColor(KnownColor.InactiveCaptionText),
                                    textRect, fmt);
                    }
                    else
                    {

                        //e.Graphics.DrawString(
                        //    itemText, this.Font, brText,
                        //    textRect, fmt);



                        if (iStateId == TS_HOT | iStateId == TS_CHECKED | iStateId == TS_HOTCHECKED | iStateId == TS_PRESSED)
                        {
                            FontStyle fs = FontStyle.Bold;

                            Font f = new Font(this.Font, fs);
                            
                            e.Graphics.DrawString(
                                itemText, f, brText,
                                textRect, fmt);


                        }
                        else
                        {
                            e.Graphics.DrawString(
                                itemText, this.Font, brText,
                                textRect, fmt);

                            //Pen p = new Pen(Color.DarkGray);
                            ////e.Graphics.DrawLine(p,textRect.Left,textRect.Top,textRect.Right,textRect.Bottom);
                            //e.Graphics.DrawLine(p, textRect.Left, textRect.Bottom + 2, textRect.Right, textRect.Bottom + 2);
                        }
                    }
                    Pen p = new Pen(Color.DarkGray);
                    Rectangle bRect = new Rectangle(3, barItem.Start, m_buttonWidth - 6, barItem.Extent);
                    //e.Graphics.DrawLine(p, itemRect.Left, itemRect.Bottom, itemRect.Right, itemRect.Bottom);
                    e.Graphics.DrawLine(p, bRect.Left, bRect.Bottom + 1, bRect.Right, bRect.Bottom + 1);
                }
                brText.Dispose();
                fmt.Dispose();
            }
        }

        protected virtual ButtonListBarItems CreateItemCollection()
        {
            return new ButtonListBarItems(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (m_items != null)
            {
                if (m_items.Count > 0)
                {
                    int lNewIndex = -1;
                    int lCurrentIndex = -1;
                    int lMouseOverIndex = -1;
                    bool bFound = false;

                    for (int i = 0; i < m_items.Count; i++)
                    {
                        if (m_items[i].MouseOver)
                        {
                            lMouseOverIndex = i;
                        }
                        if (m_items[i].Selected)
                        {
                            lCurrentIndex = i;
                        }
                    }

                    if (lMouseOverIndex > 0)
                    {
                        lCurrentIndex = lMouseOverIndex;
                    }

                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            lNewIndex = selectNext(lCurrentIndex, -1);
                            break;
                        case Keys.Down:
                            lNewIndex = selectNext(lCurrentIndex, 1);
                            break;
                        case Keys.PageUp:
                            lNewIndex = selectNext(lCurrentIndex, -4);
                            break;
                        case Keys.PageDown:
                            lNewIndex = selectNext(lCurrentIndex, 4);
                            break;
                        case Keys.Home:
                            lNewIndex = 1;
                            while (!bFound)
                            {
                                if (m_items[lNewIndex].Enabled)
                                {
                                    bFound = true;
                                }
                                else
                                {
                                    lNewIndex++;
                                    if (lNewIndex >= m_items.Count)
                                    {
                                        bFound = true;
                                    }
                                }
                            }
                            break;
                        case Keys.End:
                            lNewIndex = m_items.Count - 1;
                            while (!bFound)
                            {
                                if (m_items[lNewIndex].Enabled)
                                {
                                    bFound = true;
                                }
                                else
                                {
                                    lNewIndex--;
                                    if (lNewIndex < 0)
                                    {
                                        bFound = true;
                                    }
                                }
                            }
                            break;
                        case Keys.Enter:
                            if (lMouseOverIndex >= 0)
                            {
                                m_items[lMouseOverIndex].Selected = true;
                                m_items[lMouseOverIndex].MouseOver = false;
                                for (int i = 0; i < m_items.Count; i++)
                                {
                                    m_items[i].MouseOver = false;
                                }
                                ensureVisible(lMouseOverIndex);
                                ItemClickEventArgs itemClickArgs = new ItemClickEventArgs(
                                    m_items[lMouseOverIndex],
                                    new Point(0, 0),
                                    MouseButtons.None);
                                OnItemClick(itemClickArgs);
                            }
                            break;
                    }

                    if ((lNewIndex != lCurrentIndex) &&
                        (lNewIndex > -1) &&
                        (lNewIndex < m_items.Count))
                    {
                        for (int i = 0; i < m_items.Count; i++)
                        {
                            if (i == lNewIndex)
                            {
                                m_items[i].MouseOver = true;
                            }
                            else
                            {
                                m_items[i].MouseOver = false;
                            }
                        }
                        ensureVisible(lNewIndex);
                        this.Invalidate();
                    }
                }
            }
        }

        private int selectNext(
            int lCurrent,
            int lDir
            )
        {
            bool bFound = false;
            int lNewIndex = -1;
            int lLastChecked = -1;

            lLastChecked = lCurrent;
            while (!bFound)
            {
                lNewIndex = lLastChecked + lDir;

                if ((lNewIndex < 0) || (lNewIndex >= m_items.Count))
                {
                    if (Math.Abs(lDir) > 1)
                    {
                        // equivalent to hitting Home or End:
                        if (Math.Sign(lDir) == 1)
                        {
                            // End
                            lNewIndex = m_items.Count - 1;
                            while (!bFound)
                            {
                                if (m_items[lNewIndex].Enabled)
                                {
                                    bFound = true;
                                }
                                else
                                {
                                    lNewIndex--;
                                    if (lNewIndex < 0)
                                    {
                                        bFound = true;
                                    }
                                }
                            }

                        }
                        else
                        {
                            // Home
                            lNewIndex = 0;
                            while (!bFound)
                            {
                                if (m_items[lNewIndex].Enabled)
                                {
                                    bFound = true;
                                }
                                else
                                {
                                    lNewIndex++;
                                    if (lNewIndex >= m_items.Count)
                                    {
                                        bFound = true;
                                    }
                                }
                            }
                        }
                    }
                    bFound = true;
                }
                else
                {
                    lLastChecked = lNewIndex;
                    if (m_items[lNewIndex].Enabled)
                    {
                        bFound = true;
                    }
                    lDir = Math.Sign(lDir);
                }
            }
            return lNewIndex;
        }

        protected override bool IsInputKey(
            System.Windows.Forms.Keys keyData)
        {
            bool ret = base.IsInputKey(keyData);
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                case Keys.Enter:
                    ret = true;
                    break;
            }
            return ret;
        }

        protected override void OnMouseLeave(
            System.EventArgs e)
        {
            base.OnMouseLeave(e);
            int index = -1;
            for (int i = 0; i < m_items.Count; i++)
            {
                if (m_items[i].MouseDown)
                {
                    return;
                }
                else
                {
                    if (m_items[i].MouseOver)
                    {
                        index = i;
                    }
                }
            }

            if ((index >= 0) && (m_items.Count > 0))
            {
                if (HitTest() < 0)
                {
                    setToolTip("");
                    m_items[index].MouseOver = false;
                    this.Invalidate();
                }
            }
        }

        protected override void OnMouseDown(
            System.Windows.Forms.MouseEventArgs e
            )
        {
            base.OnMouseDown(e);

            ItemClickEventArgs itemEventArgs = null;
            int index = HitTest();
            if (index > -1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    m_items[index].MouseDown = true;
                    this.Invalidate();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    itemEventArgs = new ItemClickEventArgs(
                        m_items[index],
                        new Point(e.X, e.Y),
                        MouseButtons.Right);
                    OnItemClick(itemEventArgs);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                OnBarClick(e);
            }
        }

        private void setToolTip(
            string sToolTip
            )
        {
            if (m_toolTip != null)
            {
                m_toolTip.SetToolTip(this, sToolTip);
            }
        }

        protected override void OnMouseMove(
            System.Windows.Forms.MouseEventArgs e
            )
        {
            base.OnMouseMove(e);

            bool changed = false;
            int index = HitTest();
            for (int i = 0; i < m_items.Count; i++)
            {
                if (i == index)
                {
                    if (!m_items[i].MouseOver)
                    {
                        setToolTip(m_items[i].ToolTip);
                        m_items[i].MouseOver = true;
                        changed = true;
                    }
                }
                else
                {
                    if (m_items[i].MouseOver)
                    {
                        m_items[i].MouseOver = false;
                        changed = true;
                    }
                }
            }
            if (index == -1)
            {
                setToolTip("");
            }

            if (changed)
            {
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(
            System.Windows.Forms.MouseEventArgs e
            )
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                bool changed = false;
                for (int i = 0; i < m_items.Count; i++)
                {
                    if (m_items[i].MouseDown)
                    {
                        if (HitTest() == i)
                        {
                            if (m_items[i].Enabled)
                            {
                                // Click
                                m_items[i].MouseDown = false;
                                m_items[i].Selected = true;
                                ensureVisible(i);
                                this.Invalidate();
                                ItemClickEventArgs itemEventArgs = new ItemClickEventArgs(
                                    m_items[i],
                                    new Point(e.X, e.Y),
                                    MouseButtons.Left);
                                OnItemClick(itemEventArgs);
                            }
                            else
                            {
                                m_items[i].MouseDown = false;
                            }
                        }
                        else
                        {
                            // no click
                            m_items[i].MouseDown = false;
                            changed = true;
                        }
                    }
                }
                if (changed)
                {
                    this.Invalidate();
                }
            }
        }

        private void ensureVisible(
            int lIndex)
        {
            int lOffset = this.AutoScrollPosition.Y;
            int lTop = m_items[lIndex].Start + lOffset - 3;
            int lNewValue = 0;
            if (lTop < 0)
            {
                // need to scroll up
                lNewValue = (-lOffset) + lTop;
                if (lNewValue <= 2)
                {
                    lNewValue = 0;
                }
                pScrollTo(lNewValue);
            }
            else
            {
                int lBottom = 0;
                lBottom = m_items[lIndex].Start + lOffset - 3 + m_items[lIndex].Extent;
                if (lBottom > this.ClientSize.Height)
                {
                    // need to scroll down
                    lNewValue = -this.AutoScrollPosition.Y + (lBottom - this.ClientSize.Height) + 6;
                    if (lNewValue >= this.AutoScrollMinSize.Height - 4)
                    {
                        lNewValue = this.AutoScrollMinSize.Height;
                    }
                    pScrollTo(lNewValue);
                }
            }
        }

        private void pScrollTo(
            int lNewPos)
        {
            int lNow = 0;
            int lStepSize = 0;
            bool bComplete = false;
            int lNewValue = 0;

            lNow = -this.AutoScrollPosition.Y;
            if (lNewPos > lNow)
            {
                lStepSize = 1;
            }
            else
            {
                lStepSize = -1;
            }


            while (!bComplete)
            {
                lNewValue = lNow + lStepSize;
                if (lStepSize < 0)
                {
                    if (lNewValue < lNewPos)
                    {
                        lNewValue = lNewPos;
                        bComplete = true;
                    }
                }
                else
                {
                    if (lNewValue > lNewPos)
                    {
                        lNewValue = lNewPos;
                        bComplete = true;
                    }
                }
                this.AutoScrollPosition = new Point(0, Math.Abs(lNewValue));
                lStepSize = lStepSize * 2;
            }
        }

        protected override bool ProcessMnemonic(
            Char charCode)
        {
            string itemText = "";
            int pos;
            int index = -1;
            string compareText = "&" + Char.ToUpper(charCode);
            bool ret = false;

            for (int i = 0; i < m_items.Count; i++)
            {
                itemText = m_items[i].Text.ToUpper();
                pos = itemText.IndexOf(compareText);
                if (pos > 0)
                {
                    index = i;
                    ret = true;
                    break;
                }
            }
            if (index > -1)
            {
                m_items[index].Selected = true;
                ensureVisible(index);
                ItemClickEventArgs itemClickArgs = new ItemClickEventArgs(
                    m_items[index], new Point(0, 0), MouseButtons.None);
                OnItemClick(itemClickArgs);
            }

            return ret;
        }

        protected virtual void OnItemClick(
            ItemClickEventArgs e)
        {
            if (ItemClick != null)
            {
                ItemClick(this, e);
            }
        }


        protected virtual void OnBarClick(
            MouseEventArgs e)
        {
            if (BarClick != null)
            {
                BarClick(this, e);
            }
        }

        protected virtual void OnSelectionChanged(
            SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, e);
            }
        }

        protected override void OnResize(
            System.EventArgs e)
        {
            base.OnResize(e);
            OnItemChanged(null);
        }

        private int HitTest()
        {
            Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
            p = this.PointToClient(p);
            return HitTestPoint(p.X, p.Y);
        }

        private int HitTestPoint(
            int x,
            int y)
        {
            int ret = -1;
            if ((x >= 3) && (x <= m_buttonWidth - 6))
            {
                if ((y >= 0) && (y <= this.ClientSize.Height))
                {
                    ButtonListBarItem barItem = null;
                    for (int i = 0; i < m_items.Count; i++)
                    {
                        barItem = m_items[i];
                        if (y >= barItem.Start + this.AutoScrollPosition.Y)
                        {
                            if (y < barItem.Start + barItem.Extent + this.AutoScrollPosition.Y)
                            {
                                ret = i;
                                break;
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
    #endregion

	#region Event Argument Classes
	public class SelectionChangedEventArgs
	{
		private ButtonListBarItem m_item = null;

		public ButtonListBarItem Item
		{
			get
			{
				return this.m_item;
			}
		}

		public SelectionChangedEventArgs(
			ButtonListBarItem item
			)
		{
			this.m_item = item;
		}
	}

	public class ItemClickEventArgs
	{
		private ButtonListBarItem m_item = null;
		private Point m_ptClick;
		private MouseButtons m_button = MouseButtons.None;

		public ButtonListBarItem Item
		{
			get
			{
				return this.m_item;
			}
		}

		public int X
		{
			get
			{
				return m_ptClick.X;
			}
		}

		public int Y
		{
			get
			{
				return m_ptClick.Y;
			}
		}

		public MouseButtons Button
		{
			get
			{
				return this.m_button;
			}
		}

		public ItemClickEventArgs( 
			ButtonListBarItem item, 
			Point ptClick, 
			MouseButtons button
			)
		{
			m_ptClick = ptClick;
			m_item = item;
			m_button = button;
		}
	}

	#endregion

	#region Event Delegates
	public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);
	public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);
	#endregion 



	#region ButtonListBarItems
	public class ButtonListBarItems :
		CollectionBase
	{
		private ButtonListBar m_owner = null;

		public void Add(ButtonListBarItem item)
		{
			item.Owner = m_owner;
			this.InnerList.Add(item);
			m_owner.OnItemChanged(item);
		}

		public void Add(ButtonListBarItem[] items)
		{
			foreach(ButtonListBarItem item in items)
			{
				if (item != null)
				{
					item.Owner = m_owner;
					this.InnerList.Add(item);
				}
			}
			m_owner.OnItemChanged(null);
		}

		public void Insert(
			int index,
			ButtonListBarItem item
			)
		{
			item.Owner = m_owner;
			this.InnerList.Insert(index, item);
			m_owner.OnItemChanged(item);
		}

		public ButtonListBarItem this[int index]
		{
			get
			{
				return (ButtonListBarItem)this.InnerList[index];
			}
		}

		public ButtonListBarItems(
			ButtonListBar owner
			)
		{
			m_owner = owner;
		}
		public ButtonListBarItems(
			ButtonListBar owner, 
			ButtonListBarItem[] items
			) : this(owner)
		{
			foreach (ButtonListBarItem barItem in items)
			{
				barItem.Owner = m_owner;
				this.InnerList.Add(barItem);
			}
			m_owner.OnItemChanged(null);
		}

		public  ButtonListBarItems(
			ButtonListBar owner, 
			ButtonListBarItems items
			) : this(owner)
		{
			foreach (ButtonListBarItem item in items)
			{
				ButtonListBarItem newItem = (ButtonListBarItem)item.Clone();
				newItem.Owner = m_owner;
				this.InnerList.Add(newItem);
			}
			m_owner.OnItemChanged(null);
		}

		protected override void OnRemoveComplete(
			int index,
			object value
			)
		{
			base.OnRemoveComplete(index, value);
			m_owner.OnItemChanged(null);
		}

		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			m_owner.OnItemChanged(null);
		}

		protected override void OnInsertComplete(
			int index,
			object value
			)
		{
			base.OnInsertComplete(index, value);
			ButtonListBarItem item = (ButtonListBarItem)value;
			item.Owner = m_owner;
			m_owner.OnItemChanged(item);
		}

		protected override void OnSetComplete(
			int index,
			object oldValue,
			object newValue
			)
		{
			base.OnSetComplete(index, oldValue, newValue);
			ButtonListBarItem item = (ButtonListBarItem)newValue;
			item.Owner = m_owner;
			m_owner.OnItemChanged(null);
		}


	}
	#endregion

	#region ButtonListBarItem
	public class ButtonListBarItem : 
		ICloneable
	{
		private string m_caption = "";
		private string m_toolTip = "";
		private object m_tag = null;
		private int m_iconIndex = -1;
		private bool m_enabled = true;
		private int m_start = 0;
		private int m_extent = 0;
		private ButtonListBar m_owner = null;
		private bool m_mouseDown = false;
		private bool m_mouseOver = false;
		private bool m_selected = false;

		public string Text
		{
			get
			{
				return m_caption;
			}
			set
			{
				m_caption = value;
				if (m_owner != null)
				{
					m_owner.OnItemChanged(this);
				}
			}
		}

		public string ToolTip
		{
			get
			{
				return m_toolTip;
			}
			set
			{
				m_toolTip = value;
			}
		}

		public object Tag
		{
			get
			{
				return m_tag;
			}
			set
			{
				m_tag = value;
			}
		}

		public int IconIndex
		{
			get
			{
				return m_iconIndex;
			}
			set
			{
				m_iconIndex = value;
				if (m_owner != null)
				{
					m_owner.OnItemChanged(this);
				}
			}
		}

		public bool Enabled
		{
			get
			{
				return m_enabled;
			}
			set
			{
				m_enabled = value;
				if (m_owner != null)
				{
					m_owner.OnItemChanged(this);
				}				
			}
		}

		internal ButtonListBar Owner
		{
			get
			{
				return m_owner;
			}
			set
			{
				m_owner = value;
			}
		}

		internal int Start
		{
			get
			{
				return m_start;
			}
			set
			{
				m_start = value;
			}
		}

		internal int Extent
		{
			get
			{
				return m_extent;
			}
			set
			{
				m_extent = value;
			}
		}

		public bool Selected
		{
			get
			{
				return m_selected;
			}
			set
			{
				if (value != m_selected)
				{
					if (m_owner != null)
					{
						m_selected = m_owner.OnSelectItem(this, value);
						m_owner.OnItemChanged(this);
					}
				}
			}
		}

		internal bool MouseDown
		{
			get
			{
				return m_mouseDown;
			}
			set
			{
				m_mouseDown = value;
			}
		}

		internal bool MouseOver
		{
			get
			{
				return m_mouseOver;
			}
			set
			{
				m_mouseOver = value;
			}
		}

		public object Clone()
		{
			ButtonListBarItem myClone = new ButtonListBarItem( 
				m_caption, 
				m_iconIndex, 
				m_toolTip, 
				m_enabled
				);
			myClone.Tag = m_tag;
			return myClone;
		}

		public ButtonListBarItem() : base()
		{

		}
		public ButtonListBarItem(
			string text
			) : base()
		{
			m_caption = text;
		}
		public ButtonListBarItem(
			string text,
			int iconIndex
			) : this(text)
		{
			m_iconIndex = iconIndex;
		}
		public ButtonListBarItem(
			string text,
			int iconIndex,
			string toolTip
			) : this(text, iconIndex)
		{
			m_toolTip = toolTip;
		}
		public ButtonListBarItem(
			string text,
			int iconIndex,
			string toolTip,
			bool enabled
			) : this(text, iconIndex, toolTip)
		{
			m_enabled = enabled;
		}


	}
	#endregion
}
