using Clinica.Models;
using Clinica.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return View(pacientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                await _pacienteRepository.AddAsync(paciente);
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                await _pacienteRepository.UpdateAsync(paciente);
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente != null)
            {
                await _pacienteRepository.DeleteAsync(paciente);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

