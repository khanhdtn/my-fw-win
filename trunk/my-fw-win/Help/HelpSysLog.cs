#region using
using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Appender;
using ProtocolVN.Framework.Core;
using System.IO; 
#endregion

#region Đặc tả hệ thống log
///Đây là hệ thống log (Debug,Error,Info,...) sử dụng log4net có mở rộng
///.....
#endregion

namespace ProtocolVN.Framework.Win
{
    /// <summary>Lớp sử dụng log (Debug, Error, Info,...)
    /// Sử dụng log bằng phương thức tĩnh HelpSysLog.{Phương thức} 
    /// hoặc HelpSysLog.log. {Phương thức log của log4net}
    /// </summary>
    public class HelpSysLog
    {
        #region Fields
        /// <summary>Biến chứa các phương thức log
        /// </summary>
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(
           System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// File cấu hình mặc định
        /// </summary>
        private static string logFile = @"log4net.config";
        #endregion

        #region Log Error
        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="ex">Exception error</param>
        /// <param name="Subject">Nội dung thông tin cần ghi thêm</param>
        public static void AddException(Exception ex, object Subject)
        {
            log.Error(Subject, ex);
        }

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="ex">Exception error</param>
        public static void AddException(Exception ex)
        {
            AddException(ex, null);
        }
        #endregion

        #region Các phương thức đăng ký sử dụng log (Được đặt ở nơi khởi tạo ban đầu khi chương trình Start)
        /// <summary>Đăng ký sử dụng log 
        /// </summary>
        public static void RegisterConfig()
        {
            RegisterConfig(@"log4net.config");
        }

        /// <summary>Đăng ký sử dụng log
        /// </summary>
        /// <param name="fileConfig">File cấu hình log</param>
        public static void RegisterConfig(string fileConfig)
        {
            if (System.IO.File.Exists(fileConfig))
            {
                //Nếu tồn tại file cấu hình thì sử dụng file cấu hình này
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(fileConfig));
            }
            else
            {
                //Không tồn tại file cấu hình thì tạo mới
                StreamWriter sw = new StreamWriter(logFile);
                string xml = CreateXmlConfig().Replace(@"'", @"""");
                sw.WriteLine(xml);
                sw.Close();
            }
        }

        /// <summary>Đăng ký sử dụng log
        /// Nếu file fileConfig tồn tại thì sử dụng file này luôn, nếu không tồn tại thì lấy chuỗi
        /// xmlConfile để tạo file config với đường dẫn file chính là fileConfig
        /// </summary>
        /// <param name="fileConfig"></param>
        /// <param name="xmlConfig"></param>
        public static void RegisterConfig(string fileConfig, string xmlConfig)
        {
            if (System.IO.File.Exists(fileConfig))
            {
                //Nếu tồn tại file cấu hình thì sử dụng file cấu hình này
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(fileConfig));
            }
            else
            {
                //Không tồn tại file cấu hình thì tạo mới
                StreamWriter sw = new StreamWriter(fileConfig);
                string xml = xmlConfig;
                sw.WriteLine(xml);
                sw.Close();
            }
        }

        //Đây là nội dung mặc định cho file cấu hình, sau đó nếu người dùng muốn điều chỉnh lại thì 
        //vào file được tạo chỉnh sửa 
        private static string CreateXmlConfig()
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append(@"<?xml version='1.0' encoding='utf-8' ?>
<!-- Mọi chi tiết cấu hình vui lòng liên hệ Email : chautv@protocolvn.net , Phone : 0938.890.903-->
<configuration>
    <configSections>
        <section name='log4net' type='log4net.Config.Log4NetConfigurationSectionHandler, log4net'/>
    </configSections>
    
    <!-- log4net -->
    
    <log4net>
        <!-- Define some output appenders -->
        <appender name='PLRollingFileAppender' type='ProtocolVN.Framework.Win.PLLog4NetFileAppender'>
          <!--Cấu hình đường dẫn file trong lớp ConfigSevice (Nếu không có thì lấy File mặc định này)-->     
            <param name='File' value='\\logs\\PROTOCOLVN-username-MM-dd-yyyy.txt' />
            <param name='AppendToFile' value='true' />
            <param name='RollingStyle' value='Size' />
            <param name='StaticLogFileName' value='true' />
            <param name ='maxSizeRollBackups' value='10000' />
            <!--Cứ 2MB là tạo một file mới theo nguyên tắc đặt trong lớp cấu hình ConfigSevice-->
            <param name ='maximumFileSize' value='2MB' />
            <layout type='log4net.Layout.PatternLayout'>
                <param name='Header' value='[Header]&#13;&#10;' />
                <param name='Footer' value='[Footer]&#13;&#10;' />
                <param name='ConversionPattern' value='%d [%t] %-5p %C %M - %m%n' />
            </layout>
        </appender>
        <!-- Setup the root category, add the appenders and set the default level -->
        <!-- Setup the root category, add the appenders and set the default level
                ALL 
                DEBUG 
                INFO 
                WARN 
                ERROR 
                FATAL 
                OFF
            For example, setting the threshold of an appender to DEBUG will also allow INFO, 
            WARN, ERROR and FATAL messages to log along with DEBUG messages. (DEBUG is the 
            lowest level). This is usually acceptable as there is little use for DEBUG 
            messages without the surrounding INFO, WARN, ERROR and FATAL messages. 
            Similarly, setting the threshold of an appender to ERROR will filter out DEBUG, 
            INFO and ERROR messages but not FATAL messages.   
        -->
        <root>
            <level value='ALL' />
            <appender-ref ref='PLRollingFileAppender' />
        </root>
    </log4net>
</configuration>
            ");
            return sbXml.ToString();
        }
        #endregion
    }

    #region Các lớp hỗ trợ log (Được thực hiện theo hướng kế thừa của log4net)
    /// <summary>The application configuration service.
    /// </summary>
    public interface IConfigService
    {
        #region Public Methods

        /// <summary>
        /// Returns the application log level.
        /// </summary>
        /// <returns>
        /// The application log level. <see cref="log4net.Core.Level"/>
        /// </returns>
        string GetLogLevel();

        /// <summary>
        /// Gets the application log path.
        /// </summary>
        /// <returns>
        /// The application log path.
        /// </returns>
        string GetLogPath();

        /// <summary>
        /// Gets the application log FileName
        /// </summary>
        /// <returns>
        /// The application log FileName.
        /// </returns>
        string GetLogFileName();
        #endregion
    }

    /// <summary>The application PLLog4NetFileAppender (Kế thừa RollingFileAppender của log4net)
    /// </summary>
    public class PLLog4NetFileAppender : RollingFileAppender
    {
        #region Constants and Fields

        /// <summary>
        /// Component parameter business layer
        /// </summary>
        private static IConfigService service;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyLog4NetFileAppender"/> class.
        /// </summary>
        public PLLog4NetFileAppender()
            : this(new ConfigService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyLog4NetFileAppender"/> class.
        /// </summary>
        /// <param name="configService">
        /// The config service.
        /// </param>
        public PLLog4NetFileAppender(IConfigService configService)
        {
            service = configService;

            // get the log level 
            // must be a proper log4net Threshold
            string logLevel = service.GetLogLevel();

            // set the log level
            LogManager.GetRepository().Threshold = LogManager.GetRepository().LevelMap[logLevel];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the log file name.
        /// </summary>
        /// <value>The log file name.</value>
        public override string File
        {
            get
            {
                return base.File;
            }

            set
            {
                try
                {
                    // get the log directory
                    string logDirectory = service.GetLogPath();

                    // get the log file name from the config file.
                    string logFileName = service.GetLogFileName();

                    // build the new log path
                    if (!logDirectory.EndsWith("\\") || !logDirectory.EndsWith("/"))
                    {
                        logDirectory += "\\";
                    }

                    // replace the new log file path
                    base.File = logDirectory + logFileName;
                }
                catch (Exception ex)
                {
                    // TODO: Log the error
                    // use the default
                    base.File = value;
                }
            }
        }

        #endregion
    }

    /// <summary>The application configuration service.
    /// </summary>
    public class ConfigService : IConfigService
    {
        public static ConfigService Instance = new ConfigService();
        public ConfigService() { }
        #region Implemented Interfaces

        #region IConfigService

        /// <summary>
        /// Returns the application log level.
        /// </summary>
        /// <returns>
        /// The application log level. <see cref="log4net.Core.Level"/>
        /// </returns>
        public string GetLogLevel()
        {
            // TODO: Get Threshold from database
            return "DEBUG";
        }

        /// <summary>
        /// Gets the application log path.
        /// </summary>
        /// <returns>
        /// The application log path.
        /// </returns>
        public string GetLogPath()
        {
            // TODO: Get log path from database
            return @"SysLog";
        }
        /// <summary>
        /// Gets the application log FileName.
        /// </summary>
        /// <returns>
        /// The application log FileName.
        /// </returns>
        public string GetLogFileName()
        {
            // TODO: Get log FileName from database
            //File log khi vượt quá giới hạn cho phép sẽ thực hiện 2 vấn đề sau
            //1.backup file hiện tại lại theo quy tắc {Tên hiện tại}{Số thứ tự đếm}
            //2.làm sạch file hiện tại để log tiếp
            //Với cấu hình tên dưới đây sẽ log được các file có dạng theo ngày hiện tại
            //File gốc : PROTOCOLVN-[username]-01-11-2010.txt
            //File backup : PROTOCOLVN-[username]-01-11-2010.txt1, PROTOCOLVN-[username]-01-11-2010.txt2,...
            //Qua ngày hôm sau : PROTOCOLVN-[username]-01-12-2010.txt, PROTOCOLVN-[username]-01-12-2010.txt1,PROTOCOLVN-[username]-01-11-2010.txt2,..
            //Muốn xem file log đã backup thì bỏ số thứ tự đi là mở được

            //CHAUTV TO PHUOCNT : Chú ý hiện tại gọi đăng ký log trong phương thức initAppParam() nên username sẽ chưa có nó sẽ rỗng
            //Để có được username thì yêu cầu khi đưa vào FW đặt hàm khởi tạo này tại một nơi thích hợp hơn
            return "PROTOCOLVN-" + FrameworkParams.currentUser.username + "-" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
        }
        #endregion

        #endregion
    }
    #endregion
}

