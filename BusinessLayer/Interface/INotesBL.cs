﻿using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public string ImageNotes(IFormFile image, long noteId, long userId);
    }
}
