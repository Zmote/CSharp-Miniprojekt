using AutoReservation.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        private AutoReservationEntities context;

        public AutoReservationBusinessComponent()
        {
            context = new AutoReservationEntities();
        }

        public IList<Reservation> GetReservations()
        {
            var reservationen = from p in context.Reservationen select p;
            return reservationen.ToList();
        }

        public IList<Auto> GetAutos()
        {
            var autos = from p in context.Autos select p;
            return autos.ToList();
        }

        public IList<Kunde> GetKunden()
        {
            var kunden = from p in context.Kunden select p;
            return kunden.ToList();
        }

        public void UpdateAuto(Auto modifiedAuto, Auto original)
        {
            context.Autos.Attach(original);
            context.Entry(original).CurrentValues.SetValues(modifiedAuto);
        }

        public void InsertAuto(Auto auto)
        {
            context.Autos.Add(auto);
        }

        public void InsertReservation(Auto reservation)
        {
            context.Autos.Add(reservation);
            //context.SaveChanges()?
        }

        public void UpdateReservation(Reservation modifiedReservation, Reservation original)
        {
            context.Reservationen.Attach(original);
            context.Entry(original).CurrentValues.SetValues(modifiedReservation);
        }

        public void InsertReservation(Reservation reservation)
        {
            context.Reservationen.Add(reservation);
        }

        public void DeleteReservation(Reservation selectedReservation)
        {
            context.Reservationen.Attach(selectedReservation);
            context.Reservationen.Remove(selectedReservation);
        }

        public void DeleteAuto(Auto selectedAuto)
        {
            context.Autos.Attach(selectedAuto);
            context.Autos.Remove(selectedAuto);
        }
        public void InsertKunde(Kunde kunde)
        {
            context.Kunden.Add(kunde);
        }
        public void UpdateKunde(Kunde modifiedKunde, Kunde original)
        {
            context.Kunden.Attach(original);
            context.Entry(original).CurrentValues.SetValues(modifiedKunde);
        }
        public void DeleteKunde(Kunde selectedKunde)
        {
            context.Kunden.Attach(selectedKunde);
            context.Kunden.Remove(selectedKunde);
        }

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }
    }
}