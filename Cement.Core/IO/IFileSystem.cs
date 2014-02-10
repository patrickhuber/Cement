﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cement.IO
{
    public interface IFileSystem
    {
        string GetFirstFile(string path, string pattern, bool recursive);
        Stream OpenRead(string path);
    }
}