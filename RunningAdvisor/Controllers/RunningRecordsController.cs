using Microsoft.AspNetCore.Mvc;
using RunningAdvisor.Models.Entities;
using RunningAdvisor.Services;
using System.Text;
using RunningAdvisor.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RunningAdvisor.Controllers
{
    [Authorize]
    public class RunningRecordsController : Controller
    {
        private readonly RunningService _runningService;

        public RunningRecordsController(RunningService runningService)
        {
            _runningService = runningService;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var records = await _runningService.GetAllRecordsAsync();
            return View(records);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(RunningDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var record = new RunningData
                {
                    Time10k = model.Time10k,
                    AvgHeartRate10k = model.AvgHeartRate10k,
                    Time21k = model.Time21k,
                    AvgHeartRate21k = model.AvgHeartRate21k,
                    Time42k = model.Time42k,
                    AvgHeartRate42k = model.AvgHeartRate42k,
                    Cadence = model.Cadence
                };

                await _runningService.AddRecordAsync(record);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var record = await _runningService.GetRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            var model = new RunningDataViewModel
            {
                Id = record.Id,
                Time10k = record.Time10k,
                AvgHeartRate10k = record.AvgHeartRate10k,
                Time21k = record.Time21k,
                AvgHeartRate21k = record.AvgHeartRate21k,
                Time42k = record.Time42k,
                AvgHeartRate42k = record.AvgHeartRate42k,
                Cadence = record.Cadence
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RunningDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var record = new RunningData
                {
                    Id = model.Id,
                    Time10k = model.Time10k,
                    AvgHeartRate10k = model.AvgHeartRate10k,
                    Time21k = model.Time21k,
                    AvgHeartRate21k = model.AvgHeartRate21k,
                    Time42k = model.Time42k,
                    AvgHeartRate42k = model.AvgHeartRate42k,
                    Cadence = model.Cadence
                };

                await _runningService.UpdateRecordAsync(record);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _runningService.DeleteRecordAsync(id);
            return RedirectToAction(nameof(Index));
        }


       
        [HttpGet]
        public async Task<IActionResult> GeneratePlan(int id)
        {
            try
            {
                string plan = await _runningService.GenerateIndividualPlanAsync(id);
                byte[] fileContents = Encoding.UTF8.GetBytes(plan);
                string fileName = $"TrainingPlan_{id}.txt";

                return File(fileContents, "text/plain", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
