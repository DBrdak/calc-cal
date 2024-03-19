using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Domain.Users
{
    public static class UserErrors
    {
        public static Error InvalidPhoneNumber =>
            new Error(
                "Users.InvalidPhoneNumber",
                "Invalid phone number");
        public static Error InvalidFirstName =>
            new Error(
                "Users.InvalidFirstName",
                "Invalid first name");
        public static Error InvalidLastName =>
            new Error(
                "Users.InvalidLastName",
                "Invalid last name");
        public static Error InvalidUsername =>
            new Error(
                "Users.InvalidUsername",
                "Invalid username");

        public static Error InvalidFood =>
            new Error(
                "Users.InvalidFood",
                "Invalid eaten food data");

        public static Error InvalidCredentials =>
            new Error(
                "Users.InvalidCredentials",
                "Invalid credentials");
    }
}
