using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoki.Controls
{
    public class BlockMenu : System.Windows.Forms.PictureBox
    {
        internal static int SpliterHeight = 7;
        //flow items: classroom. my evaluation. my students. moment.
        //static items: settings.

        public BlockMenu()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        private System.Collections.Generic.ICollection<BlockMenuItem> flowItems = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.Generic.ICollection<BlockMenuItem> FlowItems
        {
            get
            {
                if (this.flowItems == null)
                {
                    this.flowItems = new System.Collections.Generic.List<BlockMenuItem>();
                }
                return this.flowItems;
            }
            set
            {
                if (this.flowItems != value)
                {
                    this.flowItems = value;

                }
            }
        }

        private System.Collections.Generic.ICollection<BlockMenuItem> staticItems = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.Generic.ICollection<BlockMenuItem> StaticItems
        {
            get
            {
                if (this.staticItems == null)
                {
                    this.staticItems = new System.Collections.Generic.List<BlockMenuItem>();
                }
                return this.staticItems;
            }
            set
            {
                if (this.staticItems != value)
                {
                    this.staticItems = value;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintBackground(e.Graphics);
            PaintSpliter(e.Graphics);
            PaintFlowingItems(e.Graphics);
            PaintStaticItems(e.Graphics);
        }

        private void PaintBackground(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 240, 240, 240)), this.ClientRectangle);
        }

        private void PaintSpliter(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 217, 217, 217)), this.Locations[0]);

            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 245, 245, 245)), 
                new Rectangle(this.Locations[1].Left, this.Locations[1].Top - 1,
                this.Locations[1].Width, 1));
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 217, 217, 217)), this.Locations[1]);
        }

        public void PaintFlowingItems(Graphics g)
        {
            using (Fink.Drawing.HAFGraphics haf = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                //blockMenu中默认加载图片内容设置
                g.DrawImage(ResourceHelper.ClassRoomTemp, new Rectangle(new Point(0, SpliterHeight), ResourceHelper.ClassRoomTemp.Size));
            };
        }

        public void PaintStaticItems(Graphics g)
        {
            if (this.staticItems == null || this.staticItems.Count <= 0)
            {
                return;
            }

            ClcStaticItemsRegions();
            foreach (var item in this.staticItems)
            {
                item.OnPaint(g);
            }
        }


        private void ClcFlowItemsRegions()
        {
            int offSetIndex = this.FlowItems.Count;

            foreach (var item in this.staticItems)
            {
                Rectangle rect = new Rectangle(
                    new Point(0, BlockMenu.SpliterHeight),
                    BlockMenuItem.ItemSize);
                item.ClientRectangle = rect;

                offSetIndex--;
            }
        }

        private void ClcStaticItemsRegions()
        {

            int offSetIndex = this.staticItems.Count;

            foreach (var item in this.staticItems)
            {
                Rectangle rect = new Rectangle(
                    new Point(0, this.ClientRectangle.Bottom - offSetIndex * BlockMenuItem.ItemSize.Height),
                    BlockMenuItem.ItemSize);
                item.ClientRectangle = rect;

                    offSetIndex--;
            }
        }

        private Rectangle clientRectMemory = Rectangle.Empty;
        private Rectangle[] locations = null;
        private Rectangle[] Locations
        {
            get
            {
                if (this.locations == null || this.ClientRectangle != clientRectMemory)
                {
                    Rectangle[] rects = new Rectangle[2];

                    //spliter top
                    rects[0] = new Rectangle(0, 0, this.ClientRectangle.Width, SpliterHeight);

                    //spliter center
                    rects[1] = new Rectangle(0, this.ClientRectangle.Height - BlockMenuItem.ItemSize.Height - SpliterHeight, this.ClientRectangle.Width, SpliterHeight);

                    this.clientRectMemory = this.ClientRectangle;
                    this.locations = rects;
                }

                return this.locations;
            }
        }



    }
}
