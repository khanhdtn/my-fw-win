using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.Accessibility;
using System.Data;
using System.Data.Common;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

using DevExpress.XtraTreeList;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public struct ParamInfo
    {
        public static string tableName;
        public static string valueField;
        public static string displayField;
        public static bool startWith;
        public static bool isShowButton;
    }
    
    [UserRepositoryItem("Register")]
    public class RepositoryComboboxAuto : RepositoryItemComboBox
    {
        public const string ComboBoxName = "RepositoryComboboxAuto";
        public override string EditorTypeName
        {
            get
            {
                return ComboBoxName;
            }
        }
        public override bool AutoComplete
        {
            get
            {
                return true;
            }
        }
        public override bool HotTrackItems
        {
            get
            {
                return true;
            }
        }
        public override bool ImmediatePopup
        {
            get
            {
                return true;
            }
        }

        static RepositoryComboboxAuto()
        {
            Register();
        }
        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(ComboBoxName,
                                        typeof(RepositoryItemComboboxFindCtrl), typeof(RepositoryComboboxAuto),
                                        typeof(DevExpress.XtraEditors.ViewInfo.ComboBoxViewInfo),
                                        new DevExpress.XtraEditors.Drawing.ButtonEditPainter(),
                                        true, null, typeof(PopupEditAccessible)));
        }

        public RepositoryComboboxAuto()
        {
            this.KeyDown += delegate(object sender , System.Windows.Forms.KeyEventArgs e)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    RepositoryItemComboboxFindCtrl comboBoxEdit = ((RepositoryItemComboboxFindCtrl)(sender));
                    this.Items.Clear();
                    for (int i = 0 ; i < comboBoxEdit.Properties.Items.Count ; i++)
                        this.Items.Add(comboBoxEdit.Properties.Items[i]);
                }
            };
        }

        public RepositoryComboboxAuto(GridView gridView , string idField , string tableName , string valueField , string displayField , bool startWith):this()
        {
            ParamInfo.tableName = tableName;
            ParamInfo.valueField = valueField;
            ParamInfo.displayField = displayField;
            ParamInfo.startWith = startWith;
            long idValue = -1;
            gridView.CellValueChanging+= delegate(object sender , DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                if (gridView.DataSource != null &&
                      !((DataView)gridView.DataSource).Table.Columns.Contains(idField + displayField))
                {
                    ((DataView)gridView.DataSource).Table.Columns.Add(new DataColumn(idField + displayField));
                }

                if ((idField+displayField).Equals(e.Column.FieldName))
                {
                    if (e.Value is ItemData)
                        idValue = ((ItemData)e.Value).ID;
                    else
                        idValue = -1;

                    if (e.RowHandle < 0)
                        gridView.SetRowCellValue(e.RowHandle , idField , idValue);
                    else
                    {
                        DataRow dr = gridView.GetDataRow(e.RowHandle);
                        dr[idField] = idValue;
                    }
                    
                }
            };
            gridView.InitNewRow += delegate(object sender , InitNewRowEventArgs e)
            {
                gridView.SetRowCellValue(e.RowHandle , idField , idValue);
            };
          
        }

        public RepositoryComboboxAuto(TreeList treeList , string idField , string tableName , string valueField, string displayField , bool startWith):this()
        {
            ParamInfo.tableName = tableName;
            ParamInfo.valueField = valueField;
            ParamInfo.displayField = displayField;
            ParamInfo.startWith = startWith;

            treeList.GotFocus += delegate(object sender , EventArgs e)
            {
                if (treeList.DataSource != null &&
                    !((DataTable)treeList.DataSource).Columns.Contains(idField + displayField))
                {
                    ((DataTable)treeList.DataSource).Columns.Add(new DataColumn(idField + displayField));
                }
            };
            treeList.CellValueChanging += delegate(object sender , DevExpress.XtraTreeList.CellValueChangedEventArgs e)
            {
                if ((idField + displayField).Equals(e.Column.FieldName))
                {
                    DataRowView rowView = (DataRowView)treeList.GetDataRecordByNode(e.Node);
                    DataRow dr = rowView.Row;
                    if (e.Value is ItemData)
                        dr[idField] = ((ItemData)e.Value).ID;
                    else
                        dr[idField] = -1;
                }
            };
        }
    }

    public class RepositoryItemComboboxFindCtrl : ComboBoxEdit
    {
        static RepositoryItemComboboxFindCtrl()
        {
            RepositoryComboboxAuto.Register();
        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.SelectedText == "")
                {
                    e.Handled = true;
                    string command;
                    string text = this.Text;
                    
                    if (!this.Text.Equals(String.Empty))
                    {
                        if (ParamInfo.startWith)
                            command = "SELECT " + ParamInfo.valueField + "," + ParamInfo.displayField + " FROM " + ParamInfo.tableName + " WHERE " + ParamInfo.displayField + " like '" + this.Text + "%'";
                        else
                            command = "SELECT " + ParamInfo.valueField + "," + ParamInfo.displayField + " FROM " + ParamInfo.tableName + " WHERE " + ParamInfo.displayField + " like '%" + this.Text + "%'";
                    }
                    else
                        command = "SELECT " + ParamInfo.valueField + "," + ParamInfo.displayField + " FROM " + ParamInfo.tableName + " WHERE " + ParamInfo.displayField + " like ''";

                    DataSet ds = DABase.getDatabase().LoadDataSet(command, "DANHMUC");
                    DataTable dataSource = ds.Tables[0];

                    this.Properties.Items.Clear();
                    foreach (DataRow dr in dataSource.Rows)
                        this.Properties.Items.Add(new ItemData(HelpNumber.ParseInt64(dr[ParamInfo.valueField].ToString()), dr[ParamInfo.displayField].ToString()));

                    this.ShowPopup();
                }
            }
            base.OnKeyDown(e);
        }
    }
}
