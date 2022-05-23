using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class FixedLists
    {
        public FixedLists()
        {
            customerRefereneTypeCodes = new ArrayList();
            stopRefereneTypeCodes = customerRefereneTypeCodes;
            equipmentCatagoryCodes = new ArrayList();
            typeOfStops = new ArrayList();
            paymentMethods = new ArrayList();
            applicationSources = new ArrayList();
            equipmentTypeCodes = new ArrayList();
            weightUomCodes = new ArrayList();
            phoneNumberTypes = new ArrayList();
            lengthUomCodes = new ArrayList();
            widthUomCodes = lengthUomCodes;
            heightUomCodes = lengthUomCodes;
            unitTypeCodes = new ArrayList();
            packageTypeCodes = unitTypeCodes;
            additionalServices = new ArrayList();
            stopSpecialRequirements = new ArrayList();
        }
        public void Initialize()
        {
            InitializeequipmentCatagoryCodes();
            InitializetypeOfStops();
            InitializepaymentMethods();
            InitializeapplicationSources();
            InitializeequipmentTypeCodes();
            InitializeweightUomCodes();
            InitializeLengthUomCodes();

            InitializephoneNumberTypes();
            InitializeUnitTypeCodes();
            InitializeStopSpecialRequirements();
            InitializeCustomerReferenceNumbers();
        }
        void InitializeequipmentCatagoryCodes()
        {
            equipmentCatagoryCodes.Add("DB");
            equipmentCatagoryCodes.Add("CTN");
            equipmentCatagoryCodes.Add("VN");
            equipmentCatagoryCodes.Add("FB");
            equipmentCatagoryCodes.Add("CS");
            equipmentCatagoryCodes.Add("RF");
            equipmentCatagoryCodes.Add("TRACTORONL");
            equipmentCatagoryCodes.Add("OTH");
            equipmentCatagoryCodes.Add("Sprinter");
            equipmentCatagoryCodes.Add("Cargo");
            equipmentCatagoryCodes.Add("Van");
            equipmentCatagoryCodes.Add("LS");
            equipmentCatagoryCodes.Add("LSR");
            equipmentCatagoryCodes.Add("TRR");
        }
        void InitializetypeOfStops()
        {
            typeOfStops.Add("PICKUP");
            typeOfStops.Add("INTERMEDIATE");
            typeOfStops.Add("DELIVERY");
        }
        void InitializepaymentMethods()
        {
            paymentMethods.Add("COL");
            paymentMethods.Add("PPD");
            paymentMethods.Add("THIRDPARTY");
        }
        void InitializeapplicationSources()
        {
            applicationSources.Add("GPAPI");
            applicationSources.Add("XpoConnect");
            applicationSources.Add("EDI");
        }
        void InitializeequipmentTypeCodes()
        {
            equipmentTypeCodes.Add("AC");
            equipmentTypeCodes.Add("BT");
            equipmentTypeCodes.Add("C");
            equipmentTypeCodes.Add("CI");
            equipmentTypeCodes.Add("CONT");
            equipmentTypeCodes.Add("CR");
            equipmentTypeCodes.Add("DD");
            equipmentTypeCodes.Add("DT");
            equipmentTypeCodes.Add("F");
            equipmentTypeCodes.Add("F45");
            equipmentTypeCodes.Add("F48");
            equipmentTypeCodes.Add("F53");
            equipmentTypeCodes.Add("F60");
            equipmentTypeCodes.Add("FA");
            equipmentTypeCodes.Add("FD");
            equipmentTypeCodes.Add("FH");
            equipmentTypeCodes.Add("FM");
            equipmentTypeCodes.Add("FR");
            equipmentTypeCodes.Add("FT");
            equipmentTypeCodes.Add("FZ");
            equipmentTypeCodes.Add("HB");
            equipmentTypeCodes.Add("HOP");
            equipmentTypeCodes.Add("LA");
            equipmentTypeCodes.Add("LB");
            equipmentTypeCodes.Add("MV");
            equipmentTypeCodes.Add("MX");
            equipmentTypeCodes.Add("NU");
            equipmentTypeCodes.Add("OT");
            equipmentTypeCodes.Add("PO");
            equipmentTypeCodes.Add("R");
            equipmentTypeCodes.Add("R2");
            equipmentTypeCodes.Add("R48");
            equipmentTypeCodes.Add("R53");
            equipmentTypeCodes.Add("RA");
            equipmentTypeCodes.Add("RG");
            equipmentTypeCodes.Add("RL");
            equipmentTypeCodes.Add("RM");
            equipmentTypeCodes.Add("RN");
            equipmentTypeCodes.Add("RZ");
            equipmentTypeCodes.Add("SD");
            equipmentTypeCodes.Add("SDCK");
            equipmentTypeCodes.Add("ST");
            equipmentTypeCodes.Add("TA");
            equipmentTypeCodes.Add("TN");
            equipmentTypeCodes.Add("TRAC");
            equipmentTypeCodes.Add("TS");
            equipmentTypeCodes.Add("TT");
            equipmentTypeCodes.Add("V");
            equipmentTypeCodes.Add("V2");
            equipmentTypeCodes.Add("VA");
            equipmentTypeCodes.Add("VAN");
            equipmentTypeCodes.Add("VB");
            equipmentTypeCodes.Add("VC");
            equipmentTypeCodes.Add("VF");
            equipmentTypeCodes.Add("VG");
            equipmentTypeCodes.Add("VH");
            equipmentTypeCodes.Add("VI");
            equipmentTypeCodes.Add("VL");
            equipmentTypeCodes.Add("VM");
            equipmentTypeCodes.Add("VN"); 
            equipmentTypeCodes.Add("VN53");
            equipmentTypeCodes.Add("VR48");
            equipmentTypeCodes.Add("VR53");
            equipmentTypeCodes.Add("VS");
            equipmentTypeCodes.Add("VT");
            equipmentTypeCodes.Add("VV");
            equipmentTypeCodes.Add("VZ");
            equipmentTypeCodes.Add("VR");
            equipmentTypeCodes.Add("VRM");
            equipmentTypeCodes.Add("VRZ");
            equipmentTypeCodes.Add("VHC");
            equipmentTypeCodes.Add("COFC");
            equipmentTypeCodes.Add("CS");
            equipmentTypeCodes.Add("RGE");
            equipmentTypeCodes.Add("SD48");
            equipmentTypeCodes.Add("SD53");
            equipmentTypeCodes.Add("C20");
            equipmentTypeCodes.Add("C40");
            equipmentTypeCodes.Add("C53");
            equipmentTypeCodes.Add("LTL");
            equipmentTypeCodes.Add("OD");
            equipmentTypeCodes.Add("INT");
            equipmentTypeCodes.Add("AIR");
            equipmentTypeCodes.Add("LCL");
            equipmentTypeCodes.Add("CC20");
            equipmentTypeCodes.Add("CC40");
            equipmentTypeCodes.Add("CC48");
            equipmentTypeCodes.Add("CC53");
            equipmentTypeCodes.Add("CH20");
            equipmentTypeCodes.Add("CH40");
            equipmentTypeCodes.Add("CH48");
            equipmentTypeCodes.Add("CH53");
            equipmentTypeCodes.Add("HEAT");
            equipmentTypeCodes.Add("CONE");
            equipmentTypeCodes.Add("NA");
            equipmentTypeCodes.Add("CargoVan");
            equipmentTypeCodes.Add("Sprinter");
            equipmentTypeCodes.Add("LS");
            equipmentTypeCodes.Add("LSR");
            equipmentTypeCodes.Add("HC40");
            equipmentTypeCodes.Add("HCOC");
            equipmentTypeCodes.Add("HHCC");
            equipmentTypeCodes.Add("TRR");
            equipmentTypeCodes.Add("MNV");
            equipmentTypeCodes.Add("CBV");
            equipmentTypeCodes.Add("EIGHTEEN_D");
            equipmentTypeCodes.Add("TWENTYTWO_D");
            equipmentTypeCodes.Add("TWENTYFOUR_D");
            equipmentTypeCodes.Add("TWENTYEIGHT_D");
            equipmentTypeCodes.Add("VSPR");
            equipmentTypeCodes.Add("VZM");
            equipmentTypeCodes.Add("RZM");
        }
        void InitializeweightUomCodes()
        {
            weightUomCodes.Add("KG");
            weightUomCodes.Add("LB");
        }
        void InitializephoneNumberTypes()
        {
            phoneNumberTypes.Add("MOBILE");
            phoneNumberTypes.Add("WORK");
            phoneNumberTypes.Add("HOME");
        }
        void InitializeLengthUomCodes()
        {
            lengthUomCodes.Add("CM");
            lengthUomCodes.Add("IN");
            lengthUomCodes.Add("FT");
        }
        void InitializewidthUomCodes()
        {

        }
        void InitializeheightUomCodes()
        {

        }
        void InitializeUnitTypeCodes()
        {
            unitTypeCodes.Add("ACCE");
            unitTypeCodes.Add("DRUM");
            unitTypeCodes.Add("SKIDS");
            unitTypeCodes.Add("ATTCH");
            unitTypeCodes.Add("ENVE");
            unitTypeCodes.Add("SLIP");
            unitTypeCodes.Add("BAGS");
            unitTypeCodes.Add("FEET");
            unitTypeCodes.Add("SPRS");
            unitTypeCodes.Add("BALE");
            unitTypeCodes.Add("FIRK");
            unitTypeCodes.Add("TOTE");
            unitTypeCodes.Add("BASK");
            unitTypeCodes.Add("GYLD");
            unitTypeCodes.Add("TRKL");
            unitTypeCodes.Add("BHDS");
            unitTypeCodes.Add("GAYL");
            unitTypeCodes.Add("TRLR");
            unitTypeCodes.Add("BNDL");
            unitTypeCodes.Add("KEGS");
            unitTypeCodes.Add("TRNK");
            unitTypeCodes.Add("BOXES");
            unitTypeCodes.Add("LOOS");
            unitTypeCodes.Add("TRAY");
            unitTypeCodes.Add("BRRL");
            unitTypeCodes.Add("OCTA");
            unitTypeCodes.Add("TUBE");
            unitTypeCodes.Add("BUCK");
            unitTypeCodes.Add("PACK");
            unitTypeCodes.Add("UNIT");
            unitTypeCodes.Add("CARB");
            unitTypeCodes.Add("PAIL");
            unitTypeCodes.Add("UNPK");
            unitTypeCodes.Add("CASES");
            unitTypeCodes.Add("PIEC");
            unitTypeCodes.Add("VEH");
            unitTypeCodes.Add("CASE");
            unitTypeCodes.Add("PALLETS");
            unitTypeCodes.Add("BINS");
            unitTypeCodes.Add("CHST");
            unitTypeCodes.Add("PLTS");
            unitTypeCodes.Add("COIL");
            unitTypeCodes.Add("RACK");
            unitTypeCodes.Add("CARTONS");
            unitTypeCodes.Add("RACKS");
            unitTypeCodes.Add("CRTS");
            unitTypeCodes.Add("REEL");
            unitTypeCodes.Add("CTN");
            unitTypeCodes.Add("ROLL");
            unitTypeCodes.Add("CYLS");
            unitTypeCodes.Add("SKID");
        }
        void InitializepackageTypeCodes()
        {

        }
        void InitializeAdditionalServices()
        {

        }
        
        void InitializeCustomerReferenceNumbers()
        {
            customerRefereneTypeCodes.Add("AN"); 
            customerRefereneTypeCodes.Add("AO"); 
            customerRefereneTypeCodes.Add("BM"); 
            customerRefereneTypeCodes.Add("CG"); 
            customerRefereneTypeCodes.Add("MB"); 
            customerRefereneTypeCodes.Add("OI"); 
            customerRefereneTypeCodes.Add("OR"); 
            customerRefereneTypeCodes.Add("PO"); 
            customerRefereneTypeCodes.Add("PS"); 
            customerRefereneTypeCodes.Add("PU"); 
            customerRefereneTypeCodes.Add("QL"); 
            customerRefereneTypeCodes.Add("SI"); 
            customerRefereneTypeCodes.Add("SN"); 
            customerRefereneTypeCodes.Add("ZZ"); 
            customerRefereneTypeCodes.Add("11"); 
            customerRefereneTypeCodes.Add("19"); 
            customerRefereneTypeCodes.Add("2I"); 
            customerRefereneTypeCodes.Add("6O"); 
            customerRefereneTypeCodes.Add("6Y"); 
            customerRefereneTypeCodes.Add("CN"); 
            customerRefereneTypeCodes.Add("CO"); 
            customerRefereneTypeCodes.Add("CR"); 
            customerRefereneTypeCodes.Add("DJ"); 
            customerRefereneTypeCodes.Add("DN"); 
            customerRefereneTypeCodes.Add("DO"); 
            customerRefereneTypeCodes.Add("DP"); 
            customerRefereneTypeCodes.Add("EH"); 
            customerRefereneTypeCodes.Add("EMM"); 
            customerRefereneTypeCodes.Add("EQ"); 
            customerRefereneTypeCodes.Add("IA"); 
            customerRefereneTypeCodes.Add("IL"); 
            customerRefereneTypeCodes.Add("KK"); 
            customerRefereneTypeCodes.Add("KR"); 
            customerRefereneTypeCodes.Add("MI"); 
            customerRefereneTypeCodes.Add("MO"); 
            customerRefereneTypeCodes.Add("OQ"); 
            customerRefereneTypeCodes.Add("P8"); 
            customerRefereneTypeCodes.Add("PRT"); 
            customerRefereneTypeCodes.Add("QN"); 
            customerRefereneTypeCodes.Add("QY"); 
            customerRefereneTypeCodes.Add("RB"); 
            customerRefereneTypeCodes.Add("RE"); 
            customerRefereneTypeCodes.Add("RN"); 
            customerRefereneTypeCodes.Add("SC"); 
            customerRefereneTypeCodes.Add("SO"); 
            customerRefereneTypeCodes.Add("SQ"); 
            customerRefereneTypeCodes.Add("TH"); 
            customerRefereneTypeCodes.Add("TN"); 
            customerRefereneTypeCodes.Add("VD"); 
            customerRefereneTypeCodes.Add("VR"); 
            customerRefereneTypeCodes.Add("RSN"); 
            customerRefereneTypeCodes.Add("8"); 
            customerRefereneTypeCodes.Add("23"); 
            customerRefereneTypeCodes.Add("4B"); 
            customerRefereneTypeCodes.Add("51"); 
            customerRefereneTypeCodes.Add("6A"); 
            customerRefereneTypeCodes.Add("6M"); 
            customerRefereneTypeCodes.Add("6P"); 
            customerRefereneTypeCodes.Add("71"); 
            customerRefereneTypeCodes.Add("A0"); 
            customerRefereneTypeCodes.Add("A6"); 
            customerRefereneTypeCodes.Add("A8"); 
            customerRefereneTypeCodes.Add("ACI"); 
            customerRefereneTypeCodes.Add("AG"); 
            customerRefereneTypeCodes.Add("AQ"); 
            customerRefereneTypeCodes.Add("AT"); 
            customerRefereneTypeCodes.Add("BC"); 
            customerRefereneTypeCodes.Add("BL"); 
            customerRefereneTypeCodes.Add("BN"); 
            customerRefereneTypeCodes.Add("BV"); 
            customerRefereneTypeCodes.Add("CB"); 
            customerRefereneTypeCodes.Add("CF"); 
            customerRefereneTypeCodes.Add("CH"); 
            customerRefereneTypeCodes.Add("CP"); 
            customerRefereneTypeCodes.Add("CT"); 
            customerRefereneTypeCodes.Add("DAN"); 
            customerRefereneTypeCodes.Add("DG"); 
            customerRefereneTypeCodes.Add("DI"); 
            customerRefereneTypeCodes.Add("DK"); 
            customerRefereneTypeCodes.Add("DM"); 
            customerRefereneTypeCodes.Add("DU"); 
            customerRefereneTypeCodes.Add("GD"); 
            customerRefereneTypeCodes.Add("GM"); 
            customerRefereneTypeCodes.Add("IK"); 
            customerRefereneTypeCodes.Add("K6"); 
            customerRefereneTypeCodes.Add("LI"); 
            customerRefereneTypeCodes.Add("LM"); 
            customerRefereneTypeCodes.Add("LO"); 
            customerRefereneTypeCodes.Add("MH"); 
            customerRefereneTypeCodes.Add("NS"); 
            customerRefereneTypeCodes.Add("OC"); 
            customerRefereneTypeCodes.Add("ON"); 
            customerRefereneTypeCodes.Add("OP"); 
            customerRefereneTypeCodes.Add("OU"); 
            customerRefereneTypeCodes.Add("OW"); 
            customerRefereneTypeCodes.Add("PI"); 
            customerRefereneTypeCodes.Add("PL"); 
            customerRefereneTypeCodes.Add("PP"); 
            customerRefereneTypeCodes.Add("PUA"); 
            customerRefereneTypeCodes.Add("Q1"); 
            customerRefereneTypeCodes.Add("QJ"); 
            customerRefereneTypeCodes.Add("RI"); 
            customerRefereneTypeCodes.Add("RZ"); 
            customerRefereneTypeCodes.Add("S5"); 
            customerRefereneTypeCodes.Add("TDT"); 
            customerRefereneTypeCodes.Add("TF"); 
            customerRefereneTypeCodes.Add("TO"); 
            customerRefereneTypeCodes.Add("TP"); 
            customerRefereneTypeCodes.Add("TS"); 
            customerRefereneTypeCodes.Add("UP"); 
            customerRefereneTypeCodes.Add("VA"); 
            customerRefereneTypeCodes.Add("VE"); 
            customerRefereneTypeCodes.Add("ZG"); 
            customerRefereneTypeCodes.Add("ZM"); 
            customerRefereneTypeCodes.Add("1"); 
            customerRefereneTypeCodes.Add("2"); 
            customerRefereneTypeCodes.Add("3"); 
            customerRefereneTypeCodes.Add("4"); 
            customerRefereneTypeCodes.Add("5"); 
            customerRefereneTypeCodes.Add("6"); 
            customerRefereneTypeCodes.Add("7"); 
            customerRefereneTypeCodes.Add("8"); 
            customerRefereneTypeCodes.Add("9"); 
            customerRefereneTypeCodes.Add("0A"); 
            customerRefereneTypeCodes.Add("0B"); 
            customerRefereneTypeCodes.Add("0D"); 
            customerRefereneTypeCodes.Add("0E"); 
            customerRefereneTypeCodes.Add("0F"); 
            customerRefereneTypeCodes.Add("0G"); 
            customerRefereneTypeCodes.Add("0H"); 
            customerRefereneTypeCodes.Add("0I"); 
            customerRefereneTypeCodes.Add("0J"); 
            customerRefereneTypeCodes.Add("0K"); 
            customerRefereneTypeCodes.Add("0L"); 
            customerRefereneTypeCodes.Add("0M"); 
            customerRefereneTypeCodes.Add("0N"); 
            customerRefereneTypeCodes.Add("0P"); 
            customerRefereneTypeCodes.Add("10"); 
            customerRefereneTypeCodes.Add("12"); 
            customerRefereneTypeCodes.Add("13"); 
            customerRefereneTypeCodes.Add("14"); 
            customerRefereneTypeCodes.Add("15"); 
            customerRefereneTypeCodes.Add("16"); 
            customerRefereneTypeCodes.Add("17"); 
            customerRefereneTypeCodes.Add("18"); 
            customerRefereneTypeCodes.Add("1A"); 
            customerRefereneTypeCodes.Add("1B"); 
            customerRefereneTypeCodes.Add("1C"); 
            customerRefereneTypeCodes.Add("1D"); 
            customerRefereneTypeCodes.Add("1E"); 
            customerRefereneTypeCodes.Add("1F"); 
            customerRefereneTypeCodes.Add("1G"); 
            customerRefereneTypeCodes.Add("1H"); 
            customerRefereneTypeCodes.Add("1I"); 
            customerRefereneTypeCodes.Add("1J"); 
            customerRefereneTypeCodes.Add("1K"); 
            customerRefereneTypeCodes.Add("1L"); 
            customerRefereneTypeCodes.Add("1M"); 
            customerRefereneTypeCodes.Add("1N"); 
            customerRefereneTypeCodes.Add("1O"); 
            customerRefereneTypeCodes.Add("1P"); 
            customerRefereneTypeCodes.Add("1Q"); 
            customerRefereneTypeCodes.Add("1R"); 
            customerRefereneTypeCodes.Add("1S"); 
            customerRefereneTypeCodes.Add("1T"); 
            customerRefereneTypeCodes.Add("1U"); 
            customerRefereneTypeCodes.Add("1V"); 
            customerRefereneTypeCodes.Add("1W"); 
            customerRefereneTypeCodes.Add("1X"); 
            customerRefereneTypeCodes.Add("1Y"); 
            customerRefereneTypeCodes.Add("1Z"); 
            customerRefereneTypeCodes.Add("20"); 
            customerRefereneTypeCodes.Add("21"); 
            customerRefereneTypeCodes.Add("22"); 
            customerRefereneTypeCodes.Add("24"); 
            customerRefereneTypeCodes.Add("25"); 
            customerRefereneTypeCodes.Add("26"); 
            customerRefereneTypeCodes.Add("27"); 
            customerRefereneTypeCodes.Add("28"); 
            customerRefereneTypeCodes.Add("29"); 
            customerRefereneTypeCodes.Add("2A"); 
            customerRefereneTypeCodes.Add("2B"); 
            customerRefereneTypeCodes.Add("2C"); 
            customerRefereneTypeCodes.Add("2D"); 
            customerRefereneTypeCodes.Add("2E"); 
            customerRefereneTypeCodes.Add("2F"); 
            customerRefereneTypeCodes.Add("2G"); 
            customerRefereneTypeCodes.Add("2H"); 
            customerRefereneTypeCodes.Add("2J"); 
            customerRefereneTypeCodes.Add("2K"); 
            customerRefereneTypeCodes.Add("2L"); 
            customerRefereneTypeCodes.Add("2M"); 
            customerRefereneTypeCodes.Add("2N"); 
            customerRefereneTypeCodes.Add("2O"); 
            customerRefereneTypeCodes.Add("2P"); 
            customerRefereneTypeCodes.Add("2Q"); 
            customerRefereneTypeCodes.Add("2R"); 
            customerRefereneTypeCodes.Add("2S"); 
            customerRefereneTypeCodes.Add("2T"); 
            customerRefereneTypeCodes.Add("2U"); 
            customerRefereneTypeCodes.Add("2V"); 
            customerRefereneTypeCodes.Add("2W"); 
            customerRefereneTypeCodes.Add("2X"); 
            customerRefereneTypeCodes.Add("2Y"); 
            customerRefereneTypeCodes.Add("2Z"); 
            customerRefereneTypeCodes.Add("30"); 
            customerRefereneTypeCodes.Add("31"); 
            customerRefereneTypeCodes.Add("32"); 
            customerRefereneTypeCodes.Add("33"); 
            customerRefereneTypeCodes.Add("34"); 
            customerRefereneTypeCodes.Add("35"); 
            customerRefereneTypeCodes.Add("36"); 
            customerRefereneTypeCodes.Add("37"); 
            customerRefereneTypeCodes.Add("38"); 
            customerRefereneTypeCodes.Add("39"); 
            customerRefereneTypeCodes.Add("3A"); 
            customerRefereneTypeCodes.Add("3B"); 
            customerRefereneTypeCodes.Add("3C"); 
            customerRefereneTypeCodes.Add("3D"); 
            customerRefereneTypeCodes.Add("3E"); 
            customerRefereneTypeCodes.Add("3F"); 
            customerRefereneTypeCodes.Add("3G"); 
            customerRefereneTypeCodes.Add("3H"); 
            customerRefereneTypeCodes.Add("3I"); 
            customerRefereneTypeCodes.Add("3J"); 
            customerRefereneTypeCodes.Add("3K"); 
            customerRefereneTypeCodes.Add("3L"); 
            customerRefereneTypeCodes.Add("3M"); 
            customerRefereneTypeCodes.Add("3N"); 
            customerRefereneTypeCodes.Add("3O"); 
            customerRefereneTypeCodes.Add("3P"); 
            customerRefereneTypeCodes.Add("3Q"); 
            customerRefereneTypeCodes.Add("3R"); 
            customerRefereneTypeCodes.Add("3S"); 
            customerRefereneTypeCodes.Add("3T"); 
            customerRefereneTypeCodes.Add("3U"); 
            customerRefereneTypeCodes.Add("3V"); 
            customerRefereneTypeCodes.Add("3W"); 
            customerRefereneTypeCodes.Add("3X"); 
            customerRefereneTypeCodes.Add("3Y"); 
            customerRefereneTypeCodes.Add("3Z"); 
            customerRefereneTypeCodes.Add("40"); 
            customerRefereneTypeCodes.Add("41"); 
            customerRefereneTypeCodes.Add("42"); 
            customerRefereneTypeCodes.Add("43"); 
            customerRefereneTypeCodes.Add("44"); 
            customerRefereneTypeCodes.Add("45"); 
            customerRefereneTypeCodes.Add("46"); 
            customerRefereneTypeCodes.Add("47"); 
            customerRefereneTypeCodes.Add("48"); 
            customerRefereneTypeCodes.Add("49"); 
            customerRefereneTypeCodes.Add("4A"); 
            customerRefereneTypeCodes.Add("4C"); 
            customerRefereneTypeCodes.Add("4D"); 
            customerRefereneTypeCodes.Add("4E"); 
            customerRefereneTypeCodes.Add("4F"); 
            customerRefereneTypeCodes.Add("4G"); 
            customerRefereneTypeCodes.Add("4H"); 
            customerRefereneTypeCodes.Add("4I"); 
            customerRefereneTypeCodes.Add("4J"); 
            customerRefereneTypeCodes.Add("4K"); 
            customerRefereneTypeCodes.Add("4L"); 
            customerRefereneTypeCodes.Add("4M"); 
            customerRefereneTypeCodes.Add("4N"); 
            customerRefereneTypeCodes.Add("4O"); 
            customerRefereneTypeCodes.Add("4P"); 
            customerRefereneTypeCodes.Add("4Q"); 
            customerRefereneTypeCodes.Add("4R"); 
            customerRefereneTypeCodes.Add("4S"); 
            customerRefereneTypeCodes.Add("4T"); 
            customerRefereneTypeCodes.Add("4U"); 
            customerRefereneTypeCodes.Add("4V"); 
            customerRefereneTypeCodes.Add("4W"); 
            customerRefereneTypeCodes.Add("4X"); 
            customerRefereneTypeCodes.Add("4Y"); 
            customerRefereneTypeCodes.Add("4Z"); 
            customerRefereneTypeCodes.Add("50"); 
            customerRefereneTypeCodes.Add("52"); 
            customerRefereneTypeCodes.Add("53"); 
            customerRefereneTypeCodes.Add("54"); 
            customerRefereneTypeCodes.Add("55"); 
            customerRefereneTypeCodes.Add("56"); 
            customerRefereneTypeCodes.Add("57"); 
            customerRefereneTypeCodes.Add("58"); 
            customerRefereneTypeCodes.Add("59"); 
            customerRefereneTypeCodes.Add("5A"); 
            customerRefereneTypeCodes.Add("5B"); 
            customerRefereneTypeCodes.Add("5C"); 
            customerRefereneTypeCodes.Add("5D"); 
            customerRefereneTypeCodes.Add("5E"); 
            customerRefereneTypeCodes.Add("5F"); 
            customerRefereneTypeCodes.Add("5G"); 
            customerRefereneTypeCodes.Add("5H"); 
            customerRefereneTypeCodes.Add("5I"); 
            customerRefereneTypeCodes.Add("5J"); 
            customerRefereneTypeCodes.Add("5K"); 
            customerRefereneTypeCodes.Add("5L"); 
            customerRefereneTypeCodes.Add("5M"); 
            customerRefereneTypeCodes.Add("5N"); 
            customerRefereneTypeCodes.Add("5O"); 
            customerRefereneTypeCodes.Add("5P"); 
            customerRefereneTypeCodes.Add("5Q"); 
            customerRefereneTypeCodes.Add("5R"); 
            customerRefereneTypeCodes.Add("5S"); 
            customerRefereneTypeCodes.Add("5T"); 
            customerRefereneTypeCodes.Add("5U"); 
            customerRefereneTypeCodes.Add("5V"); 
            customerRefereneTypeCodes.Add("5W"); 
            customerRefereneTypeCodes.Add("5X"); 
            customerRefereneTypeCodes.Add("5Y"); 
            customerRefereneTypeCodes.Add("5Z"); 
            customerRefereneTypeCodes.Add("60"); 
            customerRefereneTypeCodes.Add("61"); 
            customerRefereneTypeCodes.Add("63"); 
            customerRefereneTypeCodes.Add("64"); 
            customerRefereneTypeCodes.Add("65"); 
            customerRefereneTypeCodes.Add("66"); 
            customerRefereneTypeCodes.Add("67"); 
            customerRefereneTypeCodes.Add("68"); 
            customerRefereneTypeCodes.Add("69"); 
            //customerRefereneTypeCodes.Add("6B"); customerRefereneTypeCodes.Add("6C"); customerRefereneTypeCodes.Add("6D"); customerRefereneTypeCodes.Add("6E"); customerRefereneTypeCodes.Add("6F"); customerRefereneTypeCodes.Add("6G"); customerRefereneTypeCodes.Add("6H"); customerRefereneTypeCodes.Add("6I"); customerRefereneTypeCodes.Add("6J"); customerRefereneTypeCodes.Add("6K"); customerRefereneTypeCodes.Add("6L"); customerRefereneTypeCodes.Add("6N"); customerRefereneTypeCodes.Add("6Q"); customerRefereneTypeCodes.Add("6R"); customerRefereneTypeCodes.Add("6S"); customerRefereneTypeCodes.Add("6T"); customerRefereneTypeCodes.Add("6U"); customerRefereneTypeCodes.Add("6V"); customerRefereneTypeCodes.Add("6W"); customerRefereneTypeCodes.Add("6X"); customerRefereneTypeCodes.Add("6Z"); customerRefereneTypeCodes.Add("70"); customerRefereneTypeCodes.Add("72"); customerRefereneTypeCodes.Add("73"); customerRefereneTypeCodes.Add("74"); customerRefereneTypeCodes.Add("75"); customerRefereneTypeCodes.Add("76"); customerRefereneTypeCodes.Add("77"); customerRefereneTypeCodes.Add("78"); customerRefereneTypeCodes.Add("79"); customerRefereneTypeCodes.Add("7A"); customerRefereneTypeCodes.Add("7B"); customerRefereneTypeCodes.Add("7C"); customerRefereneTypeCodes.Add("7D"); customerRefereneTypeCodes.Add("7E"); customerRefereneTypeCodes.Add("7F"); customerRefereneTypeCodes.Add("7G"); customerRefereneTypeCodes.Add("7H"); customerRefereneTypeCodes.Add("7I"); customerRefereneTypeCodes.Add("7J"); customerRefereneTypeCodes.Add("7K"); customerRefereneTypeCodes.Add("7L"); customerRefereneTypeCodes.Add("7M"); customerRefereneTypeCodes.Add("7N"); customerRefereneTypeCodes.Add("7O"); customerRefereneTypeCodes.Add("7P"); customerRefereneTypeCodes.Add("7Q"); customerRefereneTypeCodes.Add("7R"); customerRefereneTypeCodes.Add("7S"); customerRefereneTypeCodes.Add("7T"); customerRefereneTypeCodes.Add("7U"); customerRefereneTypeCodes.Add("7W"); customerRefereneTypeCodes.Add("7X"); customerRefereneTypeCodes.Add("7Y"); customerRefereneTypeCodes.Add("7Z"); customerRefereneTypeCodes.Add("80"); customerRefereneTypeCodes.Add("81"); customerRefereneTypeCodes.Add("82"); customerRefereneTypeCodes.Add("83"); customerRefereneTypeCodes.Add("84"); customerRefereneTypeCodes.Add("85"); customerRefereneTypeCodes.Add("86"); customerRefereneTypeCodes.Add("87"); customerRefereneTypeCodes.Add("88"); customerRefereneTypeCodes.Add("89"); customerRefereneTypeCodes.Add("8A"); customerRefereneTypeCodes.Add("8B"); customerRefereneTypeCodes.Add("8C"); customerRefereneTypeCodes.Add("8D"); customerRefereneTypeCodes.Add("8E"); customerRefereneTypeCodes.Add("8F"); customerRefereneTypeCodes.Add("8G"); customerRefereneTypeCodes.Add("8H"); customerRefereneTypeCodes.Add("8I"); customerRefereneTypeCodes.Add("8J"); customerRefereneTypeCodes.Add("8K"); customerRefereneTypeCodes.Add("8L"); customerRefereneTypeCodes.Add("8M"); customerRefereneTypeCodes.Add("8N"); customerRefereneTypeCodes.Add("8O"); customerRefereneTypeCodes.Add("8P"); customerRefereneTypeCodes.Add("8Q"); customerRefereneTypeCodes.Add("8R"); customerRefereneTypeCodes.Add("8S"); customerRefereneTypeCodes.Add("8U"); customerRefereneTypeCodes.Add("8V"); customerRefereneTypeCodes.Add("8W"); customerRefereneTypeCodes.Add("8X"); customerRefereneTypeCodes.Add("8Y"); customerRefereneTypeCodes.Add("8Z"); customerRefereneTypeCodes.Add("90"); customerRefereneTypeCodes.Add("91"); customerRefereneTypeCodes.Add("92"); customerRefereneTypeCodes.Add("93"); customerRefereneTypeCodes.Add("94"); customerRefereneTypeCodes.Add("95"); customerRefereneTypeCodes.Add("96"); customerRefereneTypeCodes.Add("97"); customerRefereneTypeCodes.Add("98"); customerRefereneTypeCodes.Add("99"); customerRefereneTypeCodes.Add("9A"); customerRefereneTypeCodes.Add("9B"); customerRefereneTypeCodes.Add("9C"); customerRefereneTypeCodes.Add("9D"); customerRefereneTypeCodes.Add("9E"); customerRefereneTypeCodes.Add("9F"); customerRefereneTypeCodes.Add("9G"); customerRefereneTypeCodes.Add("9H"); customerRefereneTypeCodes.Add("9I"); customerRefereneTypeCodes.Add("9J"); customerRefereneTypeCodes.Add("9K"); customerRefereneTypeCodes.Add("9L"); customerRefereneTypeCodes.Add("9M"); customerRefereneTypeCodes.Add("9N"); customerRefereneTypeCodes.Add("9P"); customerRefereneTypeCodes.Add("9Q"); customerRefereneTypeCodes.Add("9R"); customerRefereneTypeCodes.Add("9S"); customerRefereneTypeCodes.Add("9T"); customerRefereneTypeCodes.Add("9U"); customerRefereneTypeCodes.Add("9V"); customerRefereneTypeCodes.Add("9W"); customerRefereneTypeCodes.Add("9X"); customerRefereneTypeCodes.Add("9Y"); customerRefereneTypeCodes.Add("9Z"); customerRefereneTypeCodes.Add("A1"); customerRefereneTypeCodes.Add("A2"); customerRefereneTypeCodes.Add("A3"); customerRefereneTypeCodes.Add("A4"); customerRefereneTypeCodes.Add("A5"); customerRefereneTypeCodes.Add("A7"); customerRefereneTypeCodes.Add("A9"); customerRefereneTypeCodes.Add("AA"); customerRefereneTypeCodes.Add("AAA"); customerRefereneTypeCodes.Add("AAB"); customerRefereneTypeCodes.Add("AAC"); customerRefereneTypeCodes.Add("AAD"); customerRefereneTypeCodes.Add("AAE"); customerRefereneTypeCodes.Add("AAF"); customerRefereneTypeCodes.Add("AAG"); customerRefereneTypeCodes.Add("AAH"); customerRefereneTypeCodes.Add("AAI"); customerRefereneTypeCodes.Add("AAJ"); customerRefereneTypeCodes.Add("AAK"); customerRefereneTypeCodes.Add("AAL"); customerRefereneTypeCodes.Add("AAM"); customerRefereneTypeCodes.Add("AAN"); customerRefereneTypeCodes.Add("AAO"); customerRefereneTypeCodes.Add("AAP"); customerRefereneTypeCodes.Add("AAQ"); customerRefereneTypeCodes.Add("AAR"); customerRefereneTypeCodes.Add("AAS"); customerRefereneTypeCodes.Add("AAT"); customerRefereneTypeCodes.Add("AAU"); customerRefereneTypeCodes.Add("AAV"); customerRefereneTypeCodes.Add("AAW"); customerRefereneTypeCodes.Add("AAX"); customerRefereneTypeCodes.Add("AAY"); customerRefereneTypeCodes.Add("AAZ"); customerRefereneTypeCodes.Add("AB"); customerRefereneTypeCodes.Add("ABA"); customerRefereneTypeCodes.Add("ABB"); customerRefereneTypeCodes.Add("ABC"); customerRefereneTypeCodes.Add("ABD"); customerRefereneTypeCodes.Add("ABE"); customerRefereneTypeCodes.Add("ABF"); customerRefereneTypeCodes.Add("ABG"); customerRefereneTypeCodes.Add("ABH"); customerRefereneTypeCodes.Add("ABJ"); customerRefereneTypeCodes.Add("ABK"); customerRefereneTypeCodes.Add("ABL"); customerRefereneTypeCodes.Add("ABM"); customerRefereneTypeCodes.Add("ABN"); customerRefereneTypeCodes.Add("ABO"); customerRefereneTypeCodes.Add("ABP"); customerRefereneTypeCodes.Add("ABQ"); customerRefereneTypeCodes.Add("ABR"); customerRefereneTypeCodes.Add("ABS"); customerRefereneTypeCodes.Add("ABT"); customerRefereneTypeCodes.Add("ABU"); customerRefereneTypeCodes.Add("ABV"); customerRefereneTypeCodes.Add("ABY"); customerRefereneTypeCodes.Add("AC"); customerRefereneTypeCodes.Add("ACA"); customerRefereneTypeCodes.Add("ACB"); customerRefereneTypeCodes.Add("ACC"); customerRefereneTypeCodes.Add("ACD"); customerRefereneTypeCodes.Add("ACE"); customerRefereneTypeCodes.Add("ACF"); customerRefereneTypeCodes.Add("ACG"); customerRefereneTypeCodes.Add("ACH"); customerRefereneTypeCodes.Add("ACJ"); customerRefereneTypeCodes.Add("ACK"); customerRefereneTypeCodes.Add("ACR"); customerRefereneTypeCodes.Add("ACS"); customerRefereneTypeCodes.Add("ACT"); customerRefereneTypeCodes.Add("AD"); customerRefereneTypeCodes.Add("ADA"); customerRefereneTypeCodes.Add("ADB"); customerRefereneTypeCodes.Add("ADC"); customerRefereneTypeCodes.Add("ADD"); customerRefereneTypeCodes.Add("ADE"); customerRefereneTypeCodes.Add("ADF"); customerRefereneTypeCodes.Add("ADG"); customerRefereneTypeCodes.Add("ADH"); customerRefereneTypeCodes.Add("ADI"); customerRefereneTypeCodes.Add("ADM"); customerRefereneTypeCodes.Add("AE"); customerRefereneTypeCodes.Add("AEA"); customerRefereneTypeCodes.Add("AEB"); customerRefereneTypeCodes.Add("AEC"); customerRefereneTypeCodes.Add("AED"); customerRefereneTypeCodes.Add("AEE"); customerRefereneTypeCodes.Add("AEF"); customerRefereneTypeCodes.Add("AEG"); customerRefereneTypeCodes.Add("AEH"); customerRefereneTypeCodes.Add("AEI"); customerRefereneTypeCodes.Add("AEJ"); customerRefereneTypeCodes.Add("AEK"); customerRefereneTypeCodes.Add("AEL"); customerRefereneTypeCodes.Add("AEM"); customerRefereneTypeCodes.Add("AF"); customerRefereneTypeCodes.Add("AH"); customerRefereneTypeCodes.Add("AHC"); customerRefereneTypeCodes.Add("AI"); customerRefereneTypeCodes.Add("AJ"); customerRefereneTypeCodes.Add("AK"); customerRefereneTypeCodes.Add("AL"); customerRefereneTypeCodes.Add("ALC"); customerRefereneTypeCodes.Add("ALG"); customerRefereneTypeCodes.Add("ALH"); customerRefereneTypeCodes.Add("ALI"); customerRefereneTypeCodes.Add("ALJ"); customerRefereneTypeCodes.Add("ALT"); customerRefereneTypeCodes.Add("AM"); customerRefereneTypeCodes.Add("AP"); customerRefereneTypeCodes.Add("API"); customerRefereneTypeCodes.Add("AR"); customerRefereneTypeCodes.Add("AS"); customerRefereneTypeCodes.Add("ASL"); customerRefereneTypeCodes.Add("ASP"); customerRefereneTypeCodes.Add("AST"); customerRefereneTypeCodes.Add("ATC"); customerRefereneTypeCodes.Add("AU"); customerRefereneTypeCodes.Add("AV"); customerRefereneTypeCodes.Add("AW"); customerRefereneTypeCodes.Add("AX"); customerRefereneTypeCodes.Add("AY"); customerRefereneTypeCodes.Add("AZ"); customerRefereneTypeCodes.Add("B1"); customerRefereneTypeCodes.Add("B2"); customerRefereneTypeCodes.Add("B3"); customerRefereneTypeCodes.Add("B4"); customerRefereneTypeCodes.Add("B5"); customerRefereneTypeCodes.Add("B6"); customerRefereneTypeCodes.Add("B7"); customerRefereneTypeCodes.Add("B8"); customerRefereneTypeCodes.Add("B9"); customerRefereneTypeCodes.Add("BA"); customerRefereneTypeCodes.Add("BAA"); customerRefereneTypeCodes.Add("BAB"); customerRefereneTypeCodes.Add("BAC"); customerRefereneTypeCodes.Add("BAD"); customerRefereneTypeCodes.Add("BAE"); customerRefereneTypeCodes.Add("BAF"); customerRefereneTypeCodes.Add("BAG"); customerRefereneTypeCodes.Add("BAH"); customerRefereneTypeCodes.Add("BAI"); customerRefereneTypeCodes.Add("BB"); customerRefereneTypeCodes.Add("BCI"); customerRefereneTypeCodes.Add("BD"); customerRefereneTypeCodes.Add("BE"); customerRefereneTypeCodes.Add("BF"); customerRefereneTypeCodes.Add("BG"); customerRefereneTypeCodes.Add("BH"); customerRefereneTypeCodes.Add("BI"); customerRefereneTypeCodes.Add("BJ"); customerRefereneTypeCodes.Add("BK"); customerRefereneTypeCodes.Add("BKT"); customerRefereneTypeCodes.Add("BLT"); customerRefereneTypeCodes.Add("BMM"); customerRefereneTypeCodes.Add("BO"); customerRefereneTypeCodes.Add("BOI"); customerRefereneTypeCodes.Add("BP"); customerRefereneTypeCodes.Add("BQ"); customerRefereneTypeCodes.Add("BR"); customerRefereneTypeCodes.Add("BS"); customerRefereneTypeCodes.Add("BT"); customerRefereneTypeCodes.Add("BU"); customerRefereneTypeCodes.Add("BW"); customerRefereneTypeCodes.Add("BX"); customerRefereneTypeCodes.Add("BY"); customerRefereneTypeCodes.Add("BZ"); customerRefereneTypeCodes.Add("C0"); customerRefereneTypeCodes.Add("C1"); customerRefereneTypeCodes.Add("C2"); customerRefereneTypeCodes.Add("C3"); customerRefereneTypeCodes.Add("C4"); customerRefereneTypeCodes.Add("C5"); customerRefereneTypeCodes.Add("C6"); customerRefereneTypeCodes.Add("C7"); customerRefereneTypeCodes.Add("C8"); customerRefereneTypeCodes.Add("C9"); customerRefereneTypeCodes.Add("CA"); customerRefereneTypeCodes.Add("CBG"); customerRefereneTypeCodes.Add("CC"); customerRefereneTypeCodes.Add("CD"); customerRefereneTypeCodes.Add("CDN"); customerRefereneTypeCodes.Add("CE"); customerRefereneTypeCodes.Add("CI"); customerRefereneTypeCodes.Add("CIR"); customerRefereneTypeCodes.Add("CIT"); customerRefereneTypeCodes.Add("CJ"); customerRefereneTypeCodes.Add("CK"); customerRefereneTypeCodes.Add("CL"); customerRefereneTypeCodes.Add("CM"); customerRefereneTypeCodes.Add("CMN"); customerRefereneTypeCodes.Add("CMP"); customerRefereneTypeCodes.Add("CMT"); customerRefereneTypeCodes.Add("CNO"); customerRefereneTypeCodes.Add("COL"); customerRefereneTypeCodes.Add("COT"); customerRefereneTypeCodes.Add("CPA"); customerRefereneTypeCodes.Add("CPT"); customerRefereneTypeCodes.Add("CQ"); customerRefereneTypeCodes.Add("CRN"); customerRefereneTypeCodes.Add("CRS"); customerRefereneTypeCodes.Add("CS"); customerRefereneTypeCodes.Add("CSC"); customerRefereneTypeCodes.Add("CSG"); customerRefereneTypeCodes.Add("CST"); customerRefereneTypeCodes.Add("CTS"); customerRefereneTypeCodes.Add("CU"); customerRefereneTypeCodes.Add("CV"); customerRefereneTypeCodes.Add("CW"); customerRefereneTypeCodes.Add("CX"); customerRefereneTypeCodes.Add("CY"); customerRefereneTypeCodes.Add("CYC"); customerRefereneTypeCodes.Add("CZ"); customerRefereneTypeCodes.Add("D0"); customerRefereneTypeCodes.Add("D1"); customerRefereneTypeCodes.Add("D2"); customerRefereneTypeCodes.Add("D3"); customerRefereneTypeCodes.Add("D4"); customerRefereneTypeCodes.Add("D5"); customerRefereneTypeCodes.Add("D6"); customerRefereneTypeCodes.Add("D7"); customerRefereneTypeCodes.Add("D8"); customerRefereneTypeCodes.Add("D9"); customerRefereneTypeCodes.Add("DA"); customerRefereneTypeCodes.Add("DB"); customerRefereneTypeCodes.Add("DC"); customerRefereneTypeCodes.Add("DD"); customerRefereneTypeCodes.Add("DE"); customerRefereneTypeCodes.Add("DF"); customerRefereneTypeCodes.Add("DH"); customerRefereneTypeCodes.Add("DHH"); customerRefereneTypeCodes.Add("DIS"); customerRefereneTypeCodes.Add("DL"); customerRefereneTypeCodes.Add("DNR"); customerRefereneTypeCodes.Add("DNS"); customerRefereneTypeCodes.Add("DOA"); customerRefereneTypeCodes.Add("DOC"); customerRefereneTypeCodes.Add("DOE"); customerRefereneTypeCodes.Add("DOI"); customerRefereneTypeCodes.Add("DOJ"); customerRefereneTypeCodes.Add("DOL"); customerRefereneTypeCodes.Add("DON"); customerRefereneTypeCodes.Add("DOS"); customerRefereneTypeCodes.Add("DOT"); customerRefereneTypeCodes.Add("DQ"); customerRefereneTypeCodes.Add("DR"); customerRefereneTypeCodes.Add("DRN"); customerRefereneTypeCodes.Add("DS"); customerRefereneTypeCodes.Add("DSC"); customerRefereneTypeCodes.Add("DSI"); customerRefereneTypeCodes.Add("DST"); customerRefereneTypeCodes.Add("DT"); customerRefereneTypeCodes.Add("DTS"); customerRefereneTypeCodes.Add("DUN"); customerRefereneTypeCodes.Add("DV"); customerRefereneTypeCodes.Add("DW"); customerRefereneTypeCodes.Add("DX"); customerRefereneTypeCodes.Add("DY"); customerRefereneTypeCodes.Add("DZ"); customerRefereneTypeCodes.Add("E1"); customerRefereneTypeCodes.Add("E2"); customerRefereneTypeCodes.Add("E3"); customerRefereneTypeCodes.Add("E4"); customerRefereneTypeCodes.Add("E5"); customerRefereneTypeCodes.Add("E6"); customerRefereneTypeCodes.Add("E7"); customerRefereneTypeCodes.Add("E8"); customerRefereneTypeCodes.Add("E9"); customerRefereneTypeCodes.Add("EA"); customerRefereneTypeCodes.Add("EB"); customerRefereneTypeCodes.Add("EC"); customerRefereneTypeCodes.Add("ED"); customerRefereneTypeCodes.Add("EDA"); customerRefereneTypeCodes.Add("EE"); customerRefereneTypeCodes.Add("EF"); customerRefereneTypeCodes.Add("EG"); customerRefereneTypeCodes.Add("EI"); customerRefereneTypeCodes.Add("EJ"); customerRefereneTypeCodes.Add("EK"); customerRefereneTypeCodes.Add("EL"); customerRefereneTypeCodes.Add("EM"); customerRefereneTypeCodes.Add("EN"); customerRefereneTypeCodes.Add("END"); customerRefereneTypeCodes.Add("EO"); customerRefereneTypeCodes.Add("EP"); customerRefereneTypeCodes.Add("EPA"); customerRefereneTypeCodes.Add("EPB"); customerRefereneTypeCodes.Add("ER"); customerRefereneTypeCodes.Add("ES"); customerRefereneTypeCodes.Add("ESN"); customerRefereneTypeCodes.Add("ET"); customerRefereneTypeCodes.Add("EU"); customerRefereneTypeCodes.Add("EV"); customerRefereneTypeCodes.Add("EW"); customerRefereneTypeCodes.Add("EX"); customerRefereneTypeCodes.Add("EY"); customerRefereneTypeCodes.Add("EZ"); customerRefereneTypeCodes.Add("F1"); customerRefereneTypeCodes.Add("F2"); customerRefereneTypeCodes.Add("F3"); customerRefereneTypeCodes.Add("F4"); customerRefereneTypeCodes.Add("F5"); customerRefereneTypeCodes.Add("F6"); customerRefereneTypeCodes.Add("F7"); customerRefereneTypeCodes.Add("F8"); customerRefereneTypeCodes.Add("F9"); customerRefereneTypeCodes.Add("FA"); customerRefereneTypeCodes.Add("FB"); customerRefereneTypeCodes.Add("FC"); customerRefereneTypeCodes.Add("FCN"); customerRefereneTypeCodes.Add("FD"); customerRefereneTypeCodes.Add("FE"); customerRefereneTypeCodes.Add("FF"); customerRefereneTypeCodes.Add("FG"); customerRefereneTypeCodes.Add("FH"); customerRefereneTypeCodes.Add("FI"); customerRefereneTypeCodes.Add("FJ"); customerRefereneTypeCodes.Add("FK"); customerRefereneTypeCodes.Add("FL"); customerRefereneTypeCodes.Add("FLZ"); customerRefereneTypeCodes.Add("FM"); customerRefereneTypeCodes.Add("FMP"); customerRefereneTypeCodes.Add("FN"); customerRefereneTypeCodes.Add("FND"); customerRefereneTypeCodes.Add("FO"); customerRefereneTypeCodes.Add("FP"); customerRefereneTypeCodes.Add("FQ"); customerRefereneTypeCodes.Add("FR"); customerRefereneTypeCodes.Add("FS"); customerRefereneTypeCodes.Add("FSN"); customerRefereneTypeCodes.Add("FT"); customerRefereneTypeCodes.Add("FTN"); customerRefereneTypeCodes.Add("FU"); customerRefereneTypeCodes.Add("FV"); customerRefereneTypeCodes.Add("FW"); customerRefereneTypeCodes.Add("FWC"); customerRefereneTypeCodes.Add("FX"); customerRefereneTypeCodes.Add("FY"); customerRefereneTypeCodes.Add("FZ"); customerRefereneTypeCodes.Add("G1"); customerRefereneTypeCodes.Add("G2"); customerRefereneTypeCodes.Add("G3"); customerRefereneTypeCodes.Add("G4"); customerRefereneTypeCodes.Add("G5"); customerRefereneTypeCodes.Add("G6"); customerRefereneTypeCodes.Add("G7"); customerRefereneTypeCodes.Add("G8"); customerRefereneTypeCodes.Add("G9"); customerRefereneTypeCodes.Add("GA"); customerRefereneTypeCodes.Add("GB"); customerRefereneTypeCodes.Add("GC"); customerRefereneTypeCodes.Add("GE"); customerRefereneTypeCodes.Add("GF"); customerRefereneTypeCodes.Add("GG"); customerRefereneTypeCodes.Add("GH"); customerRefereneTypeCodes.Add("GI"); customerRefereneTypeCodes.Add("GJ"); customerRefereneTypeCodes.Add("GK"); customerRefereneTypeCodes.Add("GL"); customerRefereneTypeCodes.Add("GN"); customerRefereneTypeCodes.Add("GO"); customerRefereneTypeCodes.Add("GP"); customerRefereneTypeCodes.Add("GQ"); customerRefereneTypeCodes.Add("GR"); customerRefereneTypeCodes.Add("GS"); customerRefereneTypeCodes.Add("GT"); customerRefereneTypeCodes.Add("GU"); customerRefereneTypeCodes.Add("GV"); customerRefereneTypeCodes.Add("GW"); customerRefereneTypeCodes.Add("GWS"); customerRefereneTypeCodes.Add("GX"); customerRefereneTypeCodes.Add("GY"); customerRefereneTypeCodes.Add("GZ"); customerRefereneTypeCodes.Add("H1"); customerRefereneTypeCodes.Add("H2"); customerRefereneTypeCodes.Add("H3"); customerRefereneTypeCodes.Add("H4"); customerRefereneTypeCodes.Add("H5"); customerRefereneTypeCodes.Add("H6"); customerRefereneTypeCodes.Add("H7"); customerRefereneTypeCodes.Add("H8"); customerRefereneTypeCodes.Add("H9"); customerRefereneTypeCodes.Add("HA"); customerRefereneTypeCodes.Add("HB"); customerRefereneTypeCodes.Add("HC"); customerRefereneTypeCodes.Add("HD"); customerRefereneTypeCodes.Add("HE"); customerRefereneTypeCodes.Add("HF"); customerRefereneTypeCodes.Add("HG"); customerRefereneTypeCodes.Add("HH"); customerRefereneTypeCodes.Add("HHT"); customerRefereneTypeCodes.Add("HI"); customerRefereneTypeCodes.Add("HJ"); customerRefereneTypeCodes.Add("HK"); customerRefereneTypeCodes.Add("HL"); customerRefereneTypeCodes.Add("HM"); customerRefereneTypeCodes.Add("HMB"); customerRefereneTypeCodes.Add("HN"); customerRefereneTypeCodes.Add("HO"); customerRefereneTypeCodes.Add("HP"); customerRefereneTypeCodes.Add("HPI"); customerRefereneTypeCodes.Add("HQ"); customerRefereneTypeCodes.Add("HR"); customerRefereneTypeCodes.Add("HS"); customerRefereneTypeCodes.Add("HT"); customerRefereneTypeCodes.Add("HU"); customerRefereneTypeCodes.Add("HUD"); customerRefereneTypeCodes.Add("HV"); customerRefereneTypeCodes.Add("HW"); customerRefereneTypeCodes.Add("HX"); customerRefereneTypeCodes.Add("HY"); customerRefereneTypeCodes.Add("HZ"); customerRefereneTypeCodes.Add("I1"); customerRefereneTypeCodes.Add("I2"); customerRefereneTypeCodes.Add("I3"); customerRefereneTypeCodes.Add("I4"); customerRefereneTypeCodes.Add("I5"); customerRefereneTypeCodes.Add("I7"); customerRefereneTypeCodes.Add("I8"); customerRefereneTypeCodes.Add("I9"); customerRefereneTypeCodes.Add("IB"); customerRefereneTypeCodes.Add("IC"); customerRefereneTypeCodes.Add("ICD"); customerRefereneTypeCodes.Add("ID"); customerRefereneTypeCodes.Add("IE"); customerRefereneTypeCodes.Add("IF"); customerRefereneTypeCodes.Add("IFT"); customerRefereneTypeCodes.Add("IG"); customerRefereneTypeCodes.Add("IH"); customerRefereneTypeCodes.Add("II"); customerRefereneTypeCodes.Add("IID"); customerRefereneTypeCodes.Add("IJ"); customerRefereneTypeCodes.Add("IM"); customerRefereneTypeCodes.Add("IMP"); customerRefereneTypeCodes.Add("IMS"); customerRefereneTypeCodes.Add("IN"); customerRefereneTypeCodes.Add("IND"); customerRefereneTypeCodes.Add("IO"); customerRefereneTypeCodes.Add("IP"); customerRefereneTypeCodes.Add("IQ"); customerRefereneTypeCodes.Add("IR"); customerRefereneTypeCodes.Add("IRN"); customerRefereneTypeCodes.Add("IRP"); customerRefereneTypeCodes.Add("IS"); customerRefereneTypeCodes.Add("ISC"); customerRefereneTypeCodes.Add("ISN"); customerRefereneTypeCodes.Add("ISS"); customerRefereneTypeCodes.Add("IT"); customerRefereneTypeCodes.Add("IU"); customerRefereneTypeCodes.Add("IV"); customerRefereneTypeCodes.Add("IW"); customerRefereneTypeCodes.Add("IX"); customerRefereneTypeCodes.Add("IZ"); customerRefereneTypeCodes.Add("J0"); customerRefereneTypeCodes.Add("J1"); customerRefereneTypeCodes.Add("J2"); customerRefereneTypeCodes.Add("J3"); customerRefereneTypeCodes.Add("J4"); customerRefereneTypeCodes.Add("J5"); customerRefereneTypeCodes.Add("J6"); customerRefereneTypeCodes.Add("J7"); customerRefereneTypeCodes.Add("J8"); customerRefereneTypeCodes.Add("J9"); customerRefereneTypeCodes.Add("JA"); customerRefereneTypeCodes.Add("JB"); customerRefereneTypeCodes.Add("JC"); customerRefereneTypeCodes.Add("JD"); customerRefereneTypeCodes.Add("JE"); customerRefereneTypeCodes.Add("JF"); customerRefereneTypeCodes.Add("JH"); customerRefereneTypeCodes.Add("JI"); customerRefereneTypeCodes.Add("JK"); customerRefereneTypeCodes.Add("JL"); customerRefereneTypeCodes.Add("JM"); customerRefereneTypeCodes.Add("JN"); customerRefereneTypeCodes.Add("JO"); customerRefereneTypeCodes.Add("JP"); customerRefereneTypeCodes.Add("JQ"); customerRefereneTypeCodes.Add("JR"); customerRefereneTypeCodes.Add("JS"); customerRefereneTypeCodes.Add("JT"); customerRefereneTypeCodes.Add("JU"); customerRefereneTypeCodes.Add("JV"); customerRefereneTypeCodes.Add("JW"); customerRefereneTypeCodes.Add("JX"); customerRefereneTypeCodes.Add("JY"); customerRefereneTypeCodes.Add("JZ"); customerRefereneTypeCodes.Add("K0"); customerRefereneTypeCodes.Add("K1"); customerRefereneTypeCodes.Add("K2"); customerRefereneTypeCodes.Add("K3"); customerRefereneTypeCodes.Add("K4"); customerRefereneTypeCodes.Add("K5"); customerRefereneTypeCodes.Add("K7"); customerRefereneTypeCodes.Add("K8"); customerRefereneTypeCodes.Add("K9"); customerRefereneTypeCodes.Add("KA"); customerRefereneTypeCodes.Add("KB"); customerRefereneTypeCodes.Add("KC"); customerRefereneTypeCodes.Add("KD"); customerRefereneTypeCodes.Add("KE"); customerRefereneTypeCodes.Add("KG"); customerRefereneTypeCodes.Add("KH"); customerRefereneTypeCodes.Add("KI"); customerRefereneTypeCodes.Add("KJ"); customerRefereneTypeCodes.Add("KL"); customerRefereneTypeCodes.Add("KM"); customerRefereneTypeCodes.Add("KN"); customerRefereneTypeCodes.Add("KO"); customerRefereneTypeCodes.Add("KP"); customerRefereneTypeCodes.Add("KQ"); customerRefereneTypeCodes.Add("KS"); customerRefereneTypeCodes.Add("KT"); customerRefereneTypeCodes.Add("KU"); customerRefereneTypeCodes.Add("KV"); customerRefereneTypeCodes.Add("KW"); customerRefereneTypeCodes.Add("KX"); customerRefereneTypeCodes.Add("KY"); customerRefereneTypeCodes.Add("KZ"); customerRefereneTypeCodes.Add("L1"); customerRefereneTypeCodes.Add("L2"); customerRefereneTypeCodes.Add("L3"); customerRefereneTypeCodes.Add("L4"); customerRefereneTypeCodes.Add("L5"); customerRefereneTypeCodes.Add("L6"); customerRefereneTypeCodes.Add("L7"); customerRefereneTypeCodes.Add("L8"); customerRefereneTypeCodes.Add("L9"); customerRefereneTypeCodes.Add("LA"); customerRefereneTypeCodes.Add("LB"); customerRefereneTypeCodes.Add("LC"); customerRefereneTypeCodes.Add("LD"); customerRefereneTypeCodes.Add("LE"); customerRefereneTypeCodes.Add("LEN"); customerRefereneTypeCodes.Add("LF"); customerRefereneTypeCodes.Add("LG"); customerRefereneTypeCodes.Add("LH"); customerRefereneTypeCodes.Add("LIC"); customerRefereneTypeCodes.Add("LJ"); customerRefereneTypeCodes.Add("LK"); customerRefereneTypeCodes.Add("LL"); customerRefereneTypeCodes.Add("LN"); customerRefereneTypeCodes.Add("LOI"); customerRefereneTypeCodes.Add("LP"); customerRefereneTypeCodes.Add("LQ"); customerRefereneTypeCodes.Add("LR"); customerRefereneTypeCodes.Add("LS"); customerRefereneTypeCodes.Add("LSD"); customerRefereneTypeCodes.Add("LT"); customerRefereneTypeCodes.Add("LU"); customerRefereneTypeCodes.Add("LV"); customerRefereneTypeCodes.Add("LVO"); customerRefereneTypeCodes.Add("LW"); customerRefereneTypeCodes.Add("LX"); customerRefereneTypeCodes.Add("LY"); customerRefereneTypeCodes.Add("LZ"); customerRefereneTypeCodes.Add("M1"); customerRefereneTypeCodes.Add("M2"); customerRefereneTypeCodes.Add("M3"); customerRefereneTypeCodes.Add("M5"); customerRefereneTypeCodes.Add("M6"); customerRefereneTypeCodes.Add("M7"); customerRefereneTypeCodes.Add("M8"); customerRefereneTypeCodes.Add("M9"); customerRefereneTypeCodes.Add("MA"); customerRefereneTypeCodes.Add("MBX"); customerRefereneTypeCodes.Add("MC"); customerRefereneTypeCodes.Add("MCI"); customerRefereneTypeCodes.Add("MD"); customerRefereneTypeCodes.Add("MDN"); customerRefereneTypeCodes.Add("ME"); customerRefereneTypeCodes.Add("MF"); customerRefereneTypeCodes.Add("MG"); customerRefereneTypeCodes.Add("MJ"); customerRefereneTypeCodes.Add("MK"); customerRefereneTypeCodes.Add("ML"); customerRefereneTypeCodes.Add("MM"); customerRefereneTypeCodes.Add("MN"); customerRefereneTypeCodes.Add("MP"); customerRefereneTypeCodes.Add("MQ"); customerRefereneTypeCodes.Add("MR"); customerRefereneTypeCodes.Add("MS"); customerRefereneTypeCodes.Add("MSL"); customerRefereneTypeCodes.Add("MT"); customerRefereneTypeCodes.Add("MU"); customerRefereneTypeCodes.Add("MV"); customerRefereneTypeCodes.Add("MW"); customerRefereneTypeCodes.Add("MX"); customerRefereneTypeCodes.Add("MY"); customerRefereneTypeCodes.Add("MZ"); customerRefereneTypeCodes.Add("MZO"); customerRefereneTypeCodes.Add("N0"); customerRefereneTypeCodes.Add("N1"); customerRefereneTypeCodes.Add("N2"); customerRefereneTypeCodes.Add("N3"); customerRefereneTypeCodes.Add("N4"); customerRefereneTypeCodes.Add("N5"); customerRefereneTypeCodes.Add("N6"); customerRefereneTypeCodes.Add("N7"); customerRefereneTypeCodes.Add("N8"); customerRefereneTypeCodes.Add("N9"); customerRefereneTypeCodes.Add("NA"); customerRefereneTypeCodes.Add("NAS"); customerRefereneTypeCodes.Add("NB"); customerRefereneTypeCodes.Add("NC"); customerRefereneTypeCodes.Add("ND"); customerRefereneTypeCodes.Add("NDA"); customerRefereneTypeCodes.Add("NDB"); customerRefereneTypeCodes.Add("NE"); customerRefereneTypeCodes.Add("NF"); customerRefereneTypeCodes.Add("NFC"); customerRefereneTypeCodes.Add("NFD"); customerRefereneTypeCodes.Add("NFM"); customerRefereneTypeCodes.Add("NFN"); customerRefereneTypeCodes.Add("NFS"); customerRefereneTypeCodes.Add("NG"); customerRefereneTypeCodes.Add("NH"); customerRefereneTypeCodes.Add("NI"); customerRefereneTypeCodes.Add("NJ"); customerRefereneTypeCodes.Add("NK"); customerRefereneTypeCodes.Add("NL"); customerRefereneTypeCodes.Add("NM"); customerRefereneTypeCodes.Add("NN"); customerRefereneTypeCodes.Add("NO"); customerRefereneTypeCodes.Add("NP"); customerRefereneTypeCodes.Add("NQ"); customerRefereneTypeCodes.Add("NR"); customerRefereneTypeCodes.Add("NT"); customerRefereneTypeCodes.Add("NU"); customerRefereneTypeCodes.Add("NW"); customerRefereneTypeCodes.Add("NX"); customerRefereneTypeCodes.Add("NY"); customerRefereneTypeCodes.Add("NZ"); customerRefereneTypeCodes.Add("O1"); customerRefereneTypeCodes.Add("O2"); customerRefereneTypeCodes.Add("O5"); customerRefereneTypeCodes.Add("O7"); customerRefereneTypeCodes.Add("O8"); customerRefereneTypeCodes.Add("O9"); customerRefereneTypeCodes.Add("OA"); customerRefereneTypeCodes.Add("OB"); customerRefereneTypeCodes.Add("OD"); customerRefereneTypeCodes.Add("OE"); customerRefereneTypeCodes.Add("OF"); customerRefereneTypeCodes.Add("OG"); customerRefereneTypeCodes.Add("OH"); customerRefereneTypeCodes.Add("OJ"); customerRefereneTypeCodes.Add("OK"); customerRefereneTypeCodes.Add("OL"); customerRefereneTypeCodes.Add("OM"); customerRefereneTypeCodes.Add("OS"); customerRefereneTypeCodes.Add("OT"); customerRefereneTypeCodes.Add("OV"); customerRefereneTypeCodes.Add("OX"); customerRefereneTypeCodes.Add("OZ"); customerRefereneTypeCodes.Add("P1"); customerRefereneTypeCodes.Add("P2"); customerRefereneTypeCodes.Add("P3"); customerRefereneTypeCodes.Add("P4"); customerRefereneTypeCodes.Add("P5"); customerRefereneTypeCodes.Add("P6"); customerRefereneTypeCodes.Add("P7"); customerRefereneTypeCodes.Add("P9"); customerRefereneTypeCodes.Add("PA"); customerRefereneTypeCodes.Add("PAC"); customerRefereneTypeCodes.Add("PAN"); customerRefereneTypeCodes.Add("PAP"); customerRefereneTypeCodes.Add("PB"); customerRefereneTypeCodes.Add("PC"); customerRefereneTypeCodes.Add("PCC"); customerRefereneTypeCodes.Add("PCN"); customerRefereneTypeCodes.Add("PD"); customerRefereneTypeCodes.Add("PDL"); customerRefereneTypeCodes.Add("PE"); customerRefereneTypeCodes.Add("PF"); customerRefereneTypeCodes.Add("PG"); customerRefereneTypeCodes.Add("PGC"); customerRefereneTypeCodes.Add("PGN"); customerRefereneTypeCodes.Add("PGS"); customerRefereneTypeCodes.Add("PH"); customerRefereneTypeCodes.Add("PHC"); customerRefereneTypeCodes.Add("PID"); customerRefereneTypeCodes.Add("PIN"); customerRefereneTypeCodes.Add("PJ"); customerRefereneTypeCodes.Add("PK"); customerRefereneTypeCodes.Add("PLA"); customerRefereneTypeCodes.Add("PLN"); customerRefereneTypeCodes.Add("PM"); customerRefereneTypeCodes.Add("PMN"); customerRefereneTypeCodes.Add("PN"); customerRefereneTypeCodes.Add("PNN"); customerRefereneTypeCodes.Add("POL"); customerRefereneTypeCodes.Add("PQ"); customerRefereneTypeCodes.Add("PR"); customerRefereneTypeCodes.Add("PRS"); customerRefereneTypeCodes.Add("PSI"); customerRefereneTypeCodes.Add("PSL"); customerRefereneTypeCodes.Add("PSM"); customerRefereneTypeCodes.Add("PSN"); customerRefereneTypeCodes.Add("PT"); customerRefereneTypeCodes.Add("PTC"); customerRefereneTypeCodes.Add("PV"); customerRefereneTypeCodes.Add("PW"); customerRefereneTypeCodes.Add("PWC"); customerRefereneTypeCodes.Add("PWS"); customerRefereneTypeCodes.Add("PX"); customerRefereneTypeCodes.Add("PY"); customerRefereneTypeCodes.Add("PZ"); customerRefereneTypeCodes.Add("Q2"); customerRefereneTypeCodes.Add("Q3"); customerRefereneTypeCodes.Add("Q4"); customerRefereneTypeCodes.Add("Q5"); customerRefereneTypeCodes.Add("Q6"); customerRefereneTypeCodes.Add("Q7"); customerRefereneTypeCodes.Add("Q8"); customerRefereneTypeCodes.Add("Q9"); customerRefereneTypeCodes.Add("QA"); customerRefereneTypeCodes.Add("QB"); customerRefereneTypeCodes.Add("QC"); customerRefereneTypeCodes.Add("QD"); customerRefereneTypeCodes.Add("QE"); customerRefereneTypeCodes.Add("QF"); customerRefereneTypeCodes.Add("QG"); customerRefereneTypeCodes.Add("QH"); customerRefereneTypeCodes.Add("QI"); customerRefereneTypeCodes.Add("QK"); customerRefereneTypeCodes.Add("QM"); customerRefereneTypeCodes.Add("QO"); customerRefereneTypeCodes.Add("QP"); customerRefereneTypeCodes.Add("QQ"); customerRefereneTypeCodes.Add("QR"); customerRefereneTypeCodes.Add("QS"); customerRefereneTypeCodes.Add("QT"); customerRefereneTypeCodes.Add("QU"); customerRefereneTypeCodes.Add("QV"); customerRefereneTypeCodes.Add("QW"); customerRefereneTypeCodes.Add("QX"); customerRefereneTypeCodes.Add("QZ"); customerRefereneTypeCodes.Add("R0"); customerRefereneTypeCodes.Add("R1"); customerRefereneTypeCodes.Add("R2"); customerRefereneTypeCodes.Add("R3"); customerRefereneTypeCodes.Add("R4"); customerRefereneTypeCodes.Add("R5"); customerRefereneTypeCodes.Add("R6"); customerRefereneTypeCodes.Add("R7"); customerRefereneTypeCodes.Add("R8"); customerRefereneTypeCodes.Add("R9"); customerRefereneTypeCodes.Add("RA"); customerRefereneTypeCodes.Add("RAA"); customerRefereneTypeCodes.Add("RAN"); customerRefereneTypeCodes.Add("RC"); customerRefereneTypeCodes.Add("RD"); customerRefereneTypeCodes.Add("REC"); customerRefereneTypeCodes.Add("RF"); customerRefereneTypeCodes.Add("RG"); customerRefereneTypeCodes.Add("RGI"); customerRefereneTypeCodes.Add("RH"); customerRefereneTypeCodes.Add("RIG"); customerRefereneTypeCodes.Add("RJ"); customerRefereneTypeCodes.Add("RK"); customerRefereneTypeCodes.Add("RL"); customerRefereneTypeCodes.Add("RM"); customerRefereneTypeCodes.Add("RO"); customerRefereneTypeCodes.Add("RP"); customerRefereneTypeCodes.Add("RPP"); customerRefereneTypeCodes.Add("RPT"); customerRefereneTypeCodes.Add("RQ"); customerRefereneTypeCodes.Add("RR"); customerRefereneTypeCodes.Add("RRS"); customerRefereneTypeCodes.Add("RS"); customerRefereneTypeCodes.Add("RT"); customerRefereneTypeCodes.Add("RU"); customerRefereneTypeCodes.Add("RV"); customerRefereneTypeCodes.Add("RW"); customerRefereneTypeCodes.Add("RX"); customerRefereneTypeCodes.Add("RY"); customerRefereneTypeCodes.Add("S0"); customerRefereneTypeCodes.Add("S1"); customerRefereneTypeCodes.Add("S2"); customerRefereneTypeCodes.Add("S3"); customerRefereneTypeCodes.Add("S4"); customerRefereneTypeCodes.Add("S6"); customerRefereneTypeCodes.Add("S7"); customerRefereneTypeCodes.Add("S8"); customerRefereneTypeCodes.Add("S9"); customerRefereneTypeCodes.Add("SA"); customerRefereneTypeCodes.Add("SB"); customerRefereneTypeCodes.Add("SBN"); customerRefereneTypeCodes.Add("SCA"); customerRefereneTypeCodes.Add("SD"); customerRefereneTypeCodes.Add("SE"); customerRefereneTypeCodes.Add("SEK"); customerRefereneTypeCodes.Add("SES"); customerRefereneTypeCodes.Add("SF"); customerRefereneTypeCodes.Add("SG"); customerRefereneTypeCodes.Add("SH"); customerRefereneTypeCodes.Add("SHL"); customerRefereneTypeCodes.Add("SJ"); customerRefereneTypeCodes.Add("SK"); customerRefereneTypeCodes.Add("SL"); customerRefereneTypeCodes.Add("SM"); customerRefereneTypeCodes.Add("SNH"); customerRefereneTypeCodes.Add("SNV"); customerRefereneTypeCodes.Add("SP"); customerRefereneTypeCodes.Add("SPL"); customerRefereneTypeCodes.Add("SPN"); customerRefereneTypeCodes.Add("SR"); customerRefereneTypeCodes.Add("SS"); customerRefereneTypeCodes.Add("ST"); customerRefereneTypeCodes.Add("STB"); customerRefereneTypeCodes.Add("STR"); customerRefereneTypeCodes.Add("SU"); customerRefereneTypeCodes.Add("SUB"); customerRefereneTypeCodes.Add("SUO"); customerRefereneTypeCodes.Add("SV"); customerRefereneTypeCodes.Add("SW"); customerRefereneTypeCodes.Add("SX"); customerRefereneTypeCodes.Add("SY"); customerRefereneTypeCodes.Add("SZ"); customerRefereneTypeCodes.Add("T0"); customerRefereneTypeCodes.Add("T1"); customerRefereneTypeCodes.Add("T2"); customerRefereneTypeCodes.Add("T3"); customerRefereneTypeCodes.Add("T4"); customerRefereneTypeCodes.Add("T5"); customerRefereneTypeCodes.Add("T6"); customerRefereneTypeCodes.Add("T7"); customerRefereneTypeCodes.Add("T8"); customerRefereneTypeCodes.Add("T9"); customerRefereneTypeCodes.Add("TA"); customerRefereneTypeCodes.Add("TB"); customerRefereneTypeCodes.Add("TC"); customerRefereneTypeCodes.Add("TD"); customerRefereneTypeCodes.Add("TE"); customerRefereneTypeCodes.Add("TG"); customerRefereneTypeCodes.Add("TI"); customerRefereneTypeCodes.Add("TIP"); customerRefereneTypeCodes.Add("TJ"); customerRefereneTypeCodes.Add("TK"); customerRefereneTypeCodes.Add("TL"); customerRefereneTypeCodes.Add("TM"); customerRefereneTypeCodes.Add("TOC"); customerRefereneTypeCodes.Add("TPN"); customerRefereneTypeCodes.Add("TQ"); customerRefereneTypeCodes.Add("TR"); customerRefereneTypeCodes.Add("TSN"); customerRefereneTypeCodes.Add("TT"); customerRefereneTypeCodes.Add("TU"); customerRefereneTypeCodes.Add("TV"); customerRefereneTypeCodes.Add("TW"); customerRefereneTypeCodes.Add("TX"); customerRefereneTypeCodes.Add("TY"); customerRefereneTypeCodes.Add("TZ"); customerRefereneTypeCodes.Add("U0"); customerRefereneTypeCodes.Add("U1"); customerRefereneTypeCodes.Add("U2"); customerRefereneTypeCodes.Add("U3"); customerRefereneTypeCodes.Add("U4"); customerRefereneTypeCodes.Add("U5"); customerRefereneTypeCodes.Add("U6"); customerRefereneTypeCodes.Add("U8"); customerRefereneTypeCodes.Add("U9"); customerRefereneTypeCodes.Add("UA"); customerRefereneTypeCodes.Add("UB"); customerRefereneTypeCodes.Add("UC"); customerRefereneTypeCodes.Add("UD"); customerRefereneTypeCodes.Add("UE"); customerRefereneTypeCodes.Add("UF"); customerRefereneTypeCodes.Add("UG"); customerRefereneTypeCodes.Add("UH"); customerRefereneTypeCodes.Add("UI"); customerRefereneTypeCodes.Add("UJ"); customerRefereneTypeCodes.Add("UK"); customerRefereneTypeCodes.Add("UL"); customerRefereneTypeCodes.Add("UM"); customerRefereneTypeCodes.Add("UN"); customerRefereneTypeCodes.Add("UO"); customerRefereneTypeCodes.Add("UQ"); customerRefereneTypeCodes.Add("UR"); customerRefereneTypeCodes.Add("URL"); customerRefereneTypeCodes.Add("US"); customerRefereneTypeCodes.Add("UT"); customerRefereneTypeCodes.Add("UU"); customerRefereneTypeCodes.Add("UV"); customerRefereneTypeCodes.Add("UW"); customerRefereneTypeCodes.Add("UX"); customerRefereneTypeCodes.Add("UY"); customerRefereneTypeCodes.Add("UZ"); customerRefereneTypeCodes.Add("V0"); customerRefereneTypeCodes.Add("V1"); customerRefereneTypeCodes.Add("V2"); customerRefereneTypeCodes.Add("V3"); customerRefereneTypeCodes.Add("V4"); customerRefereneTypeCodes.Add("V5"); customerRefereneTypeCodes.Add("V6"); customerRefereneTypeCodes.Add("V7"); customerRefereneTypeCodes.Add("V8"); customerRefereneTypeCodes.Add("V9"); customerRefereneTypeCodes.Add("VB"); customerRefereneTypeCodes.Add("VC"); customerRefereneTypeCodes.Add("VF"); customerRefereneTypeCodes.Add("VG"); customerRefereneTypeCodes.Add("VH"); customerRefereneTypeCodes.Add("VI"); customerRefereneTypeCodes.Add("VJ"); customerRefereneTypeCodes.Add("VK"); customerRefereneTypeCodes.Add("VL"); customerRefereneTypeCodes.Add("VM"); customerRefereneTypeCodes.Add("VN"); customerRefereneTypeCodes.Add("VO"); customerRefereneTypeCodes.Add("VP"); customerRefereneTypeCodes.Add("VQ"); customerRefereneTypeCodes.Add("VS"); customerRefereneTypeCodes.Add("VT"); customerRefereneTypeCodes.Add("VU"); customerRefereneTypeCodes.Add("VV"); customerRefereneTypeCodes.Add("VW"); customerRefereneTypeCodes.Add("VX"); customerRefereneTypeCodes.Add("VY"); customerRefereneTypeCodes.Add("VZ"); customerRefereneTypeCodes.Add("W1"); customerRefereneTypeCodes.Add("W2"); customerRefereneTypeCodes.Add("W3"); customerRefereneTypeCodes.Add("W4"); customerRefereneTypeCodes.Add("W5"); customerRefereneTypeCodes.Add("W6"); customerRefereneTypeCodes.Add("W7"); customerRefereneTypeCodes.Add("W8"); customerRefereneTypeCodes.Add("W9"); customerRefereneTypeCodes.Add("WA"); customerRefereneTypeCodes.Add("WB"); customerRefereneTypeCodes.Add("WC"); customerRefereneTypeCodes.Add("WCS"); customerRefereneTypeCodes.Add("WD"); customerRefereneTypeCodes.Add("WDR"); customerRefereneTypeCodes.Add("WE"); customerRefereneTypeCodes.Add("WF"); customerRefereneTypeCodes.Add("WG"); customerRefereneTypeCodes.Add("WH"); customerRefereneTypeCodes.Add("WI"); customerRefereneTypeCodes.Add("WJ"); customerRefereneTypeCodes.Add("WK"); customerRefereneTypeCodes.Add("WL"); customerRefereneTypeCodes.Add("WM"); customerRefereneTypeCodes.Add("WN"); customerRefereneTypeCodes.Add("WO"); customerRefereneTypeCodes.Add("WP"); customerRefereneTypeCodes.Add("WQ"); customerRefereneTypeCodes.Add("WR"); customerRefereneTypeCodes.Add("WS"); customerRefereneTypeCodes.Add("WT"); customerRefereneTypeCodes.Add("WU"); customerRefereneTypeCodes.Add("WV"); customerRefereneTypeCodes.Add("WW"); customerRefereneTypeCodes.Add("WX"); customerRefereneTypeCodes.Add("WY"); customerRefereneTypeCodes.Add("WZ"); customerRefereneTypeCodes.Add("X0"); customerRefereneTypeCodes.Add("X1"); customerRefereneTypeCodes.Add("X2"); customerRefereneTypeCodes.Add("X3"); customerRefereneTypeCodes.Add("X4"); customerRefereneTypeCodes.Add("X5"); customerRefereneTypeCodes.Add("X6"); customerRefereneTypeCodes.Add("X7"); customerRefereneTypeCodes.Add("X8"); customerRefereneTypeCodes.Add("X9"); customerRefereneTypeCodes.Add("XA"); customerRefereneTypeCodes.Add("XB"); customerRefereneTypeCodes.Add("XC"); customerRefereneTypeCodes.Add("XD"); customerRefereneTypeCodes.Add("XE"); customerRefereneTypeCodes.Add("XF"); customerRefereneTypeCodes.Add("XG"); customerRefereneTypeCodes.Add("XH"); customerRefereneTypeCodes.Add("XI"); customerRefereneTypeCodes.Add("XJ"); customerRefereneTypeCodes.Add("XK"); customerRefereneTypeCodes.Add("XL"); customerRefereneTypeCodes.Add("XM"); customerRefereneTypeCodes.Add("XN"); customerRefereneTypeCodes.Add("XO"); customerRefereneTypeCodes.Add("XP"); customerRefereneTypeCodes.Add("XQ"); customerRefereneTypeCodes.Add("XR"); customerRefereneTypeCodes.Add("XS"); customerRefereneTypeCodes.Add("XT"); customerRefereneTypeCodes.Add("XU"); customerRefereneTypeCodes.Add("XV"); customerRefereneTypeCodes.Add("XW"); customerRefereneTypeCodes.Add("XX"); customerRefereneTypeCodes.Add("XY"); customerRefereneTypeCodes.Add("XZ"); customerRefereneTypeCodes.Add("Y0"); customerRefereneTypeCodes.Add("Y1"); customerRefereneTypeCodes.Add("Y2"); customerRefereneTypeCodes.Add("Y3"); customerRefereneTypeCodes.Add("Y4"); customerRefereneTypeCodes.Add("Y5"); customerRefereneTypeCodes.Add("Y6"); customerRefereneTypeCodes.Add("Y7"); customerRefereneTypeCodes.Add("Y8"); customerRefereneTypeCodes.Add("Y9"); customerRefereneTypeCodes.Add("YA"); customerRefereneTypeCodes.Add("YB"); customerRefereneTypeCodes.Add("YC"); customerRefereneTypeCodes.Add("YD"); customerRefereneTypeCodes.Add("YE"); customerRefereneTypeCodes.Add("YF"); customerRefereneTypeCodes.Add("YH"); customerRefereneTypeCodes.Add("YI"); customerRefereneTypeCodes.Add("YJ"); customerRefereneTypeCodes.Add("YK"); customerRefereneTypeCodes.Add("YL"); customerRefereneTypeCodes.Add("YM"); customerRefereneTypeCodes.Add("YN"); customerRefereneTypeCodes.Add("YO"); customerRefereneTypeCodes.Add("YP"); customerRefereneTypeCodes.Add("YQ"); customerRefereneTypeCodes.Add("YR"); customerRefereneTypeCodes.Add("YS"); customerRefereneTypeCodes.Add("YT"); customerRefereneTypeCodes.Add("YV"); customerRefereneTypeCodes.Add("YW"); customerRefereneTypeCodes.Add("YX"); customerRefereneTypeCodes.Add("YY"); customerRefereneTypeCodes.Add("YZ"); customerRefereneTypeCodes.Add("Z1"); customerRefereneTypeCodes.Add("Z2"); customerRefereneTypeCodes.Add("Z3"); customerRefereneTypeCodes.Add("Z4"); customerRefereneTypeCodes.Add("Z5"); customerRefereneTypeCodes.Add("Z6"); customerRefereneTypeCodes.Add("Z7"); customerRefereneTypeCodes.Add("Z8"); customerRefereneTypeCodes.Add("Z9"); customerRefereneTypeCodes.Add("ZA"); customerRefereneTypeCodes.Add("ZB"); customerRefereneTypeCodes.Add("ZC"); customerRefereneTypeCodes.Add("ZD"); customerRefereneTypeCodes.Add("ZE"); customerRefereneTypeCodes.Add("ZF"); customerRefereneTypeCodes.Add("ZH"); customerRefereneTypeCodes.Add("ZI"); customerRefereneTypeCodes.Add("ZJ"); customerRefereneTypeCodes.Add("ZK"); customerRefereneTypeCodes.Add("ZL"); customerRefereneTypeCodes.Add("ZN"); customerRefereneTypeCodes.Add("ZO"); customerRefereneTypeCodes.Add("ZP"); customerRefereneTypeCodes.Add("ZQ"); customerRefereneTypeCodes.Add("ZR"); customerRefereneTypeCodes.Add("ZS"); customerRefereneTypeCodes.Add("ZT"); customerRefereneTypeCodes.Add("ZU"); customerRefereneTypeCodes.Add("ZV"); customerRefereneTypeCodes.Add("ZW"); customerRefereneTypeCodes.Add("ZX"); customerRefereneTypeCodes.Add("ZY"); customerRefereneTypeCodes.Add("TRN"); customerRefereneTypeCodes.Add("TCE"); customerRefereneTypeCodes.Add("TCO"); customerRefereneTypeCodes.Add("UUID"
        }

        void InitializeStopSpecialRequirements()
        {
            stopSpecialRequirements.Add("LFD: Liftgate Delivery");
            stopSpecialRequirements.Add("LFP: Liftgate Pickup");
            stopSpecialRequirements.Add("RSD: Residential Delivery");
            stopSpecialRequirements.Add("RSP: Residential Pickup");
        }
        public ArrayList customerRefereneTypeCodes;
        public ArrayList stopRefereneTypeCodes;
        public ArrayList equipmentCatagoryCodes;
        public ArrayList typeOfStops;
        public ArrayList paymentMethods;
        public ArrayList applicationSources;
        public ArrayList equipmentTypeCodes;
        public ArrayList weightUomCodes;
        public ArrayList phoneNumberTypes;
        public ArrayList lengthUomCodes;
        public ArrayList widthUomCodes;
        public ArrayList heightUomCodes;
        public ArrayList unitTypeCodes;
        public ArrayList packageTypeCodes;
        public ArrayList additionalServices;
        public ArrayList stopSpecialRequirements;

    }
}
