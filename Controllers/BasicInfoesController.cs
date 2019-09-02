using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewMylogin.Models;
using System.Web;


namespace NewMylogin.Controllers
{
    public class BasicInfoesController : Controller
    {
        private readonly BasicInfoContext _context;
        private readonly BlogContext blgcontext;
        public BasicInfoesController(BasicInfoContext context, BlogContext bcontext)
        {
            _context = context;
            blgcontext = bcontext;
        }

        // GET: BasicInfoes
        public async Task<IActionResult> Index(string SearchString)
        {
            var users = from u in _context.BasicInfo
                        select u;
            if (!String.IsNullOrEmpty(SearchString))
            {
                users = users.Where(u => u.Alias.Contains(SearchString));
            }
            return View(await users.ToListAsync());
        }

        // GET: BasicInfoes/Details/5
        public async Task<IActionResult> Details(string Email)
        {
            var bloglist = from blg in blgcontext.Blog
                           where blg.AuthorEmail == Email
                           select blg;
            //var blog = bloglist.FirstOrDefault<Blog>();
            
            if (Email == null)
            {
                return NotFound();
            }

            var basicInfo = await _context.BasicInfo
                .FirstOrDefaultAsync(m=>m.Email==Email);
            if (basicInfo == null)
            {
                return NotFound();
            }

            ViewBag.alias = basicInfo.Alias;
            ViewBag.email = basicInfo.Email;
            ViewBag.team= basicInfo.Team;
            ViewBag.expertise= basicInfo.Expertise;
            

            return View(await bloglist.ToListAsync());
        }

        // GET: BasicInfoes/Create
        public IActionResult Create(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }

        // POST: BasicInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Alias,Email,Expertise,Team")] BasicInfo basicInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basicInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(basicInfo);
        }

        // GET: BasicInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicInfo = await _context.BasicInfo.FindAsync(id);
            if (basicInfo == null)
            {
                return NotFound();
            }
            return View(basicInfo);
        }

        // POST: BasicInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Alias,Email,Expertise,Team")] BasicInfo basicInfo)
        {
            if (id != basicInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basicInfo);
                    var blogs = from b in blgcontext.Blog
                                where b.AuthorEmail == basicInfo.Email
                                select b;
                    foreach(var blog in blogs)
                    {
                        blog.AuthorAlias = basicInfo.Alias;
                    }
                    await _context.SaveChangesAsync();
                    await blgcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasicInfoExists(basicInfo.Id))
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
            return View(basicInfo);
        }

        // GET: BasicInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicInfo = await _context.BasicInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basicInfo == null)
            {
                return NotFound();
            }

            return View(basicInfo);
        }

        // POST: BasicInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basicInfo = await _context.BasicInfo.FindAsync(id);
            _context.BasicInfo.Remove(basicInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasicInfoExists(int id)
        {
            return _context.BasicInfo.Any(e => e.Id == id);
        }
    }
}
