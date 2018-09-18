using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace WhiteLabel
{
	public partial class Login : ContentPage
	{
		private string Url = "https://whitelabel-dxebr.d.epsilon.com/api/sitecore/Accounts/AppLogin?Email=’{emailId}’&amp;Password=’{pswd}’";
        private readonly HttpClient client = new HttpClient();

		void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		public Login()
		{
			InitializeComponent();
			picker.SelectedIndex = 0;
			         
			var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
				Handle_Clicked_1(s, e);
            };
            //fblogin.GestureRecognizers.Add(tapGestureRecognizer);
		}

		private void ShowLoading(string text = "Loading..") {
			actIndicatorContainer.IsVisible = true;
            actIndicator2.IsRunning = true;
		}

		private void HideLoading() {
			actIndicatorContainer.IsVisible = false;
            actIndicator2.IsRunning = false;
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			ShowLoading();	
			GlobalData.language = picker.SelectedItem.ToString();
			bool isSuccess = await DoLogin(userIdField.Text, passwordField.Text);
			if (isSuccess)
			{
				Application.Current.Properties["isLoggedIn"] = Boolean.TrueString;
				NavigateToHome();

			}
			else {
				HideLoading();
				DisplayAlert("Failed", "Please check the credentials entered", "OK");
				return;
			}
		}

		private void NavigateToHome()
		{
			GlobalData.language = picker.SelectedItem.ToString();
			MasterDetailPage fpm = new MasterDetailPage();

			HideLoading();
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
		}

		void Handle_Clicked_3(object sender, System.EventArgs e)
		{
			ShowLoading();
			NavigateToHome();
		}
        
		protected override async void OnAppearing()
        {
			if (Application.Current.Properties.ContainsKey("isLoggedIn"))
            {
                var id = Application.Current.Properties["isLoggedIn"];
				if (id != null && id.ToString() != Boolean.FalseString)
                {
                    NavigateToHome();
                    return;
                }
            }
            NavigationPage.SetHasNavigationBar(this, false);

		}

		void Handle_Clicked_1(object sender, System.EventArgs e)
		{
			ShowLoading();
			GlobalData.language = picker.SelectedItem.ToString();
			var apiRequest =
                    "https://www.facebook.com/dialog/oauth?client_id="
                    + "235241537158062"
                    + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html"
                    + "&scope=user_birthday,email";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
		}

		private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {			
            var accessToken = ExtractAccessTokenFromUrl(e.Url);
            if (accessToken != "")
            {				
				((WebView)sender).IsVisible = false;
                var vm = new FacebookViewModel();

                await vm.SetFacebookUserProfileAsync(accessToken);

				bool isLoggedIn = await DoLogin(vm.FacebookProfile.Email, vm.FacebookProfile.FirstName);
				if(!isLoggedIn){
					bool reg = await DoRegister(vm.FacebookProfile.Email, vm.FacebookProfile.FirstName);
					if(reg){
						isLoggedIn = await DoLogin(vm.FacebookProfile.Email, vm.FacebookProfile.FirstName);
						if(isLoggedIn) {
							NavigateToHome();
						} else {
							HideLoading();
							DisplayAlert("Failed", "Please check the credentials entered", "OK");
						}
					} else {
						HideLoading();
						DisplayAlert("Failed Facebook Registration", "Please check the credentials entered", "OK");
					}
				} else {
					NavigateToHome();
				}
            }
        }

		private async Task<bool> DoLogin(String UserName, String Password) {
			ShowLoading("Logging in..");
			var LoginUrl = "https://whitelabel-dxebr.d.epsilon.com/api/sitecore/Accounts/AppLogin?Email=’{emailId}’&amp;Password=’{pswd}’";
			LoginUrl = LoginUrl.Replace("{emailId}", UserName);
			LoginUrl = LoginUrl.Replace("{pswd}", Password);
			string content = await client.GetStringAsync(LoginUrl);
			if (content.Contains("\"Success\":true")){
				Application.Current.Properties["isLoggedIn"] = Boolean.TrueString;
				return true;
			}
			HideLoading();
			return false;
		}

		async void Handle_Clicked_2(object sender, System.EventArgs e)
		{
			//Application.Current.MainPage = new Register();
			Navigation.PushModalAsync(new Register());
			/*
			GlobalData.language = picker.SelectedItem.ToString();
			if(userIdField.Text =="" || passwordField.Text == ""){
				DisplayAlert("Alert", "Please enter User Name and Password", "OK");
			}
			bool reg = await DoRegister(userIdField.Text, passwordField.Text);
			if(reg) {
				bool isLogged = await DoLogin(userIdField.Text, passwordField.Text);
				if(isLogged){
					NavigateToHome();
				} else {
					DisplayAlert("Login Failed", "Please check the credentials entered", "OK");
				}
			} else {
				DisplayAlert("Registration Failed", "Please check the credentials entered", "OK");
			}*/

		}
        
		private async Task<bool> DoRegister(String UserName, String Password)
        {
			ShowLoading("Registering..");
			var RegUrl = "https://whitelabel-dxebr.d.epsilon.com/api/sitecore/Accounts/AppRegister?Email=’{emailId}’&Password=’{pswd}’&ConfirmPassword=’{pswd}’";
			RegUrl = RegUrl.Replace("{emailId}", UserName);
			RegUrl = RegUrl.Replace("{pswd}", Password);
			string content = await client.GetStringAsync(RegUrl);
            if (content.Contains("\"success\":true"))
            {
                return true;
            }
			HideLoading();
            return false;
        }

		private void SetPageContent(FacebookProfile facebookProfile)
        {
			Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(8, 30),
                Children =
                {
                    new Label
                    {
                        Text = facebookProfile.Name,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Id,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.IsVerified.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
						Text = "Android",
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Gender,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.AgeRange.Min.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Picture.Data.Url,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Cover.Source,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                }
            };
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
	}
}
