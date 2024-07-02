﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Webshop.Help.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private string connectionString; //the server connectionstring without database
        private string mainconnectionString;
        private string server = "localhost";
        private List<string> Errors = new List<string>();

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            this.connectionString = config.GetConnectionString("DefaultConnection");
            this.mainconnectionString = this.connectionString;
            string newServer = Environment.GetEnvironmentVariable("SERVER");
            if (!string.IsNullOrEmpty(newServer))
            {
                this.server = newServer;
            }
            this.mainconnectionString = this.mainconnectionString.Replace("{server}", this.server);
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            //create the database
            this.connectionString = this.mainconnectionString + ";database=master";
            CreateDatabase();
            CreateReviewDatabase();//creating psureviews database
            this.connectionString = this.mainconnectionString + ";database=psuwebshop"; //make sure they are created in the right database
            CreateCategoryTable();
            CreateCustomerTable();
            CreateProductTable();
            CreateProductCategoryTable();
            this.connectionString = this.mainconnectionString + ";database=PSUReviews"; //make sure they are created in the right database
            CreateReviewsTable();
            TempData["errors"] = Errors;
            return Redirect("/?seed=1");
        }

        private void CreateDatabase()
        {            
            ExecuteSQL("CREATE DATABASE psuwebshop", this.connectionString);           
        }

        private void CreateReviewDatabase()
        {
            ExecuteSQL("CREATE DATABASE PSUReviews", this.connectionString);
        }

        private void CreateReviewsTable()
        {
            string sql = @"CREATE TABLE [dbo].[Reviews](
	        [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	        [ProductId] [int] NOT NULL,
	        [UserId] [int] NOT NULL,
	        [Comment] [nvarchar](max) NOT NULL,
	        [Rating] [int] NOT NULL,
	        [Created] [datetime] NOT NULL)";

            string alterSql = "ALTER TABLE [dbo].[Reviews] ADD  DEFAULT (getdate()) FOR [Created]";
            
            ExecuteSQL(sql, this.connectionString);
            ExecuteSQL(alterSql, this.connectionString);
        }

        private void CreateCategoryTable()
        {
            string sql = "CREATE TABLE Category(" +
            "[Id] [int] IDENTITY(1,1) NOT NULL," +
            "[Name] [nvarchar](150) NOT NULL," +
            "[ParentId] [int] NOT NULL," +
            "[Description] [ntext] NOT NULL," +
            "CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED " +
            "(" +
            "[Id] ASC" +
            ")" +
            ")";
            ExecuteSQL(sql, this.connectionString);
        }

        private void CreateCustomerTable()
        {
            string sql = "CREATE TABLE Customer(" +
            "[Id] [int] IDENTITY(1,1) NOT NULL," +
            "[Name] [nvarchar](150) NOT NULL," +
            "[Address] [nvarchar](200) NOT NULL," +
            "[Address2] [nvarchar](200) NULL," +
            "[City] [nvarchar](200) NOT NULL," +
            "[Region] [nvarchar](200) NOT NULL," +
            "[PostalCode] [nvarchar](50) NOT NULL," +
            "[Country] [nvarchar](150) NOT NULL," +
            "[Email] [nvarchar](100) NOT NULL," +
            "CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED " +
            "(" +
            "[Id] ASC" +
            ")" +
            ")";
            ExecuteSQL(sql, this.connectionString);
        }

        private void CreateProductTable()
        {
            string sql = "CREATE TABLE Product(" +
            "[Id] [int] IDENTITY(1,1) NOT NULL," +
            "[Name] [nvarchar](150) NOT NULL," +
            "[SKU] [nvarchar](50) NOT NULL," +
            "[Price] [int] NOT NULL," +
            "[Currency] [nvarchar](3) NOT NULL," +
            "[Description] [ntext] NULL," +
            "[AmountInStock] [int] NULL," +
            "[MinStock] [int] NULL," +
            "CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED " +
            "(" +
            "[Id] ASC" +
            ")" +
            ")";
            ExecuteSQL(sql, this.connectionString);
        }

        private void CreateProductCategoryTable()
        {
            string sql = "CREATE TABLE ProductCategory(" +
            "[ProductId] [int] NOT NULL," +
            "[CategoryId] [int] NOT NULL," +
            "CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED " +
            "(" +
            "[ProductId] ASC," +
            "[CategoryId] ASC" +
            ")" +
            ")";
            ExecuteSQL(sql, this.connectionString);
        }

        private void ExecuteSQL(string sql, string localConnectionString)
        {
            try
            {
                Console.WriteLine("Connection: " + localConnectionString);
                using (SqlConnection connection = new SqlConnection(localConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            } catch(Exception ex)
            {
                Errors.Add(ex.Message);
            }
        }
    }
}