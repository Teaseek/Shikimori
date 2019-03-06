using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShikiApiLib;

namespace AppMatches.Client.ViewModels
{
	public class StudioViewModel : INotifyPropertyChanged
	{
		public readonly Studio StudioModel;

		public StudioViewModel(Studio studio)
		{
			StudioModel = studio;
		}

		public string Name => StudioModel.name;

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
