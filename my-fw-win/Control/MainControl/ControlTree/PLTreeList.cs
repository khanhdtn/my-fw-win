using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using ProtocolVN.Framework.Win;

namespace DevExpress.XtraTreeList
{
    public class PLTreeList : TreeList
    {  
        private object _PrintElement;
        private object _ExportElement;
        public PLTreeList() : base()
        {
            this.ToPLTreeList();
        }
      
        public void _SetPermissionElement(object print, object export)
        {
            this._PrintElement = print;
            this._ExportElement = export;
        }
        private void ToPLTreeList()
        {
            //Danh sách các thuộc tính đã đặt sẵn
            this.HorzScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;
            this.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;
            
            this.OptionsView.ShowIndicator = true;
            this.OptionsBehavior.AutoFocusNewNode = true;

            this.OptionsView.AutoWidth = true;
            this.OptionsView.EnableAppearanceEvenRow = true;
            this.OptionsView.EnableAppearanceOddRow = true;
            this.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            this.ShowTreeListMenu += new TreeListMenuEventHandler(TreeViewVN_ShowTreeListMenu);
            this.DragObjectDrop += new DragObjectDropEventHandler(TreeViewVN_DragObjectDrop);
        }

        #region Xử lý không ẩn cột khi kéo cột ra ngoài
        void TreeViewVN_DragObjectDrop(object sender, DragObjectDropEventArgs e)
        {
            try
            {
                TreeListColumn col = (TreeListColumn)e.DragObject;
                if (!col.Visible)
                    col.Visible = true;
            }
            catch { }
        }
        #endregion

        #region Xử lý menu
        void TreeViewVN_ShowTreeListMenu(object sender , TreeListMenuEventArgs e)
        {
            if (e.Menu is DevExpress.XtraTreeList.Menu.TreeListColumnMenu)
            {
                InsertMenu((DevExpress.XtraTreeList.Menu.TreeListColumnMenu)e.Menu);
            }
        }
        
