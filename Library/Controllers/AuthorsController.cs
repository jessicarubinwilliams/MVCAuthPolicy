using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers {
  public class AuthorsController: Controller {
    private readonly LibraryContext _db;

    public AuthorsController(LibraryContext db) {
      _db = db;
    }

    [Authorize(Roles = "Librarian")]
    public ActionResult Index() {
      List <Author> model = _db.Authors.ToList();
      return View(model);
    }

    public ActionResult Create() {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Author Author) {
      _db.Authors.Add(Author);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id) {
      var thisAuthor = _db.Authors
        .Include(Author => Author.JoinEntities)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(Author => Author.AuthorId == id);
      return View(thisAuthor);
    }
    public ActionResult Edit(int id) {
      var thisAuthor = _db.Authors.FirstOrDefault(Author => Author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult Edit(Author Author) {
      _db.Entry(Author).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id) {
      var thisAuthor = _db.Authors.FirstOrDefault(Author => Author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id) {
      var thisAuthor = _db.Authors.FirstOrDefault(Author => Author.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}