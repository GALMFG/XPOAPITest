using System.Collections.Generic;

namespace XPOAPITest
{
    public class StopContactInformation : Contact
    {

        public StopContactInformation():base()
        {
            phoneNumbers = new  List<StopContactPhoneNumber> ();
    }
        public IList<StopContactPhoneNumber> phoneNumbers { get; set; }
        public void addPhoneNumber(StopContactPhoneNumber phoneNumber)
        {
            if (phoneNumbers is null)
                phoneNumbers = new List<StopContactPhoneNumber>();
            phoneNumbers.Add(phoneNumber);
        }
    }

}
