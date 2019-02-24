using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShikiApiLib;
using System.Windows;

namespace WpfApp2
{
	public class ApplicationViewModel : INotifyPropertyChanged
	{
		private AnimeMatchViewModel selectedAnimeMatch;
		public AnimeMatchViewModel SelectedAnimeMatch
		{
			get { return selectedAnimeMatch; }
			set
			{
				selectedAnimeMatch = value;
				selectedAnimeMatch.Match.GetFull();
				OnPropertyChanged(nameof(SelectedAnimeMatch));
			}
		}

		public ObservableCollection<AnimeMatchViewModel> AnimeMatchs { get; set; } = new ObservableCollection<AnimeMatchViewModel>();
		public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

		public ApplicationViewModel()
		{
			var ids = new[] { 227956, 345242, 204018 }; //404534
			foreach (var id in ids)
				Users.Add(new User(id));

			var matches = AnimeMatch.GetMatchAnimes(Users.ToList());
			foreach (var match in matches)
				AnimeMatchs.Add(new AnimeMatchViewModel(match));

			AnimeMatchs = new ObservableCollection<AnimeMatchViewModel>(AnimeMatchs.OrderByDescending(o => o.MatchCount).ThenBy(x => x.RussianName));
		}

		#region Commands

		private RelayCommand openSiteCommand;
		public RelayCommand OpenSiteCommand
		{
			get
			{
				return openSiteCommand ??
				  (openSiteCommand = new RelayCommand(obj =>
				  {
					  var selected = obj as AnimeMatchViewModel;
					  System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
					  try
					  {
						  myProcess.StartInfo.UseShellExecute = true;
						  myProcess.StartInfo.FileName = selected.Url;
						  myProcess.Start();
					  }
					  catch (Exception ex)
					  {
						  MessageBox.Show(ex.Message);
					  }
				  }));
			}
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
