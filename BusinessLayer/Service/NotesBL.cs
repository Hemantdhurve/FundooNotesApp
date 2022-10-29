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
    public class NotesBL:INotesBL
    {
        private readonly INotesRL inotesRL;
        public NotesBL(INotesRL inotesRL)
        {
            this.inotesRL = inotesRL;     
        }

        public NotesEntity CreateNotes(NotesModel notesModel, long userId)
        {
            
            try
            {
                return inotesRL.CreateNotes(notesModel,userId);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId)
        {
            try
            {
                return inotesRL.RetrieveNotes(userId,noteId);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public NotesEntity UpdateNote(long userId,long noteId, NotesModel notesModel)
        {
            try
            {
                return inotesRL.UpdateNote(userId,noteId,notesModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
