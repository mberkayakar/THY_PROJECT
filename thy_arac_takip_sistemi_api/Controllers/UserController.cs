using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Models.ControllerObjects;
using OfficeOpenXml;
using thy_arac_takip_sistemi_api.Repo;

namespace thy_arac_takip_sistemi_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUser user;
        private readonly MyDbContext db;

        public UserController(IUser _user, MyDbContext _db, ILogger<UserController> logger)
        {
            _logger = logger;
            db = _db;
            user = _user;
        }
        //TODO Comment
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            _logger.LogInformation("Getting ALL User starting.");

            IQueryable<User> model = user.GetAllUsers;
            if (model == null)
            {
                _logger.LogError("Getting ALL User model is null!");

                return NotFound();
            }
            _logger.LogInformation("Getting ALL User finished.");

            return Ok(model);
        }
        [Route("login")]
        [HttpPost]
        public IActionResult LoginUser([FromBody] User _user)
        {
            _logger.LogInformation("LoginUser . User email : " + _user.email);
            if (_user == null)
            {
                _logger.LogError("LoginUser  failed. User email : " + _user.email);
                return NotFound();
            }

            User model = user.Login(_user.email);
            if (model != null)
            {
                _logger.LogInformation("LoginUser  OK. User email :" + _user.email);
                return Ok(model);

            }
            else
            {
                _logger.LogError("LoginUser maybe null  Failed.email :" + _user.email);

                return NotFound();
            }

        }
        [Route("create")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] User _user)
        {
            _logger.LogInformation("CreateUser . User email : " + _user.email);
            if (_user == null)
            {
                _logger.LogError("CreateUser  failed. User email : " + _user.email);
                return NotFound();
            }

            POJO model = user.CreateUser(_user);
            if (model != null)
            {
                _logger.LogInformation("CreateUser  OK. User email :" + _user.email);
                return Ok(model);

            }
            else
            {
                _logger.LogError("CreateUser  Failed.email :" + _user.email);

                return NotFound();
            }

        }
        [HttpPost]
        [Route("update/authority")]
        public IActionResult UpdateAuthority([FromBody] User _user)
        {
            _logger.LogInformation("UpdateAuthority id : " + _user.id);
            if (_user == null)
            {
                _logger.LogError("UpdateAuthority failed. id: " + _user.id);
                return NotFound();
            }

            POJO model = user.UpdateAuthority(user: _user);
            if (model.Flag == true)
            {
                _logger.LogInformation("UpdateAuthority  OK. id :" + _user.id);
                return Ok(model);

            }
            else
            {
                _logger.LogError("UpdateAuthority Failed pts OK.id :" + _user.id, "exception : " + model.Message);

                return NotFound(model);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(int? id)
        {
            _logger.LogInformation("Delete User From Id Starting");

            if (id == null || id == 0)
            {
                _logger.LogError("Delete User From Id Failed Id is Null or Equal 0");

                return NotFound();
            }
            POJO model = user.DeleteUser(id.Value);
            if (model.Flag == false)
            {
                _logger.LogError("Delete User From Id Failed id is " + id.Value + " Exception:" + model.Message);

                return NotFound(model);
            }
            _logger.LogInformation("Delete User From Id Success id is: " + id.Value);
            return Ok(model);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteUserEntries([FromBody] ListNumber idList)
        {
            if (idList == null || idList.idList == null)
            {
                return BadRequest();
            }
            POJO model = user.DeleteUsers(idList: idList.idList);
            if (model.Flag == true)
            {
                return Ok(model);
            }
            return NotFound(model);
        }


        //EXCEL FILE IMPORT
        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportUsersWithExcel(IFormFile file)
        {

            List<User> list = new List<User>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
                    var rowCount = workSheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        //unique ßemail constraint
                        if (db.Users.FirstOrDefault(e => e.email == workSheet.Cells[row, 2].Value.ToString().Trim()) == null)
                        {
                            System.Console.WriteLine(workSheet.Cells[row, 1].Value.ToString().Trim());
                            db.Users.Add(new User
                            {

                                userId = workSheet.Cells[row, 1].Value.ToString().Trim() ?? "",
                                email = workSheet.Cells[row, 2].Value.ToString().Trim() ?? "",
                                authority = workSheet.Cells[row, 3].Value.ToString().Trim() ?? "",
                                dateCreated = DateTime.Now,
                            });
                        }
                    }
                    db.SaveChanges();

                    return Ok("Eklendi");
                }

            }


        }
    }
}