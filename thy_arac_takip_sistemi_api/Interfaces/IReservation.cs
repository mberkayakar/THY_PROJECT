using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IReservation
    {
        //Get all reservation
        List<Reservation> GetReservations();


        //Get all reservation not door assigned
        IQueryable<Reservation> GetReservationsNotDoorAssigned { get; }
        //Get reservations from agent ıd
        //? GET 
        List<Reservation> GetActiveReservationsWithAgentId(long agentId);
        List<Reservation> GetAllReservationsFinishedWithAgentId(long agentId);
        List<Reservation> GetPassiveReservationsWithAgentId(long agentId);

        //Get reservation from plate
        Reservation GetReservationFromPlate(string plate);

        //Get reservation from id
        Reservation GetReservationFromId(int id);

        //Get reservations between two arrive date,
        IQueryable<Reservation> GetReservationsBetweenTwoArriveDate(DateTime start, DateTime finish);


        //Create reservation 
        POJO CreateReservation(Reservation reservation);

        //Update reservation
        POJO ModifyReservation(Reservation reservation);

        //Delete reservation via id
        POJO DeleteReservation(int id);

        POJO DeleteReservations(List<int> idList);




    }
}
