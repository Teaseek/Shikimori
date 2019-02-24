using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WpfApp2
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		List<User> Users = new List<User>();
		List<AnimeMatch> Matches;
		public MainWindow()
		{
			InitializeComponent();
			Users.Add(new User(227956));
			Users.Add(new User(345242));
			Users.Add(new User(204018));
			//Users.Add(new User(404534));

			Matches = Anime.GetMatchAnimes(Users).OrderByDescending(o => o.MatchCount).ThenBy(x => x.AnimeShortInfo.RusName).ToList();
			AnimeLb.ItemsSource = Matches;
		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var lb = (sender as ListBox);
			if (lb.SelectedIndex == -1)
				return;
			var selected = (AnimeMatch)lb.SelectedItem;
			selected.GetFull();
			Content.DataContext = selected;
			UserList.ItemsSource = selected.UsersMatch;

		}

		private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
		{
			System.Diagnostics.Process myProcess = new System.Diagnostics.Process();

			try
			{
				// true is the default, but it is important not to set it to false
				myProcess.StartInfo.UseShellExecute = true;
				myProcess.StartInfo.FileName = AnimeTitle.Tag.ToString();
				myProcess.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void AnimeTitle_MouseEnter(object sender, MouseEventArgs e)
		{
			AnimeTitle.Foreground = Brushes.Blue;
		}

		private void AnimeTitle_MouseLeave(object sender, MouseEventArgs e)
		{
			AnimeTitle.Foreground = Brushes.Black;
		}
	}
}

