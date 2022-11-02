using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(long notesId,long userId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId);
    }
}
