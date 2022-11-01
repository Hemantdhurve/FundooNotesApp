using CommonLayer.Model;
using RepositoryLayer.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity CreateCollab(long notesId, string email);
        public IEnumerable<CollabEntity> RetrieveCollab(long notesId, long userId);
        public bool DeleteCollab(long collabId, long userId);
    }
}
