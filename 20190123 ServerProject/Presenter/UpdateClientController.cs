using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UpdateUpload.Interface;
using UpdateUpload.Model;
using DistributionUpdateLibrary;

namespace UpdateUpload.Presenter
{
    class UpdateClientController : IUpdateClientController
    {
        #region Variable

        NetworkCredential networkCredential;

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
            this.networkCredential = new NetworkCredential(UserName, Password);
        }

        #endregion

        #region Function

        #region Private

        private bool DoesFtpDirectoryExist(string existedfolder)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RequestUriString);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = this.networkCredential;

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

        private string GetCRCTxtFileContent(List<FileInformation> fileInfomationList, string dllFileVersion)
        {
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
                    request.Credentials = this.networkCredential;

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

        public int UploadDllFilesToFTP(List<FileInformation> fileInfomationList, string dllFileVersion)
        {
            try
            {
                if (fileInfomationList.Count > 0)
                {
                    foreach (FileInformation fileInfomation in fileInfomationList)
                    {
                        string fileFullPath = (string.Format("{0}{1}{2}{3}", RequestUriString, dllFileVersion, Slash, fileInfomation.FileName));
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fileFullPath);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = this.networkCredential;

                        byte[] fileContents = fileInfomation.bytes;

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
                request.Credentials = this.networkCredential;

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

        public int UploadCRCTxtFileToFTP(List<FileInformation> fileInfomationList, string dllFileVersion)
        {
            try
            {
                string fullPath = string.Format("{0}{1}{2}", RequestUriString , dllFileVersion , DotTxtString);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fullPath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = this.networkCredential;

                string txtContent = GetCRCTxtFileContent(fileInfomationList, dllFileVersion);
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

        public List<FileInformation> GetFileInformationList(string[] fullPathfileNames)
        {
            List<FileInformation> fileInfomationList = new List<FileInformation>();
            FileInformation fileInformation;

            string fileName = string.Empty;

            foreach (string fullPathfileName in fullPathfileNames)
            {
                fileInformation = new FileInformation();
                fileName = Path.GetFileName(fullPathfileName);
                fileInformation.FileName = fileName;
                fileInformation.bytes = File.ReadAllBytes(fullPathfileName);
                fileInformation.Crc32 = Crc32.CRC32Bytes(fileInformation.bytes);

                fileInfomationList.Add(fileInformation);
            }
            return fileInfomationList;
        }

        #endregion

        #endregion

        #endregion

    }
}
