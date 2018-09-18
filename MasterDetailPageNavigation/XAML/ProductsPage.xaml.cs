using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Collections;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel; 

namespace WhiteLabel
{
    public partial class ProductsPage : ContentPage
    {
		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var content = e.Item as ContentListItem;
			ProductDetail productDetail = new ProductDetail();
			productDetail.BindingContext = content;
			Navigation.PushAsync(productDetail);
		}

		private string Url = "https://whitelabel-dxebr.d.epsilon.com/sitecore/api/ssc/aggregate/content/Items('%7b9290CF6A-F110-472E-A894-22E2238D0AFA%7d')/Children?language=en&$expand=Fields($select=Name,Value,Url)&sc_apikey={3EF5CA5D-52D4-4FCF-A614-6260D5E52522}";

        private readonly HttpClient client = new HttpClient();

        public ListView ListView { get { return listView1; } }

        public bool IsBusy = true;

		public ObservableCollection<WhiteLabel.ContentListItem> contentListItems { get; set; }


		public ProductsPage()
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
                ColorCode = "#6688cc"
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
                ColorCode = "#6688cc"
            });


            //listView1.ItemsSource = contentListItems;
        }


        protected override async void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => { listView1.IsRefreshing = true; listView1.BeginRefresh(); });
			Url = "https://whitelabel-dxebr.d.epsilon.com/sitecore/api/ssc/aggregate/content/Items('%7b9290CF6A-F110-472E-A894-22E2238D0AFA%7d')/Children?language="+Utility.getLanguageCode(GlobalData.language)+"&$expand=Fields($select=Name,Value,Url)&sc_apikey={3EF5CA5D-52D4-4FCF-A614-6260D5E52522}";
            string content = await client.GetStringAsync(Url);
			contentListItems = new ObservableCollection<WhiteLabel.ContentListItem>();
            //ArticleResponse response = JsonConvert.DeserializeObject<ArticleResponse>(content);
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
        class ArticleResponse
        {
            public string context { get; set; }
            public List<ArticleData> value { get; set; }
        }
    }
}
