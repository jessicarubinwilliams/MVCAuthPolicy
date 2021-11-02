using System.Collections.Generic;

namespace Library.Models {
  public class Book {
    public Book() {
      this.JoinEntities = new HashSet <AuthorBook> ();
    }

    public int BookId {get; set;}
    public string Description {get; set;}
    public virtual ApplicationUser User {get; set;}

    public virtual ICollection <AuthorBook> JoinEntities {
      get;
    }
  }
}