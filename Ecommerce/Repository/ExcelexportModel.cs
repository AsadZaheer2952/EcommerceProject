namespace Ecommerce.Repository
{
    public class ExcelexportModel<T>
    {

        public string type { get; set; }
        public List<T> data { get; set; }
        public ExcelexportModel(List<T> data, string message = null)
        {
            this.type = message;
            this.data = data;
        }
    }
}