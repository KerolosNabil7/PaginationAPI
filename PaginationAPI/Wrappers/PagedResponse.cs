﻿namespace PaginationAPI.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri? FirstPage { get; set; }
        public Uri? LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri? NextPage { get; set; }
        public Uri? PreviousPage { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize) : base(data) // Fix: Pass 'data' to the base class constructor
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
