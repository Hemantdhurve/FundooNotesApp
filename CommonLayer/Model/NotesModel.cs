using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CommonLayer.Model
{
    public class NotesModel
    {
        //public NotesModel()
        //{
        //    this.Pin = true;
        //    this.Trash = false;
        //    this.Archieve = false;
        //}

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Backgroundcolor { get; set; }
        public string Image { get; set; }
        public bool Pin { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }    
        public bool Trash { get; set; } 
        public bool Archieve { get; set; }

       
      
    }
}
