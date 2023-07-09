using System.Collections.Generic;
using System.IO;

namespace WebApplication1.Models.HomeModels
{
    public class HomeOutputModel
    {
        public Intro Intro { get; set; } = new Intro();
        public List<Content> Contents { get; set; } = new List<Content>();


    }

    public class Intro
    {
        public string Heading { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class Content
    {
        public int Id { get; set; } = default!;
        public string Img { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Text { get; set; } = default!;
        public FileInfo File { get; set; } = default!;
    }


}
