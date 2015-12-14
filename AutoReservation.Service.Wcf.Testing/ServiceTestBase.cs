using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void Test_GetAutos()
        {
            Assert.IsNotNull(Target.Autos);
        }

        [TestMethod]
        public void Test_GetKunden()
        {
            Assert.IsNotNull(Target.Kunden);
        }

        [TestMethod]
        public void Test_GetReservationen()
        {
            Assert.IsNotNull(Target.Reservationen);
        }

        [TestMethod]
        public void Test_GetAutoById()
        {
            AutoDto myAuto = Target.GetAutoById(1);
            Assert.AreEqual(myAuto.Marke, "Fiat Punto");
        }

        [TestMethod]
        public void Test_GetKundeById()
        {
            KundeDto myKunde = Target.GetKundeById(1);
            Assert.AreEqual(myKunde.Vorname, "Anna");
        }

        [TestMethod]
        public void Test_GetReservationByNr()
        {
            ReservationDto myReservation = Target.GetReservationByNr(1);
            Assert.AreEqual(myReservation.Kunde.Vorname, "Anna");
        }

        [TestMethod]
        public void Test_GetReservationByIllegalNr()
        {
            Assert.IsNull(Target.GetReservationByNr(10));
        }

        [TestMethod]
        public void Test_InsertAuto()
        {
            AutoDto myAuto = new AutoDto
            {
                Id = 4,
                Marke = "Porsche",
                Tagestarif = 200,
                AutoKlasse = AutoKlasse.Standard
            };

            Target.InsertAuto(myAuto);
            AutoDto insertedAuto = Target.GetAutoById(4);
            Assert.AreEqual(myAuto.Id, insertedAuto.Id);
            Assert.AreEqual(myAuto.AutoKlasse, insertedAuto.AutoKlasse);
            Assert.AreEqual(myAuto.Marke, insertedAuto.Marke);
            Assert.AreEqual(myAuto.Tagestarif, insertedAuto.Tagestarif);
        }

        [TestMethod]
        public void Test_InsertKunde()
        {
            KundeDto myKunde = new KundeDto
            {
                Id = 5,
                Vorname = "Zafer",
                Nachname = "Dogan",
                Geburtsdatum = new DateTime(2015, 12, 12)
            };

            Target.InsertKunde(myKunde);
            KundeDto insertedKunde = Target.GetKundeById(5);
            Assert.AreEqual(myKunde.Id, insertedKunde.Id);
            Assert.AreEqual(myKunde.Vorname, insertedKunde.Vorname);
            Assert.AreEqual(myKunde.Nachname, insertedKunde.Nachname);
            Assert.AreEqual(myKunde.Geburtsdatum, insertedKunde.Geburtsdatum);
        }

        [TestMethod]
        public void Test_InsertReservation()
        {
            AutoDto myAuto = Target.GetAutoById(1);
            KundeDto myKunde = Target.GetKundeById(1);
            ReservationDto myReservation = new ReservationDto
            {
                ReservationNr = 0,
                Auto = myAuto,
                Kunde = myKunde,
                Von = new DateTime(2015, 12, 12),
                Bis = new DateTime(2015, 12, 13)
            };
            Target.InsertReservation(myReservation);
            ReservationDto insertedReservation = Target.GetReservationByNr(4);
            Assert.AreEqual(insertedReservation.Auto.Marke, myReservation.Auto.Marke);
            Assert.AreEqual(insertedReservation.Auto.Tagestarif, myReservation.Auto.Tagestarif);
            Assert.AreEqual(insertedReservation.Kunde.Vorname, myReservation.Kunde.Vorname);
            Assert.AreEqual(insertedReservation.Kunde.Nachname, myReservation.Kunde.Nachname);
            Assert.AreEqual(insertedReservation.Von, myReservation.Von);
            Assert.AreEqual(insertedReservation.Bis, myReservation.Bis);
        }

        [TestMethod]
        public void Test_UpdateAuto()
        {
            AutoDto myAuto = new AutoDto
            {
                Id = 1,
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 2000,
                Basistarif = 0,
                Marke = "Porsche X2"
            };
            AutoDto originalAuto = Target.GetAutoById(1);
            Target.UpdateAuto(myAuto,originalAuto);
            Assert.AreEqual(myAuto.Marke, "Porsche X2");
            Assert.AreEqual(myAuto.Tagestarif, 2000);
            Assert.AreEqual(myAuto.Basistarif, 0);
        }

        [TestMethod]
        public void Test_UpdateKunde()
        {
            KundeDto originalKunde = Target.GetKundeById(1);
            KundeDto myKunde = new KundeDto
            {
                Id = 1,
                Vorname = "Zafer",
                Nachname = "Dogan",
                Geburtsdatum = new DateTime(2001, 12, 13)
            };
            Debug.WriteLine("I am original kunde:{0}",originalKunde);
            Target.UpdateKunde(myKunde,originalKunde);
            KundeDto changedKunde = Target.GetKundeById(1);
            Debug.WriteLine("I am changed kunde:{0}", changedKunde);
            Assert.AreEqual(changedKunde.Vorname, "Zafer");
            Assert.AreEqual(changedKunde.Nachname, "Dogan");
        }

        [TestMethod]
        public void Test_UpdateReservation()
        {

            AutoDto myAuto = Target.GetAutoById(1);
            KundeDto myKunde = Target.GetKundeById(1);
            ReservationDto myReservation = new ReservationDto
            {
                ReservationNr = 3,
                Auto = myAuto,
                Kunde = myKunde,
                Von = new DateTime(2015, 12, 12),
                Bis = new DateTime(2015, 12, 13)
            };

            ReservationDto originalReservation = Target.GetReservationByNr(3);
            Target.UpdateReservation(myReservation,originalReservation);
            ReservationDto updatedReservation = Target.GetReservationByNr(3);
            Assert.AreEqual(updatedReservation.Auto.Tagestarif, 50);
            Assert.AreEqual(updatedReservation.Kunde.Vorname, "Anna");
            Assert.AreEqual(updatedReservation.Kunde.Nachname, "Nass");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoDto>))]
        public void Test_UpdateAutoWithOptimisticConcurrency()
        {
            AutoDto myAuto1 = new AutoDto
            {
                Id = 1,
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 550,
                Basistarif = 0,
                Marke = "Porsche X2"
            };
            AutoDto myAuto2 = new AutoDto
            {
                Id = 1,
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 1000,
                Basistarif = 0,
                Marke = "Porsche X2"
            };
            AutoDto originalAuto = Target.GetAutoById(1);
            Target.UpdateAuto(myAuto1, originalAuto);
            Target.UpdateAuto(myAuto2, originalAuto);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<KundeDto>))]
        public void Test_UpdateKundeWithOptimisticConcurrency()
        {            
            KundeDto myKunde1 = new KundeDto
            {
                Id = 1,
                Vorname = "Jean Claude",
                Nachname = "Vandame",
                Geburtsdatum = new DateTime(2001, 12, 13)
            };
            KundeDto myKunde2 = new KundeDto
            {
                Id = 1,
                Vorname = "Zafer",
                Nachname = "Dogan",
                Geburtsdatum = new DateTime(2001, 12, 13)
            };
            KundeDto originalKunde = Target.GetKundeById(1);
            Target.UpdateKunde(myKunde1, originalKunde);
            Target.UpdateKunde(myKunde2, originalKunde);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ReservationDto>))]
        public void Test_UpdateReservationWithOptimisticConcurrency()
        {
            AutoDto myAuto = Target.GetAutoById(1);
            AutoDto myAuto2 = Target.GetAutoById(2);
            KundeDto myKunde = Target.GetKundeById(1);
            KundeDto myKunde2 = Target.GetKundeById(2);
            ReservationDto myReservation1 = new ReservationDto
            {
                ReservationNr = 3,
                Auto = myAuto,
                Kunde = myKunde,
                Von = new DateTime(2015, 12, 12),
                Bis = new DateTime(2015, 12, 14)
            };
            ReservationDto myReservation2 = new ReservationDto
            {
                ReservationNr = 3,
                Auto = myAuto2,
                Kunde = myKunde2,
                Von = new DateTime(2015, 12, 12),
                Bis = new DateTime(2015, 12, 13)
            };

            ReservationDto originalReservation = Target.GetReservationByNr(3);
            Target.UpdateReservation(myReservation1, originalReservation);
            Target.UpdateReservation(myReservation2, originalReservation);
        }

        [TestMethod]
        public void Test_DeleteKunde()
        {
            KundeDto myKunde = new KundeDto
            {
                Id = 1,
                Nachname = "Nass",
                Vorname = "Anna",
                Geburtsdatum = new DateTime(1961,5,5)
            };
            int before = Target.Kunden.Count();
            Target.DeleteKunde(myKunde);
            int after = Target.Kunden.Count();
            Assert.AreNotEqual(before, after);
        }

        [TestMethod]
        public void Test_DeleteAuto()
        {
            AutoDto myAuto = new AutoDto
            {
                Id = 1,
                Marke = "Fiat Punto",
                Tagestarif = 50,
                Basistarif = 0,
                AutoKlasse = AutoKlasse.Standard
            };
            int before = Target.Autos.Count();
            Target.DeleteAuto(myAuto);
            int after = Target.Autos.Count();
            Assert.AreNotEqual(before, after);
        }

        [TestMethod]
        public void Test_DeleteReservation()
        {
            ReservationDto myReservation = Target.GetReservationByNr(1);
            int before = Target.Reservationen.Count();
            Target.DeleteReservation(myReservation);
            int after = Target.Reservationen.Count();
            Assert.AreNotEqual(before, after);
        }
    }
}
