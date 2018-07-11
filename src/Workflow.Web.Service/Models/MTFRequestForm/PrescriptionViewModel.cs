using System.Runtime.Serialization;
using Workflow.Domain.Entities.MTF;

namespace Workflow.Web.Models.MTFRequestForm
{
    [DataContract]
    public class PrescriptionViewModel : Prescription
    {
        [DataMember(Name = "medicine")]
        public string Medicine { get; set; }
    }
}
