using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void Test_UpdateAuto()
        {
            Auto meinAuto = new StandardAuto
            {
                Id = 1,
                Tagestarif = 2000,
                Marke = "Porsche X2"
            };
            Auto originalAuto = Target.GetAutoById(1);
            Target.UpdateAuto(meinAuto, originalAuto);
            Assert.AreEqual(meinAuto.Marke, "Porsche X2");
            Assert.AreEqual(meinAuto.Tagestarif, 2000);
        }

        [TestMethod]
        public void Test_UpdateKunde()
        {
            Kunde meinKunde = new Kunde
            {
                Id = 1,
                Vorname = "Zafer",
                Nachname = "Dogan",
                Geburtsdatum = new System.DateTime(1989, 5, 5)
            };
            Kunde meinOriginalKunde = Target.GetKundeById(1);
            Target.UpdateKunde(meinKunde, meinOriginalKunde);
            Kunde meinUpdatedKunde = Target.GetKundeById(1);
            Assert.AreEqual(meinKunde.Vorname, meinUpdatedKunde.Vorname);
            Assert.AreEqual(meinKunde.Nachname, meinUpdatedKunde.Nachname);
            Assert.AreEqual(meinKunde.Geburtsdatum, meinUpdatedKunde.Geburtsdatum);
        }

        [TestMethod]
        public void Test_UpdateReservation()
        {
            Auto meinAuto = Target.GetAutoById(2);
            Kunde meinKunde = Target.GetKundeById(1);
            Reservation meinReservation = new Reservation
            {
                Auto = meinAuto,
                AutoId = 2,
                Kunde = meinKunde,
                KundeId = 1,
                ReservationNr = 1,
                Von = new System.DateTime(1960, 2, 2),
                Bis = new System.DateTime(1960, 2, 5)
            };
            Reservation meinOriginalReservation = Target.GetReservationByNr(1);
            Target.UpdateReservation(meinReservation, meinOriginalReservation);
            Reservation meinInsertedReservation = Target.GetReservationByNr(1);
            Assert.AreEqual(meinReservation.Auto.Marke, meinInsertedReservation.Auto.Marke);
            Assert.AreEqual(meinReservation.Auto.Tagestarif, meinInsertedReservation.Auto.Tagestarif);
            Assert.AreEqual(meinReservation.AutoId, meinInsertedReservation.AutoId);
            Assert.AreEqual(meinReservation.Von, meinInsertedReservation.Von);
            Assert.AreEqual(meinReservation.Bis, meinInsertedReservation.Bis);
        }
    }
}
