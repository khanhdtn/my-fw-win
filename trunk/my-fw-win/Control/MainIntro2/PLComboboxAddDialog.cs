using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Accessibility;
using System.Data;
using DevExpress.XtraTreeList;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using System.Windows.Forms;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Control cho phép chọn và thêm bằng 1 hợp thoại
    /// </summary>
    public class PLComboboxAddDialog : ComboBoxEdit , ISelectionControl
    {
        bool isUp = false;
        bool isDown = false;
        bool isSpace = false;

        public PLComboboxAddDialog()
        {
            this.Properties.PopupSizeable = true;
            this.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.Properties.HotTrackItems = false;
            this.Properties.ShowPopupShadow = true;
            this.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                try
                {
                    if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
                    {
                        e.Handled = true;
                        this.ShowPopup();
                        this.PopupForm.ListBox.SetSelected(2, true);
                        this.PopupForm.ListBox.Focus();
                    }
                    //Tranh truong hop AutoComplete
                    else if (e.KeyCode == Keys.Space)
                        isSpace = true;
                    else
                        isSpace = false;
                        
                }
                catch
                {
                    this.ShowPopup();
                }

            };
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            
            this.PopupForm.ListBox.SelectedIndexChanged += new EventHandler(ListBox_SelectedIndexChanged);
            this.PopupForm.ListBox.KeyDown += new KeyEventHandler(ListBox_KeyDown);
            this.PopupForm.SizeChanged += new EventHandler(PopupForm_SizeChanged);
        }

        void PopupForm_SizeChanged(object sender, EventArgs e)
        {
            ResizeLine();   
        }

        private void ResizeLine()
        {
            string line = "_";
            for (int i = 0; i < this.PopupForm.Width/7+2; i++)
                line += "_";
            this.Properties.Items.RemoveAt(1);
            this.Properties.Items.Insert(1, line);
            this.ShowPopup();
        }

        void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Up)
            {
                isUp = true;
                isDown = false;
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                isUp = false;
                isDown = true;
            }
            else
            {
                isUp = false;
                isDown = false;
            }
        }

        void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PopupForm.ListBox.SelectedIndex == 1)
            {
                this.PopupForm.ListBox.SetSelected(1, false);
                if (isUp)
                {
                    this.PopupForm.ListBox.SetSelected(0, true);
                    isUp = false;
                }
                else if(isDown)
                {
                    this.PopupForm.ListBox.SetSelected(2, true);
                    isDown = false;
                }
            }
            else if (this.PopupForm.ListBox.SelectedIndex == 0 && isSpace)
            {
                this.PopupForm.ListBox.SetSelected(0, false);
                isSpace = false;
            }
        }
        protected override void OnPopupSelectedIndexChanged()
        {
            if(this.PopupForm.ListBox.SelectedIndex != 0)
                base.OnPopupSelectedIndexChanged();
        }
        private void AddLine()
        {
            string line="_";
            for (int i = 0; i < this.Width /6; i++)
                line += "_";
            this.Properties.Items.Add(new ItemData(-1, line));
        }
        private void AddItem(DataTable dt, string ValueField, string DisplayField)
        {
            this.Properties.Items.Add(new ItemData(-2,"       <<Thêm Mới>>        "));
            AddLine();
            foreach (DataRow dr in dt.Rows)
                this.Properties.Items.Add(
                    new ItemData(HelpNumber.ParseInt64(dr[ValueField].ToString()),dr[DisplayField].ToString()));
        }

        public void _init(frmAddItem form , string TableName , string ValueField , string DisplayField)
        {
            DataTable dt = HelpDanhMucDB.LoadData(TableName , ValueField , DisplayField);
            AddItem(dt , ValueField , DisplayField);
            this.SelectedIndexChanged += delegate(object sender , EventArgs e)
            {
                if (_getSelectedID() == -2)
                {
                    this.Text = "";
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this.Properties.Items.Add(new ItemData(form._getId(), form._getValue().ToString().Trim()));
                        this.SelectedIndex = this.Properties.Items.Count - 1;
                    }
                }
            };
          
        }

        #region ISelectionControl Members

        public long _getSelectedID()
        {
            try
            {
                return ((ItemData)this.EditValue).ID;
            }
            catch { return -3; }
        }
        
        #endregion        
    
        #region IPLControl Members

        public void _init()
        {
            
        }

        public void _refresh()
        {
            
        }
        
        public void _setSelectedID(long id)
        {
            
        }

        #endregion

        #region ISelectionControl Members


        public void _refresh(object NewSrc)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public interface IAddItem
    {
        long _getId();
        object _getValue();
    }
}
