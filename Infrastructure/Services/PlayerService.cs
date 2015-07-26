using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LabProject.Models;
using LabProject.Infrastructure;

namespace LabProject.Infrastructure.Services
{
    public class PlayerService
    {
        private List<Player> players = PlayersFileReader.GetAll();

        /// <summary>
        /// Returns PlayersViewModel-object that contains Player collection and PageInfo object
        /// </summary>
        public PlayersViewModel GetPlayersWithPage(int page, int itemsPerPage)
        {
            int totalPages = (int)Math.Ceiling((double)players.Count / itemsPerPage);

            if (page < 1) page = 1;
            else if (page > totalPages) page = totalPages;

            var playersViewModel = new PlayersViewModel();
            var pageInfo = new PageInfo();

            pageInfo.TotalPages = totalPages;
            pageInfo.PageNumber = page;

            var items = players.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            playersViewModel.Players = items;
            playersViewModel.PageInfo = pageInfo;

            return playersViewModel;
        }
    }
}