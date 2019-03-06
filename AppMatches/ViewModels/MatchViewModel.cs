using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using AppMatches.Model;

namespace AppMatches.Client.ViewModels
{
	public class MatchViewModel : INotifyPropertyChanged
	{
		public Match Match { get; }

		public ObservableCollection<UserViewModel> Users { get; set; } = new ObservableCollection<UserViewModel>();
		public int MatchCount => Users.Count;

		public MatchViewModel(Match match)
		{
			Match = match;
			foreach (var user in Match.UsersMatch)
				Users.Add(new UserViewModel(user));
		}

		public string UsersMatchLine => Match.UsersMatchLine;
		public string GenresLine => Match.GenresLine;
		public string StudiosLine => Match.StudiosLine;
		public string RussianName => Match.RussianName;
		public string EnglishName => Match.EnglishName;
		public string Url => Match.Url;
		public BitmapImage Poster => Match.Poster;
		public string Kind => Match.Kind;
		public double TitleScore => Match.TitleScore;
		public int TotalEpisodes => Match.TotalEpisodes;
		public int Duration => Match.Duration;
		public string TitleStatus => Match.Status;
		public string AiredOn => Match.AiredOn;
		public string Rating => Match.Rating;
		public string Description => Match.Description;


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
