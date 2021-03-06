using System;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
	public class Option
	{
        public static string OPTION_FILE = RadParams.RUNTIME_PATH + @"\conf\option.cpl";

        public String numFormat;
        public String round;//int
        public String thousandSeparator;//char
        public String decSeparator;//char
        public String dateFormat;
        public String timeFormat;
        public String dateTimeFormat;
        public String Skin;//int
        public String printerName;
        public String _IsHomePage;//Y/N
        public String _IsMinMenu;//Y/N

        private FAConfigOption config;

        public String DateTimeFomat
        {
            get
            {
                return this.dateFormat + " " + this.timeFormat;
            }
        }

        public Option()
        {
            try
            {
                config = new FAConfigOption();
                config.cfgFile = OPTION_FILE;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }

        public void load()
        //load data from XML file
        {          
            this.numFormat = config.GetValue("//option//add[@key='NumberFormat']");
            this.round = config.GetValue("//option//add[@key='Round']");
            this.thousandSeparator = config.GetValue("//option//add[@key='ThousandSeparator']");
            this.decSeparator = config.GetValue("//option//add[@key='DecSeparator']");
            this.dateFormat = config.GetValue("//option//add[@key='DateFormat']");
            this.timeFormat = config.GetValue("//option//add[@key='TimeFormat']");
            this.dateTimeFormat = config.GetValue("//option//add[@key='DateTimeFormat']");
            this.Skin = config.GetValue("//option//add[@key='Skin']");
            this.printerName = config.GetValue("//option//add[@key='PrinterName']");
            this._IsHomePage = config.GetValue("//option//add[@key='IsHomePage']");
            this._IsMinMenu = config.GetValue("//option//add[@key='IsMinMenu']");
        }

        public void update()
        //save to XML file
        {
            config.SetValue("//option//add[@key='NumberFormat']", numFormat);
            config.SetValue("//option//add[@key='Round']", round);
            config.SetValue("//option//add[@key='ThousandSeparator']", thousandSeparator);
            config.SetValue("//option//add[@key='DecSeparator']", decSeparator);
            config.SetValue("//option//add[@key='DateFormat']", dateFormat);
            config.SetValue("//option//add[@key='TimeFormat']", timeFormat);
            config.SetValue("//option//add[@key='DateTimeFormat']", dateTimeFormat);
            config.SetValue("//option//add[@key='Skin']", Skin);
            config.SetValue("//option//add[@key='PrinterName']", printerName);
            config.SetValue("//option//add[@key='IsHomePage']", _IsHomePage);
            config.SetValue("//option//add[@key='IsMinMenu']", _IsMinMenu);            
        }

        public static void saveDoc(string FileName)
        {
            string str =  
            @"<?xml version='1.0' encoding='utf-8'?>
            <configuration>
              <option>
                <add key='NumberFormat' value='' />
                <add key='Round' value='0' />
                <add key='ThousandSeparator' value='.' />
                <add key='DecSeparator' value=',' />
                <add key='DateFormat' value='dd/MM/yyyy' />
                <add key='TimeFormat' value='HH:mm:ss' />
                <add key='DateTimeFormat' value='' />
                <add key='Skin' value='37' />
                <add key='Lang' value='vn' />
                <add key='PrinterName' value='' />
                <add key='IdCountry' value='893' />
                <add key='IdProvider' value='123' />
                <add key='IdProduct' value='6' />
                <add key='StampWidth' value='200' />
                <add key='StampHeight' value='120' />
                <add key='BCWidth' value='200' />
                <add key='BCHeight' value='50' />
                <add key='BCModule' value='1' />
                <add key='UnitUsing' value='1' />
                <add key='UnitPos' value='1' />
                <add key='UnitAlight' value='0' />
                <add key='NameUsing' value='1' />
                <add key='NamePos' value='2' />
                <add key='NameAlight' value='0' />
                <add key='BCUsing' value='1' />
                <add key='BCPos' value='3' />
                <add key='BCAlight' value='0' />
                <add key='PriceUsing' value='1' />
                <add key='PricePos' value='4' />
                <add key='PriceAlight' value='0' />
                <add key='SymBC' value='-1' />
              </option>
            </configuration>";
            ConfigFile.WriteXML(FileName, str);
        }
	}


}