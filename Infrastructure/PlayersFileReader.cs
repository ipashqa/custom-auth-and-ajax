using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using LabProject.Models;

namespace LabProject.Infrastructure
{
    public static class PlayersFileReader
    {
        static public List<Models.Player> GetAll()
        {
            string[] players = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/Content/Players/Players.txt"), Encoding.Default);
            string[] points = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/Content/Players/Points.txt"));
            string[] goals = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/Content/Players/Goals.txt"));
            string[] assists = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/Content/Players/Assists.txt"));
            string[] nationality = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/Content/Players/Nationality.txt"));

            int itemsCount = players.Length;

            var result = new List<Player>();

            for (int i = 0; i < itemsCount; ++i)
            {
                Player player = new Player();

                player.Points = int.Parse(points[i]);
                player.Goals = int.Parse(goals[i]);
                player.Assists = int.Parse(assists[i]);
                player.Nationality = nationality[i];

                string[] splittedPlayer = players[i].Split(' ');
                player.Name = splittedPlayer[0] + " " + splittedPlayer[1];

                result.Add(player);
            }

            return result;
        }
    }
}