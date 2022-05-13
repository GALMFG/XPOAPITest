using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XPOAPITest
{
    public class Quote
    {
        public string quoteId { get; set; }

        public string carrierCode { get; set; }

        public string carrierName { get; set; }

        public string serviceLevel { get; set; }

        public string pickupDate { get; set; }

        public string deliveryDate { get; set; }
        public string movementType { get; set; }


        public double lineHaul { get; set; }

        public double fsc { get; set; }

        public string scac { get; set; }
        public int estimatedTransitTime { get; set; }

        public IList<Accessorial> accessorials { get; set; }

        public IList<Rate> rateList { get; set; }

        public IList<Stop> legs { get; set; }

        public double accessorialsTotalCost { get; set; }

        public double subTotal { get; set; }

        public double totalCost { get; set; }

        public bool isNmfcCarrier { get; set; }

        public bool isContractRate { get; set; }

        public double totalDistance { get; set; }
        public string currencyCode { get; set; }

        public string expiresOn { get; set; }
        public string createdOn { get; set; }

        public TabPage addTab( String  tabName)
        {
            TabPage tabPageQuote = new TabPage(tabName);
            tabPageQuote.Name = "tabQuote" + this.carrierName.Replace(" ","");

            Label lblCarrierName = new Label();
            lblCarrierName.Name = "lblCarrierName";
            lblCarrierName.Size = new Size(190, 30);
            lblCarrierName.Location = new Point(20, 20);
            lblCarrierName.Text = "Carrier Name";
            tabPageQuote.Controls.Add(lblCarrierName);

            if (this.carrierName != null)
            {
                Label lblCarrierNameValue = new Label();
                lblCarrierNameValue.Size = new Size(140, 30);
                lblCarrierNameValue.Location = new Point(210, 20);
                lblCarrierNameValue.Text = this.carrierName;
                lblCarrierNameValue.Name = "lblCarrierNameValue";
                tabPageQuote.Controls.Add(lblCarrierNameValue);
            }

            Label lblQuoteId = new Label();
            lblQuoteId.Size = new Size(190, 30);
            lblQuoteId.Location = new Point(470, 20);
            lblQuoteId.Text = "Quote Id";
            tabPageQuote.Controls.Add(lblQuoteId);
            if (this.quoteId != null)
            {
                Label lblQuoteIdValue = new Label();
                lblQuoteIdValue.Size = new Size(140, 30);
                lblQuoteIdValue.Location = new Point(670, 20);
                lblQuoteIdValue.Text = this.quoteId;
                lblQuoteIdValue.Name = "lblQuoteIdValue";
                tabPageQuote.Controls.Add(lblQuoteIdValue);
            }
            Label lblPickupDate = new Label();
            lblPickupDate.Size = new Size(190, 30);
            lblPickupDate.Location = new Point(20, 60);
            lblPickupDate.Text = "Pickup Date";
            tabPageQuote.Controls.Add(lblPickupDate);
            if (this.pickupDate != null)
            {
                Label lblPickupDateValue = new Label();
                lblPickupDateValue.Size = new Size(140, 30);
                lblPickupDateValue.Location = new Point(210, 60);
                lblPickupDateValue.Text = this.pickupDate;
                tabPageQuote.Controls.Add(lblPickupDateValue);
            }
            Label lblDeliveryDate = new Label();
            lblDeliveryDate.Size = new Size(190, 30);
            lblDeliveryDate.Location = new Point(470, 60);
            lblDeliveryDate.Text = "Delivery Date";
            tabPageQuote.Controls.Add(lblDeliveryDate);
            if (this.deliveryDate != null)
            {
                Label lblDeliveryValue = new Label();
                lblDeliveryValue.Size = new Size(140, 30);
                lblDeliveryValue.Location = new Point(670, 60);
                lblDeliveryValue.Text = this.deliveryDate;
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
            lblEstimatedTransitTimeValue.Text = this.estimatedTransitTime.ToString().Trim();
            tabPageQuote.Controls.Add(lblEstimatedTransitTimeValue);

            Label lblFSC = new Label();
            lblFSC.Size = new Size(190, 30);
            lblFSC.Location = new Point(20, 140);
            lblFSC.Text = "FSC";
            tabPageQuote.Controls.Add(lblFSC);

            Label lblFSCValue = new Label();
            lblFSCValue.Size = new Size(140, 30);
            lblFSCValue.Location = new Point(210, 140);
            lblFSCValue.Text = this.fsc.ToString().Trim();
            tabPageQuote.Controls.Add(lblFSCValue);

            Label lblSCAC = new Label();
            lblSCAC.Size = new Size(190, 30);
            lblSCAC.Location = new Point(470, 140);
            lblSCAC.Text = "SCAC";
            tabPageQuote.Controls.Add(lblSCAC);
            if (this.scac != null)
            {
                Label lblSCACValue = new Label();
                lblSCACValue.Size = new Size(140, 30);
                lblSCACValue.Location = new Point(670, 140);
                lblSCACValue.Text = this.scac.ToString().Trim();
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
            lblLineHaulValue.Text = this.lineHaul.ToString().Trim();
            tabPageQuote.Controls.Add(lblLineHaulValue);

            Label lblTotalDistance = new Label();
            lblTotalDistance.Size = new Size(190, 30);
            lblTotalDistance.Location = new Point(470, 180);
            lblTotalDistance.Text = "Total Distance";
            tabPageQuote.Controls.Add(lblTotalDistance);

            Label lblTotalDistanceValue = new Label();
            lblTotalDistanceValue.Size = new Size(140, 30);
            lblTotalDistanceValue.Location = new Point(670, 180);
            lblTotalDistanceValue.Text = this.totalDistance.ToString().Trim();
            tabPageQuote.Controls.Add(lblTotalDistanceValue);

            Label lblMovementType = new Label();
            lblMovementType.Size = new Size(190, 30);
            lblMovementType.Location = new Point(20, 220);
            lblMovementType.Text = "Movement Type";
            tabPageQuote.Controls.Add(lblMovementType);
            if (this.movementType != null)
            {
                Label lblMovementTypeValue = new Label();
                lblMovementTypeValue.Size = new Size(140, 30);
                lblMovementTypeValue.Location = new Point(210, 220);
                lblMovementTypeValue.Text = this.movementType.Trim();
                //lblMovementTypeValue.Text = "DIRECT";
                tabPageQuote.Controls.Add(lblMovementTypeValue);
            }
            Label lblServiceLevel = new Label();
            lblServiceLevel.Size = new Size(190, 30);
            lblServiceLevel.Location = new Point(470, 220);
            lblServiceLevel.Text = "Service Level";
            tabPageQuote.Controls.Add(lblServiceLevel);
            if (this.serviceLevel != null)
            {
                Label lblServiceLevelValue = new Label();
                lblServiceLevelValue.Size = new Size(140, 30);
                lblServiceLevelValue.Location = new Point(670, 220);
                lblServiceLevelValue.Text = this.serviceLevel;
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
            lblSubTotalValue.Text = this.subTotal.ToString().Trim();
            
            tabPageQuote.Controls.Add(lblSubTotalValue);

            Label lblTotalCost = new Label();
            lblTotalCost.Size = new Size(190, 30);
            lblTotalCost.Location = new Point(470, 260);
            lblTotalCost.Text = "Total Cost";
            
            tabPageQuote.Controls.Add(lblTotalCost);

            Label lblTotalCostValue = new Label();
            lblTotalCostValue.Size = new Size(140, 30);
            lblTotalCostValue.Location = new Point(670, 260);
            lblTotalCostValue.Text = this.totalCost.ToString().Trim();
            lblTotalCostValue.Name = "lblTotalCostValue";
            tabPageQuote.Controls.Add(lblTotalCostValue);

            RadioButton rdoIsContractRate = new RadioButton();
            rdoIsContractRate.Size = new Size(190, 30);
            rdoIsContractRate.Location = new Point(20, 300);
            rdoIsContractRate.Text = "Is Contract Rate";
            tabPageQuote.Controls.Add(rdoIsContractRate);
            
                if (this.isContractRate) { rdoIsContractRate.Checked = true; }

            RadioButton rdoIsNMFCCarrier = new RadioButton();
            rdoIsNMFCCarrier.Size = new Size(190, 30);
            rdoIsNMFCCarrier.Location = new Point(470, 300);
            rdoIsNMFCCarrier.Text = "Is NMFC Carrier";
            tabPageQuote.Controls.Add(rdoIsNMFCCarrier);
            
                if (this.isNmfcCarrier)
                    rdoIsNMFCCarrier.Checked = true;
            DataGridView gvRateList = new DataGridView();
            gvRateList.Size = new Size(1000, 250);
           
            gvRateList.Location = new Point(20, 340);


            tabPageQuote.Controls.Add(gvRateList);

            DataGridView gvAccessorialsList = new DataGridView();
            gvAccessorialsList.Size = new Size(1000, 300);
            gvAccessorialsList.Location = new Point(20, 600);


            tabPageQuote.Controls.Add(gvAccessorialsList);




            DataTable dt = new DataTable();

            dt.Columns.Add("Code");
            dt.Columns.Add("Cost");
            dt.Columns.Add("CurrencyCode");
            dt.Columns.Add("IsOptional");
            dt.Columns.Add("Name");
            dt.Columns.Add("TypeCode");


            IList<Rate> rates = this.rateList;

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


            IList<Accessorial> accessorials = this.accessorials;

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

    }
}
