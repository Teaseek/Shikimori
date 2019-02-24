using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ShikiApiLib;

namespace WpfApp2
{
	public class UserViewModel : INotifyPropertyChanged
	{
		public readonly User MatchUser;

		public UserViewModel(User user)
		{
			MatchUser = user;
		}

		public string Name
		{
			get { return MatchUser.Info.nickname; }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
