using HotelRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Services.Validators
{
    public interface IConflictValidator
    {
        Task<Reservation> HasConflictWith(Reservation reservation);
    }
}
