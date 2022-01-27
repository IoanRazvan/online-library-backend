using ProiectDAW.DTOs;
using ProiectDAW.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface ILibraryService : IGenericService<Library>
    {
        Task<List<LibraryDTO>> FindLibrariesOfPrincipal();

        Task<List<LibraryDTO>> FindLibrariesOfPrincipalThatContainBook(Guid bookId);

        Task<List<LibraryDTO>> UpdateLibraryAssignment(LibraryAssignmentUpdateDTO newLibraryAssignments);
    }
}
