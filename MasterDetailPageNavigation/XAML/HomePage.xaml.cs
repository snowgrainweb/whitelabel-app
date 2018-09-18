using Xamarin.Forms;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace WhiteLabel
{
	public partial class HomePage : ContentPage
	{
		public ListView ListView { get { return listView1; } }
		private static string ln = Utility.getLanguageCode(GlobalData.language);
		private static string Url = "https://whitelabel-dxebr.d.epsilon.com/api/sitecore/mobileapp/Home?language=";
		public ObservableCollection<WhiteLabel.ContentListItem> contentListItems { get; set; }
		private readonly HttpClient client = new HttpClient();



		public HomePage()
		{			
			ToolbarItem itemStudy = new ToolbarItem
			{
				Icon = "search.png",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => Navigation.PushAsync(new SearchPage()))
			};
			ToolbarItems.Add(itemStudy);
			InitializeComponent();
			contentListItems = new ObservableCollection<WhiteLabel.ContentListItem>();
			contentListItems.Add(new WhiteLabel.ContentListItem
			{
				Title = "Product",
				Image = "http://www.millaboratories.in/wp-content/uploads/2015/10/banner1.png",
				Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque est ex, gravida quis varius in, pretium et augue. Donec bibendum rutrum enim, vitae ultricies tortor semper nec.",
				Date = "23-07-2018",
				ColorCode = "#66cc88"
			});
			contentListItems.Add(new WhiteLabel.ContentListItem
			{
				Title = "Article",
				Image = "https://www.solidworks.com/sites/default/files/2017-12/PRODUCT-CATEGORY-TECH-COM-inspection-MBD-shop%20floor-composer-edrawings-hero-001.jpg",
				Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque est ex, gravida quis varius in, pretium et augue. Donec bibendum rutrum enim, vitae ultricies tortor semper nec.",
				Date = "23-07-2018",
				ColorCode = "#6688cc"
			});
			contentListItems.Add(new WhiteLabel.ContentListItem
			{
				Title = "Product",
				Image = "https://yj4dfcucm3mswcd2nbkkg14m-wpengine.netdna-ssl.com/wp-content/uploads/2017/11/featureimagesArtboard-1-100.jpg",
				Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque est ex, gravida quis varius in, pretium et augue. Donec bibendum rutrum enim, vitae ultricies tortor semper nec.",
				Date = "23-07-2018",
				ColorCode = "#66cc88"
			});


			//listView1.ItemsSource = contentListItems;
		}

		protected override async void OnAppearing()
		{
			
			InitializeComponent();
			if(Navigation.NavigationStack.Count > 0 && Navigation.NavigationStack[0].GetType() == typeof(Login)){
				Navigation.RemovePage(Navigation.NavigationStack[0]);	
			}

			Device.BeginInvokeOnMainThread(() => { listView1.IsRefreshing = true; listView1.BeginRefresh(); });
			var UrlD = Url + Utility.getLanguageCode(GlobalData.language);
			string content = await client.GetStringAsync(UrlD);
			HomeData response = JsonConvert.DeserializeObject<HomeData>(content);
			contentListItems = new ObservableCollection<WhiteLabel.ContentListItem>();
			listView1.IsVisible = false;           
			contentListItems.Add(new ContentListItem
			{
				Title = response.Article.Name,
				Image = "https://whitelabel-dxebr.d.epsilon.com/sitecore" + response.Article.Image,
				Content = Regex.Replace((response.Article.Description ?? ""), "<.*?>", "").Length <= 120 ? Regex.Replace((response.Article.Description ?? ""), "<.*?>", "") : Regex.Replace((response.Article.Description ?? ""), "<.*?>", "").Substring(0, 117) + "...",
				Date = "",
				ColorCode = "#66cc88"


			});
			contentListItems.Add(new ContentListItem
			{
				Title = response.Product.Name,
				Image = "https://whitelabel-dxebr.d.epsilon.com/sitecore" + response.Product.Image,
				Content = Regex.Replace((response.Product.Description ?? ""), "<.*?>", "").Length <= 120 ? Regex.Replace((response.Product.Description ?? ""), "<.*?>", "") : Regex.Replace((response.Product.Description ?? ""), "<.*?>", "").Substring(0, 117) + "...",
				Date = "",
				ColorCode = "#66cc88"

			});
			contentListItems.Add(new ContentListItem
			{
				Title = response.Offers.Name,
				Image = "https://whitelabel-dxebr.d.epsilon.com/sitecore" + response.Offers.Image,
				Content = Regex.Replace((response.Offers.Description ?? ""), "<.*?>", "").Length <= 120 ? Regex.Replace((response.Offers.Description ?? ""), "<.*?>", "") : Regex.Replace((response.Offers.Description ?? ""), "<.*?>", "").Substring(0, 117) + "...",
				Date = "",
				ColorCode = "#66cc88"

			});

			Device.BeginInvokeOnMainThread(() => { listView1.IsRefreshing = false; listView1.EndRefresh(); });
			listView1.ItemsSource = null;
			listView1.ItemsSource = contentListItems;
			listView1.IsVisible = true;
			base.OnAppearing();

		}
	}
	class HomePageClass
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
	}
	class HomeData
	{
		public HomePageClass Article { get; set; }
		public HomePageClass Product { get; set; }
		public HomePageClass Offers { get; set; }
	}
}

