using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateAndUploadLibrary
{
    public interface IFakeWebClient
    {
        byte[] DownloadData(string address);
    }
}
