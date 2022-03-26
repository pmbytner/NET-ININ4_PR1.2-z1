using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NET__ININ4_PR1._2_z1
{
    internal class Model : INotifyPropertyChanged
    {
        string imię = "Nemo";
        public string Imię {
            get { return imię; }
            set
            {
                imię = value;
                OnPropertyChanged();
                OnPropertyChanged("Format");
            }
        }
        public string Format
        {
            get { return $"Podane imię to " + Imię; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}