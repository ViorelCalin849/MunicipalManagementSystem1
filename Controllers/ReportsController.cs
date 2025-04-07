using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalManagementSystem.Data;
using MunicipalManagementSystem.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly MunicipalDbContext _context;

        public ReportsController(MunicipalDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Reports
                .Include(r => r.Citizen)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var report = await _context.Reports
                .Include(r => r.Citizen)
                .FirstOrDefaultAsync(m => m.ReportID == id);

            return report == null ? NotFound() : View(report);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Citizens = await _context.Citizens.OrderBy(c => c.CitizenID).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Report report)
        {
            Debug.WriteLine($"Received CitizenID: {report.CitizenID}");
            Debug.WriteLine($"Received ReportType: {report.ReportType}");
            Debug.WriteLine($"Received Details: {report.Details}");

            try
            {
                var validationErrors = new List<string>();

                if (report.CitizenID <= 0)
                {
                    validationErrors.Add("Please select a valid Citizen");
                    ModelState.AddModelError("CitizenID", "Invalid Citizen");
                }

                if (string.IsNullOrWhiteSpace(report.ReportType))
                {
                    validationErrors.Add("Report Type is required");
                    ModelState.AddModelError("ReportType", "Required");
                }

                if (string.IsNullOrWhiteSpace(report.Details))
                {
                    validationErrors.Add("Details are required");
                    ModelState.AddModelError("Details", "Required");
                }

                if (validationErrors.Any())
                {
                    TempData["Error"] = string.Join("<br>", validationErrors);
                }
                else
                {
                    report.SubmissionDate = DateTime.Now;
                    report.Status = "Under Review";
                    _context.Add(report);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Report created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Database error: " + ex.InnerException?.Message ?? ex.Message);
                TempData["Error"] = "Failed to save to database. Please try again.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                TempData["Error"] = "An unexpected error occurred.";
            }

            ViewBag.Citizens = await _context.Citizens.OrderBy(c => c.CitizenID).ToListAsync();
            return View(report);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var report = await _context.Reports.FindAsync(id);
            if (report == null) return NotFound();

            ViewBag.Citizens = await _context.Citizens.OrderBy(c => c.CitizenID).ToListAsync();
            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Report report)
        {
            if (id != report.ReportID) return NotFound();

            try
            {
                // Verify Citizen exists before updating
                var citizenExists = await _context.Citizens
                    .AnyAsync(c => c.CitizenID == report.CitizenID);

                if (!citizenExists)
                {
                    ModelState.AddModelError("CitizenID", "Selected citizen does not exist");
                    ViewBag.Citizens = await _context.Citizens.ToListAsync();
                    return View(report);
                }

                // Ensure original submission date is preserved
                var originalReport = await _context.Reports
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.ReportID == id);

                report.SubmissionDate = originalReport.SubmissionDate;

                _context.Update(report);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Report updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ReportExists(report.ReportID))
                    return NotFound();

                ModelState.AddModelError("", "Concurrency error: " + ex.Message);
                TempData["Error"] = "The record was modified by another user. Please refresh and try again.";
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Database error: " + ex.InnerException?.Message);
                TempData["Error"] = "Failed to save changes. Please check your input.";
            }

            ViewBag.Citizens = await _context.Citizens.ToListAsync();
            return View(report);
        }
   
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reportRequest = await _context.Reports
                .Include(r => r.Citizen)
                .FirstOrDefaultAsync(m => m.ReportID == id);

            return reportRequest == null ? NotFound() : View(reportRequest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportRequest = await _context.Reports.FindAsync(id);
            if (reportRequest == null) return RedirectToAction(nameof(Index));

            _context.Reports.Remove(reportRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportID == id);
        }
    }
}