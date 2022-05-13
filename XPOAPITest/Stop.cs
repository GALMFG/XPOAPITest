using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class Stop
    {
        public AddressInformation addressInformations { get; set; }

        public string type { get; set; }

        public string scheduledTimeFrom { get; set; }

        public string scheduledTimeTo { get; set; }

        public IList<StopContactInformation> stopContactInformations { get; set; }

        public IList<SpecialRequirement> specialRequirements { get; set; }

        public IList<StopReferenceNumber> stopReferenceNumbers { get; set; }

        public string note { get; set; }

        public int sequenceNo { get; set; }

        public void addContact(StopContactInformation contact)
        {
            if (stopContactInformations is null)
                stopContactInformations = new List<StopContactInformation>();
            stopContactInformations.Add(contact);
        }

        public void addSpecialRequirement(SpecialRequirement specialRequirement)
        {
            if (specialRequirements is null)
                specialRequirements = new List<SpecialRequirement>();
            specialRequirements.Add(specialRequirement);
        }

        public void addStopReferenceNumber(StopReferenceNumber stopReferenceNumber)
        {
            if (stopReferenceNumbers is null)
                stopReferenceNumbers = new List<StopReferenceNumber>();
            stopReferenceNumbers.Add(stopReferenceNumber);
        }
    }
}
