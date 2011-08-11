using System;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    /// <summary>Chọn khoản thời gian tự động cập nhật vào 2 DateEdit tương ứng
    /// - DateEdit From
    /// - DateEdit To
    /// </summary>
    public partial class PLDateTimeRange : DevExpress.XtraEditors.XtraUserControl
    {
        public DateEdit _TuNgay, _DenNgay;
        public PLDateTimeRange()
        {
            InitializeComponent();                       
        }
        // Khởi tạo một UserControl
        public void _init(DateEdit tungay, DateEdit denngay)
        {
            this._TuNgay = tungay;
            this._DenNgay = denngay;
        }
      
        // Lấy ngày hiện tại
        private void Ngayhientai(DateEdit tungay, DateEdit denngay)
        {           
            tungay.EditValue = DateTime.Now;
            denngay.EditValue = DateTime.Now;
        }
        //*--Những hàm dùng để lấy tuần hiện tại */
        private void Tuanhientai(DateEdit tungay, DateEdit denngay)
        {
            DateTime dautuan = HelpDate.GetStartOfCurrentWeek(); ;
            DateTime cuoituan = HelpDate.GetEndOfCurrentWeek();
            tungay.EditValue = dautuan;
            denngay.EditValue = cuoituan;
        }
        // Tuần vừa rồi 
        private void Tuanvuaroi(DateEdit tungay, DateEdit denngay)
        {
            DateTime dautuan = HelpDate.GetStartOfCurrentWeek();
            DateTime cuoituan = HelpDate.GetEndOfCurrentWeek();
            tungay.EditValue = dautuan.AddDays(-7);
            denngay.EditValue = cuoituan.AddDays(-7);
        }
        // Ngày hôm qua 
        private void Ngayhomqua(DateEdit tungay, DateEdit denngay)
        {
            tungay.EditValue = DateTime.Now.AddDays(-1);
            denngay.EditValue = DateTime.Now;
        }

        //--Những hàm dùng để lấy ngày tháng hiện tại --
        private void Thanghientai(DateEdit tungay, DateEdit denngay)
        {
            DateTime ngaydauthang = HelpDate.GetStartOfCurrentMonth();
            tungay.EditValue = ngaydauthang;
            DateTime ngaycuoicuathang = HelpDate.GetEndOfCurrentMonth();
            denngay.EditValue = ngaycuoicuathang;
        }
        private void Namhientai(DateEdit tungay, DateEdit denngay)
        {
            DateTime daunam = HelpDate.GetStartOfCurrentYear();
            tungay.EditValue = daunam;
            DateTime cuoinam = HelpDate.GetEndOfCurrentYear();
            denngay.EditValue = cuoinam;
        }
        private void Quyhientai(DateEdit tungay, DateEdit denngay)
        {
            DateTime ngaydauquy = HelpDate.GetStartOfCurrentQuarter();
            tungay.EditValue = ngaydauquy;
            DateTime ngaycuoiquy = HelpDate.GetEndOfCurrentQuarter();
            denngay.EditValue = ngaycuoiquy;
        }
        // Tháng trước 
        private void Thangtruoc(DateEdit tungay, DateEdit denngay)
        {
            tungay.EditValue = DateTime.Now.AddMonths(-1);
            denngay.EditValue = DateTime.Now;
        }
        // Năm trước 
        private void Namtruoc(DateEdit tungay, DateEdit denngay)
        {
            DateTime daunam = HelpDate.GetStartOfCurrentYear();
            tungay.EditValue = daunam.AddYears(-1);
            DateTime cuoinam = HelpDate.GetEndOfCurrentYear();
            denngay.EditValue = cuoinam.AddYears(-1);
        }
        // Quý trước 
        private void Quytruoc(DateEdit tungay, DateEdit denngay)
        {
            DateTime ngaydauquy = HelpDate.GetStartOfCurrentQuarter();
            tungay.EditValue = ngaydauquy.AddMonths(-3);
            DateTime ngaycuoiquy = HelpDate.GetEndOfCurrentQuarter();
            denngay.EditValue = ngaycuoiquy.AddMonths(-3);
        }
        // Ngày đầu tuần đến nay        
        private void Ngaydautuandennay(DateEdit tungay, DateEdit denngay)
        {
            DateTime ngaydautuan = HelpDate.GetStartOfCurrentWeek();
            tungay.EditValue = ngaydautuan;
            denngay.EditValue = DateTime.Now;
        }
        // Ngày đầu tháng đến nay
        private void Ngaydauthangdennay(DateEdit tungay, DateEdit denngay)
        {
            DateTime ngaydauthang = HelpDate.GetStartOfCurrentMonth();
            tungay.EditValue = ngaydauthang;
            denngay.EditValue = DateTime.Now;
        }
        // Ngày đầu quý đến nay
        private void Ngaydauquydennay(DateEdit tungay, DateEdit denngay)
        {
            DateTime ngaydauthang = HelpDate.GetStartOfCurrentQuarter(); ;
            tungay.EditValue = ngaydauthang;
            denngay.EditValue = DateTime.Now;
        }
        //Tháng 1->12 , Dùng hàm lấy ngày bắt đầu của tháng 
        //truyền tham số là tháng cần lấy và năm hiện tại
        private void Thang(DateEdit tungay, DateEdit denngay, int thang)
        {
            DateTime dauthang = HelpDate.GetStartOfMonth(thang, DateTime.Now.Year);
            tungay.EditValue = dauthang;
            DateTime cuoithang = HelpDate.GetEndOfMonth(thang, DateTime.Now.Year);
            denngay.EditValue = cuoithang;
        }
        // Ngày đầu năm đến nay
        private void Ngaydaunamdennay(DateEdit tungay, DateEdit denngay)
        {
            DateTime ngaydaunam = HelpDate.GetStartOfCurrentYear();
            tungay.EditValue = ngaydaunam;
            denngay.EditValue = DateTime.Now;
        }
        private void cbkybaocao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbkybaocao.Text == "Ngày hiện tại")
            {
                Ngayhientai(this._TuNgay, this._DenNgay);            
            }
            else if (cbkybaocao.Text == "Tháng hiện tại")
            {
                Thanghientai(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "Tuần hiện tại")
            {
                Tuanhientai(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "Năm hiện tại")
            {
                Namhientai(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "Quý hiện tại")
            {
                Quyhientai(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "---Ngày hôm qua")
            {
                Ngayhomqua(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "---Tuần vừa rồi")
            {
                Tuanvuaroi(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "---Tháng trước")
            {
                Thangtruoc(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "---Quý trước")
            {
                Quytruoc(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "---Năm trước")
            {
                Namtruoc(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "Ngày đầu tuần đến nay")
            {
                Ngaydautuandennay(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "Ngày đầu tháng đến nay")
            {
                Ngaydauthangdennay(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "Ngày đầu quý đến nay")
            {
                Ngaydauquydennay(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "Ngày đầu năm đến nay")
            {
                Ngaydaunamdennay(this._TuNgay, this._DenNgay);
            }
            else if (cbkybaocao.Text == "---Tháng 1")
            {
                Thang(this._TuNgay, this._DenNgay, 1);
            }
            else if (cbkybaocao.Text == "---Tháng 2")
            {
                Thang(this._TuNgay, this._DenNgay, 2);
            }
            else if (cbkybaocao.Text == "---Tháng 3")
            {
                Thang(this._TuNgay, this._DenNgay, 3);
            }
            else if (cbkybaocao.Text == "---Tháng 4")
            {
                Thang(this._TuNgay, this._DenNgay, 4);
            }
            else if (cbkybaocao.Text == "---Tháng 5")
            {
                Thang(this._TuNgay, this._DenNgay, 5);
            }
            else if (cbkybaocao.Text == "---Tháng 6")
            {
                Thang(this._TuNgay, this._DenNgay, 6);
            }
            else if (cbkybaocao.Text == "---Tháng 7")
            {
                Thang(this._TuNgay, this._DenNgay, 7);
            }
            else if (cbkybaocao.Text == "---Tháng 8")
            {
                Thang(this._TuNgay, this._DenNgay, 8);
            }
            else if (cbkybaocao.Text == "---Tháng 9")
            {
                Thang(this._TuNgay, this._DenNgay, 9);
            }
            else if (cbkybaocao.Text == "---Tháng 10")
            {
                Thang(this._TuNgay, this._DenNgay, 10);
            }
            else if (cbkybaocao.Text == "---Tháng 11")
            {
                Thang(this._TuNgay, this._DenNgay, 11);
            }
            else if (cbkybaocao.Text == "---Tháng 12")
            {
                Thang(this._TuNgay, this._DenNgay, 12);
            }          
        }
    }
}
