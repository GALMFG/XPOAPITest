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
    public partial class FormUserInterface : Form
    {
        QuoteResponse quoteResponse;
        QuoteRequest quoteRequest;

        OrderResponse orderResponse;
        OrderRequest orderRequest;


        FixedLists fixedLists;

        public FormUserInterface()
        {
            InitializeComponent();
        }

        public void ApplyConfigSettings()
        {
            quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
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
            fillAdditionalServicesDropDownList();
            ApplyConfigSettings();
        }
        public void fillAdditionalServicesDropDownList()
        {
            foreach (String item in fixedLists.customerAdditionalServices)
                comboBoxAdditionalServiceCode.Items.Add(item);
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
                comboBoxStopRefernceTypeCode.Items.Add(item);
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

                    TabPage lowestGuranteedPriceTabPage = lowestpriceQuote.addTab("Lowest Guaranteed Price Quote");
                    tabQuotes.TabPages.Add(lowestGuranteedPriceTabPage);
                    IList<Quote> quotes = priceResponse.quoteDetails;
                    foreach (Quote quote in quotes)
                    {
                        TabPage tabPageQuote = quote.addTab(quote.carrierName);
                        tabQuotes.TabPages.Add(tabPageQuote);
                    }

                }
            }


        }

        private void FormUserInterface_Load(object sender, EventArgs e)
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

            quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            quoteRequest.transportationMode.Add(XPOSettings.TransportationMode);
            if (textBoxPartnerOrderCode.Text != "")
                quoteRequest.partnerOrderCode = textBoxPartnerOrderCode.Text;

            if (comboBoxEquipmentCategoryCode.Text != "")
                quoteRequest.equipmentCategoryCode = comboBoxEquipmentCategoryCode.Text;

            if (comboBoxEquipmentTypeCode.Text != "")
                quoteRequest.equipmentTypeCode = comboBoxEquipmentTypeCode.Text;


            if (textBoxBOLNumber.Text != "")
                quoteRequest.bolNumber = textBoxBOLNumber.Text;

            if (textBoxShipmentId.Text != "")
                quoteRequest.shipmentId = textBoxShipmentId.Text;

            if (comboBoxApplicationSource.Text != "")
                quoteRequest.applicationSource = comboBoxApplicationSource.Text;


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
                Stop stop = stops.Where(s => s.type == "PICKUP").FirstOrDefault();
                if (stop is null)
                {
                    MessageBox.Show("Please provide stops infomation for PICKUP");
                    return false;
                }
                stop = stops.Where(s => s.type == "DELIVERY").FirstOrDefault();
                if (stop is null)
                {
                    MessageBox.Show("Please provide stops infomation for DELIVERY");
                    return false;
                }
            }
            IList<QuoteItem> items = quoteRequest.items;
            if (items.Count == 0)
            {
                MessageBox.Show("Please provide at least one item");
                return false;
            }
            IList<ContactInformation> contacts = quoteRequest.contactInformations;
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
            stop.scheduledTimeTo = dateTimePickerScheduledTimeTo.Value.ToString("yyyy-MM-ddTHH:mm:ss")+"-04:00";
            stop.scheduledTimeFrom = dateTimePickerScheduledTimeFrom.Value.ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
            stop.sequenceNo = Convert.ToInt32(numericUpDownSequenceNo.Value);

            stop.note = richTextBoxNote.Text;


            AddressInformation addressInformations = stop.addressInformations;
            addStopAddressInformation(stop);
            comboBoxStops.Items.Add(textBoxCity.Text + "," + textBoxState.Text + "," + textBoxCountry.Text + "," + textBoxZipCode.Text);

            return stop;
        }

        private void addItem(QuoteRequest quoteRequest)
        {
            if (quoteRequest is null) quoteRequest = new QuoteRequest();
            IList<QuoteItem> quoteItems = quoteRequest.items;

            QuoteItem item = quoteItems.Where(s => s.itemNumber == textBoxItemNumber.Text && s.itemDescription == textBoxItemDescription.Text).FirstOrDefault();

            if (item is null)
            {
                item = new QuoteItem();
                quoteItems.Add(item);
            }
            item.productCode = textBoxItemProductCode.Text;
            item.itemDescription = textBoxItemDescription.Text;
            item.itemNumber = textBoxItemNumber.Text;
            item.units = Convert.ToInt32(textBoxItemUnits.Text);
            item.unitTypeCode = comboBoxItemUnitTypeCode.Text;
            item.packageUnits = Convert.ToInt32(textBoxPackageUnits.Text);
            item.packageTypeCode = comboBoxProductTypeCode.Text;
            item.weight = Convert.ToInt32(textBoxItemWeight.Text);
            item.weightUomCode = comboBoxItemWeightUOMCode.Text;

            item.height = Convert.ToInt32(textBoxItemHeight.Text);
            item.heightUomCode = comboBoxItemHeightUOMCode.Text;

            item.length = Convert.ToInt32(textBoxItemLength.Text);
            item.lengthUomCode = comboBoxItemLengthUOMCode.Text;
            item.width = Convert.ToInt32(textBoxItemWidth.Text);
            item.widthUomCode = comboBoxItemWidthUOMCode.Text;

            if (radioButtonHazardousMaterialYes.Checked)
            {
                item.isHazmat = true;
                HazardousItemInfo hazardousItemInfo = item.hazardousItemInfo;
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
                item.isTemperatureControlled = true;
                TemperatureInformation temperatureInformation = item.temperatureInformation;
                temperatureInformation.high = textBoxTemperatureHigh.Text;
                temperatureInformation.low = textBoxTemperatureLow.Text;
                temperatureInformation.highUom = textBoxTemperatureHighUOM.Text;
                temperatureInformation.lowUom = textBoxTemperatureLowUOM.Text;
            }

            item.sku = textBoxSKU.Text;
            item.classcode = textBoxItemClass.Text;
            item.nmfcCode = textBoxItemNMFCCode.Text;
            item.declaredValueAmount = Convert.ToDouble(textBoxItemDeclaredValueAmount.Text);

            //   MessageBox.Show(dateTimePickerScheduledTimeFrom.Value.ToString());

            comboBoxItem.Items.Add(textBoxItemNumber.Text + "," + textBoxItemDescription.Text);
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
            stopContactInformation.title = textBoxStopContactTitle.Text;

            if (radioButtonStopContactPrimaryYes.Checked)
                stopContactInformation.isPrimary = true;
            AddStopContactPhoneNumber(stopContactInformation);
            comboBoxStopContact.Items.Add(textBoxStopContactFirstName.Text + "," + textBoxStopContactLastName.Text + "," + textBoxStopContactEmail.Text);
            return stopContactInformation;
        }
        private ContactInformation AddCustomerContactInformation(QuoteRequest quoteRequest)
        {

            IList<ContactInformation> contactInformations = quoteRequest.contactInformations;

            ContactInformation contactInformation = contactInformations.Where(s => s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();

            if (contactInformation is null)
            {
                contactInformation = new ContactInformation();
                contactInformations.Add(contactInformation);
            }

            contactInformation.firstName = textBoxCustomerContactFirstName.Text;

            contactInformation.lastName = textBoxCustomerContactLastName.Text;
            contactInformation.email = textBoxCustomerContactEmail.Text;
            contactInformation.title = textBoxCustomerContactTitle.Text;

            if (radioButtonContactPrimaryYes.Checked)
                contactInformation.isPrimary = true;
            AddCustomerContactPhoneNumber(contactInformation);

            comboBoxCustomerConact.Items.Add(textBoxCustomerContactFirstName.Text + "," + textBoxCustomerContactLastName.Text + "," + textBoxCustomerContactEmail.Text);
            return contactInformation;
        }

        private PhoneNumber AddCustomerContactPhoneNumber(ContactInformation contactInformation)
        {
            IList<PhoneNumber> ContactPhoneNumbers = contactInformation.phoneNumbers;

            PhoneNumber phoneNumber = ContactPhoneNumbers.Where(s => s.number == textBoxCustomerContactPhoneNumber.Text).FirstOrDefault();
            if (phoneNumber is null)
            {
                phoneNumber = new PhoneNumber();
                ContactPhoneNumbers.Add(phoneNumber);
            }
            phoneNumber.number = textBoxCustomerContactPhoneNumber.Text;
            phoneNumber.type = comboBoxCustomerContactPhoneNumberType.Text;

            comboBoxCustomerContactPhoneNumber.Items.Add(textBoxCustomerContactPhoneNumber.Text + "," + comboBoxCustomerContactPhoneNumberType.Text);

            return phoneNumber;
        }

        private StopContactPhoneNumber AddStopContactPhoneNumber(StopContactInformation stopContactInformation)
        {
            IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContactInformation.phoneNumbers;

            StopContactPhoneNumber phoneNumber = stopContactPhoneNumbers.Where(s => s.number == textBoxCustomerContactPhoneNumber.Text).FirstOrDefault();
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
        bool validateCustomerContact()
        {
            if (textBoxCustomerContactFirstName.Text.Trim() == "")
            {
                MessageBox.Show("Contact First Name cannot be empty");
                return false;
            }
            if (textBoxCustomerContactLastName.Text.Trim() == "")
            {
                MessageBox.Show("Contact Last Name cannot be empty");
                return false;
            }
            if (textBoxCustomerContactEmail.Text.Trim() == "")
            {
                MessageBox.Show("Contact Email cannot be empty");
                return false;
            }
            if (textBoxCustomerContactPhoneNumber.Text.Trim() == "")
            {
                MessageBox.Show("Contact Phone Number cannot be empty");
                return false;
            }
            return true;
        }
        bool ValidateStopAddress()
        {
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
            //XPO xpo = new XPO();
            ////xpo.createToken();
            //orderResponse = await xpo.convertToOrder(tabQuotes1.SelectedTab,quoteRequest);

            //if (orderResponse is not null)
            //{
            //    Label lblQuoteIdValue = tabQuotes.SelectedTab.Controls["lblQuoteIdValue"] as Label;
            //    lblQuoteId.Text = lblQuoteIdValue.Text;
            //    Label lblCarrierValue = tabQuotes.SelectedTab.Controls["lblCarrierNameValue"] as Label;
            //    lblCarrier.Text = lblCarrierValue.Text;
            //    Label lblQuotedAmountValue = tabQuotes.SelectedTab.Controls["lblTotalCostValue"] as Label;
            //    lblQuotedAmount.Text = lblQuotedAmountValue.Text;

            //    int orderId = orderResponse.orderId;
            //    lblOrderId.Text = orderId.ToString();
            //    tabControlMain.SelectedIndex = 3;
            //}
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
                stop = addStop(quoteRequest);
            }
            IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;

            StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();
            //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

            if (stopContactInformation is null)
            {
                stopContactInformation = addStopContactInformation(stop);
                return;
            }

            IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContactInformation.phoneNumbers;

            StopContactPhoneNumber phoneNumber = stopContactPhoneNumbers.Where(s => s.number == textBoxCustomerContactPhoneNumber.Text).FirstOrDefault();
            if (phoneNumber is null)
            {
                phoneNumber = AddStopContactPhoneNumber(stopContactInformation);
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
            if (comboBoxStops.Text == "PICKUP" && numericUpDownSequenceNo.Value >= 1)
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
            IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceNumbers;

            StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == comboBoxStopRefernceTypeCode.Text).FirstOrDefault();
            //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

            if (stopReferenceTypeCode is null)
            {
                stopReferenceTypeCode = AddStopReferenceTypeCode(stop);
                return;
            }
        }

        private StopReferenceTypeCode AddStopReferenceTypeCode(Stop stop)
        {

            IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceNumbers;

            StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == textBoxCustomerContactEmail.Text).FirstOrDefault();

            if (stopReferenceTypeCode is null)
            {
                stopReferenceTypeCode = new StopReferenceTypeCode();
                stopReferenceTypeCodes.Add(stopReferenceTypeCode);
            }

            stopReferenceTypeCode.typeCode = comboBoxStopRefernceTypeCode.Text;

            stopReferenceTypeCode.value = textBoxStopReferenceTypeCodeValue.Text;

            comboBoxStopRefernceNumbers.Items.Add(comboBoxStopRefernceTypeCode.Text);
            return stopReferenceTypeCode;
        }

        private void buttonStopSpecialRequirement_Click(object sender, EventArgs e)
        {
            if (!ValidateStopAddress())
            {
                return;
            }
            IList<Stop> stops = quoteRequest.stops;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


            if (stop is null)
            {
                stop = addStop(quoteRequest);
            }
            IList<StopSpecialRequirement> stopSpecialRequirements = stop.specialRequirement;

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
            IList<StopSpecialRequirement> stopSpecialRequirements = stop.specialRequirement;

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
            QuoteItem item = items.Where(s => s.productCode == textBoxItemProductCode.Text && s.itemNumber == textBoxItemNumber.Text && s.itemDescription == textBoxItemDescription.Text).FirstOrDefault();


            if (item is null)
            {
                addItem(quoteRequest);
            }

        }

        private void buttonCustomerContactPhoneNumber_Click(object sender, EventArgs e)
        {
            if (!validateCustomerContact()) return;
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            ContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();


            if (customerContactInformation is null)
            {
                customerContactInformation = AddCustomerContactInformation(quoteRequest);
            }
            else
            {
                IList<PhoneNumber> ContactPhoneNumbers = customerContactInformation.phoneNumbers;

                PhoneNumber phoneNumber = ContactPhoneNumbers.Where(s => s.number == textBoxCustomerContactPhoneNumber.Text).FirstOrDefault();
                if (phoneNumber is null)
                {
                    phoneNumber = AddCustomerContactPhoneNumber(customerContactInformation);
                }
            }
            //IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContact.phoneNumbers;

        }

        private void buttonCustomerContact_Click(object sender, EventArgs e)
        {
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            ContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();


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
            IList<AdditionalService> additionalServices = quoteRequest.additionalServices;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);

            addAdditionalService();

        }

        private void buttonAddCustomerReferenceNumber_Click(object sender, EventArgs e)
        {
            IList<CustomerReferenceNumber> referenceNumbers = quoteRequest.referenceNumbers;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            CustomerReferenceNumber referenceNumber = referenceNumbers.Where(s => s.typeCode == comboBoxContactReferenceNumberTypeCode.Text).FirstOrDefault();

            addCustomerReferenceNumber();
        }

        private void  addCustomerReferenceNumber()
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
        }
        private void addAdditionalService()
        {
            IList<AdditionalService> additionalServices = quoteRequest.additionalServices;

            AdditionalService additionalService = additionalServices.Where(s => s.code == comboBoxAdditionalServiceCode.Text).FirstOrDefault();

            if (additionalService is null)
            {
                additionalService = new AdditionalService();
                additionalService.code = comboBoxAdditionalServiceCode.Text;
                additionalServices.Add(additionalService);

                comboBoxAdditionalServices.Items.Add(comboBoxAdditionalServiceCode.Text);
            }
        }


        private void buttonStopReferenceTypeCodeAdd_Click_1(object sender, EventArgs e)
        {
            if (!ValidateStopAddress())
            {
                return;
            }
            IList<Stop> stops = quoteRequest.stops;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


            if (stop is null)
            {
                stop = addStop(quoteRequest);
            }
            IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceNumbers;

            StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == comboBoxSpecialRequirementCode.Text).FirstOrDefault();
            //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

            if (stopReferenceTypeCode is null)
            {
                stopReferenceTypeCode = AddStopReferenceTypeCode(stop);
                return;
            }
        }

        private void buttonAddStopContactPhoneNumber_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonAddStop_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonAddStopContact_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonCustomerContactPhoneNumber_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonAddCustomerContact_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddCustomerReferenceNumber_Click_1(object sender, EventArgs e)
        {

        }


        private async void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateQuoteRequestObject())
            {
                return;
            }
            XPO xpo = new XPO();
            //xpo.createToken();
            quoteResponse = await xpo.getQuote(quoteRequest);
           // quoteResponse = await xpo.getQuote();




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

                    TabPage lowestGuranteedPriceTabPage = lowestpriceQuote.addTab("Lowest Guaranteed Price Quote");
                    tabQuotes.TabPages.Add(lowestGuranteedPriceTabPage);
                    IList<Quote> quotes = priceResponse.quoteDetails;
                    foreach (Quote quote in quotes)
                    {
                        TabPage tabPageQuote = quote.addTab(quote.carrierName);
                        tabQuotes.TabPages.Add(tabPageQuote);
                    }

                }
                tabControlMain.SelectedIndex = 5;
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            XPO xpo = new XPO();
            //xpo.createToken();
            // orderResponse = await xpo.convertToOrder(tabQuotes.SelectedTab);
            TextBox txtTrackingNumber = tabPageGeneral.Controls["textBoxTrackingNumber"] as TextBox;
            if (txtTrackingNumber.Text.Trim() == "")
            {
                MessageBox.Show("Please provide the tracking Number");
                tabControlMain.SelectedIndex = 0;
                txtTrackingNumber.Focus();
                return;
            }
            orderResponse = await xpo.convertToOrder(tabQuotes.SelectedTab, quoteRequest, txtTrackingNumber.Text);
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
                tabControlMain.SelectedIndex = 6;
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxStops_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedStopValue=comboBoxStops.SelectedItem.ToString();
            if (selectedStopValue == "NEW...")
            {

                textBoxLocationId.Text = "";
                textBoxLocationName.Text = "";
                textBoxAddress1.Text = "";
                textBoxAddress2.Text = "";
                textBoxCity.Text = "";
                textBoxState.Text = "";
                textBoxCountry.Text = "";
                textBoxZipCode.Text = "";


                comboBoxStopContact.Items.Clear();
                comboBoxStopContact.Items.Add("NEW...");
                comboBoxStopContactPhoneNumber.Items.Clear();
                comboBoxStopContactPhoneNumber.Items.Add("NEW...");
                comboBoxStopRefernceNumbers.Items.Clear();
                comboBoxStopRefernceNumbers.Items.Add("NEW...");
                comboBoxStopSpecialRequirement.Items.Clear();
                comboBoxStopSpecialRequirement.Items.Add("NEW...");                
                textBoxStopContactFirstName.Text = "";
                textBoxStopContactLastName.Text = "";
                textBoxStopContactEmail.Text = "";
                textBoxStopContactTitle.Text = "";
                textBoxStopContactPhoneNumber.Text = "";
                textBoxSpecialRequirementValue.Text = "";
                textBoxStopReferenceTypeCodeValue.Text = "";
                comboBoxStopRefernceTypeCode.SelectedIndex = -1;
                comboBoxSpecialRequirementCode.SelectedIndex = -1;
                comboBoxStopContactPhoneNumberType.SelectedIndex = -1;
            }
            else { 
                String[] stopsValues = selectedStopValue.Split(",");
                IList<Stop> stops = quoteRequest.stops;


                // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
                Stop stop = stops.Where(s => s.addressInformations.cityName == stopsValues[0] && s.addressInformations.stateCode == stopsValues[1] && s.addressInformations.country == stopsValues[2] && s.addressInformations.zipCode == stopsValues[3]).FirstOrDefault();


                if (stop is not null)
                {
                    textBoxCity.Text = stopsValues[0];
                    textBoxState.Text = stopsValues[1];
                    textBoxCountry.Text = stopsValues[2];
                    textBoxZipCode.Text = stopsValues[3];
                    comboBoxStopType.SelectedItem = stop.type;
                    IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
                    comboBoxStopContact.Items.Clear();
                    comboBoxStopContact.Items.Add("NEW...");
                    int indexContacts = 0;
                    foreach (StopContactInformation contactInformation in stopContactInformations)
                    {
                        indexContacts++;
                        comboBoxStopContact.Items.Add(contactInformation.firstName+","+ contactInformation.lastName +","+ contactInformation.email);
                        if (indexContacts == 1)
                        {
                            textBoxStopContactFirstName.Text = contactInformation.firstName;
                            textBoxStopContactLastName.Text = contactInformation.lastName;
                            textBoxStopContactEmail.Text = contactInformation.email;
                            textBoxStopContactTitle.Text = contactInformation.title;
                            if (contactInformation.isPrimary)
                                radioButtonStopContactPrimaryYes.Checked = true;
                            IList<StopContactPhoneNumber> phoneNumbers = contactInformation.phoneNumbers;
                            comboBoxStopContactPhoneNumber.Items.Clear();
                            comboBoxStopContactPhoneNumber.Items.Add("NEW...");
                            int indexPhoneNumbers=0;
                            foreach (StopContactPhoneNumber phoneNumber in phoneNumbers)
                            {
                                indexPhoneNumbers++;
                                comboBoxStopContactPhoneNumber.Items.Add(phoneNumber.number + "," + phoneNumber.type);
                                if (indexPhoneNumbers == 1)
                                {
                                    textBoxStopContactPhoneNumber.Text = phoneNumber.number;
                                }
                            }
                        }
                    }                    
                }
            }
        }

        private void comboBoxCustomerRefernceNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedCustomerRefernceNumberValue = comboBoxStopRefernceNumbers.SelectedValue.ToString();
            IList<Stop> stops = quoteRequest.stops;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


            if (stop is not null)
            {
                IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceNumbers;
                StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == selectedCustomerRefernceNumberValue).FirstOrDefault();
                //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

                if (stopReferenceTypeCode is not null)
                {
                    comboBoxStopRefernceTypeCode.SelectedItem = stopReferenceTypeCode.typeCode;
                    textBoxStopReferenceTypeCodeValue.Text = stopReferenceTypeCode.value;
                }

            }
        }

        private void comboBoxStopContact_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedStopContactInformation = comboBoxStopContact.SelectedItem.ToString();
            if (selectedStopContactInformation == "NEW...")
            {
                textBoxStopContactEmail.Text = "";
                textBoxStopContactFirstName.Text = "";
                textBoxStopContactLastName.Text = "";
                textBoxStopContactTitle.Text = "";

                radioButtonStopContactPrimaryYes.Checked = false;

                comboBoxStopContactPhoneNumber.Items.Clear();
                comboBoxStopContactPhoneNumber.Items.Add("NEW...");
                textBoxStopContactPhoneNumber.Text = "";
                comboBoxStopContactPhoneNumber.Text = "";
                comboBoxStopContactPhoneNumberType.SelectedIndex = -1;
            }
            else
            {
                IList<Stop> stops = quoteRequest.stops;


                Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


                if (stop is not null)
                {
                    String[] selectedStopContactInformationValues = selectedStopContactInformation.Split(",");
                    IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
                    StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == selectedStopContactInformationValues[2]).FirstOrDefault();
                    //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

                    if (stopContactInformation is not null)
                    {
                        textBoxStopContactEmail.Text = stopContactInformation.email;
                        textBoxStopContactFirstName.Text = stopContactInformation.firstName;
                        textBoxStopContactLastName.Text = stopContactInformation.lastName;
                        textBoxStopContactTitle.Text = stopContactInformation.title;
                        if (stopContactInformation.isPrimary)
                            radioButtonStopContactPrimaryYes.Checked = true;
                        IList<StopContactPhoneNumber> phoneNumbers = stopContactInformation.phoneNumbers;
                        comboBoxStopContactPhoneNumber.Items.Clear();
                        comboBoxStopContactPhoneNumber.Items.Add("NEW...");
                        int indexPhoneNumbers = 0;
                        foreach (StopContactPhoneNumber phoneNumber in phoneNumbers)
                        {
                            indexPhoneNumbers++;
                            comboBoxStopContactPhoneNumber.Items.Add(phoneNumber.number + "," + phoneNumber.type);
                            if (indexPhoneNumbers == 1)
                            {
                                textBoxStopContactPhoneNumber.Text = phoneNumber.number;
                            }
                        }
                        comboBoxStopContactPhoneNumber.Text = "";

                    }
                }
            }
        }

        private void comboBoxCustomerConact_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedCustomerContactInformation = comboBoxCustomerConact.SelectedItem.ToString();
            if (selectedCustomerContactInformation == "NEW...")
            {
                textBoxStopContactEmail.Text = "";
                textBoxCustomerContactFirstName.Text = "";
                textBoxCustomerContactLastName.Text = "";
                textBoxCustomerContactEmail.Text = "";
                textBoxCustomerContactTitle.Text = "";
                radioButtonContactPrimaryYes.Checked = false;
                comboBoxCustomerContactPhoneNumber.Items.Clear();
                comboBoxCustomerContactPhoneNumber.Items.Add("NEW...");
                comboBoxCustomerContactPhoneNumber.Text = "";
                comboBoxCustomerContactPhoneNumberType.SelectedIndex = -1;
                textBoxCustomerContactPhoneNumber.Text = "";

            }
            else
            {
                
                    String[] selectedCustomerContactInformationValues = selectedCustomerContactInformation.Split(",");
                    IList<ContactInformation> contactInformations = quoteRequest.contactInformations;
                    ContactInformation contactInformation = contactInformations.Where(s => s.email == selectedCustomerContactInformationValues[2]).FirstOrDefault();
                    //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

                    if (contactInformation is not null)
                    {
                        textBoxCustomerContactEmail.Text = contactInformation.email;
                        textBoxCustomerContactFirstName.Text = contactInformation.firstName;
                        textBoxCustomerContactLastName.Text = contactInformation.lastName;
                        textBoxCustomerContactTitle.Text = contactInformation.title;
                        if (contactInformation.isPrimary)
                            radioButtonContactPrimaryYes.Checked = true;
                        IList<PhoneNumber> phoneNumbers = contactInformation.PhoneNumbers;
                        comboBoxCustomerContactPhoneNumber.Items.Clear();
                        comboBoxCustomerContactPhoneNumber.Items.Add("NEW...");
                        int indexPhoneNumbers = 0;
                        foreach (PhoneNumber phoneNumber in phoneNumbers)
                        {
                            indexPhoneNumbers++;
                            comboBoxCustomerContactPhoneNumber.Items.Add(phoneNumber.number + "," + phoneNumber.type);
                            if (indexPhoneNumbers == 1)
                            {
                                textBoxCustomerContactPhoneNumber.Text = phoneNumber.number;
                                comboBoxCustomerContactPhoneNumberType.SelectedItem = phoneNumber.type;
                            }
                        }
                    comboBoxCustomerContactPhoneNumber.Text = "";
                    }
            }
        }

        private void comboBoxCustomerContactPhoneNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedCustomerContactPhoneNumber = comboBoxCustomerContactPhoneNumber.SelectedItem.ToString();
            if (selectedCustomerContactPhoneNumber == "NEW...")
            {
                textBoxCustomerContactPhoneNumber.Text = "";
                comboBoxCustomerContactPhoneNumberType.SelectedIndex = -1;
            }
            else
            {

                IList<ContactInformation> contactInformations = quoteRequest.contactInformations;
                ContactInformation contactInformation = contactInformations.Where(s => s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();
                //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

                if (contactInformation is not null)
                {
                    String[] selectedStopContactPhoneNumberValues = selectedCustomerContactPhoneNumber.Split(",");

                    IList<PhoneNumber> phoneNumbers = contactInformation.PhoneNumbers;
                    PhoneNumber contactPhoneNumber = phoneNumbers.Where(s => s.number == selectedStopContactPhoneNumberValues[0]).FirstOrDefault();
                    textBoxCustomerContactPhoneNumber.Text = contactPhoneNumber.number;
                    comboBoxCustomerContactPhoneNumberType.SelectedItem = contactPhoneNumber.type;
                }
            }
        }

        private void comboBoxStopContactPhoneNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedStopContactPhoneNumber = comboBoxStopContactPhoneNumber.SelectedItem.ToString();
            if (selectedStopContactPhoneNumber == "NEW...")
            {
                textBoxStopContactPhoneNumber.Text = "";
                comboBoxStopContactPhoneNumberType.SelectedIndex = -1;

            }
            else
            {
                String[] selectedStopContactPhoneNumberValues = comboBoxStopContactPhoneNumber.SelectedItem.ToString().Split(",");

                IList<Stop> stops = quoteRequest.stops;


                Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();


                if (stop is not null)
                {
                    IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
                    StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();
                    //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

                    if (stopContactInformation is not null)
                    {

                        IList<StopContactPhoneNumber> phoneNumbers = stopContactInformation.PhoneNumbers;
                        StopContactPhoneNumber stopContactPhoneNumber = phoneNumbers.Where(s => s.number == selectedStopContactPhoneNumberValues[0]).FirstOrDefault();
                        textBoxStopContactPhoneNumber.Text = stopContactPhoneNumber.number;
                        comboBoxStopContactPhoneNumberType.SelectedItem = stopContactPhoneNumber.type;

                    }
                }
            }
        }

        private void comboBoxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedItem = comboBoxItem.SelectedItem.ToString();
            if (selectedItem == "NEW...")
            {
                textBoxItemProductCode.Text = "";
                textBoxItemDescription.Text = "";
                textBoxItemNumber.Text = "";
                textBoxItemUnits.Text = "";

                textBoxPackageUnits.Text = "";
                textBoxItemWeight.Text = "";
                textBoxItemHeight.Text = "";
                textBoxItemLength.Text = "";

                textBoxItemWidth.Text = "";
                textBoxSKU.Text = "";
                textBoxItemClass.Text = "";
                textBoxItemNMFCCode.Text = "";

                textBoxItemDeclaredValueAmount.Text = "";
                textBoxSKU.Text = "";
                textBoxItemClass.Text = "";
                textBoxItemNMFCCode.Text = "";

                comboBoxItemUnitTypeCode.SelectedIndex = -1;
                comboBoxProductTypeCode.SelectedIndex = -1;
                comboBoxItemWeightUOMCode.SelectedIndex = -1;
                comboBoxItemHeightUOMCode.SelectedIndex = -1;
                comboBoxItemLengthUOMCode.SelectedIndex = -1;
                comboBoxItemWidthUOMCode.SelectedIndex = -1;

                radioButtonHazardousMaterialYes.Checked = false;
                radioButtonTemperatureControlledYes.Checked = false;

                textBoxHazMatUNNumber.Text = "";
                textBoxHazMatPackingGroup.Text = "";
                textBoxHazMatReceptacleSize.Text = "";
                textBoxHazMatHazardousClass.Text = "";
                textBoxHazMatNumberOfReceptacles.Text = "";
                textBoxHazMatUnitOfMeasure.Text = "";
                textBoxHazMatContainerType.Text = "";
                textBoxHazMatHazardousDescription.Text = "";
                textBoxHazardousPhoneNumber.Text = "";
                textBoxHazMatShippingName.Text = "";

                textBoxTemperatureHigh.Text = "";
                textBoxTemperatureLow.Text = "";
                textBoxTemperatureHighUOM.Text = "";
                textBoxTemperatureLowUOM.Text = "";                

            }
            else
            {

                String[] selectedItemValues = selectedItem.Split(",");
                IList<QuoteItem> items = quoteRequest.items;
                QuoteItem item = items.Where(s => s.itemNumber == selectedItemValues[0]  && s.itemDescription == selectedItemValues[1]).FirstOrDefault();
                //   Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text).FirstOrDefault();

                if (item is not null)
                {
                    textBoxItemProductCode.Text = item.productCode;
                    textBoxItemDescription.Text = item.itemDescription;
                    textBoxItemNumber.Text = item.itemNumber;
                    textBoxItemUnits.Text = item.units.ToString();

                    textBoxPackageUnits.Text = item.packageUnits.ToString();
                    textBoxItemWeight.Text = item.weight.ToString();
                    textBoxItemHeight.Text = item.height.ToString();
                    textBoxItemLength.Text = item.length.ToString();

                    textBoxItemWidth.Text = item.width.ToString();

                    comboBoxItemUnitTypeCode.SelectedItem = item.unitTypeCode;

                    comboBoxItemWeightUOMCode.SelectedItem = item.weightUomCode;
                    comboBoxItemHeightUOMCode.SelectedItem = item.heightUomCode;
                    comboBoxItemLengthUOMCode.SelectedItem = item.lengthUomCode;
                    comboBoxItemWidthUOMCode.SelectedItem = item.widthUomCode;

                    if(item.isHazmat)
                    radioButtonHazardousMaterialYes.Checked = true;
                    if(item.isTemperatureControlled)
                    radioButtonTemperatureControlledYes.Checked = true;
                    HazardousItemInfo hazardousItemInfo =item.hazardousItemInfo;
                    if(hazardousItemInfo is not null)
                    {
                        textBoxHazMatUNNumber.Text = hazardousItemInfo.unNumber.ToString();
                        textBoxHazMatPackingGroup.Text = hazardousItemInfo.packingGroup.ToString();
                        textBoxHazMatReceptacleSize.Text = hazardousItemInfo.receptacleSize.ToString();
                        textBoxHazMatHazardousClass.Text = hazardousItemInfo.hazardousClass;
                        textBoxHazMatNumberOfReceptacles.Text = hazardousItemInfo.numberofReceptacles.ToString();
                        textBoxHazMatUnitOfMeasure.Text = hazardousItemInfo.unitofMeasure;
                        textBoxHazMatContainerType.Text = hazardousItemInfo.containerType;
                        textBoxHazMatHazardousDescription.Text = hazardousItemInfo.hazardousDescription;
                        textBoxHazardousPhoneNumber.Text = hazardousItemInfo.hazardousPhoneNumber;
                        textBoxHazMatShippingName.Text = hazardousItemInfo.shippingName;
                    }
                    TemperatureInformation temperatureInformation = item.temperatureInformation;
                    if (temperatureInformation is not null)
                    {

                        textBoxTemperatureHigh.Text = temperatureInformation.high;
                        textBoxTemperatureLow.Text = temperatureInformation.low;
                        textBoxTemperatureHighUOM.Text = temperatureInformation.highUom;
                        textBoxTemperatureLowUOM.Text = temperatureInformation.lowUom;
                    }
                }
            }
        }

        private void comboBoxStopType_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxStopContactFirstName.Text = "";
            textBoxStopContactLastName.Text = "";
            textBoxStopContactEmail.Text = "";
            textBoxStopContactTitle.Text = "";
            radioButtonStopContactPrimaryYes.Checked = false;
            numericUpDownSequenceNo.Value = 1;
            textBoxStopContactPhoneNumber.Text = "";

            comboBoxStopContact.Items.Clear();
            comboBoxStopContact.Items.Add("NEW...");
            comboBoxStopContactPhoneNumber.Items.Clear();
            comboBoxStopContactPhoneNumber.Items.Add("NEW...");
            comboBoxStopRefernceNumbers.Items.Clear();
            comboBoxStopRefernceNumbers.Items.Add("NEW...");
            comboBoxStopSpecialRequirement.Items.Clear();
            comboBoxStopSpecialRequirement.Items.Add("NEW...");

            textBoxSpecialRequirementValue.Text = "";
            textBoxStopReferenceTypeCodeValue.Text = "";


            textBoxLocationId.Text = "";
            textBoxLocationName.Text = "";
            textBoxAddress1.Text = "";
            textBoxAddress2.Text = "";
            textBoxCity.Text = "";
            textBoxState.Text = "";
            textBoxCountry.Text = "";
            textBoxZipCode.Text = "";
            textBoxLocationId.Text = "";
        }

        private void comboBoxCustomerReferenceNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedCustomerReferenceNumber = comboBoxCustomerReferenceNumbers.SelectedItem.ToString();
            if (selectedCustomerReferenceNumber == "NEW...")
            {
                textBoxContactReferenceNumberValue.Text = "";
            }
            else
            {
                IList<CustomerReferenceNumber> customerReferenceNumbers = quoteRequest.referenceNumbers;
                CustomerReferenceNumber customerReferenceNumber = customerReferenceNumbers.Where(s => s.typeCode == selectedCustomerReferenceNumber).FirstOrDefault();

                if (customerReferenceNumber is not null)
                {
                    textBoxContactReferenceNumberValue.Text = customerReferenceNumber.value;
                    comboBoxContactReferenceNumberTypeCode.SelectedItem = customerReferenceNumber.typeCode;

                }
            }
        }

    }
}
