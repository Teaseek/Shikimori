using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using AppMatches.Model;

namespace AppMatches.Client.ViewModels
{
	public class ApplicationViewModel : INotifyPropertyChanged
	{
		private MatchViewModel selectedMatch;
		public MatchViewModel SelectedMatch
		{
			get => selectedMatch;
			set
			{
				selectedMatch = value;
				selectedMatch.Match.GetFullData();
				OnPropertyChanged(nameof(SelectedMatch));
			}
		}

		private ObservableCollection<MatchViewModel> matches = new ObservableCollection<MatchViewModel>();
		public ObservableCollection<MatchViewModel> Matches
		{
			get => matches;
			set
			{
				matches = value;
				OnPropertyChanged(nameof(Matches));
			}
		}
		public ObservableCollection<UserViewModel> Users { get; set; } = new ObservableCollection<UserViewModel>();

		public ApplicationViewModel()
		{
			for (var index = 0; index < User.Users.Count; index++)
			{
				var user = User.Users[index];
				Users.Add(new UserViewModel(user));
				Users[index].Selected += ApplicationViewModel_Selected;
				Users[index].IsSelected = true;
			}

			//SelectedUsers = Users;
			//GetData(User.Users);
		}

		private void ApplicationViewModel_Selected(object sender, EventArgs e)
		{
			var users = Users.Where(x => x.IsSelected).ToList();
			GetData(users.Select(x => x.MatchUser).ToList());
		}

		public void GetData(List<User> users)
		{
			Matches.Clear();
			foreach (var match in User.GetMatches(users))
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
