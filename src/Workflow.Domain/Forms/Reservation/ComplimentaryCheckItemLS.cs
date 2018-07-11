using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Reservation
{
    [DataContract]
    public class ComplimentaryCheckItemLS
    {
        [DataMember(Name = "mealExcludingAlcohol")]
        public bool MealExcludingAlcohol { get; set; }

        [DataMember(Name = "alcohol")]
        public bool Alcohol { get; set; }

        [DataMember(Name = "tobacco")]
        public bool Tobacco { get; set; }

        [DataMember(Name = "spa")]
        public bool Spa { get; set; }

        [DataMember(Name = "souvenirShop")]
        public bool SouvenirShop { get; set; }

        [DataMember(Name = "airportTransfers")]
        public bool AirportTransfers { get; set; }

        [DataMember(Name = "otherTransportwithinCity")]
        public bool OtherTransportwithinCity { get; set; }

        [DataMember(Name = "extraBeds")]
        public bool ExtraBeds { get; set; }
    }
}
