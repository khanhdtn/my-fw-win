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
    //***MINHLS
    //Su kien cho cac nut add,edit,remove nam trong ham EmbeddedNavigator_ButtonClick
    public class PLGridViewUpdateHelp
    {
        private GridControl gridControl;
        private ImageList imageList;
        private NavigatorCustomButton btnFirst;
        private NavigatorCustomButton btnPrev;
        private NavigatorCustomButton btnNext;
        private NavigatorCustomButton btnLast;
        private NavigatorCustomButton btngAdd;
        private NavigatorCustomButton btngEdit;
        private NavigatorCustomButton btngRemove;
        private NavigatorCustomButton btngEndEdit;
        private NavigatorCustomButton btngCancelEdit;
        private NavigatorCustomButton btnRefresh;

        private int totalRow;
        public delegate object AddAction(object param);
        public AddAction AddFunction;
        public delegate object EditAction(object param);
        public EditAction EditFunction;
        public delegate object DeleteAction(object param);
        public DeleteAction DeleteFunction;
        public delegate object PrintAction(object param);
        public PrintAction PrintFunction;
        public delegate object SaveAction(object param);
        public SaveAction SaveFunction;
        public delegate object NoSaveAction(object param);
        public NoSaveAction NoSaveFunction;
        private DMBasicGrid basic;
        public PLGridViewUpdateHelp(GridControl gridCtrl, int NumPerPage, DMBasicGrid basic)
        {
            this.gridControl = gridCtrl;
            this.gridControl.UseEmbeddedNavigator = true;
            InvisibleAllNavigateButton();
            AddCustomButton();
            InitPager(NumPerPage);
            this.basic = basic;
        }

        void Refresh()
        {
            PagerInfo page = (PagerInfo)TagPropertyMan.Get(this.gridControl.Tag, PagerInfo.PAGE_INFO);
            page.Data = basic.GetDataSource();
            page.Refresh(-1);            

            object temp = this.gridControl.Tag;
            TagPropertyMan.InsertOrUpdate(ref temp, PagerInfo.PAGE_INFO, page);
            this.gridControl.Tag = temp;

            ShowCurrentPage(page);
        }

        private void ShowCurrentPage(PagerInfo page)
        {
            if (page.CurrentPage == 0) return;

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
                    //gridControl.UseEmbeddedNavigator = false;
                    btnFirst.Visible = false;
                    btnPrev.Visible = false;
                    btnNext.Visible = false;
                    btnLast.Visible = false;
                    gridControl.EmbeddedNavigator.TextStringFormat = "";
                    page.CurrentPage = 0;
                }
                else
                {
                    btnFirst.Visible = true;
                    btnPrev.Visible = true;
                    btnNext.Visible = true;
                    btnLast.Visible = true;
                    //gridControl.UseEmbeddedNavigator = true;
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
                else if (e.Button.Tag.Equals("Refresh"))
                {   //MINHLS
                    //Su kien them vao de add them mot dong moi
                    //PLMessageBox.ShowConfirmMessage("Add");
                    Refresh();
                }
                else if (e.Button.Tag.Equals("Add"))
                {   //MINHLS
                    //Su kien them vao de add them mot dong moi
                    //PLMessageBox.ShowConfirmMessage("Add");
                    AddFunction(null);
                    //Refresh();
                }
                else if (e.Button.Tag.Equals("Edit"))
                {
                    //MINHLS
                    //Su kien them vao de edit mot dong
                   EditFunction(null);
                   //Refresh();
                }
                else if (e.Button.Tag.Equals("Remove"))
                {
                    //MINHLS
                    //Su kien them vao de xoa mot dong
                    DeleteFunction(null);
                    Refresh();
                }
                else if (e.Button.Tag.Equals("EndEdit"))
                {
                    //MINHLS
                    //Su kien them vao de luu dong moi hoac chinh sua
                    SaveFunction(null);
                    Refresh();
                }
                else if (e.Button.Tag.Equals("CancelEdit"))
                {
                    //MINHLS
                    //Su kien them vao de luu dong moi hoac chinh sua
                    NoSaveFunction(null);
                    Refresh();
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
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.add);
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.edit);
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.delete);
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.save);
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.nosave);
            //Refresh
            imageList.Images.Add(global::ProtocolVN.Framework.Win.Properties.Resources.nosave);         

            #region Button_First
            btnFirst = new NavigatorCustomButton(0,"Trang đầu");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btnFirst.Tag = "FirstPage";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[]{btnFirst});
            #endregion 

            #region Button_Prev
            btnPrev = new NavigatorCustomButton(1,"Trang trước");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btnPrev.Tag = "PrevPage";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btnPrev });
            #endregion

            #region Button_Next
            btnNext = new NavigatorCustomButton(2,"Trang kế");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btnNext });
            btnNext.Tag = "NextPage";
            #endregion

            #region Button_Last
            btnLast = new NavigatorCustomButton(3,"Trang cuối");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btnLast });
            btnLast.Tag = "LastPage";
            #endregion

            #region Refresh
            btnRefresh = new NavigatorCustomButton(9, "Refresh");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btnRefresh });
            btnRefresh.Tag = "Refresh";
            #endregion

            #region Button_Add
            btngAdd = new NavigatorCustomButton(4,"Thêm");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btngAdd.Tag = "Add";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btngAdd });
            #endregion

            #region Button_Edit
            btngEdit = new NavigatorCustomButton(5,"Sữa");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btngEdit.Tag = "Edit";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btngEdit });
            #endregion

            #region Button_Remove
            btngRemove = new NavigatorCustomButton(6,"Xóa");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btngRemove.Tag = "Remove";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btngRemove });
            #endregion

            #region Button_EndEdit
            btngEndEdit = new NavigatorCustomButton(7,"Lưu");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btngEndEdit.Tag = "EndEdit";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btngEndEdit });
            #endregion

            #region Button_CancelEdit
            btngCancelEdit = new NavigatorCustomButton(8,"Không lưu");
            gridControl.EmbeddedNavigator.Buttons.ImageList = imageList;
            btngCancelEdit.Tag = "CancelEdit";
            gridControl.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] { btngCancelEdit });
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
