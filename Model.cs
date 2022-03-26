using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NET__ININ4_PR1._2_z1
{
    class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropetyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        string wynik = "0";
        bool
            ułamek = false,
            przecinek = false,
            ujemna = false,
            flagaWyniku = false
            ;
        Stack<double> buforLiczb = new();
        string buforDziałania = null;
        public string Wynik {
            get { return wynik; }
            set
            {
                wynik = value;
                OnPropetyChanged();
            }
        }

        internal void DopiszCyfrę(string cyfra)
        {
            if (flagaWyniku)
                Skasuj();
            if (Wynik == "0")
                Wynik = "";
            Wynik += cyfra;
            if (przecinek && !ułamek)
                ułamek = true;
        }
        internal void PrzełączUłamek()
        {
            if (flagaWyniku)
                Skasuj();
            if(!ułamek)
                if (przecinek)
                {
                    przecinek = false;
                    Wynik = Wynik.Substring(0, Wynik.Length - 1);
                }
                else
                {
                    przecinek = true;
                    Wynik += ",";
                }
            if(Wynik == "-0")
                KorektaUjemnegoZera();
        }
        internal void PrzełączZnak()
        {
            if (Wynik != "0")
                if (ujemna)
                {
                    Wynik = Wynik.Substring(1);
                    ujemna = false;
                }
                else
                {
                    Wynik = "-" + Wynik;
                    ujemna = true;
                }
        }
        private void KorektaUjemnegoZera()
        {
            Wynik = "0";
            ujemna = false;
        }


        internal void CofnijZnak()
        {
            if (Wynik[^1] == ',')
            {
                PrzełączUłamek();
                return;
            }
            string w = Wynik.Substring(0, Wynik.Length - 1);
            if (w == "")
                Wynik = "0";
            else if (w == "-0")
                KorektaUjemnegoZera();
            else
                Wynik = w;
        }
        internal void Skasuj()
        {
            Wynik = "0";
            ujemna = ułamek = przecinek = false;
        }
        internal void Resetuj()
        {
            Skasuj();
            buforLiczb.Clear();
            buforDziałania = null;
        }

        internal void ZwykłeDziałanie(string oznaczenie)
        {
            buforLiczb.Push(
                Convert.ToDouble(Wynik)
                );
            Skasuj();
            if (buforLiczb.Count == 1)
                buforDziałania = oznaczenie;
            else
            {
                double w = WykonajDziałanie(
                    buforLiczb.Pop(),
                    buforLiczb.Pop()
                    );
                buforLiczb.Push(w);
                Wynik = w.ToString();
                flagaWyniku = true;
            }
        }

        private double WykonajDziałanie(double v1, double v2)
        {
            if (buforDziałania == "+")
                return v1 + v2;
            else
                return double.NaN;
        }
    }
}
