using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabProject.Models
{
    public class PlayersViewModel
    {
        public IEnumerable<Player> Players { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}