﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAT.Data.EF.Models;

namespace SAT.UI.MVC.Controllers
{
    public class ScheduledClassesController : Controller
    {
        private readonly SatContext _context;

        public ScheduledClassesController(SatContext context)
        {
            _context = context;
        }

        // GET: ScheduledClasses
        public async Task<IActionResult> Index()
        {
            var satContext = _context.ScheduledClasses.Include(s => s.Course).Include(s => s.Scs);
            return View(await satContext.ToListAsync());
        }

        // GET: ScheduledClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledClass = await _context.ScheduledClasses
                .Include(s => s.Course)
                .Include(s => s.Scs)
                .FirstOrDefaultAsync(m => m.ScheduledClassId == id);
            if (scheduledClass == null)
            {
                return NotFound();
            }

            return View(scheduledClass);
        }

        // GET: ScheduledClasses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseDescription");
            ViewData["Scsid"] = new SelectList(_context.ScheduledClassStatuses, "Scsid", "Scname");
            return View();
        }

        // POST: ScheduledClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduledClassId,CourseId,StartDate,EndDate,InstructorName,Location,Scsid")] ScheduledClass scheduledClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduledClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseDescription", scheduledClass.CourseId);
            ViewData["Scsid"] = new SelectList(_context.ScheduledClassStatuses, "Scsid", "Scname", scheduledClass.Scsid);
            return View(scheduledClass);
        }

        // GET: ScheduledClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledClass = await _context.ScheduledClasses.FindAsync(id);
            if (scheduledClass == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseDescription", scheduledClass.CourseId);
            ViewData["Scsid"] = new SelectList(_context.ScheduledClassStatuses, "Scsid", "Scname", scheduledClass.Scsid);
            return View(scheduledClass);
        }

        // POST: ScheduledClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduledClassId,CourseId,StartDate,EndDate,InstructorName,Location,Scsid")] ScheduledClass scheduledClass)
        {
            if (id != scheduledClass.ScheduledClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduledClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduledClassExists(scheduledClass.ScheduledClassId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseDescription", scheduledClass.CourseId);
            ViewData["Scsid"] = new SelectList(_context.ScheduledClassStatuses, "Scsid", "Scname", scheduledClass.Scsid);
            return View(scheduledClass);
        }

        // GET: ScheduledClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledClass = await _context.ScheduledClasses
                .Include(s => s.Course)
                .Include(s => s.Scs)
                .FirstOrDefaultAsync(m => m.ScheduledClassId == id);
            if (scheduledClass == null)
            {
                return NotFound();
            }

            return View(scheduledClass);
        }

        // POST: ScheduledClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduledClass = await _context.ScheduledClasses.FindAsync(id);
            if (scheduledClass != null)
            {
                _context.ScheduledClasses.Remove(scheduledClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduledClassExists(int id)
        {
            return _context.ScheduledClasses.Any(e => e.ScheduledClassId == id);
        }
    }
}
