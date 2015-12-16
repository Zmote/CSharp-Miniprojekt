using AutoReservation.TestEnvironment;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Ui.Factory;
using Ninject;
using System.ServiceModel;
using System.Diagnostics;
using AutoReservation.Service.Wcf;

namespace AutoReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        private static ServiceHost serviceHost;
        //To avoid having to change Dependencies.Ninject.xml manually
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(AutoReservationService));
            serviceHost.Open();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            if (serviceHost.State != CommunicationState.Closed)
                serviceHost.Close();
        }

        private IKernel kernel;

        [TestInitialize]
        public void InitializeTestData()
        {
            kernel = new StandardKernel();
            kernel.Load("Dependencies.Ninject.xml");
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void Test_AutosLoad()
        {
            AutoViewModel vm = new AutoViewModel(kernel.Get<IServiceFactory>());
            vm.Init();
            Assert.IsNotNull(vm.Autos);
        }

        [TestMethod]
        public void Test_KundenLoad()
        {
            KundeViewModel vm = new KundeViewModel(kernel.Get<IServiceFactory>());
            vm.Init();
            Assert.IsNotNull(vm.Kunden);
        }

        [TestMethod]
        public void Test_ReservationenLoad()
        {
            ReservationViewModel vm = new ReservationViewModel(kernel.Get<IServiceFactory>());
            vm.Init();
            Assert.IsNotNull(vm.Reservationen);
        }
    }
}