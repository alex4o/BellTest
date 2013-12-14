﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.ComponentModel;


using System.Diagnostics;

namespace Bell
{
	enum ClassType { 
		hour = 0,
		normalmid = 1,
		longmid = 2,
		shortmid = 3,
	}

	class ClockModel : INotifyPropertyChanged
	{

		#region Fields
		private int h = 0;	

		#endregion 
		public KeyValuePair<ClassType, TimeSpan>[] LoadList()
		{
			return LoadList(40,10,20);
		}

		public KeyValuePair<ClassType, TimeSpan>[] LoadList(int h, int m, int ml)
		{
			TimeSpan t = new TimeSpan(7, 30, 0);
			TimeSpan hour = new TimeSpan(0, h, 0);
			TimeSpan mid = new TimeSpan(0, m, 0);
			TimeSpan lmid = new TimeSpan(0, ml, 0);
			List<KeyValuePair<ClassType, TimeSpan>> llist = new List<KeyValuePair<ClassType, TimeSpan>>();
			int i = 0;
			ClassType lasttype = ClassType.normalmid;
			while (i < 7 * 2) {
				switch(lasttype){
					case ClassType.hour:
						lasttype = (i == 5)? ClassType.longmid : ClassType.normalmid ;
						llist.Add(new KeyValuePair<ClassType, TimeSpan>(lasttype, t));
						if (i == 5) {
							t += lmid;
							
						}else{
							t += mid;
							
						}
						
					break;
					case ClassType.normalmid:
						lasttype = ClassType.hour;
						llist.Add(new KeyValuePair<ClassType, TimeSpan>(lasttype, t));
						t += hour;
						
						
					break;
					case ClassType.shortmid:

					break;
					case ClassType.longmid:
						lasttype = ClassType.hour;
						llist.Add(new KeyValuePair<ClassType, TimeSpan>(lasttype, t));
						t += hour;
						
						
					break;
				}
				i++;
				
			}
			return llist.ToArray();			
			
		}

		public ClockModel()
		{
			 
			
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