using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");
var db = new DataContext();

string tester = "";
while (tester.ToLower() != "q")
{
    Console.Write("Enter your selection: \n 1) Display all blogs \n 2) Add blog \n 3) Create post \n 4) Display posts ");
  tester = Console.ReadLine();
  if (Convert.ToInt32(tester) == 1)
  {
    //display all blogs
    var query = db.Blogs.OrderBy(b => b.Name);

  Console.WriteLine("All blogs in the database:");
  foreach (var item in query)
  {
     Console.WriteLine(item.Name);
  }
  } else if (Convert.ToInt32(tester) == 2)
    {
        //add blog
    } else if (Convert.ToInt32(tester) == 3)
    {
        // create posts
    } else if (Convert.ToInt32(tester) == 4)
    {
        // display posts
    }
}
// Create and save a new Blog
Console.Write("Enter a name for a new Blog: ");
var name = Console.ReadLine();

var blog = new Blog { Name = name };
db.AddBlog(blog);

logger.Info("Blog added - {name}", name);

// Display all Blogs from the database

logger.Info("Program ended");