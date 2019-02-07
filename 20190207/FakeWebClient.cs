using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UpdateAndUploadLibrary
{
    public class FakeWebClient : WebClient
    {
        public new byte[] DownloadData(string address)
        {
            string fileConstentOnServer=  @"1.0.0.109
                                           BXLLIB.dll || 3509475351
                                           Distribution.DLL || 2654671972
                                           DistributionItems.DLL || 1475712246
                                           DistributionLibrary.DLL || 4092703406
                                           DistributionUpdate.DLL || 2468602010
                                           DistributionUpdateLibrary.DLL || 610321594
                                           DotNetDBF.DLL || 2044110516
                                           ExcelLibrary.DLL || 1041977618
                                           kd 256.ico || 2438001374
                                           KDDistribution.exe || 3555648412
                                           KDDistribution.exe.config || 3251518489
                                           KDDistributionUpdate.exe || 4062766577
                                           KDDistributionUpdate.exe.config || 2088815806
                                           Microsoft.Office.Interop.Excel.DLL || 3034234403
                                           Microsoft.ReportViewer.Common.dll || 2311878800
                                           Microsoft.ReportViewer.DataVisualization.dll || 2404050035
                                           Microsoft.ReportViewer.ProcessingObjectModel.dll || 791590992
                                           Microsoft.ReportViewer.WinForms.dll || 152033660
                                           Microsoft.Vbe.Interop.DLL || 2854101119
                                           MTC.DLL || 1673003747
                                           Nosun.DLL || 4200554813
                                           office.DLL || 2879764208
                                           Serialization.DLL || 1917984648
                                           stdole.dll || 2984433064
                                           System.Data.SQLite.dll || 3155191852
                                           TSCLIBx86.dll || 3861450121";
            fileConstentOnServer = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(fileConstentOnServer);

            return bytes;
        }
    }
}
