using Dynamic_WebForm_Generator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dynamic_WebForm_Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public FormController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        #region add new user
        [HttpPost ("user")]

        public IActionResult AddUser([FromForm] UsersModel user )
        {
            SqlDatabase db = new SqlDatabase(this.configuration.GetConnectionString("default"));
            DbCommand cmd = db.GetStoredProcCommand("API_Users_Insert");
            db.AddInParameter(cmd, "UserName", DbType.String, user.UserName);
            db.AddInParameter(cmd, "Email", DbType.String, user.Email);
            bool IsSuccess = Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true: false;
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();   
            if(IsSuccess == true)
            {
                response.Add("Status", true);
                response.Add("message", "user Added Successfully");
                return Ok(response);    
            }
            else
            {
                response.Add("Status", false);
                response.Add("message", "some error has occured");
                return Ok(response);
            }
        }
        #endregion
        #region Add new Form Field
        [HttpPost ("form_field")]
        public IActionResult AddFormFields([FromForm] FormModal fm)
        {
            SqlDatabase db = new SqlDatabase(this.configuration.GetConnectionString("default"));
            DbCommand cmd = db.GetStoredProcCommand("API_Forms_Insert");
            db.AddInParameter(cmd, "UserId", DbType.Int64, fm.userId);
            db.AddInParameter(cmd, "FieldName", DbType.String, fm.FieldName);
            db.AddInParameter(cmd, "FieldType", DbType.String, fm.FieldType);
            db.AddInParameter(cmd, "FieldLabel", DbType.String, fm.FieldLabel);
            db.AddInParameter(cmd, "Options", DbType.String, fm.Options);
            db.AddInParameter(cmd, "IsRequired", DbType.Int64, fm.isRequired);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            bool IsSuccess = Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            if (IsSuccess)
            {
                response.Add("Status", true);
                response.Add("message", "form field Added Successfully");
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("message", "some error has occured");
                return Ok(response);
            }


        }
        #endregion
        [HttpPost ("Submit")]

        public IActionResult Submit_Field([FromForm] SubmitValuesModal values) {
            SqlDatabase db = new SqlDatabase(this.configuration.GetConnectionString("default"));
            DbCommand cmd = db.GetStoredProcCommand("API_Submit_Forms_Insert");
            db.AddInParameter(cmd, "UserId", DbType.Int64, 1); // to be updated
            db.AddInParameter(cmd, "FormId", DbType.Int64, values.Form_id);
            db.AddInParameter(cmd, "FieldName", DbType.String, values.Field_name);
            db.AddInParameter(cmd, "values", DbType.String, values.Field_value);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            bool IsSuccess = Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            if (IsSuccess)
            {
                response.Add("Status", true);
                response.Add("message", "field value Added Successfully");
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("message", "some error has occured");
                return Ok(response);
            }
        }
    }

}
