using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;

namespace AutoReservation.Ui.Factory
{
    class LocalDataAccessServiceFactory : IServiceFactory
    {
        public IAutoReservationService GetService()
        {
            return new AutoReservationService();
        }
    }
}
