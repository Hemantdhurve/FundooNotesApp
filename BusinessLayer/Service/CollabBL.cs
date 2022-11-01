using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL:ICollabBL
    {
        private readonly ICollabRL icollabRL;
        public CollabBL(ICollabRL icollabRL)
        {
            this.icollabRL = icollabRL;
        }

        public CollabEntity CreateCollab(long notesId, string email)
        {
            try
            {
                return icollabRL.CreateCollab(notesId, email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CollabEntity> RetrieveCollab(long notesId, long userId)
        {
            try
            {
                return icollabRL.RetrieveCollab(notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteCollab(long collabId, long userId)
        {
            try
            {
                return icollabRL.DeleteCollab(collabId, userId);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
