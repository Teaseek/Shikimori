using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AppMatches.Model;

namespace AppMatches.Client.ViewModels
{
	public class ApplicationViewModel : INotifyPropertyChanged
	{
		private MatchViewModel _selectedMatch;
		public MatchViewModel SelectedMatch
		{
			get => _selectedMatch;
			set
			{
				_selectedMatch = value;
				_selectedMatch.Match.GetFullData();
				OnPropertyChanged(nameof(SelectedMatch));
			}
		}

		public ObservableCollection<MatchViewModel> Matches { get; set; } = new ObservableCollection<MatchViewModel>();
		public ObservableCollection<UserViewModel> Users { get; set; } = new ObservableCollection<UserViewModel>();

		public ApplicationViewModel()
		{
			foreach (var user in User.Users)
				Users.Add(new UserViewModel(user));

			foreach (var match in User.GetMatches(User.Users))
				Matches.Add(new MatchViewModel(match));

			Matches = new ObservableCollection<MatchViewModel>(Matches.OrderByDescending(o => o.MatchCount).ThenBy(x => x.RussianName));
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
					  var selected = obj as MatchViewModel;
					  System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
					  try
					  {
						  myProcess.StartInfo.UseShellExecute = true;
						  if (selected != null)
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
