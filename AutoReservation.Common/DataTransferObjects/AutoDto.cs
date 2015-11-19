using AutoReservation.Common.Extensions;
using AutoReservation.Common.DataTransferObjects.Core;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    public class AutoDto : DtoBase<AutoDto>
    {
        private int id;
        private string marke;
        private int tagestarif;
        private int basistarif;
        private int autoklasse;

        public int Id
        {
            get { return id; }
            set
            {
                if(id == value)
                {
                    return;
                }
                id = value;
                this.OnPropertyChanged(p => p.Id);
            }
        }

        public string Marke
        {
            get { return marke; }
            set
            {
                if (marke.Equals(value))
                {
                    return;
                }
                marke = value;
                this.OnPropertyChanged(p => p.Marke);
            }
        }

        public int Tagestarif
        {
            get { return tagestarif; }
            set
            {
                if(tagestarif == value)
                {
                    return;
                }
                tagestarif = value;
                this.OnPropertyChanged(p => p.Tagestarif);
            }
        }

        public int Basistarif
        {
            get { return basistarif; }
            set
            {
                if (basistarif == value)
                {
                    return;
                }
                basistarif = value;
                this.OnPropertyChanged(p => p.Basistarif);
            }
        }

        public int AutoKlasse
        {
            get { return autoklasse; }
            set
            {
                if (autoklasse == value)
                {
                    return;
                }
                autoklasse = value;
                this.OnPropertyChanged(p => p.AutoKlasse);
            }
        }


        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            //TODO: Check --> implementation muss wohl ein bisschen anders sein, direkter Zugriff auf DB-Objekte? Maybe, lese Miniprojekt PDF
            if (AutoKlasse == AutoKlasse.Luxusklasse && basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override AutoDto Clone()
        {
            return new AutoDto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
                AutoKlasse = AutoKlasse,
                Basistarif = Basistarif
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}; {4}",
                Id,
                Marke,
                Tagestarif,
                Basistarif,
                AutoKlasse);
        }
    }
}
