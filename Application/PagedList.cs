namespace Application
{
    public class PagedList<T>
    {
        public PagedList(List<T> items, int currentPage, int itemsPerPage, int totalRecordsCount, string searchText, string orderByProperty, SortOrder sortOrder)
        {
            this.Items = items;
            this.CurrentPage = currentPage;
            this.ItemsPerPage = itemsPerPage;
            this.TotalRecordsCount = totalRecordsCount;
            this.SearchText = searchText;
            this.OrderByProperty = orderByProperty;
            this.SortOrder = sortOrder;

        }
        public List<T> Items { get; private set; }
        public int CurrentPage { get; private set; }
        public int ItemsPerPage { get; private set; }
        public int TotalRecordsCount { get; private set; }
        public string SearchText { get; private set; }
        public string OrderByProperty { get; private set; }
        public SortOrder SortOrder { get; set; }
    }
}