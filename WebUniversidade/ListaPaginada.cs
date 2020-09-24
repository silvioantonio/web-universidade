using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversidade
{
    public class ListaPaginada<T> : List<T>
    {
        public int IndicePagina { get; private set; }
        public int TotalPaginas { get; private set; }

        public ListaPaginada(List<T> itens, int cont, int indicePagina, int tamanhoPagina)
        {
            IndicePagina = indicePagina;
            TotalPaginas = (int)Math.Ceiling(cont / (double)tamanhoPagina);

            this.AddRange(itens);
        }


        public bool HasPreviousPage
        {
            get { return (IndicePagina > 1); }
        }

        public bool HasNextPage
        {
            get { return (IndicePagina < TotalPaginas); }
        }

        public static async Task<ListaPaginada<T>> CriarAsync(IQueryable<T> fonte, int indicePagina, int tamanhoPagina)
        {
            var cont = await fonte.CountAsync();
            var itens = await fonte.Skip((indicePagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();

            return new ListaPaginada<T>(itens, cont, indicePagina, tamanhoPagina);
        }

    }
}
