﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AG_RESTful.Models;
using static System.Net.Mime.MediaTypeNames;

namespace AG_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly AssistantGenesisDataContext _context;

        public DocumentsController(AssistantGenesisDataContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return await _context.Document.ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Document.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        [HttpGet("user={userId}&type={type}")]
        public ActionResult<Document> GetDocumentByUserAndType(int userId, string type)
        {
            var document = _context.Document.Where(doc => doc.userID == userId && doc.Type.Equals(type)).OrderByDescending(doc => doc.ID).FirstOrDefault();

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.ID)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            //_context.Document.Add(document);
            _context.Database.ExecuteSqlRawAsync($"INSERT INTO Document (userID, TimeStamp, Type, Path) VALUES ({document.userID}, '{document.TimeStamp.ToString("yyyy-MM-ddTHH:mm:ss.fff")}', '{document.Type}', '{document.Path}')");
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDocument", new { id = document.ID }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Document.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            _context.Document.Remove(document);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool DocumentExists(int id)
        {
            return _context.Document.Any(e => e.ID == id);
        }
    }
}
