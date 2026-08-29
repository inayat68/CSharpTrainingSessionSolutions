using System;
using System.Collections.Generic;
using System.Text;
// Ctrl + R + G to remove unnecessary Namespaces

namespace WebApis.Models;

    
public class Post
{
    public int UserId { get; set; }

    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;
}
