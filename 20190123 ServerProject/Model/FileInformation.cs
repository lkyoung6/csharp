﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateUpload.Model
{
    public class FileInformation
    {

        internal string FileName { get; set; }
        internal uint Crc32 { get; set; }
        internal byte[] bytes { get; set; }

    }
}
