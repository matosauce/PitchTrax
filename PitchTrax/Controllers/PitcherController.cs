using System;
using System.Collections.Generic;
using PitchTrax.DAOs;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.Controllers
{
    public class PitcherController
    {
        private readonly PitcherDAO _pitcherDao;
        private readonly SQLiteConnection _connection;
        private readonly PitchTypeDAO _pitchTypeDao;

        public PitcherController()
        {
            _connection  = new PitchTraxDatabase().GetAsyncConnection();
            _pitchTypeDao = new PitchTypeDAO();
            _pitcherDao = new PitcherDAO();
        }

        public List<PitchType> GetAllPitchTypes()
        {
            return _pitchTypeDao.GetAllPitchTypes(_connection);
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

            if (myPitcher.PitcherId != -1)
            {

                _pitcherDao.ModifyExistingPitcher(_connection, myPitcher);
            }
            else
            {
                _pitcherDao.InsertNewPitcher(_connection, myPitcher);
            }
        }
    }
}
