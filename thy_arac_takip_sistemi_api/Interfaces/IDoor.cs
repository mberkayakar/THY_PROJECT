using System;
using System.Linq;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IDoor
    {
        POJO MatchDoorAndSCC(DoorAndSCC doorAndScc);
        POJO MatchDoorAndSCCList(DoorAndSCCList doorAndScc);
        POJO ModifyDoor(Door door);
        IQueryable<Door> GetDoorAll { get; }
        Door GetDoorAvaliableWithSCC(SCC scc);
        POJO RemoveReservation(int doorId);
        POJO MakeReservationManual(int doorId, int reservationId);
    }
}
