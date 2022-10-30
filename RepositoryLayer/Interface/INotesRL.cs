using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId);
        public NotesEntity UpdateNote(long userId,long noteId, NotesModel notesModel);
        public bool DeleteNote(long userId, long noteId);
        public bool PinNote(long noteId);
      }
}
