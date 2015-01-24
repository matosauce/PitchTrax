using System;
using System.Collections.Generic;
using PitchTrax.DAOs;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.Controllers
{
    public class PitcherController
    {
        private readonly PitcherDao _pitcherDao;
        private readonly PitchTypeDao _pitchTypeDao;

        public PitcherController()
        {
            var connection = new PitchTraxDatabase().GetAsyncConnection();
            _pitchTypeDao = new PitchTypeDao(connection);
            _pitcherDao = new PitcherDao(connection);
        }

        public List<PitchType> GetAllPitchTypes()
        {
            return _pitchTypeDao.GetAllPitchTypes();
        }

        public List<Pitcher> GetAllPitchers()
        {
            return _pitcherDao.GetAllPitchers();
        }

        public Pitcher GetPitcherById(int id)
        {
            return _pitcherDao.GetPitcherById(id);
        }

        public void DeletePitcher(int id)
        {
            _pitcherDao.DeleteExistingPitcher(id);
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

                _pitcherDao.ModifyExistingPitcher(myPitcher);
            }
            else
            {
                _pitcherDao.InsertNewPitcher(myPitcher);
            }
        }
    }
}
