using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Mvc;
using ksbnet_api_v1.Models;

namespace ksbnet_api_v1.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ProjectController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult GetAllProjects()
    {
        string query = @"select * from dbo.project";
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

    //[HttpGet("type/{project_type}")]
    //public JsonResult getProjectsByType(int project_type)
    //{
    //    string query = @"select * from dbo.project where project_type=@ProjectType";
    //    DataTable table = new DataTable();
    //    string sqlDataSource = _configuration.GetConnectionString("ksbnet_dataConn");
    //    SqlDataReader myReader;
    //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
    //    {
    //        myCon.Open();
    //        using (SqlCommand myCommand = new SqlCommand(query, myCon))
    //        {
    //            myCommand.Parameters.AddWithValue("@ProjectType", project_type);
    //            myReader = myCommand.ExecuteReader();
    //            table.Load(myReader);
    //            myReader.Close();
    //            myCon.Close();
    //        }
    //    }
    //    return new JsonResult(table);
    //}

    [HttpGet("slug/{slug}")]
    public JsonResult GetProjectBySlug(string slug)
    {
        string query = @"select * from dbo.project where slug =@Slug";
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