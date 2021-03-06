﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebAppTDD.Data;
using WebAppTDD.Models;

namespace WebAppTDD.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Class> classesRepository;

        public HomeController(IRepository<Class> repository)
        {
            classesRepository = repository;
        }

        public ActionResult Index()
        {
            return View("Index", classesRepository.GetAll());
        }
        
        public ActionResult Details(int id)
        {
            try
            {
                return View("Details", classesRepository.GetById(id));
            } catch { 
                throw new HttpException(404, "Emae"); 
            }
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Class cl)
        {
            if (cl != null)
            {
                classesRepository.Add(cl);
                return RedirectToAction("Index");
            }
            else return View("Create");
                
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}