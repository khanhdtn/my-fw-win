using System;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Drawing.Imaging;
using DevExpress.XtraGrid.Blending;
using DevExpress.XtraBars;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ làm việc với XtraGridView
    /// </summary>
    public class HelpGrid : HelpGridColumn
    {
        /// <summary>
        /// Xóa tất cả các thông báo lỗi đang có trên lưới
        /// </summary>
        /// <param name="gridview"></param>
        public static void ClearAllError(GridView gridview)
        {
            try
            {
                DataTable dt = ((DataView)gridview.DataSource).Table;
                DataRow[] arrayRow = dt.GetErrors();
                foreach (DataRow row in arrayRow)
                    row.ClearErrors();
            }
            catch { }
        }

        /// <summary>
        /// Tự động chỉnh lại độ rộng cho phù hợp
        /// </summary>        
        public static bool PLBestFit(GridView grid)
        {
            try
            {
                grid.OptionsView.ColumnAutoWidth = true;
                int totalWidth = 0;
                for (int i = 0; i < grid.VisibleColumns.Count; i++)
                {
                    totalWidth += grid.VisibleColumns[i].VisibleWidth;
                }
                grid.OptionsView.ColumnAutoWidth = false;

                int totalWidthBest = 0;
                grid.BestFitColumns();
                for (int i = 0; i < grid.VisibleColumns.Count; i++)
                {
                    totalWidthBest += grid.VisibleColumns[i].VisibleWidth;
                }

                if (totalWidth < totalWidthBest)
                {
                    grid.OptionsView.ColumnAutoWidth = false;
                }
                else
                    grid.OptionsView.ColumnAutoWidth = true;

                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            return false;
        }

        /// <summary>
        /// Hiển thị tổng số mẫu tin dưới lưới
        /// </summary>
        public static void ShowNumOfRecord(GridControl gridControl1)
        {
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.First.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Last.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Next.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Prev.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.TextStringFormat = "Tổng số mẫu tin: {1}";
            gridControl1.EmbeddedNavigator.Name = "";
            gridControl1.UseEmbeddedNavigator = true;
        }

        #region WarterMark
        /// <summary>
        /// Đặt wartermark vào lưới
        /// </summary>
        public static void SetWaterMark(GridView grid, System.Drawing.Image image, float opacity)
        {
            //Image result = SetImageOpacity(image, opacity);
            Image result = image;
            XtraGridBlending blend = new XtraGridBlending();
            GridControl gridControl = grid.GridControl;
            blend.GridControl = gridControl;
            gridControl.BackgroundImage = result;
            gridControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            gridControl.BackColor = Color.White;
        }

        private static Image SetImageOpacity(System.Drawing.Image originalImage, float opacitySize)
        {
            Bitmap opacityImage = new Bitmap(originalImage.Width, originalImage.Height);
            Graphics graphics = Graphics.FromImage(opacityImage);
            // Set alpha in the ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = opacitySize;
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            // Drawing
            graphics.DrawImage(originalImage, new Rectangle(0, 0, opacityImage.Width, opacityImage.Height), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, imageAttributes);
            graphics.Dispose();

            return opacityImage;
        }
        #endregion

        #region Tạo PopupMenu cho lưới
        /// <summary>
        /// Thêm danh sách menu ngữ cảnh vào trong GridView.
        /// Menu này áp dụng khi click phải trên phần nội dung của lưới
        /// </summary>
        public static void AddContextMenu(GridView grid, List<ItemInfo> items)
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

                    int[] a = grid.GetSelectedRows();
                    DataRow dr = grid.GetDataRow(a[0]);
                    objs.Add(dr);

                    del(objs);
                };
            }

            grid.MouseUp += delegate(object sender, MouseEventArgs e)
            {
                if ((e.Button & MouseButtons.Right) != 0 && grid.GridControl.ClientRectangle.Contains(e.X, e.Y))
                {
                    menu.ShowPopup(manager, Control.MousePosition);
                }
                else
                {
                    menu.HidePopup();
                }
            };

            grid.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if ((e.Button & MouseButtons.Right) != 0 && grid.GridControl.ClientRectangle.Contains(e.X, e.Y))
                {
                    menu.ShowPopup(manager, Control.MousePosition);
                }
                else
                {
                    menu.HidePopup();
                }
            };

        }
        
        /// <summary>Thêm ContextMenuStrip vào Grid
        /// </summary>
        /// <param name="grid">Grid cần gắn ContextMenuStrip</param>
        /// <param name="keyField">Tên field cần lấy dữ liệu (ví dụ: lấy giá trị của field ID)</param>
        /// <param name="captions">Mảng tên cho mỗi item trong ContextMenuStrip</param>
        /// <param name="images">Mảng hình ảnh gắng với mỗi item trong ContextMenuStrip</param>
        /// <param name="delegates">Mảng delegate gọi tới một phương thức, gắn với mỗi item trong ContextMenuStrip</param>
        public static void AddMenuToGridView(DevExpress.XtraGrid.GridControl grid, string fieldName, string[] captions, string[] images, DelegationLib.CallFunction_MulIn_NoOut[] delegates, PermissionItem[] pers)
        {
            if (captions == null)
                return;

            GridView gridView1 = (GridView)grid.MainView;
            ContextMenuStrip ctrContextMenuStrip = new ContextMenuStrip();
            //Chưa chỉnh lại Size 
            int i = 0;
            foreach (string s in captions)
            {
                //Start Check Permission
                if (pers != null)
                {
                    if (pers[i] != null)
                    {
                        if (ApplyPermissionAction.checkPermission(pers[i]) == null ||
                            ApplyPermissionAction.checkPermission(pers[i]) == false)
                        {
                            i++;
                            continue;
                        }
                    }
                }
                //Tạo Item
                Image image = null;
                image = ResourceMan.getImage16(images[i]);
                ToolStripMenuItem itemI = new ToolStripMenuItem(s, image);
                itemI.Name = i.ToString();
                itemI.Click += delegate(object sender, EventArgs e)
                {
                    //Lấy giá trị chọn từ lưới
                    List<object> objs = new List<object>();
                    foreach (int index in gridView1.GetSelectedRows())
                    {
                        DataRow row = gridView1.GetDataRow(index);
                        objs.Add(row[fieldName]);
                    }

                    //Chọn xử lý tương ứng với chọn lựa
                    delegates[HelpNumber.ParseInt64(((ToolStripMenuItem)sender).Name)](objs);
                };
                ctrContextMenuStrip.Items.Add(itemI);
                i++;
            }
            grid.ContextMenuStrip = ctrContextMenuStrip;
        }

        /// <summary>Thêm ContextMenuStrip vào Grid
        /// </summary>
        /// <param name="grid">Grid cần gắn ContextMenuStrip</param>
        /// <param name="keyField">Tên field cần lấy dữ liệu (ví dụ: lấy giá trị của field ID)</param>
        /// <param name="captions">Mảng tên cho mỗi item trong ContextMenuStrip</param>
        /// <param name="images">Mảng hình ảnh gắng với mỗi item trong ContextMenuStrip</param>
        /// <param name="delegates">Mảng delegate gọi tới một phương thức, gắn với mỗi item trong ContextMenuStrip</param>
        
        public static void AddMenuToGridView(DevExpress.XtraGrid.GridControl grid, string fieldName, string[] captions, string[] images, DelegationLib.CallFunction_MulIn_NoOut[] delegates)
        {
            AddMenuToGridView(grid, fieldName, captions, images, delegates, null);
        }
        #endregion        

        #region Format Condition
        /// <summary>
        /// Đặt màu sắc của cột tùy vào điều kiện được đặt ở dưới lưới
        /// </summary>
        public static void SetCondition(
            GridView xtraGridView, string columnName,
            FormatConditionEnum[] formatConditionEnum,
            object[] Value1s, object[] Value2s, Object[] formatAppearance)
        {
            StyleFormatCondition temp = new StyleFormatCondition();
            //Chọn cột định dạng
            temp.Column = xtraGridView.Columns[columnName];
            //Đặt điều kiện
            for (int i = 0; i < formatConditionEnum.Length; i++)
            {
                Object TmpformatAppearance = null;
                if (formatAppearance.Length == 1)
                    TmpformatAppearance = formatAppearance[0];
                else
                    TmpformatAppearance = formatAppearance[i];
                //Chọn kiểu định dạng
                if (TmpformatAppearance is DevExpress.Utils.AppearanceObject)
                {
                    temp.Appearance.Assign((DevExpress.Utils.AppearanceObject)TmpformatAppearance);
                }
                else
                {
                    temp.Appearance.BackColor = ((System.Drawing.Color)TmpformatAppearance);
                    temp.Appearance.Options.UseBackColor = true;
                }
                temp.Condition = formatConditionEnum[i];
                temp.Value1 = Value1s[i];
                if(Value2s[i] != null) temp.Value2 = Value2s[i];
                
                xtraGridView.FormatConditions.Add(temp);
            }
        }

        public static void SetFormatCondition(
            GridView xtraGridView, string columnName,
            FormatConditionEnum[] formatConditionEnum, object[] Value1s, object[] Value2s,
            DevExpress.Utils.AppearanceObject formatAppearance)
        {
            SetCondition(xtraGridView, columnName, formatConditionEnum, Value1s, Value2s, new Object[]{formatAppearance});
        }

        public static void SetColorCondition(
            GridView xtraGridView, string columnName,
            FormatConditionEnum[] formatConditionEnum, object[] Value1s, object[] Value2s,
            System.Drawing.Color formatAppearance)
        {
            SetCondition(xtraGridView, columnName, formatConditionEnum, Value1s, Value2s, new Object[] { formatAppearance });
        }
        #endregion

        #region PHUOC TODO : Hỗ trợ refresh lưới sau 1 thời gian
        /// <summary>Hàm tự động refresh lưới sau mot thời gian cho trước 
        /// </summary>
        public static RefreshXtraGrid AddRefresh(GridControl grid, int MiliSecond, RefreshXtraGrid.delegateGetDataset dlgDataset, RefreshXtraGrid.delegateRefreshDisplayDataset dlgDisplayData)
        {
            return new RefreshXtraGrid(grid, MiliSecond, dlgDataset, dlgDisplayData);
        }
        #endregion

        /// <summary>
        /// Đặt tính chỉ đọc vào GridView
        /// </summary>
        /// <param name="gridView"></param>
        public static void SetReadOnly(GridView gridView)
        {
            gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gridView.OptionsBehavior.Editable = false;
        }
        /// <summary>Hàm làm lưới chuyển thành dạng chỉ đọc và không quay lại trạng thái trước đó
        /// - Hàm này chỉ sử dụng khi lưới có 1 số cột dạng Memo hoặc dạng Popup
        /// phải ở chế độ Editable mới thấy được.
        /// - Nếu ngược lại ta chỉ cần dùng hàm SetReadOnly trên HelpGrid
        /// </summary>
        /// <param name="gridViews"></param>
        public static void SetReadOnlyHaveMemoCtrl(params GridView[] gridViews)
        {
            if (gridViews == null) return;
            foreach (GridView gridView in gridViews)
            {
                gridView.OptionsBehavior.Editable = true;
                foreach (GridColumn col in gridView.Columns)
                {
                    if (col.ColumnEdit != null && col.ColumnEdit is RepositoryItemMemoExEdit)
                    {
                        col.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        col.OptionsColumn.AllowEdit = false;
                    }
                    col.OptionsColumn.ReadOnly = true;
                }
                gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            }

        }
        /// <summary>
        /// Đặt giá trị mới cho footer
        /// </summary>
        /// <param name="griView"></param>
        /// <param name="firstString"></param>
        /// <param name="lastString"></param>
        public static void DrawStringFooter(GridView griView, string firstString, string lastString)
        {
            if (griView.OptionsView.ShowFooter == false)
                griView.OptionsView.ShowFooter = true;
            griView.CustomDrawFooter += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
            {
                string middleString = griView.RowCount.ToString();
                e.Painter.DrawObject(e.Info);

                Font fonFirst = new Font("Arial", 10, FontStyle.Regular);
                StringFormat formatFirst = new StringFormat(StringFormatFlags.DisplayFormatControl, 1);
                formatFirst.LineAlignment = StringAlignment.Center;//canh giữa
                e.Graphics.DrawString("     " + firstString, fonFirst, Brushes.Blue, e.Bounds, formatFirst);

                //vẽ chuỗi giữa
                SizeF size = e.Graphics.MeasureString("     " + firstString, fonFirst);
                Font fonSecond = new Font("Arial", 10, FontStyle.Bold);
                RectangleF recTangel = new RectangleF(e.Bounds.Left + size.Width, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(middleString, fonSecond, Brushes.Black, recTangel, formatFirst);

                //vẽ chuỗi cuối                
                size.Width += e.Graphics.MeasureString(middleString, fonSecond).Width;
                Font fonThird = new Font("Arial", 10, FontStyle.Regular);

                recTangel = new RectangleF(e.Bounds.Left + size.Width, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(lastString, fonThird, Brushes.Red, recTangel, formatFirst);

                ////Prevent default drawing of the footer
                e.Handled = true;
            };
        }

        /// <summary>
        /// Set STT cho lưới
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="NumLine"></param>
        public static void SetNumLine(GridView grid, int NumLine)
        {
            grid.CalcRowHeight += delegate(object sender, RowHeightEventArgs e)
            {
                if (e.RowHandle >= 0)
                    e.RowHeight = 20 * NumLine;
            };
        }

        #region Disable Cell
        /// <summary>
        /// SetDisable 1 cell trên lưới
        /// </summary>
        /// <param name="view"></param>
        /// <param name="FieldName"></param>
        /// <param name="KeyField"></param>
        /// <param name="KeyValue"></param>
        public static void SetCellDisable(GridView view, string FieldName, string KeyField, long KeyValue)
        {
            long[] ids = new long[] { KeyValue };
            SetCellDisable(view, FieldName, KeyField, ids);
        }
        public static void SetCellDisable(GridView view, string FieldName, string KeyField, long[] KeyValues)
        {
            view.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
            {
                if (e.RowHandle == view.FocusedRowHandle) return;

                if (e.Column.FieldName == FieldName)
                {
                    for (int vt = 0; vt <= KeyValues.Length - 1; vt++)
                    {
                        //if (Convert.ToInt64(view.GetRowCellDisplayText(e.RowHandle, KeyField)) == KeyValues[vt])
                        //{
                        //    e.Appearance.BackColor = Color.Gray;
                        //    // view.Columns[FieldName].OptionsColumn.AllowFocus = false;
                        //}
                        if (Convert.ToInt64(view.GetRowCellValue(e.RowHandle, KeyField)) == KeyValues[vt])
                        {
                            e.Appearance.BackColor = Color.Gray;
                            // view.Columns[FieldName].OptionsColumn.AllowFocus = false;
                        }
                    }
                }
            };
            view.ShowingEditor += delegate(object sender, CancelEventArgs e)
            {
                int index = view.FocusedRowHandle;
                if (index >= 0)
                {
                    for (int vt = 0; vt <= KeyValues.Length - 1; vt++)
                    {
                        //if (Convert.ToInt64(view.GetRowCellDisplayText(index, KeyField)) == KeyValues[vt]
                        //    && view.FocusedColumn.FieldName == FieldName)
                        //{
                        //    e.Cancel = true;
                        //}
                        if (Convert.ToInt64(view.GetRowCellValue(index, KeyField)) == KeyValues[vt]
                            && view.FocusedColumn.FieldName == FieldName)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            };
        }
        #endregion

        /// <summary>
        /// Đặt focus vào dòng mới
        /// </summary>
        /// <param name="grid"></param>
        public static void SetFocusNewRow(GridView grid)
        {
            if (grid.GridControl != null)
            {
                grid.GridControl.ProcessGridKey += delegate(object sender, KeyEventArgs e)
                {
                    if (ShortcutKey.K_FOCUS_NEW_ROW == e.KeyCode)
                    {
                        grid.FocusedRowHandle = -(Int32.MaxValue);//-2147483647
                        grid.Focus();
                        grid.FocusedColumn = grid.VisibleColumns[0];
                        grid.ShowEditor();
                    }
                };
            }
            else
            {
                PLMessageBoxDev.ShowMessage("GridControl phải được khởi tạo trước");              
            }
        }

        /// <summary>
        /// Đặt các giá trị Y cho tất cả các Field có dạng checkbox trên lưới
        /// </summary>
        /// <param name="NewRow"></param>
        /// <param name="FieldNames"></param>
        public static void CheckDefault(DataRow NewRow, String[] FieldNames)
        {
            for (int i = 0; i < FieldNames.Length; i++)
            {
                NewRow[FieldNames[i]] = "Y";
            }
        }

        /// <summary>
        /// Đặt Y cho cột VISIBLE_BIT
        /// </summary>
        /// <param name="NewRow"></param>
        public static void CheckVisibleDefault(DataRow NewRow)
        {
            CheckDefault(NewRow, new String[] { "VISIBLE_BIT" });
        }

        /// <summary>
        /// Đặt focus vào dùng FILTER ROW
        /// </summary>
        /// <param name="gridView"></param>
        public static void SetFocusFilterRow(GridView gridView)
        {
            try
            {
                gridView.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                if (gridView.FocusedColumn.Visible == false)
                    gridView.FocusedColumn = gridView.VisibleColumns[0];
                gridView.ShowEditor();
            }
            catch { }
        }

        /// <summary>
        /// Đặt focus vào dùng FILTER ROW
        /// </summary>
        /// <param name="gridView"></param>
        public static void setFocusFilterRow(GridView gridView){
            try
            {                
                gridView.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                if(gridView.FocusedColumn.Visible == false)
                    gridView.FocusedColumn = gridView.VisibleColumns[0];
                gridView.ShowEditor();
            }
            catch { }
        }


        /// <summary>
        /// Gắn title vào trong lưới thay cho các dòng kéo thả trước đây
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="title"></param>
        public static void SetTitle(GridView gridView, String title)
        {
            gridView.ViewCaption = title;
            gridView.ViewCaptionHeight = 30;
            gridView.OptionsView.ShowViewCaption = true;
        }

        /// <summary>
        /// Gắn title vào trong lưới thay cho các dòng kéo thả trước đây
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="title"></param>
        public static void SetUpperTitle(GridView gridView, String title)
        {
            SetTitle(gridView, title.ToUpper());
        }


        public static void MoveGroupTextPanelToTitle(GridView gridView, params String[] newTitle)
        {
            if (newTitle != null && newTitle.Length > 0)
            {
                SetTitle(gridView, newTitle[0]);
            }
            else
            {
                if(gridView.GroupPanelText != "")
                    SetTitle(gridView, gridView.GroupPanelText);
            }

            gridView.GroupPanelText = "";
            gridView.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Inch);
            gridView.Appearance.GroupPanel.Options.UseFont = true;
        }

        /// <summary>
        /// Hiển thị dữ liệu 1 dòng trên lưới trên 1 form. Đang thử dùng.
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="grid"></param>
        /// <param name="view"></param>
        /// <param name="row"></param>
        /// <param name="isEditable"></param>
        public static void ShowRecordDialog(XtraForm frm, GridControl grid, GridView view, DataRow row, bool isEditable)
        {
            PopupFormGridEditDataInPopupForm f = new PopupFormGridEditDataInPopupForm();
            f.InitData(frm, grid, view, row, isEditable);
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.MinimizeBox = false;
            f.MaximizeBox = false;
            f.SizeGripStyle = SizeGripStyle.Hide;

            bool ret = f.ShowDialog() == DialogResult.OK;
            if (ret & isEditable)
            {
                row.ItemArray = f.Row.ItemArray;
                row.EndEdit();
            }
            return;
        }

        /// <summary>
        /// Xuất ra file dữ liệu trên gridView và định dạng dựa vào ext
        /// </summary>
        public static bool exportFile(GridView gridView, String ext, bool openFile, ref string outFileName )
        {
            bool flag = false;
            bool succ = false;
            string filePath = null;
            bool flagShowView = gridView.OptionsView.ShowViewCaption;
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

                PrintableComponentLink link = null;

                if (gridView.GridControl != null)
                {
                    if (FrameworkParams.headerLetter != null)
                    {
                        gridView.OptionsView.ShowViewCaption = false;
                        link = FrameworkParams.headerLetter.Draw(gridView.GridControl, gridView.ViewCaption,
                            "Ngày xuất báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));
                    }
                }

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
                        if (gridView is PLGridView)
                        {
                            if (link != null)
                                link.PrintingSystem.ExportToXls(f.FileName, new XlsExportOptions(((PLGridView)gridView)._TextExportMode));
                            else
                                gridView.ExportToXls(f.FileName, new XlsExportOptions(((PLGridView)gridView)._TextExportMode));
                        }
                        else
                        {
                            if (link != null)
                                link.PrintingSystem.ExportToXls(f.FileName, new XlsExportOptions(TextExportMode.Text));
                            else
                                gridView.ExportToXls(f.FileName, new XlsExportOptions(TextExportMode.Text));
                        }
                        succ = true;
                    }
                    else if (ext.Equals("xlsx"))
                    {
                        if (gridView is PLGridView)
                        {

                            if (link != null)
                                link.PrintingSystem.ExportToXlsx(f.FileName, new XlsxExportOptions(((PLGridView)gridView)._TextExportMode));
                            else
                                gridView.ExportToXlsx(f.FileName, new XlsxExportOptions(((PLGridView)gridView)._TextExportMode));
                        }
                        else
                        {
                            if (link != null)
                                link.PrintingSystem.ExportToXlsx(f.FileName, new XlsxExportOptions(TextExportMode.Text));
                            else
                                gridView.ExportToXlsx(f.FileName, new XlsxExportOptions(TextExportMode.Text));
                        }
                        succ = true;
                    }
                    else if (ext.Equals("pdf"))
                    {
                        if (link != null)
                            link.PrintingSystem.ExportToPdf(f.FileName);
                        else
                            gridView.ExportToPdf(f.FileName);
                        succ = true;
                    }
                    else if (ext.Equals("rtf"))
                    {
                        if (link != null)
                            link.PrintingSystem.ExportToRtf(f.FileName);
                        else
                            gridView.ExportToRtf(f.FileName);
                        succ = true;
                    }
                    else if (ext.Equals("htm"))
                    {
                        if (link != null)
                            link.PrintingSystem.ExportToHtml(f.FileName);
                        else
                            gridView.ExportToHtml(f.FileName);
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
                    outFileName = filePath;
                    if (ext.Equals("xls") || ext.Equals("xlsx"))
                    {
                        try
                        {
                            System.Globalization.CultureInfo oldCi = System.Threading.Thread.CurrentThread.CurrentCulture;
                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                            Microsoft.Office.Interop.Excel.ApplicationClass excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(filePath,
                                                      0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                                                      true, false, 0, true, false, false);
                            Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.ActiveSheet;
                            excelSheet.Columns.AutoFit();
                            excelWorkbook.Save();
                            System.Threading.Thread.CurrentThread.CurrentCulture = oldCi;
                            if (openFile && PLMessageBox.ShowConfirmMessage("Bạn có muốn mở tập tin này không?") == DialogResult.Yes)
                            {
                                excelApp.Visible = true;
                            }
                            else
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                                excelSheet = null;
                                excelWorkbook = null;
                                excelApp = null;
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                            }
                        }
                        catch
                        {
                            if (openFile && PLMessageBox.ShowConfirmMessage("Bạn có muốn mở tập tin này không?") == DialogResult.Yes)
                            {
                                if (!HelpFile.OpenFile(filePath))
                                    HelpMsgBox.ShowNotificationMessage("Mở tập tin không thành công");
                            }
                        }
                    }
                    else if (openFile && PLMessageBox.ShowConfirmMessage("Bạn có muốn mở tập tin này không?") == DialogResult.Yes)
                    {
                        if (!HelpFile.OpenFile(filePath))
                            HelpMsgBox.ShowNotificationMessage("Mở tập tin không thành công");
                    }
                }

                gridView.OptionsView.ShowViewCaption = flagShowView;
            }
            return true;
        }
        public static bool exportFile(GridView gridView, String ext)
        {
            string file = "";
          return  exportFile(gridView, ext, true,ref file);
        }
    
        #region Gắn Item chuẩn vào barMan
        /// <summary>
        /// Gắn Item xuất ra File vào barMan ( giống như các màn hình quản lý)
        /// Xuất ra dữ liệu trên gridView
        /// </summary>
        public static BarSubItem addXuatRaFile(BarManager barMan, GridView gridView)
        {
            BarSubItem xuatRaFile = new BarSubItem(barMan, "Xuất ra file");
            xuatRaFile.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            xuatRaFile.Glyph = FWImageDic.EXPORT_TO_FILE_IMAGE20;
            
            barMan.Bars[0].LinksPersistInfo.Add(new LinkPersistInfo(xuatRaFile));

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
        #endregion

        #region Gắn Item chuẩn trong ToolStrip

        /// <summary>
        /// Gắn nút cho phép import dữ liệu từ excel file và gắn vào toolStripBar.( giống như các màn hình danh mục)      
        /// </summary>
        public static ToolStripDropDownButton addImportXLSFileItem(ToolStrip toolStripBar, GridView gridView)
        {
            if (gridView is PLGridView)
            {
                toolStripBar.SuspendLayout();

                ToolStripDropDownButton importXLSFile = new System.Windows.Forms.ToolStripDropDownButton();
                importXLSFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
                //TODO:
                importXLSFile.Image = FWImageDic.EXPORT_TO_FILE_IMAGE20;
                importXLSFile.ImageTransparentColor = System.Drawing.Color.Magenta;
                importXLSFile.Name = "ImportXLSFile";
                importXLSFile.Size = new System.Drawing.Size(200, 20);
                importXLSFile.Text = "Nhập từ file";

                ToolStripMenuItem xlsFileMau = new ToolStripMenuItem("Xuất File mẫu để vào dữ liệu");
                xlsFileMau.Name = "xlsFileMau";
                xlsFileMau.DisplayStyle = ToolStripItemDisplayStyle.Text;
                xlsFileMau.Size = new System.Drawing.Size(200, 20);
                xlsFileMau.Click += delegate(object sender, EventArgs e)
                {
                    HelpGrid.exportFile(gridView, "xlsx");
                };
                importXLSFile.DropDownItems.Add(xlsFileMau);

                ToolStripMenuItem itemChonFileImport = new ToolStripMenuItem("Chọn File chứa dữ liệu nhập");
                itemChonFileImport.Name = "itemChonFileImport";
                itemChonFileImport.DisplayStyle = ToolStripItemDisplayStyle.Text;
                itemChonFileImport.Click += delegate(object sender, EventArgs e)
                {
                    HelpGrid.exportFile(gridView, "xls");
                };
                importXLSFile.DropDownItems.Add(itemChonFileImport);

                toolStripBar.Items.Add(importXLSFile);
                toolStripBar.ResumeLayout(false);
                toolStripBar.PerformLayout();
                
                return importXLSFile;
            }

            return null;
        }
        
        /// <summary>
        /// Gắn item Xuất dữ liệu ra excel và gắn vào toolStrip.
        /// Dùng trong Quản lý Danh Mục
        /// </summary>
        public static ToolStripDropDownButton addXuatRaFileItem(ToolStrip toolStripBar, GridView gridView)
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
                HelpGrid.exportFile(gridView, "xlsx");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaExcel2007);

            ToolStripMenuItem itemXuatRaExcel2003 = new ToolStripMenuItem("Xuất ra file Excel 97 - 2003");
            itemXuatRaExcel2003.Name = "itemXuatRaExcel2003";
            itemXuatRaExcel2003.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaExcel2003.Click += delegate(object sender, EventArgs e)
            {
                HelpGrid.exportFile(gridView, "xls");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaExcel2003);

            ToolStripMenuItem itemXuatRaPDF = new ToolStripMenuItem("Xuất ra file PDF");
            itemXuatRaPDF.Name = "itemXuatRaPDF";
            itemXuatRaPDF.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaPDF.Click += delegate(object sender, EventArgs e)
            {
                HelpGrid.exportFile(gridView, "pdf");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaPDF);

            ToolStripMenuItem itemXuatRaHTML = new ToolStripMenuItem("Xuất ra file HTML");
            itemXuatRaHTML.Name = "itemXuatRaHTML";
            itemXuatRaHTML.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaHTML.Click += delegate(object sender, EventArgs e)
            {
                HelpGrid.exportFile(gridView, "htm");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaHTML);

            ToolStripMenuItem itemXuatRaRTF = new ToolStripMenuItem("Xuất ra file RTF");
            itemXuatRaRTF.Name = "itemXuatRaRTF";
            itemXuatRaRTF.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXuatRaRTF.Click += delegate(object sender, EventArgs e)
            {
                HelpGrid.exportFile(gridView, "rtf");
            };
            xuatRaFile.DropDownItems.Add(itemXuatRaRTF);

            toolStripBar.Items.Add(xuatRaFile);
            toolStripBar.ResumeLayout(false);
            toolStripBar.PerformLayout();

            return xuatRaFile;
        }
        /// <summary>
        /// Gắn item IN lưới vào toolStripBar.
        /// </summary>
        public static ToolStripDropDownButton addInLuoiItem(ToolStrip toolStripBar, GridView gridView)
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
                if (gridView.GridControl != null)
                {
                    gridView.GridControl.Print();
                }
            };
            inLuoi.DropDownItems.Add(itemIn);

            ToolStripMenuItem itemXemTruoc = new ToolStripMenuItem("Xem trước");
            itemXemTruoc.Name = "xemTruoc";
            itemXemTruoc.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemXemTruoc.Size = new System.Drawing.Size(200, 20);
            itemXemTruoc.Click += delegate(object sender, EventArgs e)
            {
                HelpMsgBox._showWaitingMsg(delegate(){
                    if (gridView.GridControl != null)
                    {
                        gridView.GridControl.ShowPrintPreview();
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

        public static ToolStripDropDownButton addNhapTuFileItem(ToolStrip toolStripBar, GridView gridView)
        {
            toolStripBar.SuspendLayout();

            ToolStripDropDownButton nhapTuFile = new System.Windows.Forms.ToolStripDropDownButton();
            nhapTuFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            nhapTuFile.Image = FWImageDic.EXPORT_TO_FILE_IMAGE20;
            nhapTuFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            nhapTuFile.Name = "nhapTuFile";
            nhapTuFile.Size = new System.Drawing.Size(200, 20);
            nhapTuFile.Text = "Nhập từ file";

            ToolStripMenuItem itemTemplate = new ToolStripMenuItem("Tạo định dạng file nhập");
            itemTemplate.Name = "taoDinhDangNhap";
            itemTemplate.DisplayStyle = ToolStripItemDisplayStyle.Text;
            itemTemplate.Size = new System.Drawing.Size(200, 20);
            nhapTuFile.DropDownItems.Add(itemTemplate);

            ToolStripMenuItem itemChonFile = new ToolStripMenuItem("Chọn file để nhập");
            itemChonFile.Name = "chonFileNhap";
            itemChonFile.DisplayStyle = ToolStripItemDisplayStyle.Text;
            nhapTuFile.DropDownItems.Add(itemChonFile);

            ToolStripSeparator InLuoiSep = new System.Windows.Forms.ToolStripSeparator();
            InLuoiSep.Name = "InLuoiSep";
            InLuoiSep.Size = new System.Drawing.Size(200, 20);

            toolStripBar.Items.Add(InLuoiSep);
            toolStripBar.Items.Add(nhapTuFile);
            toolStripBar.ResumeLayout(false);
            toolStripBar.PerformLayout();

            return nhapTuFile;
        }
        #endregion

        #region HelpSummaries
        /// <summary>
        /// Các dạng tính toán hổ trợ trên Grid
        /// </summary>
        public enum CalculationType
        {
            SUM,        //Tính tổng
            MIN,        //Tính min
            MAX,        //Tính max
            COUNT,      //Tính số phần tử
            AVERAGE     //Tính trung bình
        }

        /// <summary>
        /// Hiển thị thông tin tính toán (SUM, MIN, MAX, COUNT, AVERAGE) của 1 nhóm trên Grid
        /// + kiểm tra hợp lệ cột cần tính toán
        /// </summary>
        /// <param name="grid">GridView</param>
        /// <param name="column">GridColumn</param>
        public static void ShowGroupCalcInfo(GridView grid, GridColumn column, CalculationType calculationType)
        {
            if (calculationType == CalculationType.SUM)
            {
                if (checkValidate(column, CalculationType.SUM))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                        DevExpress.Data.SummaryItemType.Sum, column.FieldName, column,
                                        "SUM={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Tổng cộng> không hợp lệ trên cột <" + column.Caption + ">.");
            }
            else if (calculationType == CalculationType.MIN)
            {
                if (checkValidate(column, CalculationType.MIN))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                       DevExpress.Data.SummaryItemType.Min, column.FieldName, column,
                                       "MIN={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Giá trị nhỏ nhất> không hợp lệ trên cột <" + column.Caption + ">.");
            }
            else if (calculationType == CalculationType.MAX)
            {
                if (checkValidate(column, CalculationType.MAX))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                       DevExpress.Data.SummaryItemType.Max, column.FieldName, column,
                                       "MAX={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Giá trị lớn nhất> không hợp lệ trên cột <" + column.Caption + ">.");
            }
            else if (calculationType == CalculationType.COUNT)
            {
                if (checkValidate(column, CalculationType.COUNT))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                       DevExpress.Data.SummaryItemType.Count, column.FieldName, column,
                                       "NUM={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Số phần tử> không hợp lệ trên cột <" + column.Caption + ">.");
            }
            else if (calculationType == CalculationType.AVERAGE)
            {
                if (checkValidate(column, CalculationType.AVERAGE))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                       DevExpress.Data.SummaryItemType.Average, column.FieldName, column,
                                       "AVG={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Trung bình> không hợp lệ trên cột <" + column.Caption + ">.");
            }
        }

        /// <summary>
        /// Ẩn thông tin tính toán của 1 nhóm trên Grid
        /// </summary>
        /// <param name="grid">GridView</param>
        /// <param name="column">GridColumn</param>            
        public static void HideGroupCalcInfo(GridView grid, GridColumn column)
        {
            if (column.SummaryItem != null)
            {
                column.UnGroup();
                grid.GroupSummary.Remove(column.SummaryItem);
            }
        }

        /// <summary>
        /// Kiểm tra ràng buộc loại tính toán trên cột
        /// </summary>
        /// <param name="column">Cột check</param>
        /// <param name="calculationType">Loại tính toán</param>
        /// <returns></returns>
        private static bool checkValidate(GridColumn column, CalculationType calculationType)
        {
            //Nếu cột là kiểu Text hay DateTime thì không chấp nhận phép tính SUM và AVERAGE
            if ((column.ColumnEdit == null || column.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemDateEdit)
                && (calculationType == CalculationType.SUM || calculationType == CalculationType.AVERAGE))
                return false;
            return true;
        }
        #endregion

        /*
        * "[Page #]" (represented via the PreviewStringId.PageInfo_PageNumber property)
        * "[Page # of Pages #]" (represented via the PreviewStringId.PageInfo_PageNumberOfTotal property)
        * "[Date Printed]" (represented via the PreviewStringId.PageInfo_PageDate property)
        * "[Time Printed]" (represented via the PreviewStringId.PageInfo_PageTime property)
        * "[User Name]" (represented via the PreviewStringId.PageInfo_PageUserName property)
        */
        public static PrintableComponentLink GetPrintableComponentLink(
            IPrintable gridControl, 
            String reportHeader,
            Image ReportHeaderImage,
            String rtfGridHeader,
            float rtfGridHeaderHeight,
            String mainTitle, 
            String subTitle, 
            String reportFooter,
            String rtfGridFooter)
        {
            float height = 0;

            DevExpress.XtraPrinting.PrintingSystem printingSystem1;
            printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem();
            ((System.ComponentModel.ISupportInitialize)(printingSystem1)).BeginInit();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink1;
            printableComponentLink1 = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink1.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink1.Margins.Left = 50;
            printableComponentLink1.Margins.Right = 50;
            printableComponentLink1.Margins.Top = 50;
            printableComponentLink1.Margins.Bottom = 50;
            printingSystem1.Links.AddRange(new object[] { printableComponentLink1 });
            printableComponentLink1.Component = gridControl;            
            printableComponentLink1.PrintingSystem = printingSystem1;

            DevExpress.XtraPrinting.PageHeaderArea headerArea;
            headerArea = new DevExpress.XtraPrinting.PageHeaderArea();
            headerArea.Content.Add(reportHeader);
            headerArea.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near;

            #region Đầu trang
            if (rtfGridHeader != null)
            {
                printableComponentLink1.RtfReportHeader = rtfGridHeader;
                height = rtfGridHeaderHeight;
            }            
            printableComponentLink1.CreateReportHeaderArea += delegate(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
            {
                float currentHeight = height;

                #region Giải pháp 1
                Image headerImage = (ReportHeaderImage == null ? 
                    FWImageDic.LOGO_IMAGE48 : ReportHeaderImage);

                DevExpress.XtraPrinting.ImageBrick logo;
                logo = e.Graph.DrawImage(headerImage,
                    new RectangleF(10, 5, headerImage.Width, headerImage.Height),
                    DevExpress.XtraPrinting.BorderSide.None, Color.Transparent);
                //currentHeight += headerImage.Height;
                #endregion

                if (mainTitle != null)
                {
                    DevExpress.XtraPrinting.TextBrick brick;
                    brick = e.Graph.DrawString(mainTitle, Color.Navy, new RectangleF(0, currentHeight, 620, 40), DevExpress.XtraPrinting.BorderSide.None);
                    currentHeight += 40;
                    brick.Font = new Font("Tahoma", 20);
                    brick.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);
                    brick.BackColor = Color.White;
                    brick.ForeColor = Color.Black;

                }

                if (subTitle != null)
                {
                    DevExpress.XtraPrinting.TextBrick brickDate;
                    brickDate = e.Graph.DrawString(subTitle, Color.Navy, new RectangleF(0, currentHeight, 620, 40), DevExpress.XtraPrinting.BorderSide.None);
                    currentHeight += 40;
                    brickDate.Font = new Font("Tahoma", 10);
                    brickDate.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);
                    brickDate.BackColor = Color.White;
                    brickDate.ForeColor = Color.Black;
                }
            };
            #endregion

            if(rtfGridFooter!= null)
                printableComponentLink1.RtfReportFooter = rtfGridFooter;

            #region Header Footer
            DevExpress.XtraPrinting.PageFooterArea footerArea;
            footerArea = new DevExpress.XtraPrinting.PageFooterArea();
            footerArea.Content.Add(reportFooter);
            footerArea.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near;

            DevExpress.XtraPrinting.PageHeaderFooter pageHeaderFooter;
            pageHeaderFooter = new DevExpress.XtraPrinting.PageHeaderFooter(headerArea, footerArea);
            printableComponentLink1.PageHeaderFooter = pageHeaderFooter;
            #endregion

            ((System.ComponentModel.ISupportInitialize)(printingSystem1)).EndInit();
            
            printableComponentLink1.CreateDocument();

            return printableComponentLink1;
            
        }
    }
}
