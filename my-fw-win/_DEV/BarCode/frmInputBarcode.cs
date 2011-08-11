using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;


namespace ProtocolVN.Framework.Win
{
    
    public partial class frmInputBarcode : XtraForm
    {
        public static double MAX_SO_LUONG = 9999;
        
        /// <summary>Hàm này kiểm tra xem 1 BARCODE có hợp lệ không
        /// Nếu hợp lệ trả về chuỗi != ""
        /// Nếu không hợp lệ trả về chuỗi rỗng
        /// </summary>
        /// <param name="barCode">Đây chính là chuỗi barCode nhận được từ máy</param>
        /// <returns></returns>
        public delegate String TraCuu(String barCode);
        /// <summary>Hàm này thực hiện mỗi khi có 1 barCode 
        /// hợp lệ được đưa vào hệ thống. XtraForm chính là formBarCode.
        /// Từ Form này có thể thao tác lấy thông tin về barCode và số lượng để xử lý
        /// + Số lượng hay trọng lượng lấy từ thuộc tính _So
        /// + Mã vạch lấy từ thuộc tính _MaVach
        /// </summary>
        /// <param name="frmBarcode"></param>
        /// <returns></returns>
        public delegate bool ThucHien(frmInputBarcode frmBarcode);

        public delegate bool CapNhatGiaTriKhiBarCodeHopLe(frmInputBarcode frmBarCode);

        private TraCuu _TraCuu;
        private ThucHien _ThucHien;
        private CapNhatGiaTriKhiBarCodeHopLe _CapNhatGiaTriKhiBarCodeHopLe;
        /// <summary>
        /// Lấy mã vạch vừa nhập thành công vào hệ thống. 
        /// Thành công có nghĩa là được chấp nhận và có số lượng là 1
        /// </summary>
        public String _MaVach;
        /// <summary>
        /// Lấy phần giá trị số vừa nhập vào
        /// </summary>
        public String _SoLg;

        private frmInputBarcode(XtraForm main, TraCuu traCuu, ThucHien thucHien, CapNhatGiaTriKhiBarCodeHopLe capNhat, BarcodeInputType type)
        {
            InitializeComponent();
            this._SoLg = "1";
            this._TraCuu = traCuu;
            this._ThucHien = thucHien;
            this._CapNhatGiaTriKhiBarCodeHopLe = capNhat;
            if(type == BarcodeInputType.TYPE1){
                this.barCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.barCode_KeyUp2);
                this.Slg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Slg_KeyUp2);
                this.FormClosing += new FormClosingEventHandler(this.frmInputBarcode__FormClosing);
                this.Slg.TextChanged += new EventHandler(Slg_TextChanged);
            }

            this.Top = main.Top;
            this.Left = main.Left + (main.Width - this.Width) / 2;

