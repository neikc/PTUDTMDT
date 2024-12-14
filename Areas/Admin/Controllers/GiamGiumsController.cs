using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTUDTMDT.Models;

namespace PTUDTMDT.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin/Voucher/[action]")]
    [Area("Admin")]
    public class GiamGiumsController : Controller
    {
        private readonly PtudtmdtContext _context;

        public GiamGiumsController(PtudtmdtContext context)
        {
            _context = context;
        }

        // GET: Admin/GiamGiums
        public async Task<IActionResult> Index()
        {
            return View(await _context.GiamGia.ToListAsync());
        }

        // GET: Admin/GiamGiums/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamGium = await _context.GiamGia
                .FirstOrDefaultAsync(m => m.MaGiamGia == id);
            if (giamGium == null)
            {
                return NotFound();
            }

            return View(giamGium);
        }

        // GET: Admin/GiamGiums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GiamGiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGiamGia,TenMa,GiaTri,NgayBatDau,NgayKetThuc,SoLuong,TrangThai")] GiamGium giamGium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giamGium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giamGium);
        }

        // GET: Admin/GiamGiums/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamGium = await _context.GiamGia.FindAsync(id);
            if (giamGium == null)
            {
                return NotFound();
            }
            return View(giamGium);
        }

        // POST: Admin/GiamGiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaGiamGia,TenMa,GiaTri,NgayBatDau,NgayKetThuc,SoLuong,TrangThai")] GiamGium giamGium)
        {
            if (id != giamGium.MaGiamGia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giamGium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiamGiumExists(giamGium.MaGiamGia))
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
            return View(giamGium);
        }

        // GET: Admin/GiamGiums/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamGium = await _context.GiamGia
                .FirstOrDefaultAsync(m => m.MaGiamGia == id);
            if (giamGium == null)
            {
                return NotFound();
            }

            return View(giamGium);
        }

        // POST: Admin/GiamGiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var giamGium = await _context.GiamGia.FindAsync(id);
            if (giamGium != null)
            {
                _context.GiamGia.Remove(giamGium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiamGiumExists(string id)
        {
            return _context.GiamGia.Any(e => e.MaGiamGia == id);
        }
    }
}
