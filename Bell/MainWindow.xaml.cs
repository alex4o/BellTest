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
using System.Windows.Media.Animation;

namespace Bell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		bool hides = true;

		MainViewModel mvm;
        public MainWindow()
        {
            InitializeComponent();
			mvm = this.DataContext as MainViewModel;

			
			//this.DataContext = cm;

        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			
			//cm.Error = "sdaasddsa";
			//this.DataContext = cm;

			
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			mvm.genList(Convert.ToInt32(ho.Text), Convert.ToInt32(mo.Text), Convert.ToInt32(mlo.Text));
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			DoubleAnimation da = new DoubleAnimation();
			
			da.From = CustomEdit.ActualHeight;
			if (hides)
			{
				da.To = 0;
				hides = !hides;
			}
			else { 
				da.To = 400;
				hides = !hides;
			}
			da.Duration = new Duration(TimeSpan.FromSeconds(1));
			CustomEdit.BeginAnimation(StackPanel.HeightProperty,da);
		}
    }
}
