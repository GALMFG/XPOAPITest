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
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using RestSharp;

using Serilog;

namespace XPOAPITest
{
    public partial class FormUserInterface : Form
    {
        QuoteResponse quoteResponse;
        QuoteRequest quoteRequest;

        OrderResponse orderResponse;
        OrderRequest orderRequest;
        String selectedFileName;
        String outputFileName;
        String selectedPath;
        String xpoToken;
        FixedLists fixedLists;
        public FormUserInterface()
        {

            InitializeComponent();
        }
            
        private void FormUserInterface_Load(object sender, EventArgs e)
        {
            quoteRequest = new QuoteRequest();
            labelSelectedOutputFolderCaption.Text=@"C:\temp";
            IntializeDropdownLists();
            getConfigData();
            InitializeTimePicker();
        }

        public void ApplyConfigSettings()
        {
            quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
        }
        public void SaveSampleJSONData()
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(quoteRequest);
            if (!File.Exists(outputFileName))
                File.Create(outputFileName).Dispose();
            File.WriteAllText(outputFileName, jsonString);

            //using (StreamWriter file = File.CreateText(outputFileName))
            //{
            //    //JsonSerializer serializer = new JsonSerializer();
            //    //serializer.Serialize(file, quoteRequest);


            //    string jsonString = System.Text.Json.JsonSerializer.Serialize(quoteRequest);
            //    File.WriteAllText(outputFileName, jsonString);

