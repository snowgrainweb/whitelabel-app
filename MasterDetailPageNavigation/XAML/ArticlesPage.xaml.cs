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

namespace WhiteLabel
{
	public partial class Articles : ContentPage
    {

		private string Url = "https://whitelabel-dxebr.d.epsilon.com/sitecore/api/ssc/aggregate/content/Items('%7bEFD9575A-D601-4C08-A3A0-E9714D12FB73%7d')/Children?language=en&$expand=Fields($select=Name,Value,Url)&sc_apikey={3EF5CA5D-52D4-4FCF-A614-6260D5E52522}";

		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var content = e.Item as ContentListItem;
            ProductDetail productDetail = new ProductDetail();
            productDetail.BindingContext = content;
            Navigation.PushAsync(productDetail);
		}

		private readonly HttpClient client = new HttpClient();

		public ListView ListView { get { return listView1; } }           

		public bool IsBusy = true;

		public ObservableCollection<WhiteLabel.ContentListItem> contentListItems { get; set; }

		public Articles()
        {
			InitializeComponent();
        }

		protected override async void OnAppearing() {
			Device.BeginInvokeOnMainThread(() => { listView1.IsRefreshing = true; listView1.BeginRefresh(); });
			Url = "https://whitelabel-dxebr.d.epsilon.com/sitecore/api/ssc/aggregate/content/Items('%7bEFD9575A-D601-4C08-A3A0-E9714D12FB73%7d')/Children?language="+Utility.getLanguageCode(GlobalData.language)+"&$expand=Fields($select=Name,Value,Url)&sc_apikey={3EF5CA5D-52D4-4FCF-A614-6260D5E52522}";
			string content = await client.GetStringAsync(Url);
			contentListItems = new ObservableCollection<WhiteLabel.ContentListItem>();
			//ArticleResponse response = Utility.Articles;
			//System.Diagnostics.Debug.WriteLine(content);
			contentListItems = Utility.GetItemList(content);
			InitializeComponent();
			listView1.IsVisible = false;
			listView1.ItemsSource = null;
			listView1.ItemsSource = contentListItems;
			Device.BeginInvokeOnMainThread(() => { listView1.IsRefreshing = false; listView1.EndRefresh(); });
			IsBusy = false;
			listView1.IsVisible = true;
			base.OnAppearing();
		}
        
		protected override bool OnBackButtonPressed() {
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
