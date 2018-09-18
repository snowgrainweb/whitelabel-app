using System;
using System.Collections.Generic;
using Xamarin.Forms;
using paytm;

namespace WhiteLabel
{
    public partial class ProductDetail : ContentPage
    {
        public ProductDetail()
        {
			
            InitializeComponent();
            
        }

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			string orderid = System.Guid.NewGuid().ToString();
            String merchantKey = "TDhoW2TrdXK&VLD_";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", "Epsilo89332283489229");
            parameters.Add("CHANNEL_ID", "WAP");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", "vellore.balaji@gmail.com");
            parameters.Add("MOBILE_NO", "7777777777");
            parameters.Add("CUST_ID", "vellorebalajigmailcom");
            parameters.Add("ORDER_ID", orderid);
            parameters.Add("TXN_AMOUNT", "200");
            //parameters.Add("CALLBACK_URL", "url"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid;
            string outputHTML = @"<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            var browser = new WebView();
            browser.HeightRequest = 300;
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = outputHTML;
            browser.Source = htmlSource;
            Content = browser;
            browser.Navigating += Browser_Navigating;
			browser.Navigated += Browser_Navigated;
		}

		protected override async void OnAppearing()
		{
			
			//Navigation.PushAsync(new OrderPage());
		}

		void Browser_Navigated(object sender, WebNavigatedEventArgs e)
		{
			if (e.Url.Contains("southindia"))
            {
                ((WebView)sender).IsVisible = false;
                Navigation.PushAsync(new OrderPage());
            }
		}


		void Browser_Navigating(object sender, WebNavigatingEventArgs e)
		{
			if(e.Url.Contains("southindia")) {
				((WebView)sender).IsVisible = false;
				Navigation.PushAsync(new OrderPage());
			}
		}

    }
}
