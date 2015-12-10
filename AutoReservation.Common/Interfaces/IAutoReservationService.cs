using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        IEnumerable<KundeDto> Kunden
        {
            [OperationContract] get;
        }
        IEnumerable<AutoDto> Autos
        {
            [OperationContract] get;
        }
        IEnumerable<ReservationDto> Reservationen
        {
            [OperationContract] get;
        }

        [OperationContract]
        ReservationDto GetReservationByNr(int nr);
        [OperationContract]
        KundeDto GetKundeById(int id);
        [OperationContract]
        AutoDto GetAutoById(int id);
        [OperationContract]
        void InsertReservation(ReservationDto reservation);
        [OperationContract]
        [FaultContract(typeof(ReservationDto))]
        void UpdateReservation(ReservationDto modifiedReservation, ReservationDto original);
        [OperationContract]
        void DeleteReservation(ReservationDto selectedReservation);
        [OperationContract]
        void InsertAuto(AutoDto auto);
        [OperationContract]
        [FaultContract(typeof(AutoDto))]
        void UpdateAuto(AutoDto modifiedAuto, AutoDto original);
        [OperationContract]
        void DeleteAuto(AutoDto selectedAuto);
        [OperationContract]
        void InsertKunde(KundeDto kunde);
        [OperationContract]
        [FaultContract(typeof(KundeDto))]
        void UpdateKunde(KundeDto modifiedKunde, KundeDto original);
        [OperationContract]
        void DeleteKunde(KundeDto selectedKunde);
    }
}
