using System.Collections.Generic;


namespace ProiectDAW.Services.Types
{
    public class Page<T>
    {
        public int CurrentPageNumber { get; set; }

        public int LastPageNumber { get; set; }

        public List<T> Result { get; set; }

        public string Query { get; set; }

        public int PageSize { get; set; }

        public int? Order { get; set; }
    }
}
