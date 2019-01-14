//----------------------------------------------------------------------------
// File    : Delegates.cs
// Date    : 01/10/2005
// Author  : Aju George
// Email   : aju_george_2002@yahoo.co.in ; george.aju@gmail.com
// 
// Updates :
//           See ToolBox.cs
//
// Legal   : See ToolBox.cs
//----------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using System.Xml;

namespace Silver.UI
{
    #region Delegates
    public delegate void TabSelectionChangedHandler  (ToolBoxTab  sender, EventArgs e                );
    public delegate void TabMouseEventHandler        (ToolBoxTab  sender, MouseEventArgs e           );
    public delegate void ItemSelectionChangedHandler (ToolBoxItem sender, EventArgs e                );
    public delegate void ItemMouseEventHandler       (ToolBoxItem sender, MouseEventArgs e           );
    public delegate void ItemKeyEventHandler         (ToolBoxItem sender, KeyEventArgs e             );
    public delegate void ItemKeyPressEventHandler    (ToolBoxItem sender, KeyPressEventArgs e        );
    public delegate void DragDropFinishedHandler     (ToolBoxItem sender, DragDropEffects e          );
    public delegate void RenameFinishedHandler       (ToolBoxItem sender, RenameFinishedEventArgs e  );
    public delegate void XmlSerializerHandler        (ToolBoxItem sender, XmlSerializationEventArgs e);
    public delegate void PreDragDropHandler          (ToolBoxItem sender, PreDragDropEventArgs      e);

    public delegate void LayoutFinished();
    #endregion //Delegates

    #region Classes

    public class RenameFinishedEventArgs : EventArgs
    {
        #region Private Attributes
        private string _newCaption;
        private bool   _cancel;
        private bool   _continueRenaming;
        private bool   _escapeKeyPressed;
        #endregion //Private Attributes

        #region Properties
        public string NewCaption
        {
            get{return _newCaption;}
        }

        public bool Cancel
        {
            get{return _cancel;}
            set{_cancel=value;}
        }

        public bool ContinueRenaming
        {
            get{return _continueRenaming;}
            set{_continueRenaming=value;}
        }

        public bool EscapeKeyPressed
        {
            get{return _escapeKeyPressed;}
        }

        #endregion //Properties

        #region Construction
        public RenameFinishedEventArgs(string newCaption, bool escapeKeyPressed)
        {
            _newCaption       = newCaption;
            _escapeKeyPressed = escapeKeyPressed;
        }
        #endregion //Construction
    }

    public class XmlSerializationEventArgs : EventArgs
    {
        #region Private Attributes
        private bool    _isLoading;
        private object  _object;
        private XmlNode _xmlNode;
        #endregion //Private Attributes

        #region Properties
        public bool IsLoading
        {
            get{return _isLoading;}
        }

        public bool IsSaving
        {
            get{return !_isLoading;}
        }

        public XmlNode Node
        {
            get{return _xmlNode;}
        }

        public object Object
        {
            get{return _object;}
            set{_object=value;}
        }

        #endregion //Properties

        #region Construction
        public XmlSerializationEventArgs(object o, XmlNode node, bool isLoading)
        {
            _object    = o;
            _xmlNode   = node;
            _isLoading = isLoading;
        }
        #endregion //Construction

    }

    public class PreDragDropEventArgs : EventArgs
    {
        #region Private Attributes
        private DataObject   _dataObject;
        private object       _object;
        #endregion //Private Attributes

        #region Properties

        public DataObject DataObject
        {
            get{return _dataObject;}
        }

        public object DragObject
        {
            get{return _object;}
        }

        #endregion //Properties
        #region Construction
        public PreDragDropEventArgs(DataObject d, object o)
        {
            _dataObject = d;
            _object     = o;
        }
        #endregion //Construction

    }

    #endregion //Classes
}

//----------------------------------------------------------------------------
