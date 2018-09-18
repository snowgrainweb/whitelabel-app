using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WhiteLabel
{
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }
        
		void Handle_Clicked(object sender, System.EventArgs e)
		{
			Device.OpenUri(new Uri("mailto:support@whitelabel.com"));
		}

		void Handle_Clicked_1(object sender, System.EventArgs e)
		{
			Device.OpenUri(new Uri("tel:1800-123-987"));
		}
		protected override bool OnBackButtonPressed()
        {
            MasterDetailPage fpm = new MasterDetailPage();


            var NavStyle = new Style(typeof(NavigationPage))
            {
                Setters = {
                        new Setter{Property= NavigationPage.BarBackgroundColorProperty, Value="#37484d"},
                        new Setter{Property=NavigationPage.BarTextColorProperty, Value="White"}
                    }
            };
            Resources = new ResourceDictionary();
            Resources.Add(NavStyle);
            fpm.Resources = Resources;
            fpm.Master = new MasterPage(); // You have to create a Master ContentPage()
            fpm.Detail = new MainPage(); // You have to create a Detail ContenPage()
            Application.Current.MainPage = fpm;
            return true;
        }
    }
}
