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
        private readonly ILibraryBookRepository _libraryBookRepo;

        public LibraryService(ILibraryRepository repo, ILibraryBookRepository libraryBookRepo, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(repo)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _libraryBookRepo = libraryBookRepo;
        }

        public async Task<List<LibraryDTO>> FindLibrariesByOwner()
        {
            User principal = _httpContextAccessor.GetPrincipal();
            return _mapper.Map<List<Library>, List<LibraryDTO>>(await ((ILibraryRepository)_repo).FindLibrariesByPredicate((library) => library.OwnerId.Equals(principal.Id)));
        }

        public async Task<List<LibraryDTO>> FindLibrariesByOwnerAndBook(Guid bookId)
        {
            User principal = _httpContextAccessor.GetPrincipal();
            return _mapper.Map<List<Library>, List<LibraryDTO>>(await ((ILibraryRepository)_repo).FindLibrariesByPredicate((library) => library.OwnerId.Equals(principal.Id) && library.Books.Any(book => book.BookId.Equals(bookId))));
        }

        public async Task<List<LibraryDTO>> UpdateLibraryBook(ManyToManyUpdateDTO newLibraryAssignments)
        {
            var toRemove = newLibraryAssignments.Removed.Select(libId => new LibraryBook { BookId = newLibraryAssignments.EntityId, LibraryId = libId });
            var toAdd = newLibraryAssignments.Added.Select(libId => new LibraryBook { BookId = newLibraryAssignments.EntityId, LibraryId = libId });
            if (toAdd.Count() != 0)
                _libraryBookRepo.CreateRange(toAdd);
            if (toRemove.Count() != 0)
                _libraryBookRepo.DeleteRange(toRemove);
            if (!await _libraryBookRepo.Save())
                return null;
            return await FindLibrariesByOwnerAndBook(newLibraryAssignments.EntityId);
        }
    }
}
