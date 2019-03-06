using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Media.Imaging;
using ShikiApiLib;

namespace AppMatches.Model
{
	public class Match
	{
		//public static IReadOnlyList<Match> Matches = new List<Match>();
		//public delegate void MatchStateHandler(object sender, EventArgs e);
		//public event MatchStateHandler FullInfoAdded;

		private AnimeRate ShortInfo { get; set; }
		private AnimeFullInfo Info { get; set; }
		public List<User> UsersMatch { get; set; }
		public string UsersMatchLine
		{
			get
			{
				var line = "";
				foreach (var user in UsersMatch)
					line += $"{user.Info.nickname} ";
				return line.TrimEnd();
			}
		}
		public string GenresLine
		{
			get
			{
				var line = "";
				foreach (var genre in Info.Genres)
					line += $"{genre.russian} ";
				return line.TrimEnd();
			}
		}
		public string StudiosLine
		{
			get
			{
				var line = "";
				foreach (var studio in Info.Studios)
					line += $"{studio.name} ";
				return line.TrimEnd();
			}
		}
		public string RussianName => ShortInfo.RusName;
		public string EnglishName => ShortInfo.Name;
		public string Url => ShortInfo.Url;
		public string Kind
		{
			get
			{
				var kind = "";
				switch (Info.Kind)
				{
					case "tv":
						kind = "TV Сериал";
						break;
					case "movie":
						kind = "Фильм";
						break;
					case "ova":
						kind = Info.Kind.ToUpper();
						break;
					case "ona":
						kind = Info.Kind.ToUpper();
						break;
					case "special":
						kind = "Спешл";
						break;
					case "music":
						kind = "Клип";
						break;
					case "tv_13":
						kind = "TV Сериал Короткий";
						break;
					case "tv_24":
						kind = "TV Сериал Средний";
						break;
					case "tv_48":
						kind = "TV Сериал Длинный";
						break;
				}
				return kind;
			}
		}
		public double TitleScore => Info.TitleScore;
		public int TotalEpisodes => Info.TotalEpisodes;
		public int Duration => Info.Duration;
		public string Status
		{
			get
			{
				var status = "";
				switch (Info.TitleStatus)
				{
					case "anons":
						status = "Анонс";
						break;
					case "ongoing":
						status = "Онгоинг";
						break;
					case "released":
						status = "Вышло";
						break;
				}
				return status;
			}
		}
		public string AiredOn
		{
			get
			{
				var start = Convert.ToDateTime(Info.AiredOn).ToLongDateString();
				var end = Convert.ToDateTime(Info.ReleasedOn).ToLongDateString();
				if (ShortInfo.Kind == "movie" ||
					ShortInfo.Kind == "special" ||
					ShortInfo.Kind == "music")
					return start;
				var status = $"с {start}";
				if (!string.IsNullOrWhiteSpace(Info.ReleasedOn))
				{
					status += $" по {end}";
				}
				return status;
			}
		}
		public string Rating
		{
			get
			{
				var rate = "";
				switch (Info.Rating)
				{
					case "g":
						rate = "G - Нет возрастных ограничений";
						break;
					case "pg":
						rate = "PG - Рекомендуется присутствие родителей";
						break;
					case "pg_13":
						rate = "PG-13 - Детям до 13 лет просмотр не желателен";
						break;
					case "r":
						rate = "R - Лицам до 17 лет обязательно присутствие взрослого";
						break;
					case "r_plus":
						rate = "R+ - Лицам до 17 лет просмотр запрещён";
						break;
					case "rx":
						rate = "Rx - Хентай";
						break;
					default:
						rate = "Нет рейтинга";
						break;
				}
				return rate;
			}
		}
		public string Description => Info.Description;
		private BitmapImage poster = new BitmapImage(new Uri("pack://application:,,,/Res/missing_original.jpg"));
		public BitmapImage Poster {
			get => poster;
			set => poster = value;
		}

		public void GetFullData()
		{
			if (Info != null)
				return;
			Info = Data.GetFullData(ShortInfo);
			//FullInfoAdded?.Invoke(this, new EventArgs());
		}

		public Match(AnimeRate rate, List<User> usersMatch)
		{
			ShortInfo = rate;
			UsersMatch = usersMatch;
			if (rate.Poster.original == null)
			{
				Poster = new BitmapImage(new Uri("pack://application:,,,/Res/missing_original.jpg"));
			}
			else
			{
				Poster = new BitmapImage();
				Poster.BeginInit();
				Poster.UriSource = new Uri(rate.Poster.original);
				Poster.EndInit();
			}
		}

		public static List<Match> GetMatch(List<User> users)
		{
			var matches = new List<Match>();
			var planned = new List<AnimeRate>();
			var watched = new List<AnimeRate>();
			foreach (var user in users)
			{
				foreach (var anim in user.Planned)
				{
					if (planned.FirstOrDefault(x => x.TitleId == anim.TitleId) == null)
						planned.Add(anim);
				}
				foreach (var anim in user.Animes.Where(x => x.UserStatus != "planned"))
				{
					if (watched.FirstOrDefault(x => x.TitleId == anim.TitleId) == null)
						watched.Add(anim);
				}
			}

			foreach (var anim in planned)
			{
				if (anim.TitleStatus != "released" ||
					watched.FirstOrDefault(x => x.TitleId == anim.TitleId) != null)
					continue;
				var usersMatch = new List<User>();
				foreach (var user in users)
				{
					if (user.Planned.FirstOrDefault(x => x.TitleId == anim.TitleId) != null)
						usersMatch.Add(user);
				}

				matches.Add(new Match(anim, usersMatch));
			}

			return matches;
		}
	}
}

