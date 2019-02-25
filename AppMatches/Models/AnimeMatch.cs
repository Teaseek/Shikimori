using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Media.Imaging;
using ShikiApiLib;

namespace AppMatches
{
	public class AnimeMatch
	{
		public AnimeRate AnimeShortInfo { get; set; }
		public AnimeFullInfo AnimeInfo { get; set; }
		public List<User> UsersMatch { get; set; }

		public BitmapImage Poster;
		public void GetFull()
		{
			if (AnimeInfo == null)
				AnimeInfo = Anime.GetAnime(AnimeShortInfo);
		}

		public AnimeMatch(AnimeRate animeRate, List<User> usersMatch)
		{
			AnimeShortInfo = animeRate;
			UsersMatch = usersMatch;

			Poster = new BitmapImage();
			Poster.BeginInit();
			Poster.UriSource = new Uri(AnimeShortInfo.Poster.original);
			Poster.EndInit();
		}

		public static List<AnimeMatch> GetMatchAnimes(List<User> users)
		{
			var matches = new List<AnimeMatch>();
			var planned = new List<AnimeRate>();
			var watched = new List<AnimeRate>();
			foreach (var user in users)
			{
				foreach (var anime in user.PlannedAnime)
				{
					if (planned.FirstOrDefault(x => x.TitleId == anime.TitleId) == null)
						planned.Add(anime);
				}
				foreach (var anime in user.Anime.Where(x => x.UserStatus != "planned"))
				{
					if (watched.FirstOrDefault(x => x.TitleId == anime.TitleId) == null)
						watched.Add(anime);
				}
			}

			foreach (var anime in planned)
			{
				if (anime.TitleStatus != "released" ||
					watched.FirstOrDefault(x => x.TitleId == anime.TitleId) != null)
					continue;
				var usersMatch = new List<User>();
				foreach (var user in users)
				{
					if (user.PlannedAnime.FirstOrDefault(x => x.TitleId == anime.TitleId) != null)
						usersMatch.Add(user);
				}

				matches.Add(new AnimeMatch(anime, usersMatch));
			}

			return matches;
		}
	}
}
