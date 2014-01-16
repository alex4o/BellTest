using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Bell
{
	/// <summary>
	/// Interaction logic for Patern.xaml
	/// </summary>
	/// 
	class TimePeriod {
		public String Name{
			get;
			set;
		}
		public TimeSpan Period
		{
			get;
			set;
		}

		public TimePeriod(String name, TimeSpan period)
		{
			Name = name;
			Period = period;
		}

		public override string ToString()
		{
			return this.Name;
		}
	}

	public partial class Patern : Window
	{
		MainViewModel mvvm;
		ObservableCollection<TimePeriod> Paterns = new ObservableCollection<TimePeriod>();
		internal Patern(MainViewModel MVMA)
		{
			InitializeComponent();
			mvvm = MVMA;
			PaternList.ItemsSource = Paterns;

		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			Paterns.Add(new TimePeriod("New", new TimeSpan()));
		}

		private void Rem_Click(object sender, RoutedEventArgs e)
		{
			if (PaternList.SelectedIndex < 0)
			{
				return;
			}
			Paterns.RemoveAt(PaternList.SelectedIndex);
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			if (PaternList.SelectedIndex < 0)
			{
				return;
			}
			Paterns[PaternList.SelectedIndex].Name = name.Text;
			Paterns[PaternList.SelectedIndex].Period = TimeSpan.Parse(time.Text);

		}

		private void Up_Click(object sender, RoutedEventArgs e)
		{
			int si = PaternList.SelectedIndex;
			if (si < 0) {
				return;
			}
			TimePeriod cont = Paterns.ElementAt<TimePeriod>(si);
			if (si - 1 < 0)
			{
				return;
			}
			Paterns.RemoveAt(si);

			Paterns.Insert(si-1,cont);
			PaternList.SelectedIndex = si - 1;
		}

		private void Down_Click(object sender, RoutedEventArgs e)
		{
			int si = PaternList.SelectedIndex;
			if (si < 0)
			{
				return;
			}
			TimePeriod cont = Paterns.ElementAt<TimePeriod>(si);
			if (si + 1 > Paterns.Count)
			{
				return;
			}
			Paterns.RemoveAt(si);

			Paterns.Insert(si + 1, cont);
			PaternList.SelectedIndex = si + 1;
		}
		private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
		{
			if (PaternList.SelectedItem != null)
			{
				TimePeriod tp =  PaternList.SelectedItem as TimePeriod;
				name.Text = tp.ToString();
				time.Text = tp.Period.ToString();
			}
		}


	}
}
