using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ShikiApiLib;
using static ShikiApiLib.ShikiApiStatic;

namespace WpfApp2
{

	public class AnimeMatch
	{
		public AnimeRate AnimeShortInfo { get; set; }
		public AnimeFullInfo AnimeInfo { get; set; }
		public List<User> UsersMatch { get; set; }
		public BitmapImage Poster
		{
			get
			{
				var bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = new Uri(AnimeShortInfo.Poster.original, UriKind.Absolute);
				bitmap.EndInit();
				return bitmap;
			}
		}
		public int MatchCount { get { return UsersMatch.Count; } }

		public void GetFull()
		{
			if (AnimeInfo == null)
				AnimeInfo = Anime.GetAnime(AnimeShortInfo);
		}
		public AnimeMatch(AnimeRate animeRate, List<User> usersMatch)
		{
			AnimeShortInfo = animeRate;
			UsersMatch = usersMatch;
		}

	}

	public static class Anime
	{
		public static List<AnimeFullInfo> AnimeInfo = new List<AnimeFullInfo>();

		public static AnimeFullInfo GetAnime(AnimeRate anim)
		{
			var info = AnimeInfo.FirstOrDefault(x => x.TitleId == anim.TitleId);
			if (info == null)
			{
				info = GetAnimeFullInfo(anim.TitleId);
				AnimeInfo.Add(info);
				return info;
			}
			return info;
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
				foreach (var anime in user.Anime)
				{
					if (watched.FirstOrDefault(x => x.TitleId == anime.TitleId) == null &&
						anime.UserStatus != $"planned")
						watched.Add(anime);
				}
			}

			foreach (var anime in planned)
			{
				//var count = planned.Count(x=> x.TitleId == anime.TitleId);
				if (anime.TitleStatus != "released" ||
					watched.FirstOrDefault(x => x.TitleId == anime.TitleId) != null)
					continue;
				var usersMatch = new List<User>();
				foreach (var user in users)
					if (user.PlannedAnime.FirstOrDefault(x => x.TitleId == anime.TitleId) != null)
						usersMatch.Add(user);
				matches.Add(new AnimeMatch(anime, usersMatch));
			}

			return matches;
		}

	}
}
