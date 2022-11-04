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
[Route("api/[controller]")]

public class ContactController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ContactController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //get all messages
    [HttpGet]
    public JsonResult Get()
    {
        string query = @"select * from dbo.contact_submission";
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
    //post new message
    [HttpPost]
    public JsonResult Post(Message mes)
    {
        string query = @"INSERT INTO dbo.contact_submission (name,email,subject,message) VALUES (
                @Name,@Email, @Subject,@Message
            )";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("ksbnet_dataConn");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@Name", mes.name);
                myCommand.Parameters.AddWithValue("@Email", mes.email);
                myCommand.Parameters.AddWithValue("@Subject", mes.subject);
                myCommand.Parameters.AddWithValue("@Message", mes.message);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult("Created Successfuly");
    }
}

