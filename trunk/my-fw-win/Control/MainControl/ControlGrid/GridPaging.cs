using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using System.Drawing;

namespace ProtocolVN.Framework.Win
{
    class GridPaging
    {
        private GridControl gridControl;
        private ImageList imageList;
        private NavigatorCustomButton btnFirst;
        private NavigatorCustomButton btnPrev;
        private NavigatorCustomButton btnNext;
        private NavigatorCustomButton btnLast;
        private int totalRow;
        //NumPerPage -- So dong tren 1 trang
        public GridPaging(GridControl gridCtrl, int NumPerPage)
        {
            this.gridControl = gridCtrl;
            this.gridControl.UseEmbeddedNavigator = true;
            InvisibleAllNavigateButton();
            AddCustomButton();
            InitPager(NumPerPage);
        }

        private void ShowCurrentPage(PagerInfo page)
        {
            DataTable tempt = page.GetCurrentPage();
            gridControl.DataSource = tempt;
            gridControl.EmbeddedNavigator.TextStringFormat = "Số mẫu tin: " + totalRow + "  " +  page.CurrentPage + "/" + page.TotalPage;
            if (page.CurrentPage == page.TotalPage)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
            if (page.CurrentPage == 1)
            {
                btnPrev.Enabled = false;
                btnFirst.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
                btnFirst.Enabled = true;
            }
        }

        public void Refresh()
        {
            throw new Exception("Chưa hỗ trợ");            
        }

        //NumPerPage -- so dong tren 1 trang
        public void InitPager(int NumPerPage)
        {
            try
            {
                DataTable dt = (DataTable)gridControl.DataSource;
                PagerInfo page = new PagerInfo();
                page.Data = dt;
                page.NumPerPage = NumPerPage; //Data la DataTable

                totalRow = dt.Rows.Count;
                if (totalRow % NumPerPage == 0)
                {
                    page.TotalPage = totalRow / NumPerPage;
                }
                else
                {
                    page.TotalPage = totalRow / NumPerPage + 1;
                }
                if (dt.Rows.Count == 0)
                {
                    gridControl.UseEmbeddedNavigator = false;
                    return;
                }
                else
                {
                    gridControl.UseEmbeddedNavigator = true;
                    page.CurrentPage = 1;
                }
                object temp = this.gridControl.Tag;
                TagPropertyMan.InsertOrUpdate(ref temp, PagerInfo.PAGE_INFO, page);
                this.gridControl.Tag = temp;
                ShowCurrentPage(page);

                gridControl.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(EmbeddedNavigator_ButtonClick);
            }
            catch { }
        }
        private void EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                if (e.Button.Tag.Equals("NextPage"))
                {
                    PagerInfo page = (PagerInfo)TagPropertyMan.Get(this.gridControl.Tag, PagerInfo.PAGE_INFO);
                    page.NextPage();
                    ShowCurrentPage(page);
                }
                else if (e.Button.Tag.Equals("PrevPage"))
                {
                    PagerInfo page = (PagerInfo)TagPropertyMan.Get(this.gridControl.Tag, PagerInfo.PAGE_INFO);
                    page.PrevPage();
                    ShowCurrentPage(page);
                }
                else if (e.Button.Tag.Equals("FirstPage"))
                {
                    PagerInfo page = (PagerInfo)TagPropertyMan.Get(this.gridControl.Tag, PagerInfo.PAGE_INFO);
                    page.FirstPage();
                    ShowCurrentPage(page);
                }
                else if (e.Button.Tag.Equals("LastPage"))
                {
                    PagerInfo page = (PagerInfo)TagPropertyMan.Get(this.gridControl.Tag, PagerInfo.PAGE_INFO);
                    page.LastPage();
                    ShowCurrentPage(page);
                }
            }
        }
        private void AddCustomButton()
        {
            imageList = new ImageList();
            imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList.ImageSize = new System.Drawing.Size(11, 11);
            imageList.TransparentColor = System.Drawing.Color.Transparent;

            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.First);
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.Prev);
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.Next);
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.Last);

            #region Button_First
            btnFirst = new NavigatorCustomButton(0, "Trang đầu");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btnFirst.Tag = "FirstPage";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[]{btnFirst});
            #endregion 

            #region Button_Prev
            btnPrev = new NavigatorCustomButton(1, "Trang trước");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btnPrev.Tag = "PrevPage";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btnPrev });
            #endregion

            #region Button_Next
            btnNext = new NavigatorCustomButton(2, "Trang kế");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btnNext });
            btnNext.Tag = "NextPage";
            #endregion

            #region Button_Last
            btnLast = new NavigatorCustomButton(3, "Trang cuối");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btnLast });
            btnLast.Tag = "LastPage";
            #endregion
        }
        private void InvisibleAllNavigateButton()
        {
            gridControl.EmbeddedNavigator.Buttons.Next.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.Prev.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.First.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.Last.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        }
    }
}
