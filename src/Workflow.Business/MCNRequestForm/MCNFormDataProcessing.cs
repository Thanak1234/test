﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.MCNRequestForm
{
    public class MCNFormDataProcessing : AbstractFormDataProcessing,IMCNFormDataProcessing
    {
        public bool IsSaveRequestData
        {
            get; set;
        }
    }
}
