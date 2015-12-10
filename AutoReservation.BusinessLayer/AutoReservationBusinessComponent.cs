using AutoReservation.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        public AutoReservationBusinessComponent()
        {
        }

        public IList<Reservation> GetReservations()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var reservationen = from p in context.Reservationen
                                    .Include(a => a.Auto)
                                    .Include(b => b.Kunde)
                                    select p;
                return reservationen.ToList();
            }
        }

        public IList<Auto> GetAutos()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var autos = from p in context.Autos select p;
                return autos.ToList();
            }
        }

        public IList<Kunde> GetKunden()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var kunden = from p in context.Kunden select p;
                return kunden.ToList();
            }
        }

        public void UpdateAuto(Auto modifiedAuto, Auto original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Autos.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modifiedAuto);
                    context.SaveChanges();
                }                
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }

        }

        public void InsertAuto(Auto auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Autos.Add(auto);
                context.SaveChanges();
            }
        }

        public void UpdateReservation(Reservation modifiedReservation, Reservation original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Reservationen.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modifiedReservation);
                    context.SaveChanges();
                }catch(DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public void InsertReservation(Reservation reservation)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
        }

        public void DeleteReservation(Reservation selectedReservation)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Reservationen.Attach(selectedReservation);
                context.Reservationen.Remove(selectedReservation);
                context.SaveChanges();
            }
        }

        public void DeleteAuto(Auto selectedAuto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Autos.Attach(selectedAuto);
                context.Autos.Remove(selectedAuto);
                context.SaveChanges();
            }
        }
        public void InsertKunde(Kunde kunde)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Kunden.Add(kunde);
                context.SaveChanges();
            }
        }
        public void UpdateKunde(Kunde modifiedKunde, Kunde original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Kunden.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modifiedKunde);
                    context.SaveChanges();
                }catch(DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }
        public void DeleteKunde(Kunde selectedKunde)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Kunden.Attach(selectedKunde);
                context.Kunden.Remove(selectedKunde);
                context.SaveChanges();
            }
        }

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }
    }
}