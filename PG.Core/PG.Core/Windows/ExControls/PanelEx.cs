using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PG.Core.Windows.ExControls
{
    public class PanelEx: System.Windows.Forms.Panel
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            
            if (TabRenderer.IsSupported && Application.RenderWithVisualStyles)
            {
                TabRenderer.DrawTabPage(e.Graphics, this.ClientRectangle);
            }
            else
            {
                base.OnPaintBackground(e);
                ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle, Border3DStyle.Raised);
            }
        }


    }
}
