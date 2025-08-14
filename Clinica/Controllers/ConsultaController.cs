using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Clinica.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IConsultaRepository _consultaRepo;
        private readonly IPacienteRepository _pacienteRepo;

        public ConsultaController(IConsultaRepository consultaRepo, IPacienteRepository pacienteRepo)
        {
            _consultaRepo = consultaRepo;
            _pacienteRepo = pacienteRepo;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            var consultas = await _consultaRepo.GetAllAsync(); // espera-se que o repo retorne consultas com Paciente carregado quando necessário
            return View(consultas);
        }

        // GET: Consulta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _consultaRepo.GetByIdAsync(id.Value);
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // GET: Consulta/Create
        public async Task<IActionResult> Create()
        {
            await FillPacientesSelectListAsync();
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                await FillPacientesSelectListAsync(consulta.PacienteId);
                return View(consulta);
            }

            await _consultaRepo.AddAsync(consulta);
            return RedirectToAction(nameof(Index));
        }

        // GET: Consulta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _consultaRepo.GetByIdAsync(id.Value);
            if (consulta == null) return NotFound();

            await FillPacientesSelectListAsync(consulta.PacienteId);
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Consulta consulta)
        {
            if (id != consulta.ConsultaId) return NotFound();

            if (!ModelState.IsValid)
            {
                await FillPacientesSelectListAsync(consulta.PacienteId);
                return View(consulta);
            }

            await _consultaRepo.UpdateAsync(consulta);
            return RedirectToAction(nameof(Index));
        }

        // GET: Consulta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _consultaRepo.GetByIdAsync(id.Value);
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: Consulta/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _consultaRepo.GetByIdAsync(id);
            if (consulta == null) return NotFound();

            await _consultaRepo.DeleteAsync(consulta);
            return RedirectToAction(nameof(Index));
        }

        // Helper para popular o select de pacientes
        private async Task FillPacientesSelectListAsync(int? selectedPacienteId = null)
        {
            var pacientes = await _pacienteRepo.GetAllAsync();
            ViewBag.Pacientes = new SelectList(pacientes, "PacienteId", "Nome", selectedPacienteId);
        }
    }
}

