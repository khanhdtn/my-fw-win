using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class PhieuQuanLyUtil
    {
        public static void CreateBusinessMenu(
            GridView gridViewMaster,
            DevExpress.XtraBars.BarSubItem barSubItem1,
            string fieldName,
            string[] captions, 
            string[] ImageNames, 
            DelegationLib.CallFunction_MulIn_NoOut[] delegates, 
            PermissionItem[] pers)
        {
            if (captions == null)
            {
                //barSubItem1.Enabled = false;
                barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                return;
            }

            int index = 0;
            foreach (string s in captions)
            {
                //Start Check Permission
                if (pers != null)
                {
                    if (pers[index] != null)
                    {
                        if (ApplyPermissionAction.checkPermission(pers[index]) == null ||
                            ApplyPermissionAction.checkPermission(pers[index]) == false)
                        {
                            index++;
                            continue;
                        }
                    }
                }
                //End Check Permission

                DevExpress.XtraBars.BarButtonItem temp = new DevExpress.XtraBars.BarButtonItem();
                temp.Caption = captions[index];
                temp.Name = index.ToString();
                if (!ImageNames[index].Equals(""))
                {
                    temp.Glyph = ResourceMan.getImage16(ImageNames[index]);
                }
                temp.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                temp.ItemClick += delegate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
                {
                    if (gridViewMaster.SelectedRowsCount < 1)
                    {
                        HelpMsgBox.ShowNotificationMessage("Vui lòng chọn dữ liệu !");
                        return;
                    }
                    //Lấy danh sách các giá trị (row[fieldName]) đang chọn 
                    List<object> objs = new List<object>();
                    foreach (int i in gridViewMaster.GetSelectedRows())
                    {
                        DataRow row = gridViewMaster.GetDataRow(i);
                        objs.Add(row[fieldName]);
                    }

                    //Chọn xử lý tương ứng với chọn lựa
                    delegates[HelpNumber.ParseInt32(e.Item.Name)](objs);
                };

                barSubItem1.ItemLinks.Add(temp);
                index++;
            }
            //Không có chọn lựa ẩn luôn.
            if (barSubItem1.ItemLinks == null || barSubItem1.ItemLinks.Count == 0)
                barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        public static void SetContextMenuOnGrid(
            GridControl gridControlMaster, 
            _MenuItem AppendMenu, 
            _MenuItem NghiepVuMenu, 
            bool IncludeNghiepVu)
        {
            try
            {
                _MenuItem BothMenu = null;
                if (IncludeNghiepVu)
                {
                    if (NghiepVuMenu == null && AppendMenu != null) BothMenu = AppendMenu;
                    else if (NghiepVuMenu != null && AppendMenu == null) BothMenu = NghiepVuMenu;
                    else if (NghiepVuMenu == null && AppendMenu == null) BothMenu = null;
                    else
                    {
                        //Merge 2 thang : Chua xay dung
                    }
                }
                else
                {
                    BothMenu = AppendMenu;
                }

                if (BothMenu != null) 
                    HelpGrid.AddMenuToGridView(gridControlMaster, BothMenu.FieldName, 
                        BothMenu.CaptionNames, BothMenu.ImageNames, BothMenu.Funcs);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }

        public static List<DataRow> GetSelectedDataRow(PLGridView gridView)
        {
            List<DataRow> rowList = new List<DataRow>();
            if (gridView.GetSelectedRows() != null)
            {
                foreach (int index in gridView.GetSelectedRows())
                {
                    if (index > -1) rowList.Add(gridView.GetDataRow(index));
                }                
            }
            return rowList;
        }

        public static List<DataRow> GetSelectedDataRow(PLBandedGridView gridView)
        {
            List<DataRow> rowList = new List<DataRow>();
            if (gridView.GetSelectedRows() != null)
            {
                foreach (int index in gridView.GetSelectedRows())
                {
                    if (index > -1) rowList.Add(gridView.GetDataRow(index));
                }
            }
            return rowList;
        }


        public static long[] GetSelectedID(PLGridView gridView, string IDField)           
        {
            if (gridView.GetSelectedRows() != null)
            {
                long[] ids = new long[gridView.GetSelectedRows().Length];
                int i = 0;
                foreach (int index in gridView.GetSelectedRows())
                {
                    if (index > -1)
                    {
                        DataRow temp = gridView.GetDataRow(index);
                        ids[i++] = HelpNumber.ParseInt64(temp[IDField]);
                    }
                }
                return ids;
            }
            return null;            
        }

        public static long[] GetSelectedID(PLBandedGridView gridView, string IDField)
        {
            if (gridView.GetSelectedRows() != null)
            {
                long[] ids = new long[gridView.GetSelectedRows().Length];
                int i = 0;
                foreach (int index in gridView.GetSelectedRows())
                {
                    if (index > -1)
                    {
                        DataRow temp = gridView.GetDataRow(index);
                        ids[i++] = HelpNumber.ParseInt64(temp[IDField]);
                    }
                }
                return ids;
            }
            return null;
        }

        public static void ShowMenu(
            DevExpress.XtraBars.BarSubItem barSubItem1,
            bool _status, 
            GridControl gridControlMaster,
            SplitContainerControl splitContainerControl1,
            SplitPanelVisibility _type)
        {
            barSubItem1.Enabled = _status;         

            if (gridControlMaster.ContextMenuStrip != null)
            {
                foreach (ToolStripItem var in gridControlMaster.ContextMenuStrip.Items)
                {
                    var.Enabled = _status;
                }
            }
            splitContainerControl1.PanelVisibility = _type;
        }

        public static void UpdateDuyetState(
            bool DuyetSupport, 
            bool AllowUpdatePhieuDuyet,
            GridView gridViewMaster,
            DevExpress.XtraBars.BarButtonItem barButtonItemCommit,
            DevExpress.XtraBars.BarButtonItem barButtonItemNoCommit,
            DevExpress.XtraBars.BarButtonItem barButtonItemDelete,
            DevExpress.XtraBars.BarButtonItem barButtonItemUpdate)
        {
            if (DuyetSupport)
            {
                DataRow row = gridViewMaster.GetDataRow(gridViewMaster.FocusedRowHandle);
                if (row["DUYET"] == null) row["DUYET"] = "1";
                if (row["DUYET"].ToString() == "1")
                {
                    barButtonItemCommit.Enabled = true;
                    barButtonItemNoCommit.Enabled = true;
                    barButtonItemDelete.Enabled = true;
                    barButtonItemUpdate.Enabled = true;
                }
                else if (row["DUYET"].ToString() == "3")
                {
                    barButtonItemCommit.Enabled = true;
                    barButtonItemNoCommit.Enabled = false;
                    barButtonItemDelete.Enabled = true;
                    barButtonItemUpdate.Enabled = true;
                }
                else if (row["DUYET"].ToString() == "2")
                {
                    if (AllowUpdatePhieuDuyet)
                    {
                        barButtonItemCommit.Enabled = false;
                        barButtonItemNoCommit.Enabled = true;
                    }
                    else
                    {
                        barButtonItemCommit.Enabled = false;
                        barButtonItemNoCommit.Enabled = false;
                    }
                    barButtonItemUpdate.Enabled = false;
                    barButtonItemDelete.Enabled = false;
                }
            }
        }
        
        public static BarSubItem XuatRaFile(BarManager barMan, GridView gridView)
        {
            BarSubItem xuatRaFile = new BarSubItem(barMan, "Xuất ra file");
            xuatRaFile.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            xuatRaFile.Glyph = FWImageDic.EXPORT_TO_FILE_IMAGE20;

            if (barMan.Bars[0].LinksPersistInfo.Count - 3 >= 0)
                barMan.Bars[0].LinksPersistInfo.Insert(barMan.Bars[0].LinksPersistInfo.Count - 3, new LinkPersistInfo(xuatRaFile, true));
            else
                barMan.Bars[0].LinksPersistInfo.Add(new LinkPersistInfo(xuatRaFile, true));

            BarButtonItem itemXuatRaExcel2007 = new BarButtonItem(barMan, "Xuất ra file Excel 2007");
            itemXuatRaExcel2007.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            itemXuatRaExcel2007.ItemClick += delegate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            {
                HelpGrid.exportFile(gridView, "xlsx");
            };
            xuatRaFile.LinksPersistInfo.Add(new LinkPersistInfo(itemXuatRaExcel2007));

            BarButtonItem itemXuatRaExcel2003 = new BarButtonItem(barMan, "Xuất ra file Excel 97 - 2003");
            itemXuatRaExcel2003.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            itemXuatRaExcel2003.ItemClick += delegate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            {
                HelpGrid.exportFile(gridView, "xls");
            };
            xuatRaFile.LinksPersistInfo.Add(new LinkPersistInfo(itemXuatRaExcel2003));

            BarButtonItem itemXuatRaPDF = new BarButtonItem(barMan, "Xuất ra file PDF");
            itemXuatRaPDF.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            itemXuatRaPDF.ItemClick += delegate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            {
                HelpGrid.exportFile(gridView, "pdf");
            };
            xuatRaFile.LinksPersistInfo.Add(new LinkPersistInfo(itemXuatRaPDF));

            BarButtonItem itemXuatRaHTML = new BarButtonItem(barMan, "Xuất ra file HTML");
            itemXuatRaHTML.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            itemXuatRaHTML.ItemClick += delegate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            {
                HelpGrid.exportFile(gridView, "htm");
            };
            xuatRaFile.LinksPersistInfo.Add(new LinkPersistInfo(itemXuatRaHTML));

            BarButtonItem itemXuatRaRTF = new BarButtonItem(barMan, "Xuất ra file RTF");
            itemXuatRaRTF.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            itemXuatRaRTF.ItemClick += delegate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            {
                HelpGrid.exportFile(gridView, "rtf");
            };
            xuatRaFile.LinksPersistInfo.Add(new LinkPersistInfo(itemXuatRaRTF));

            return xuatRaFile;
        }
    }
}
