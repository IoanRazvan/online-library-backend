using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using ProiectDAW.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class LibraryService : GenericService<Library>, ILibraryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public LibraryService(ILibraryRepository repo, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(repo)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<List<LibraryDTO>> FindLibrariesOfPrincipal()
        {
            User principal = _httpContextAccessor.GetPrincipal();
            return _mapper.Map<List<Library>, List<LibraryDTO>>(await ((ILibraryRepository)_repo).FindLibrariesByPredicate((library) => library.OwnerId.Equals(principal.Id)));
        }

        public async Task<List<LibraryDTO>> FindLibrariesOfPrincipalThatContainBook(Guid bookId)
        {
            User principal = _httpContextAccessor.GetPrincipal();
            return _mapper.Map<List<Library>, List<LibraryDTO>>(await ((ILibraryRepository)_repo).FindLibrariesByPredicate((library) => library.OwnerId.Equals(principal.Id) && library.Books.Any(book => book.Id.Equals(bookId))));
        }
    }
}
