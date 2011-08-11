using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using System.Drawing;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ProtocolVN.Framework.Win
{

    public class HelpTreeList : HelpTree {

    }

    [Obsolete("Dùng lớp HelpTreeList thay thế")]
    public class HelpTree : HelpTreeColumn
    {
        public static void AddContextMenu(TreeList tree, List<ItemInfo> items)
        {
            BarManager manager = new BarManager(); ;
            PopupMenu menu = new PopupMenu();

            if (items == null) return;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Per != null)
                    if (ApplyPermissionAction.checkPermission(items[i].Per) == null ||
                       ApplyPermissionAction.checkPermission(items[i].Per) == false)
                    {
                        continue;
                    }

                Image image = ResourceMan.getImage16(items[i].Image);
                BarItem item = new BarButtonItem();
                item.Caption = items[i].Caption;
                item.Name = i.ToString();
                item.Glyph = image;
                manager.Items.Add(item);
                menu.ItemLinks.Add(manager.Items[i]);
                DelegationLib.CallFunction_MulIn_NoOut del = items[i].Delegates;
                item.ItemClick += delegate(object sender, ItemClickEventArgs e)
                {
                    string name = item.Name;
                    List<object> objs = new List<object>();
                    for (int index = 0; index <= tree.Selection.Count; index++)
                    {
                        TreeListNode row = tree.Selection[index];
                        objs.Add(row);
                    }
                    del(objs);
                };
            }

            tree.MouseUp += delegate(object sender, MouseEventArgs e)
            {
                if ((e.Button & MouseButtons.Right) != 0 && tree.ClientRectangle.Contains(e.X, e.Y))
                {
                    menu.ShowPopup(manager, Control.MousePosition);
                }
                else
                {
                    menu.HidePopup();
                }
            };

            tree.MouseLeave += delegate(object sender, EventArgs e)
            {
                menu.HidePopup();
            };
        }

        public delegate void ModifyMenu(List<ItemInfo> lstItem, List<DataRow> rowSelect);
        public static void AddPopupMenuExt(TreeList treeList, List<ItemInfo> lstItem, ModifyMenu function)
        {
            HelpTree.AddContextMenu(treeList, lstItem);//return popupMenu
            PopupMenu menu = new PopupMenu();
            menu.BeforePopup += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                List<DataRow> lstRow = new List<DataRow>();
                foreach (TreeListNode node in treeList.Selection)
                {
                    DataRow dr = ((DataRowView)treeList.GetDataRecordByNode(node)).Row;
                    lstRow.Add(dr);
                }
                function(lstItem, lstRow);
            };
        }

        public static void ShowFilter(
            TreeList treeList, bool readOnly, string idField, 
            string idParent, FilterConditionEnum filterCondition)
        {
            object prevalue = null;
            TreeListColumn currentColumn = null;
            bool Flag = false;
            Dictionary<string, object> preValue = new Dictionary<string, object>();
            Dictionary<string, int> indexCondition = new Dictionary<string, int>();

            //Chèn row filter vào treelist 
            DataTable dtSource = (DataTable)treeList.DataSource;
            DataRow filterRow = dtSource.NewRow();
            foreach (DataColumn column in dtSource.Columns)
            {
                try
                {
                    if (column.ColumnName != idField && column.ColumnName != idParent)
                    {
                        try
                        {
                            filterRow[column.ColumnName] = "";
                        }
                        catch
                        {
                            filterRow[column.ColumnName] = DBNull.Value;
                        }
                        
                    }
                }
                catch (Exception ex) { PLException.AddException(ex); }
            }
            filterRow[idField] = long.MinValue;
            filterRow[idParent] = long.MinValue;
            dtSource.Rows.InsertAt(filterRow, 0);

            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsBehavior.AutoSelectAllInEditor = false;
            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsBehavior.EnterMovesNextColumn = true;
            treeList.ExpandAll();

            if (readOnly)
            {
                treeList.AfterFocusNode += delegate(object sender, NodeEventArgs e)
                {
                    if (e.Node.Id == 0)
                        treeList.OptionsBehavior.Editable = true;
                    else
                        treeList.OptionsBehavior.Editable = false;
                };
            }
            else
                treeList.OptionsBehavior.Editable = true;

            treeList.FocusedNodeChanged += delegate(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
            {
                try
                {
                    if (e.Node.Id == 0)
                        treeList.OptionsBehavior.Editable = true;
                    else
                        treeList.OptionsBehavior.Editable = false;
                }
                catch { }
            };
            treeList.FocusedColumnChanged += delegate(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
            {
                if (Flag)
                {
                    Flag = false;
                    treeList.FocusedColumn = currentColumn;
                    treeList.ShowEditor();
                }
            };
            treeList.ValidateNode += delegate(object sender, ValidateNodeEventArgs e)
            {
                try
                {
                    if (e.Node.Id == 0)
                        e.Valid = true;
                }
                catch { }
            };
            treeList.CellValueChanged += delegate(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
            {
                try
                {
                    if (e.Node.Id == 0)
                    {
                        Flag = true;
                        currentColumn = e.Column;
                        object value;
                        int index;

                        preValue.TryGetValue(e.Column.FieldName, out value);
                        indexCondition.TryGetValue(e.Column.FieldName, out index);

                        treeList.FilterConditions[index].Value1 = prevalue;
                        treeList.FilterConditions[index].Visible = true;
                        
                        treeList.FilterConditions[index].Value1 = e.Value;
                        treeList.FilterConditions[index].Visible = false;
                        
                        e.Node[e.Column.FieldName] = e.Value;
                        //treeList.ShowEditor();
                        //SendKeys.Send("{END}");

                        if (preValue.ContainsKey(e.Column.FieldName))
                        {
                            preValue[e.Column.FieldName] = e.Value;
                        }
                        else
                        {
                            preValue.Add(e.Column.FieldName, e.Value);
                        }
                    }
                }
                catch { }
            };
            treeList.FilterNode += delegate(object sender, FilterNodeEventArgs e)
            {
                try
                {
                    if (e.Node.Id == 0)
                        e.Handled = true;
                    else if (e.Node.Level == 0)
                        e.Handled = true;
                }
                catch { }
            };

            //Gán điều kiện lọc cho các column
            int numColumn = treeList.Columns.Count;
            try
            {
                for (int i = 0; i < numColumn; i++)
                {
                    TreeListColumn tc = treeList.Columns[i];
                    treeList.FilterConditions.Add(new FilterCondition(filterCondition, tc, null, null, true));
                    try
                    {
                        indexCondition.Add(tc.FieldName, i);
                        preValue.Add(tc.FieldName, null);
                    }
                    catch { }
                }
            }
            catch { }
        }

        public static void ShowFilter(TreeList Tree)
        {
        }
        public static void HideFilter(TreeList Tree)
        {
        }

        public static bool exportFile(TreeList treeList, String ext)
        {
            bool flag = false;
            bool succ = false;
            string filePath = null;
            try
            {
                SaveFileDialog f = new SaveFileDialog();
                f.Title = "Chọn tên tập tin muốn lưu";
                if (ext.Equals("xls"))
                    f.Filter = "Excel 97 - 2003 files (*.xls)|*.xls";
                else if (ext.Equals("xlsx"))
                    f.Filter = "Excel 2007 files (*.xlsx)|*.xlsx";
                else if (ext.Equals("pdf"))
                    f.Filter = "PDF (*.pdf)|*.pdf";
                else if (ext.Equals("htm"))
                    f.Filter = "Web Pages (*.htm)|*.htm";
                else if (ext.Equals("rtf"))
                    f.Filter = "Rich Text files (*.rtf)|*.rtf";

                f.ShowDialog();

                if (f.FileName != "")
                {
                    filePath = f.FileName;

                    if (FrameworkParams.wait == null)
                    {
                        FrameworkParams.wait = new WaitingMsg();
                        flag = true;
                    }

                    if (ext.Equals("xls"))
                    {
                        treeList.ExportToXls(f.FileName);
                        succ = true;
                    }
                    else if (ext.Equals("xlsx"))
                    {
                        treeList.ExportToXlsx(f.FileName);
                        succ = true;
                    }
                    else if (ext.Equals("pdf"))
                    {
                        treeList.ExportToPdf(f.FileName);
                        succ = true;
                    }
                    else if (ext.Equals("rtf"))
                    {
                        treeList.ExportToRtf(f.FileName);
                        succ = true;
                    }
                    else if (ext.Equals("htm"))
                    {
                        treeList.ExportToHtml(f.FileName);
                        succ = true;
                    }
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                return false;
            }
            finally
            {
                if (FrameworkParams.wait != null && flag == true)
                    FrameworkParams.wait.Finish();
                if (succ == true)
                {
                    if (PLMessageBox.ShowConfirmMessage("Bạn có muốn mở tập tin này không?") == DialogResult.Yes)
                    {
                        if (!HelpFile.OpenFile(filePath))
                            HelpMsgBox.ShowNotificationMessage("Mở tập tin không thành công");
                    }
                }
            }
            return true;
        }

        public static ToolStripDropDownButton addXuatRaFileItem(ToolStrip toolStripBar, TreeList treeList)
        {
            toolStripBar.SuspendLayout();

            ToolStripDropDownButton xuatRaFile = new System.Windows.Forms.ToolStripDropDownButton();
            xuatRaFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            xuatRaFile.Image = FWImageDic.EXPORT_TO_FILE_IMAGE20;
            xuatRaFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            xuatRaFile.Name = "xuatRaFile";
            xuatRaFile.Size = new System.Drawing.Size(200, 20);
            xuatRaFile.Text = "Xuất ra file";

            ToolStripMenuItem itemXuatRaExcel2007 = new ToolStripMenuItem("Xuất ra file Excel 2007");
            itemXuatRaExcel2007.Name = "itemXuatRaExcel2007";
            itemXuatRaExcel2007.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaExcel2007.Size = new System.Drawing.Size(200, 20);
            itemXuatRaExcel2007.Click += delegate(object sender, EventArgs e)
            {
                HelpTreeList.exportFile(treeList, "xlsx");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaExcel2007);

            ToolStripMenuItem itemXuatRaExcel2003 = new ToolStripMenuItem("Xuất ra file Excel 97 - 2003");
            itemXuatRaExcel2003.Name = "itemXuatRaExcel2003";
            itemXuatRaExcel2003.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaExcel2003.Click += delegate(object sender, EventArgs e)
            {
                HelpTreeList.exportFile(treeList, "xls");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaExcel2003);

            ToolStripMenuItem itemXuatRaPDF = new ToolStripMenuItem("Xuất ra file PDF");
            itemXuatRaPDF.Name = "itemXuatRaPDF";
            itemXuatRaPDF.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaPDF.Click += delegate(object sender, EventArgs e)
            {
                HelpTreeList.exportFile(treeList, "pdf");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaPDF);

            ToolStripMenuItem itemXuatRaHTML = new ToolStripMenuItem("Xuất ra file HTML");
            itemXuatRaHTML.Name = "itemXuatRaHTML";
            itemXuatRaHTML.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaHTML.Click += delegate(object sender, EventArgs e)
            {
                HelpTreeList.exportFile(treeList, "htm");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaHTML);

            ToolStripMenuItem itemXuatRaRTF = new ToolStripMenuItem("Xuất ra file RTF");
            itemXuatRaRTF.Name = "itemXuatRaRTF";
            itemXuatRaRTF.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaRTF.Click += delegate(object sender, EventArgs e)
            {
                HelpTreeList.exportFile(treeList, "rtf");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaRTF);

            toolStripBar.Items.Add(xuatRaFile);
            toolStripBar.ResumeLayout(false);
            toolStripBar.PerformLayout();

            return xuatRaFile;
        }

        public static ToolStripDropDownButton addInLuoiItem(ToolStrip toolStripBar, TreeList treeList)
        {
            toolStripBar.SuspendLayout();

            ToolStripSeparator InLuoiSep = new System.Windows.Forms.ToolStripSeparator();
            InLuoiSep.Name = "InLuoiSep";
            InLuoiSep.Size = new System.Drawing.Size(200, 20);

            ToolStripDropDownButton inLuoi = new System.Windows.Forms.ToolStripDropDownButton();
            inLuoi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            inLuoi.Image = FWImageDic.PRINT_IMAGE20;
            inLuoi.ImageTransparentColor = System.Drawing.Color.Magenta;
            inLuoi.Name = "inLuoi";
            inLuoi.Size = new System.Drawing.Size(200, 20);
            inLuoi.Text = "In danh sách";

            ToolStripMenuItem itemIn = new ToolStripMenuItem("In");
            itemIn.Name = "in";
            itemIn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemIn.Size = new System.Drawing.Size(200, 20);
            itemIn.Click += delegate(object sender, EventArgs e)
            {
                if (treeList != null)
                {
                    treeList.Print();
                }
            };
            inLuoi.DropDownItems.Add(itemIn);

            ToolStripMenuItem itemXemTruoc = new ToolStripMenuItem("Xem trước");
            itemXemTruoc.Name = "xemTruoc";
            itemXemTruoc.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXemTruoc.Size = new System.Drawing.Size(200, 20);
            itemXemTruoc.Click += delegate(object sender, EventArgs e)
            {
                HelpMsgBox._showWaitingMsg(delegate()
                {
                    if (treeList != null)
                    {
                        treeList.ShowPrintPreview();
                    }
                });
            };
            inLuoi.DropDownItems.Add(itemXemTruoc);

            toolStripBar.Items.Add(InLuoiSep);
            toolStripBar.Items.Add(inLuoi);
            toolStripBar.ResumeLayout(false);
            toolStripBar.PerformLayout();

            return inLuoi;
        }
    }
}
