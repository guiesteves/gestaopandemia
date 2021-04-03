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
    public class VacinaController : Controller
    {
        private readonly VacinaDao _vacinaDao;
        private readonly AgentePatogenicoDao _agentePatogenicoDao;
        private readonly LaboratorioDao _laboratorioDao;
        private readonly TipoVacinaDao _tipoVacinaDao;
        private readonly IMapper _mapper;

        public VacinaController(IMapper mapper, VacinaDao vacinaDao, AgentePatogenicoDao agentePatogenicoDao,
                                LaboratorioDao laboratorioDao, TipoVacinaDao tipoVacinaDao)
        {
            _vacinaDao = vacinaDao;
            _agentePatogenicoDao = agentePatogenicoDao;
            _laboratorioDao = laboratorioDao;
            _tipoVacinaDao = tipoVacinaDao;
            _mapper = mapper;
        }

        // GET: Vacina
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<VacinaViewModel>>(await _vacinaDao.RecuperarTodosAsync()));
        }

        // GET: Vacina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _vacinaDao.RecuperarPorIdAsync(id.Value);
            if (vacina == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VacinaViewModel>(vacina));
        }

        // GET: Vacina/Create
        public async Task<IActionResult> Create()
        {
            ViewData["listaAgentePatogenico"] = await _agentePatogenicoDao.RecuperarTodosAsync();
            ViewData["listaLaboratorio"] = await _laboratorioDao.RecuperarTodosAsync();
            ViewData["listaTipoVacina"] = await _tipoVacinaDao.RecuperarTodosAsync();
            return View();
        }

        // POST: Vacina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VacinaId,Nome,LaboratorioId,TipoVacinaId,AgentePatogenicoId")] VacinaViewModel vacinaViewModel)
        {
            if (ModelState.IsValid)
            {
                using var transacao = _vacinaDao.ObterNovaTransacao();
                _vacinaDao.Incluir(_mapper.Map<Vacina>(vacinaViewModel));
                _vacinaDao.SalvarAlteracoesContexto();
                transacao.Commit();
                return RedirectToAction(nameof(Index));
            }

            ViewData["listaAgentePatogenico"] = await _agentePatogenicoDao.RecuperarTodosAsync();
            ViewData["listaLaboratorio"] = await _laboratorioDao.RecuperarTodosAsync();
            ViewData["listaTipoVacina"] = await _tipoVacinaDao.RecuperarTodosAsync();

            return View(vacinaViewModel);
        }

        // GET: Vacina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _vacinaDao.RecuperarPorIdAsync(id.Value);
            if (vacina == null)
            {
                return NotFound();
            }

            ViewData["listaAgentePatogenico"] = await _agentePatogenicoDao.RecuperarTodosAsync();
            ViewData["listaLaboratorio"] = await _laboratorioDao.RecuperarTodosAsync();
            ViewData["listaTipoVacina"] = await _tipoVacinaDao.RecuperarTodosAsync();

            return View(_mapper.Map<VacinaViewModel>(vacina));
        }

        // POST: Vacina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VacinaId,Nome,LaboratorioId,TipoVacinaId,AgentePatogenicoId")] VacinaViewModel vacinaViewModel)
        {
            if (id != vacinaViewModel.VacinaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using var transacao = _vacinaDao.ObterNovaTransacao();
                    _vacinaDao.Atualizar(_mapper.Map<Vacina>(vacinaViewModel));
                    _vacinaDao.SalvarAlteracoesContexto();
                    transacao.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacinaExists(int.Parse(vacinaViewModel.VacinaId)))
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

            ViewData["listaAgentePatogenico"] = await _agentePatogenicoDao.RecuperarTodosAsync();
            ViewData["listaLaboratorio"] = await _laboratorioDao.RecuperarTodosAsync();
            ViewData["listaTipoVacina"] = await _tipoVacinaDao.RecuperarTodosAsync();

            return View(vacinaViewModel);
        }

        // GET: Vacina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _vacinaDao.RecuperarPorIdAsync(id.Value);

            if (vacina == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VacinaViewModel>(vacina));
        }

        // POST: Vacina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using var transacao = _vacinaDao.ObterNovaTransacao();
            var vacina = await _vacinaDao.RecuperarPorIdAsync(id);
            _vacinaDao.Excluir(vacina);
            _vacinaDao.SalvarAlteracoesContexto();
            transacao.Commit();

            return RedirectToAction(nameof(Index));
        }

        private bool VacinaExists(int id)
        {
            return _vacinaDao.ExistePorId(id);
        }


        public async Task<IActionResult> ObterPlanilha()
        {
            using XLWorkbook workbook = new();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Vacina");

            int linhaCorrente = 1;
            #region Cabeçalho
            worksheet.Cell(linhaCorrente, 1).Value = "#";
            worksheet.Cell(linhaCorrente, 2).Value = "Nome";
            worksheet.Cell(linhaCorrente, 3).Value = "Nome do Laboratório";
            worksheet.Cell(linhaCorrente, 4).Value = "Tipo da Vacina";
            worksheet.Cell(linhaCorrente, 5).Value = "Agente patogênico";
            linhaCorrente++;
            #endregion

            List<Vacina> listaVacina = await _vacinaDao.RecuperarTodosAsync();

            foreach (Vacina vacina in listaVacina)
            {
                worksheet.Cell(linhaCorrente, 1).Value = vacina.VacinaId.ToString();
                worksheet.Cell(linhaCorrente, 2).Value = vacina.Nome;
                worksheet.Cell(linhaCorrente, 3).Value = vacina.Laboratorio.Nome;
                worksheet.Cell(linhaCorrente, 4).Value = vacina.TipoVacina.Descricao;
                worksheet.Cell(linhaCorrente, 5).Value = vacina.AgentePatogenico.Nome;
                linhaCorrente++;
            }

            using MemoryStream stream = new();
            workbook.SaveAs(stream);

            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Vacina.xlsx");
        }
    }
}
