using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ShikiApiLib;
using System.Collections.ObjectModel;

namespace AppMatches
{
	public class AnimeMatchViewModel : INotifyPropertyChanged
	{
		public AnimeMatch Match { get; set; }
		public ObservableCollection<UserViewModel> Users { get; set; } = new ObservableCollection<UserViewModel>();
		public int MatchCount => Users.Count;

		public AnimeMatchViewModel(AnimeMatch match)
		{
			Match = match;
			foreach (var user in Match.UsersMatch)
				Users.Add(new UserViewModel(user));
		}

		public string RussianName
		{
			get { return Match.AnimeShortInfo.RusName; }
			set
			{
				Match.AnimeShortInfo.RusName = value;
				OnPropertyChanged(nameof(RussianName));
			}
		}
		public string EnglishName
		{
			get { return Match.AnimeShortInfo.Name; }
			set
			{
				Match.AnimeShortInfo.Name = value;
				OnPropertyChanged(nameof(EnglishName));
			}
		}
		public ObservableCollection<UserViewModel> MatchUsers
		{
			get { return Users; }
			set
			{
				Users = value;
				OnPropertyChanged(nameof(MatchUsers));
			}
		}
		public string Url
		{
			get { return Match.AnimeShortInfo.Url; }
		}
		public BitmapImage Poster
		{
			get
			{
				var bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = new Uri(Match.AnimeShortInfo.Poster.original, UriKind.Absolute);
				bitmap.EndInit();
				return bitmap;
			}
		
		}
		public string Kind
		{
			get { return Match.AnimeInfo.Kind; }
			set
			{
				Match.AnimeInfo.Kind = value;
				OnPropertyChanged(nameof(Kind));
			}
		}
		public double TitleScore
		{
			get { return Match.AnimeInfo.TitleScore; }
			set
			{
				Match.AnimeInfo.TitleScore = value;
				OnPropertyChanged(nameof(TitleScore));
			}
		}
		public int TotalEpisodes
		{
			get { return Match.AnimeInfo.TotalEpisodes; }
			set
			{
				Match.AnimeInfo.TotalEpisodes = value;
				OnPropertyChanged(nameof(TotalEpisodes));
			}
		}
		public int Duration
		{
			get { return Match.AnimeInfo.Duration; }
			set
			{
				Match.AnimeInfo.Duration = value;
				OnPropertyChanged(nameof(Duration));
			}
		}
		public string TitleStatus
		{
			get { return Match.AnimeInfo.TitleStatus; }
			set
			{
				Match.AnimeInfo.TitleStatus = value;
				OnPropertyChanged(nameof(TitleStatus));
			}
		}
		public string AiredOn
		{
			get
			{
				var end = $"с {Match.AnimeInfo.AiredOn}";
				if (!string.IsNullOrWhiteSpace(Match.AnimeInfo.ReleasedOn))
				{
					end += $" по {Match.AnimeInfo.ReleasedOn}";
				}
				return end;
			}
		}
		public string Rating
		{
			get { return Match.AnimeInfo.Rating.ToUpper(); }
			set
			{
				Match.AnimeInfo.Rating = value;
				OnPropertyChanged(nameof(Rating));
			}
		}
		public List<Studio> Studios
		{
			get { return Match.AnimeInfo.Studios; }
			set
			{
				Match.AnimeInfo.Studios = value;
				OnPropertyChanged(nameof(Studios));
			}
		}
		public List<Genre> Genres
		{
			get { return Match.AnimeInfo.Genres; }
			set
			{
				Match.AnimeInfo.Genres = value;
				OnPropertyChanged(nameof(Genres));
			}
		}
		public string Description
		{
			get { return Match.AnimeInfo.Description; }
			set
			{
				Match.AnimeInfo.Description = value;
				OnPropertyChanged(nameof(Description));
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
