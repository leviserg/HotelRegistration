using HotelRegistration.DTOs;
using HotelRegistration.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelRegistration.DbContexts
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<ReservationDto> Reservations { get; set; }

        public bool HasTable(string tableName)
        {
            var connection = this.Database.GetDbConnection();
            try
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "@tableName";
                parameter.Value = tableName;
                cmd.Parameters.Add(parameter);

                using (var reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
