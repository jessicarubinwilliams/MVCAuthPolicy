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
  [Authorize]
  public class BooksController: Controller {
    private readonly LibraryContext _db;
    private readonly UserManager <ApplicationUser> _userManager;

    public BooksController(UserManager <ApplicationUser> userManager, LibraryContext db) {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index() {
      return View(_db.Books.ToList());
    }

    public ActionResult Create() {
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View();
    }

    [HttpPost]
    public async Task <ActionResult> Create(Book Book, int AuthorId) {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Book.User = currentUser;
      _db.Books.Add(Book);
      _db.SaveChanges();
      if (AuthorId != 0) {
        _db.AuthorBook.Add(new AuthorBook() {
          AuthorId = AuthorId, BookId = Book.BookId
        });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id) {
      var thisBook = _db.Books
        .Include(Book => Book.JoinEntities)
        .ThenInclude(join => join.Author)
        .FirstOrDefault(Book => Book.BookId == id);
      return View(thisBook);
    }

    public ActionResult Edit(int id) {
      var thisBook = _db.Books.FirstOrDefault(Book => Book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book Book, int AuthorId) {
      if (AuthorId != 0) {
        _db.AuthorBook.Add(new AuthorBook() {
          AuthorId = AuthorId, BookId = Book.BookId
        });
      }
      _db.Entry(Book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id) {
      var thisBook = _db.Books.FirstOrDefault(Book => Book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddAuthor(Book Book, int AuthorId) {
      if (AuthorId != 0) {
        _db.AuthorBook.Add(new AuthorBook() {
          AuthorId = AuthorId, BookId = Book.BookId
        });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id) {
      var thisBook = _db.Books.FirstOrDefault(Book => Book.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id) {
      var thisBook = _db.Books.FirstOrDefault(Book => Book.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId) {
      var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBook.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}