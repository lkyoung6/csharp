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
using Moq;

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

        //[TestCase(@"C:\FTP\1.0.0.109/", @"ftp://localhost/1.0.0.109/", "BXLLIB.dll")]
        //[TestCase(@"C:\FTP\1.0.0.109/", @"ftp://localhost/1.0.0.109/", "Distribution.DLL")]
        //[TestCase(@"C:\FTP\1.0.0.109/", @"ftp://localhost/1.0.0.109/", "DistributionItems.DLL")]
        //[Test]
        //public void ExtractBytesFromFtpFile_ReturnsSameBytesExtractedFromLocal(string localPath, string ftpPath, string fileName)
        //{
        //    // Arrange
        //    //var client = CreateDefaultWeblClient();

        //    // Act
        //    byte[] expected = File.ReadAllBytes(localPath + fileName);
        //    byte[] actual = client.DownloadData(ftpPath + fileName);

        //    // Assert
        //    Assert.AreEqual(expected, actual);
        //}


        //[TestCase(@"ftp://localhost/", "HDVersion.txt", "1.0.0.108")]
        //[TestCase(@"ftp://localhost/", "KDVersion.txt","1.0.0.109")]
        [Test]
        public void ExtractVersionFromFileOnFtp_ReturnsThirdParameter()
        {
            // Arrage
            Dictionary<string, string> expected = new Dictionary<string, string>();

            expected.Add("BXLLIB.dll", "3509475351");
            expected.Add("Distribution.DLL", "2654671972");
            expected.Add("DistributionItems.DLL", "1475712246");
            expected.Add("DistributionLibrary.DLL", "4092703406");
            expected.Add("DistributionUpdate.DLL ", "");
            expected.Add("DistributionUpdateLibrary.DLL ", "");
            expected.Add("DotNetDBF.DLL ", "");
            expected.Add("ExcelLibrary.DLL ", "");
            expected.Add("kd 256.ico", "2438001374");
            expected.Add("KDDistribution.exe ", "3555648412");
            expected.Add("KDDistribution.exe.config ", "3251518489");
            expected.Add("KDDistributionUpdate.exe ", "4062766577");
            expected.Add("KDDistributionUpdate.exe.config ", "2088815806");
            expected.Add("Microsoft.Office.Interop.Excel.DLL ", "3034234403");
            expected.Add("Microsoft.ReportViewer.Common.dll ", "2311878800");
            expected.Add("Microsoft.ReportViewer.DataVisualization.dll ", "2404050035");
            expected.Add("Microsoft.ReportViewer.ProcessingObjectModel.dll ", "791590992");
            expected.Add("Microsoft.ReportViewer.WinForms.dll ", "152033660");
            expected.Add("Microsoft.Vbe.Interop.DLL ", "2854101119");
            expected.Add("MTC.DLL ", "1673003747");
            expected.Add("Nosun.DLL ", "4200554813");
            expected.Add("office.DLL ", "2879764208");
            expected.Add("Serialization.DLL ", "1917984648");
            expected.Add("stdole.dll ", "2984433064");
            expected.Add("System.Data.SQLite.dll ", "3155191852");
            expected.Add("TSCLIBx86.dll ", "3861450121");

            DistributionUpdate distributionUpdate = new DistributionUpdate(new FakeWebClient());

            //string targetVersion = "1.0.0.109";
            //string reuqestUri = "ftp://localhost/";
            Dictionary<string, string> actual = new Dictionary<string, string>();
            actual = distributionUpdate.ExtractFileNamesAndCRCFromTxtfile("1.0.0.109", "ftp://localhost/"+ "1.0.0.109.txt");

            //Mock<WebClient> client = new Mock<WebClient>();
            
            //Mock<IFakeWebClient> fakeClinet = new Mock<IFakeWebClient>();

            

            //var client = CreateDefaultWeblClient();

            // Act
            //byte[] bytes = fakeWebClient.DownloadData();
            //string actual = Encoding.UTF8.GetString(bytes);

            // Assert
            Assert.AreEqual(expected, actual);
        }


        //[TestCase(@"ftp://localhost/", "1.0.0.109.txt", 1, "BXLLIB.dll||3509475351")]
        //[TestCase(@"ftp://localhost/", "1.0.0.109.txt", 26, "TSCLIBx86.dll||3861450121")]
        //[Test]
        //public void ExtractFileNameAndCrc_ReturnsThirdParameter(string ftpPath, string fileName, int index)
        //{
        //    // Arrage
        //    //Dictionary<string, string> expeczted = new Dictionary<string, string>();
        //    Dictionary<string, byte[]> expected = new Dictionary<string, byte[]>();

        //    Mock<WebClient> chk = new Mock<WebClient>();
        //    chk.Setup(x=>x.ConnectClient()).re


        //    string testString = "testString";
        //    List<string> fileNameList = new List<string>();
        //    DistributionUpdate obje = new DistributionUpdate();



        //    Assert.AreEqual(obje.ExtractFileNameAndByte(testString, fileNameList), expected);





            

            

        //    //var client = new FakeWebClient();
        //    //byte[] bytes = client.DownloadData();
        //    //string contents = Encoding.UTF8.GetString(bytes);
        //    //string[] contentsLines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        //    Dictionary<string, string> actual = new Dictionary<string, string>();

        //    for (int i = 1; i < contentsLines.Length; i++)
        //    {
        //        string[] FileNameAndCrc = contentsLines[i].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

        //        if (FileNameAndCrc.Length == 2)
        //        {
        //            actual.Add(FileNameAndCrc[0], FileNameAndCrc[1]);
        //        }
        //    }

        //    Assert.Equals(expected["BXLLIB.dll"], actual["BXLLIB.dll"]);
        //    Assert.Equals(expected["Distribution.DLL"], actual["Distribution.DLL"]);




        //}

        //Helper Method
        //private FakeWebClient CreateDefaultWeblClient()
        //{
        //    FakeWebClient webClient = new FakeWebClient();
        //    return webClient;
        //}



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

