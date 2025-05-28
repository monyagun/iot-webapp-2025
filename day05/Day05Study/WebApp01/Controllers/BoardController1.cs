using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp01.Controllers
{
    public class BoardController1 : Controller
    {
        // GET: BoardController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: BoardController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BoardController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoardController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BoardController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BoardController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BoardController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BoardController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
