

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class AssetCategory
    {

        public int AssetId { get; set; }

        public string AssetName { get; set; }

        public string Description { get; set; }

        public Nullable<bool> Status { get; set; }

    }
}
