using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL:ILabelBL
    {
        private readonly ILabelRL ilabelRL;
        public LabelBL(ILabelRL ilabelRL)
        {
            this.ilabelRL = ilabelRL;
        }

        public LabelEntity CreateLabel(long notesId,long userId, string labelName)
        {
            try
            {
                return ilabelRL.CreateLabel(notesId,userId, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<LabelEntity> RetrieveLabel(long labelId)
        {
            try
            {
                return ilabelRL.RetrieveLabel(labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteLabel(long labelId)
        {
            try
            {
                return ilabelRL.DeleteLabel(labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public LabelEntity EditLabel(long noteId, string labelName)
        {
            try
            {
                return ilabelRL.EditLabel(noteId,labelName);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
