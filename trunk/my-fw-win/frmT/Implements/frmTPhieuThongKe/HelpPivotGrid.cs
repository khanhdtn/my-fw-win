using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// DUYVT_16.03.10: Trợ giúp khởi tạo Field trên PivotGrid
    /// </summary>
    public class HelpPivotGrid
    {
        public static void VietHoaMenuGridPivot(PivotGridControl pivotGridMaster)
        {
            pivotGridMaster.ShowMenu += delegate(object sender, PivotGridMenuEventArgs e)
            {
                if (e.MenuType == PivotGridMenuType.Header)
                {
                    foreach (DevExpress.Utils.Menu.DXMenuItem item in e.Menu.Items)
                    {
                        if (item.Caption.Equals("Refresh Data"))
                            item.Caption = "Làm tươi dữ liệu";
                        else if (item.Caption.Equals("Hide"))
                            item.Caption = "Ẩn";
                        else if (item.Caption.Equals("Order"))
                        {
                            item.Caption = "Sắp xếp";
                            try
                            {
                                DevExpress.Utils.Menu.DXSubMenuItem subitem = (DevExpress.Utils.Menu.DXSubMenuItem)item;
                                foreach (DevExpress.Utils.Menu.DXMenuItem subitemOrder in subitem.Items)
                                {
                                    if (subitemOrder.Caption.Equals("Move to Beginning"))
                                        subitemOrder.Caption = "Di chuyển về đầu";
                                    else if (subitemOrder.Caption.Equals("Move to Left"))
                                        subitemOrder.Caption = "Di chuyển sang trái";
                                    else if (subitemOrder.Caption.Equals("Move to Right"))
                                        subitemOrder.Caption = "Di chuyển sang phải";
                                    if (subitemOrder.Caption.Equals("Move to End"))
                                        subitemOrder.Caption = "Di chuyển về cuối";
                                }
                            }
                            catch { }

                        }
                        else if (item.Caption.Equals("Show Field List"))
                            item.Caption = "Xem danh sách cột ẩn";
                        else if (item.Caption.Equals("Show Prefilter"))
                            item.Caption = "Xem điều kiện lọc";
                    }
                }
                else if (e.MenuType == PivotGridMenuType.FieldValue)
                {
                    foreach (DevExpress.Utils.Menu.DXMenuItem item in e.Menu.Items)
                    {
                        if (item.Caption.Contains("by This Column"))
                        {
                            string[] caption = item.Caption.Split(new string[] { "Sort ", " by This Column" }, StringSplitOptions.RemoveEmptyEntries);//Sort \"Tên hàng hóa\" by This Column
                            item.Caption = "Sắp xếp " + caption[0] + " theo cột này";
                        }
                        else if (item.Caption.Contains("by This Row"))
                        {
                            string[] caption = item.Caption.Split(new string[] { "Sort ", " by This Row" }, StringSplitOptions.RemoveEmptyEntries);//Sort \"Tên hàng hóa\" by This Column
                            item.Caption = "Sắp xếp " + caption[0] + " theo dòng này";
                        }
                        else if (item.Caption.Equals("Collapse"))
                            item.Caption = "Đóng nhóm";
                        else if (item.Caption.Equals("Collapse All"))
                            item.Caption = "Đóng tất cả nhóm";
                        else if (item.Caption.Equals("Expand All"))
                            item.Caption = "Mở tất cả nhóm";
                        else if (item.Caption.Equals("Expand"))
                            item.Caption = "Mở nhóm";
                    }

                }
                else if (e.MenuType == PivotGridMenuType.HeaderArea)
                {
                    foreach (DevExpress.Utils.Menu.DXMenuItem item in e.Menu.Items)
                    {
                        if (item.Caption.Equals("Refresh Data"))
                            item.Caption = "Làm tươi dữ liệu";
                        else if (item.Caption.Equals("Show Field List"))
                            item.Caption = "Xem danh sách cột ẩn";
                        else if (item.Caption.Equals("Hide Field List"))
                            item.Caption = "Ẩn danh sách cột";
                        else if (item.Caption.Equals("Show Prefilter"))
                            item.Caption = "Xem điều kiện lọc";
                    }

                }
            };
        }

        public static void AddMenuToGridView(PivotGridControl grid, string fieldName,
            string[] captions, string[] images, DelegationLib.CallFunction_MulIn_NoOut[] delegates)
        {
            AddMenuToGridView(grid, fieldName, captions, images, delegates, null);
        }

        public static void AddMenuToGridView(PivotGridControl grid, string fieldName,
            string[] captions, string[] images, DelegationLib.CallFunction_MulIn_NoOut[] delegates,
            PermissionItem[] pers)
        {
            EventHandler handler = null;
            if (captions != null)
            {
                ContextMenuStrip strip = new ContextMenuStrip();
                int index = 0;
                foreach (string str in captions)
                {
                    bool? nullable;
                    if (((pers != null) && (pers[index] != null)) &&
                        !(ApplyPermissionAction.checkPermission(pers[index]).HasValue &&
                        !(!(nullable = ApplyPermissionAction.checkPermission(pers[index])).GetValueOrDefault() && nullable.HasValue)))
                    {
                        index++;
                    }
                    else
                    {
                        Image image = null;
                        image = ResourceMan.getImage16(images[index]);
                        ToolStripMenuItem item = new ToolStripMenuItem(str, image);
                        item.Name = index.ToString();
                        if (handler == null)
                        {
                            handler = new EventHandler(delegate(object sender, EventArgs e)
                            {
                                List<object> data = new List<object>();
                                foreach (Point point in grid.Cells.MultiSelection.SelectedCells)
                                {
                                    data.Add(point);
                                }
                                delegates[(int)((IntPtr)HelpNumber.ParseInt64(((ToolStripMenuItem)sender).Name))](data);
                            });
                        }
                        item.Click += handler;
                        strip.Items.Add(item);
                        index++;
                    }
                }
                grid.ContextMenuStrip = strip;
            }
        }
    }
}