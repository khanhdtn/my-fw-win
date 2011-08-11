using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// DUYVT_29.03.10
    /// </summary>
    public class PhieuThongKeHelp
    {
        public static void CreateBusinessMenu(PivotGridControl gridMaster, BarSubItem barSubItem1, 
            string fieldName, string[] captions, string[] ImageNames, 
            DelegationLib.CallFunction_MulIn_NoOut[] delegates, PermissionItem[] pers)
        {
            ItemClickEventHandler handler = null;
            if (captions == null)
            {
                barSubItem1.Visibility = BarItemVisibility.Never;
            }
            else
            {
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
                        BarButtonItem item = new BarButtonItem();
                        item.Caption = captions[index];
                        item.Name=index.ToString();
                        if (!ImageNames[index].Equals(""))
                        {
                            item.Glyph=ResourceMan.getImage16(ImageNames[index]);
                        }
                        item.PaintStyle = BarItemPaintStyle.Standard;
                        if (handler == null)
                        {
                            handler += new ItemClickEventHandler(delegate(object sender,  ItemClickEventArgs e)
                            {
                                if (gridMaster.Cells.MultiSelection.SelectedCells.Count < 1)
                                {
                                    HelpMsgBox.ShowNotificationMessage("");
                                }
                                else
                                {
                                    List<object> data = new List<object>();
                                    foreach (Point point in gridMaster.Cells.MultiSelection.SelectedCells)
                                    {
                                        data.Add(point);
                                    }
                                    delegates[HelpNumber.ParseInt32(e.Item.Name)](data);
                                }
                            });
                        }
                        item.ItemClick += handler;
                        barSubItem1.ItemLinks.Add(item);
                        index++;
                    }
                }
                if ((barSubItem1.ItemLinks == null) || (barSubItem1.ItemLinks.Count == 0))
                {
                    barSubItem1.Visibility = BarItemVisibility.Never;
                }
            }
        }

        public static Point[] GetSelectedCell(PivotGridControl grid)
        {
            if (grid.Cells.MultiSelection.SelectedCells.Count > 0)
            {
                Point[] pointArray = new Point[grid.Cells.MultiSelection.SelectedCells.Count];
                int num = 0;
                foreach (Point point in grid.Cells.MultiSelection.SelectedCells)
                {
                    if (point != null)
                    {
                        pointArray[num++] = point;
                    }
                }
                return pointArray;
            }
            return null;
        }

        public static void SetContextMenuOnGrid(PivotGridControl pivotGridMaster,
            _MenuItem AppendMenu, _MenuItem NghiepVuMenu, bool IncludeNghiepVu)
        {
            try
            {
                _MenuItem item = null;
                if (IncludeNghiepVu)
                {
                    if ((NghiepVuMenu == null) && (AppendMenu != null))
                    {
                        item = AppendMenu;
                    }
                    else if ((NghiepVuMenu != null) && (AppendMenu == null))
                    {
                        item = NghiepVuMenu;
                    }
                    else if ((NghiepVuMenu == null) && (AppendMenu == null))
                    {
                        item = null;
                    }
                }
                else
                {
                    item = AppendMenu;
                }
                if (item != null)
                {
                    HelpPivotGrid.AddMenuToGridView(pivotGridMaster, item.FieldName, 
                        item.CaptionNames, item.ImageNames, item.Funcs);
                }
            }
            catch (Exception exception)
            {
                PLException.AddException(exception);
            }
        }

        public static void ShowMenu(BarSubItem barSubItem1, bool _status,
            PivotGridControl pivotGridMaster, SplitContainerControl splitContainerControl1)
        {
            barSubItem1.Enabled = _status;
            if (pivotGridMaster.ContextMenuStrip != null)
            {
                foreach (ToolStripItem item in pivotGridMaster.ContextMenuStrip.Items)
                {
                    item.Enabled = _status;
                }
            }
        }
    }
}