using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sperience;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace Sperience.Controllers
{
    public class SubjectDocumentsController : ApiController
    {
        private SperienceEntities db = new SperienceEntities();

        // GET: api/SubjectDocuments
        public IQueryable<SubjectDocument> GetSubjectDocuments()
        {
            return db.SubjectDocuments;
        }

        // GET: api/SubjectDocuments/5
        [ResponseType(typeof(SubjectDocument))]
        public IHttpActionResult GetSubjectDocument(int id)
        {
            SubjectDocument subjectDocument = db.SubjectDocuments.Find(id);
            if (subjectDocument == null)
            {
                return NotFound();
            }

            return Ok(subjectDocument);
        }

        // PUT: api/SubjectDocuments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubjectDocument(int id, SubjectDocument subjectDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectDocument.Id)
            {
                return BadRequest();
            }

            db.Entry(subjectDocument).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectDocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //public async Task<HttpResponseMessage> PostFile()
        //{
        //    // Check if the request contains multipart/form-data. 
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    string root = HttpContext.Current.Server.MapPath("~/App_Data");
        //    var provider = new MultipartFormDataStreamProvider(root);

        //    try
        //    {
        //        StringBuilder sb = new StringBuilder(); // Holds the response body 

        //        // Read the form data and return an async task. 
        //        await Request.Content.ReadAsMultipartAsync(provider);

        //        // This illustrates how to get the form data. 
        //        foreach (var key in provider.FormData.AllKeys)
        //        {
        //            foreach (var val in provider.FormData.GetValues(key))
        //            {
        //                sb.Append(string.Format("{0}: {1}\n", key, val));
        //            }
        //        }

        //        // This illustrates how to get the file names for uploaded files. 
        //        foreach (var file in provider.FileData)
        //        {
        //            FileInfo fileInfo = new FileInfo(file.LocalFileName);
        //            sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));
        //        }
        //        return new HttpResponseMessage()
        //        {
        //            Content = new StringContent(sb.ToString())
        //        };
        //    }
        //    catch (System.Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}

        //[HttpPost]
        //public IHttpActionResult PostFileUpload()
        //{

        //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        // Get the uploaded image from the Files collection  
        //        var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];
        //        if (httpPostedFile != null)
        //        {
        //            SubjectDocument subjectDocument = new SubjectDocument();
        //            int length = httpPostedFile.ContentLength;
        //            var docdata = new byte[length];   
        //            httpPostedFile.InputStream.Read(docdata, 0, length);
        //            subjectDocument.SubjectDocumentDescription = Path.GetFileName(httpPostedFile.FileName);
        //            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
        //            subjectDocument.SubjectDocumentPath = fileSavePath;
        //            db.SubjectDocuments.Add(subjectDocument);
        //            db.SaveChanges();
                    
        //            // Save the uploaded file to "UploadedFiles" folder  
        //            httpPostedFile.SaveAs(fileSavePath);
        //            return Ok("File Uploaded");
        //        }
        //    }
        //    return Ok("File is not Uploaded");
        //}  

        // POST: api/SubjectDocuments
        [HttpPost]
        public IHttpActionResult PostSubjectDocument()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                //Get the uploaded image from the Files collection  
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];
                if (httpPostedFile != null)
                {
                    SubjectDocument document = new SubjectDocument();
                    int length = httpPostedFile.ContentLength;
                    byte[] Filedata = new byte[length];  
                    httpPostedFile.InputStream.Read(Filedata, 0, length);
                    document.SubjectDocumentDescription = Path.GetFileName(httpPostedFile.FileName);
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
                    if (System.IO.File.Exists(fileSavePath))
                    {
                        return Ok("File Already Exist");
                    }
                    document.SubjectDocumentPath = fileSavePath;
                    document.SubjectId = 5;
                    document.LocationId = 1;
                    document.CompanyId = 1;
                    document.Createdby = 1;
                    document.CreatedDate = System.DateTime.Today;
                    db.SubjectDocuments.Add(document);
                    db.SaveChanges();
                    //var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
                    // Save the uploaded file to "UploadedFiles" folder  
                    httpPostedFile.SaveAs(fileSavePath);
                    return Ok("File Uploaded");
                }
            }
            return Ok("File is not Uploaded");
        }

        // DELETE: api/SubjectDocuments/5
        
        [ResponseType(typeof(String))]
        public IHttpActionResult DeleteSubjectDocument(int id)
        {
            SubjectDocument subjectDocument = db.SubjectDocuments.Find(id);
            if (subjectDocument == null)
            {
                return NotFound();
            }

            string fullPath = subjectDocument.SubjectDocumentPath;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.SubjectDocuments.Remove(subjectDocument);
            db.SaveChanges();

            return Ok("File is deleted");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectDocumentExists(int id)
        {
            return db.SubjectDocuments.Count(e => e.Id == id) > 0;
        }
    }
}