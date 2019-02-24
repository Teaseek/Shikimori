using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ShikiApiLib;
using static ShikiApiLib.ShikiApiStatic;

namespace AppMatches
{
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
	}
}
