using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ksbnet_api_v1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using ksbnet_api_v1.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ksbnet_api_v1.Controllers;

[ApiController]
[Route("api/functions")]

public class FunctionController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public FunctionController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //get all messages
    [HttpGet("project/{slug}")]
    public JsonResult GetFunctionBySlug(string slug)
    {
        string query = @"select f.id, f.function_name, f.function_description, f.function_image
                        from functions f
                        inner join project_functions pf on f.id = pf.function_id
                        inner join project p on pf.project_id = p.id
                        where p.slug = @Slug";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("ksbnet_dataConn");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@Slug", slug);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult(table);
    }
}

