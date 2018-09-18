using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WhiteLabel
{
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();
        }

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			Navigation.PopAsync();
			Navigation.PopAsync();
		}
    }
}
