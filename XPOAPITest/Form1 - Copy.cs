using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XPOAPITest
{
    public partial class Form1 : Form
    {
        QuoteResponse quoteResponse;
        QuoteRequest quoteRequest;

        OrderResponse orderResponse;
        OrderRequest orderRequest;

        FixedLists fixedLists;

        public Form1()
        {
            InitializeComponent();
        }

        public void ApplyConfigSettings()
        {
            quoteRequest.PartnerIdentifierCode=XPOSettings.PartnerIdentifierCode;
        }
        public void IntializeDropdownLists()
        {
            fixedLists = new FixedLists();
            fixedLists.Initialize();
            fillEquipmentCatagoryCodeDropDownList();
            fillEquipmentTypesDropDownList();
            fillTypeOfStopsDropDownList();
            fillPaymentMethodDropDownList();
            fillApplicationSourceDropDownList();
            fillWeightUomCodeDropDownList();
            fillLengthUomCodeDropDownList();
            fillWidthUomCodeDropDownList();
            fillHeightUomCodeDropDownList();
            fillPhoneNumberTypeDropDownList();
            fillUnitTypeCodeDropDownList();
            fillPackageTypeCodeDropDownList();
            fillStopSpecialRequirementsDropDownList();
            fillCustomerReferenceTypeCodeDropDownList();
            fillStopReferencTypeCodeDropDownList();
            ApplyConfigSettings();
        }
        public void fillStopSpecialRequirementsDropDownList()
        {
            foreach (String item in fixedLists.stopSpecialRequirements)
                comboBoxSpecialRequirementCode.Items.Add(item);
        }
        public void fillCustomerReferenceTypeCodeDropDownList()
        {
            foreach (String item in fixedLists.customerRefereneTypeCodes)
                comboBoxContactReferenceNumberTypeCode.Items.Add(item);
        }
        public void fillStopReferencTypeCodeDropDownList()
        {
            foreach (String item in fixedLists.customerRefereneTypeCodes)
                comboBoxStopReefernceTypeCode.Items.Add(item);
        }
        public void fillEquipmentCatagoryCodeDropDownList()
        {
            foreach (String item in fixedLists.equipmentCatagoryCodes)
                comboBoxEquipmentCategoryCode.Items.Add(item);
        }
        public void fillEquipmentTypesDropDownList()
        {
            foreach (String item in fixedLists.equipmentTypeCodes)
                comboBoxEquipmentTypeCode.Items.Add(item);
        }
        public void fillTypeOfStopsDropDownList()
        {
            foreach (String item in fixedLists.typeOfStops)
                comboBoxStopType.Items.Add(item);
        }
        public void fillPaymentMethodDropDownList()
        {
            foreach (String item in fixedLists.equipmentTypeCodes)
                comboBoxEquipmentTypeCode.Items.Add(item);
        }
        public void fillApplicationSourceDropDownList()
        {

            foreach (String item in fixedLists.applicationSources)
                comboBoxApplicationSource.Items.Add(item);
            
        }
        public void fillWeightUomCodeDropDownList()
        {
            foreach (String item in fixedLists.weightUomCodes)
                comboBoxItemWeightUOMCode.Items.Add(item);
        }
        public void fillLengthUomCodeDropDownList()
        {
            foreach (String item in fixedLists.lengthUomCodes)
                comboBoxItemLengthUOMCode.Items.Add(item);
            
        }
        public void fillWidthUomCodeDropDownList()
        {
            foreach (String item in fixedLists.widthUomCodes)
                comboBoxItemWidthUOMCode.Items.Add(item);

        }
        public void fillHeightUomCodeDropDownList()
        {
            foreach (String item in fixedLists.heightUomCodes)
                comboBoxItemHeightUOMCode.Items.Add(item);

        }
        public void fillPhoneNumberTypeDropDownList()
        {
            foreach (String item in fixedLists.phoneNumberTypes)
            {
                comboBoxCustomerContactPhoneNumberType.Items.Add(item);
                comboBoxStopContactPhoneNumberType.Items.Add(item);
            }
        }
        public void fillUnitTypeCodeDropDownList()
        {

            foreach (String item in fixedLists.unitTypeCodes)
                comboBoxItemUnitTypeCode.Items.Add(item);
            
        }
        public void fillPackageTypeCodeDropDownList()
        {
            foreach (String item in fixedLists.packageTypeCodes)
                comboBoxProductTypeCode.Items.Add(item);
        }
        private async void buttonGetQuote_Click(object sender, EventArgs e)
        {
            if (!ValidateQuoteRequestObject())
            {
                return;
            }
            XPO xpo = new XPO();
            //xpo.createToken();
            quoteResponse = await xpo.getQuote(quoteRequest);
            //quoteResponse = await xpo.getQuote();




            IList<PriceResponse> priceSearchResponse = quoteResponse.priceSearchResponse;
            if (priceSearchResponse != null)
            {
                tabControlMain.SelectedIndex = 2;
                foreach (PriceResponse priceResponse in priceSearchResponse)
                {
                    Quote lowestpriceQuote = priceResponse.lowestPriceQuote;

                    TabPage lowestPriceTabPage = lowestpriceQuote.addTab("Lowest Price Quote");
                    tabQuotes.TabPages.Add(lowestPriceTabPage);
                    Quote lowestGuranteedpriceQuote = priceResponse.lowestGuaranteedQuotePrice;
                    
                    TabPage lowestGuranteedPriceTabPage= lowestpriceQuote.addTab("Lowest Guaranteed Price Quote");
                    tabQuotes.TabPages.Add(lowestGuranteedPriceTabPage);
                    IList < Quote > quotes= priceResponse.quoteDetails;
                    foreach(Quote quote in quotes)
                    {                        
                        TabPage tabPageQuote=quote.addTab(quote.carrierName);
                        tabQuotes.TabPages.Add(tabPageQuote);
                    }

                }
            }

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            quoteRequest = new QuoteRequest();
            IntializeDropdownLists();
            getConfigData();
        }

        void getConfigData()
        {
            XPOSettings.XAPIKeyToken = ConfigurationManager.AppSettings.Get("x-api-key_token");
            XPOSettings.XAPIKeyRequest = ConfigurationManager.AppSettings.Get("x-api-key_request");
            XPOSettings.ClientId = ConfigurationManager.AppSettings.Get("client_id");
            XPOSettings.ClientSecret = ConfigurationManager.AppSettings.Get("client_secret");
            XPOSettings.Scope = ConfigurationManager.AppSettings.Get("scope");
            XPOSettings.GrantType = ConfigurationManager.AppSettings.Get("grant_type");
            XPOSettings.PartnerIdentifierCode = ConfigurationManager.AppSettings.Get("PartnerIdentifierCode");
            XPOSettings.XPOConnectURL = ConfigurationManager.AppSettings.Get("xpoConnectUrl");
            XPOSettings.TransportationMode = ConfigurationManager.AppSettings.Get("transportationMode");
        }
        private void InitializeTimePicker()
        {
            dateTimePickerScheduledTimeFrom = new DateTimePicker();
            dateTimePickerScheduledTimeFrom.Format = DateTimePickerFormat.Time;
            dateTimePickerScheduledTimeFrom.ShowUpDown = true;
            dateTimePickerScheduledTimeFrom.Location = new Point(10, 10);
            dateTimePickerScheduledTimeFrom.Width = 100;
            Controls.Add(dateTimePickerScheduledTimeFrom);

            dateTimePickerScheduledTimeTo = new DateTimePicker();
            dateTimePickerScheduledTimeTo.Format = DateTimePickerFormat.Time;
            dateTimePickerScheduledTimeTo.ShowUpDown = true;
            dateTimePickerScheduledTimeTo.Location = new Point(10, 10);
            dateTimePickerScheduledTimeTo.Width = 100;
            Controls.Add(dateTimePickerScheduledTimeTo);
        }
        private bool ValidateQuoteRequestObject()
        {

            quoteRequest.PartnerIdentifierCode=XPOSettings.PartnerIdentifierCode;
            quoteRequest.TransportationMode.Add( XPOSettings.TransportationMode);
            if (textBoxPartnerOrderCode.Text != "")
                 quoteRequest.PartnerOrderCode=textBoxPartnerOrderCode.Text ;

            if (comboBoxEquipmentCategoryCode.Text != "")
                 quoteRequest.EquipmentCategoryCode = comboBoxEquipmentCategoryCode.Text;

            if (comboBoxEquipmentTypeCode.Text != "")
                 quoteRequest.EquipmentTypeCode=comboBoxEquipmentTypeCode.Text ;


            if (textBoxBOLNumber.Text != "")
                 quoteRequest.BolNumber= textBoxBOLNumber.Text ;

            if (textBoxShipmentId.Text != "")
                 quoteRequest.ShipmentId=textBoxShipmentId.Text ;

            if (comboBoxApplicationSource.Text != "")
                 quoteRequest.ApplicationSource=comboBoxApplicationSource.Text ;


            IList<Stop> stops = quoteRequest.stops;
            if (stops.Count == 0)
            {
                MessageBox.Show("Please provide stops infomation");
                return false;
            }
            else
            {
                
                // Stop stop = null;

                //stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.cityName == textBoxState.Text && s.addressInformations.cityName == textBoxCountry.Text && s.addressInformations.cityName == textBoxZipCode.Text);
                Stop stop = stops.Where(s => s.Type == "PICKUP").FirstOrDefault();
                if (stop is null)
                {
                    MessageBox.Show("Please provide stops infomation for PICKUP");
                    return false;
                }
                 stop = stops.Where(s => s.Type == "DELIVERY").FirstOrDefault();
                if (stop is null)
                {
                    MessageBox.Show("Please provide stops infomation for DELIVERY");
                    return false;
                }
            }
            IList<QuoteItem> items = quoteRequest.Items;
            if (items.Count == 0)
            {
                MessageBox.Show("Please provide at least one item");
                return false;
            }
            IList<CustomerContactInformation> contacts = quoteRequest.ContactInformations;
            if (contacts.Count == 0)
            {
                MessageBox.Show("Please provide at least one Contact");
                return false;
            }
            return true;
        }
        private Stop addStop(QuoteRequest quoteRequest)
        {

            IList<Stop> stops = quoteRequest.stops;
           // Stop stop = null;

            //stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.cityName == textBoxState.Text && s.addressInformations.cityName == textBoxCountry.Text && s.addressInformations.cityName == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();
            if (stop is null)
            {
                stop = new Stop();
                quoteRequest.AddStop(stop);
            }
                stop.type = comboBoxStopType.Text;
                //   MessageBox.Show(dateTimePickerScheduledTimeFrom.Value.ToString());
                stop.scheduledTimeTo = dateTimePickerScheduledTimeTo.Value.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK");
                stop.scheduledTimeFrom = dateTimePickerScheduledTimeFrom.Value.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK");
                stop.sequenceNo = Convert.ToInt32(numericUpDownSequenceNo.Value);

                stop.note = richTextBoxNote.Text;


            AddressInformation addressInformations = stop.addressInformations;
            addStopAddressInformation(stop);
            comboBoxStops.Items.Add(textBoxCity.Text+","+ textBoxState.Text+","+ textBoxCountry.Text+","+ textBoxZipCode.Text);

            return stop;
        }

        private QuoteItem addItem(QuoteRequest quoteRequest)
        {
            if (quoteRequest is null) quoteRequest = new QuoteRequest();
            IList<QuoteItem> quoteItems= quoteRequest.items;

            QuoteItem item = quoteItems.Where(s => s.itemNumber == textBoxItemNumber.Text && s.itemDescription == textBoxItemDescription.Text ).FirstOrDefault();

            if (item is null)
            {
                item = new QuoteItem();
                quoteItems.Add(item);
            }
            item.productCode = textBoxItemProductCode.Text;            
            item.ItemDescription = textBoxItemDescription.Text;
            item.itemNumber = textBoxItemNumber.Text;
            item.Units = Convert.ToInt32(textBoxItemUnits.Text);
            item.UnitTypeCode = comboBoxItemUnitTypeCode.Text;
            item.PackageUnits = Convert.ToInt32(textBoxPackageUnits.Text);
            item.PackageTypeCode = comboBoxProductTypeCode.Text;
            item.weight = Convert.ToInt32(textBoxItemWeight.Text);
            item.WeightUomCode = comboBoxItemWeightUOMCode.Text;
            
            item.Height = Convert.ToInt32(textBoxItemHeight.Text);
            item.HeightUomCode = comboBoxItemHeightUOMCode.Text;
            
            item.Length = Convert.ToInt32(textBoxItemLength.Text);
            item.LengthUomCode = comboBoxItemLengthUOMCode.Text;
            item.Width = Convert.ToInt32(textBoxItemWidth.Text);
            item.WidthUomCode = comboBoxItemWidthUOMCode.Text;

            if (radioButtonHazardousMaterialYes.Checked)
            {
                item.isHazmat = true;
                HazardousItemInfo hazardousItemInfo = item.HazardousItemInfo;
                hazardousItemInfo.unNumber = Convert.ToInt32(textBoxHazMatUNNumber.Text);
                hazardousItemInfo.packingGroup = Convert.ToInt32(textBoxHazMatPackingGroup.Text);
                hazardousItemInfo.receptacleSize = Convert.ToInt32(textBoxHazMatReceptacleSize.Text);
                hazardousItemInfo.hazardousClass = textBoxHazMatHazardousClass.Text;
                hazardousItemInfo.numberofReceptacles = Convert.ToInt32(textBoxHazMatNumberOfReceptacles.Text);
                hazardousItemInfo.unitofMeasure = textBoxHazMatUnitOfMeasure.Text;
                hazardousItemInfo.containerType = textBoxHazMatContainerType.Text;
                hazardousItemInfo.hazardousDescription = textBoxHazMatHazardousDescription.Text;
                hazardousItemInfo.hazardousPhoneNumber = textBoxHazardousPhoneNumber.Text;
                hazardousItemInfo.shippingName = textBoxHazMatShippingName.Text;
            }
            if (radioButtonTemperatureControlledYes.Checked)
            {
                item.IsTemperatureControlled = true;
                TemperatureInformation temperatureInformation = item.TemperatureInformation;
                temperatureInformation.high = textBoxTemperatureHigh.Text;
                temperatureInformation.low = textBoxTemperatureLow.Text;
                temperatureInformation.highUom = textBoxTemperatureHighUOM.Text;
                temperatureInformation.lowUom = textBoxTemperatureLowUOM.Text;
            }

            item.sku= textBoxSKU.Text;
            item.itemclass = textBoxItemClass.Text;
            item.NmfcCode = textBoxItemNMFCCode.Text;
            item.declaredValueAmount = Convert.ToDouble(textBoxItemDeclaredValueAmount.Text);

            //   MessageBox.Show(dateTimePickerScheduledTimeFrom.Value.ToString());

            comboBoxItem.Items.Add(textBoxItemNumber.Text + "," + textBoxItemDescription.Text);
            return (item);
        }
        private void addStopAddressInformation(Stop stop)
        {
            AddressInformation addressInformations = stop.addressInformations;
            if (addressInformations is null)
                addressInformations = new AddressInformation();
            addressInformations.addressLine1 = textBoxAddress1.Text;
            addressInformations.addressLine2 = textBoxAddress2.Text;
            addressInformations.cityName = textBoxCity.Text;
            addressInformations.stateCode = textBoxState.Text;
            addressInformations.country = textBoxCountry.Text;
            addressInformations.zipCode = textBoxZipCode.Text;
            addressInformations.locationId = textBoxLocationId.Text;
            addressInformations.locationName = textBoxLocationName.Text;
        }
        private StopContactInformation addStopContactInformation(Stop stop)
        {
            IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
            StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();

            if (stopContactInformation is null)
            {
                stopContactInformation = new StopContactInformation();
                stopContactInformations.Add(stopContactInformation);
            }

            stopContactInformation.firstName = textBoxStopContactFirstName.Text;

            stopContactInformation.lastName = textBoxStopContactLastName.Text;
            stopContactInformation.email = textBoxStopContactEmail.Text;
            stopContactInformation.title = textBoxTitle.Text;

            if (radioButtonStopContactPrimaryYes.Checked)
                stopContactInformation.isPrimary = true;
            AddStopContactPhoneNumber(stopContactInformation);
            comboBoxStopContact.Items.Add(textBoxStopContactFirstName.Text+","+ textBoxStopContactLastName.Text+","+ textBoxStopContactEmail.Text);
            return stopContactInformation;
        }
        private CustomerContactInformation AddCustomerContactInformation(QuoteRequest quoteRequest)
        {

            IList<CustomerContactInformation> contactInformations = quoteRequest.contactInformations;

            CustomerContactInformation contactInformation = contactInformations.Where(s => s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();

            if (contactInformation is null)
            {
                contactInformation = new CustomerContactInformation();
                contactInformations.Add(contactInformation);
            }

            contactInformation.firstName = textBoxCustomerContactFirstName.Text;

            contactInformation.lastName = textBoxCustomerContactLastName.Text;
            contactInformation.email = textBoxCustomerContactEmail.Text;
            contactInformation.title = textBoxCustomerContactEmail.Text;

            if (radioButtonContactPrimaryYes.Checked)
                contactInformation.isPrimary = true;
            AddCustomerContactPhoneNumber(contactInformation);

            comboBoxCustomerConact.Items.Add(textBoxCustomerContactFirstName.Text + "," + textBoxCustomerContactLastName.Text + "," + textBoxCustomerContactEmail.Text);
            return contactInformation;
        }

        private PhoneNumber AddCustomerContactPhoneNumber(CustomerContactInformation contactInformation)
        {
            IList<PhoneNumber> customerContactPhoneNumbers = contactInformation.phoneNumbers;

            PhoneNumber phoneNumber = customerContactPhoneNumbers.Where(s => s.number == textBoxContactPhoneNumber.Text).FirstOrDefault();
            if (phoneNumber is null)
            {
                phoneNumber = new PhoneNumber();
                customerContactPhoneNumbers.Add(phoneNumber);
            }
            phoneNumber.number = textBoxContactPhoneNumber.Text;
            phoneNumber.type = comboBoxCustomerContactPhoneNumberType.Text;

            comboBoxCustomerContactPhoneNumber.Items.Add(textBoxContactPhoneNumber.Text + "," + comboBoxCustomerContactPhoneNumberType.Text);

            return phoneNumber;
        }

        private StopContactPhoneNumber AddStopContactPhoneNumber(StopContactInformation stopContactInformation)
        {
            IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContactInformation.phoneNumbers;

            StopContactPhoneNumber phoneNumber = stopContactPhoneNumbers.Where(s => s.number == textBoxContactPhoneNumber.Text).FirstOrDefault();
            if (phoneNumber is null)
            {
                phoneNumber = new StopContactPhoneNumber();
                stopContactPhoneNumbers.Add(phoneNumber);
            }
            phoneNumber.number = textBoxStopContactPhoneNumber.Text;
            phoneNumber.type = comboBoxStopContactPhoneNumberType.Text;
            if (radioButtonStopContactPhoneNumberPrimaryYes.Checked)
                phoneNumber.isPrimary = true;
            comboBoxStopContactPhoneNumber.Items.Add(textBoxStopContactPhoneNumber.Text + "," + comboBoxStopContactPhoneNumberType.Text);
            return phoneNumber;
        }

        bool validateStopContact()
        {
            if (textBoxStopContactFirstName.Text.Trim() == "")
            {
                MessageBox.Show("Contact First Name cannot be empty");
                return false;
            }
            if (textBoxStopContactLastName.Text.Trim() == "")
            {
                MessageBox.Show("Contact First Name cannot be empty");
                return false;
            }
            return true;
        }
        bool ValidateStopAddress() { 
            if (textBoxCity.Text.Trim() == "")
            {
                MessageBox.Show("City cannot be empty");
                return false;
            }
            if (textBoxState.Text.Trim() == "")
            {
                MessageBox.Show(" State cannot be empty");
                return false;
            }
            if (textBoxCountry.Text.Trim() == "")
            {
                MessageBox.Show("Country cannot be empty");
                return false;
            }
            if (textBoxZipCode.Text.Trim() == "")
            {
                MessageBox.Show("Zip Code cannot be empty");
                return false;
            }
            return true;
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            XPO xpo = new XPO();
            //xpo.createToken();
            orderResponse = await xpo.convertToOrder(tabQuotes1.SelectedTab);

            if (orderResponse is not null)
            {
                Label lblQuoteIdValue = tabQuotes.SelectedTab.Controls["lblQuoteIdValue"] as Label;
                lblQuoteId.Text = lblQuoteIdValue.Text;
                Label lblCarrierValue = tabQuotes.SelectedTab.Controls["lblCarrierNameValue"] as Label;
                lblCarrier.Text = lblCarrierValue.Text;
                Label lblQuotedAmountValue = tabQuotes.SelectedTab.Controls["lblTotalCostValue"] as Label;
                lblQuotedAmount.Text = lblQuotedAmountValue.Text;
               
                int orderId = orderResponse.orderId;
                lblOrderId.Text = orderId.ToString();
                tabControlMain.SelectedIndex = 3;
            }
        }

        private void tabPageQuotes_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddStopContactPhoneNumber_Click(object sender, EventArgs e)
        {
            if (!ValidateStopAddress())
            {
                return;
            }
            if (!validateStopContact())
            {
                return;
            }








            if (textBoxStopContactFirstName.Text.Trim() == "")
                {
                    MessageBox.Show("Contact First Name cannot be empty");
                    return;
                }
                if (textBoxStopContactLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Contact First Name cannot be empty");
                    return;
                }


            IList<Stop> stops = quoteRequest.stops;


           // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


            if (stop is null)
            {
                stop=addStop(quoteRequest);
            }
            IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;

            StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault(); 
         //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

            if (stopContactInformation is null)
            {
                stopContactInformation= addStopContactInformation(stop);
                return;
            }

            IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContactInformation.phoneNumbers;

            StopContactPhoneNumber phoneNumber = stopContactPhoneNumbers.Where(s => s.number == textBoxContactPhoneNumber.Text).FirstOrDefault(); 
            if (phoneNumber is null)
            {
                phoneNumber= AddStopContactPhoneNumber(stopContactInformation);
            }
                //IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContact.phoneNumbers;

            }

            private void buttonAddStop_Click(object sender, EventArgs e)
        {


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStops.Text == "PICKUP")
            {
                numericUpDownSequenceNo.Value = 1;
            }
        }

        private void numericUpDownSequenceNo_ValueChanged(object sender, EventArgs e)
        {
            if (comboBoxStops.Text == "PICKUP"  && numericUpDownSequenceNo.Value >= 1)
            {
                MessageBox.Show("Pickup Stop cannot be greater than 1");
            }
            if (comboBoxStops.Text == "INTERMEDIATE" && numericUpDownSequenceNo.Value == 1)
            {
                MessageBox.Show("Pickup Stop cannot be equal to 1");
            }
        }

        private void buttonAddStopContact_Click(object sender, EventArgs e)
        {

        }

        private void tabQuotes1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonstopReferenceTypeCodeAdd_Click(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


            if (stop is null)
            {
                stop = addStop(quoteRequest);
            }
            IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceTypeCodes;

            StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode== comboBoxStopReefernceTypeCode.Text).FirstOrDefault();
            //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

            if (stopReferenceTypeCode is null)
            {
                stopReferenceTypeCode = AddStopReferenceTypeCode(stop);
                return;
            }
        }

        private StopReferenceTypeCode AddStopReferenceTypeCode(Stop stop)
        {

            IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceTypeCodes;

            StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == textBoxCustomerContactEmail.Text).FirstOrDefault();

            if (stopReferenceTypeCode is null)
            {
                stopReferenceTypeCode = new StopReferenceTypeCode();
                stopReferenceTypeCodes.Add(stopReferenceTypeCode );
            }

            stopReferenceTypeCode.typeCode = comboBoxStopReefernceTypeCode.Text;

            stopReferenceTypeCode.value = textBoxStopReferenceTypeCodeValue.Text;

            comboBoxStopReefernceTypeCode.Items.Add(comboBoxStopReefernceTypeCode.Text);
            return stopReferenceTypeCode;
        }

        private void buttonStopSpecialRequirement_Click(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


            if (stop is null)
            {
                stop = addStop(quoteRequest);
            }
            IList<StopSpecialRequirement> stopSpecialRequirements = stop.SpecialRequirements;

            StopSpecialRequirement stopSpecialRequirement = stopSpecialRequirements.Where(s => s.code == comboBoxSpecialRequirementCode.Text).FirstOrDefault();
            //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

            if (stopSpecialRequirement is null)
            {
                stopSpecialRequirement = AddStopSpecialRequirement(stop);
                return;
            }
        }


        private StopSpecialRequirement AddStopSpecialRequirement(Stop stop)
        {
            IList<StopSpecialRequirement> stopSpecialRequirements = stop.SpecialRequirements;

            StopSpecialRequirement stopSpecialRequirement = stopSpecialRequirements.Where(s => s.code == textBoxCustomerContactEmail.Text).FirstOrDefault();

            if (stopSpecialRequirement is null)
            {
                stopSpecialRequirement = new StopSpecialRequirement();
                stopSpecialRequirements.Add(stopSpecialRequirement);
            }

            stopSpecialRequirement.code = comboBoxSpecialRequirementCode.Text;

            stopSpecialRequirement.value = textBoxSpecialRequirementValue.Text;

            comboBoxStopSpecialRequirement.Items.Add(comboBoxSpecialRequirementCode.Text);
            return stopSpecialRequirement;
        }

        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            IList<QuoteItem> items = quoteRequest.items;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            QuoteItem item = items.Where(s => s.productCode == textBoxItemProductCode.Text && s.itemNumber == textBoxItemNumber.Text && s.itemDescription == textBoxItemDescription.Text ).FirstOrDefault();


            if (item is null)
            {
                item = addItem(quoteRequest);
            }
            
        }

        private void buttonCustomerContactPhoneNumber_Click(object sender, EventArgs e)
        {
            IList<CustomerContactInformation> customerContactInformations = quoteRequest.ContactInformations;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            CustomerContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();


            if (customerContactInformation is null)
            {
                customerContactInformation = AddCustomerContactInformation(quoteRequest);
            }
            else
            {
                IList<PhoneNumber> customerContactPhoneNumbers = customerContactInformation.phoneNumbers;

                PhoneNumber phoneNumber = customerContactPhoneNumbers.Where(s => s.number == textBoxContactPhoneNumber.Text).FirstOrDefault();
                if (phoneNumber is null)
                {
                    phoneNumber = AddCustomerContactPhoneNumber(customerContactInformation);
                }
            }
            //IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContact.phoneNumbers;

        }

        private void buttonCustomerContact_Click(object sender, EventArgs e)
        {
            IList<CustomerContactInformation> customerContactInformations = quoteRequest.ContactInformations;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            CustomerContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();


            if (customerContactInformation is null)
            {
                customerContactInformation = AddCustomerContactInformation(quoteRequest);
            }
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddAdditionalServices_Click(object sender, EventArgs e)
        {
            IList <String> additionalServices = quoteRequest.AdditionalServices;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            String additionalService = additionalServices.Where(s => s == textBoxAdditionalServiceCode.Text).FirstOrDefault();


            additionalService = addAdditionalService(additionalService);
        }

        private void buttonAddCustomerReferenceNumber_Click(object sender, EventArgs e)
        {
            IList<CustomerReferenceNumber> referenceNumbers = quoteRequest.referenceNumbers;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            CustomerReferenceNumber referenceNumber = referenceNumbers.Where(s => s.typeCode == comboBoxContactReferenceNumberTypeCode.Text).FirstOrDefault();


            if (referenceNumber is null)
            {
                referenceNumber = addCustomerReferenceNumber(quoteRequest);
            }
        }

        private CustomerReferenceNumber addCustomerReferenceNumber(QuoteRequest quoteRequest)
        {
            IList<CustomerReferenceNumber> customerReferenceNumbers = quoteRequest.referenceNumbers;

            CustomerReferenceNumber customerReferenceNumber = customerReferenceNumbers.Where(s => s.typeCode == comboBoxContactReferenceNumberTypeCode.Text).FirstOrDefault();

            if (customerReferenceNumber is null)
            {
                customerReferenceNumber = new CustomerReferenceNumber();
                customerReferenceNumbers.Add(customerReferenceNumber);
            }

            customerReferenceNumber.typeCode = comboBoxContactReferenceNumberTypeCode.Text;
            customerReferenceNumber.value = textBoxContactReferenceNumberValue.Text;


            comboBoxCustomerReferenceNumbers.Items.Add(comboBoxContactReferenceNumberTypeCode.Text);
            return customerReferenceNumber;
        }
        private String addAdditionalService(String service)
        {
            IList<String> additionalServices = quoteRequest.AdditionalServices;

            String additionalService = additionalServices.Where(s => s == textBoxAdditionalServiceCode.Text).FirstOrDefault();

            additionalService = textBoxAdditionalServiceCode.Text;
            additionalServices.Add(additionalService);


            comboBoxAdditionalServicesCode.Items.Add(textBoxAdditionalServiceCode.Text);
            return additionalService;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void buttonStopReferenceTypeCodeAdd_Click_1(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


            if (stop is null)
            {
                stop = addStop(quoteRequest);
            }
            IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceTypeCodes;

            StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == comboBoxSpecialRequirementCode.Text).FirstOrDefault();
            //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

            if (stopReferenceTypeCode is null)
            {
                stopReferenceTypeCode = AddStopReferenceTypeCode(stop);
                return;
            }
        }
    }
}
