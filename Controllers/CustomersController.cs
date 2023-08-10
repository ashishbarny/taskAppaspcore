using Microsoft.AspNetCore.Mvc;

using taskApp.Domain.Entity;
using taskApp.Domain.Interfaces;
using taskApp.Domain.Services.Repositories;

namespace TaskManagementSystem.WebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController() { 
            _customerRepository = new CustomerRepository();

        }
        // GET: Projects
        public ActionResult ListAll()
        {
            var customers = _customerRepository.GetAll();

            return View(customers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (_customerRepository.Create(customer))
                    return RedirectToAction("ListAll");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var customer = _customerRepository.GetById(id);
                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (_customerRepository.Update(customer))
                {
                    return RedirectToAction("ListAll");
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var customer = _customerRepository.GetById(id);

            return View(customer);
        }

        [HttpPost]
        public ActionResult Delete(Customer customer)
        {
            if (_customerRepository.Delete(customer.ID))
                return RedirectToAction("ListAll");

            return View();
        }
    }
}