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
    [Authorize(Roles = "ADMIN, GESTOR_PATOGENO")]
    public class TipoAgentePatogenicoController : Controller
    {
        private readonly TipoAgentePatogenicoDao _tipoAgentePatogenicoDao;
        private readonly IMapper _mapper;

        public TipoAgentePatogenicoController(IMapper mapper, TipoAgentePatogenicoDao tipoAgentePatogenicoDao)
        {
            _tipoAgentePatogenicoDao = tipoAgentePatogenicoDao;
            _mapper = mapper;
        }

        // GET: TipoAgentePatogenico
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<TipoAgentePatogenicoViewModel>>(await _tipoAgentePatogenicoDao.RecuperarTodosAsync()));
        }

        // GET: TipoAgentePatogenico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAgentePatogenico = await _tipoAgentePatogenicoDao.RecuperarPorIdAsync(id.Value);
            if (tipoAgentePatogenico == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TipoAgentePatogenicoViewModel>(tipoAgentePatogenico));
        }

        // GET: TipoAgentePatogenico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoAgentePatogenico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TipoAgentePatogenicoId,Nome")] TipoAgentePatogenicoViewModel tipoAgentePatogenicoViewModel)
        {
            if (ModelState.IsValid)
            {
                _tipoAgentePatogenicoDao.Incluir(_mapper.Map<TipoAgentePatogenico>(tipoAgentePatogenicoViewModel));
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAgentePatogenicoViewModel);
        }

        // GET: TipoAgentePatogenico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAgentePatogenico = await _tipoAgentePatogenicoDao.RecuperarPorIdAsync(id.Value);
            if (tipoAgentePatogenico == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<TipoAgentePatogenicoViewModel>(tipoAgentePatogenico));
        }

        // POST: TipoAgentePatogenico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("TipoAgentePatogenicoId,Nome")] TipoAgentePatogenicoViewModel tipoAgentePatogenicoViewModel)
        {
            if (id != tipoAgentePatogenicoViewModel.TipoAgentePatogenicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tipoAgentePatogenicoDao.Atualizar(_mapper.Map<TipoAgentePatogenico>(tipoAgentePatogenicoViewModel));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAgentePatogenicoExists(int.Parse(tipoAgentePatogenicoViewModel.TipoAgentePatogenicoId)))
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
            return View(tipoAgentePatogenicoViewModel);
        }

        // GET: TipoAgentePatogenico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAgentePatogenico = await _tipoAgentePatogenicoDao.RecuperarPorIdAsync(id.Value);
            if (tipoAgentePatogenico == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TipoAgentePatogenicoViewModel>(tipoAgentePatogenico));
        }

        // POST: TipoAgentePatogenico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoAgentePatogenico = await _tipoAgentePatogenicoDao.RecuperarPorIdAsync(id);
            _tipoAgentePatogenicoDao.Excluir(tipoAgentePatogenico);
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAgentePatogenicoExists(int id)
        {
            return _tipoAgentePatogenicoDao.ExistePorId(id);
        }

        public async Task<IActionResult> ObterPlanilha()
        {
            using XLWorkbook workbook = new();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Tipo Agente Patogênico");

            int linhaCorrente = 1;
            #region Cabeçalho
            worksheet.Cell(linhaCorrente, 1).Value = "Id Tipo Agente Patogênico";
            worksheet.Cell(linhaCorrente, 2).Value = "Nome";
            linhaCorrente++;
            #endregion

            List<TipoAgentePatogenico> listaTipoAgentePatogenico = await _tipoAgentePatogenicoDao.RecuperarTodosAsync();

            foreach (TipoAgentePatogenico tipoAgentePatogenico in listaTipoAgentePatogenico)
            {
                worksheet.Cell(linhaCorrente, 1).Value = tipoAgentePatogenico.TipoAgentePatogenicoId.ToString();
                worksheet.Cell(linhaCorrente, 2).Value = tipoAgentePatogenico.Nome;
                linhaCorrente++;
            }

            using MemoryStream stream = new();
            workbook.SaveAs(stream);

            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TipoAgentePatogenico.xlsx");
        }
    }
}

