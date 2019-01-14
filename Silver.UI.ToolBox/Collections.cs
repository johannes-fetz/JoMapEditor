//----------------------------------------------------------------------------
// File    : Collections.cs
// Date    : 02/10/2005
// Author  : Aju George
// Email   : aju_george_2002@yahoo.co.in ; george.aju@gmail.com
// 
// Updates :
//           See ToolBox.cs
//
// Legal   : See ToolBox.cs
//----------------------------------------------------------------------------

using System;
using System.Collections;

namespace Silver.UI
{
    #region ToolBoxTabCollection Class

    [Serializable]
    public class ToolBoxTabCollection : CollectionBase
    {
        #region Constructor

        #endregion //Constructor

        #region Properties
        public ToolBoxTab this[int index]
        {
            get{return (ToolBoxTab)this.List[index];}
            set{this.List[index]=value;}
        }
        #endregion //Properties

        #region Public Methods
        public int Add(ToolBoxTab tab)
        {
            return this.InnerList.Add(tab);
        }

        public void Insert(int index, ToolBoxTab tab)
        {
            this.InnerList.Insert(index,tab);
        }

        public void Remove(ToolBoxTab tab)
        {
            this.InnerList.Remove(tab);
        }

        public int IndexOf(ToolBoxTab tab)
        {
            return this.InnerList.IndexOf(tab);
        }

        public bool Contains(ToolBoxTab tab)
        {
            return this.InnerList.Contains(tab);
        }

        #endregion //Public Methods
    }

    #endregion //ToolBoxTabCollection Class

    #region ToolBoxItemCollection Class

    [Serializable]
    public class ToolBoxItemCollection : CollectionBase
    {
        #region Constructor

        #endregion //Constructor

        #region Properties
        public ToolBoxItem this[int index]
        {
            get{return (ToolBoxItem)this.List[index];}
            set{this.List[index]=value;}
        }
        #endregion //Properties

        #region Public Methods
        public int Add(ToolBoxItem item)
        {
            return this.InnerList.Add(item);
        }

        public void Insert(int index, ToolBoxItem item)
        {
            this.InnerList.Insert(index,item);
        }

        public void Remove(ToolBoxItem item)
        {
            this.InnerList.Remove(item);
        }

        public int IndexOf(ToolBoxItem item)
        {
            return this.InnerList.IndexOf(item);
        }

        public bool Contains(ToolBoxItem item)
        {
            return this.InnerList.Contains(item);
        }

        #endregion //Public Methods
    }

    #endregion //ToolBoxTabCollection Class

}
