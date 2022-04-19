using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using wsa_500_routes.Models;

namespace wsa_500_routes.Controllers
{
/*    [Route("api/[controller]")]*/
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Grab our sql connection using our ValuesController constructor
        private readonly IConfiguration _configuration;
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("api/[controller]/products")]
        [HttpGet]
        public JsonResult GetProducts()
        {
            string query = @"
                Select * from wsa_500.products;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult(table);
        }


        [Route("api/[controller]/categories")]
        [HttpGet]
        public JsonResult GetCategories()
        {
            string query = @"
                Select * from wsa_500.categories;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult(table);
        }

        // Get Route to return Categories based on categoryID
        [Route("api/[controller]/categories/{id}")]
        [HttpGet]
        public JsonResult GetSpecificCategory(int id)
        {
            string query = @"
                Select * from wsa_500.categories
                   WHERE (`CategoryID` = @CategoryID);;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@CategoryID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult(table);
        }



        // Get Route to return Products Based on ProductID
        [Route("api/[controller]/products/{id}")]
        [HttpGet]
        public JsonResult GetSpecificProduct(int id)
        {
            string query = @"
                Select * from wsa_500.products
                   WHERE (`ProductID` = @ProductID);;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ProductID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult(table);
        }
        // Insert route for products
        [Route("api/[controller]/products")]
        [HttpPost]
        public JsonResult PostProduct(Products prod)
        {
            string query = @"
                INSERT INTO `wsa_500`.`products` (`ProductName`, `SupplierID`, `CategoryID`, 
                        `QuantityPerUnit`, `UnitPrice`, `UnitsInStock`, `UnitsOnOrder`, `ReorderLevel`, `Discontinued`) 
                        VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice,
                            @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ProductName", prod.ProductName);
                    myCommand.Parameters.AddWithValue("@SupplierID", prod.SupplierID);
                    myCommand.Parameters.AddWithValue("@CategoryID", prod.CategoryID);
                    myCommand.Parameters.AddWithValue("@UnitPrice", prod.UnitPrice);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", prod.UnitsInStock);
                    myCommand.Parameters.AddWithValue("@unitsOnOrder", prod.UnitsOnOrder);
                    myCommand.Parameters.AddWithValue("@ReorderLevel", prod.ReorderLevel);
                    myCommand.Parameters.AddWithValue("@Discontinued", prod.Discontinued);
                    myCommand.Parameters.AddWithValue("@QuantityPerUnit", prod.QuantityPerUnit);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult("Added Success");
        }

        // Insert route for categories
        [Route("api/[controller]/categories")]
        [HttpPost]
        public JsonResult PostCategories(Categories prod)
        {
            string query = @"
                INSERT INTO `wsa_500`.`categories` 
                                (`CategoryID`, `CategoryName`, `Description`, `Picture`) 
                        VALUES (@CategoryID, @CategoryName, @Description, @Picture);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@CategoryID", prod.CategoryID);
                    myCommand.Parameters.AddWithValue("@CategoryName", prod.CategoryName);
                    myCommand.Parameters.AddWithValue("@Description", prod.Description);
                    myCommand.Parameters.AddWithValue("@Picture", prod.Picture);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult("Added Success");
        }

        //Put Routes:
        //Update Category
        // Insert route for categories
        [Route("api/[controller]/categories/update/{id}")]
        [HttpPut("{id}")]
        public JsonResult PutCategories(Categories prod, int id)
        {
            string query = @"
                UPDATE `wsa_500`.`categories` 
                    SET `CategoryName` = @CategoryName,
                        `Description` = @Description,
                        `Picture` = @Picture
                    WHERE (`CategoryID` = @CategoryID);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@CategoryID", id);
                    myCommand.Parameters.AddWithValue("@CategoryName", prod.CategoryName);
                    myCommand.Parameters.AddWithValue("@Description", prod.Description);
                    myCommand.Parameters.AddWithValue("@Picture", prod.Picture);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult("Added Success");
        }

        [Route("api/[controller]/products/update/{id}")]
        [HttpPut("{id}")]
        public JsonResult PutProduct(Products prod, int id)
        {
            string query = @"
                UPDATE `wsa_500`.`products` 
                    SET `ProductName` = @ProductName, 
                            `SupplierID` = @SupplierID, 
                            `CategoryID` = @CategoryID, 
                            `QuantityPerUnit` = @QuantityPerUnit, 
                            `UnitPrice` = @UnitPrice, 
                            `UnitsInStock` = @UnitsInStock, 
                            `UnitsOnOrder` = @UnitsOnOrder,
                            `ReorderLevel` = @ReorderLevel, 
                            `Discontinued` = @Discontinued
                    WHERE (`ProductID`=@ProductID);
            ";
  

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ProductName", prod.ProductName);
                    myCommand.Parameters.AddWithValue("@SupplierID", prod.SupplierID);
                    myCommand.Parameters.AddWithValue("@CategoryID", prod.CategoryID);
                    myCommand.Parameters.AddWithValue("@UnitPrice", prod.UnitPrice);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", prod.UnitsInStock);
                    myCommand.Parameters.AddWithValue("@unitsOnOrder", prod.UnitsOnOrder);
                    myCommand.Parameters.AddWithValue("@ReorderLevel", prod.ReorderLevel);
                    myCommand.Parameters.AddWithValue("@Discontinued", prod.Discontinued);
                    myCommand.Parameters.AddWithValue("@QuantityPerUnit", prod.QuantityPerUnit);
                    myCommand.Parameters.AddWithValue("@ProductID", id);
                    
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult("Added Success");
        }






        // Update only Picture for category
        [Route("api/[controller]/categories/update/picture/{id}")]
        [HttpPut("{id}")]
        public JsonResult PutCategoriesPhoto(Categories prod, int id)
        {
            string query = @"
                UPDATE `wsa_500`.`categories` 
                    SET `Picture` = @Picture
                    WHERE (`CategoryID` = @CategoryID);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Picture", prod.Picture);
                    myCommand.Parameters.AddWithValue("@CategoryID", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult("Added Success");
        }

        //Delete Route:
        //Delete Category Route
        [Route("api/[controller]/categories/delete/{id}")]
        [HttpDelete("{id}")]
        public JsonResult DeleteCategory(int id)
        {
            string query = @"
                delete from wsa_500.categories
                where CategoryID=@CategoryID;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@CategoryID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult("Deleted Succesfully");
        }

        //Delete Product Route
        [Route("api/[controller]/products/delete/{id}")]
        [HttpDelete("{id}")]
        public JsonResult DeleteProduct(int id)
        {
            string query = @"
                delete from wsa_500.products
                where ProductID=@ProductID;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WSA500AppCon");
            MySqlDataReader myReader;

            //filling the data we retrieve from our sql into our data table
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ProductID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            //Return data in json format
            return new JsonResult("Deleted Succesfully");
        }
    }
}
