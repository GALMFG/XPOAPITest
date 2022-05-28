using System.Collections.Generic;

namespace XPOAPITest
{
    public class Stop
    {
        public Stop()
        {
             addressInformations= new AddressInformation();

            stopContactInformations=new  List<StopContactInformation>();

            specialRequirement= new List<StopSpecialRequirement>();

            stopReferenceNumbers = new List<StopReferenceTypeCode> ();
        }
        public AddressInformation addressInformations { get; set; }

        public string type { get; set; }

        public string scheduledTimeFrom { get; set; }

        public string scheduledTimeTo { get; set; }

        public IList<StopContactInformation> stopContactInformations { get; set; }

        public IList<StopSpecialRequirement> specialRequirement { get; set; }

        public IList<StopReferenceTypeCode> stopReferenceNumbers { get; set; }

        public string note { get; set; }

        public int sequenceNo { get; set; }

        public void addContact(StopContactInformation contact)
        {
            if (stopContactInformations is null)
                stopContactInformations = new List<StopContactInformation>();
            stopContactInformations.Add(contact);
        }

        public void addSpecialRequirement(StopSpecialRequirement requirement)
        {
            if (specialRequirement is null)
                specialRequirement = new List<StopSpecialRequirement>();
            specialRequirement.Add(requirement);
        }

        public void addStopReferenceNumber(StopReferenceTypeCode stopReferenceTypeCode)
        {
            if (stopReferenceNumbers is null)
                stopReferenceNumbers = new List<StopReferenceTypeCode>();
            stopReferenceNumbers.Add(stopReferenceTypeCode);
        }
    }
}
