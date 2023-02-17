using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(long noteId,long userId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId);
        public IEnumerable<LabelEntity> RetrieveAllLabel(long userId);
        public bool DeleteLabel(long labelId);
        public LabelEntity EditLabel(long noteId, string labelName);
    }
}
