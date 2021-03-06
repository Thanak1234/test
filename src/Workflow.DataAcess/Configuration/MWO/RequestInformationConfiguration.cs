﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Configuration.MWO {
    public class RequestInformationConfiguration : EntityTypeConfiguration<RequestInformation> {

        public RequestInformationConfiguration() {
            ToTable("MWO_REQUEST_INFORMATION", "MAINTENANCE");
            HasKey(t => t.Id);

            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Location).HasColumnName("LOCATION");
            Property(t => t.Mode).HasColumnName("MODE");
            Property(t => t.CcdId).HasColumnName("CCD_ID");
            Property(t => t.ReferenceNumber).HasColumnName("REFERENCE_NUMBER");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.RequestType).HasColumnName("REQUEST_TYPE");
            Property(t => t.LocationType).HasColumnName("LOCATION_TYPE");
            Property(t => t.SubLocation).HasColumnName("SUB_LOCATION");
        }
    }
}
