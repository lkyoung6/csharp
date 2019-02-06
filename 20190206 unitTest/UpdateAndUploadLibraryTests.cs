using NUnit.Framework;
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
using UpdateAndUploadLibrary;
using System.IO;
using ServerHelper.Model;

namespace UpdateAndUploadLibrary.Test
{
    [TestFixture]
    public class UpdateAndUploadLibraryTests
    {
        #region Variable

        #region Constant

        #endregion

        #region Property

        #endregion

        #region Static


        //WebClient Client;


        #endregion

        #endregion

        #region Function

        #region Public

        #endregion

        #region Private


        //[TestCase("leetoya", "0000", @"ftp://localhost/")]
        //[SetUp]
        //public void Initialize()
        //{
        //    string id = "leetoya";
        //    string password = "0000";
        //    string uri = @"ftp://localhost/";
        //    this.Client = new WebClient();
        //    this.Client.Credentials = new NetworkCredential(id, password);
        //    this.Client.BaseAddress = uri;
        //}

        //[TearDown]
        //public void EndTest()
        //{
        //    Client.Dispose();
        //}

        [Test]
        public void CreateRecvData()
        {
            // Arranage

            // Act
            //FileInfo fileInfo = new FileInfo();
            //string companyName = encryptedCompanyName.CreateRecvData<string>();

            // Assert

        }

        //[TestCase ("HD")]
        //[TestCase ("HD")]
        //[Test]
        //private void ExtractVersionFromTxtfile(string companyName)
        //{
        //    string version = string.Empty;

        //    string ftpUri = this.Client.BaseAddress;

        //    string fileName = string.Format("{0}{1}", companyName, "Version.txt");
        //    string requestUriString = string.Format("{0}{1}", ftpUri, fileName);

        //    //byte[] bytes = this.ExtractBytesFromFtp(requestUriString);
        //    //version = Encoding.UTF8.GetString(bytes);

        //}


        //[Test]
        //public void GetConnStringFromAppConfig()
        //{
        //    //DataAccess da = new DataAccess();
        //    //string actualString = da.ConnectionString;
        //    string expectedString = ConfigurationManager.ConnectionStrings["ConnectionString"];
        //    //string requestUri = ConfigurationManager.AppSettings["FtpUri"];
        //    Assert.AreEqual(expectedString, actualString);
        //}

        [TestCase("server", "에러 메시지", null)]
        [Test]
        public void LogWrite(string id, string exceptionMsg, string data)
        {
            // Arrange
            var HDTest = new DBConnectionInfo("EXPRESS", "Kdlab001", "183.111.71.231", "1521", "KDHDEXP", true);
            Database db = new Database(HDTest);

            // Act
            ServerHelperDotMgr.Log.Write(id, exceptionMsg, data, db);

            // Assert



        }

        [TestCase(@"C:\FTP\1.0.0.109/", @"ftp://localhost/1.0.0.109/", "BXLLIB.dll")]
        [TestCase(@"C:\FTP\1.0.0.109/", @"ftp://localhost/1.0.0.109/", "Distribution.DLL")]
        [TestCase(@"C:\FTP\1.0.0.109/", @"ftp://localhost/1.0.0.109/", "DistributionItems.DLL")]
        [Test]
        public void ExtractBytesFromFtpFile_ReturnsSameBytesExtractedFromLocal(string localPath, string ftpPath, string fileName)
        {
            // Arrange
            var client = CreateDefaultWeblClient();

            // Act
            byte[] expected = File.ReadAllBytes(localPath + fileName);
            byte[] actual = client.DownloadData(ftpPath + fileName);

            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestCase(@"ftp://localhost/", "HDVersion.txt", "1.0.0.108")]
        [TestCase(@"ftp://localhost/", "KDVersion.txt","1.0.0.109")]
        [Test]
        public void ExtractVersionFromFileOnFtp_ReturnsThirdParameter(string ftpPath, string fileName,string expected)
        {
            // Arrage
            var client = CreateDefaultWeblClient();

            // Act
            byte[] bytes = client.DownloadData(ftpPath + fileName);
            string actual = Encoding.UTF8.GetString(bytes);

            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestCase(@"ftp://localhost/", "1.0.0.109.txt", 1, "BXLLIB.dll||3509475351")]
        [TestCase(@"ftp://localhost/", "1.0.0.109.txt", 26, "TSCLIBx86.dll||3861450121")]
        [Test]
        public void ExtractFileNameAndCrc_ReturnsThirdParameter(string ftpPath, string fileName, int index, string expected)
        {
            // Arrage
            var client = CreateDefaultWeblClient();
            var fileNameAndByte = new Dictionary<string, byte[]>();

            // Act
            byte[] bytes = client.DownloadData(ftpPath + fileName);
            string contents = Encoding.UTF8.GetString(bytes);
            string[] contentsLines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string actual = contentsLines[index];

            // Assert
            Assert.AreEqual(expected, actual);
        }

