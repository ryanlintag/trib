namespace Application
{
    public record SearchQuery
    {
        public string SearchValue { get; private set; } = string.Empty;
        public int PageNumber { get; private set; } = 1;
        public int PageSize { get; private set; } = 20;
        public string SortProperty { get; private set; } = string.Empty;
        public SortOrder SortOrder { get; private set; }
        public SearchQuery(string? searchValue, int? pageNumber, int? pageSize, string? sortProperty, string? sortOrder)
        {
            this.SearchValue = string.IsNullOrEmpty(searchValue) ? string.Empty : searchValue;
            if(!pageNumber.HasValue) pageNumber = 1;
            if (pageNumber.HasValue && pageNumber < 1) pageNumber = 1;
            this.PageNumber = pageNumber.Value;
            if (!pageSize.HasValue) pageSize = 20;
            if (pageSize.HasValue && pageSize < 20) pageSize = 20;
            this.PageSize = pageSize.Value;
            this.SortProperty = string.IsNullOrEmpty(sortProperty) ? string.Empty : sortProperty; 
            this.SortOrder = new SortOrder(sortOrder);
        }
    }
    public record SortOrder
    {
        public SortOrder(string value)
        {
            if(value == "DESC")
            {
                this.Value = "DESC";
            }
            else
            {
                this.Value = "ASC";
            }
        }
        public string Value { get; private set; }
        public string OrderByAscending { get { return "ASC"; } }
        public string OrderByDescending { get { return "DESC"; } }
    }
}
