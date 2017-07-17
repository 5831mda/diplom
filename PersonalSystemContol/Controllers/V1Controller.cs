
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Linq;

using PersonalSystemContol.DataBase;
using PersonalSystemContol.JSONAdapters;
using PersonalSystemContol.Models;
using PersonalSystemContol.Responses;

namespace PersonalSystemContol.Controllers
{
    /// <summary>
    /// Контроллер API версии 1.
    /// </summary>
    [Route("api/[controller]")]
    public class V1Controller : Controller
    {
        private DataBase.BaseContext _db;
        public V1Controller(DataBase.BaseContext context)
        {
            this._db = context;
        }

        /// <summary>
        /// Добавление инженера.
        /// </summary>
        /// <param name="engineer">Инженер</param>
        /// <returns></returns>
        [HttpPost("engineers")]
        public IActionResult AddEngineer([FromBody] Engineer engineer)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            try
            {
                if (engineer == null)
                {
                    throw new Exception("Can not get the engineer parameters");
                }
                if (engineer.FullName.Length == 0 )
                {
                    throw new Exception("full name not found");
                }
                _db.Engineers.Add(engineer);
                _db.SaveChanges();
                Response.StatusCode = 200;
                return Json(new StandartResponse("Created", engineer.Id));
            }
            catch ( Exception exception )
            {
                Response.StatusCode = 404;
                return Json(new ErrorResponse("Error",exception.Message));
            }
        }

        /// <summary>
        /// Добавление задачи для инженера.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <returns></returns>
        [HttpPost("tasks")]
        public IActionResult AddTask([FromBody] Task task)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            try
            {
                if ( task == null )
                {
                    throw new Exception("Can not get the task parameters");
                }
                if ( !_db.Engineers.Any(e => e.Id==task.EngineerId) )
                {
                    throw new Exception("engineer not found");
                }
                if ((task.Name.Length == 0 ))
                {
                    throw new Exception("missing required parameter");
                }
                if (task.StartTime == DateTime.MinValue)
                {
                    task.StartTime = DateTime.Now;
                }
                _db.Tasks.Add(task);
                _db.SaveChanges();
                Response.StatusCode = 200;
                return Json(new StandartResponse("Created", task.Id));
            }
            catch ( Exception exception )
            {
                Response.StatusCode = 404;
                return Json(new ErrorResponse("Error", exception.Message));
            }
            
        }

        /// <summary>
        /// Получение задач для инженера.
        /// </summary>
        /// <param name="id">Идентификатор инженера</param>
        /// <returns></returns>
        [HttpGet("engineers/{id}/tasks")]
        public IActionResult GetTasks(int id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            try
            {
                Engineer engineer = _db.Engineers.Include(en => en.Tasks).ThenInclude(tas=>tas.Reports).FirstOrDefault(en => en.Id == id);
                if (engineer == null)
                {
                    throw new Exception("engineer not found");
                }
                if ( engineer.Tasks == null )
                {
                    engineer.Tasks = new List<Task>();
                }
                Response.StatusCode = 200;
                return Json(engineer.Tasks.Where(t=>!t.Reports.Any()));
            }
            catch ( Exception exception )
            {
                Response.StatusCode = 404;
                return Json(new ErrorResponse("Error",exception.Message));
            }
            
        }


        /// <summary>
        /// Завершение задания.
        /// </summary>
        /// <param name="adaptedReport">Данные отчета</param>
        /// <param name="taskId">Идентификатор задания</param>
        /// <returns></returns>
        [HttpPost("tasks/{taskId}/finish")]
        public IActionResult FinishTask([FromBody] FinishTaskAdapter adaptedReport, int taskId)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            try
            {
                Task task = _db.Tasks.FirstOrDefault(t => t.Id == taskId);
                if ( task == null )
                {
                    throw new Exception("task not found");
                }
                Report newReport = adaptedReport.GetReport();
                newReport.TaskId = taskId;
                _db.Reports.Add(newReport);
                _db.SaveChanges();
                if ( task.PhotoRequired == true )
                {
                    if ( adaptedReport.Link.Length == 0 )
                    {
                        throw new Exception("need photo link");
                    }
                    Photo photo = adaptedReport.GetPhoto(newReport.Id);
                    _db.Photos.Add(photo);
                    _db.SaveChanges();
                }
                _db.Update(task);
                _db.SaveChanges();
                Response.StatusCode = 200;
                return Json(new StandartResponse("finished", task.Id));
            }
            catch ( Exception exception )
            {
                Response.StatusCode = 404;
                return Json(new ErrorResponse("Error",exception.Message));
            }
        }

        /// <summary>
        /// Переделка задания.
        /// </summary>
        /// <returns></returns>
        [HttpPost("tasks/{taskId}/remake")]
        public IActionResult RemakeTask(int taskId)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            try
            {
                Task task = _db.Tasks.FirstOrDefault(t => t.Id == taskId);
                if (task == null)
                {
                    throw new Exception("task not found");
                }
                _db.Update(task);
                _db.SaveChanges();
                Response.StatusCode = 200;
                return Json(new StandartResponse("remaked", task.Id));
            }
            catch (Exception exception)
            {
                Response.StatusCode = 404;
                return Json(new ErrorResponse("Error", exception.Message));
            }
        }

        /// <summary>
        /// Получение списка всех задач за указанный период .
        /// </summary>
        /// <param name="period_start">Начало периода</param>
        /// <param name="period_end">Конец периода</param>
        /// <returns></returns>
        [HttpGet("tasks/period")]
        public IActionResult GetTasksByPeriod(DateTime period_start, DateTime period_end)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            try
            {
                IQueryable<Task> tasks = _db.Tasks.Where(t => t.StartTime >= period_start)
                        .Include(t => t.Reports)
                        .ThenInclude(r => r.Photos)
                        .Where(t => (t.Reports.Count == 0) || (!t.Reports.Any(r => r.EndTime > period_end)));
                var outTasks = new List<Task>();
                foreach (Task task in tasks)
                {
                    if (task.Reports != null)
                    {
                        outTasks.Add(task);
                    }
                }
                Response.StatusCode = 200;
                return Json(outTasks);
            }
            catch ( Exception exception )
            {
                Response.StatusCode = 404;
                return Json(new ErrorResponse("Error",exception.Message));
            }
        }

        /// <summary>
        /// Получение документации о текущей версии API.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDocumentation()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            return Json(new Documentation.V1Documentation());
        }
    }
}
