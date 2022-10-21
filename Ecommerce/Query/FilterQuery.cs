namespace Ecommerce.Query
{
    public class FilterQuery
    {
        
        public string? Category_Name { get; set; }
       

        public bool HaveFilter => !string.IsNullOrEmpty(Category_Name);
    
}
}
