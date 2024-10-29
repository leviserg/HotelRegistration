﻿using HotelRegistration.DTOs;
using HotelRegistration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelRegistration.DbContexts
{
    public class ReservationDbContextDesign : IDesignTimeDbContextFactory<ReservationDbContext>
    {
        private const string connectionStringKey = "ReservationsDbConnection";
        private string DB_CONNECTION_STRING => ConfigurationHelper.GetConnectionString(connectionStringKey);
        public ReservationDbContext CreateDbContext(string[] args)
        {
            ReservationDbContextFactory _dbContextFactory = new ReservationDbContextFactory(DB_CONNECTION_STRING);
            return _dbContextFactory.CreateDbContext();
        }
    }
}