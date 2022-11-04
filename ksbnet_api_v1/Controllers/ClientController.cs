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
[Route("api/clients")]

public class ClientController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ClientController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult GetClients()
    {
        string query = @"select * from dbo.client";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("ksbnet_dataConn");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult(table);
    }
    [HttpGet("project/{slug}")]
    public JsonResult getProjectPartners(string slug)
    {
        string query = @"select c.id, c.client_name, c.client_logo_filename from client c
                        inner JOIN  
                            project_clients pc on c.id = pc.client_id
                        inner JOIN  
                            project p on pc.project_id = p.id
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
