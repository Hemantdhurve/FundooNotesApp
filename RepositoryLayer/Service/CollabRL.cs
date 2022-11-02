using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL:ICollabRL
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration iconfiguration;

        public CollabRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }

        public CollabEntity CreateCollab(long notesId, string email)
        {
            try
            {

                var noteResult = fundooContext.NotesTable.Where(x => x.NoteId == notesId).FirstOrDefault();
                var emailResult = fundooContext.Usertable.Where(x => x.Email == email).FirstOrDefault();

                if (emailResult != null && noteResult != null)
                {
                    CollabEntity collabEntity = new CollabEntity();

                    collabEntity.Email = emailResult.Email;
                    collabEntity.NoteId = noteResult.NoteId;
                    collabEntity.UserId = emailResult.UserId;

                    fundooContext.Add(collabEntity);
                    fundooContext.SaveChanges();
                    return collabEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<CollabEntity> RetrieveCollab(long collabId)
        {
            try
            {
                var result = fundooContext.CollabTable.Where(x => x.CollabId == collabId);  //noteId used
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteCollab(long collabId, long noteId)
        {
            try
            {
                var result = fundooContext.CollabTable.FirstOrDefault(x => x.CollabId == collabId);

                fundooContext.CollabTable.Remove(result);

                fundooContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
