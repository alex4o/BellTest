using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
namespace Bell
{
	class MainViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Hour> hList;
		ClockModel cm;
		Multimedia.Timer clock;
		private DateTime _now;
		private string _timetext;
		KeyValuePair<ClassType, TimeSpan>[] chart;
		public String TimeText
		{
			get { return _timetext; }
			set
			{
				_timetext = value;
				NotifyPropertyChanged("TimeText");
			}
		}

		public ObservableCollection<Hour> HList
		{
			get
			{
				return hList;
			}
			set
			{
				hList = value;
			}
				
		}
		DateTime Now
		{
			get { return _now; }
			set
			{
				_now = value;
				TimeText = value.ToString("HH:mm:ss");
			}
		}

		void tick(object sender, EventArgs e)
		{
			Now = Now.AddSeconds(1);
			/*if (Now.TimeOfDay >= list[hour].Value)
			{
				hour += 1;
				_now = DateTime.Now;

			}*/

		}

		public MainViewModel()
		{
			cm = new ClockModel();
			chart = cm.LoadList();
			hList = new ObservableCollection<Hour>();
			foreach (KeyValuePair<ClassType, TimeSpan> o in chart) {
				hList.Add(new Hour() { Type = o.Key.ToString(), Start = o.Value.ToString()});
			}
			Thread t = new Thread(() =>
			{
				clock = new Multimedia.Timer();
				clock.Period = 1000;
				clock.Resolution = 0;
				clock.Tick += tick;
				_now = DateTime.Now;
				clock.Start();
			});
			t.Start();
			
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


		class Hour
		{
			public String Type
			{
				get;
				set;
			}

			public String Start
			{
				get;
				set;
			}

		}
}
