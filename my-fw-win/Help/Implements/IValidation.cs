using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;

namespace ProtocolVN.Framework.Win
{
    public interface IValidation
    {
        void SetError(DXErrorProvider errorProvider, string errorMsg);
    }
    
    public interface ITextValidation : IValidation
    {
        string GetData();
    }

    public interface IIDValidation : IValidation
    {
        long _getSelectedID();
    }

    public enum OutputError
    {
        ERROR_PROVIDER,
        MSG_BOX,
        NONE
    }

    public enum CheckType
    {
        DecGreaterZero,
        DecGreater0,
        DecGreaterEqual0,
        DecALessB,
        DecALessEqualB,
        IntGreaterZero,
        IntGreater0,
        IntGreaterEqual0,
        IntALessB,
        IntALessEqualB,
        RequireEmail,
        OptionEmail,
        RequireMaxLength,
        OptionMaxLength,
        RequireDate,
        OptionDate,
        DateALessB,
        DateALessEqualB,
        Required,
        RequiredID
    }

    public class FieldNameCheck
    {
        public string FieldName;
        public CheckType[] Types;
        public string[] ErrMsgs;
        public string Subject;
        public object[] Params;
        public OutputError Output = OutputError.ERROR_PROVIDER;

        public FieldNameCheck(string FieldName, CheckType[] Types, string[] ErrMsgs, OutputError Output, object[] Other)
        {
            this.FieldName = FieldName;
            this.Types = Types;
            this.ErrMsgs = ErrMsgs;
            this.Params = Other;
            this.Output = Output;
        }
        public FieldNameCheck(string FieldName, CheckType[] Types, string[] ErrMsgs, object[] Other)
        {
            this.FieldName = FieldName;
            this.Types = Types;
            this.ErrMsgs = ErrMsgs;
            this.Params = Other;
            this.Output = OutputError.ERROR_PROVIDER;
        }
        public FieldNameCheck(string FieldName, CheckType[] Types, string Subject, object[] Other)
        {
            this.FieldName = FieldName;
            this.Types = Types;
            this.Subject = Subject;
            this.Params = Other;
            this.Output = OutputError.ERROR_PROVIDER;
        }
    }
}
