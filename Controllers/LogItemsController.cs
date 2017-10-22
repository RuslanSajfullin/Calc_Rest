using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication3.Context;
using WebApplication3.Entity;

namespace WebApplication3.Controllers
{
    public class LogItemsController : ApiController
    {
        private LogContext db = new LogContext();

        // GET: api/LogItems
        public IQueryable<LogItem> GetLog()
        {
            return db.Log;
        }

        // GET: api/LogItems/5
        [ResponseType(typeof(LogItem))]
        public IHttpActionResult GetLogItem(long id)
        {
            LogItem logItem = db.Log.Find(id);
            if (logItem == null)
            {
                return NotFound();
            }

            return Ok(logItem);
        }

        // PUT: api/LogItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLogItem(long id, LogItem logItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != logItem.Id)
            {
                return BadRequest();
            }

            db.Entry(logItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogItemExists(id))
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

        // POST: api/LogItems
        [ResponseType(typeof(LogItem))]
        public IHttpActionResult PostLogItem(LogItem logItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Log.Add(logItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = logItem.Id }, logItem);
        }

        // DELETE: api/LogItems/5
        [ResponseType(typeof(LogItem))]
        public IHttpActionResult DeleteLogItem(long id)
        {
            LogItem logItem = db.Log.Find(id);
            if (logItem == null)
            {
                return NotFound();
            }

            db.Log.Remove(logItem);
            db.SaveChanges();

            return Ok(logItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LogItemExists(long id)
        {
            return db.Log.Count(e => e.Id == id) > 0;
        }
    }
}