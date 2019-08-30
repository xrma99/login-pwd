using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewMylogin.Models;

namespace NewMylogin.Controllers
{
    public class BasicInfoesController : Controller
    {
        private readonly BasicInfoContext _context;

        public BasicInfoesController(BasicInfoContext context)
        {
            _context = context;
        }

        // GET: BasicInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BasicInfo.ToListAsync());
        }

        // GET: BasicInfoes/Details/5
        public async Task<IActionResult> Details(string Email)
        {
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

            return View(basicInfo);
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
                    await _context.SaveChangesAsync();
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
