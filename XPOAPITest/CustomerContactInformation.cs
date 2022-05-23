using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class CustomerContactInformation : Contact
    {
        public CustomerContactInformation()
        {
            phoneNumbers = new List<PhoneNumber>();
        }
        public IList<PhoneNumber> phoneNumbers;
        public IList<PhoneNumber> PhoneNumbers {
            get { return phoneNumbers; }
            set { phoneNumbers = value; }
        }
        public void addPhoneNumber(PhoneNumber phoneNumber)
            {
                if (phoneNumbers is null)
                    phoneNumbers = new List<PhoneNumber>();
                phoneNumbers.Add(phoneNumber);
            }
    }
}
