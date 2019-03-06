using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShikiApiLib;
using static ShikiApiLib.ShikiApiStatic;

namespace AppMatches.Model
{
	public static class Data
	{
		public static List<AnimeFullInfo> Infos = new List<AnimeFullInfo>();

		public static AnimeFullInfo GetFullData(AnimeRate anim)
		{
			var info = Infos.FirstOrDefault(x => x.TitleId == anim.TitleId);
			if (info != null)
				return info;
			info = GetAnimeFullInfo(anim.TitleId);
			Infos.Add(info);
			return info;
		}

	}
}
