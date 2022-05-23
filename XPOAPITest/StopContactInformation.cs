﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class StopContactInformation : Contact
    {

        public StopContactInformation():base()
        {
            phoneNumbers = new  List<StopContactPhoneNumber> ();
    }
        public IList<StopContactPhoneNumber> phoneNumbers;
        public IList<StopContactPhoneNumber>  PhoneNumbers {
            get { return phoneNumbers; }
            set { phoneNumbers = value; }
        }
        public void addPhoneNumber(StopContactPhoneNumber phoneNumber)
        {
            if (phoneNumbers is null)
                phoneNumbers = new List<StopContactPhoneNumber>();
            phoneNumbers.Add(phoneNumber);
        }
    }

}
