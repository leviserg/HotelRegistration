SELECT TOP (1000) [ReservationId]
      ,[FloorNumber]
      ,[RoomNumber]
      ,[VisitorName]
      ,[StartDate]
      ,[EndDate]
FROM [HotelReservation].[dbo].[Reservations]

DELETE FROM [dbo].[Reservations] WHERE ReservationId = 1006

DBCC CHECKIDENT ('[Reservations]', RESEED, 7);