using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using UpdateUpload.Interface;
using UpdateUpload.Presenter;
using UpdateUpload.Model;

namespace UpdateUpload
{
    public partial class FormUpdateClient : Form, IFormUpdateClient
    {
        #region Variable

        #region Constant

        //상수 (const) or (static readonly)

        private readonly string HapdongExpressString = "HAPDONG EXPRESS";
        private readonly string KyoungdongExpressString = "KYOUNGDONG EXPRESS";
        private readonly string DistributionDllFileName = "Distribution.DLL";
        private const string DoubleBackslash = "\\";

        #endregion

        #region Property


        #region Static

        #endregion

        #region Control Property

        #endregion


        private string SourceFolderPath { get; set; }
        private string CorparationName { get; set; }

        #endregion



        #endregion

        #region Event Handler


        private void rdo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = sender as RadioButton;

            string programFilesFolderPath = string.Empty;
            if (Environment.Is64BitOperatingSystem)
            {
                programFilesFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            }
            else
            {
                programFilesFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }


            if (rdo != null)
            {
                string programFullpath = string.Empty;
                if (rdo.Text == "KD")
                {
                    programFullpath = string.Format("{0}{1}{2}", programFilesFolderPath, DoubleBackslash, this.KyoungdongExpressString);
                    txtPath.Text = programFullpath;
                    this.SourceFolderPath = programFullpath;
                    this.CorparationName = "KD";
                }
                else
                {
                    programFullpath = string.Format("{0}{1}{2}", programFilesFolderPath, DoubleBackslash, this.HapdongExpressString);
                    txtPath.Text = programFullpath;
                    this.SourceFolderPath = programFullpath;
                    this.CorparationName = "HD";
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (this.CorparationName != null)
            {
                FileVersionInfo DistributionDLLFileInfo = FileVersionInfo.GetVersionInfo(string.Format("{0}{1}{2}", this.SourceFolderPath, DoubleBackslash, this.DistributionDllFileName));
                string dllFileVersion = DistributionDLLFileInfo.FileVersion;
                DirectoryInfo directoryInfo = new DirectoryInfo(SourceFolderPath);
                List<FileInformation> fileInfomationList = new List<FileInformation>();
                string[] fullPathfileNames = Directory.GetFiles(SourceFolderPath, "*", SearchOption.TopDirectoryOnly);

                fileInfomationList = Presenter.GetFileInformationList(fullPathfileNames);

                int isMakeFolder = Presenter.MakeFolderForDll(dllFileVersion);

                if (isMakeFolder == -1)
                {
                    MessageBox.Show("폴더 생성 에러: {version} 폴더 생성에 실패했습니다.");
                    return;
                }

                int isUploadDllFiles = Presenter.UploadDllFilesToFTP(fileInfomationList, dllFileVersion);

                if (isUploadDllFiles == -1)
                {
                    MessageBox.Show("파일 생성 에러: Dll 파일을 업로드 하는데 실패했습니다.");
                    return;
                }

                int isUploadVersionTxtFile = 0;
                if (chkIsCreateVerionTxtFile.Checked == true)
                {
                    isUploadVersionTxtFile = Presenter.UploadVersionTxtfileToFTP(this.CorparationName, dllFileVersion);
                }

                if (isUploadVersionTxtFile == -1)
                {
                    MessageBox.Show("파일 생성 에러: {}Version.txt 파일 생성에 실패 했습니다.");
                    return;
                }

                int isUploadCrcTxtFile = Presenter.UploadCRCTxtFileToFTP(fileInfomationList, dllFileVersion);

                if (isUploadCrcTxtFile == -1)
                {
                    MessageBox.Show("파일 생성 에러: crc 파일 생성에 실패 했습니다.");
                    return;
                }
                MessageBox.Show("정상적으로 업로드 완료 되었습니다.");
            }
            else
            {
                MessageBox.Show("회사를 선택해 주세요.");
            }
        }




        #endregion

        #region Constructor

        //생성자

        public FormUpdateClient()
        {
            InitializeComponent();
            this.Presenter = new UpdateClientController(this);
        }

        #endregion

        #region Function

        #region Static

        #endregion

        #region Public

        #endregion

        #region Private


        #endregion

        #endregion

        #region Interface

        #region IDisposable

        #endregion

        #region Interface IFormUpdateClient

        #region Property

        public IUpdateClientController Presenter { get; set; }

        #endregion

        #region Function

        #endregion

        #endregion

        #endregion


    }
}
