

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Model;


namespace TestProject.MockData
{
    public class UseMockData
    {
        public static List<Category> GetAll()
        {
            return new List<Category>
            {
                new Category
                {
                    Category_Name = "test",
                    Category_Description = "test",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "string",
                    UpdatedAt= DateTime.Now,
                    UpdatedBy = "string",
                    DeletedAt = DateTime.Now,
                    DeletedBy = "string",
                },
                  new Category
                {
                    Category_Name = "sport",
                    Category_Description = "string",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "string",
                    UpdatedAt= DateTime.Now,
                    UpdatedBy = "string",
                    DeletedAt = DateTime.Now,
                    DeletedBy = "string",
                },
                    new Category
                {
                    Category_Name = "Categor_Name",
                    Category_Description = "abc",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Asad",
                    UpdatedAt= DateTime.Now,
                    UpdatedBy = "asad",
                    DeletedAt = DateTime.Now,
                    DeletedBy = "Asad",
                },
            };
        }
        public static Category AddCategory()
        {
            return new Category
            {
                 
                Category_Name = "asad",
                Category_Description="laptop",
                ParentId=0,
                CreatedAt=DateTime.Now,
                CreatedBy="category",
                UpdatedAt  =DateTime.Now,
                UpdatedBy="category",
                DeletedAt=DateTime.Now,
                DeletedBy ="category",

               
            };
        }
    }
}
    

