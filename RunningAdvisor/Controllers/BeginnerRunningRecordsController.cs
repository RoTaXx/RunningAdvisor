using Microsoft.AspNetCore.Mvc;
using RunningAdvisor.Models.Entities;
using RunningAdvisor.ViewModels;
using System.Text;
using RunningAdvisor.Services;
using Microsoft.AspNetCore.Authorization;

namespace RunningAdvisor.Controllers
{
    [Authorize]
    public class BeginnerRunningRecordsController : Controller
    {
        private readonly BeginnerRunningService _beginnerRunningService;

        public BeginnerRunningRecordsController(BeginnerRunningService beginnerRunningService)
        {
            _beginnerRunningService = beginnerRunningService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var records = await _beginnerRunningService.GetAllRecordsAsync();
            return View(records);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(BeginnerRunningDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var record = new BeginnerRunningData
                {
                    Time3k = model.Time3k,
                    AvgHeartRate3k = model.AvgHeartRate3k,
                    Time5k = model.Time5k,
                    AvgHeartRate5k = model.AvgHeartRate5k,
                    Time10k = model.Time10k,
                    AvgHeartRate10k = model.AvgHeartRate10k
                };

                await _beginnerRunningService.AddRecordAsync(record);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var record = await _beginnerRunningService.GetRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            var model = new BeginnerRunningDataViewModel
            {
                Id = record.Id,
                Time3k = record.Time3k,
                AvgHeartRate3k = record.AvgHeartRate3k,
                Time5k = record.Time5k,
                AvgHeartRate5k = record.AvgHeartRate5k,
                Time10k = record.Time10k,
                AvgHeartRate10k = record.AvgHeartRate10k
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BeginnerRunningDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var record = new BeginnerRunningData
                {
                    Id = model.Id,
                    Time3k = model.Time3k,
                    AvgHeartRate3k = model.AvgHeartRate3k,
                    Time5k = model.Time5k,
                    AvgHeartRate5k = model.AvgHeartRate5k,
                    Time10k = model.Time10k,
                    AvgHeartRate10k = model.AvgHeartRate10k
                };

                await _beginnerRunningService.UpdateRecordAsync(record);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _beginnerRunningService.DeleteRecordAsync(id);
            return RedirectToAction(nameof(Index));
        }

       

       
        [HttpGet]
        public async Task<IActionResult> GeneratePlan(int id)
        {
            try
            {
                string plan = await _beginnerRunningService.GenerateIndividualPlanAsync(id);
                byte[] fileContents = Encoding.UTF8.GetBytes(plan);
                string fileName = $"BeginnerTrainingPlan_{id}.txt";

                return File(fileContents, "text/plain", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
