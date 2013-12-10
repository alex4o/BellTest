using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.ComponentModel;
 using System.Threading;
using System.Data.SQLite;
using System.Diagnostics;

namespace Bell
{
	class ClockModel : INotifyPropertyChanged
	{

		#region Fields
		private int h = 0;
		private string _timetext;
		private string _err;
		private DateTime _now;
		public KeyValuePair<int,TimeSpan>[] list;
		public static SQLiteConnection con;
		Multimedia.Timer clock;
		int i = 0;
		#endregion

		#region Properties
		public String TimeText
		{
			get { return _timetext; }
			set
			{
				_timetext = value;
				NotifyPropertyChanged("TimeText");
			}
		}

		public string time_type{
			get {
				return (h % 2 == 1) ? "час" : "междучасие";
			}
		}

		public KeyValuePair<int, TimeSpan>[] ls {
			get {
				return list;
			}
			set {
				list = value;
			}
		}

		public string next_hour {
			get {
				if (h == list.Length - 1) {
					return "Няма";
				}

				if (h % 2 == 1)
				{
					return list[h+1].Value.ToString(@"\:hh\:mm");
				}
				else {
					return list[h].Value.ToString(@"\:hh\:mm");
				}
			}
		}

		public int hour
		{
			get{
				return h;
			}
			set
			{
				if(h < (list.Length - 1)){
				h = value;
				NotifyPropertyChanged("time_type");
				NotifyPropertyChanged("next_hour");
				}
			}
		}	
		
		
		public int curr_sleep{
			get{
				return (h % 2 == 0) ? h : h + 1;
			}
		}
		
		public String Error
		{
			get { return _err; }
			set
			{
				_err = value;
				NotifyPropertyChanged("Error");
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
		#endregion 

		void tick(object sender, EventArgs e)
        {
			i += 1;
			
            Now = Now.AddSeconds(1);
            if(Now.TimeOfDay >= list[hour].Value){
				hour += 1;
				_now = DateTime.Now;
				
			}

        }

		public void LoadList(int h,int m,int ml)
		{
			TimeSpan t = new TimeSpan(7, 30, 0);
			TimeSpan hour = new TimeSpan(0, h, 0);
			TimeSpan mid = new TimeSpan(0, m, 0);
			TimeSpan lmid = new TimeSpan(0, ml, 0);
			List<KeyValuePair<int,TimeSpan>> llist =  new List<KeyValuePair<int,TimeSpan>>();
			int i = 1;

			do
			{
				if (i == 8)
				{
					i = 9;
					continue;
				}
				llist.Add(new KeyValuePair<int, TimeSpan>(1, t));
				t += hour;

				if (i == 3)
				{
					llist.Add(new KeyValuePair<int, TimeSpan>(2, t));
					t += lmid;

				}
				else
				{

					llist.Add(new KeyValuePair<int, TimeSpan>(0, t));


					t += mid;

				}

				i++;
			} while (i < 9);
			list = llist.ToArray();
		}

		public KeyValuePair<int,TimeSpan> getHour(int num){
			num = num + (num - 1);
			return list[num - 1];
		}

		public KeyValuePair<int, TimeSpan> getMiddle(int num)
		{
			num = num + (num - 1);
			if (num >= list.Length / 2) {
				return list[num - 1];
			}
			return list[num];
		}

		public ClockModel()
		{


			LoadList(40,10,20);
			

			while (DateTime.Now.TimeOfDay > list[h].Value && h < (list.Length - 2))
			{
				
				h += 1;
			}
			//clock = new System.Timers.Timer(1000);
			Thread t = new Thread(() =>
			{
				clock = new Multimedia.Timer();
				clock.Period = 1000;
				clock.Resolution = 0;
				clock.Tick += tick;
				clock.Start();
			});

			t.Start();
			//clock.Elapsed += tick;
			_now = DateTime.Now;
			
			//clock.Start();
			 
			
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