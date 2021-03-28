using AutoMapper;
using ClosedXML.Excel;
using CVC19.Data.Dao;
using CVC19.Models;
using CVC19.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CVC19.Controllers
{
    [Authorize]
    public class LaboratorioController : Controller
    {
        private readonly LaboratorioDao _laboratorioDao;
        private readonly PaisDao _paisDao;
        private readonly IMapper _mapper;

        public LaboratorioController(IMapper mapper, LaboratorioDao laboratorioDao, PaisDao paisDao)
        {
            _laboratorioDao = laboratorioDao;
            _paisDao = paisDao;
            _mapper = mapper;
        }

        // GET: Laboratorio
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<LaboratorioViewModel>>(await _laboratorioDao.RecuperarTodosAsync()));
        }

        // GET: Laboratorio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorio = await _laboratorioDao.RecuperarPorIdAsync(id.Value);
            if (laboratorio == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<LaboratorioViewModel>(laboratorio));
        }

        // GET: Laboratorio/Create
        public async Task<IActionResult> Create()
        {
            ViewData["listaPais"] = await _paisDao.RecuperarTodosAsync();
            return View();
        }

        // POST: Laboratorio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LaboratorioId,Nome,PaisId")] LaboratorioViewModel laboratorioViewModel)
        {
            if (ModelState.IsValid)
            {
                _laboratorioDao.Incluir(_mapper.Map<Laboratorio>(laboratorioViewModel));
                return RedirectToAction(nameof(Index));
            }
            ViewData["listaPais"] = await _paisDao.RecuperarTodosAsync();
            return View(laboratorioViewModel);
        }

        // GET: Laboratorio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorio = await _laboratorioDao.RecuperarPorIdAsync(id.Value);
            if (laboratorio == null)
            {
                return NotFound();
            }
            ViewData["listaPais"] = await _paisDao.RecuperarTodosAsync();
            return View(_mapper.Map<LaboratorioViewModel>(laboratorio));
        }

        // POST: Laboratorio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LaboratorioId,Nome,PaisId")] LaboratorioViewModel laboratorioViewModel)
        {
            if (id != laboratorioViewModel.LaboratorioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _laboratorioDao.Atualizar(_mapper.Map<Laboratorio>(laboratorioViewModel));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaboratorioExists(int.Parse(laboratorioViewModel.LaboratorioId)))
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
            ViewData["listaPais"] = await _paisDao.RecuperarTodosAsync();
            return View(laboratorioViewModel);
        }

        // GET: Laboratorio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorio = await _laboratorioDao.RecuperarPorIdAsync(id.Value);
            if (laboratorio == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<LaboratorioViewModel>(laboratorio));
        }

        // POST: Laboratorio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laboratorio = await _laboratorioDao.RecuperarPorIdAsync(id);
            _laboratorioDao.Excluir(laboratorio);
            return RedirectToAction(nameof(Index));
        }

        private bool LaboratorioExists(int id)
        {
            return _laboratorioDao.ExistePorId(id);
        }


        public async Task<IActionResult> ObterPlanilha()
        {
            using XLWorkbook workbook = new();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Laboratório");

            int linhaCorrente = 1;
            #region Cabeçalho
            worksheet.Cell(linhaCorrente, 1).Value = "Id Laboratório";
            worksheet.Cell(linhaCorrente, 2).Value = "Nome";
            worksheet.Cell(linhaCorrente, 3).Value = "País";
            linhaCorrente++;
            #endregion

            List<Laboratorio> listaLaboratorio = await _laboratorioDao.RecuperarTodosAsync();

            foreach (Laboratorio laboratorio in listaLaboratorio)
            {
                worksheet.Cell(linhaCorrente, 1).Value = laboratorio.LaboratorioId.ToString();
                worksheet.Cell(linhaCorrente, 2).Value = laboratorio.Nome;
                worksheet.Cell(linhaCorrente, 3).Value = laboratorio.Pais.Nome;
                linhaCorrente++;
            }

            using MemoryStream stream = new();
            workbook.SaveAs(stream);

            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Laboratorio.xlsx");
        }
    }
}

