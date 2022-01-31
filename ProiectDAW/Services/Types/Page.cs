using System;
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

    public class PageUtils
    {
        public static int ComputeLastPage(int totalRecords, int pageSize)
        {
            int result = (int)Math.Ceiling((double)totalRecords / pageSize) - 1;
            return result < 0 ? 0 : result;
        }
    }
}
