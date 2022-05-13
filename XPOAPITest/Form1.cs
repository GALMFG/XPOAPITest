using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            XPO xpo = new XPO();
            //xpo.createToken();
              quoteResponse = await xpo.getQuote();




            IList<PriceResponse> priceSearchResponse = quoteResponse.priceSearchResponse;
            if (priceSearchResponse != null)
            {
                foreach(PriceResponse priceResponse in priceSearchResponse)
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
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            XPO xpo = new XPO();
            //xpo.createToken();
            orderResponse = await xpo.convertToOrder(tabQuotes.SelectedTab);



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
                tabControlMain.SelectedIndex = 1;
            }
        }

        private void tabPageQuotes_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
