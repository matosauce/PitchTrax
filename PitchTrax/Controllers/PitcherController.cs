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
            var connection = new PitchTraxDatabase().GetConnection();
            _pitchTypeDao = new PitchTypeDao(connection);
            _pitcherDao = new PitcherDao(connection);
        }

        public IEnumerable<PitchType> GetAllPitchTypes()
        {
            return _pitchTypeDao.GetAllPitchTypes();
        }

        public IEnumerable<Pitcher> GetAllPitchers()
        {
            return _pitcherDao.GetAllPitchers();
        }

        public Pitcher GetPitcherById(int id)
        {
            return _pitcherDao.GetPitcherById(id);
        }

        public IEnumerable<PitchType> GetPitchTypesKnownByPitcher(int pitcherId)
        {
            return _pitchTypeDao.GetPitchesKnownByPitcher(pitcherId);
        } 

        public void DeletePitcher(int id)
        {
            _pitcherDao.DeleteExistingPitcher(id);
        }

        public void SaveNewPitchTypesToPitcher(int pitcherId, IEnumerable<int> pitchTypeIds)
        {
            _pitchTypeDao.UpdateKnownPitchTypes(pitcherId, pitchTypeIds);
        } 

        public int InsertPitcher(string id, string firstName, string lastName, string number, string hand)
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
                return _pitcherDao.ModifyExistingPitcher(myPitcher);
            return _pitcherDao.InsertNewPitcher(myPitcher);
        }
    }
}
