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
                Auto = myAuto,
                Kunde = myKunde,
                Von = new DateTime(2015, 12, 12),
                Bis = new DateTime(2015, 12, 13)
            };
            //TODO: Problem: Doesn't insert for some reason
            //Note: Insert Reservation works from UI though
            //so nothing wrong with datastructure, something in test setup
            Target.InsertReservation(myReservation);
            ReservationDto insertedReservation = Target.GetReservationByNr(5);
            Debug.WriteLine("I am Reservations:{0}", Target.Reservationen.Last());
            Assert.AreEqual(myReservation.ReservationNr, insertedReservation.ReservationNr);
            Assert.AreEqual(myReservation.Auto, insertedReservation.Auto);
            Assert.AreEqual(myReservation.Kunde, insertedReservation.Kunde);
            Assert.AreEqual(myReservation.Von, insertedReservation.Von);
            Assert.AreEqual(myReservation.Bis, insertedReservation.Bis);
        }

        [TestMethod]
        public void Test_UpdateAuto()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_UpdateKunde()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_UpdateReservation()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoDto>))]
        public void Test_UpdateAutoWithOptimisticConcurrency()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<KundeDto>))]
        public void Test_UpdateKundeWithOptimisticConcurrency()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ReservationDto>))]
        public void Test_UpdateReservationWithOptimisticConcurrency()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_DeleteKunde()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_DeleteAuto()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_DeleteReservation()
        {
            Assert.Inconclusive("Test not implemented.");
        }
    }
}
