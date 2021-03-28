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
            ViewData["chartA"] = GenerateBarChart(_vacinaDaoDao.ObterQuantidadeVacinaPorLaboratorio(), "Quantidade Vacinas Por Laboratório", 6);
            ViewData["chartB"] = GeneratePieChart(_varianteAgentePatogenicoDao.ObterQuantidadeVariantesPorPais(), "Quantidade Variantes Por País", 6);
            ViewData["chartC"] = GeneratePieChart(_laboratorioDao.ObterQuantidadeLaboratoriosPorPais(), "Quantidade Laboratórios Por País", 6);
            ViewData["chartD"] = GeneratePieChart(_vacinaDaoDao.ObterQuantidadeVacinaPorTipoVacina(), "Quantidade Vacinas Por Tipo", 6);
            ViewData["chartE"] = GeneratePieChart(_varianteAgentePatogenicoDao.ObterQuantidadeVariantesPorAgentePatogenico(), "Quantidade de Variantes Por Agente Patogênico", 6);

            return View();
        }


        private static (byte, byte, byte) GerarRGB()
        {
            return ((byte)(32 * _random.Next(0, 8)), (byte)(32 * _random.Next(0, 8)), (byte)(32 * _random.Next(0, 8)));
        }


        private static Chart GenerateBarChart(List<Tuple<string, int>> lista, string titulo, int quantidadeMaximaItens)
        {
            quantidadeMaximaItens -= 1;

            List<Dataset> datasets = new();
            (byte, byte, byte) rgb;

            if (quantidadeMaximaItens >= lista.Count) 
            {
                quantidadeMaximaItens = lista.Count;
            }

            for (int i = 0; i < quantidadeMaximaItens; i++)
            {
                Tuple<string, int> dados = lista[i];
                rgb = GerarRGB();

                datasets.Add(new BarDataset()
                {

                    Data = new List<double?>() { dados.Item2 },
                    BackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(rgb.Item1, rgb.Item2, rgb.Item3, 0.9) },
                    BorderColor = new List<ChartColor>() { ChartColor.FromRgb(rgb.Item1, rgb.Item2, rgb.Item3) },
                    Label = dados.Item1
                });
            }

            if (quantidadeMaximaItens < lista.Count) 
            {
                int valor = 0;
                for (int i = quantidadeMaximaItens; i < lista.Count; i++)
                {
                    valor += lista[i].Item2;
                }

                rgb = GerarRGB();
                datasets.Add(new BarDataset()
                {

                    Data = new List<double?>() { valor },
                    BackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(rgb.Item1, rgb.Item2, rgb.Item3, 0.9) },
                    BorderColor = new List<ChartColor>() { ChartColor.FromRgb(rgb.Item1, rgb.Item2, rgb.Item3) },
                    Label = "Outros"
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

        private static Chart GeneratePieChart(List<Tuple<string, int>> lista, string titulo, int quantidadeMaximaItens)
        {
            quantidadeMaximaItens -= 1;
            List<ChartColor> chartColorBackground = new();
            List<ChartColor> chartColorHoverBackground = new();
            List<string> titulos = new();
            List<double?> valores = new();
            (byte, byte, byte) rgb;
            
            if (quantidadeMaximaItens >= lista.Count)
            {
                quantidadeMaximaItens = lista.Count;
            }

            for (int i = 0; i < quantidadeMaximaItens; i++)
            {
                rgb = GerarRGB();
                chartColorBackground.Add(ChartColor.FromRgba(rgb.Item1, rgb.Item2, rgb.Item3, 0.7));
                chartColorHoverBackground.Add(ChartColor.FromRgb(rgb.Item1, rgb.Item2, rgb.Item3));
                titulos.Add(lista[i].Item1);
                valores.Add(lista[i].Item2);
            }
            if (quantidadeMaximaItens < lista.Count)
            {
                int valor = 0;
                for (int i = quantidadeMaximaItens; i < lista.Count; i++)
                {
                    valor += lista[i].Item2;
                }

                int valorOutros = 0;
                for (int i = quantidadeMaximaItens; i < lista.Count; i++)
                {
                    valorOutros += lista[i].Item2;
                }

                rgb = GerarRGB();
                chartColorBackground.Add(ChartColor.FromRgba(rgb.Item1, rgb.Item2, rgb.Item3, 0.7));
                chartColorHoverBackground.Add(ChartColor.FromRgb(rgb.Item1, rgb.Item2, rgb.Item3));
                titulos.Add("Outros");
                valores.Add(valorOutros);
            }

            Chart chart = new()
            {
                Type = Enums.ChartType.Pie,
                Data = new()
                {
                    Labels = titulos,
                    Datasets = new List<Dataset>()
                    {
                        new PieDataset()
                        {
                            BackgroundColor = chartColorBackground,
                            HoverBackgroundColor =chartColorHoverBackground,
                            Data = valores,
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