            //}
        }
            public void LoadSampleJSONData()
        {


            string jsonString = File.ReadAllText(labelSelectedJSONSampleDataFolder.Text + "\\" + listBoxSampleJSONFiles.SelectedItem);
            quoteRequest = JsonSerializer.Deserialize<QuoteRequest>(jsonString)!;
            if (quoteRequest is null)
            {
                MessageBox.Show("File Did not contain valid JSON data");
                return;
            }

            string[] files = Directory.GetFiles(labelSelectedOutputFolderCaption.Text, "*.json");
            selectedFileName = listBoxSampleJSONFiles.SelectedItem.ToString();
            // string pattern = "Sales_[0-9]{6}\\.json";
            string inputFilepattern = "(Ver)[0-9]*[0-9]*\\.";
            string outputFilepattern = "[0-9]*[0-9]*";
            string selectedFilePattern = selectedFileName.Replace(".", inputFilepattern);
            int versionValue = 0;
            foreach (string file in files)
            {
                if (Regex.IsMatch(file, inputFilepattern))
                {
                    foreach (Match match in Regex.Matches(file, inputFilepattern))
                    {
                        String text = match.Value;
                        String versionNumber = text.Substring(3, 2);
                        int number  = Convert.ToInt32(versionNumber);
                        if (number >= versionValue)
                            versionValue = number;
                    }


                    // Found a file that matches!
                }
            }
            String versionText = "";
            versionValue++;
            if (versionValue < 10)
                { if (versionValue == 1)
                        versionText = "Ver01.";
                   else
                        versionText = "Ver0" + versionValue.ToString()+".";
                }           
            else
                versionText = "Ver" +versionValue.ToString() + "\\.";
            outputFileName = labelSelectedOutputFolderCaption.Text + "\\" + Regex.Replace(selectedFileName, "\\.", versionText);


            loadGeneralControls();
            loadStopControls();
            loadCustomerContactInformationControls();
            loadCustomerReferenceControls();
            loadCustomerAdditionalServicesControls();
            loadItemsControls();


            //using (TextReader tr = File.OpenText(labelSelectedJSONSampleDataFolder.Text + "\\" + listBoxSampleJSONFiles.SelectedItem))
            ////TextReader tr = File.OpenText(labelSelectedJSONSampleDataFolder.Text + "\\" + listBoxSampleJSONFiles.SelectedItem);
            //{
            //    JsonTextReader reader = new JsonTextReader(tr);
            //    JsonSerializer jseri = new JsonSerializer();
            //    SelectedPath = labelSelectedJSONSampleDataFolder.Text;
            //    selectedFileName = listBoxSampleJSONFiles.SelectedItem.ToString();
            //    quoteRequest = jseri.Deserialize<QuoteRequest>(reader);
            //    if (quoteRequest is null)
            //    {
            //        MessageBox.Show("File Did not contain valid JSON data");
            //        return;
            //    }
            //    loadGeneralControls();
            //    loadStopControls();
            //    loadCustomerContactInformationControls();
            //    loadCustomerReferenceControls();
            //    loadCustomerAdditionalServicesControls();
            //    loadItemsControls();
            //}
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
                comboBoxCustomerConact.SelectedIndex = 1;

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
            comboBoxItem.Items.Clear();
            comboBoxItem.Items.Add("NEW...");
            foreach (QuoteItem item in quoteRequest.items)
            {
                counterItems++;
                comboBoxItem.Items.Add(item.itemNumber + "," + item.itemDescription);
                if (counterItems == 1)
                {
                    textBoxItemClass.Text = item.Class;
                    textBoxItemProductCode.Text = item.productCode;
                    textBoxItemDescription.Text = item.itemDescription;
                    textBoxItemNumber.Text = item.itemNumber;
                    textBoxItemUnits.Text = item.units.ToString();
                    comboBoxItemUnitTypeCode.Text = item.unitTypeCode;
                    textBoxPackageUnits.Text = item.packageUnits.ToString();
                    comboBoxPackageTypeCode.Text = item.packageTypeCode;
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
            if (counterItems >= 1)
            {
                comboBoxItem.SelectedIndex = 1;
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

        private TabPage addTab(Quote quote, String tabName)
        {
            TabPage tabPageQuote = new TabPage(tabName);
            tabPageQuote.Name = "tabQuote" + quote.carrierName.Replace(",", "");

            Label lblCarrierName = new Label();
            lblCarrierName.Name = "lblCarrierName";
            lblCarrierName.Size = new Size(190, 30);
            lblCarrierName.Location = new Point(20, 20);
            lblCarrierName.Text = "Carrier Name";
            tabPageQuote.Controls.Add(lblCarrierName);

            if (quote.carrierName != null)
            {
                Label lblCarrierNameValue = new Label();
                lblCarrierNameValue.Size = new Size(140, 30);
                lblCarrierNameValue.Location = new Point(210, 20);
                lblCarrierNameValue.Text = quote.carrierName;
                lblCarrierNameValue.Name = "lblCarrierNameValue";
                tabPageQuote.Controls.Add(lblCarrierNameValue);
            }

            Label lblQuoteId = new Label();
            lblQuoteId.Size = new Size(190, 30);
            lblQuoteId.Location = new Point(470, 20);
            lblQuoteId.Text = "Quote Id";
            tabPageQuote.Controls.Add(lblQuoteId);
            if (quote.quoteId != null)
            {
                Label lblQuoteIdValue = new Label();
                lblQuoteIdValue.Size = new Size(140, 30);
                lblQuoteIdValue.Location = new Point(670, 20);
                lblQuoteIdValue.Text = quote.quoteId;
                lblQuoteIdValue.Name = "lblQuoteIdValue";
                tabPageQuote.Controls.Add(lblQuoteIdValue);
            }
            Label lblPickupDate = new Label();
            lblPickupDate.Size = new Size(190, 30);
            lblPickupDate.Location = new Point(20, 60);
            lblPickupDate.Text = "Pickup Date";
            tabPageQuote.Controls.Add(lblPickupDate);
            if (quote.pickupDate != null)
            {
                Label lblPickupDateValue = new Label();
                lblPickupDateValue.Size = new Size(140, 30);
                lblPickupDateValue.Location = new Point(210, 60);
                lblPickupDateValue.Text = quote.pickupDate;
                tabPageQuote.Controls.Add(lblPickupDateValue);
            }
            Label lblDeliveryDate = new Label();
            lblDeliveryDate.Size = new Size(190, 30);
            lblDeliveryDate.Location = new Point(470, 60);
            lblDeliveryDate.Text = "Delivery Date";
            tabPageQuote.Controls.Add(lblDeliveryDate);
            if (quote.deliveryDate != null)
            {
                Label lblDeliveryValue = new Label();
                lblDeliveryValue.Size = new Size(140, 30);
                lblDeliveryValue.Location = new Point(670, 60);
                lblDeliveryValue.Text = quote.deliveryDate;
                tabPageQuote.Controls.Add(lblDeliveryValue);
            }
            Label lblEstimatedTransitTime = new Label();
            lblEstimatedTransitTime.Size = new Size(190, 30);
            lblEstimatedTransitTime.Location = new Point(20, 100);
            lblEstimatedTransitTime.Text = "Estimated Transit Time";
            tabPageQuote.Controls.Add(lblEstimatedTransitTime);

            Label lblEstimatedTransitTimeValue = new Label();
            lblEstimatedTransitTimeValue.Size = new Size(140, 30);
            lblEstimatedTransitTimeValue.Location = new Point(210, 100);
            lblEstimatedTransitTimeValue.Text = quote.estimatedTransitTime.ToString().Trim();
            tabPageQuote.Controls.Add(lblEstimatedTransitTimeValue);

            Label lblFSC = new Label();
            lblFSC.Size = new Size(190, 30);
            lblFSC.Location = new Point(20, 140);
            lblFSC.Text = "FSC";
            tabPageQuote.Controls.Add(lblFSC);

            Label lblFSCValue = new Label();
            lblFSCValue.Size = new Size(140, 30);
            lblFSCValue.Location = new Point(210, 140);
            lblFSCValue.Text = quote.fsc.ToString().Trim();
            tabPageQuote.Controls.Add(lblFSCValue);

            Label lblSCAC = new Label();
            lblSCAC.Size = new Size(190, 30);
            lblSCAC.Location = new Point(470, 140);
            lblSCAC.Text = "SCAC";
            tabPageQuote.Controls.Add(lblSCAC);
            if (quote.scac != null)
            {
                Label lblSCACValue = new Label();
                lblSCACValue.Size = new Size(140, 30);
                lblSCACValue.Location = new Point(670, 140);
                lblSCACValue.Text = quote.scac.ToString().Trim();
                tabPageQuote.Controls.Add(lblSCACValue);
            }
            Label lblLineHaul = new Label();
            lblLineHaul.Size = new Size(190, 30);
            lblLineHaul.Location = new Point(20, 180);
            lblLineHaul.Text = "Line Haul";

            tabPageQuote.Controls.Add(lblLineHaul);

            Label lblLineHaulValue = new Label();
            lblLineHaulValue.Size = new Size(140, 30);
            lblLineHaulValue.Location = new Point(210, 180);
            lblLineHaulValue.Text = quote.lineHaul.ToString().Trim();
            tabPageQuote.Controls.Add(lblLineHaulValue);

            Label lblTotalDistance = new Label();
            lblTotalDistance.Size = new Size(190, 30);
            lblTotalDistance.Location = new Point(470, 180);
            lblTotalDistance.Text = "Total Distance";
            tabPageQuote.Controls.Add(lblTotalDistance);

            Label lblTotalDistanceValue = new Label();
            lblTotalDistanceValue.Size = new Size(140, 30);
            lblTotalDistanceValue.Location = new Point(670, 180);
            lblTotalDistanceValue.Text = quote.totalDistance.ToString().Trim();
            tabPageQuote.Controls.Add(lblTotalDistanceValue);

            Label lblMovementType = new Label();
            lblMovementType.Size = new Size(190, 30);
            lblMovementType.Location = new Point(20, 220);
            lblMovementType.Text = "Movement Type";
            tabPageQuote.Controls.Add(lblMovementType);
            if (quote.movementType != null)
            {
                Label lblMovementTypeValue = new Label();
                lblMovementTypeValue.Size = new Size(140, 30);
                lblMovementTypeValue.Location = new Point(210, 220);
                lblMovementTypeValue.Text = quote.movementType.Trim();
                //lblMovementTypeValue.Text = "DIRECT";
                tabPageQuote.Controls.Add(lblMovementTypeValue);
            }
            Label lblServiceLevel = new Label();
            lblServiceLevel.Size = new Size(190, 30);
            lblServiceLevel.Location = new Point(470, 220);
            lblServiceLevel.Text = "Service Level";
            tabPageQuote.Controls.Add(lblServiceLevel);
            if (quote.serviceLevel != null)
            {
                Label lblServiceLevelValue = new Label();
                lblServiceLevelValue.Size = new Size(140, 30);
                lblServiceLevelValue.Location = new Point(670, 220);
                lblServiceLevelValue.Text = quote.serviceLevel;
                tabPageQuote.Controls.Add(lblServiceLevelValue);
            }
            Label lblSubTotal = new Label();
            lblSubTotal.Size = new Size(190, 30);
            lblSubTotal.Location = new Point(20, 260);
            lblSubTotal.Text = "Sub Total";
            tabPageQuote.Controls.Add(lblSubTotal);

            Label lblSubTotalValue = new Label();
            lblSubTotalValue.Size = new Size(140, 30);
            lblSubTotalValue.Location = new Point(210, 260);
            lblSubTotalValue.Text = quote.subTotal.ToString().Trim();

            tabPageQuote.Controls.Add(lblSubTotalValue);

            Label lblTotalCost = new Label();
            lblTotalCost.Size = new Size(190, 30);
            lblTotalCost.Location = new Point(470, 260);
            lblTotalCost.Text = "Total Cost";

            tabPageQuote.Controls.Add(lblTotalCost);

            Label lblTotalCostValue = new Label();
            lblTotalCostValue.Size = new Size(140, 30);
            lblTotalCostValue.Location = new Point(670, 260);
            lblTotalCostValue.Text = quote.totalCost.ToString().Trim();
            lblTotalCostValue.Name = "lblTotalCostValue";
            tabPageQuote.Controls.Add(lblTotalCostValue);

            RadioButton rdoIsContractRate = new RadioButton();
            rdoIsContractRate.Size = new Size(190, 30);
            rdoIsContractRate.Location = new Point(20, 300);
            rdoIsContractRate.Text = "Is Contract Rate";
            tabPageQuote.Controls.Add(rdoIsContractRate);

            if (quote.isContractRate) { rdoIsContractRate.Checked = true; }

            RadioButton rdoIsNMFCCarrier = new RadioButton();
            rdoIsNMFCCarrier.Size = new Size(190, 30);
            rdoIsNMFCCarrier.Location = new Point(470, 300);
            rdoIsNMFCCarrier.Text = "Is NMFC Carrier";
            tabPageQuote.Controls.Add(rdoIsNMFCCarrier);

            if (quote.isNmfcCarrier)
                rdoIsNMFCCarrier.Checked = true;
            DataGridView gvRateList = new DataGridView();
            gvRateList.Size = new Size(1000, 200);

            gvRateList.Location = new Point(20, 340);


            tabPageQuote.Controls.Add(gvRateList);
            DataGridView gvAccessorialsList = new DataGridView();
            gvAccessorialsList.Size = new Size(1000, 200);
            gvAccessorialsList.Location = new Point(20, 550);

            tabPageQuote.Controls.Add(gvAccessorialsList);

            DataTable dt = new DataTable();

            dt.Columns.Add("Code");
            dt.Columns.Add("Cost");
            dt.Columns.Add("CurrencyCode");
            dt.Columns.Add("IsOptional");
            dt.Columns.Add("Name");
            dt.Columns.Add("TypeCode");


            IList<Rate> rates = quote.rateList;

            foreach (Rate rate in rates)
            {
                var row = dt.NewRow();
                row[0] = rate.code;
                row[1] = rate.cost;
                row[2] = rate.currencyCode;
                row[3] = rate.isOptional;
                row[4] = rate.name;
                row[5] = rate.typeCode;
                dt.Rows.Add(row);
            }

            gvRateList.DataSource = dt;

            gvRateList.AutoSizeColumnsMode =
        DataGridViewAutoSizeColumnsMode.AllCells;


            dt = new DataTable();

            dt.Columns.Add("ACode");
            dt.Columns.Add("Cost");
            dt.Columns.Add("Name");


            IList<Accessorial> accessorials = quote.accessorials;

            foreach (Accessorial accessorial in accessorials)
            {
                var row = dt.NewRow();
                row[0] = accessorial.accessorialCode;
                row[1] = accessorial.accessorialName;
                row[2] = accessorial.accessorialCost;

                dt.Rows.Add(row);
            }

            gvAccessorialsList.DataSource = dt;
            gvAccessorialsList.AutoSizeColumnsMode =
        DataGridViewAutoSizeColumnsMode.AllCells;
            return (tabPageQuote);
        }
        private async void buttonGetQuote_Click(object sender, EventArgs e)
        {
            if (!ValidateQuoteRequestObject())
            {
                return;
            }
            XPO xpo = new XPO();           

            String token = await  xpo.getToken(XPOSettings.XPOConnectURL,XPOSettings.XAPIKeyToken, XPOSettings.ClientId, XPOSettings.ClientSecret, XPOSettings.Scope, XPOSettings.GrantType);
            quoteResponse = await xpo.getQuote(quoteRequest, token, XPOSettings.XPOConnectURL, XPOSettings.XAPIKeyRequest);
            if (quoteResponse is null)
                return;
            if (quoteResponse.errorDetails is not null)
            {
                int counter = 0;
                foreach (String quoteError in quoteResponse.errorDetails)
                    Log.Error("Error # [{Index}] : [{quoteError}]", counter++, quoteError);
            }
            else
                Log.Information("Quote Request was successfull");
            IList<PriceResponse> priceSearchResponse = quoteResponse.priceSearchResponse;
            if (priceSearchResponse != null)
            {
                                
                tabControlQuotes.TabPages.Clear();
                tabControlMain.SelectedIndex = 7;
                foreach (PriceResponse priceResponse in priceSearchResponse)
                {
                    if (priceResponse.lowestPriceQuote is null)
                        Log.Information("The results of Quote request does not contain Lowest Price Quote");
                    else
                    {
                        Quote lowestpriceQuote = priceResponse.lowestPriceQuote;
                        Log.Information("Lowest Price Quote :Carrier [{totalcost}]  Cost [{totalcost}] ", lowestpriceQuote.carrierName, lowestpriceQuote.totalCost);
                        TabPage lowestPriceTabPage = addTab(lowestpriceQuote, "Lowest Price Quote");
                        tabControlQuotes.TabPages.Add(lowestPriceTabPage);                        
                    }
                    if (priceResponse.lowestGuaranteedQuotePrice is null)
                        Log.Information("The results of Quote request does not contain Lowest Quaranteed Price Quote");
                    else
                    {
                        Quote lowestGuranteedpriceQuote = priceResponse.lowestGuaranteedQuotePrice;
                        Log.Information("Lowest Price Quote :Carrier [{totalcost}]  Cost [{totalcost}] ", lowestGuranteedpriceQuote.carrierName, lowestGuranteedpriceQuote.totalCost);
                        TabPage lowestGuranteedPriceTabPage = addTab(lowestGuranteedpriceQuote, "Lowest Guaranteed Price Quote");
                        tabControlQuotes.TabPages.Add(lowestGuranteedPriceTabPage);
                    }
                    if (priceResponse.bestDealQuotePrice is null)
                        Log.Information("The results of Quote request does not contain Best Deal Price Quote");
                    else
                    {
                        Quote bestDealQuotePrice = priceResponse.bestDealQuotePrice;
                        Log.Information("Lowest Price Quote :Carrier [{totalcost}]  Cost [{totalcost}] ", bestDealQuotePrice.carrierName, bestDealQuotePrice.totalCost);
                        TabPage bestDealPriceQuoteTabPage = addTab(bestDealQuotePrice, "Best Deal Price Quote");
                        tabControlQuotes.TabPages.Add(bestDealPriceQuoteTabPage);
                    }
                    IList<Quote> quotes = priceResponse.quoteDetails;
                    int counter=1;
                    foreach (Quote quote in quotes)
                    {
                        Log.Information("Quote  [{totalcost}] Carrier [{totalcost}]  Cost [{totalcost}] ", counter++, quote.carrierName, quote.totalCost);
                        TabPage tabPageQuote = addTab(quote,quote.carrierName);
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
            XPOSettings.ApplicationSource = ConfigurationManager.AppSettings.Get("applicationSource");
        }
        private void InitializeTimePicker()
        {
            dateTimePickerScheduledTimeFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerScheduledTimeFrom.CustomFormat = "yyyy-MM-ddTHH:mm:ss";

            dateTimePickerScheduledTimeTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerScheduledTimeTo.CustomFormat = "yyyy-MM-ddTHH:mm:ss";
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
            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is null)
            {
                stop = new Stop();
                quoteRequest.AddStop(stop);
                comboBoxStops.Items.Add(textBoxStopCity.Text + "," + textBoxStopState.Text + "," + textBoxStopCountry.Text + "," + textBoxStopZipCode.Text);
            }
            stop.type = comboBoxStopType.Text;
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

            QuoteItem item = getItem();

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
            item.Class = textBoxItemClass.Text;
            item.nmfcCode = textBoxItemNMFCCode.Text;
            item.declaredValueAmount = Convert.ToDouble(textBoxItemDeclaredValueAmount.Text);
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

            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            return stop;
        }
        public QuoteItem getItem()
        {
            IList<QuoteItem> items = quoteRequest.items;

            QuoteItem item = items.Where(s => s.productCode == textBoxItemProductCode.Text && s.itemNumber == textBoxItemNumber.Text && s.itemDescription == textBoxItemDescription.Text).FirstOrDefault();
            return item;
        }
        public ContactInformation getCustomerContactInformation()
        {
            IList<ContactInformation> contactInformations = quoteRequest.contactInformations;

            ContactInformation contactInformation = contactInformations.Where(s => s.email ==  textBoxCustomerContactEmail.Text).FirstOrDefault();
            return contactInformation;
        }
        public PhoneNumber getCustomerContactPhoneNumber(ContactInformation customerContactInformation)
        {
            IList<PhoneNumber> customerContactPhoneNumbers = customerContactInformation.phoneNumbers;

            PhoneNumber phoneNumber = customerContactPhoneNumbers.Where(s => s.number == textBoxCustomerContactPhoneNumber.Text).FirstOrDefault();
            return phoneNumber;
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
            SaveSampleJSONData();
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
            addItem(quoteRequest);

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

            ContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();


            if (customerContactInformation is null)
            {
                customerContactInformation = AddCustomerContactInformation();
            }
            AddCustomerContactPhoneNumber(customerContactInformation);
        }

        private void buttonCustomerContact_Click(object sender, EventArgs e)
        {
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;

            ContactInformation customerContactInformation = customerContactInformations.Where(s => s.firstName == textBoxCustomerContactFirstName.Text && s.lastName == textBoxCustomerContactLastName.Text && s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();
            if (customerContactInformation is null)
            {
                customerContactInformation = AddCustomerContactInformation();
            }
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
                    comboBoxItemWeightUOMCode.Text = item.weightUomCode;

                    textBoxItemHeight.Text = item.height.ToString();
                    comboBoxItemHeightUOMCode.Text = item.heightUomCode;

                    textBoxItemLength.Text = item.length.ToString();
                    comboBoxItemLengthUOMCode.SelectedItem = item.lengthUomCode;
                    textBoxItemWidth.Text = item.width.ToString();
                    comboBoxItemWidthUOMCode.SelectedItem = item.widthUomCode;


                    textBoxItemProductCode.Text = item.productCode;
                    textBoxItemDescription.Text = item.itemDescription;
                    textBoxItemNumber.Text = item.itemNumber;
                    textBoxItemUnits.Text = item.units.ToString();
                    comboBoxItemUnitTypeCode.Text = item.unitTypeCode;
                    textBoxPackageUnits.Text = item.packageUnits.ToString();
                    comboBoxPackageTypeCode.Text = item.packageTypeCode;
                    textBoxItemWeight.Text = item.weight.ToString();
                    comboBoxItemWeightUOMCode.SelectedItem = item.weightUomCode;
                    textBoxItemHeight.Text = item.height.ToString();
                    comboBoxItemHeightUOMCode.SelectedItem = item.heightUomCode;
                    textBoxItemLength.Text = item.length.ToString();
                    comboBoxItemLengthUOMCode.SelectedItem = item.lengthUomCode;
                    textBoxItemWidth.Text = item.width.ToString();
                    comboBoxItemWidthUOMCode.SelectedItem = item.widthUomCode;
                    textBoxItemClass.Text = item.Class;


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
                    selectedPath = fbd.SelectedPath;
                    foreach (String fileName in files)
                    {
                        listBoxSampleJSONFiles.Items.Add(Path.GetFileName(fileName));
                    }
                }
            }
        }

        private void buttonLoadSampleJSONFile_Click(object sender, EventArgs e)
        {
            LoadSampleJSONData();
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
            SaveSampleJSONData();
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

        private void buttonSaveStopReferenceTypeCode_Click(object sender, EventArgs e)
        {
            if (!ValidateStopAddress())
            {
                return;
            }
            Stop stop = addStop();
            if (stop is null)
            {
                return;
            }
            AddStopReferenceTypeCode();
            SaveSampleJSONData();
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
            OrderRequest orderRequest = new();
            orderRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;

            orderRequest.transportationMode = XPOSettings.TransportationMode;
            orderRequest.applicationSource = XPOSettings.ApplicationSource;
            Label lblQuoteIdValue = tabControlQuotes.SelectedTab.Controls["lblQuoteIdValue"] as Label;
            String selectedQuoteId = lblQuoteIdValue.Text;

            orderRequest.quoteId = selectedQuoteId;
            orderResponse = await xpo.ConvertToOrder(orderRequest, txtTrackingNumber.Text,xpoToken,XPOSettings.XPOConnectURL, XPOSettings.XAPIKeyRequest);

            if (orderResponse is null)
                Log.Information("Request to Convert Quote to Order was successfull");
            else
            {
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
                    Log.Information("Order Id :[{orderid}]", orderId);
                }
            }
        }
        private void buttonSaveAdditionalServices_Click(object sender, EventArgs e)
        {
            addAdditionalService();
            SaveSampleJSONData();
        }

        private void buttonSaveCustomerReferenceNumber_Click(object sender, EventArgs e)
        {
            addCustomerReferenceNumber();
        }

        private void buttonSaveCustomerContact_Click_1(object sender, EventArgs e)
        {
            if (!validateCustomerContact())
            {
                return;
            }

            Stop stop = getStop();
            ContactInformation customerContactInformation = getCustomerContactInformation();
            if (customerContactInformation is null)
                customerContactInformation = AddCustomerContactInformation();
            SaveSampleJSONData();
            SaveSampleJSONData();
        }

        private void buttonSaveCustomerContactPhoneNumber_Click(object sender, EventArgs e)
        {
            if (!validateCustomerContact())
            {
                return;
            }

            if (textBoxCustomerContactPhoneNumber.Text.Trim() == "")
            {
                MessageBox.Show("Contact Phone Number cannot be empty");
                return;
            }
            IList<ContactInformation> customerContactInformations = quoteRequest.contactInformations;
            ContactInformation customerContactInformation = getCustomerContactInformation();
            if(customerContactInformation is null)
                customerContactInformation=AddCustomerContactInformation();

                AddCustomerContactPhoneNumber(customerContactInformation);
            SaveSampleJSONData();
        }

        private void buttonAddStopSpecialRequirement_Click(object sender, EventArgs e)
        {
            AddStopSpecialRequirement();
        }

        private void buttonSaveItem_Click(object sender, EventArgs e)
        {
            addItem(quoteRequest);
            SaveSampleJSONData();
        }


        private void buttonDeleteStopContactPhoneNumber_Click_1(object sender, EventArgs e)
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
                    SaveSampleJSONData();
                }
            }
        }

        private void buttonDeleteStopContact_Click_1(object sender, EventArgs e)
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
                    SaveSampleJSONData();
                }
            }
        }

        private void buttonDeleteStop_Click_1(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;


            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();
            if (stop is not null)
            {
                stops.Remove(stop);
                comboBoxStops.Items.Remove(textBoxStopCity.Text + "," + textBoxStopState.Text + "," + textBoxStopCountry.Text + "," + textBoxStopZipCode.Text);
                resetStopControls();
                SaveSampleJSONData();
            }
        }

        private void buttonDeleteCustomerContactPhoneNumber_Click_1(object sender, EventArgs e)
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

        private void buttonDeleteCustomerContact_Click_1(object sender, EventArgs e)
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

        private void buttonDeleteCustomerReferenceNumber_Click_1(object sender, EventArgs e)
        {
            IList<CustomerReferenceNumber> referenceNumbers = quoteRequest.referenceNumbers;

            CustomerReferenceNumber referenceNumber = referenceNumbers.Where(s => s.typeCode == comboBoxCustomerReferenceNumberTypeCode.Text).FirstOrDefault();
            if (referenceNumber is not null)
            {
                referenceNumbers.Remove(referenceNumber);
                comboBoxCustomerReferenceNumbers.Items.Remove(comboBoxCustomerReferenceNumberTypeCode.Text);
            }
        }

        private void buttonDeleteItem_Click_1(object sender, EventArgs e)
        {
            IList<QuoteItem> items = quoteRequest.items;

            QuoteItem item = items.Where(s => s.productCode == textBoxItemProductCode.Text && s.itemNumber == textBoxItemNumber.Text && s.itemDescription == textBoxItemDescription.Text).FirstOrDefault();

            if (item is not null)
            {
                items.Remove(item);
                comboBoxItem.Items.Remove(textBoxItemNumber.Text + "," + textBoxItemDescription.Text);
                SaveSampleJSONData();
            }
        }

        private void buttonDeleteStopSpecialRequirement_Click_1(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;

            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();

            IList<StopSpecialRequirement> stopSpecialRequirements = stop.specialRequirement;

            StopSpecialRequirement stopSpecialRequirement = stopSpecialRequirements.Where(s => s.code == comboBoxSpecialRequirementCode.Text).FirstOrDefault();

            if (stopSpecialRequirement is null)
            {
                stopSpecialRequirements.Remove(stopSpecialRequirement);
                comboBoxStopSpecialRequirement.Items.Remove(comboBoxSpecialRequirementCode.Text);
                SaveSampleJSONData();
            }
        }

        private void buttonDeleteStopReferenceTypeCode_Click(object sender, EventArgs e)
        {
            IList<Stop> stops = quoteRequest.stops;

            Stop stop = stops.Where(s => s.addressInformations.cityName == textBoxStopCity.Text && s.addressInformations.stateCode == textBoxStopState.Text && s.addressInformations.country == textBoxStopCountry.Text && s.addressInformations.zipCode == textBoxStopZipCode.Text).FirstOrDefault();

            IList<StopReferenceTypeCode> stopReferenceTypeCodes = stop.stopReferenceNumbers;

            StopReferenceTypeCode stopReferenceTypeCode = stopReferenceTypeCodes.Where(s => s.typeCode == comboBoxSpecialRequirementCode.Text).FirstOrDefault();

            if (stopReferenceTypeCode is null)
            {
                stopReferenceTypeCodes.Remove(stopReferenceTypeCode);
                comboBoxStopRefernceNumbers.Items.Remove(comboBoxStopRefernceTypeCode.Text);
                SaveSampleJSONData();
            }
        }

        private void buttonSaveGeneralQuoteInfo_Click_1(object sender, EventArgs e)
        {
            if (!ValidateQuoteGeneralSettings())
                return;
            updateGeneralQuoteInfo();
            SaveSampleJSONData();
        }

        private void comboBoxAdditionalServices_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            String selectedAdditionalService = comboBoxAdditionalServices.SelectedItem.ToString();

            IList<AdditionalService> additionalServices = quoteRequest.additionalServices;
            AdditionalService additionalService = additionalServices.Where(s => s.code == selectedAdditionalService).FirstOrDefault();

            if (additionalService is not null)
            {
                comboBoxAdditionalServiceCode.SelectedItem = selectedAdditionalService;
            }
        }

        private void buttonDeleteAdditionalServices_Click_1(object sender, EventArgs e)
        {
            IList<AdditionalService> additionalServices = quoteRequest.additionalServices;

            AdditionalService additionalService = additionalServices.Where(s => s.code == comboBoxAdditionalServiceCode.Text).FirstOrDefault();
            if (additionalService is not null)
            {
                additionalServices.Remove(additionalService);
                comboBoxAdditionalServices.Items.Remove(comboBoxAdditionalServiceCode);
            }
        }

        private void radioButtonHazardousMaterialNo_CheckedChanged_1(object sender, EventArgs e)
        {
            resetHazardousMaterials();
        }

        private void radioButtonTemperatureControlledNo_CheckedChanged_1(object sender, EventArgs e)
        {
            resetTemperatureInformation();
        }

        private void buttonSaveStop_Click(object sender, EventArgs e)
        {
            addStop();
            SaveSampleJSONData();
        }

        private void buttonSaveStopAddress_Click_1(object sender, EventArgs e)
        {
            Stop stop = getStop();
            if (stop is null)
            {
                stop = addStop();
            }
            addStopAddressInformation(stop);
            SaveSampleJSONData();
        }

        private void comboBoxCustomerReferenceNumbers_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void comboBoxStopContact_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            String selectedStopContactInformation = comboBoxStopContact.SelectedItem.ToString();
            if (selectedStopContactInformation == "NEW...")
            {
                resetStopContactInformationControls();
            }
            else
            {
                Stop stop = getStop();

                if (stop is not null)
                {
                    String[] selectedStopContactInformationValues = selectedStopContactInformation.Split(",");
                    IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
                    if (stopContactInformations is null)
                        return;
                    StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == selectedStopContactInformationValues[2]).FirstOrDefault();

                    if (stopContactInformation is null)
                        return;
                   
                    textBoxStopContactEmail.Text = stopContactInformation.email;
                    textBoxStopContactFirstName.Text = stopContactInformation.firstName;
                    textBoxStopContactLastName.Text = stopContactInformation.lastName;
                    textBoxStopContactTitle.Text = stopContactInformation.title;
                    if (stopContactInformation.isPrimary)
                        radioButtonStopContactPrimaryYes.Checked = true;
                    IList<StopContactPhoneNumber> phoneNumbers = stopContactInformation.phoneNumbers;
                    comboBoxStopContactPhoneNumber.Items.Clear();
                    comboBoxStopContactPhoneNumber.Items.Add("NEW...");
                    if (phoneNumbers is null)
                        return;
                    int indexPhoneNumbers = 0;
                    foreach (StopContactPhoneNumber phoneNumber in phoneNumbers)
                    {
                        indexPhoneNumbers++;
                        comboBoxStopContactPhoneNumber.Items.Add(phoneNumber.number + "," + phoneNumber.type);
                        if (indexPhoneNumbers == 1)
                        {
                            textBoxStopContactPhoneNumber.Text = phoneNumber.number;
                            comboBoxStopContactPhoneNumberType.SelectedItem = phoneNumber.type;
                            if (phoneNumber.isPrimary)
                                radioButtonStopContactPhoneNumberPrimaryYes.Checked = true;
                        }
                    }
                    if (indexPhoneNumbers >= 1)
                        comboBoxStopContactPhoneNumber.SelectedIndex = 1;
                }
            }
        }

        private void comboBoxCustomerConact_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            String selectedCustomerContactInformation = comboBoxCustomerConact.SelectedItem.ToString();
            if (selectedCustomerContactInformation == "NEW...")
            {
                resetCustomerContactInformationControls();
                return;
            }

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

        private void comboBoxCustomerContactPhoneNumber_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            String selectedCustomerContactPhoneNumber = comboBoxCustomerContactPhoneNumber.SelectedItem.ToString();
            if (selectedCustomerContactPhoneNumber == "NEW...")
            {
                textBoxCustomerContactPhoneNumber.Text = "";
                comboBoxCustomerContactPhoneNumberType.SelectedIndex = -1;
                resetCustomerContactPhoneNumberControls();
                return;
            }

            IList<ContactInformation> contactInformations = quoteRequest.contactInformations;
            ContactInformation contactInformation = contactInformations.Where(s => s.email == textBoxCustomerContactEmail.Text).FirstOrDefault();

            if (contactInformation is null)
                return;
            String[] selectedStopContactPhoneNumberValues = selectedCustomerContactPhoneNumber.Split(",");

            IList<PhoneNumber> phoneNumbers = contactInformation.PhoneNumbers;
            PhoneNumber contactPhoneNumber = phoneNumbers.Where(s => s.number == selectedStopContactPhoneNumberValues[0]).FirstOrDefault();
            textBoxCustomerContactPhoneNumber.Text = contactPhoneNumber.number;
            comboBoxCustomerContactPhoneNumberType.SelectedItem = contactPhoneNumber.type; 
        }

        private void comboBoxStopContactPhoneNumber_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            String selectedStopContactPhoneNumber = comboBoxStopContactPhoneNumber.SelectedItem.ToString();
            if (selectedStopContactPhoneNumber == "NEW...")
            {
                textBoxStopContactPhoneNumber.Text = "";
                comboBoxStopContactPhoneNumberType.SelectedIndex = -1;
                return;
            }
            String[] selectedStopContactPhoneNumberValues = comboBoxStopContactPhoneNumber.SelectedItem.ToString().Split(",");
            if (selectedStopContactPhoneNumberValues.Length == 0)
                return;
            Stop stop = getStop();           
            IList<StopContactInformation> stopContactInformations = stop.stopContactInformations;
            StopContactInformation stopContactInformation = stopContactInformations.Where(s => s.email == textBoxStopContactEmail.Text).FirstOrDefault();

            if (stopContactInformation is null)
                    return;
            IList<StopContactPhoneNumber> phoneNumbers = stopContactInformation.phoneNumbers;
            StopContactPhoneNumber stopContactPhoneNumber = phoneNumbers.Where(s => s.number == selectedStopContactPhoneNumberValues[0]).FirstOrDefault();
            if (stopContactPhoneNumber is null)
                return;
            textBoxStopContactPhoneNumber.Text = stopContactPhoneNumber.number;
            comboBoxStopContactPhoneNumberType.SelectedItem = stopContactPhoneNumber.type;
            if (stopContactPhoneNumber.isPrimary)
                radioButtonStopContactPhoneNumberPrimaryYes.Checked = true;
        }

        private void buttonSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                DialogResult result = fbd.ShowDialog();
                labelSelectedOutputFolderCaption.Text = fbd.SelectedPath;
                labelSelectedOutputFolderCaption.BackColor = Color.Aqua;
                labelSelectedOutputFolderCaption.Visible = true;
            }
        }
    }
}
