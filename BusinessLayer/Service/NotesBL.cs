using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
                throw e;
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

                throw e;
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

                throw e;
            }
        }
        
        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                return inotesRL.DeleteNote(userId,noteId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool PinNote(long noteId,long userId)
        {
            try
            {
                return inotesRL.PinNote(noteId,userId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool ArchieveNote(long noteId, long userId)
        {
            try
            {
                return inotesRL.ArchieveNote(noteId, userId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool TrashNote(long noteId, long userId)
        {
            try
            {
                return inotesRL.TrashNote(noteId, userId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public string ImageNotes(IFormFile image, long noteId, long userId)
        {
            try
            {
                return inotesRL.ImageNotes(image,noteId,userId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string BackgroundColorNote(long noteId, string backgroundColor)
        {
            try
            {
                return inotesRL.BackgroundColorNote(noteId,backgroundColor);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
