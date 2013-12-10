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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

			public String End
			{
				get;
				set;
			}
		}

		ClockModel cm;
        public MainWindow()
        {
            InitializeComponent();
			cm = (ClockModel)this.DataContext;
			if (cm.list == null) {
				return;
			}
			
			for (int i = 1; i <= cm.list.Length / 2; i++)
			{
				this.Clases.Items.Add(new Hour() { Start = cm.getHour(i).Value.ToString(@"hh\:mm"), End = cm.getMiddle(i).Value.ToString(@"hh\:mm"), Type = "Час"  });
				if (i + 2 != cm.list.Length)
				{
					this.Clases.Items.Add(new Hour() { Start = cm.getMiddle(i).Value.ToString(@"hh\:mm"), End = cm.getHour(i + 1).Value.ToString(@"hh\:mm"), Type = "Междучасие" });
				}
			}
			//this.DataContext = cm;

        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			
			//cm.Error = "sdaasddsa";
			//this.DataContext = cm;

			
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			cm.LoadList(int.Parse(ho.Text), int.Parse(mo.Text), int.Parse(mlo.Text));
			this.Clases.Items.Clear();
			for (int i = 0; i < cm.list.Length - 2; i += 2)
			{
				this.Clases.Items.Add(new Hour() { Start = cm.list[i].Value.ToString(@"hh\:mm"), End = cm.list[i + 1].Value.ToString(@"hh\:mm"), Type = "Час" });
				if (i + 1 != cm.list.Length)
				{
					this.Clases.Items.Add(new Hour() { Start = cm.list[i + 1].Value.ToString(@"hh\:mm"), End = cm.list[i + 2].Value.ToString(@"hh\:mm"), Type = "Междучасие" });
				}
			}
		}
    }
}
