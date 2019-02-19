using MyHangman.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHangman.Models
{
    public class Player
    {
        //Properties - same as what's in the database
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //returns the playerid username first_name last_name
        public override string ToString()
        {
            return PlayerId.ToString().PadRight(6) + Username.PadRight(30) + FirstName.PadRight(30) + LastName.PadRight(30);
        }
    }
}
