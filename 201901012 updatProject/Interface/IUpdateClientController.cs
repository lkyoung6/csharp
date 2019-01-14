using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UpdateUpload.Interface;
using WinSCP;

namespace UpdateUpload.Presenter
{
    public interface IUpdateClientController : IDisposable
    {
        #region Variable

        #region Constant

        #endregion

        #region Property

        #region Static

        #endregion

        #region Control Property

        #endregion
        
        #endregion

        #endregion

        #region Event Handler

        #endregion

        #region Constructor

        #endregion

        #region Function

        #region Static

        #endregion

        #region Public

        int MakeFolderForDll(string folderNameincludingDll);

        int UploadVersionTxtfileToFTP(string corparationName, string dllFileVersion);

        int UploadDllFilesToFTP(DirectoryInfo directoryInfo, string dllFileVersion);

        int UploadCRCTxtFileToFTP(string sourceFolderPath, string dllFileVersion);

        #endregion

        #region Private

        #endregion

        #endregion

        #region Interface

        #region IDisposable


        #endregion

        #region Interface IFormUpdateClient

        #region Property

        IFormUpdateClient FormUpdateClient { get; }

        #endregion

        #region Function

        #endregion

        #endregion

        #endregion


    }
}
