using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId);
        public NotesEntity UpdateNote(long userId,long noteId, NotesModel notesModel);
        public bool DeleteNote(long userId, long noteId);
        public bool PinNote(long noteId);
        public bool ArchieveNote(long noteId);
        public bool TrashNote(long noteId);
    }
}
