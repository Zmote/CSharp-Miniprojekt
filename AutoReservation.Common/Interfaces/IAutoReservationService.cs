using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        //TODO: 3 Factories, Remote, Local, Null
        // --> Remote: Normale Connection zum Server(letzte Übung)
        //--> Local: bei GetService() -> new AutoService() instanzieren
        //--> bei null --> sollte Mock-Service laden.

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
