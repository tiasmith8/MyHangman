using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using MyHangman.Models;

namespace MyHangman.DAL
{
    public class PlayerSqlDAO : IPlayerDAO
    {
        //variable to hold the connection information passed in from Main()
        private string connectionString;

        //constructor that takes in the connection string 
        public PlayerSqlDAO(string databaseconnectionString)
        {
            //Set whatever database it's connecting to to the private variable to use in this class.
            this.connectionString = databaseconnectionString;
        }

        /// <summary>
        /// List players
        /// </summary>
        /// <returns></returns>
        public IList<Player> GetAllUsers()
        {
            //Create a list of players to be returned.
            List<Player> gamePlayers = new List<Player>();

            //Open a connection to the database
            try
            {   //pass in the connection string to a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Open the connection
                    conn.Open();

                    //Create a command to query for the users
                    SqlCommand cmd = new SqlCommand("SELECT * FROM players;", conn);
                    //Execute the command. Since reading in data, get SqlDataReader back
                    SqlDataReader reader = cmd.ExecuteReader();

                    //Read in each row creating object for each row
                    while(reader.Read())
                    {
                        Player player = ConvertReaderToPlayer(reader);
                        gamePlayers.Add(player);
                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Issue with connecting to the database");
                Console.WriteLine(ex.Message);
                throw;
            }

            return gamePlayers;
        }

        /// <summary>
        /// Converts a row returned from database into a player object to add to the List of player objects.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Player ConvertReaderToPlayer(SqlDataReader reader)
        {
            Player player = new Player();
            player.PlayerId = Convert.ToInt32(reader["player_id"]);
            player.FirstName = Convert.ToString(reader["first_name"]);
            player.LastName = Convert.ToString(reader["last_name"]);
            player.Username = Convert.ToString(reader["username"]);
            player.Password = Convert.ToString(reader["password"]);

            return player;
        }

        public bool AddUser(Player player)
        {
            //Open db connection
            try
            {
                //Implements IDisposable
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Open the connection
                    conn.Open();

                    //Insert new data into the database
                    SqlCommand cmd = new SqlCommand("INSERT INTO players " +
                        "VALUES(@first_name, @last_name, @username, @password)", conn);
                    cmd.Parameters.AddWithValue("@first_name", player.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", player.LastName);
                    cmd.Parameters.AddWithValue("@username", player.Username);
                    cmd.Parameters.AddWithValue("@password", player.Password);

                    return (cmd.ExecuteNonQuery()==1);
                }
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Issue connecting to the database");
                Console.WriteLine(ex.Message);
                throw;
            }


        }

        public IList<Player> GetPlayerByUsername(string username)
        {
            throw new NotImplementedException();
        }

        
    }
}
