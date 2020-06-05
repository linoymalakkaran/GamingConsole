using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingConsole.Models
{
    public class BasketBallBoard
    {
        [Required]
        [Key]
        [DisplayName("Console Number")]
        public int ConsoleNumber { get; set; }

        [DisplayName("Team One Score")]
        public int TeamOneScore { get; set; }

        [DisplayName("Team Two Score")]
        public int TeamTwoScore { get; set; }

        [DisplayName("Console Name")]
        public string ConsoleName { get; set; }

        [DisplayName("Status")]
        [Required]
        public string Status { get; set; }
    }
}