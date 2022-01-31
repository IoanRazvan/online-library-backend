using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Services.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface ILibraryService : IGenericService<Library>
    {
        Task<List<LibraryDTO>> FindLibrariesByOwner();

        Task<List<LibraryDTO>> FindLibrariesByOwnerAndBook(Guid bookId);

        Task<List<LibraryDTO>> UpdateLibraryBook(ManyToManyUpdateDTO newLibraryAssignments);
    }
}
