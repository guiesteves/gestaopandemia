using ChartJSCore.Helpers;
using ChartJSCore.Models;
using CVC19.Data.Dao;
using CVC19.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CVC19.Controllers
{
    public class HomeController : Controller
    {
        private readonly VarianteAgentePatogenicoDao _varianteAgentePatogenicoDao;
        private readonly LaboratorioDao _laboratorioDao;
        private readonly VacinaDao _vacinaDaoDao;
        private static readonly Random _random = new();

        public HomeController(VarianteAgentePatogenicoDao varianteAgentePatogenicoDao,
                              LaboratorioDao laboratorioDao, VacinaDao vacinaDaoDao)
        {
            _varianteAgentePatogenicoDao = varianteAgentePatogenicoDao;
            _laboratorioDao = laboratorioDao;
            _vacinaDaoDao = vacinaDaoDao;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            ViewData["chartA"] = GenerateBarChart(_vacinaDaoDao.ObterQuantidadeVacinaPorLaboratorio(), "Quantidade Vacinas Por Laboratório");
            ViewData["chartB"] = GeneratePieChart(_varianteAgentePatogenicoDao.ObterQuantidadeVariantesPorPais(), "Quantidade Variantes Por País");
            ViewData["chartC"] = GeneratePieChart(_laboratorioDao.ObterQuantidadeLaboratoriosPorPais(), "Quantidade Laboratórios Por País");
            ViewData["chartD"] = GeneratePieChart(_vacinaDaoDao.ObterQuantidadeVacinaPorTipoVacina(), "Quantidade Vacinas Por Tipo");
            ViewData["chartE"] = GeneratePieChart(_varianteAgentePatogenicoDao.ObterQuantidadeVariantesPorAgentePatogenico(), "Quantidade de Variantes Por Agente Patogênico");

            return View();
        }


        private static (byte, byte, byte) GerarRGB()
        {
            return ((byte)(32 * _random.Next(0, 8)), (byte)(32 * _random.Next(0, 8)), (byte)(32 * _random.Next(0, 8)));
        }


        private static Chart GenerateBarChart(List<Tuple<string, int>> lista, string titulo)
        {
            List<Dataset> datasets = new();

            for (int i = 0; i < lista.Count; i++)
            {
                Tuple<string, int> dados = lista[i];
                (byte, byte, byte) rgb = GerarRGB();

                datasets.Add(new BarDataset()
                {

                    Data = new List<double?>() { dados.Item2 },
                    BackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(rgb.Item1, rgb.Item2, rgb.Item3, 0.9) },
                    BorderColor = new List<ChartColor>() { ChartColor.FromRgb(rgb.Item1, rgb.Item2, rgb.Item3) },
                    Label = dados.Item1
                });
            }

            Chart chart = new()
            {
                Type = Enums.ChartType.Bar,
                Data = new()
                {
                    Labels = new List<string>() { titulo },
                    Datasets = datasets
                },

                Options = new()
                {
                    Title = new()
                    {
                        Display = true,
                        Text = titulo,
                    },

                    Legend = new()
                    {
                        Display = true,
                    },

                    Scales = new()
                    {
                        XAxes = new List<Scale>
                        {
                            new BarScale
                            {
                                GridLines = new GridLine()
                                {
                                    OffsetGridLines = false
                                },


                            },
                        },

                        YAxes = new List<Scale>
                        {
                            new CartesianScale
                            {
                                Ticks = new CartesianLinearTick
                                {
                                    BeginAtZero = true,
                                    StepSize = 1,
                                    SuggestedMax = 3
                                }
                            }
                        },

                    },

                    Layout = new()
                    {
                        Padding = new Padding
                        {
                            PaddingObject = new PaddingObject
                            {
                                Left = 0,
                                Right = 0
                            }
                        }
                    }
                }
            };

            return chart;
        }

        private static Chart GeneratePieChart(List<Tuple<string, int>> lista, string titulo)
        {

            List<ChartColor> chartColorBackground = new();
            List<ChartColor> chartColorHoverBackground = new();
            for (int i = 0; i < lista.Count; i++)
            {
                (byte, byte, byte) rgb = GerarRGB();
                chartColorBackground.Add(ChartColor.FromRgba(rgb.Item1, rgb.Item2, rgb.Item3, 0.7));
                chartColorHoverBackground.Add(ChartColor.FromRgb(rgb.Item1, rgb.Item2, rgb.Item3));
            }

            Chart chart = new()
            {
                Type = Enums.ChartType.Pie,
                Data = new()
                {
                    Labels = lista.Select(l => l.Item1).ToList(),
                    Datasets = new List<Dataset>()
                    {
                        new PieDataset()
                        {
                            BackgroundColor = chartColorBackground,
                            HoverBackgroundColor =chartColorHoverBackground,
                            Data = lista.Select(l => l.Item2).Select<int, double?>(i => i).ToList(),
                        }
                    }
                },

                Options = new()
                {
                    Title = new()
                    {
                        Display = true,
                        Text = titulo,
                    }
                }
            };

            return chart;
        }
    }
}
