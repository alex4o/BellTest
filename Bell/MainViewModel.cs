using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bell
{
	class MainViewModel : INotifyPropertyChanged
	{

		ClockModel cm;
		public String TimeText
		{
			get { return _timetext; }
			set
			{
				_timetext = value;
				NotifyPropertyChanged("TimeText");
			}
		}


		public MainViewModel()
		{
			cm = new ClockModel();
		}

		#region PropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string str)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(str));
			}
		}

		#endregion
	}
}
