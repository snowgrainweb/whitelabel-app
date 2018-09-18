using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;  
using System.Collections.ObjectModel;  

namespace WhiteLabel
{
	public class Contacts
	{
		public string Name { get; set; }
		public string Num { get; set; }
		public string imgsource { get; set; }
	}

	public partial class SearchPage : ContentPage
	{
		private const string Url = "https://whitelabel-dxebr.d.epsilon.com/sitecore/api/ssc/aggregate/content/Items?language=en&$expand=Fields($select=Name,Value)&$filter=Fields/any(f: (f/Name eq 'AppDescription' and f/Value eq 'This is a none - searchable word qwerhhjjhll'))&sc_apikey={3EF5CA5D-52D4-4FCF-A614-6260D5E52522}";

		private readonly HttpClient client = new HttpClient();

		public ObservableCollection<WhiteLabel.ContentListItem> contentListItems { get; set; }
		public List<Contacts> tempdata;
		public SearchPage()
		{
			InitializeComponent();
		}
		async void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			Device.BeginInvokeOnMainThread(() => { list.IsRefreshing = true; list.BeginRefresh(); });
			var NewUrl = "https://whitelabel-dxebr.d.epsilon.com/sitecore/api/ssc/aggregate/content/Items?language=en&$expand=Fields($select=Name,Value,Url)&$filter=Fields/any(f: (f/Name eq 'AppName' and f/Value eq '" + e.NewTextValue + "'))&sc_apikey={3EF5CA5D-52D4-4FCF-A614-6260D5E52522}";
			string content = await client.GetStringAsync(NewUrl);
            contentListItems = new ObservableCollection<WhiteLabel.ContentListItem>();
            //ArticleResponse response = Utility.Articles;
            //System.Diagnostics.Debug.WriteLine(content);

			var NewUrlD = "https://whitelabel-dxebr.d.epsilon.com/sitecore/api/ssc/aggregate/content/Items?language=en&$expand=Fields($select=Name,Value,Url)&$filter=Fields/any(f: (f/Name eq 'AppDescription' and f/Value eq '" + e.NewTextValue + "'))&sc_apikey={3EF5CA5D-52D4-4FCF-A614-6260D5E52522}";
            string contentD = await client.GetStringAsync(NewUrlD);            

            contentListItems = Utility.GetItemList(content);
			foreach(WhiteLabel.ContentListItem item in Utility.GetItemList(contentD)) {
				contentListItems.Add(item);
			}
			list.IsVisible = false;
            list.ItemsSource = null;
            list.ItemsSource = contentListItems;
            Device.BeginInvokeOnMainThread(() => { list.IsRefreshing = false; list.EndRefresh(); });
            IsBusy = false;
            list.IsVisible = true;
		}
		protected override async void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => { list.IsRefreshing = true; list.BeginRefresh(); });
            string content = await client.GetStringAsync(Url);
            contentListItems = new ObservableCollection<WhiteLabel.ContentListItem>();
            //ArticleResponse response = Utility.Articles;
            //System.Diagnostics.Debug.WriteLine(content);
            contentListItems = Utility.GetItemList(content);
            InitializeComponent();
			list.IsVisible = false;
			list.ItemsSource = null;
			list.ItemsSource = contentListItems;
			Device.BeginInvokeOnMainThread(() => { list.IsRefreshing = false; list.EndRefresh(); });
            IsBusy = false;
			list.IsVisible = true;
			search.Focus();
            base.OnAppearing();
        }

		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var content = e.Item as ContentListItem;
            ProductDetail productDetail = new ProductDetail();
            productDetail.BindingContext = content;
            Navigation.PushAsync(productDetail);
		}
	}
}
