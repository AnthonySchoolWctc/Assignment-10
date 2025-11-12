using Microsoft.IdentityModel.Tokens;
using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
logger.Info("Program started");
var db = new DataContext();
int count = 0;
string tester = "";
while (!tester.Equals("q"))
{
  Console.Write("Enter your selection: \n 1) Display all blogs \n 2) Add blog \n 3) Create post \n 4) Display posts ");
  tester = Console.ReadLine();
  if (tester.Equals("q"))
  {
    break;
  }
  if (Convert.ToInt32(tester) == 1)
  {
    //display all blogs
    var querybyname = db.Blogs.OrderBy(b => b.Name);
    count = 0;

    foreach (var item in querybyname)
    {
      count++;
    }
    Console.WriteLine($"{count} blogs returned");
    foreach (var item in querybyname)
    {

      Console.WriteLine(item.Name);
    }
  }
  else if (Convert.ToInt32(tester) == 2)
  {
    //add blog
    Console.Write("Enter a name for a new Blog: ");
    var name = Console.ReadLine();
    if (name.IsNullOrEmpty())
    {
      logger.Error("Blog Name cannot be null or empty");
    }
    else
    {
      var blog = new Blog { Name = name };
      db.AddBlog(blog);

      logger.Info("Blog added - {name}", name);
    }


  }
  else if (Convert.ToInt32(tester) == 3)
  {
    // create posts
    Console.WriteLine("Please select the blog you wish to post to: ");
    var query = db.Blogs.OrderBy(b => b.BlogId);

    foreach (var item in query)
    {

      Console.WriteLine($"{item.BlogId}) {item.Name}");
    }
    var id = Console.ReadLine();
    var blogidchecker = 0;

    foreach (var item in query)
    {
      if (Convert.ToInt32(id) == item.BlogId)
      {
        blogidchecker = 1;
        break;
      }
    }
    if (blogidchecker == 1)
    {


      Console.WriteLine("Enter a name for a new Post: ");
      var name = Console.ReadLine();
      if (name.IsNullOrEmpty())
      {

        logger.Error("Post Name cannot be null or empty");
        break;
      }

      Console.WriteLine("Enter post content: ");
      var content = Console.ReadLine();

      var post = new Post { Title = name, Content = content, BlogId = Convert.ToInt32(id) };
      db.AddPost(post);





    }


  }
  else if (Convert.ToInt32(tester) == 4)
  {
    // display posts
    Console.WriteLine("Please select the blog you wish to see, 0 for all posts: ");
    var query = db.Blogs.OrderBy(b => b.BlogId);
    var maxblogId = 0;
    foreach (var item in query)
    {
      
      Console.WriteLine($"{item.BlogId}) {item.Name}");
    }
    var id = Console.ReadLine();
    if (Convert.ToInt32(id) < 0)
    {
      logger.Error("Blog Id cannot be negative");
    }
    else if (Convert.ToInt32(id) == 0)
    {
      var allposts = db.Posts.OrderBy(b => b.BlogId);
      foreach (var item in allposts)
      {
        Console.WriteLine($"{item.Blog.Name}: {item.Title}, {item.Content}");
      }


    }
    else if (Convert.ToInt32(id) != 0 )
    {
      var posts = db.Posts.Where(b => b.BlogId == Convert.ToInt32(id));

      foreach (var item in posts)
      {



        Console.WriteLine($"{item.Blog.Name}: {item.Title}, {item.Content}");
      }


    }
  }
  else
  {
    logger.Error("Blogid must be a valid number from the list");
        }

    
}
logger.Info("Program ended");