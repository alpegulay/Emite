﻿namespace App.Exam.Emite.Api.Core.Models.Pagination
{
    public class PaginatedResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
