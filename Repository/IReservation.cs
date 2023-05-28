using HotelApi.model_s;

namespace HotelApi.Repository
{
    public interface IReservation
    {
        IEnumerable<Reservation> GetReservation();
        Reservation GetReservationById(int reservationId);
        Reservation PostReservation(Reservation Reservation);
        Reservation PutReservation(int reservationId, Reservation Reservation);
        Reservation DeleteReservation(int reservationId);
    }
}
