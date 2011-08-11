using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ làm việc với thời gian
    /// 
    /*  d - Numeric day of the month without a leading zero.
        dd - Numeric day of the month with a leading zero.
        ddd - Abbreviated name of the day of the week.
        dddd - Full name of the day of the week.

        f,ff,fff,ffff,fffff,ffffff,fffffff - 
	        Fraction of a second. The more Fs the higher the precision.

        h - 12 Hour clock, no leading zero.
        hh - 12 Hour clock with leading zero.
        H - 24 Hour clock, no leading zero.
        HH - 24 Hour clock with leading zero.

        m - Minutes with no leading zero.
        mm - Minutes with leading zero.

        M - Numeric month with no leading zero.
        MM - Numeric month with a leading zero.
        MMM - Abbreviated name of month.
        MMMM - Full month name.

        s - Seconds with no leading zero.
        ss - Seconds with leading zero.

        t - AM/PM but only the first letter. 
        tt - AM/PM ( a.m. / p.m.)

        y - Year with out century and leading zero.
        yy - Year with out century, with leading zero.
        yyyy - Year with century.

        zz - Time zone off set with +/-.
     */    
    public class HelpTime
    {
        public static int CalTime(object gioDi, object gioVe)
        {
            int time = -1;
            try
            {
                TimeSpan tgDi = Convert.ToDateTime(gioDi).TimeOfDay;
                TimeSpan tgVe = Convert.ToDateTime(gioVe).TimeOfDay;
                if ((tgVe - tgDi).Hours < 0)
                    time = 24 - tgDi.Hours + tgVe.Hours;
                else
                    time = (tgVe - tgDi).Hours;
            }
            catch { }
            return time;
        }

        public static void SetTime(DevExpress.XtraEditors.TimeEdit Ctrl, TimeSpan? Time)
        {
            try
            {
                Ctrl.EditValue = Time;
            }
            catch (Exception) { }
        }

        public static TimeSpan? GetTime(DevExpress.XtraEditors.TimeEdit Ctrl)
        {
            try
            {
                DateTime d = (DateTime)Ctrl.EditValue;
                TimeSpan? time = new TimeSpan(d.Ticks);
                return time;
            }
            catch (Exception) { return null; }
        }

        public static void SetFormat(DevExpress.XtraEditors.TimeEdit control)
        {
            if (control.Properties.Buttons.Count < 1)
                control.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                        new DevExpress.XtraEditors.Controls.EditorButton()});
            control.Properties.Mask.EditMask = FrameworkParams.option.timeFormat;
            control.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

    }
}
