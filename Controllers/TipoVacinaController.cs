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
    [Authorize(Roles = "ADMIN, GESTOR_VACINA")]
    public class TipoVacinaController : Controller
    {
        private readonly TipoVacinaDao _tipoVacinaDao;
        private readonly VacinaDao _vacinaDao;
        private readonly IMapper _mapper;

        public TipoVacinaController(IMapper mapper, TipoVacinaDao tipoVacinaDao,
                                    VacinaDao vacinaDao)
        {
            _tipoVacinaDao = tipoVacinaDao;
            _vacinaDao = vacinaDao;
            _mapper = mapper;
        }

        // GET: TipoVacina
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<TipoVacinaViewModel>>(await _tipoVacinaDao.RecuperarTodosAsync()));
        }

        // GET: TipoVacina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVacina = await _tipoVacinaDao.RecuperarPorIdAsync(id.Value);

            if (tipoVacina == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TipoVacinaViewModel>(tipoVacina));
        }

        // GET: TipoVacina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoVacina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TipoVacinaId,Nome,Descricao")] TipoVacinaViewModel tipoVacinaViewModel)
        {
            if (ModelState.IsValid)
            {
                _tipoVacinaDao.Incluir(_mapper.Map<TipoVacina>(tipoVacinaViewModel));
                return RedirectToAction(nameof(Index));
            }
            return View(tipoVacinaViewModel);
        }

        // GET: TipoVacina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVacina = await _tipoVacinaDao.RecuperarPorIdAsync(id.Value);
            if (tipoVacina == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<TipoVacinaViewModel>(tipoVacina));
        }

        // POST: TipoVacina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("TipoVacinaId,Nome,Descricao")] TipoVacinaViewModel tipoVacinaViewModel)
        {
            if (id != tipoVacinaViewModel.TipoVacinaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tipoVacinaDao.Atualizar(_mapper.Map<TipoVacina>(tipoVacinaViewModel));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoVacinaExists(int.Parse(tipoVacinaViewModel.TipoVacinaId)))
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
            return View(tipoVacinaViewModel);
        }

        // GET: TipoVacina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVacina = await _tipoVacinaDao.RecuperarPorIdAsync(id.Value);

            if (tipoVacina == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TipoVacinaViewModel>(tipoVacina));
        }

        // POST: TipoVacina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoVacina = await _tipoVacinaDao.RecuperarPorIdAsync(id);
            
            if (_vacinaDao.ExistePorTipoVacinaId(tipoVacina.TipoVacinaId))
            {
                ModelState.AddModelError(string.Empty, "Não é possivel excluir pois existe vacina vinculado a este tipo de vacina");
                return View(_mapper.Map<TipoVacinaViewModel>(tipoVacina));
            }

            _tipoVacinaDao.Excluir(tipoVacina);
            return RedirectToAction(nameof(Index));
        }

        private bool TipoVacinaExists(int id)
        {
            return _tipoVacinaDao.ExistePorId(id);
        }

        public async Task<IActionResult> ObterPlanilha()
        {
            using XLWorkbook workbook = new();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Tipo Vacina");

            int linhaCorrente = 1;
            #region Cabeçalho
            worksheet.Cell(linhaCorrente, 1).Value = "Id Tipo Vacina";
            worksheet.Cell(linhaCorrente, 2).Value = "Nome";
            worksheet.Cell(linhaCorrente, 3).Value = "Descrição";
            linhaCorrente++;
            #endregion

            List<TipoVacina> listatipoVacina = await _tipoVacinaDao.RecuperarTodosAsync();

            foreach (TipoVacina tipoVacina in listatipoVacina)
            {
                worksheet.Cell(linhaCorrente, 1).Value = tipoVacina.TipoVacinaId.ToString();
                worksheet.Cell(linhaCorrente, 2).Value = tipoVacina.Nome;
                worksheet.Cell(linhaCorrente, 3).Value = tipoVacina.Descricao;
                linhaCorrente++;
            }

            using MemoryStream stream = new();
            workbook.SaveAs(stream);

            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TipoVacina.xlsx");
        }
    }
}
