using DistributionUpdateLibrary.DataContracts;
using DistributionUpdateLibrary.Interfaces;
using ServerHelper.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using FileInfo = DistributionUpdateLibrary.DataContracts.FileInfo;

namespace UpdateAndUploadLibrary
{
    public class DistributionUpdate : IDistributionUpdate
    {
        #region Variable

        NetworkCredential networkCredential;

        #region Constant

        //상수 (const) or (static readonly)
        const string UserName = "leetoya";
        const string Password = "0000";

        #endregion

        #region Property

        #endregion

        #region Static

        #endregion

        #endregion

        #region Constructor

        public DistributionUpdate()
        {
            this.networkCredential = new NetworkCredential(UserName, Password);
        }

        #endregion

        #region Function

        #region Public

        // 1.FTP서버의 폴더 "C:\FTP"에서 FileInfo를 만드는 함수
        // 2.FilInfo를 스트링으로 변환해서 클라이언트로 보냄
        //value는 클라이언트부터 넘어온 "KD" or "HD"
        public string getFileInfo(string companyName)
        {
            FileInfo fileInfo = new FileInfo();
            companyName = companyName.CreateRecvData<string>();

            if (!string.IsNullOrWhiteSpace(companyName))
            {
                string targetVersion = ExtractVersionFromTxtfile(companyName);
                Dictionary<string, string> fileNameAndCrc = ExtractFileNamesAndCRCFromTxtfile(targetVersion);

                fileInfo.Version = targetVersion;
                fileInfo.Files = fileNameAndCrc;
            }

            return fileInfo.CreateSendData();
        }



        public string Update(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                FileRequest request = value.CreateRecvData<FileRequest>();
                List<string> fileListNeededForUpdate = new List<string>();

                fileListNeededForUpdate = request.Files;

                FileRespon fileResponse = new FileRespon();

                foreach (string fileName in fileListNeededForUpdate)
                {
                    byte[] bytes = ExtractBytes(fileName, request.Version);
                    fileResponse.Files.Add(fileName, bytes);
                }

                if (fileResponse != null)
                    return fileResponse.CreateSendData();
            }
            return string.Empty;
        }

        public void LogWrite(string value)
        {
            try {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Log log = value.CreateRecvData<Log>();
                    ServerHelper.Mgr.Log.Write(log.MB_ID, log.ExceptionMsg, log.data);

                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            //To do at a later time.

        }

        #endregion

        #region Private

        private Dictionary<string, string> ExtractFileNamesAndCRCFromTxtfile(string targetVersion)
        {
            WebClient client = new WebClient();
            string ftpDefaultPath = "ftp://192.168.53.26/";
            string fileName = targetVersion + ".txt";
            string requestUriString = ftpDefaultPath + fileName;
            client.Credentials = new NetworkCredential("leetoya", "0000");
            string contents = client.DownloadString(requestUriString);

            string[] contentsLines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            //현재 targetDll버젼하고 {버젼.txt}파일의 첫 번째 라인의 버젼값하고 같을때만 파일을 만든다.                                                      
            //***** 예외 처리 필요(Log 함수) *****

            Dictionary<string, string> fileNameAndCRCDictionary = new Dictionary<string, string>();

            if (contentsLines[0] == targetVersion)
            {
                for (int i = 1; i < contentsLines.Length; i++)
                {
                    string[] FileNameAndCrc = contentsLines[i].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    fileNameAndCRCDictionary.Add(FileNameAndCrc[0], FileNameAndCrc[1]);
                }
            }
            return fileNameAndCRCDictionary;
        }

        private string ExtractVersionFromTxtfile(string companyName)
        {
            WebClient client = new WebClient();
            string ftpDefaultPath = "ftp://192.168.53.26/";
            string fileName = companyName + "Version.txt";
            string requestUriString = ftpDefaultPath + fileName;
            client.Credentials = new NetworkCredential("leetoya", "0000");
            //로그 작성 필요 {}Version.txt 파일이 존재 하지 않습니다.//
            string version = client.DownloadString(requestUriString);

            return version;
        }

        private Dictionary<string, byte[]> ExtractFileNameAndByte(string targetVersion, List<string> fileNameList)
        {
            Dictionary<string, byte[]> fileNameAndByte = new Dictionary<string, byte[]>();

            WebClient client = new WebClient();
            client.Credentials = this.networkCredential;

            string ftpDefaultPath = string.Empty;
            string requestUriString = string.Empty;

            foreach (var fileName in fileNameList)
            {
                ftpDefaultPath = "ftp://192.168.53.26/";
                requestUriString = ftpDefaultPath + targetVersion + "/" + fileName;

                byte[] bytes = client.DownloadData(requestUriString);
                Console.WriteLine(bytes);
                fileNameAndByte.Add(fileName, bytes);
            }

            return fileNameAndByte;
        }


        private byte[] ExtractBytes(string fileName, string targetVersion)
        {
            WebClient client = new WebClient();
            client.Credentials = this.networkCredential;

            string ftpDefaultPath = string.Empty;
            string requestUriString = string.Empty;

            ftpDefaultPath = "ftp://192.168.53.26/";
            requestUriString = ftpDefaultPath + targetVersion + "/" + fileName;

            byte[] bytes = client.DownloadData(requestUriString);
            return bytes;
        }


        #endregion

        #endregion

    }
}
