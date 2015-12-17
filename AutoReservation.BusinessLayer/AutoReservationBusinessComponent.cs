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

        public Reservation GetReservationByNr(int nr)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return context.Reservationen.Where(a => a.ReservationNr == nr)
                    .Include(a => a.Auto)
                    .Include(b => b.Kunde)
                    .FirstOrDefault();
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

        public Auto GetAutoById(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return context.Autos.Where(a => a.Id == id).FirstOrDefault();
            }
        }

        public Kunde GetKundeById(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return context.Kunden.Where(a => a.Id == id).FirstOrDefault();
            }
        }

        public IEnumerable<Kunde> GetKunden()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                //kann also auch direkt, ohne Linq query, geholt werden
                var kunden = context.Kunden;
                //ToList() bei IEnumberable, wegen Deffered Evaluation(damit hier schon query ausgeführt wird und nicht
                //ausserhalb des using blocks
                //Unterschied .FirstOrDefault() vs SingleOrDefault() --> 
                // bei SingleOrDefault wird falls indexiert, binary search angewendet, schnell
                // falls nicht indexiert, lineare Suche, langsam
                // SingleOrDefault wirft exception, falls der Eintrag nicht gefunden wird
                //das using Konstrukt hier kann mit jedem Objekt, das IDisposable einsetzt, verwendet werden,
                //weil beim Verlassen des using Blocks, .Dispose() aufgerufen wird. Wie ein finally-Block, das
                //den Garbage-Collector aufruft(Analogie Java)
                //using ist auch BestPractice bei Sachen wie IOStreams
                //--> darum zyklen klein halten, damit keine File-Blockade
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
                try
                {
                    context.Reservationen.Add(reservation);
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("I am ex.Data:{0}",ex.Data);
                }
                
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