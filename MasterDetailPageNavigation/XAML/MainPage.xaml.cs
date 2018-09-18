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
    public partial class MainPage : MasterDetailPage
    {

		private readonly HttpClient client = new HttpClient();
		private const string Url = "https://whitelabel-dxebr.d.epsilon.com/sitecore/api/ssc/aggregate/content/Items('{id}')/Children?language=en&$expand=Fields($select=Name,Value,Url)&sc_apikey={3EF5CA5D-52D4-4FCF-A614-6260D5E52522}";

        public MainPage()
        {
            InitializeComponent();

            masterPage.ListView.ItemSelected += OnItemSelected;

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
			var item = e.SelectedItem as MasterPageItem;
			try
			{				
				item.IsInProgress = true;
				string content = await client.GetStringAsync(Url.Replace("{id}", item.Guid));
				ArticleResponse response = JsonConvert.DeserializeObject<ArticleResponse>(content);
				if(item.Title == "Logout" || item.Title == "Login"){
					//Application.Current.Properties["isLoggedIn"] = false;
					try
					{
						Application.Current.Properties["isLoggedIn"] = Boolean.FalseString;
						Application.Current.MainPage = new Login();
						return;
					}catch (Exception ex){
						
					}
				}
				if(item.Title == "Register") {
					try
                    {
                        Application.Current.Properties["isLoggedIn"] = Boolean.FalseString;
						Application.Current.MainPage = new Register();
                        return;
                    }
                    catch (Exception ex)
                    {

                    }

				}
				if(item.Title == "Contact Us") {
					Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ContactPage)));     
					masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    item.IsInProgress = false;
					return;
				}
				if (item != null)
				{					
					Utility.Articles = response;
					if (response.value != null && response.value.Count > 0)
					{						
						//NavigationPage((Page)Activator.CreateInstance(Utility.GetTargetType(response.value[0].TemplateName == "Page Data" ? response.value[1].TemplateName : response.value[0].TemplateName)));
						Detail = new NavigationPage((Page)Activator.CreateInstance(Utility.GetTargetType(response.value[0].TemplateName == "Page Data" ? response.value[1].TemplateName : response.value[0].TemplateName)));
					} else {
						Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));		
					}
					masterPage.ListView.SelectedItem = null;
					IsPresented = false;
					item.IsInProgress = false;

				}
			} catch(Exception e1){
				Console.Write(e1);
                
			}
        }
    }
	public class ArticleCat {
		public string TemplateName { get; set; }
		public List<NameValue> Fields { get; set; }
		public string Guid { get; set; }
	}
	public class ArticleCats {
		public List<ArticleCat> value { get; set; }
	}

}
