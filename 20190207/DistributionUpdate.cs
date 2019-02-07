using DistributionUpdateLibrary.DataContracts;
using DistributionUpdateLibrary.Interfaces;
using ServerHelper.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using FileInfo = DistributionUpdateLibrary.DataContracts.FileInfo;
using ServerHelperDotMgr = ServerHelper.Mgr;

namespace UpdateAndUploadLibrary
{
    public class DistributionUpdate : IDistributionUpdate
    {
        #region Variable

        #region Constant

        #endregion

        #region Property

        private WebClient WebClient { get; set; }

        #endregion

        #region Static

        static string UserName = ConfigurationManager.AppSettings["UserName"];
        static string Password = ConfigurationManager.AppSettings["Password"];
        static NetworkCredential NetworkCredential = new NetworkCredential(UserName, Password);

        #endregion

        #endregion

        #region Constructor

        public DistributionUpdate()
        {

        }

        public DistributionUpdate(WebClient webClient)
        {
            if (webClient != null)
            {
                this.WebClient = webClient;
                this.WebClient.Credentials = NetworkCredential;
                //this.WebClient.BaseAddress = uriString;
            }
        }


     

        #endregion

        #region Function

        #region Public

        public string getFileInfo(string encryptedCompanyName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo();
                string companyName = encryptedCompanyName.CreateRecvData<string>();

                if (!string.IsNullOrWhiteSpace(companyName))
                {

                    string ftpUri = ConfigurationManager.AppSettings["FtpUri"];
                    string fileName = string.Format("{0}{1}", companyName, "Version.txt");
                    string requestUriString = string.Format("{0}{1}", ftpUri, fileName);
                    string targetVersion = this.ExtractVersionFromTxtfile(companyName, requestUriString);

                    //string ftpUri2 = ConfigurationManager.AppSettings["FtpUri"];
                    string fileName2 = string.Format("{0}{1}", targetVersion, ".txt");
                    string requestUriString2 = string.Format("{0}{1}", ftpUri, fileName);

                    Dictionary<string, string> fileNameAndCrc = this.ExtractFileNamesAndCRCFromTxtfile(targetVersion, requestUriString2);

                    fileInfo.Version = targetVersion;
                    fileInfo.Files = fileNameAndCrc;
                }

                return fileInfo.CreateSendData();
            }
            catch (Exception e)
            {
                ServerHelperDotMgr.Log.Write<string>(e.ToString(), null);
                return string.Empty;
            }
        }

        public string Update(string encryptedFileRequest)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(encryptedFileRequest))
                {
                    FileRequest fileRequest = encryptedFileRequest.CreateRecvData<FileRequest>();
                    List<string> fileListNeededForUpdate = new List<string>();

                    fileListNeededForUpdate = fileRequest.Files;

                    FileRespon fileResponse = new FileRespon();

                    foreach (string fileName in fileListNeededForUpdate)
                    {
                        string ftpUri = ConfigurationManager.AppSettings["FtpUri"];
                        string requestUriString = string.Format("{0}{1}{2}{3}", ftpUri, fileRequest.Version, "/", fileName);

                        byte[] bytes = this.WebClient.DownloadData(requestUriString);
                        //byte[] bytes = this.ExtractBytesFromFtp(requestUriString);

                        if (bytes != null)
                        {
                            fileResponse.Files.Add(fileName, bytes);
                        }
                    }

                    return fileResponse.CreateSendData();
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                ServerHelperDotMgr.Log.Write<string>(e.ToString(), null);
                return string.Empty;
            }
        }

        public void LogWrite(string encrypedLog)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(encrypedLog))
                {
                    Log log = encrypedLog.CreateRecvData<Log>();
                    ServerHelperDotMgr.Log.Write(log.MB_ID, log.ExceptionMsg, log.data);
                }
            }
            catch (Exception e)
            {
                ServerHelperDotMgr.Log.Write<string>(e.ToString(), null);
            }
        }

        #endregion

        #region Private

        public Dictionary<string, string> ExtractFileNamesAndCRCFromTxtfile(string targetVersion, string requestUriString)
        {
            try
            {
                //string ftpUri = ConfigurationManager.AppSettings["FtpUri"];
                //string fileName = string.Format("{0}{1}", targetVersion, ".txt");
                //string requestUriString = string.Format("{0}{1}", ftpUri, fileName);

                //20190207 테스트 목적으로 작성
                //IFakeWebClient fakeWebClient = new FakeWebClient();
                //byte[] bytes = fakeWebClient.DownloadData();

                byte[] bytes = this.WebClient.DownloadData(requestUriString);
                //byte[] bytes = this.ExtractBytesFromFtp(requestUriString);
                string contents = Encoding.UTF8.GetString(bytes);
                string[] contentsLines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                Dictionary<string, string> fileNameAndCRCDictionary = new Dictionary<string, string>();

                if (contentsLines[0] == targetVersion)
                {
                    for (int i = 1; i < contentsLines.Length; i++)
                    {
                        string[] FileNameAndCrc = contentsLines[i].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                        if (FileNameAndCrc.Length == 2)
                        {
                            fileNameAndCRCDictionary.Add(FileNameAndCrc[0], FileNameAndCrc[1]);
                        }
                    }
                }
                return fileNameAndCRCDictionary;
            }
            catch (Exception e)
            {
                ServerHelperDotMgr.Log.Write<string>(e.ToString(), null);
                return null;
            }
        }

        //잠시 쉬었다가
        private string ExtractVersionFromTxtfile(string companyName,string requestUriString)
        {
            try
            {
                string version = string.Empty;

                byte[] bytes = this.WebClient.DownloadData(requestUriString);
                version = Encoding.UTF8.GetString(bytes);

                return version;
            }
            catch (Exception e)
            {
                ServerHelperDotMgr.Log.Write<string>(e.ToString(), null);
                return null;
            }
        }

        public Dictionary<string, byte[]> ExtractFileNameAndByte(string targetVersion, List<string> fileNameList)
        {
            try
            {
                Dictionary<string, byte[]> fileNameAndByte = new Dictionary<string, byte[]>();

                string ftpUri = ConfigurationManager.AppSettings["FtpUri"];

                foreach (var fileName in fileNameList)
                {
                    string requestUriString = string.Format("{0}{1}{2}{3}", ftpUri, targetVersion, "/", fileName);
                    //WebClient webClient = CreateWebClientObeject();
                    byte[] bytes = this.WebClient.DownloadData(requestUriString);
                    //byte[] bytes = this.ExtractBytesFromFtp(webClient,requestUriString);

                    //webClient.Dispose();

                    fileNameAndByte.Add(fileName, bytes);
                }
                return fileNameAndByte;
            }
            catch (Exception e)
            {
                ServerHelperDotMgr.Log.Write<string>(e.ToString(), null);
                return null;
            }
        }

        //private WebClient CreateWebClientObeject()
        //{
        //    WebClient webClient = new WebClient();
        //    webClient.Credentials = NetworkCredential;

        //    return webClient;
        //}

        //private byte[] ExtractBytesFromFtp(WebClient client, string requestUriString)
        //{
        //    try
        //    {
        //            byte[] bytes = client.DownloadData(requestUriString);

        //            return bytes;
        //    }
        //    catch (Exception e)
        //    {
        //        ServerHelperDotMgr.Log.Write<string>(e.ToString(), null);
        //        return null;
        //    }
        //}


        #endregion

        #endregion

    }
}