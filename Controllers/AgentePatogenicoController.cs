using AutoMapper;
using ClosedXML.Excel;
using CVC19.Data;
using CVC19.Data.Dao;
using CVC19.Models;
using CVC19.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CVC19.Controllers
{
    [Authorize(Roles = "ADMIN, GESTOR_PATOGENO")]
    public class AgentePatogenicoController : Controller
    {
        private readonly AgentePatogenicoDao _agentePatogenicoDao;
        private readonly VarianteAgentePatogenicoDao _varianteAgentePatogenicoDao;
        private readonly TipoAgentePatogenicoDao _tipoAgentePatogenicoDao;
        private readonly PaisDao _paisDao;
        private readonly IMapper _mapper;

        public AgentePatogenicoController(IMapper mapper, AgentePatogenicoDao agentePatogenicoDao,
                                          TipoAgentePatogenicoDao tipoAgentePatogenicoDao, PaisDao paisDao,
                                          VarianteAgentePatogenicoDao varianteAgentePatogenicoDao)
        {
            _agentePatogenicoDao = agentePatogenicoDao;
            _tipoAgentePatogenicoDao = tipoAgentePatogenicoDao;
            _paisDao = paisDao;
            _varianteAgentePatogenicoDao = varianteAgentePatogenicoDao;
            _mapper = mapper;
        }

        // GET: AgentePatogenico
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<AgentePatogenicoViewModel>>(await _agentePatogenicoDao.RecuperarTodosAsync()));
        }

        // GET: AgentePatogenico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentePatogenico = await _agentePatogenicoDao.RecuperarPorIdAsync(id.Value);

            if (agentePatogenico == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AgentePatogenicoViewModel>(agentePatogenico));
        }

        // GET: AgentePatogenico/Create
        public async Task<IActionResult> Create()
        {
            ViewData["listaTipoAgentePatogenico"] = await _tipoAgentePatogenicoDao.RecuperarTodosAsync();
            ViewData["ListaPais"] = await _paisDao.RecuperarTodosAsync();
            ViewData["ExibirBotoesGrid"] = true;
            return View();
        }

        // POST: AgentePatogenico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AgentePatogenicoViewModel agentePatogenicoViewModel)
        {
            if (ModelState.IsValid)
            {
                using var transaction = _agentePatogenicoDao.ObterNovaTransacao();

                _agentePatogenicoDao.Incluir(_mapper.Map<AgentePatogenico>(agentePatogenicoViewModel));
              
                transaction.Commit();

                return RedirectToAction(nameof(Index));
            }

            ViewData["listaTipoAgentePatogenico"] = await _tipoAgentePatogenicoDao.RecuperarTodosAsync();
            ViewData["ListaPais"] = await _paisDao.RecuperarTodosAsync();
            ViewData["ExibirBotoesGrid"] = true;

            return View(agentePatogenicoViewModel);
        }

        // GET: AgentePatogenico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentePatogenico = await _agentePatogenicoDao.RecuperarPorIdAsync(id.Value);
            if (agentePatogenico == null)
            {
                return NotFound();
            }

            ViewData["listaTipoAgentePatogenico"] = await _tipoAgentePatogenicoDao.RecuperarTodosAsync();
            ViewData["ListaPais"] = await _paisDao.RecuperarTodosAsync();
            ViewData["ExibirBotoesGrid"] = true;

            return View(_mapper.Map<AgentePatogenicoViewModel>(agentePatogenico));
        }

        // POST: AgentePatogenico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] AgentePatogenicoViewModel agentePatogenicoViewModel)
        {
            if (id != agentePatogenicoViewModel.AgentePatogenicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    using var transaction = _varianteAgentePatogenicoDao.ObterNovaTransacao();

                    //Exclui Varinates foram excluidas na tela
                    _varianteAgentePatogenicoDao.ExcluirPorAgentePatogenicoNaoExistentesLista(int.Parse(agentePatogenicoViewModel.AgentePatogenicoId),
                                                                                              agentePatogenicoViewModel.ListaVarianteAgentePatogenicoViewModel
                                                                                              .Where(v => !string.IsNullOrWhiteSpace(v.VarianteAgentePatogenicoId))
                                                                                              .Select(i => int.Parse(i.VarianteAgentePatogenicoId)).ToList());

                    _agentePatogenicoDao.Atualizar(_mapper.Map<AgentePatogenico>(agentePatogenicoViewModel));
               
                    transaction.Commit();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentePatogenicoExists(int.Parse(agentePatogenicoViewModel.AgentePatogenicoId)))
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

            ViewData["listaTipoAgentePatogenico"] = await _tipoAgentePatogenicoDao.RecuperarTodosAsync();
            ViewData["ListaPais"] = await _paisDao.RecuperarTodosAsync();
            ViewData["ExibirBotoesGrid"] = true;

            return View(agentePatogenicoViewModel);
        }

        // GET: AgentePatogenico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentePatogenico = await _agentePatogenicoDao.RecuperarPorIdAsync(id.Value);

            if (agentePatogenico == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AgentePatogenicoViewModel>(agentePatogenico));
        }

        // POST: AgentePatogenico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentePatogenico = await _agentePatogenicoDao.RecuperarPorIdAsync(id);

            using var transaction = _agentePatogenicoDao.ObterNovaTransacao();

            _agentePatogenicoDao.Excluir(agentePatogenico);

            transaction.Commit();

            return RedirectToAction(nameof(Index));
        }

        private bool AgentePatogenicoExists(int id)
        {
            return _agentePatogenicoDao.ExistePorId(id);
        }

        public async Task<IActionResult> ObterPlanilha()
        {
            using XLWorkbook workbook = new();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Agente Patogênico");

            int linhaCorrente = 1;
            #region Cabeçalho
            worksheet.Cell(linhaCorrente, 1).Value = "#";
            worksheet.Cell(linhaCorrente, 2).Value = "Nome";
            worksheet.Cell(linhaCorrente, 3).Value = "Tipo de Agente Patogênico";
            linhaCorrente++;
            #endregion

            List<AgentePatogenico> listaAgentePatogenico = await _agentePatogenicoDao.RecuperarTodosAsync();

            foreach (AgentePatogenico agentePatogenico in listaAgentePatogenico)
            {
                worksheet.Cell(linhaCorrente, 1).Value = agentePatogenico.AgentePatogenicoId.ToString();
                worksheet.Cell(linhaCorrente, 2).Value = agentePatogenico.Nome;
                worksheet.Cell(linhaCorrente, 3).Value = agentePatogenico.TipoAgentePatogenico.Nome;
                linhaCorrente++;
            }

            using MemoryStream stream = new();
            workbook.SaveAs(stream);

            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AgentePatogenico.xlsx");
        }

        public async Task<IActionResult> ObterPlanilhaVariante()
        {
            using XLWorkbook workbook = new();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Variante Agente Patogênico");

            int linhaCorrente = 1;
            #region Cabeçalho
            worksheet.Cell(linhaCorrente, 1).Value = "#";
            worksheet.Cell(linhaCorrente, 2).Value = "Nome";
            worksheet.Cell(linhaCorrente, 3).Value = "Caracteristica";
            worksheet.Cell(linhaCorrente, 4).Value = "Principais Mutações";
            worksheet.Cell(linhaCorrente, 5).Value = "Nome Agente Patogênico";
            linhaCorrente++;
            #endregion

            List<VarianteAgentePatogenico> listaVarianteAgentePatogenico = await _varianteAgentePatogenicoDao.RecuperarTodosAsync();

            foreach (VarianteAgentePatogenico varianteAgentePatogenico in listaVarianteAgentePatogenico)
            {
                worksheet.Cell(linhaCorrente, 1).Value = varianteAgentePatogenico.VarianteAgentePatogenicoId.ToString();
                worksheet.Cell(linhaCorrente, 2).Value = varianteAgentePatogenico.Nome;
                worksheet.Cell(linhaCorrente, 3).Value = varianteAgentePatogenico.Caracteristica;
                worksheet.Cell(linhaCorrente, 4).Value = varianteAgentePatogenico.PrincipaisMutacoes;
                worksheet.Cell(linhaCorrente, 5).Value = varianteAgentePatogenico.AgentePatogenico.Nome;
                linhaCorrente++;
            }

            using MemoryStream stream = new();
            workbook.SaveAs(stream);

            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VarianteAgentePatogenico.xlsx");
        }
    }

}

