using System.Collections.Generic;
namespace XPOAPITest
{
    public class ContactInformation : Contact
    {
        public ContactInformation()
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
