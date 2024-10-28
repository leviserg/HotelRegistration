using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.DbContexts
{
    public class ReservationDbContextFactory
    {
        private readonly string _connectionString;

        public ReservationDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReservationDbContext CreateDbContext() {

            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options;

            return new ReservationDbContext(options);
        }
    }
}
