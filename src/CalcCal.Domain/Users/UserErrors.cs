using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Domain.Users
{
    internal static class UserErrors
    {
        public static Error InvalidPhoneNumber =>
            new Error(
                "Users.InvalidPhoneNumber",
                "Invalid phone number");
        public static Error InvalidFirstName =>
            new Error(
                "Users.InvalidPhoneNumber",
                "Invalid first name");
        public static Error InvalidLastName =>
            new Error(
                "Users.InvalidPhoneNumber",
                "Invalid last name");
    }
}
