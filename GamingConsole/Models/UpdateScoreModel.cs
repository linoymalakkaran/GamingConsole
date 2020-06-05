using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingConsole.Models
{
    public class UpdateScoreModel
    {
        public int ConsoleNumber { get; set; }
        //public int TeamOneScore { get; set; }
        //public int TeamTwoScore { get; set; }
        public bool IsTeamOne { get; set; }
        public bool IsIncrease { get; set; }
    }
}