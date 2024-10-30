using Microsoft.EntityFrameworkCore.Design;


namespace HotelRegistration.DbContexts
{
    public class ReservationDbContextDesign : IDesignTimeDbContextFactory<ReservationDbContext>
    {
        private const string connectionStringKey = "ReservationsDbConnection";
        private string DB_CONNECTION_STRING => Environment.GetEnvironmentVariable(connectionStringKey);
        public ReservationDbContext CreateDbContext(string[] args)
        {
            ReservationDbContextFactory _dbContextFactory = new ReservationDbContextFactory(DB_CONNECTION_STRING);
            return _dbContextFactory.CreateDbContext();
        }
    }
}