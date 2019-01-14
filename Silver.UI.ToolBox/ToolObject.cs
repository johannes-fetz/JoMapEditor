//----------------------------------------------------------------------------
// File    : ToolObject.cs
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
    public class ToolObject
    {
        #region Protected Attributes
        protected Rectangle _rectangle;
        protected string    _toolTip;
        protected bool      _forceCaptionToolTip    = false;
        protected bool      _captionChecked;

        #endregion //Protected Attributes

        #region Properties
        [ReadOnly(true), Browsable(false), XmlIgnore]
        public virtual Rectangle Rectangle
        {
            get{return _rectangle;}
            set
            {
                if(value != _rectangle)
                {
                    if(value.Size != _rectangle.Size)
                    {
                        _captionChecked = false;
                    }
                    _rectangle = value;
                }
            }
        }

        [Browsable(false), XmlIgnore]
        public virtual Point Location
        {
            get{return _rectangle.Location;}
            set{_rectangle.Location=value;}
        }

        [Browsable(false), XmlIgnore]
        public virtual Size Size
        {
            get{return _rectangle.Size;}
            set
            {
                if(value != _rectangle.Size)
                {
                    _captionChecked = false;
                    _rectangle.Size = value;
                }
            }
        }

        [Browsable(false), XmlIgnore]
        public virtual int X
        {
            get{return _rectangle.X;}
            set{_rectangle.X=value;}
        }

        [Browsable(false), XmlIgnore]
        public virtual int Y
        {
            get{return _rectangle.Y;}
            set{_rectangle.Y=value;}
        }

        [Browsable(false), XmlIgnore]
        public virtual int Width
        {
            get{return _rectangle.Width;}
            set
            {
                if(value != _rectangle.Width)
                {
                    _captionChecked  = false;
                    _rectangle.Width = value;
                }
            }
        }

        [Browsable(false), XmlIgnore]
        public virtual int Height
        {
            get{return _rectangle.Height;}
            set
            {
                if(value != _rectangle.Height)
                {
                    _captionChecked   = false;
                    _rectangle.Height = value;
                }
            }
        }

        [Browsable(false), XmlIgnore]
        public virtual int Left
        {
            get{return _rectangle.Left;}
        }

        [Browsable(false), XmlIgnore]
        public virtual int Right
        {
            get{return _rectangle.Right;}
        }

        [Browsable(false), XmlIgnore]
        public virtual int Top
        {
            get{return _rectangle.Top;}
        }

        [Browsable(false), XmlIgnore]
        public virtual int Bottom
        {
            get{return _rectangle.Bottom;}
        }

        [Category("General")]
        public virtual string ToolTip
        {
            get{return _toolTip;}
            set{_toolTip=value;}
        }

        [Category("General")]
        public virtual string Caption
        {
            get{return null;}
            set{;}
        }

        [Category("General"), Browsable(false), XmlIgnore]
        public virtual bool FullyVisible
        {
            get{return true;}
        }

        [Category("General"), Browsable(false), XmlIgnore]
        public bool ForceCaptionToolTip
        {
            get{return _forceCaptionToolTip;}
        }

        [Category("General"), Browsable(false), XmlIgnore]
        public bool CaptionChecked
        {
            get{return _captionChecked;}
        }

        #endregion //Properties

        #region Construction
        public ToolObject()
        {
            _rectangle = new Rectangle(0,0,0,0);
            _toolTip   = "";
        }

        public ToolObject(Rectangle rectangle)
        {
            _rectangle = new Rectangle(rectangle.Location,rectangle.Size);
            _toolTip   = "";
        }
        #endregion //Construction
    }
}
//----------------------------------------------------------------------------
