using CloudinaryDotNet;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration iconfiguration;
       
        public NotesRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
      
        public NotesEntity CreateNotes(NotesModel notesModel,long userId)
        {
            try
            {
                
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId);
                NotesEntity notesEntity = new NotesEntity();
                if (result != null)
                {
                    notesEntity.UserId=userId;
                    notesEntity.Title = notesModel.Title;
                    notesEntity.Description = notesModel.Description;
                    notesEntity.Reminder = notesModel.Reminder;
                    notesEntity.Backgroundcolor = notesModel.Backgroundcolor;
                    notesEntity.Image = notesModel.Image;
                    notesEntity.Archieve = notesModel.Archieve;
                    notesEntity.Pin = notesModel.Pin;
                    notesEntity.Trash = notesModel.Trash;
                    notesEntity.Created = notesModel.Created;
                    notesEntity.Edited = notesModel.Edited;

                    fundooContext.NotesTable.Add(notesEntity);

                    fundooContext.SaveChanges();
                    return notesEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<NotesEntity> RetrieveNotes(long userId,long noteId)
        {
            try
            {
                var result= fundooContext.NotesTable.Where(x => x.NoteId == noteId );
                return result;         
            }
            catch (Exception e) 
            {
                throw;
            }
        }

        public NotesEntity UpdateNote(long userId,long noteId, NotesModel notesModel)
        {
            try
            {
                var notesEntity = fundooContext.NotesTable.FirstOrDefault(e => e.NoteId == noteId);
                if (notesEntity != null)
                {
                    notesEntity.Title = notesModel.Title;
                    notesEntity.Description = notesModel.Description;
                    notesEntity.Archieve = notesModel.Archieve;
                    notesEntity.Backgroundcolor = notesModel.Backgroundcolor;
                    notesEntity.Pin = notesModel.Pin;
                    notesEntity.Reminder = notesModel.Reminder;
                    notesEntity.Trash = notesModel.Trash;
                    notesEntity.Created = notesModel.Created;
                    notesEntity.Edited = notesModel.Edited;

                    fundooContext.SaveChanges();
                    return notesEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId);

                fundooContext.NotesTable.Remove((NotesEntity)result);

                fundooContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }



        public bool PinNote(long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId);
                //Pin is bydefault true so checking its working use if else condition

                if (result.Pin == false)
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return true;
                }

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ArchieveNote(long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId);
                
                if (result.Archieve != true)
                {
                    result.Archieve = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Archieve = false; 
                    fundooContext.SaveChanges();
                    return true;
                }
 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool TrashNote(long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId);

                if (result.Trash != true)
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return true;
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ImageNotes(IFormFile image,long noteId,long userId)
        {
            try
            {
                var result= fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteId && x.UserId==userId);
                if (result != null)
                {
                    Account account = new Account(
                      this.iconfiguration["CloudinarySettings:CloudName"],
                      this.iconfiguration["CloudinarySettings:ApiKey"],
                      this.iconfiguration["CloudinarySettings:ApiSecret"]

                        );
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                   
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image Uploaded Successfully";
                    

                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
