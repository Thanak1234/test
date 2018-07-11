using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Reservation
{
    [DataContract]
    public class OccupancyDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "occupancy")]
        public double? Occupancy { get; set; }

        [DataMember(Name = "checkDate")]
        public DateTime? Check_Date
        {
            get
            {
                return _checkDate.Nullable();
            }
            set
            {
                _checkDate = value.Nullable();
            }
        }

        [IgnoreDataMember]
        public int Request_Header_Id { get; set; }

        private DateTime? _checkDate;
    }
}
