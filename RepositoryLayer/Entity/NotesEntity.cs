using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{

    public class NotesEntity
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Backgroundcolor { get; set; }
        public string Image { get; set; }
        public bool Archieve { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }

        //public NotesEntity()
        //{
        //    this.Pin = true;
        //    this.Trash = false;
        //    this.Archieve = false;
        //}
    }


}
