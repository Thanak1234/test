﻿using System;
using System.Collections.Generic;
using Workflow.Domain.Entities.Finance;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IFATRequestFormService : IRequestFormService<FATRequestWorkflowInstance>
    {
        IEnumerable<AssetTransferDetail> GetAssetTransferDetails(int requestHeaderId);
    }
}
