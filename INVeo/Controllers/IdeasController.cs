using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INVeo.Data;
using INVeo.Models;
using System.Text;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Numerics;
using INVeo.Services;
using NBitcoin.Secp256k1;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace INVeo.Controllers
{
    public class IdeasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdeasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ideas
        public async Task<IActionResult> Index()
        {
              return _context.Idea != null ? 
                          View(await _context.Idea.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Idea'  is null.");
        }

        // GET: DetailsForInvestor
        public async Task<IActionResult> DetailsForInvestor(int? id)
        {
            if (id == null || _context.Idea == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // GET: Ideas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Idea == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // GET: Ideas/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult PaymentPage()
        {
            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IdeaId,Seller,Price")] Idea idea)
        {
            //// Uploaded files by use GUID
            //if (ideaFile != null)
            //{
            //    var ideaFileName = Guid.NewGuid().ToString() + ".jpg";

            //    var ideaFileFullPath = System.IO.Path.Combine(
            //        System.IO.Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", ideaFileName);

            //    using (var stream = new System.IO.FileStream(ideaFileFullPath, System.IO.FileMode.Create))
            //    {
            //        await ideaFile.CopyToAsync(stream);
            //    }

            //    idea.IdeaFile = ideaFileName;
            //}
            if (ModelState.IsValid)
            {
                _context.Add(idea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(idea);
        }

        // GET: Ideas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Idea == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea.FindAsync(id);
            if (idea == null)
            {
                return NotFound();
            }
            return View(idea);
        }

        // POST: Ideas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brief,IsAccepted")] Idea idea)
        {
            if (id != idea.Id)
            {
                return NotFound();
            }
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdeaExists(idea.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(idea);
        }

        // GET: Ideas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Idea == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // POST: Ideas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Idea == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Idea'  is null.");
            }
            var idea = await _context.Idea.FindAsync(id);
            if (idea != null)
            {
                _context.Idea.Remove(idea);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Idea With Blockchain
        public async Task CreateIdeaAsync(Idea model)
        {
            BlockchainService blockchainService = new BlockchainService();
            string contractAddress = await blockchainService.DeployContractAsync(model.Title, model.Description, new BigInteger(model.Price));

            // Assuming you have a method to save the idea to your database
            var idea = new Idea
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Seller = model.Seller,
                IsAvailable = true,
                SmartContractAddress = contractAddress
            };

            SaveIdea(idea);
        }

        // Save Idea
        private async Task SaveIdea(Idea idea)
        {
            using (var context = new ApplicationDbContext(_context.GetService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Idea.Add(idea);
                await context.SaveChangesAsync();
            }
        }


        /*
        public static string GenerateBlockchainIdentity(Idea idea)
        {
            var identity = $"{idea.Id}-{idea.Name}-{idea.Brief}-{DateTime.UtcNow}";
            var bytes = Encoding.UTF8.GetBytes(identity);
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
        */
        private bool IdeaExists(int id)
        {
          return (_context.Idea?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
