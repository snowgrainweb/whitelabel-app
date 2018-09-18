using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
namespace WhiteLabel
{
	public class ContentListItem: INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private string _Title;

		private string _Image;

		private string _Content;

		private string _Date;

		private string _Name;
		private Boolean _IsProduct;

		private string _ColorCode;

		private string _DetailContent;

		public string Title { get { return _Title; } set { OnPropertyChanged("Title"); _Title = value; }}

		public string Content { get { return _Content; } set { OnPropertyChanged("Title"); _Content = value; } }

		public string Date { get { return _Date; } set { OnPropertyChanged("Title"); _Date = value; } }

		public string Image { get { return _Image; } set { OnPropertyChanged("Title"); _Image = value; } }

		public string DetailContent { get { return _DetailContent; } set { OnPropertyChanged("Title"); _DetailContent = value; } }

		public string ColorCode { get { return _ColorCode; } set { OnPropertyChanged("Title"); _ColorCode = value; } }

		public string Name { get { return _Name; } set { OnPropertyChanged("Title"); _Name = value; }}

		public Boolean IsProduct { get { return _IsProduct; } set { OnPropertyChanged("IsProduct"); _IsProduct = value; }}

		protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                       new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
