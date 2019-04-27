using MyHangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHangman.DAL
{
    //Will be used to create player objects pulled from the database
    public interface IPlayerDAO
    {
        /// <summary>
        /// Returns a player by the username.
        /// </summary>
        /// <param name="username">The player to search for.</param>
        /// <returns></returns>
        IList<Player> GetPlayerByUsername(string username);

        /// <summary>
        /// Adds a new player.
        /// </summary>
        /// <param name="player">The new player to add.</param>
        bool AddUser(Player player);

        /// <summary>
        /// Returns a list of users.
        /// </summary>
        /// <returns></returns>
        IList<Player> GetAllUsers();

        /// <summary>
        /// Returns the top 5 players.
        /// </summary>
        /// <returns></returns>
        IList<Player> GetLeaderboard();

    }
}
