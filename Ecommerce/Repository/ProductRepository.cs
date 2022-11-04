using ClosedXML.Excel;
using Ecommerce.Data;
using Ecommerce.Model;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;
using System.Diagnostics;

namespace Ecommerce.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly EcommStoreContext _context;
        public ProductRepository(EcommStoreContext context)
        {
            _context = context;

        }
        public async Task<Guid> AddProduct(Product product)
        {
            var Productrecord = new Product()
            {

                ProductDescription = product.ProductDescription,
                ProductName = product.ProductName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = product.CreatedBy,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = product.UpdatedBy,
                DeletedAt = DateTime.UtcNow,
                DeletedBy = product.DeletedBy,

            };

            _context.Products.Add(Productrecord);
            var bridge = new ProductCategories()
            {
                ProductId = Productrecord.ProductId,
                CategoryId = product.CategoryId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = product.CreatedBy,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = product.UpdatedBy,
                DeletedAt = DateTime.UtcNow,
                DeletedBy = product.DeletedBy,
            };
            _context.ProductCategories.Add(bridge);
            await _context.SaveChangesAsync();
            return Productrecord.ProductId;

        }


        public async Task<List<Product>> GetAllProduct()
        { 
            var records = await _context.Products.ToListAsync();
            /*    {

                    ProductDescription = x.ProductDescription,
                    ProductName = x.ProductName,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = x.UpdatedBy,
                    DeletedAt = DateTime.UtcNow,
                    DeletedBy = x.DeletedBy

                })*/
            return (records);
        }
        public async Task<Product> GetProductById(Guid ProductId)
        {
            var record = await _context.Products.Where(i => i.ProductId == ProductId).FirstOrDefaultAsync();
            return (record);
        }
        public async Task UpdateProduct(Guid ProductId, Product product)
        {
            var record = await _context.Products.FindAsync(ProductId);
            if (record != null)
            {

                record.ProductName = product.ProductName;
                record.ProductDescription = product.ProductDescription;
                record.UpdatedBy = product.UpdatedBy;

                await _context.SaveChangesAsync();


            }

        }
        public async Task DeleteProduct(Guid ProductId)
        {
            var delete = new Product() { ProductId = ProductId };


            _context.Products.Remove(delete);
            await _context.SaveChangesAsync();


        }
        public async Task<int> UploadExcelFile(Upload uploadRequest)
        {
            if (uploadRequest == null || !(uploadRequest.File.FileName.ToLower().Contains(".xlsx")))
                throw new ArgumentNullException(nameof(uploadRequest));
            List<Product> Parameters = new();
            DataSet dataSet;
            string filePath = "UploadFile/" + uploadRequest.File.FileName;

            using (FileStream stream = new(filePath, FileMode.OpenOrCreate))
            {
                await uploadRequest.File.CopyToAsync(stream);
            }

            FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);
            dataSet = reader.AsDataSet(
                    new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }

                    });
            if (dataSet == null || dataSet.Tables[0] == null)
                throw new ArgumentNullException(nameof(uploadRequest));

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Product rows = new()
                {
                    ProductName = dataSet.Tables[0].Rows[i].ItemArray[1] != null ? dataSet.Tables[0].Rows[i].ItemArray[1].ToString() : "NULL",
                    ProductDescription = dataSet.Tables[0].Rows[i].ItemArray[1] != null ? dataSet.Tables[0].Rows[i].ItemArray[1].ToString() : "NULL",

                    CreatedAt = DateTime.Parse(dataSet.Tables[0].Rows[i].ItemArray[3].ToString()),

                    CreatedBy = dataSet.Tables[0].Rows[i].ItemArray[4] != null ? dataSet.Tables[0].Rows[i].ItemArray[4].ToString() : "NULL",

                    UpdatedAt = DateTime.Parse(dataSet.Tables[0].Rows[i].ItemArray[5].ToString()),

                    UpdatedBy = dataSet.Tables[0].Rows[i].ItemArray[6] != null ? dataSet.Tables[0].Rows[i].ItemArray[6].ToString() : "NULL",

                    DeletedAt = DateTime.Parse(dataSet.Tables[0].Rows[i].ItemArray[7].ToString()),

                    DeletedBy = dataSet.Tables[0].Rows[i].ItemArray[8] != null ? dataSet.Tables[0].Rows[i].ItemArray[8].ToString() : "NULL"
                };

                Parameters.Add(rows);
            }
            fileStream.Close();

            if (Parameters.Count <= 0)
                throw new ArgumentNullException(nameof(uploadRequest));
            foreach (Product rows in Parameters)
            {
                Product product = new()
                {
                    ProductName = rows.ProductName,
                    ProductDescription = rows.ProductDescription,
                    CreatedAt = rows.CreatedAt,
                    CreatedBy = rows.CreatedBy,
                    UpdatedAt = rows.UpdatedAt,
                    UpdatedBy = rows.UpdatedBy,
                    DeletedAt = rows.DeletedAt,
                    DeletedBy = rows.DeletedBy
                };
                _context.Products.Add(product);

                _context.SaveChanges();

            }
            return 1;
        }
        public async Task<dynamic> ExportExcelFile()
        {
            //var products = await _context.Products.Select(x => new Product()
            //{
            //    ProductId = x.ProductId,
            //    ProductDescription = x.ProductDescription,
            //    ProductName = x.ProductName,
            //    CreatedAt = DateTime.UtcNow,
            //    CreatedBy = x.CreatedBy,
            //    UpdatedAt = DateTime.UtcNow,
            //    UpdatedBy = x.UpdatedBy,
            //    DeletedAt = DateTime.UtcNow,
            //    DeletedBy = x.DeletedBy

            //}).ToListAsync();          
            var products = await _context.Products.ToListAsync();

            if (products == null)
                throw new ArgumentNullException();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Product");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = "ProductID";
            worksheet.Cell(currentRow, 2).Value = "ProductName";
            worksheet.Cell(currentRow, 3).Value = "ProductDescription";
            worksheet.Cell(currentRow, 4).Value = "CreatedAt";
            worksheet.Cell(currentRow, 5).Value = "CreatedBy";
            worksheet.Cell(currentRow, 6).Value = "UpdatedAt";
            worksheet.Cell(currentRow, 7).Value = "UpdatedBy";
            worksheet.Cell(currentRow, 8).Value = "DeletedAt";
            worksheet.Cell(currentRow, 9).Value = "DeletedBy";
            foreach (var product in products)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = product.ProductId;
                worksheet.Cell(currentRow, 2).Value = product.ProductName;
                worksheet.Cell(currentRow, 3).Value = product.ProductDescription;
                worksheet.Cell(currentRow, 4).Value = product.CreatedAt;
                worksheet.Cell(currentRow, 5).Value = product.CreatedBy;
                worksheet.Cell(currentRow, 6).Value = product.UpdatedAt;
                worksheet.Cell(currentRow, 7).Value = product.UpdatedBy;
                worksheet.Cell(currentRow, 8).Value = product.DeletedAt;
                worksheet.Cell(currentRow, 9).Value = product.DeletedBy;
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }

        public async Task<dynamic> ExportPdfFile()
        {
            var products = await _context.Products.ToListAsync();
            {
                /*  ProductId = x.ProductId,
                  ProductDescription = x.ProductDescription,
                  ProductName = x.ProductName,
                  CreatedAt = DateTime.UtcNow,
                  CreatedBy = x.CreatedBy,
                  UpdatedAt = DateTime.UtcNow,
                  UpdatedBy = x.UpdatedBy,
                  DeletedAt = DateTime.UtcNow,
                  DeletedBy = x.DeletedBy

              })*/


                if (products == null)
                    throw new ArgumentNullException();
                var duPdf = Document
        .Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A2);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).SemiBold().FontColor(Colors.BlueGrey.Medium));
                page.Content()
                   .PaddingVertical(1, Unit.Centimetre)
                   .Column(x =>
                   {
                       x.Spacing(15);

                       x.Item().Table(t =>
                       {
                           t.ColumnsDefinition(c =>
                           {
                               c.RelativeColumn();
                               c.RelativeColumn(3);
                           });



                           //sheet
                           x.Item().Table(table =>
                           {

                               table.ColumnsDefinition(columns =>
                           {
                               columns.ConstantColumn(20);
                               columns.RelativeColumn();
                               columns.RelativeColumn();
                               columns.RelativeColumn();
                               columns.RelativeColumn();
                               columns.RelativeColumn();
                               columns.RelativeColumn();
                               columns.RelativeColumn();
                               columns.RelativeColumn();
                               columns.RelativeColumn();
                           });

                               // header
                               table.Header(header =>
                           {
                               header.Cell().Text("#");
                               header.Cell().AlignCenter().Text("ProductId");
                               header.Cell().AlignCenter().Text("ProductName");
                               header.Cell().AlignCenter().Text("ProductDescription");
                               header.Cell().AlignCenter().Text("CreatedAt");
                               header.Cell().AlignCenter().Text("CreatedBy");
                               header.Cell().AlignCenter().Text("UpdatedAt");
                               header.Cell().AlignCenter().Text("UpdatedBy");
                               header.Cell().AlignCenter().Text("DeletedAt");
                               header.Cell().AlignCenter().Text("DeletedBy");


                               header.Cell().ColumnSpan(10)
                               .PaddingVertical(10).BorderBottom(1).BorderColor(Colors.Black);
                           });



                               // Data combination
                               for (int i = 0; i < products.Count; i++)
                               {
                                   table.Cell().Element(CellStyle).AlignCenter().Text(i + 1);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].ProductId);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].ProductName);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].ProductDescription);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].CreatedAt);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].CreatedBy);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].UpdatedAt);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].UpdatedBy);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].DeletedAt);
                                   table.Cell().Element(CellStyle).AlignCenter().Text(products[i].DeletedBy);



                                   static IContainer CellStyle(IContainer container)
                                   {
                                       return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);


                                   }

                               }


                           });


                       });

                       page.Footer()
                           .AlignCenter()
                           .Text(x =>
                           {
                               x.Span("No.");
                               x.CurrentPageNumber();
                               x.Span("page");
                               x.Span("/ total");
                               x.TotalPages();
                               x.Span("page");
                           });
                   });

            });


        });
                return duPdf.GeneratePdf();
            }

/*
            List<Product> produts = new List<Product>();
            Console.WriteLine("Printing list using foreach loop\n");

            var stopWatch = Stopwatch.StartNew();
            foreach (Product product in products)
            {
                Console.WriteLine("ok");
            }*/
        }

    }
}