using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class StopContactInformation : Contact
    {
        public IList<StopPhoneNumber>  phoneNumbers { get; set; }
        public void addPhoneNumber(StopPhoneNumber phoneNumber)
        {
            if (phoneNumbers is null)
                phoneNumbers = new List<StopPhoneNumber>();
            phoneNumbers.Add(phoneNumber);
        }
    }

}
