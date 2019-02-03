using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CashBack.Context;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CashBack.Models;
using CashBack.Services.Interfaces;
using CashBack.Services;
using CashBack.Repositories.Interfaces;
using CashBack.Repositories;

namespace CashBack
{
    public class Startup
    {        
        public Startup(IConfiguration configuration)
        {          
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("database"));
            services.AddScoped<CashBackPercentualRepository>();
            services.AddScoped<CatalogoDiscoRepository>();
            services.AddScoped<VendaRepository>();

            services.AddResponseCompression();

            services.AddTransient<ICashBackPercentualService, CashBackPercentualService>();
            services.AddTransient<ICatalogoDiscoService, CatalogoDiscoService>();
            services.AddTransient<IVendaService, VendaService>();
            services.AddTransient<ICashBackPercentualRepository, CashBackPercentualRepository>();
            services.AddTransient<ICatalogoDiscoRepository, CatalogoDiscoRepository>();
            services.AddTransient<IVendaRepository, VendaRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                AdicionarPercentuaisCashBack(context);
                AdicionarAlbunsSpotify(context);
                AdicionarVendasTeste(context);
            }
            
            app.UseMvc();
        }

        private void AdicionarVendasTeste(ApplicationDbContext context)
        {
            CashBackPercentualRepository cashBackRepository = new CashBackPercentualRepository(context);

            DateTime dataBase = new DateTime(2018, 12, 03);
            DateTime dataVenda = dataBase;

            for (int i = 0; i < 40; i++)
            {
                if (i < 30) // Vender 30 discos Classicos
                {
                    if (i >= 10 && i < 20)
                    {
                        dataVenda = dataBase.AddMonths(1);
                    }
                    else if (i >= 20 && i < 30)
                    {
                        dataVenda = dataBase.AddMonths(2);
                    }

                    var vendaEfetuada = new Venda()
                    {
                        DataVenda = dataVenda
                    };

                    context.Vendas.Add(vendaEfetuada);

                    CatalogoDiscoRepository catalogoDiscos = new CatalogoDiscoRepository(context);
                    IEnumerable<Disco> discosVendidos = catalogoDiscos.ListarTodosPorGenero("CLASSIC", i, 3);

                    if (discosVendidos != null)
                    {                   
                        foreach (var item in discosVendidos)
                        {                                        
                            var ItensVendaEfetuada = new ItemVenda()
                            {
                                VendaID = vendaEfetuada.VendaID,
                                Disco  = item
                            };

                            // Busca valor do CashBack
                            decimal valorPercentualCash = cashBackRepository.ObterCashBack("CLASSIC", vendaEfetuada.DataVenda);                           
                            ItensVendaEfetuada.ValorCashBack = decimal.Round(ItensVendaEfetuada.Disco.PrecoVenda * valorPercentualCash,2);

                            context.ItensVendas.Add(ItensVendaEfetuada);
                        }
                    }

                    if (vendaEfetuada.Itens != null && vendaEfetuada.Itens.Any())
                    {
                        vendaEfetuada.CashBackTotalVenda = vendaEfetuada.Itens.Sum(x => x.ValorCashBack);
                        vendaEfetuada.ValorTotalItens = vendaEfetuada.Itens.Sum(x => x.Disco.PrecoVenda);
                    }

                    context.SaveChanges();
                }

                if (i >= 30 && i < 40) // Vender 10 discos rock
                {
                    if (i >= 30 && i < 35)
                    {
                        dataVenda = dataBase;
                    }
                    else if (i >= 35 && i < 40)
                    {
                        dataVenda = dataBase.AddMonths(2);
                    }

                    var vendaEfetuada = new Venda()
                    {
                        DataVenda = dataVenda
                    };

                    context.Vendas.Add(vendaEfetuada);

                    CatalogoDiscoRepository catalogoDiscos = new CatalogoDiscoRepository(context);
                    IEnumerable<Disco> discosVendidos = catalogoDiscos.ListarTodosPorGenero("ROCK", 0, 4);

                    if (discosVendidos != null)
                    {
                        foreach (var item in discosVendidos)
                        {
                            var ItensVendaEfetuada = new ItemVenda()
                            {
                                VendaID = vendaEfetuada.VendaID,
                                Disco = item
                            };

                            // Busca valor do CashBack
                            decimal valorPercentualCash = cashBackRepository.ObterCashBack("ROCK", vendaEfetuada.DataVenda);
                            ItensVendaEfetuada.ValorCashBack = decimal.Round(ItensVendaEfetuada.Disco.PrecoVenda * valorPercentualCash, 2);

                            context.ItensVendas.Add(ItensVendaEfetuada);
                        }
                    }

                    if (vendaEfetuada.Itens != null && vendaEfetuada.Itens.Any())
                    {
                        vendaEfetuada.CashBackTotalVenda = vendaEfetuada.Itens.Sum(x => x.ValorCashBack);
                        vendaEfetuada.ValorTotalItens = vendaEfetuada.Itens.Sum(x => x.Disco.PrecoVenda);
                    }

                    context.SaveChanges();
                }
            }            
        }

        private void AdicionarAlbunsSpotify(ApplicationDbContext context)
        {
            SpotifyService spotify = new SpotifyService();
            TokenSpotify token = spotify.ObterTokenSpotify();

            if (token != null && !string.IsNullOrWhiteSpace(token.access_token))
            {
                string[] generos = new string[] { "pop", "rock", "classical", "jazz" };

                foreach (string genero in generos)
                {
                    RetornoAlbunsSpotify.RootObject albunsSpotify = spotify.ObterListaAlbuns(token, genero);

                    if (albunsSpotify != null && albunsSpotify.playlists != null)
                    {
                        string generoBanco = string.Empty;
                        switch (genero)
                        {
                            case "pop":
                                generoBanco = "POP";
                                break;
                            case "classical":
                                generoBanco = "CLASSIC";
                                break;
                            case "rock":
                                generoBanco = "ROCK";
                                break;
                            default:
                                generoBanco = "MPB";
                                break;
                        }

                        if (albunsSpotify.playlists.items.Any())
                        {
                            foreach (var item in albunsSpotify.playlists.items)
                            {
                                Disco disco = new Disco()
                                {
                                    Genero = generoBanco,
                                    Nome = item.name
                                };

                                Random randNum = new Random();
                                decimal valorVenda = randNum.Next(10, 100);
                                double centavos = randNum.NextDouble();
                                disco.PrecoVenda = valorVenda + decimal.Round((decimal)centavos, 2);

                                context.CatalogoDiscos.Add(disco);
                            }

                            context.SaveChanges();
                        }
                    }
                }                                
            }
            
        }

        private void AdicionarPercentuaisCashBack(ApplicationDbContext context)
        {
            var pop = new CashBackPercentual
            {
                Genero = "POP",
                Domingo = 0.25M,
                Segunda = 0.07M,
                Terca = 0.06M,
                Quarta = 0.02M,
                Quinta = 0.1M,
                Sexta = 0.15M,
                Sabado = 0.2M
            };

            context.CashBackPercentuais.Add(pop);

            var mpb = new CashBackPercentual
            {
                Genero = "MPB",
                Domingo = 0.30M,
                Segunda = 0.05M,
                Terca = 0.1M,
                Quarta = 0.15M,
                Quinta = 0.2M,
                Sexta = 0.25M,
                Sabado = 0.3M
            };

            context.CashBackPercentuais.Add(mpb);

            var classic = new CashBackPercentual
            {
                Genero = "CLASSIC",
                Domingo = 0.35M,
                Segunda = 0.03M,
                Terca = 0.05M,
                Quarta = 0.08M,
                Quinta = 0.13M,
                Sexta = 0.18M,
                Sabado = 0.25M
            };

            context.CashBackPercentuais.Add(classic);

            var rock = new CashBackPercentual
            {
                Genero = "ROCK",
                Domingo = 0.4M,
                Segunda = 0.1M,
                Terca = 0.15M,
                Quarta = 0.15M,
                Quinta = 0.15M,
                Sexta = 0.20M,
                Sabado = 0.40M
            };

            context.CashBackPercentuais.Add(rock);

            context.SaveChanges();
        }
    }
}
