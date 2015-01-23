using System;
using PitchTrax.DAOs;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.Controllers
{
    public class PitcherController
    {
        private PitcherDAO _pitcherDao;
        private SQLiteConnection _connection;

        public PitcherController()
        {
            _connection  = new PitchTraxDatabase().GetAsyncConnection();
            _pitcherDao = new PitcherDAO();
        }

        public void InsertPitcher(string id, string firstName, string lastName, string number, string hand)
        {
            var myPitcher = new Pitcher
            {
                PitcherId = Convert.ToInt32(id),
                FirstName = firstName,
                LastName = lastName,
                JerseyNumber = Convert.ToInt32(number),
                Handedness = hand
            };

            var pitcherDao = new PitcherDAO();
            var connection = new PitchTraxDatabase().GetAsyncConnection();
            if (myPitcher.PitcherId != -1)
            {

                pitcherDao.ModifyExistingPitcher(connection, myPitcher);
            }
            else
            {
                pitcherDao.InsertNewPitcher(connection, myPitcher);
            }
        }
    }
}
