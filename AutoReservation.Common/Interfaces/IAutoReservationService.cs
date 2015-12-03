using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        //TODO: Add WCF Contract expression
        //TODO: 3 Factories, Remote, Local, Null
        // --> Remote: Normale Connection zum Server(letzte Übung)
        //--> Local: bei GetService() -> new AutoService() instanzieren
        //--> bei null --> sollte Mock-Service laden.

        IEnumerable<KundeDto> Kunden { get;}
        IEnumerable<AutoDto> Autos { get;}
        IEnumerable<ReservationDto> Reservationen { get;}
        
        void InsertReservation(ReservationDto reservation);
        void UpdateReservation(ReservationDto modifiedReservation, ReservationDto original);
        void DeleteReservation(ReservationDto selectedReservation);
        void InsertAuto(AutoDto auto);
        void UpdateAuto(AutoDto modifiedAuto, AutoDto original);
        void DeleteAuto(AutoDto selectedAuto);
        void InsertKunde(KundeDto kunde);
        void UpdateKunde(KundeDto modifiedKunde, KundeDto original);
        void DeleteKunde(KundeDto selectedKunde);
    }
}
