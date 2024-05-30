using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Application
{
    public  interface IUnitofWork : IDisposable
    {
        IMovieRepository? Movies { get; }

        Task<int> CompleteAsync();
    }
}
