﻿using AutoReservation.Common.Interfaces;
using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class AutoReservationService : IAutoReservationService
    {
        private AutoReservationBusinessComponent businessComponent;
        public AutoReservationService()
        {
            businessComponent = new AutoReservationBusinessComponent();
        }

        public IEnumerable<AutoDto> Autos
        {
            get
            {
                return DtoConverter.ConvertToDtos(businessComponent.GetAutos());     
            }
        }

        public IEnumerable<KundeDto> Kunden
        {
            get
            {
                return DtoConverter.ConvertToDtos(businessComponent.GetKunden());
            }
        }

        public IEnumerable<ReservationDto> Reservationen
        {
            get
            {
                return DtoConverter.ConvertToDtos(businessComponent.GetReservations());
            }
        }

        public void DeleteAuto(AutoDto selectedAuto)
        {
            businessComponent.DeleteAuto(DtoConverter.ConvertToEntity(selectedAuto));
        }

        public void DeleteReservation(ReservationDto selectedReservation)
        {
            businessComponent.DeleteReservation(DtoConverter.ConvertToEntity(selectedReservation));
        }

        public void InsertAuto(AutoDto auto)
        {
            businessComponent.InsertAuto(DtoConverter.ConvertToEntity(auto));
        }

        public void InsertReservation(ReservationDto reservation)
        {
            businessComponent.InsertReservation(DtoConverter.ConvertToEntity(reservation));
        }

        public void UpdateAuto(AutoDto modifiedAuto, AutoDto original)
        {
            businessComponent.UpdateAuto(DtoConverter.ConvertToEntity(modifiedAuto), DtoConverter.ConvertToEntity(original));
        }

        public void UpdateReservation(ReservationDto modifiedReservation, ReservationDto original)
        {
            businessComponent.UpdateReservation(DtoConverter.ConvertToEntity(modifiedReservation), DtoConverter.ConvertToEntity(original));
        }

        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }

        public void InsertKunde(KundeDto kunde)
        {
            businessComponent.InsertKunde(DtoConverter.ConvertToEntity(kunde));
        }

        public void UpdateKunde(KundeDto modifiedKunde, KundeDto original)
        {
            businessComponent.UpdateKunde(DtoConverter.ConvertToEntity(modifiedKunde), DtoConverter.ConvertToEntity(original));
        }

        public void DeleteKunde(KundeDto selectedKunde)
        {
            businessComponent.DeleteKunde(DtoConverter.ConvertToEntity(selectedKunde));
        }
    }
}