using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ làm việc với tập tin Zip
    /// </summary>
    public class ZipFile
    {
        public static List<string> GetFileNames(string zipFilePath)
        {
            ZipInputStream zipStream = null;
            List<string> filenames = new List<string>();
            try
            {
                
                zipStream = new ZipInputStream(File.OpenRead(zipFilePath));
                ZipEntry entry;
                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    filenames.Add(Path.GetFileName(entry.Name));
                }
                zipStream.Close();
            }
            catch
            {
                return null;
            }
            return filenames;
        }
        public static bool UnZip(string desPath, string zipFilePath)
        {
            ZipInputStream zipStream = null;
            try
            {
                zipStream = new ZipInputStream(File.OpenRead(zipFilePath));
                ZipEntry entry;
                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);

                    if (directoryName.Length > 0)
                    {
                        string directory = desPath + @"\" + directoryName;
                        if(!Directory.Exists(directory)) 
                            Directory.CreateDirectory(directory);
                    }

                    if ((fileName != string.Empty))
                    {
                        string filePath = desPath + @"\" + entry.Name;
                        FileStream streamWriter = File.Create(filePath);
                        int size;
                        byte[] data = new byte[(int)zipStream.Length];

                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);

                            if (size > 0)
                                streamWriter.Write(data, 0, data.Length);
                            else
                                break;
                        }

                        streamWriter.Close();
                    }
                }
                zipStream.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// DUYVT: Hàm hổ trợ zip một tập tin chị định
        /// </summary>
        /// <param name="filePath">Đường dẫn tập tin cần zip</param>
        /// <param name="outputFileName">Tên tập tin đã zip</param>
        public static bool Zip(string filePath, string outputFileName)
        {
            ZipOutputStream oZipStream = null;
            try
            {
                int TrimLength = (Directory.GetParent(filePath)).ToString().Length;

                string filePath_tmp = filePath;
                filePath_tmp = filePath_tmp.Remove(filePath.LastIndexOf(@"\") + 1);
                
                string outPath = "";
                if (outputFileName.IndexOf(@"\") < 0)
                    outPath = filePath_tmp + @"\" + outputFileName;
                else
                    outPath = outputFileName;

                FileStream ostream;
                oZipStream = new ZipOutputStream(File.Create(outPath)); //create zip stream
                oZipStream.SetLevel(9); //maximum compression

                ZipEntry oZipEntry;
                byte[] obuffer;

                oZipEntry = new ZipEntry(filePath.Remove(0, TrimLength));
                oZipStream.PutNextEntry(oZipEntry);
                if (!filePath.EndsWith(@"/")) // if a file ends with '/' its a directory
                {
                    ostream = File.OpenRead(filePath);
                    obuffer = new byte[ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    oZipStream.Write(obuffer, 0, obuffer.Length);
                    ostream.Close();
                }
                oZipStream.Finish();
                oZipStream.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// DUYVT: Hàm hổ trợ zip một thư mục chỉ định
        /// </summary>
        /// <param name="folderPath">Đường dẫn thư mục cần zip</param>
        /// <param name="outputFileName">Tên tập tin đã zip</param>
        /// <returns></returns>
        public static bool ZipFolder(string folderPath, string outputFileName)
        {
            ZipOutputStream oZipStream = null;
            try
            {
                //generate file list
                ArrayList ar = GenerateFileList(folderPath);

                int TrimLength = (Directory.GetParent(folderPath)).ToString().Length;
                folderPath = folderPath.Remove(folderPath.LastIndexOf(@"\") + 1);
                string outPath = folderPath + @"\" + outputFileName;

                FileStream ostream;
                oZipStream = new ZipOutputStream(File.Create(outPath)); //create zip stream
                oZipStream.SetLevel(9); //maximum compression

                ZipEntry oZipEntry;
                byte[] obuffer;
                foreach (string Fil in ar) //for each file, generate a zipEntry
                {
                    oZipEntry = new ZipEntry(Fil.Remove(0, TrimLength));
                    oZipStream.PutNextEntry(oZipEntry);

                    if (!Fil.EndsWith(@"/")) //if a file ends with '/' its a directory
                    {
                        ostream = File.OpenRead(Fil);
                        obuffer = new byte[ostream.Length];
                        ostream.Read(obuffer, 0, obuffer.Length);
                        oZipStream.Write(obuffer, 0, obuffer.Length);
                    }
                }
                oZipStream.Finish();
                oZipStream.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static ArrayList GenerateFileList(string Dir)
        {
            ArrayList fils = new ArrayList();
            bool Empty = true;
            foreach (string file in Directory.GetFiles(Dir)) //add each file in directory
            {
                fils.Add(file);
                Empty = false;
            }

            if (Empty)
            {
                if (Directory.GetDirectories(Dir).Length == 0)
                {
                    //if directory is completely empty, add it
                    fils.Add(Dir + @"/");
                }
            }

            foreach (string dirs in Directory.GetDirectories(Dir)) //recursive
                foreach (object obj in GenerateFileList(dirs))
                    fils.Add(obj);
            return fils; // return file list
        }
    }
}
