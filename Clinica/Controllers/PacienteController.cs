
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinica.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _pacienteRepo;

        public PacienteController(IPacienteRepository pacienteRepo)
        {
            _pacienteRepo = pacienteRepo;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
            var pacientes = await _pacienteRepo.GetAllAsync();
            return View(pacientes);
        }

        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var paciente = await _pacienteRepo.GetByIdAsync(id.Value);
            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // GET: Paciente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (!ModelState.IsValid) return View(paciente);

            await _pacienteRepo.AddAsync(paciente);
            return RedirectToAction(nameof(Index));
        }

        // GET: Paciente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var paciente = await _pacienteRepo.GetByIdAsync(id.Value);
            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // POST: Paciente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Paciente paciente)
        {
            if (id != paciente.PacienteId) return NotFound();

            if (!ModelState.IsValid) return View(paciente);

            await _pacienteRepo.UpdateAsync(paciente);
            return RedirectToAction(nameof(Index));
        }

        // GET: Paciente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var paciente = await _pacienteRepo.GetByIdAsync(id.Value);
            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // POST: Paciente/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _pacienteRepo.GetByIdAsync(id);
            if (paciente == null) return NotFound();

            await _pacienteRepo.DeleteAsync(paciente);
            return RedirectToAction(nameof(Index));
        }
    }
}

