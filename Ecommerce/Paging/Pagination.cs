namespace Ecommerce.Paging
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Pagination()
        {
            this.PageNumber = 1;
            this.PageSize = 3;
        }
    }
}
