using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using System.Globalization;
using Newtonsoft.Json;  
using System.Collections.ObjectModel; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WhiteLabel
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }
		private string Url = "https://whitelabel-dxebr.d.epsilon.com/api/sitecore/mobileapp/navigation?language=";
		public ObservableCollection<WhiteLabel.MasterPageItem> masterpageItem { get; set; }
		private readonly HttpClient client = new HttpClient();
		public bool IsInProgress = true;
		public MasterPage()
        {
            InitializeComponent();                      
			String Text = "Login";
            
			masterpageItem = new ObservableCollection<WhiteLabel.MasterPageItem>();
			masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Home",
				TargetType = typeof(WhiteLabel.HomePage),
				IconSource = "https://png.icons8.com/material/50/FFFFFF/home-page.png"
            });

			masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Articles",
				TargetType = typeof(WhiteLabel.Articles),
				IconSource = "https://png.icons8.com/material/50/FFFFFF/news-filled.png"
            });

			masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Products",
				TargetType = typeof(WhiteLabel.ProductsPage),
				IconSource = "https://png.icons8.com/material/50/FFFFFF/box.png"
            });

			masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Offers",
				TargetType = typeof(WhiteLabel.OffersPage),
				IconSource = "https://png.icons8.com/material/50/FFFFFF/discount.png"
            });

			masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Contact Us",
				TargetType = typeof(WhiteLabel.HomePage),
				IconSource = "https://png.icons8.com/material/50/FFFFFF/contacts.png"
            });

			masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Privacy Policy",
				TargetType = typeof(WhiteLabel.HomePage),
				IconSource = "https://png.icons8.com/material/50/FFFFFF/privacy.png"
            });

			masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "M-Pay",
				TargetType = typeof(WhiteLabel.HomePage),
				IconSource = "https://png.icons8.com/metro/50/FFFFFF/bank-cards.png"
            });

			masterpageItem.Add(new WhiteLabel.MasterPageItem
            {
                Title = "Preferences and Settings",
                TargetType = typeof(WhiteLabel.HomePage),
				IconSource = "https://png.icons8.com/metro/50/FFFFFF/services.png"
            });

			masterpageItem.Add(new WhiteLabel.MasterPageItem
			{				
				Title =  Text,
				TargetType = typeof(WhiteLabel.Login),
				IconSource = "https://png.icons8.com/material/50/FFFFFF/user-credentials.png"
            });

            listView.ItemsSource = masterpageItem;
        }
		protected override async void OnAppearing()
		{
			var UrlD = Url + Utility.getLanguageCode(GlobalData.language);
			string content = await client.GetStringAsync(UrlD);
            masterpageItem = new ObservableCollection<MasterPageItem>();
			MenuItemResponse response = JsonConvert.DeserializeObject<MenuItemResponse>(content);
			foreach(MenuItem item in response.NavigationList) {
				MasterPageItem mItem = new MasterPageItem();
				mItem.IconSource = "https://whitelabel-dxebr.d.epsilon.com/sitecore" + item.iconUrl;
				if (item.Title == "Login")
				{
					if (Application.Current.Properties.ContainsKey("isLoggedIn"))
					{
						var id = Application.Current.Properties["isLoggedIn"];
						if (id != null && id.ToString() != Boolean.FalseString)
						{
							mItem.Title = "Logout";
						}
						else
                        {
                            mItem.Title = item.Title;
                        }
					}
					else
                    {
                        mItem.Title = item.Title;
                    }
				}
				else
				{
					mItem.Title = item.Title;
				}
				if(item.Title == "Register"){
					if (Application.Current.Properties.ContainsKey("isLoggedIn"))
                    {
                        var id = Application.Current.Properties["isLoggedIn"];
                        if (id != null && id.ToString() != Boolean.FalseString)
                        {
							continue;
                        }                        
                    }
				}
				mItem.TargetType = Utility.GetTargetType(item.Title);
				mItem.Guid = item.Guid;
				mItem.IsInProgress = false;
				masterpageItem.Add(mItem);
			}
			listView.ItemsSource = masterpageItem;
			base.OnAppearing();
		}
	}

	class MenuItem {
		public string Guid { get; set; }
		public string Title { get; set; }
		public string iconUrl { get; set; }
		public string IsInProgress { get; set; }
	}
	class MenuItemResponse {
		public List<MenuItem> NavigationList { get; set; }
	}
}
