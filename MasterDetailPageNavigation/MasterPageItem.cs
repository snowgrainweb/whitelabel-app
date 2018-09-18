using System;
using System.ComponentModel;
namespace WhiteLabel
{
	public class MasterPageItem: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private bool _IsInProgress;
		public string Title { get; set; }

        public string BackgroundColor { get; set; }

		public string IconSource { get; set; }

		public Type TargetType { get; set; }
        
		public bool IsInProgress { get { return _IsInProgress; } set { _IsInProgress = value; OnPropertyChanged("IsInProgress"); } }

		public string Guid { get; set; }

		protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}
