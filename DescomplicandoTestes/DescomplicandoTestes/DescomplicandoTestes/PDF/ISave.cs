﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DescomplicandoTestes.PDF
{
    public interface ISave
    {
        Task SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}
