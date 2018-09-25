using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace WhiteLabel
{
    public partial class SettingsPage : ContentPage
    {
		public ObservableCollection<WhiteLabel.MasterPageItem> masterpageItem { get; set; }
        public SettingsPage()
        {
            InitializeComponent();
			String Text = "Login";

            masterpageItem = new ObservableCollection<WhiteLabel.MasterPageItem>();
            masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Change Password",
                TargetType = typeof(WhiteLabel.HomePage),
                IconSource = "https://png.icons8.com/material/50/FFFFFF/home-page.png"
            });

            masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Auto Login",
                TargetType = typeof(WhiteLabel.Articles),
                IconSource = "https://png.icons8.com/material/50/FFFFFF/news-filled.png"
            });

            masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Edit Profile",
                TargetType = typeof(WhiteLabel.ProductsPage),
                IconSource = "https://png.icons8.com/material/50/FFFFFF/box.png"
            });

            masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Quick Tipes",
                TargetType = typeof(WhiteLabel.OffersPage),
                IconSource = "https://png.icons8.com/material/50/FFFFFF/discount.png"
            });

            masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "About White Label",
                TargetType = typeof(WhiteLabel.HomePage),
                IconSource = "https://png.icons8.com/material/50/FFFFFF/contacts.png"
            });

            masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Legal",
                TargetType = typeof(WhiteLabel.HomePage),
                IconSource = "https://png.icons8.com/material/50/FFFFFF/privacy.png"
            });

            masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Logout",
                TargetType = typeof(WhiteLabel.HomePage),
                IconSource = "https://png.icons8.com/metro/50/FFFFFF/bank-cards.png"
            });
                        

            listView.ItemsSource = masterpageItem;
        }
    }
}
