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
using System.Windows.Forms;
using System.Data.Common;

namespace ProtocolVN.Framework.Win
{
    public class RepositoryDMThamTri : RepositoryItemComboBox
    {
        private string tableName;
        private string catalogName;
        public string TableName
        {
            set
            {
                tableName = value;
            }
        }
        public string CatalogName
        {
            set
            {
                catalogName = value;
            }
        }

        private const string ControlName = "RepositoryItemDMThamTri";
        static RepositoryDMThamTri()
        {
            Register();
        }
        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(ControlName,
                                        typeof(ComboBoxEdit), typeof(RepositoryDMThamTri),
                                       typeof(DevExpress.XtraEditors.ViewInfo.ComboBoxViewInfo),
                                       new DevExpress.XtraEditors.Drawing.ButtonEditPainter(), true, null));
        }
        
        public override string EditorTypeName
        {
            get
            {
                return ControlName;
            }
        }
        public override bool ImmediatePopup
        {
            get
            {
                return true;
            }
            set
            {
                base.ImmediatePopup = value;
            }
        }
        public override bool HotTrackItems
        {
            get
            {
                return true;
            }
            set
            {
                base.HotTrackItems = value;
            }
        }


        public RepositoryDMThamTri(string TableName , string TenDanhMuc)
        {
            this.tableName = TableName;
            this.catalogName = TenDanhMuc;
            this.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                ComboBoxEdit comboBox = (ComboBoxEdit)sender;
                if (e.KeyData == (Keys.Insert | Keys.Control))
                {
                    if (!comboBox.Text.Equals(string.Empty) && !Exist(comboBox))
                    {
                        if (PLDMThamTri.InsertItem(this.tableName, this.catalogName, comboBox.EditValue.ToString()))
                            this.Properties.Items.Add(comboBox.EditValue);
                        comboBox.ClosePopup();
                    }                    
                }
                else if (e.KeyData == (Keys.Delete | Keys.Control))
                {
                    if (PLDMThamTri.DeleteItem(this.tableName, this.catalogName, comboBox.EditValue.ToString()))
                    {
                        this.Properties.Items.Remove(comboBox.EditValue);
                        comboBox.Text = String.Empty;
                    }
                }
            };

            DataTable dt = PLDMThamTri.LoadItems(this.tableName, this.catalogName);
            this.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
                this.Properties.Items.Add(dr[0].ToString());
        }
       
        private bool Exist(ComboBoxEdit comboBox)
        {
            for (int i = 0 ; i < this.Properties.Items.Count ; i++)
            {
                object value = this.Properties.Items[i];
                if(comboBox.Text.Trim().Equals(value))
                    return true;
            }
            return false;
        }
        
        public void _refresh()
        {
            DataTable dt;
            dt = PLDMThamTri.LoadItems(this.tableName, this.catalogName);
            this.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
                this.Properties.Items.Add(dr[0].ToString());
        }        
    }
}
