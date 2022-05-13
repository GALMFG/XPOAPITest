using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class OrderContactInformation : Contact
    {
        public IList<PhoneNumber> phoneNumbers { get; set; }
        public void addPhoneNumber(PhoneNumber phoneNumber)
        {
            if (phoneNumbers is null)
                phoneNumbers = new List<PhoneNumber>();
            phoneNumbers.Add(phoneNumber);
        }
    }
}