        //Helper Method
        private WebClient CreateDefaultWeblClient()
        {
            string id = "leetoya";
            string password = "0000";
            var client = new WebClient();
            client.Credentials = new NetworkCredential(id, password);

            return client;
        }



        //[Test]
        //public string getFileInfo(string encryptedCompanyName)
        //{

        //        string targetVersion = this.ExtractVersionFromTxtfile(companyName);
        //        Dictionary<string, string> fileNameAndCrc = this.ExtractFileNamesAndCRCFromTxtfile(targetVersion);

        //}

        //[Test]
        //public string Update(string encryptedFileRequest)
        //{
        //        List<string> fileListNeededForUpdate = new List<string>();

        //        fileListNeededForUpdate = fileRequest.Files;

        //        FileRespon fileResponse = new FileRespon();

        //        foreach (string fileName in fileListNeededForUpdate)
        //        {
        //            string ftpUri = this.Client.BaseAddress;
        //            string requestUriString = string.Format("{ 0}{1}{2}{3}", ftpUri, fileRequest.Version, "/", fileName);

        //            byte[] bytes = this.ExtractBytesFromFtp(requestUriString);

        //            if (bytes != null)
        //            {
        //                fileResponse.Files.Add(fileName, bytes);
        //            }
        //        }

        //        return fileResponse.CreateSendData();
        //    }

        //    return string.Empty;
        //}

        //[Test]
        //public void LogWrite(string encrypedLog)
        //{
        //    if (!string.IsNullOrWhiteSpace(encrypedLog))
        //    {
        //        Log log = encrypedLog.CreateRecvData<Log>();
        //        ServerHelperDotMgr.Log.Write(log.MB_ID, log.ExceptionMsg, log.data);
        //    }
        //}

        //[Test]
        //private Dictionary<string, string> ExtractFileNamesAndCRCFromTxtfile(string targetVersion)
        //{
        //    string ftpUri = this.Client.BaseAddress;
        //    string fileName = string.Format("{0}{1}", targetVersion, ".txt");
        //    string requestUriString = string.Format("{0}{1}", ftpUri, fileName);

        //    byte[] bytes = this.ExtractBytesFromFtp(requestUriString);
        //    string contents = Encoding.UTF8.GetString(bytes);
        //    string[] contentsLines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        //    Dictionary<string, string> fileNameAndCRCDictionary = new Dictionary<string, string>();

        //    if (contentsLines[0] == targetVersion)
        //    {
        //        for (int i = 1; i < contentsLines.Length; i++)
        //        {
        //            string[] FileNameAndCrc = contentsLines[i].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
        //            if (FileNameAndCrc.Length == 2)
        //            {
        //                fileNameAndCRCDictionary.Add(FileNameAndCrc[0], FileNameAndCrc[1]);
        //            }
        //        }
        //    }
        //    return fileNameAndCRCDictionary;
        //}




        //[Test]
        //private Dictionary<string, byte[]> ExtractFileNameAndByte(string targetVersion, List<string> fileNameList)
        //{
        //    Dictionary<string, byte[]> fileNameAndByte = new Dictionary<string, byte[]>();

        //    string ftpUri = this.Client.BaseAddress;

        //    foreach (var fileName in fileNameList)
        //    {
        //        string requestUriString = string.Format("{0}{1}{2}{3}", ftpUri, targetVersion, "/", fileName);
        //        byte[] bytes = this.ExtractBytesFromFtp(requestUriString);

        //        fileNameAndByte.Add(fileName, bytes);
        //    }
        //    return fileNameAndByte;
        //}


        #endregion

        #endregion
    }
}

