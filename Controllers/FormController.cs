using Dynamic_WebForm_Generator.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using BCrypt.Net;

namespace Dynamic_WebForm_Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
       
        private readonly IConfiguration configuration;

        public object BCrypt { get; private set; }

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

        #region Login User
        [HttpPost("Login_User")]
        public IActionResult Login([FromForm] UsersModel existing_user)
        {
            UsersModel credentials = new UsersModel();
            SqlDatabase db = new SqlDatabase (this.configuration.GetConnectionString("default"));
            DbCommand cmd = db.GetStoredProcCommand("API_Users_Login");
            db.AddInParameter(cmd, "@Email",DbType.String, existing_user.Email);
            db.AddInParameter(cmd, "@Password", DbType.String, existing_user.Password);
            IDataReader rd = db.ExecuteReader(cmd);
            if (rd != null)
            {
                while (rd.Read())
                {
                    credentials.UserID = (int)rd["UserID"];
                    credentials.UserName = rd["UserName"].ToString();
                    credentials.Email = rd["Email"].ToString();
                }
            }
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (credentials.Email != null)
            {
                response.Add("Status", true);
                response.Add("Message", "User Is Logged In Successfully..");
                response.Add("UserName", credentials.UserName);
                response.Add("Email", credentials.Email);
                response.Add("UserID", credentials.UserID);
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured..");
                response.Add("UserName", "");
                response.Add("Email", "");
                response.Add("UserID", "");
                return Ok(response);
            }

        }
        #endregion

        #region Sign Up User
       
        [HttpPost("signup")]
        [Consumes("application/json")]
        public IActionResult SignUp([FromBody] UsersModel newUser)
        {
            SqlDatabase db = new SqlDatabase(this.configuration.GetConnectionString("default"));
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("API_Users_SignUp");
                db.AddInParameter(cmd, "@UserName", DbType.String, newUser.UserName);
                db.AddInParameter(cmd, "@Email", DbType.String, newUser.Email);
                db.AddInParameter(cmd, "@Password", DbType.String, newUser.Password);

                db.ExecuteNonQuery(cmd);

                response.Add("Status", true);
                response.Add("message", "User registered successfully");
                return Ok(response);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("USERNAME_EXISTS"))
                {
                    response.Add("Status", false);
                    response.Add("message", "Username already exists");
                    return Ok(response);
                }
                else if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    response.Add("Status", false);
                    response.Add("message", "Email already exists");
                    return Ok(response);
                }
                else if (ex.Number == 2627) // Unique constraint violation
                {
                    response.Add("Status", false);
                    response.Add("message", "Username or Email already exists");
                    return Ok(response);
                }
            }

            response.Add("Status", false);
            response.Add("message", "Registration failed");
            return Ok(response);
        }
        #endregion


        #region Submit User Form
        [HttpPost("User_Form_Submit")]
        public IActionResult UserForm([FromForm] UserFormModel form_values)
        {
            SqlDatabase db = new SqlDatabase(this.configuration.GetConnectionString("default"));
            DbCommand cmd = db.GetStoredProcCommand("API_UserForms_Insert");
            db.AddInParameter(cmd, "UserId", DbType.Int64, form_values.UserID); // to be updated
            db.AddInParameter(cmd, "FieldName", DbType.String, form_values.FieldName);
            db.AddInParameter(cmd, "FieldType", DbType.String, form_values.FieldType);
            db.AddInParameter(cmd, "FieldLabel", DbType.String, form_values.FieldLabel);
            db.AddInParameter(cmd, "Options", DbType.String, form_values.Options);
            db.AddInParameter(cmd, "IsRequired", DbType.Int64, form_values.IsRequired);
            db.AddInParameter(cmd, "FieldValue", DbType.String, form_values.FieldValue);


            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            bool IsSuccess = Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            if (IsSuccess)
            {
                response.Add("Status", true);
                response.Add("message", "fields and its values Added Successfully");
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

        #region Save Form in database
        [HttpPost("Save_Form")]
        public IActionResult SaveForm([FromForm] FormHTMLModel html)
        {
            SqlDatabase db = new SqlDatabase(this.configuration.GetConnectionString("default"));
            DbCommand cmd = db.GetStoredProcCommand("API_Insert_FormHTML");
            db.AddInParameter(cmd, "@UserID", DbType.Int64, html.UserID);
            db.AddInParameter(cmd, "@FormHTML", DbType.String, html.FormHTML);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            bool IsSuccess = Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            if (IsSuccess)
            {
                response.Add("Status", true);
                response.Add("message", "html Added Successfully");
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

        #region Get All Forms Of Specific User
        [HttpGet("{UserID}")]
        public IActionResult GetAllForms(int UserID)
        {
            List<FormHTMLModel> html_list = new List<FormHTMLModel>();
            SqlDatabase db = new SqlDatabase(this.configuration.GetConnectionString("default"));
            DbCommand cmd = db.GetStoredProcCommand("API_Forms_GetAll");
            db.AddInParameter(cmd, "@UserID", DbType.Int64, UserID);
            IDataReader rd = db.ExecuteReader(cmd);
            if (rd != null) 
            {
                while (rd.Read()) 
                {
                    FormHTMLModel html = new FormHTMLModel();
                    html.UserID = (int)rd["UserID"];
                    html.FormHTML = rd["FormHTML"].ToString();
                    html.FormID = (int)rd["FormID"];
                    html_list.Add(html);
                }
            }
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (html_list.Count > 0)
            {
                response.Add("Status", true);
                response.Add("message", "form retrieved Successfully");
                response.Add("Form", html_list);

                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("message", "some error has occured");
                response.Add("Form", null);

                return Ok(response);
            }
        }
        #endregion

        #region Update Form
        [HttpPut]
        public IActionResult UpdateValue([FromForm] FormHTMLModel updated_form)
        {
            SqlDatabase db = new SqlDatabase(this.configuration.GetConnectionString("Default"));
            DbCommand cmd = db.GetStoredProcCommand("API_FormHTML_Update");
            db.AddInParameter(cmd, "@UserID", DbType.Int64, updated_form.UserID);
            db.AddInParameter(cmd, "@FormID", DbType.Int64, updated_form.FormID);
            db.AddInParameter(cmd, "@FormHTML",DbType.String, updated_form.FormHTML);
            bool IsSuccess = Convert.ToBoolean(db.ExecuteNonQuery(cmd)) ? true : false;
            Dictionary<string,dynamic> response = new Dictionary<string,dynamic>();
            if (IsSuccess)
            {
                response.Add("Status", true);
                response.Add("message", "fields and its values updated Successfully");
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
    }

}
