using Clinica.Models;
using Clinica.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinica.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public ConsultaController(IConsultaRepository consultaRepository, IPacienteRepository pacienteRepository)
        {
            _consultaRepository = consultaRepository;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IActionResult> Index()
        {
            var consultas = await _consultaRepository.GetAllAsync();
            return View(consultas);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Pacientes = new MultiSelectList(await _pacienteRepository.GetAllAsync(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consulta consulta, List<int> pacientesIds)
        {
            if (ModelState.IsValid)
            {
                await _consultaRepository.AddAsync(consulta, pacientesIds);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Pacientes = new MultiSelectList(await _pacienteRepository.GetAllAsync(), "Id", "Nome", pacientesIds);
            return View(consulta);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var consulta = await _consultaRepository.GetByIdAsync(id);
            if (consulta == null) return NotFound();

            ViewBag.Pacientes = new MultiSelectList(
                await _pacienteRepository.GetAllAsync(),
                "Id",
                "Nome",
                consulta.PacienteConsultas.Select(pc => pc.PacienteId)
            );

            return View(consulta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Consulta consulta, List<int> pacientesIds)
        {
            if (ModelState.IsValid)
            {
                await _consultaRepository.UpdateAsync(consulta, pacientesIds);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Pacientes = new MultiSelectList(await _pacienteRepository.GetAllAsync(), "Id", "Nome", pacientesIds);
            return View(consulta);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var consulta = await _consultaRepository.GetByIdAsync(id);
            if (consulta == null) return NotFound();
            return View(consulta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _consultaRepository.GetByIdAsync(id);
            if (consulta != null)
            {
                await _consultaRepository.DeleteAsync(consulta);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

