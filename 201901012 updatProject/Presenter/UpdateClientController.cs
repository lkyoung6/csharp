using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UpdateUpload.Interface;
using UpdateUpload.Model;
using WinSCP;

namespace UpdateUpload.Presenter
{
    class UpdateClientController : IUpdateClientController
    {
        #region Variable

        #region Constant

        private const string VersionTxtFileName = "Version.txt";
        private const string RequestUriString = "ftp://192.168.53.26/";
        private const string UserName = "leetoya";
        private const string Password = "0000";
        private const string DoubleBackslash = "\\";
        private const string Slash = "/";
        private const string DoublePipe = "||";
        private const string DotTxtString = ".txt";

        #endregion

        #region Property

        #endregion

        #endregion

        #region Constructor

        public UpdateClientController(FormUpdateClient formUpdateClient)
        {
            this.FormUpdateClient = formUpdateClient;

        }

        #endregion

        #region Function

        #region Private

        private bool DoesFtpDirectoryExist(string existedfolder)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RequestUriString);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(UserName, Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            using (StreamReader reader = new StreamReader(responseStream))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line == existedfolder)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        private string GetCRCTxtFileContent(string[] fullPathfileNames, string dllFileVersion)
        {
            List<FileInformationNew> fileInfomationList = new List<FileInformationNew>();
            FileInformationNew fileInformation;

            string fileName = string.Empty;

            foreach (string fullPathfileName in fullPathfileNames)
            {
                fileInformation = new FileInformationNew();
                fileName = Path.GetFileName(fullPathfileName);
                fileInformation.FileName = fileName;
                fileInformation.Crc32 = Crc32.CRC32Bytes(File.ReadAllBytes(fullPathfileName));

                fileInfomationList.Add(fileInformation);
            }
            StringBuilder stringBuilder = new StringBuilder(1000);

            stringBuilder.AppendLine(dllFileVersion);

            foreach (var fileInfomation in fileInfomationList)
            {
                stringBuilder.AppendLine(string.Format("{0}{1}{2}", fileInfomation.FileName ,DoublePipe , fileInfomation.Crc32));
            }

            string content = stringBuilder.ToString();

            return content;
        }

        #endregion

        #endregion

        #region Interface

        #region IDisposable

        public void Dispose()
        {
        }

        #endregion

        #region IUpdateClientPresenter 

        #region Property

        public IFormUpdateClient FormUpdateClient { get; private set; }

        #endregion

        #region Function

        public int MakeFolderForDll(string dllFileVersion)
        {
            try
            {
                if (!DoesFtpDirectoryExist(dllFileVersion))
                {
                    WebRequest request = WebRequest.Create(string.Format("{0}{1}{2}", RequestUriString , DoubleBackslash , dllFileVersion));
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(UserName, Password);

                    using (var response = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return -1;
            }
        }

        public int UploadDllFilesToFTP(DirectoryInfo directoryInfo, string dllFileVersion)
        {
            try
            {
                FileInfo[] fileInfoList = directoryInfo.GetFiles();

                if (fileInfoList.GetLength(0) > 0)
                {
                    foreach (FileInfo fileInfo in fileInfoList)
                    {
                        string fileFullPath = (string.Format("{0}{1}{2}{3}", RequestUriString , dllFileVersion , Slash, fileInfo.Name));
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fileFullPath);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential(UserName, Password);

                        byte[] fileContents;

                        using (StreamReader sourceStream = new StreamReader(fileInfo.FullName))
                        {
                            fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                        }

                        request.ContentLength = fileContents.Length;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileContents, 0, fileContents.Length);
                        }
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return -1;
            }
        }

        public int UploadVersionTxtfileToFTP(string corparationName, string dllFileVersion)
        {
            try
            {
                string TxtFileName = string.Format("{0}{1}", corparationName , VersionTxtFileName);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Format("{0}{1}", RequestUriString ,TxtFileName));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(UserName, Password);

                byte[] fileContents;

                fileContents = Encoding.UTF8.GetBytes(dllFileVersion);

                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return -1;
            }
        }

        public int UploadCRCTxtFileToFTP(string sourceFolderPath, string dllFileVersion)
        {
            try
            {
                string fullPath = string.Format("{0}{1}{2}", RequestUriString , dllFileVersion , DotTxtString);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fullPath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(UserName, Password);

                string[] fullPathfileNames = Directory.GetFiles(sourceFolderPath, "*", SearchOption.TopDirectoryOnly);
                string txtContent = GetCRCTxtFileContent(fullPathfileNames, dllFileVersion);

                byte[] fileContents;

                fileContents = Encoding.UTF8.GetBytes(txtContent);

                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return -1;
            }
        }

        #endregion

        #endregion

        #endregion

    }
}
