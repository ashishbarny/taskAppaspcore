using Microsoft.AspNetCore.Mvc;

using taskApp.Domain.Entity;
using taskApp.Domain.Interfaces;
using taskApp.Domain.Services.Repositories;

namespace TaskManagementSystem.WebApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepository ;
        private readonly IProjectRepository _projectRepository ;
        private readonly IUserRepository _userRepository ;

        public TasksController(ITaskRepository taskRepository, IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository ;
            _taskRepository = taskRepository ;  
            _projectRepository = projectRepository ;
        }
        // GET: Tasks
        public ActionResult ListAll()
        {
            var tasks = _taskRepository.GetAll();

            return View(tasks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            fillViewBag();
            return View();
        }
        public void fillViewBag() {
            ViewBag.statuses = _taskRepository.GetStatuses();
            ViewBag.statLength = _taskRepository.GetStatuses().Count;
            ViewBag.types = _taskRepository.GetTypes();
            ViewBag.typeLength = _taskRepository.GetTypes().Count;
            ViewBag.projects = _projectRepository.GetAll();
            ViewBag.users = _userRepository.GetAll().Where(x => x.IsActivated == true);
        }

        [HttpPost]
        public ActionResult Create(taskApp.Domain.Entity.Task task)
        {
            fillViewBag();
            if (ModelState.IsValid)
            {
                if (_taskRepository.Create(task))
                    return RedirectToAction("ListAll");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            fillViewBag();
            try
            {
                var task = _taskRepository.GetById(id);
                return View(task);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(taskApp.Domain.Entity.Task task)
        {
            if (ModelState.IsValid)
            {
                if (_taskRepository.Update(task))
                {
                    return RedirectToAction("ListAll");
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var task = _taskRepository.GetById(id);

            return View(task);
        }

        [HttpPost]
        public ActionResult Delete(taskApp.Domain.Entity.Task task)
        {
            _projectRepository.DeleteTaskFromProject(task.ProjectId, task.ID);
            if (_taskRepository.Delete(task.ID))
                return RedirectToAction("ListAll");

            return View();
        }

    }
}