using Class.Domain;
using Class.Domain.Model.repositorioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcWebApplication.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: Authors
        public ActionResult Index()
        {
            var repositorio = new Class.Domain.Model.repositorioModel.RepositorioBancoAuthor();
            var Lista = repositorio.retornarTodosAuthor();
            return View(Lista);
        }
    }
}