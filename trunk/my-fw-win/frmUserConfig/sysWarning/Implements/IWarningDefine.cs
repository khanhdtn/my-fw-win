using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Plugin.WarningSystem
{
    public enum WarningExecType
    {
        LoopTime = 2,
        FirstTime = 1
    }

    public interface IWarning
    {
        List<IWarningDefine> GetWarning();
    }

    public interface IWarningDefine
    {
        string Name { get; set; }
        WarningExecType Type { get; set; }
        string Description { get; set; }
        PLOut getOutputType();

        bool Install();
        bool Uninstall();

        bool Start();
        bool Stop();

        int getPeriod();
        void SetParams(List<object> param);
        
        bool CheckConfig();
        void ShowConfig(int war_id);

        object Supervise();                        
    }

    public abstract class AWarningDefine : IWarningDefine{
        
        protected string _name;
        protected WarningExecType _type;
        protected string _des;
        
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public WarningExecType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public string Description
        {
            get { return _des; }
            set { _des = value; }
        }

        #region IWarningDefine Members


        public abstract PLOut getOutputType();

        public abstract bool Install();
        public abstract bool Uninstall();

        public abstract bool Start(); 
        public abstract bool Stop();
        
        public abstract int getPeriod();

        public abstract void SetParams(List<object> param);

        public abstract bool CheckConfig();
        public abstract void ShowConfig(int war_id);
        
        public abstract object Supervise();

        #endregion
    }
}
