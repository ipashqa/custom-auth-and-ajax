using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Threading.Tasks;
using LabProject.Models;
using System.IO;

namespace LabProject.Infrastructure
{
    public static class PlayersJsonReader
    {
        public static async Task<List<Player>> GetPlayersAsync()
        {
            string strData;
            using(StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/Players/PlayersJSON.json")))
            {
                strData = reader.ReadToEnd();
            }

            var result = await Task.Run(() => JsonConvert.DeserializeObject<List<Player>>(strData));

            return result;
        }
    }
}