        private void InsertMenu(DevExpress.XtraTreeList.Menu.TreeListColumnMenu Menu)
        {
            // Insert Menu   
            DevExpress.XtraTreeList.Menu.TreeListColumnMenu menu = (DevExpress.XtraTreeList.Menu.TreeListColumnMenu)Menu;
            if (menu.Column == null) return;

            #region 1. Auto Filter - Dev Chưa hỗ trợ
            #endregion

            #region 2. Lọc nâng cao - Dev Chưa hỗ trợ
            #endregion

            #region 3. Tính toán trong nhóm
            DevExpress.Utils.Menu.DXMenuItem itemDisplayFooter;
            itemDisplayFooter = new DevExpress.Utils.Menu.DXMenuItem("Hiện thanh tính toán");
            itemDisplayFooter.BeginGroup = true;
            if (this.OptionsView.ShowSummaryFooter == true)
                itemDisplayFooter.Caption = "Ẩn thanh tính toán";
            itemDisplayFooter.Click += delegate(object sender, EventArgs e){
                this.OptionsView.ShowSummaryFooter = !this.OptionsView.ShowSummaryFooter;
            }; 
            Menu.Items.Add(itemDisplayFooter);            
            #endregion

            #region 4. Canh lề 
            DevExpress.Utils.Menu.DXSubMenuItem itemDisplayData = new DevExpress.Utils.Menu.DXSubMenuItem("Canh lề");
            itemDisplayData.BeginGroup = true;
            Menu.Items.Add(itemDisplayData);
            DevExpress.Utils.Menu.DXMenuCheckItem itemLeft = new DevExpress.Utils.Menu.DXMenuCheckItem("Lề trái");
            DevExpress.Utils.Menu.DXMenuCheckItem itemRight = new DevExpress.Utils.Menu.DXMenuCheckItem("Lề phải");
            DevExpress.Utils.Menu.DXMenuCheckItem itemCenter = new DevExpress.Utils.Menu.DXMenuCheckItem("Lề giữa");
            itemDisplayData.Items.Add(itemLeft);
            itemDisplayData.Items.Add(itemRight);
            itemDisplayData.Items.Add(itemCenter);
            itemLeft.Tag = menu.Column.AbsoluteIndex;
            itemRight.Tag = menu.Column.AbsoluteIndex;
            itemCenter.Tag = menu.Column.AbsoluteIndex;
            itemLeft.Click += new EventHandler(itemLeft_Click);
            itemRight.Click += new EventHandler(itemRight_Click);
            itemCenter.Click += new EventHandler(itemCenter_Click);
            itemLeft.Checked = (this.Columns[menu.Column.AbsoluteIndex].AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Near);
            itemRight.Checked = (this.Columns[menu.Column.AbsoluteIndex].AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Far);
            itemCenter.Checked = (this.Columns[menu.Column.AbsoluteIndex].AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Center);
            #endregion

            #region 5. Cố định cột - Chưa hỗ trợ
            #endregion

            #region 6. Xuất ra file
          bool isExport = true;

            if (_ExportElement != null)
            {
                if (_ExportElement is DevExpress.XtraBars.BarItem)
                {
                    if (((DevExpress.XtraBars.BarItem)_ExportElement).Visibility == DevExpress.XtraBars.BarItemVisibility.Never)
                        isExport = false;
                }
                else if (_ExportElement is DevExpress.XtraEditors.SimpleButton)
                {
                    if (((DevExpress.XtraEditors.SimpleButton)_ExportElement).Visible == false)
                        isExport = false;
                }
            }
            if (isExport)
            {   //SubMenu Export Data
                DevExpress.Utils.Menu.DXSubMenuItem itemExport = new DevExpress.Utils.Menu.DXSubMenuItem("Xuất ra file");
                itemExport.BeginGroup = true;
                Menu.Items.Add(itemExport);

                //Menu Export Excel
                DevExpress.Utils.Menu.DXMenuItem itemExportExcel = new DevExpress.Utils.Menu.DXMenuItem("Excel 97 - 2003");
                itemExport.Items.Add(itemExportExcel);
                itemExportExcel.Tag = "xls";
                itemExportExcel.Click += new EventHandler(itemExport_Click);

                DevExpress.Utils.Menu.DXMenuItem itemExportExcel2007 = new DevExpress.Utils.Menu.DXMenuItem("Excel 2007");
                itemExport.Items.Add(itemExportExcel2007);
                itemExportExcel2007.Tag = "xlsx";
                itemExportExcel2007.Click += new EventHandler(itemExport_Click);

                DevExpress.Utils.Menu.DXMenuItem itemPDF = new DevExpress.Utils.Menu.DXMenuItem("PDF");
                itemExport.Items.Add(itemPDF);
                itemPDF.Tag = "pdf";
                itemPDF.Click += new EventHandler(itemExport_Click);

                //Menu Export HTML
                DevExpress.Utils.Menu.DXMenuItem itemExportHTML = new DevExpress.Utils.Menu.DXMenuItem("HTML");
                itemExport.Items.Add(itemExportHTML);
                itemExportHTML.Tag = "html";
                itemExportHTML.Click += new EventHandler(itemExport_Click);

                //Menu Export Text
                DevExpress.Utils.Menu.DXMenuItem itemExportText = new DevExpress.Utils.Menu.DXMenuItem("RTF");
                itemExport.Items.Add(itemExportText);
                itemExportText.Tag = "rtf";
                itemExportText.Click += new EventHandler(itemExport_Click);
            }
            #endregion

            #region 7. In dữ liệu
              bool isPrint = true;
            if (_PrintElement != null)
            {
                if (_PrintElement is DevExpress.XtraBars.BarItem)
                {
                    if (((DevExpress.XtraBars.BarItem)_PrintElement).Visibility == DevExpress.XtraBars.BarItemVisibility.Never)
                        isPrint = false;
                }
                else if (_PrintElement is DevExpress.XtraEditors.SimpleButton)
                {
                    if (((DevExpress.XtraEditors.SimpleButton)_PrintElement).Visible == false)
                        isPrint = false;
                }
            }
            if (isPrint)
            {
                DevExpress.Utils.Menu.DXMenuItem itemPrintData = new DevExpress.Utils.Menu.DXMenuItem("Xem trước khi in");
                itemPrintData.BeginGroup = true;
                Menu.Items.Add(itemPrintData);
                itemPrintData.Click += new EventHandler(itemPrintData_Click);
            }
            #endregion

            #region 8. Hình dạng cây - Chưa hỗ trợ
            #endregion

            #region 9. Hỗ trợ debug - Chưa hỗ trợ
            #endregion
        }        
        private void itemLeft_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            this.Columns[(int)item.Tag].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
        private void itemRight_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            this.Columns[(int)item.Tag].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        }
        private void itemCenter_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            this.Columns[(int)item.Tag].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        private void itemExport_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            HelpTreeList.exportFile(this, item.Tag.ToString());
        }
        private void itemPrintData_Click(object sender, EventArgs e)
        {
            this.ShowPrintPreview();
        }
        #endregion
    }
}