            PLKey key = new PLKey(this);
            key.Add(Keys.F5, delegate(){
                this.Close();
            });
        }
        
        #region Giải pháp 1
        //mac dinh slg=1 khi xoa user xoa trang ô nhập slg.
        void Slg_TextChanged(object sender, EventArgs e)
        {
            if (this.Slg.Text == "")
            {
                this.Slg.Text = "1";
                this.Slg.Refresh();
            }
        } 

        //khi dong form van thuc hien nhan barcode hien tai
        void frmInputBarcode__FormClosing(object sender, FormClosingEventArgs e)
        {
            //neu ma vach ko ton tai hay so luong nhap ko dung thi close form ko thuc hien nhan barcode
            string tenSP = "";
            tenSP = this._TraCuu(this.barCode.Text);
            if (tenSP == "") return;
            double sl = (double)HelpNumber.ParseDecimal(this.Slg.Text);
            if (sl <= 0 || sl > MAX_SO_LUONG) return;

            #region Điều kiện hợp lệ
            this._MaVach = this.barCode.Text;
            this._SoLg = this.Slg.Text;
            // thuc hien duoc hay khong van dong form barcode  
            this._ThucHien(this);

            #endregion

        }
        private void barCode_KeyUp2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String tenSP = this._TraCuu(barCode.Text);

                if (tenSP == "")
                {
                    this.barCode.Focus();
                    this.barCode.SelectAll();
                }
                else
                {
                    this._MaVach = this.barCode.Text;
                    //this._CapNhatGiaTriKhiBarCodeHopLe(this);

                    this.Slg.Focus();
                    this.Slg.SelectAll();
                }

                if (tenSP != "") this._TenSP.Text = tenSP;
                return;
            }
        }
        private void Slg_KeyUp2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region Vào Barcode hiện tại
                bool flag = false;
                bool invalidBarcode = false;
                this._MaVach = this.barCode.Text;
                string tenSP = "";
                tenSP = this._TraCuu(this.Slg.Text);
                if (tenSP != "")
                    //Barcode vào ngay phần số lượng
                {
                    flag = true;
                    this._SoLg = "1";
                }
                else
                    //Nhập số lượng || Barcode Invalid
                {
                    //Hạn chế chỗ này vì đã hạn chế mã vạch phải có tối đa 4 chữ số.
                    //Trong trường hợp mã vạch không phải dạng số.
                    if (this.Slg.Text.StartsWith("0") &&
                      (!this.Slg.Text.StartsWith("0.") || !this.Slg.Text.StartsWith("0,")))
                    {
                        this._SoLg = "1";
                        flag = true;
                        invalidBarcode = true;
                    }
                    else
                    {
                        double sl = (double)HelpNumber.ParseDecimal(this.Slg.Text);//Double.Parse(this.Slg.Text);
                        if (sl > 0 && sl < MAX_SO_LUONG)
                        {
                            this._SoLg = this.Slg.Text;
                        }
                        else
                        {
                            this._SoLg = "1";
                            flag = true;
                            invalidBarcode = true;
                        }
                    }
                }
                tenSP = this._TraCuu(_MaVach);
                if (tenSP != "")
                {
                    if (this._ThucHien(this))
                    {
                        if (flag == true)
                        {
                            if(invalidBarcode == false){
                                this.barCode.Text = this.Slg.Text;
                                this.barCode.Focus();
                                SendKeys.Send("{TAB}");
                                this._MaVach = this.barCode.Text;
                                //this._CapNhatGiaTriKhiBarCodeHopLe(this);
                            }
                            else{
                                this.barCode.Text = this.Slg.Text;
                                this.barCode.Focus();
                                this.barCode.SelectAll();
                                this._GiaLbl.Text = "-";
                                this._TenSP.Text = "Mã vạch không hợp lệ";
                                this.Slg.Text = "1";
                            }
                        }
                        else
                        {
                            this.barCode.Text = "";
                            this.barCode.Focus();
                        }
                        this.Slg.Text = "1";
                    }
                    else
                    {
                        this.barCode.Focus();
                        this.barCode.SelectAll();
                        this._GiaLbl.Text = "-";
                        this._TenSP.Text = "Mã vạch không hợp lệ";
                        this.Slg.Text = "1";
                    }
                }
                else
                {
                    this.barCode.Focus();
                    this.barCode.SelectAll();
                    this._GiaLbl.Text = "-";
                    this._TenSP.Text = "Mã vạch không hợp lệ";
                    this.Slg.Text = "1";
                }
                #endregion
 
                if(tenSP!="") this._TenSP.Text = tenSP;
            }
        }

        public static frmInputBarcode init(XtraForm main, IInputBarcode inputBarcode)
        {
            frmInputBarcode frm = new frmInputBarcode(main, inputBarcode.kiemTraMaVach, inputBarcode.chonSanPham, inputBarcode.capNhatTenSanPham, BarcodeInputType.TYPE1);
            return frm;
        }
        #endregion

        private void frmInputBarcode_Load(object sender, EventArgs e)
        {
            
        }
    }

    public enum BarcodeInputType
    {
        TYPE1
    }    
}
