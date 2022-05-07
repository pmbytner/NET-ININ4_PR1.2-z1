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

        string buforIO = "0";
        bool
            ułamek = false,
            przecinek = false,
            ujemna = false,
            flagaWyniku = false
            ;
        double?
            buforWyniku = null,
            buforDrugiejLiczby = null
            ;
        string buforDziałania = null;
        public string BuforIO {
            get { return buforIO; }
            set
            {
                buforIO = value;
                OnPropetyChanged();
            }
        }
        public string BuforDziałania {
            get { return buforDziałania; }
            set
            {
                buforDziałania = value;
                OnPropetyChanged();
                OnPropetyChanged("Przypominajka");
            }
        }
        public string BuforWyniku
        {
            get { return buforWyniku.ToString(); }
            set
            {
                buforWyniku = Convert.ToDouble(value);
                OnPropetyChanged();
                OnPropetyChanged("Przypominajka");
            }
        }
        public string BuforDrugiejLiczby
        {
            get { return buforDrugiejLiczby.ToString(); }
            set
            {
                buforDrugiejLiczby = Convert.ToDouble(value);
                OnPropetyChanged();
                OnPropetyChanged("Przypominajka");
            }
        }
        public string Przypominajka
        {
            get { return $"{BuforWyniku} {BuforDziałania} {BuforDrugiejLiczby}"; }
        }

        internal void DopiszCyfrę(string cyfra)
        {
            if (flagaWyniku)
                Skasuj();
            if (BuforIO == "0")
                BuforIO = "";
            BuforIO += cyfra;
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
                    BuforIO = BuforIO.Substring(0, BuforIO.Length - 1);
                }
                else
                {
                    przecinek = true;
                    BuforIO += ",";
                }
            if(BuforIO == "-0")
                KorektaUjemnegoZera();
        }
        internal void PrzełączZnak()
        {
            if (BuforIO != "0")
                if (ujemna)
                {
                    BuforIO = BuforIO.Substring(1);
                    ujemna = false;
                }
                else
                {
                    BuforIO = "-" + BuforIO;
                    ujemna = true;
                }
        }
        private void KorektaUjemnegoZera()
        {
            BuforIO = "0";
            ujemna = false;
        }


        internal void CofnijZnak()
        {
            if (BuforIO[^1] == ',')
            {
                PrzełączUłamek();
                return;
            }
            string w = BuforIO.Substring(0, BuforIO.Length - 1);
            if (w == "")
                BuforIO = "0";
            else if (w == "-0")
                KorektaUjemnegoZera();
            else
                BuforIO = w;
        }
        internal void Skasuj()
        {
            BuforIO = "0";
            ujemna = ułamek = przecinek = flagaWyniku = false;
        }
        internal void Resetuj()
        {
            Skasuj();
            /*buforLiczb.Clear();*/
            BuforDziałania = null;
            buforWyniku = buforDrugiejLiczby = null;
            OnPropetyChanged("BuforWyniku");
            OnPropetyChanged("BuforDrugiejLiczby");
            OnPropetyChanged("BuforDziałania");
            OnPropetyChanged("Przypominajka");
        }

        internal void NoweDziałanie(string oznaczenie)
        {
            ZwykłeDziałanie();
            BuforDziałania = oznaczenie;
        }
        internal void ZwykłeDziałanie()
        {
            if (buforWyniku == null)
            {
                BuforWyniku = BuforIO;
                flagaWyniku = true;
            }
            else
            {
                if (flagaWyniku == false)
                {
                    BuforDrugiejLiczby = BuforIO;
                }
                else if (buforDrugiejLiczby == null)
                    return;
                BuforWyniku = WykonajDziałanie().ToString();
                BuforIO = buforWyniku.ToString();
                flagaWyniku = true;
            }
        }
        internal void DziałanieProcentowe()
        {
            if (buforWyniku == null)
            {
                BuforWyniku = BuforIO;
                flagaWyniku = true;
            }
            else
            {
                if (flagaWyniku == false)
                {
                    BuforDrugiejLiczby = BuforIO;
                }
                else if (buforDrugiejLiczby == null)
                    return;
                buforDrugiejLiczby = buforDrugiejLiczby * buforWyniku / 100;
                BuforWyniku = WykonajDziałanie().ToString();
                BuforIO = BuforWyniku;
                flagaWyniku = true;
            }
        }
        internal void DziałanieJednoargumentowe(string oznaczenie)
        {
            if(buforWyniku == null)
            {
                BuforWyniku = BuforIO;
                flagaWyniku = true;
            }
            BuforDziałania = oznaczenie;
            BuforWyniku = WykonajDziałanieJednoargumentowe().ToString();
            BuforIO = BuforWyniku;
            flagaWyniku = true;
            //wyczyścić bufor drugiej liczby?
        }

        private double WykonajDziałanie()
        {
            if (buforDziałania == "+")
                return (double)(buforWyniku + buforDrugiejLiczby);
            else if (buforDziałania == "×")
                return (double)(buforWyniku * buforDrugiejLiczby);
            else
                return double.NaN;
        }
        private double WykonajDziałanieJednoargumentowe()
        {
            if (BuforDziałania == "1/x")
                return 1 / (double)buforWyniku;
            else
                return double.NaN;
        }
    }
}
