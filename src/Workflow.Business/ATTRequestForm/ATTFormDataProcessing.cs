﻿/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ATTRequestForm
{
    public class ATTFormDataProcessing : AbstractFormDataProcessing, IATTFormDataProcessing
    {
        public bool IsSaveRequestData
        {
            get; set;
        }
    }
}