using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShikiApiLib;

namespace AppMatches.Client.ViewModels
{
	public class GenreViewModel : INotifyPropertyChanged
	{
		public readonly Genre GenreModel;

		public GenreViewModel(Genre genre)
		{
			GenreModel = genre;
		}

		public string Name => GenreModel.russian;

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
