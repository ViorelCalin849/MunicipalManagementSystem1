using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalManagementSystem.Data;
using MunicipalManagementSystem.Models;

namespace MunicipalManagementSystem.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly MunicipalDbContext _context;

        public ServiceRequestsController(MunicipalDbContext context)
        {
            _context = context;
        }

        // GET: ServiceRequests
        public async Task<IActionResult> Index()
        {
            var requests = await _context.ServiceRequests
                .Include(r => r.Citizen)
                .Include(r => r.Staff)
                .ToListAsync();
            return View(requests);
        }

        // GET: ServiceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest = await _context.ServiceRequests
                .Include(r => r.Citizen)
                .Include(r => r.Staff)
                .FirstOrDefaultAsync(m => m.RequestID == id);

            return serviceRequest == null ? NotFound() : View(serviceRequest);
        }

        // GET: ServiceRequests/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest serviceRequest)
        {
            // Manually validate referenced IDs
            var citizenExists = await _context.Citizens.AnyAsync(c => c.CitizenID == serviceRequest.CitizenID);
            var staffExists = await _context.Staff.AnyAsync(s => s.StaffID == serviceRequest.StaffID);

            if (!citizenExists)
                ModelState.AddModelError("CitizenID", "Selected citizen does not exist.");
            if (!staffExists)
                ModelState.AddModelError("StaffID", "Selected staff member does not exist.");

            if (ModelState.IsValid)
            {
                serviceRequest.RequestDate = DateTime.Now;
                _context.Add(serviceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdowns if validation fails
            ViewBag.Citizens = await _context.Citizens.ToListAsync();
            ViewBag.Staff = await _context.Staff.ToListAsync();
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest == null) return NotFound();

            await PopulateDropdowns();
            return View(serviceRequest);
        }

        // POST: ServiceRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.RequestID) return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    // Validate existence of related entities
                    if (!await _context.Citizens.AnyAsync(c => c.CitizenID == serviceRequest.CitizenID))
                    {
                        ModelState.AddModelError("CitizenID", "Selected citizen does not exist");
                    }
                    else if (!await _context.Staff.AnyAsync(s => s.StaffID == serviceRequest.StaffID))
                    {
                        ModelState.AddModelError("StaffID", "Selected staff member does not exist");
                    }
                    else
                    {
                        _context.Update(serviceRequest);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(serviceRequest.RequestID))
                    return NotFound();
                throw;
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "contact your system administrator.");
            }

            await PopulateDropdowns();
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _context.ServiceRequests
                .Include(r => r.Citizen)
                .Include(r => r.Staff)
                .FirstOrDefaultAsync(m => m.RequestID == id);

            return serviceRequest == null ? NotFound() : View(serviceRequest);
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest == null) return RedirectToAction(nameof(Index));

            try
            {
                _context.ServiceRequests.Remove(serviceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to delete. " +
                    "Try again, and if the problem persists, " +
                    "contact your system administrator.");
                return View("Delete", serviceRequest);
            }
        }

        private bool ServiceRequestExists(int id)
        {
            return _context.ServiceRequests.Any(e => e.RequestID == id);
        }

        private async Task PopulateDropdowns()
        {
            ViewBag.Citizens = await _context.Citizens.ToListAsync();
            ViewBag.Staff = await _context.Staff.ToListAsync();
        }
    }
}