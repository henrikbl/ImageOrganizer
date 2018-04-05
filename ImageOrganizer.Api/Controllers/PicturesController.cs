using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ImageOrganizer.DataAccess;
using ImageOrganizer.Model;

namespace ImageOrganizer.Api.Controllers
{
    public class PicturesController : ApiController
    {
        private ImageOrganizerContext db = new ImageOrganizerContext();

        // GET: api/Pictures
        public IQueryable<Picture> GetPictures()
        {
            return db.Pictures;
        }

        // GET: api/Pictures/5
        [ResponseType(typeof(Picture))]
        public async Task<IHttpActionResult> GetPicture(int id)
        {
            Picture picture = await db.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            return Ok(picture);
        }

        // PUT: api/Pictures/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPicture(int id, Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != picture.PictureId)
            {
                return BadRequest();
            }

            db.Entry(picture).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
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

        // POST: api/Pictures
        [ResponseType(typeof(Picture))]
        public async Task<IHttpActionResult> PostPicture(Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pictures.Add(picture);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = picture.PictureId }, picture);
        }

        // POST: api/Pictures/5/Groups/5
        [HttpPost()]
        [Route("api/Pictures/{pictureId}/Groups/{groupId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AddToGroup(int pictureId, int groupId)
        {
            try
            {
                using(var conn = new SqlConnection(db.Database.Connection.ConnectionString.ToString()))
                {
                    var cmd = new SqlCommand("INSERT INTO PictureGroup(PictureId, GroupId) VALUES (@PictureId, @GroupId)", conn);
                    cmd.Parameters.AddWithValue("@PictureId", pictureId);
                    cmd.Parameters.AddWithValue("@GroupId", groupId);

                    conn.Open();

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                return InternalServerError();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Pictures/5
        [ResponseType(typeof(Picture))]
        public async Task<IHttpActionResult> DeletePicture(int id)
        {
            Picture picture = await db.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            db.Pictures.Remove(picture);
            await db.SaveChangesAsync();

            return Ok(picture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PictureExists(int id)
        {
            return db.Pictures.Count(e => e.PictureId == id) > 0;
        }
    }
}