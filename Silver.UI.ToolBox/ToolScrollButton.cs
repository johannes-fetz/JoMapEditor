//----------------------------------------------------------------------------
// File    : ToolScrollButton.cs
// Date    : 17/09/2004
// Author  : Aju George
// Email   : aju_george_2002@yahoo.co.in ; george.aju@gmail.com
// 
// Updates :
//           See ToolBox.cs
//
// Legal   : See ToolBox.cs
//----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace Silver.UI
{
    [Serializable]
    public class ToolScrollButton : ToolObject
    {
        #region Private Attributes

        private bool            _mouseDown;
        private bool            _mouseHover;
        private bool            _enabled;
        private ScrollDirection _direction;

        #endregion //Private Attributes

        #region Properties

        [Browsable(false), XmlIgnore]
        public bool MouseDown
        {
            get{return _mouseDown;}
            set{_mouseDown=value;}
        }

        [Browsable(false), XmlIgnore]
        public bool MouseHover
        {
            get{return _mouseHover;}
            set{_mouseHover=value;}
        }

        [Category("General")]
        public bool Enabled
        {
            get{return _enabled;}
            set
            {
                _enabled = value;
            }
        }

        [Category("General")]
        public ScrollDirection ScrollDirection
        {
            get{return _direction;}
            set{_direction=value;}
        }

        #endregion //Properties

        #region Construction
        public ToolScrollButton(ScrollDirection direction, int width, int height)
        {
            _rectangle = new Rectangle(0,0,width,height);
            _direction = direction;
            _toolTip   = (ScrollDirection.Up == direction) ? "Scroll Up" : "Scroll Down";
        }
        #endregion //Construction

        #region Public Methods

        public void Paint(Graphics g, Rectangle clipRect, bool ctrlEnabled)
        {
            Rectangle rect   = Rectangle.Empty;
            Pen       pen    = null;
            int       length = 0;
            Point     p1;
            Point     p2;

            if(_rectangle.IntersectsWith(clipRect))
            {
                pen  = (!_enabled || !ctrlEnabled) ? SystemPens.GrayText : Pens.Black;
                rect = _rectangle;

                ToolBox.DrawBorders(g,rect,_mouseDown);

                rect.Inflate(-rect.Width/3,-rect.Height/3);

                if(0 != rect.Width%2)
                {
                    rect.Width--;
                }

                if(0 != rect.Height%2)
                {
                    rect.Height--;
                }

                rect.Width  = Math.Max(8,rect.Width);
                rect.Height = Math.Max(8,rect.Height);

                rect.X -= rect.Width/4;
                //rect.Y -= rect.Height/4;

                if(_mouseDown)
                {
                    rect.Offset(1,1);
                }

                //g.DrawRectangle(Pens.Red,rect);

                //rect.X = _rectangle.X + (_rectangle.Width - rect.Width)/2;
                length = rect.Width;
                p1     = rect.Location;
                p2     = rect.Location;

                if(ScrollDirection.Down == _direction)
                {
                    p2.X = rect.Right;

                    while(0 <= length)
                    {
                        g.DrawLine(pen,p1,p2);
                        p1.X++;p2.X--;
                        p1.Y++;p2.Y++;
                        length -= 2;
                    }

                    p1.X = rect.Left+rect.Width/2;
                    p1.Y = rect.Top;
                    p2.X = p1.X;
                    p2.Y = rect.Top+rect.Height/2;

                    g.DrawLine(pen,p1,p2);
                }
                else if(ScrollDirection.Up == _direction)
                {
                    p1.X = rect.Left+rect.Width/2;
                    p1.Y = rect.Top;
                    p2.X = p1.X;
                    p2.Y = p1.Y;

                    while(0 <= length)
                    {
                        g.DrawLine(pen,p1,p2);
                        p1.X--;p2.X++;
                        p1.Y++;p2.Y++;
                        length -= 2;
                    }

                    p1.X = rect.Left+rect.Width/2;
                    p1.Y = rect.Bottom-rect.Height/2;
                    p2.X = p1.X;
                    p2.Y = rect.Top;

                    g.DrawLine(pen,p1,p2);

                }

            }
        }

        public bool HitTest(int x, int y)
        {
            return _rectangle.Contains(x,y);
        }

        #endregion //Public Methods
    }
}

//----------------------------------------------------------------------------