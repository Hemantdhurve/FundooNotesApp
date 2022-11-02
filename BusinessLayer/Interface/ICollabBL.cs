using CommonLayer.Model;
using RepositoryLayer.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity CreateCollab(long notesId, string email);
        public IEnumerable<CollabEntity> RetrieveCollab(long collabId);
        public bool DeleteCollab(long collabId, long userId);
    }
}
