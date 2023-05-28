using HotelApi.data;
using HotelApi.model_s;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Repository
{
    
        public class ReservationRepository : IReservation
        {
            private readonly HotelContext ReservationContext;

            public ReservationRepository(HotelContext con)
            {
                ReservationContext = con;
            }

            public IEnumerable<Reservation> GetReservation()
            {
                try
                {
                    return ReservationContext.Reservations.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to retrieve  ", ex);
                }
            }

            public Reservation GetReservationById(int reservationId)
            {
                try
                {
                    return ReservationContext.Reservations.FirstOrDefault(x => x.ReservationId == reservationId);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Not found with ID: {reservationId}", ex);
                }
            }

            public Reservation PostReservation(Reservation Reservation)
            {
                try
                {
                    var room = ReservationContext.Rooms.Find(Reservation.Room.RoomId);
                    if (room != null)
                    {
                        if (room.RoomCount > 0)
                        {
                            room.RoomCount--; // Decrease the available room count
                            ReservationContext.Entry(room).State = EntityState.Modified; // Update the room count in the database

                            // Assign the room to the booking
                            Reservation.Room = room;
                        }
                        else
                        {
                            throw new Exception("No available rooms for booking.");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid room ID.");
                    }

                    ReservationContext.Add(Reservation);
                    ReservationContext.SaveChanges();
                    return Reservation;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to create booking.", ex);
                }
            }

            public Reservation PutReservation(int reservationId, Reservation Reservation)
            {
                try
                {
                    var hotel = ReservationContext.Hotels.Find(Reservation.Hotel.HotelId);

                    Reservation.Hotel = hotel;
                    ReservationContext.Entry(Reservation).State = EntityState.Modified;
                    ReservationContext.SaveChanges();
                    return Reservation;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to update booking.", ex);
                }
            }

            public void DeleteReservation(int reservationId)
            {
                try
                {
                    var reservation = ReservationContext.Reservations.Find(reservationId);
                    if (reservation != null)
                    {
                        var room = ReservationContext.Rooms.Find(reservation.Room.RoomId);
                        if (room != null)
                        {
                            room.RoomCount++; // Increase the available room count
                            ReservationContext.Entry(room).State = EntityState.Modified; // Update the room count in the database
                        }

                        ReservationContext.Reservations.Remove(reservation);
                        ReservationContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to delete booking.", ex);
                }
            }

            Reservation IReservation.DeleteReservation(int reservationId)
            {
                throw new NotImplementedException();
            }
        }
    }
 