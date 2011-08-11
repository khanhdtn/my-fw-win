using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace ProtocolVN.Plugin.VietInput
{
    public struct StructFuncArg
    {
        public PLVietKey.FuncArg funcArg;
        public Object oParam;

        public StructFuncArg(PLVietKey.FuncArg funcArg , Object oParam)
        {
            this.funcArg = funcArg;
            this.oParam = oParam;
        }
    }
    /// <summary>
    /// Lớp hỗ trợ xử lý trên bàn phím 
    /// </summary>
    public class PLVietKey
    {
        public static VietKeyHandler.InputType KieuGo = VietKeyHandler.InputType.Vni;

        private Dictionary<Keys , Func> dicKeyFunc;
        private Dictionary<Keys , StructFuncArg> dicKeyFuncArg;
        private XtraForm frmOwn;
        private VietKeyHandler vietkey;

        //public PLVietKey(XtraForm frm,VietKeyHandler.InputType kieugo)
        //{
        //    this.dicKeyFunc = new Dictionary<Keys , Func>();
        //    this.dicKeyFuncArg = new Dictionary<Keys , StructFuncArg>();
        //    this.frmOwn = frm;
        //    this.frmOwn.KeyPreview = true;
        //    this.frmOwn.KeyUp += new KeyEventHandler(frmOwn_KeyUp);
        //    this.frmOwn.KeyPress += new KeyPressEventHandler(frmOwn_KeyPress);
        //    vietkey = new VietKeyHandler();
        //    vietkey.InputMethods = kieugo;
        //}

        public PLVietKey(XtraForm frm)
        {
            this.dicKeyFunc = new Dictionary<Keys , Func>();
            this.dicKeyFuncArg = new Dictionary<Keys , StructFuncArg>();
            this.frmOwn = frm;
            this.frmOwn.KeyPreview = true;
            this.frmOwn.KeyUp += new KeyEventHandler(frmOwn_KeyUp);
            this.frmOwn.KeyPress += new KeyPressEventHandler(frmOwn_KeyPress);
            vietkey = new VietKeyHandler();
        }

        //public VietKeyHandler.InputType InputType
        //{
        //    set {vietkey.InputMethods = value;  }
        //    get { return vietkey.InputMethods; }
        //}

        public VietKeyHandler.InputType InputType
        {
            set { PLVietKey.KieuGo = value; }
            get { return PLVietKey.KieuGo; }
        }

        void frmOwn_KeyPress(object sender , KeyPressEventArgs e)
        {
            vietkey.Process(e.KeyChar);
        }

        void frmOwn_KeyUp(object sender , KeyEventArgs e)
        {
            if (this.dicKeyFunc.ContainsKey(e.KeyData))
            {
                this.dicKeyFunc[e.KeyData]();
            }
            else if (this.dicKeyFuncArg.ContainsKey(e.KeyData))
            {
                this.dicKeyFuncArg[e.KeyData].funcArg(this.dicKeyFuncArg[e.KeyData].oParam);
            }
        }

        public void Add(Keys key , Func func)
        {
            this.dicKeyFunc.Add(key , func);
        }

        public void Add(Keys key , StructFuncArg structfuncArg)
        {
            this.dicKeyFuncArg.Add(key , structfuncArg);
        }

        public delegate void Func();
        public delegate void FuncArg(Object s);
    }
}
