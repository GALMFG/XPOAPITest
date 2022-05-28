using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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

        private void FormUserInterfaceCopy_Load(object sender, EventArgs e)
        {
            quoteRequest = new QuoteRequest();
            IntializeDropdownLists();
            getConfigData();
        }

        public void ApplyConfigSettings()
        {
            quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
        }
        public void LoadSampleJSONData()
        {
            if (quoteRequest is null)
            {
                MessageBox.Show("File Did not contain valid JSON data");
                return;
            }
            loadGeneralControls();
            loadStopControls();
            loadCustomerContactInformationControls();
            loadCustomerReferenceControls();
            loadCustomerAdditionalServicesControls();
            loadItemsControls();
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
            comboBoxStops.Items.Add("NEW...");
            comboBoxStopContact.Items.Add("NEW...");
            comboBoxStopContactPhoneNumber.Items.Add("NEW...");
            comboBoxCustomerConact.Items.Add("NEW...");
            comboBoxCustomerContactPhoneNumber.Items.Add("NEW...");
            ApplyConfigSettings();
        }
        public void loadCustomerContactInformationControls()
        {
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;
            int counterCustomerContactInformation = 0;
            foreach (ContactInformation contactInformation in quoteRequest.contactInformations)
            {
                counterCustomerContactInformation++;
                comboBoxCustomerConact.Items.Add(contactInformation.firstName + "," + contactInformation.lastName + "," + contactInformation.email);
                if (counterCustomerContactInformation == 1)
                {
                    textBoxCustomerContactFirstName.Text = contactInformation.firstName;

                    textBoxCustomerContactLastName.Text = contactInformation.lastName;
                    textBoxCustomerContactEmail.Text = contactInformation.email;
                    textBoxCustomerContactTitle.Text = contactInformation.title;

                    if (contactInformation.isPrimary)
                        radioButtonContactPrimaryYes.Checked = true;
                    loadCustomerContactPhoneNumberControls();
                }
            }
            if (counterCustomerContactInformation >= 1)
                comboBoxCustomerConact.SelectedIndex = 0;

        }
        public void loadCustomerContactPhoneNumberControls()
        {
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;

            ContactInformation contactInformation = customerContactInformations.Where(s => s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();

            if (contactInformation is not null)
            {
                int counterCustomerContactPhoneNumber = 0;
                foreach (PhoneNumber customerContactPhoneNumber in contactInformation.phoneNumbers)
                {
                    counterCustomerContactPhoneNumber++;
                    if (counterCustomerContactPhoneNumber == 1)
                    {
                        comboBoxCustomerContactPhoneNumberType.Text = customerContactPhoneNumber.number;
                        textBoxCustomerContactPhoneNumber.Text = customerContactPhoneNumber.type;
                    }
                    comboBoxCustomerContactPhoneNumber.Items.Add(customerContactPhoneNumber.number + "," + customerContactPhoneNumber.type);
                }
                if (counterCustomerContactPhoneNumber >= 1)
                    comboBoxCustomerContactPhoneNumber.SelectedIndex = 0;
            }
        }
        public void loadCustomerAdditionalServicesControls()
        {
            int counterCustomerAdditionalService = 0;
            foreach (AdditionalService additionalService in quoteRequest.additionalServices)
            {
                counterCustomerAdditionalService++;
                comboBoxAdditionalServices.Items.Add(additionalService.code);
                if (counterCustomerAdditionalService == 1)
                {

                    comboBoxAdditionalServiceCode.SelectedItem = additionalService.code;
                }
            }
            if (counterCustomerAdditionalService >= 1)
                comboBoxAdditionalServices.SelectedIndex = 0;
        }
        public void loadCustomerReferenceControls()
        {
            int counterCustomerReference = 0;
            foreach (CustomerReferenceNumber customerReferenceNumber in quoteRequest.referenceNumbers)
            {
                counterCustomerReference++;
                comboBoxCustomerReferenceNumbers.Items.Add(customerReferenceNumber.typeCode);
                if (counterCustomerReference == 1)
                {
                    comboBoxCustomerReferenceNumberTypeCode.SelectedItem = customerReferenceNumber.typeCode;
                    textBoxContactReferenceNumberValue.Text = customerReferenceNumber.value;
                }
            }
            if (counterCustomerReference >= 1)
                comboBoxCustomerReferenceNumbers.SelectedIndex = 0;

        }
        public void loadItemsControls()
        {
            int counterItems = 0;
            foreach (QuoteItem item in quoteRequest.items)
            {
                counterItems++;
                comboBoxItem.Items.Add(item.itemNumber + "," + item.itemDescription);
                if (counterItems == 1)
                {
                    textBoxItemProductCode.Text = item.productCode;
                    textBoxItemDescription.Text = item.itemDescription;
                    textBoxItemNumber.Text = item.itemNumber;
                    textBoxItemUnits.Text = item.units.ToString();
                    comboBoxItemUnitTypeCode.Text = item.unitTypeCode;
                    textBoxPackageUnits.Text = item.packageUnits.ToString();
                    comboBoxPackageTypeCode.Text = item.packageTypeCode.ToString();
                    textBoxItemWeight.Text = item.weight.ToString();
                    comboBoxItemWeightUOMCode.Text = item.weightUomCode;

                    textBoxItemHeight.Text = item.height.ToString();
                    comboBoxItemHeightUOMCode.Text = item.heightUomCode;

                    textBoxItemLength.Text = item.length.ToString();
                    comboBoxItemLengthUOMCode.SelectedItem = item.lengthUomCode;
                    textBoxItemWidth.Text = item.width.ToString();
                    comboBoxItemWidthUOMCode.SelectedItem = item.widthUomCode;
                    loadHazardousMaterials(item);
                    loadTemperatureInformation(item);
                }
            }
        }
        public void loadStopAddressControls()
        {

            IList<Stop> stops = quoteRequest.stops;


            Stop stop = stops.Where(s => s.type == "PICKUP").FirstOrDefault();
            if (stop is null)
            {
                MessageBox.Show("File does not have a PICKUP stop");
                return;
            }
            if (stop is not null)
            {
                AddressInformation addressInformations = stop.addressInformations;
                if (addressInformations is not null)
                {
                    textBoxStopAddress1.Text = addressInformations.addressLine1;
                    textBoxStopAddress2.Text = addressInformations.addressLine2;
                    textBoxStopCity.Text = addressInformations.cityName;
                    textBoxStopState.Text = addressInformations.stateCode;
                    textBoxStopCountry.Text = addressInformations.country;
                    textBoxStopZipCode.Text = addressInformations.zipCode;
                    textBoxStopLocationId.Text = addressInformations.locationId;
                    textBoxStopLocationName.Text = addressInformations.locationName;
                }
            }
        }
        public void loadStopContactInformationControls()
        {
            IList<Stop> stops = quoteRequest.stops;
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is not null)
            {
                int counterStopContactInformation = 0;
                comboBoxStopContact.Items.Clear();
                comboBoxStopContact.Items.Add("NEW...");
                foreach (StopContactInformation stopContactInformation in stop.stopContactInformations)
                {
                    counterStopContactInformation++;
                    if (counterStopContactInformation == 1)
                    {
                        textBoxStopContactFirstName.Text = stopContactInformation.firstName;
                        textBoxStopContactLastName.Text = stopContactInformation.lastName;
                        textBoxStopContactEmail.Text = stopContactInformation.email;
                        textBoxStopContactTitle.Text = stopContactInformation.title;

                        if (stopContactInformation.isPrimary)
                            radioButtonStopContactPrimaryYes.Checked = true;
                        IList<StopContactPhoneNumber> phoneNumbers = stopContactInformation.phoneNumbers;
                        loadStopContactPhoneNumberControls();
                    }
                    comboBoxStopContact.Items.Add(stopContactInformation.firstName + "," + stopContactInformation.lastName + "," + stopContactInformation.email);
                }
                if (counterStopContactInformation >= 1)
                    comboBoxStopContact.SelectedIndex = 1;
            }

        }
        public void loadStopContactPhoneNumberControls()
        {
            IList<Stop> stops = quoteRequest.stops;
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is not null)
            {
                IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
                StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();

                if (stopContactInformation is not null)
                {
                    int counterStopContactPhoneNumber = 0;
                    comboBoxStopContactPhoneNumber.Items.Clear();
                    comboBoxStopContactPhoneNumber.Items.Add("NEW...");
                    foreach (StopContactPhoneNumber stopContactPhoneNumber in stopContactInformation.phoneNumbers)
                    {
                        counterStopContactPhoneNumber++;
                        if (counterStopContactPhoneNumber == 1)
                        {
                            textBoxStopContactPhoneNumber.Text = stopContactPhoneNumber.number;
                            comboBoxStopContactPhoneNumberType.Text = stopContactPhoneNumber.type;
                        }
                        comboBoxStopContactPhoneNumber.Items.Add(stopContactPhoneNumber.number + "," + stopContactPhoneNumber.type);
                    }
                    if (counterStopContactPhoneNumber >= 1)
                        comboBoxStopContactPhoneNumber.SelectedIndex = 1;
                }
            }
        }
        public void loadGeneralControls()
        {

            textBoxPartnerOrderCode.Text = quoteRequest.partnerOrderCode;

            comboBoxEquipmentCategoryCode.Text = quoteRequest.equipmentCategoryCode;

            comboBoxEquipmentTypeCode.Text = quoteRequest.equipmentTypeCode;

            textBoxBOLNumber.Text = quoteRequest.bolNumber;

            textBoxShipmentId.Text = quoteRequest.shipmentId;

            comboBoxApplicationSource.Text = quoteRequest.applicationSource;
        }
        public void loadStopControls()
        {
            comboBoxStops.Items.Clear();
            comboBoxStops.Items.Add("NEW...");
            IList<Stop> stops = quoteRequest.stops;
            int counterStops = 0;
            String stopType = "";
            DateTime TimeRangeFrom = new DateTime();
            DateTime TimeRangeTo = new DateTime();
            foreach (Stop stop in stops)
            {
                counterStops++;
                AddressInformation addressInformation = stop.addressInformations;
                comboBoxStops.Items.Add(addressInformation.cityName + "," + addressInformation.stateCode + "," + addressInformation.country + "," + addressInformation.zipCode);
                if (counterStops == 1)
                {
                    loadStopAddressControls();
                    loadStopContactInformationControls();
                    loadStopReferenceControls();
                    loadStopSpecialRequirementsControls();
                    stopType = stop.type;
                    numericUpDownSequenceNo.Value = stop.sequenceNo;
                    TimeRangeTo = DateTime.Parse(stop.scheduledTimeTo);
                    TimeRangeFrom = DateTime.Parse(stop.scheduledTimeFrom);
                }

            }
            if (counterStops >= 1)
                comboBoxStops.SelectedIndex = 1;

            comboBoxStopType.SelectedItem = stopType;
            dateTimePickerScheduledTimeTo.Value = TimeRangeTo;
            dateTimePickerScheduledTimeFrom.Value = TimeRangeFrom;


        }
        public void loadStopReferenceControls()
        {
            IList<Stop> stops = quoteRequest.stops;
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is not null)
            {
                int counterStopReferenceTypeCode = 0;
                comboBoxStopRefernceNumbers.Items.Clear();
                comboBoxStopRefernceNumbers.Text = "";
                foreach (StopReferenceTypeCode stopReferenceTypeCode in stop.stopReferenceNumbers)
                {
                    counterStopReferenceTypeCode++;
                    if (counterStopReferenceTypeCode == 1)
                    {
                        comboBoxStopRefernceTypeCode.SelectedItem = stopReferenceTypeCode.typeCode;
                        textBoxStopReferenceTypeCodeValue.Text = stopReferenceTypeCode.value;
                    }
                    comboBoxStopRefernceNumbers.Items.Add(stopReferenceTypeCode.typeCode);
                }
            }

        }
        public void loadStopSpecialRequirementsControls()
        {
            IList<Stop> stops = quoteRequest.stops;
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is not null)
            {
                int counterStopSpecialRequirement = 0;
                comboBoxStopSpecialRequirement.Items.Clear();
                comboBoxStopSpecialRequirement.Text = "";
                foreach (StopSpecialRequirement stopSpecialRequirement in stop.specialRequirement)
                {
                    counterStopSpecialRequirement++;
                    if (counterStopSpecialRequirement == 1)
                    {
                        comboBoxSpecialRequirementCode.SelectedItem = stopSpecialRequirement.code;
                        textBoxSpecialRequirementValue.Text = stopSpecialRequirement.value;
                    }
                    comboBoxStopSpecialRequirement.Items.Add(stopSpecialRequirement.code);
                }
            }
        }
        public void loadHazardousMaterials(Item item)
        {
            if (item.isHazmat)
            {
                HazardousItemInfo hazardousItemInfo = item.hazardousItemInfo;
                radioButtonHazardousMaterialYes.Checked = true;
                textBoxHazMatUNNumber.Text = item.hazardousItemInfo.unNumber.ToString();
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
        }
        public void loadTemperatureInformation(Item item)
        {

            if (item.isTemperatureControlled)
            {
                radioButtonTemperatureControlledYes.Checked = true;
                TemperatureInformation temperatureInformation = item.temperatureInformation;
                textBoxTemperatureHigh.Text = temperatureInformation.high;
                textBoxTemperatureLow.Text = temperatureInformation.low;
                textBoxTemperatureHighUOM.Text = temperatureInformation.highUom;
                textBoxTemperatureLowUOM.Text = temperatureInformation.lowUom;
            }
        }
        public void resetCustomerContactInformationControls()
        {
            textBoxCustomerContactFirstName.Text = "";
            textBoxCustomerContactLastName.Text = "";
            textBoxCustomerContactEmail.Text = "";
            textBoxCustomerContactTitle.Text = "";
            radioButtonContactPrimaryYes.Checked = false;
            comboBoxCustomerContactPhoneNumber.Items.Clear();
            comboBoxCustomerContactPhoneNumber.Items.Add("NEW...");
            comboBoxCustomerContactPhoneNumber.Text = "";
            resetCustomerContactPhoneNumberControls();
        }
        public void resetCustomerContactPhoneNumberControls()
        {
            comboBoxCustomerContactPhoneNumberType.SelectedIndex = -1;
            textBoxCustomerContactPhoneNumber.Text = "";
        }
        public void resetHazardousMaterials()
        {
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
        }
        public void resetTemperatureInformation()
        {
            textBoxTemperatureHigh.Text = "";
            textBoxTemperatureLow.Text = "";
            textBoxTemperatureHighUOM.Text = "";
            textBoxTemperatureLowUOM.Text = "";
        }
        public void resetItemsControls()
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
            comboBoxPackageTypeCode.SelectedIndex = -1;
            comboBoxItemWeightUOMCode.SelectedIndex = -1;
            comboBoxItemHeightUOMCode.SelectedIndex = -1;
            comboBoxItemLengthUOMCode.SelectedIndex = -1;
            comboBoxItemWidthUOMCode.SelectedIndex = -1;

            radioButtonHazardousMaterialYes.Checked = false;
            radioButtonTemperatureControlledYes.Checked = false;
            resetHazardousMaterials();
            resetTemperatureInformation();

        }
        public void resetCustomerReferenceControls()
        {
            textBoxContactReferenceNumberValue.Text = "";
        }
        public void resetCustomerAdditionalServicesControls()
        {
        }
        public void resetStopContactInformationControls()
        {
            resetStopContactPhoneNumberControls();
            textBoxStopContactEmail.Text = "";
            textBoxStopContactFirstName.Text = "";
            textBoxStopContactLastName.Text = "";
            textBoxStopContactTitle.Text = "";
            radioButtonStopContactPrimaryYes.Checked = false;
            comboBoxStopContactPhoneNumber.Items.Clear();
            comboBoxStopContactPhoneNumber.Items.Add("NEW...");
            comboBoxStopContactPhoneNumber.Text = "";

        }
        public void resetStopContactPhoneNumberControls()
        {
            textBoxStopContactPhoneNumber.Text = "";
            comboBoxStopContactPhoneNumberType.SelectedIndex = -1;
        }
        public void resetStopAddressControls()
        {
            textBoxStopLocationId.Text = "";
            textBoxStopLocationName.Text = "";
            textBoxStopAddress1.Text = "";
            textBoxStopAddress2.Text = "";
            textBoxStopCity.Text = "";
            textBoxStopState.Text = "";
            textBoxStopCountry.Text = "";
            textBoxStopZipCode.Text = "";
        }

        public void resetStopReferenceControls()
        {
            comboBoxStopRefernceNumbers.Items.Clear();
            textBoxStopReferenceTypeCodeValue.Text = "";
            comboBoxStopRefernceTypeCode.SelectedIndex = -1;
        }
        public void resetStopSpecialRequirementsControls()
        {
            comboBoxStopSpecialRequirement.Items.Clear();
            textBoxSpecialRequirementValue.Text = "";
            comboBoxSpecialRequirementCode.SelectedIndex = -1;
        }

        public void resetStopControls()
        {
            comboBoxStopContact.Items.Clear();
            comboBoxStopContact.Items.Add("NEW...");
            comboBoxStopContact.Text = "";
            resetStopAddressControls();
            resetStopContactInformationControls();

            comboBoxStopRefernceNumbers.Items.Clear();
            comboBoxStopRefernceNumbers.Text = "";
            resetStopReferenceControls();
            comboBoxStopSpecialRequirement.Items.Clear();
            comboBoxStopSpecialRequirement.Text = "";
            resetStopSpecialRequirementsControls();
            comboBoxStopType.SelectedIndex = -1;
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
                comboBoxCustomerReferenceNumberTypeCode.Items.Add(item);
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
                comboBoxPackageTypeCode.Items.Add(item);
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
                tabControlQuotes.TabPages.Clear();
                tabControlMain.SelectedIndex = 7;
                foreach (PriceResponse priceResponse in priceSearchResponse)
                {
                    Quote lowestpriceQuote = priceResponse.lowestPriceQuote;

                    TabPage lowestPriceTabPage = lowestpriceQuote.addTab("Lowest Price Quote");
                    tabControlQuotes.TabPages.Add(lowestPriceTabPage);
                    Quote lowestGuranteedpriceQuote = priceResponse.lowestGuaranteedQuotePrice;

                    TabPage lowestGuranteedPriceTabPage = lowestpriceQuote.addTab("Lowest Guaranteed Price Quote");
                    tabControlQuotes.TabPages.Add(lowestGuranteedPriceTabPage);
                    IList<Quote> quotes = priceResponse.quoteDetails;
                    foreach (Quote quote in quotes)
                    {
                        TabPage tabPageQuote = quote.addTab(quote.carrierName);
                        tabControlQuotes.TabPages.Add(tabPageQuote);
                    }

                }
            }


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
        private bool ValidateQuoteGeneralSettings()
        {
            if (textBoxPartnerOrderCode.Text.Trim() == "")
            {
                MessageBox.Show("Please provide Partner Order Code");
                return false;
            }

            if (comboBoxEquipmentCategoryCode.Text.Trim() == "")
            {
                MessageBox.Show("Please provide EquipmentCategoryCode");
                return false;
            }

            if (comboBoxEquipmentTypeCode.Text.Trim() == "")
            {
                MessageBox.Show("Please provide Equipment Type Code");
                return false;
            }


            if (textBoxBOLNumber.Text.Trim() == "")
            {
                MessageBox.Show("Please provide BOL Number");
                return false;
            }

            if (textBoxShipmentId.Text.Trim() == "")
            {
                MessageBox.Show("Please provide Shipment Id");
                return false;
            }

            if (comboBoxApplicationSource.Text.Trim() == "")
            {
                MessageBox.Show("Please provide Application Source");
                return false;
            }
            return true;
        }
        private bool ValidateQuoteRequestObject()
        {
            if (!ValidateQuoteGeneralSettings())
            {
                return false;
            }
            quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            if (!quoteRequest.transportationMode.Contains(XPOSettings.TransportationMode))
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
        private Stop addStop()
        {
            IList<Stop> stops = quoteRequest.stops;
            // Stop stop = null;

            //stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.cityName == textBoxState.Text && s.addressInformations.cityName == textBoxCountry.Text && s.addressInformations.cityName == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is null)
            {
                stop = new Stop();
                quoteRequest.AddStop(stop);
                comboBoxStops.Items.Add(textBoxStopCity.Text + "," + textBoxStopState.Text + "," + textBoxStopCountry.Text + "," + textBoxStopZipCode.Text);
            }
            stop.type = comboBoxStopType.Text;
            //   MessageBox.Show(dateTimePickerScheduledTimeFrom.Value.ToString());
            stop.scheduledTimeTo = dateTimePickerScheduledTimeTo.Value.ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
            stop.scheduledTimeFrom = dateTimePickerScheduledTimeFrom.Value.ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
            stop.sequenceNo = Convert.ToInt32(numericUpDownSequenceNo.Value);

            stop.note = richTextBoxNote.Text;


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
                comboBoxItem.Items.Add(textBoxItemNumber.Text + "," + textBoxItemDescription.Text);
            }
            item.productCode = textBoxItemProductCode.Text;
            item.itemDescription = textBoxItemDescription.Text;
            item.itemNumber = textBoxItemNumber.Text;
            item.units = Convert.ToInt32(textBoxItemUnits.Text);
            item.unitTypeCode = comboBoxItemUnitTypeCode.Text;
            item.packageUnits = Convert.ToInt32(textBoxPackageUnits.Text);
            item.packageTypeCode = comboBoxPackageTypeCode.Text;
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


        }
        private void addStopAddressInformation(Stop stop)
        {
            if (!ValidateStopAddress())
            {
                return;
            }
            AddressInformation addressInformations = stop.addressInformations;
            if (addressInformations is null)
                addressInformations = new AddressInformation();
            addressInformations.addressLine1 = textBoxStopAddress1.Text;
            addressInformations.addressLine2 = textBoxStopAddress2.Text;
            addressInformations.cityName = textBoxStopCity.Text;
            addressInformations.stateCode = textBoxStopState.Text;
            addressInformations.country = textBoxStopCountry.Text;
            addressInformations.zipCode = textBoxStopZipCode.Text;
            addressInformations.locationId = textBoxStopLocationId.Text;
            addressInformations.locationName = textBoxStopLocationName.Text;
        }

        public Stop getStop()
        {
            IList<Stop> stops = quoteRequest.stops;

            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            return stop;
        }
        public StopContactInformation getStopContactInformation(Stop stop)
        {
            IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
            StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();
            return stopContactInformation;
        }
        public StopContactPhoneNumber getStopContactPhoneNumber(StopContactInformation stopContactInformation)
        {
            IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContactInformation.phoneNumbers;

            StopContactPhoneNumber phoneNumber = stopContactPhoneNumbers.Where(s => s.number == textBoxCustomerContactPhoneNumber.Text).FirstOrDefault();
            return phoneNumber;
        }

        private StopContactInformation addStopContactInformation(Stop stop)
        {
            StopContactInformation stopContactInformation = getStopContactInformation(stop);

            if (stopContactInformation is null)
            {
                stopContactInformation = new StopContactInformation();
                stop.stopContactInformations.Add(stopContactInformation);
                comboBoxStopContact.Items.Add(textBoxStopContactFirstName.Text + "," + textBoxStopContactLastName.Text + "," + textBoxStopContactEmail.Text);
            }

            stopContactInformation.firstName = textBoxStopContactFirstName.Text;

            stopContactInformation.lastName = textBoxStopContactLastName.Text;
            stopContactInformation.email = textBoxStopContactEmail.Text;
            stopContactInformation.title = textBoxStopContactTitle.Text;

            if (radioButtonStopContactPrimaryYes.Checked)
                stopContactInformation.isPrimary = true;
            //           AddStopContactPhoneNumber(stopContactInformation);
            return stopContactInformation;
        }
        private ContactInformation AddCustomerContactInformation()
        {

            IList<ContactInformation> contactInformations = quoteRequest.contactInformations;

            ContactInformation contactInformation = contactInformations.Where(s => s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();

            if (contactInformation is null)
            {
                contactInformation = new ContactInformation();
                contactInformations.Add(contactInformation);
                comboBoxCustomerConact.Items.Add(textBoxCustomerContactFirstName.Text + "," + textBoxCustomerContactLastName.Text + "," + textBoxCustomerContactEmail.Text);
            }

            contactInformation.firstName = textBoxCustomerContactFirstName.Text;

            contactInformation.lastName = textBoxCustomerContactLastName.Text;
            contactInformation.email = textBoxCustomerContactEmail.Text;
            contactInformation.title = textBoxCustomerContactTitle.Text;

            if (radioButtonContactPrimaryYes.Checked)
                contactInformation.isPrimary = true;
            AddCustomerContactPhoneNumber(contactInformation);

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
                comboBoxCustomerContactPhoneNumber.Items.Add(textBoxCustomerContactPhoneNumber.Text + "," + comboBoxCustomerContactPhoneNumberType.Text);
            }
            phoneNumber.number = textBoxCustomerContactPhoneNumber.Text;
            phoneNumber.type = comboBoxCustomerContactPhoneNumberType.Text;



            return phoneNumber;
        }

        private StopContactPhoneNumber AddStopContactPhoneNumber(StopContactInformation stopContactInformation)
        {
            IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContactInformation.phoneNumbers;

            StopContactPhoneNumber phoneNumber = new StopContactPhoneNumber();
            stopContactPhoneNumbers.Add(phoneNumber);

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
            if (textBoxStopContactEmail.Text.Trim() == "")
            {
                MessageBox.Show("Contact Email cannot be empty");
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
            return true;
        }
        bool ValidateStopAddress()
        {
            if (textBoxStopCity.Text.Trim() == "")
            {
                MessageBox.Show("City cannot be empty");
                return false;
            }
            if (textBoxStopState.Text.Trim() == "")
            {
                MessageBox.Show(" State cannot be empty");
                return false;
            }
            if (textBoxStopCountry.Text.Trim() == "")
            {
                MessageBox.Show("Country cannot be empty");
                return false;
            }
            if (textBoxStopZipCode.Text.Trim() == "")
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

            if (textBoxStopContactPhoneNumber.Text.Trim() == "")
            {
                MessageBox.Show("Contact Phone Number cannot be empty");
                return;
            }

            Stop stop = getStop();
            StopContactInformation stopContactInformation = getStopContactInformation(stop);
            if (stopContactInformation is null)
                stopContactInformation = addStopContactInformation(stop);

            StopContactPhoneNumber stopContactPhoneNumber = getStopContactPhoneNumber(stopContactInformation);

            if (stopContactPhoneNumber is null)
                AddStopContactPhoneNumber(stopContactInformation);

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
            Stop stop = addStop();

            if (stop is null)
            {
                stop = addStop();
            }
            AddStopReferenceTypeCode();
        }

        private void AddStopReferenceTypeCode()
        {
            Stop stop = addStop();

            if (stop is null)
            {
                stop = addStop();
            }

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
        }

        private void buttonStopSpecialRequirement_Click(object sender, EventArgs e)
        {

            AddStopSpecialRequirement();
        }


        private void AddStopSpecialRequirement()
        {
            IList<Stop> stops = quoteRequest.stops;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();

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
            if (textBoxCustomerContactPhoneNumber.Text.Trim() == "")
            {
                MessageBox.Show("Contact Phone Number cannot be empty");
                return;
            }
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            ContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();


            if (customerContactInformation is null)
            {
                customerContactInformation = AddCustomerContactInformation();
            }
            AddCustomerContactPhoneNumber(customerContactInformation);
            //IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContact.phoneNumbers;

        }

        private void buttonCustomerContact_Click(object sender, EventArgs e)
        {
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;


            // Stop stop = stops.Single(s => s.addressInformations.cityName == textBoxCity.Text && s.addressInformations.stateCode == textBoxState.Text && s.addressInformations.country == textBoxCountry.Text && s.addressInformations.zipCode == textBoxZipCode.Text);
            ContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();


            if (customerContactInformation is null)
            {
                customerContactInformation = AddCustomerContactInformation();
            }
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddAdditionalServices_Click(object sender, EventArgs e)
        {
            addAdditionalService();

        }

        private void buttonAddCustomerReferenceNumber_Click(object sender, EventArgs e)
        {
            addCustomerReferenceNumber();
        }

        private void addCustomerReferenceNumber()
        {
            IList<CustomerReferenceNumber> customerReferenceNumbers = quoteRequest.referenceNumbers;

            CustomerReferenceNumber customerReferenceNumber = customerReferenceNumbers.Where(s => s.typeCode == comboBoxCustomerReferenceNumberTypeCode.Text).FirstOrDefault();

            if (customerReferenceNumber is null)
            {
                customerReferenceNumber = new CustomerReferenceNumber();
                customerReferenceNumbers.Add(customerReferenceNumber);
                comboBoxCustomerReferenceNumbers.Items.Add(comboBoxCustomerReferenceNumberTypeCode.Text);
            }

            customerReferenceNumber.typeCode = comboBoxCustomerReferenceNumberTypeCode.Text;
            customerReferenceNumber.value = textBoxContactReferenceNumberValue.Text;



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
                return;
            //    updateGeneralQuoteInfo();

            XPO xpo = new XPO();
            //xpo.createToken();
            quoteResponse = await xpo.getQuote(quoteRequest);
            //quoteResponse = await xpo.getQuote();




            IList<PriceResponse> priceSearchResponse = quoteResponse.priceSearchResponse;
            if (priceSearchResponse != null)
            {
                foreach (PriceResponse priceResponse in priceSearchResponse)
                {
                    Quote lowestpriceQuote = priceResponse.lowestPriceQuote;
                    if (lowestpriceQuote is not null)
                    {
                        TabPage lowestPriceTabPage = lowestpriceQuote.addTab("Lowest Price Quote");
                        tabControlQuotes.TabPages.Add(lowestPriceTabPage);
                    }
                    Quote lowestGuranteedpriceQuote = priceResponse.lowestGuaranteedQuotePrice;
                    if (lowestGuranteedpriceQuote is not null)
                    {
                        TabPage lowestGuranteedPriceTabPage = lowestpriceQuote.addTab("Lowest Guaranteed Price Quote");
                        tabControlQuotes.TabPages.Add(lowestGuranteedPriceTabPage);
                    }
                    IList<Quote> quotes = priceResponse.quoteDetails;
                    if (quotes.Count > 0)
                    {
                        foreach (Quote quote in quotes)
                        {
                            TabPage tabPageQuote = quote.addTab(quote.carrierName);
                            tabControlQuotes.TabPages.Add(tabPageQuote);
                        }
                    }
                }
                tabControlMain.SelectedIndex = 7;
            }
            else
            {
                TabPage tabPageException = new TabPage("Exception");
                tabPageException.Name = "tabQuoteException";

                Label lblException = new Label();
                lblException.Name = "lblException";
                lblException.AutoSize = true;
                lblException.Location = new Point(20, 20);
                lblException.Text = quoteResponse.message;
                tabPageException.Controls.Add(lblException);
                tabControlQuotes.TabPages.Add(tabPageException);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            XPO xpo = new XPO();

            TextBox txtTrackingNumber = tabPageGeneral.Controls["textBoxTrackingNumber"] as TextBox;
            if (txtTrackingNumber.Text.Trim() == "")
            {
                MessageBox.Show("Please provide the tracking Number");
                tabControlMain.SelectedIndex = 1;
                txtTrackingNumber.Focus();
                return;
            }
            orderResponse = await xpo.convertToOrder(tabControlQuotes.SelectedTab, quoteRequest, txtTrackingNumber.Text);
            if (orderResponse is not null)
            {
                Label lblQuoteIdValue = tabControlQuotes.SelectedTab.Controls["lblQuoteIdValue"] as Label;
                lblQuoteId.Text = lblQuoteIdValue.Text;
                Label lblCarrierValue = tabControlQuotes.SelectedTab.Controls["lblCarrierNameValue"] as Label;
                lblCarrier.Text = lblCarrierValue.Text;
                Label lblQuotedAmountValue = tabControlQuotes.SelectedTab.Controls["lblTotalCostValue"] as Label;
                lblQuotedAmount.Text = lblQuotedAmountValue.Text;

                int orderId = orderResponse.orderId;
                lblOrderId.Text = orderId.ToString();
                tabControlMain.SelectedIndex = 8;
                if (orderId == 0)
                {
                    labelExceptionConvertToOrder.Visible = true;
                    labelExceptionConvertToOrder.Text = orderResponse.message;
                }
                else
                {
                    labelExceptionConvertToOrder.Visible = false;
                }
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxStops_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedStopValue = comboBoxStops.SelectedItem.ToString();
            if (selectedStopValue == "NEW...")
            {
                resetStopControls();
            }
            else
            {
                String[] stopsValues = selectedStopValue.Split(",");
                IList<Stop> stops = quoteRequest.stops;

                Stop stop = stops.Where(s => s.addressInformations.cityName == stopsValues[0] && s.addressInformations.stateCode == stopsValues[1] && s.addressInformations.country == stopsValues[2] && s.addressInformations.zipCode == stopsValues[3]).FirstOrDefault();


                if (stop is not null)
                {
                    textBoxStopCity.Text = stopsValues[0];
                    textBoxStopState.Text = stopsValues[1];
                    textBoxStopCountry.Text = stopsValues[2];
                    textBoxStopZipCode.Text = stopsValues[3];
                    dateTimePickerScheduledTimeTo.Value = DateTime.Parse(stop.scheduledTimeTo);
                    dateTimePickerScheduledTimeFrom.Value = DateTime.Parse(stop.scheduledTimeFrom);
                    comboBoxStopType.SelectedItem = stop.type;
                    numericUpDownSequenceNo.Value = stop.sequenceNo;
                    AddressInformation addressInformation = stop.addressInformations;
                    if (addressInformation is not null)
                    {
                        textBoxStopLocationId.Text = addressInformation.locationId;
                        textBoxStopLocationName.Text = addressInformation.locationName;
                        textBoxStopAddress1.Text = addressInformation.addressLine1;
                        textBoxStopAddress2.Text = addressInformation.addressLine2;
                        textBoxStopCity.Text = addressInformation.cityName;
                        textBoxStopState.Text = addressInformation.stateCode;
                        textBoxStopCountry.Text = addressInformation.country;
                        textBoxStopZipCode.Text = addressInformation.zipCode;
                    }
                    IList<StopSpecialRequirement> stopSpecialRequirements = stop.specialRequirement;
                    int indexSpecialRequirements = 0;
                    foreach (StopSpecialRequirement stopSpecialRequirement in stopSpecialRequirements)
                    {
                        indexSpecialRequirements++;
                        comboBoxStopSpecialRequirement.Items.Add(comboBoxSpecialRequirementCode.Text);
                        if (indexSpecialRequirements == 1)
                        {
                            textBoxSpecialRequirementValue.Text = stopSpecialRequirement.value;
                            comboBoxSpecialRequirementCode.SelectedItem = stopSpecialRequirement.code;
                        }
                    }
                    IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceNumbers;
                    int indexStopReferenceTypeCodes = 0;
                    foreach (StopReferenceTypeCode stopReferenceTypeCode in stopReferenceTypeCodes)
                    {
                        indexStopReferenceTypeCodes++;
                        comboBoxStopSpecialRequirement.Items.Add(comboBoxSpecialRequirementCode.Text);
                        if (indexStopReferenceTypeCodes == 1)
                        {
                            textBoxStopReferenceTypeCodeValue.Text = stopReferenceTypeCode.value;
                            comboBoxStopRefernceTypeCode.SelectedItem = stopReferenceTypeCode.typeCode;
                        }
                    }
                    IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
                    comboBoxStopContact.Items.Clear();
                    comboBoxStopContact.Items.Add("NEW...");
                    int indexContacts = 0;
                    foreach (StopContactInformation contactInformation in stopContactInformations)
                    {
                        indexContacts++;
                        comboBoxStopContact.Items.Add(contactInformation.firstName + "," + contactInformation.lastName + "," + contactInformation.email);
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
                        }
                    }
                }
            }
        }

        private void comboBoxCustomerRefernceNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedCustomerRefernceNumberValue = comboBoxStopRefernceNumbers.SelectedValue.ToString();
            IList<Stop> stops = quoteRequest.stops;

            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();


            if (stop is not null)
            {
                IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceNumbers;
                StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == selectedCustomerRefernceNumberValue).FirstOrDefault();

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
                resetStopContactInformationControls();
            }
            else
            {
                IList<Stop> stops = quoteRequest.stops;


                Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();


                if (stop is not null)
                {
                    String[] selectedStopContactInformationValues = selectedStopContactInformation.Split(",");
                    IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
                    StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == selectedStopContactInformationValues[2]).FirstOrDefault();

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

                    }
                }
            }
        }

        private void comboBoxCustomerConact_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedCustomerContactInformation = comboBoxCustomerConact.SelectedItem.ToString();
            if (selectedCustomerContactInformation == "NEW...")
            {


            }
            else
            {

                String[] selectedCustomerContactInformationValues = selectedCustomerContactInformation.Split(",");
                IList<ContactInformation> contactInformations = quoteRequest.contactInformations;
                ContactInformation contactInformation = contactInformations.Where(s => s.email == selectedCustomerContactInformationValues[2]).FirstOrDefault();

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
                    if (indexPhoneNumbers >= 1)
                        comboBoxCustomerContactPhoneNumber.SelectedIndex = 1;

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


                Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();


                if (stop is not null)
                {
                    IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
                    StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();

                    if (stopContactInformation is not null)
                    {

                        IList<StopContactPhoneNumber> phoneNumbers = stopContactInformation.phoneNumbers;
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
                resetItemsControls();
            }
            else
            {

                String[] selectedItemValues = selectedItem.Split(",");
                IList<QuoteItem> items = quoteRequest.items;
                QuoteItem item = items.Where(s => s.itemNumber == selectedItemValues[0] && s.itemDescription == selectedItemValues[1]).FirstOrDefault();

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

                    if (item.isHazmat)
                        radioButtonHazardousMaterialYes.Checked = true;
                    if (item.isTemperatureControlled)
                        radioButtonTemperatureControlledYes.Checked = true;
                    HazardousItemInfo hazardousItemInfo = item.hazardousItemInfo;
                    if (hazardousItemInfo is not null)
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
        }

        private void comboBoxCustomerReferenceNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedCustomerReferenceNumber = comboBoxCustomerReferenceNumbers.SelectedItem.ToString();
            if (selectedCustomerReferenceNumber == "NEW...")
            {

            }
            else
            {
                IList<CustomerReferenceNumber> customerReferenceNumbers = quoteRequest.referenceNumbers;
                CustomerReferenceNumber customerReferenceNumber = customerReferenceNumbers.Where(s => s.typeCode == selectedCustomerReferenceNumber).FirstOrDefault();

                if (customerReferenceNumber is not null)
                {
                    textBoxContactReferenceNumberValue.Text = customerReferenceNumber.value;
                    comboBoxCustomerReferenceNumberTypeCode.SelectedItem = customerReferenceNumber.typeCode;

                }
            }
        }

        private void buttonDeleteStopContactPhoneNumber_Click(object sender, EventArgs e)
        {

            IList<Stop> stops = quoteRequest.stops;


            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is not null)
            {
                IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;

                StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();


                IList<StopContactPhoneNumber> stopContactPhoneNumbers = stopContactInformation.phoneNumbers;

                StopContactPhoneNumber phoneNumber = stopContactPhoneNumbers.Where(s => s.number == textBoxStopContactPhoneNumber.Text).FirstOrDefault();
                if (phoneNumber is not null)
                {
                    stopContactPhoneNumbers.Remove(phoneNumber);
                    comboBoxStopContactPhoneNumber.Items.Remove(textBoxStopContactPhoneNumber.Text + "," + comboBoxStopContactPhoneNumberType.Text);
                    resetStopContactPhoneNumberControls();
                }
            }
        }

        private void buttonDeleteStopContact_Click(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;

            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is not null)
            {
                IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;

                StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();

                if (stopContactInformation is not null)
                {
                    stopContactInformations.Remove(stopContactInformation);
                    comboBoxStopContact.Items.Remove(textBoxStopContactFirstName.Text + "," + textBoxStopContactLastName.Text + "," + textBoxStopContactEmail.Text);
                    resetStopContactInformationControls();
                }
            }
        }

        private void buttonDeleteStop_Click(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;


            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is not null)
            {
                stops.Remove(stop);
                comboBoxStops.Items.Remove(textBoxStopCity.Text + "," + textBoxStopState.Text + "," + textBoxStopCountry.Text + "," + textBoxStopZipCode.Text);
                resetStopControls();
            }
        }

        private void buttonDeleteCustomerContactPhoneNumber_Click(object sender, EventArgs e)
        {

            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;

            ContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();
            IList<PhoneNumber> ContactPhoneNumbers = customerContactInformation.phoneNumbers;

            PhoneNumber phoneNumber = ContactPhoneNumbers.Where(s => s.number == textBoxCustomerContactPhoneNumber.Text).FirstOrDefault();
            if (phoneNumber is not null)
            {
                ContactPhoneNumbers.Remove(phoneNumber);
                comboBoxCustomerContactPhoneNumber.Items.Remove(textBoxCustomerContactPhoneNumber.Text + "," + comboBoxCustomerContactPhoneNumberType.Text);
                resetCustomerContactPhoneNumberControls();
            }

        }

        private void buttonDeleteCustomerContact_Click(object sender, EventArgs e)
        {
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;


            ContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();
            if (customerContactInformation is not null)
            {
                customerContactInformations.Remove(customerContactInformation);
                comboBoxCustomerConact.Items.Remove(textBoxCustomerContactFirstName.Text + "," + textBoxCustomerContactLastName.Text + "," + textBoxCustomerContactEmail.Text);
                resetCustomerContactInformationControls();
            }
        }

        private void buttonDeleteCustomerReferenceNumber_Click(object sender, EventArgs e)
        {
            IList<CustomerReferenceNumber> referenceNumbers = quoteRequest.referenceNumbers;

            CustomerReferenceNumber referenceNumber = referenceNumbers.Where(s => s.typeCode == comboBoxCustomerReferenceNumberTypeCode.Text).FirstOrDefault();
            if (referenceNumber is not null)
            {
                referenceNumbers.Remove(referenceNumber);
                comboBoxCustomerReferenceNumbers.Items.Remove(comboBoxCustomerReferenceNumberTypeCode.Text);
            }
        }

        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            IList<QuoteItem> items = quoteRequest.items;

            QuoteItem item = items.Where(s => s.productCode == textBoxItemProductCode.Text && s.itemNumber == textBoxItemNumber.Text && s.itemDescription == textBoxItemDescription.Text).FirstOrDefault();


            if (item is not null)
            {
                items.Remove(item);
                comboBoxItem.Items.Remove(textBoxItemNumber.Text + "," + textBoxItemDescription.Text);
            }
        }

        private void buttonDeleteStopSpecialRequirement_Click(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;


            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();

            IList<StopSpecialRequirement> stopSpecialRequirements = stop.specialRequirement;

            StopSpecialRequirement stopSpecialRequirement = stopSpecialRequirements.Where(s => s.code == comboBoxSpecialRequirementCode.Text).FirstOrDefault();

            if (stopSpecialRequirement is null)
            {
                stopSpecialRequirements.Remove(stopSpecialRequirement);
                comboBoxStopSpecialRequirement.Items.Remove(comboBoxSpecialRequirementCode.Text);
            }
        }

        private void buttonDeleteStopReferenceTypeCodeAdd_Click(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;


            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();


            IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceNumbers;

            StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == comboBoxSpecialRequirementCode.Text).FirstOrDefault();

            if (stopReferenceTypeCode is null)
            {
                stopReferenceTypeCodes.Remove(stopReferenceTypeCode);
                comboBoxStopRefernceNumbers.Items.Remove(comboBoxStopRefernceTypeCode.Text);
            }
        }

        private void comboBoxAdditionalServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedAdditionalService = comboBoxAdditionalServices.SelectedItem.ToString();

            IList<AdditionalService> additionalServices = quoteRequest.additionalServices;
            AdditionalService additionalService = additionalServices.Where(s => s.code == selectedAdditionalService).FirstOrDefault();

            if (additionalService is not null)
            {
                comboBoxAdditionalServiceCode.SelectedItem = selectedAdditionalService;
            }
        }

        private void buttonDeleteAdditionalServices_Click(object sender, EventArgs e)
        {
            IList<AdditionalService> additionalServices = quoteRequest.additionalServices;


            AdditionalService additionalService = additionalServices.Where(s => s.code == comboBoxAdditionalServiceCode.Text).FirstOrDefault();
            if (additionalService is not null)
            {
                additionalServices.Remove(additionalService);
                comboBoxAdditionalServices.Items.Remove(comboBoxAdditionalServiceCode);
                ;
            }
        }

        private void radioButtonHazardousMaterialNo_CheckedChanged(object sender, EventArgs e)
        {
            resetHazardousMaterials();
        }

        private void radioButtonTemperatureControlledNo_CheckedChanged(object sender, EventArgs e)
        {
            resetTemperatureInformation();
        }

        private void buttonSelectJSONFile_Click(object sender, EventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                DialogResult result = fbd.ShowDialog();
                labelSelectedJSONSampleDataFolder.Text = fbd.SelectedPath;
                labelSelectedJSONSampleDataFolder.BackColor = Color.Aqua;
                labelSelectedFileCaption.Visible = true;
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    foreach (String fileName in files)
                    {
                        listBoxSampleJSONFiles.Items.Add(Path.GetFileName(fileName));
                    }
                }
            }
        }

        private void buttonLoadSampleJSONFile_Click(object sender, EventArgs e)
        {

            TextReader tr = File.OpenText(labelSelectedJSONSampleDataFolder.Text + "\\" + listBoxSampleJSONFiles.SelectedItem);
            JsonTextReader reader = new JsonTextReader(tr);
            JsonSerializer jseri = new JsonSerializer();

            quoteRequest = jseri.Deserialize<QuoteRequest>(reader);
            LoadSampleJSONData();

        }

        private void tabPageStops_Click(object sender, EventArgs e)
        {

        }

        private void buttonSaveCustomerContact_Click(object sender, EventArgs e)
        {
            if (!validateCustomerContact()) return;
            AddCustomerContactInformation();
        }

        private void buttonSaveStopContact_Click(object sender, EventArgs e)
        {
            if (!ValidateStopAddress())
            {
                return;
            }
            if (!validateStopContact())
            {
                return;
            }


            Stop stop = getStop();
            addStopContactInformation(stop);
        }

        private void buttonSaveGeneralQuoteInfo_Click(object sender, EventArgs e)
        {
            if (!ValidateQuoteGeneralSettings())
                return;
            updateGeneralQuoteInfo();
        }

        private void updateGeneralQuoteInfo()
        {
            quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;

            quoteRequest.partnerOrderCode = textBoxPartnerOrderCode.Text;

            quoteRequest.equipmentCategoryCode = comboBoxEquipmentCategoryCode.Text;

            quoteRequest.equipmentTypeCode = comboBoxEquipmentTypeCode.Text;


            quoteRequest.bolNumber = textBoxBOLNumber.Text;

            quoteRequest.shipmentId = textBoxShipmentId.Text;

            quoteRequest.applicationSource = comboBoxApplicationSource.Text;
        }

        private void buttonSaveStopAddress_Click(object sender, EventArgs e)
        {
            Stop stop = getStop();
            if (stop is null)
            {
                stop = addStop();
            }
            addStopAddressInformation(stop);
        }

        private void buttonUpdateStop_Click(object sender, EventArgs e)
        {
            addStop();
        }

        private void buttonSelectJSONFile_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonSaveStopReferenceTypeCode_Click(object sender, EventArgs e)
        {
            Stop stop = addStop();

            if (stop is null)
            {
                stop = addStop();
            }
            AddStopReferenceTypeCode();
        }

        private void buttonLoadSampleJSONFile_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonGetQuote_Click_1(object sender, EventArgs e)
        {

        }

        private  async void buttonConvertToOrder_Click(object sender, EventArgs e)
        {
            XPO xpo = new XPO();

            TextBox txtTrackingNumber = tabPageGeneral.Controls["textBoxTrackingNumber"] as TextBox;
            if (txtTrackingNumber.Text.Trim() == "")
            {
                MessageBox.Show("Please provide the tracking Number");
                tabControlMain.SelectedIndex = 1;
                txtTrackingNumber.Focus();
                return;
            }
            orderResponse = await xpo.convertToOrder(tabControlQuotes.SelectedTab, quoteRequest, txtTrackingNumber.Text);
            if (orderResponse is not null)
            {
                Label lblQuoteIdValue = tabControlQuotes.SelectedTab.Controls["lblQuoteIdValue"] as Label;
                lblQuoteId.Text = lblQuoteIdValue.Text;
                Label lblCarrierValue = tabControlQuotes.SelectedTab.Controls["lblCarrierNameValue"] as Label;
                lblCarrier.Text = lblCarrierValue.Text;
                Label lblQuotedAmountValue = tabControlQuotes.SelectedTab.Controls["lblTotalCostValue"] as Label;
                lblQuotedAmount.Text = lblQuotedAmountValue.Text;

                int orderId = orderResponse.orderId;
                lblOrderId.Text = orderId.ToString();
                tabControlMain.SelectedIndex = 8;
                if (orderId == 0)
                {
                    labelExceptionConvertToOrder.Visible = true;
                    labelExceptionConvertToOrder.Text = orderResponse.message;
                }
                else
                {
                    labelExceptionConvertToOrder.Visible = false;
                }
            }
        }
    }
}
