using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface ILibraryRepository : IGenericRepository<Library>
    {
        Task<List<Library>> FindLibrariesByPredicate(Expression<Func<Library, bool>> predicate);
    }
}
