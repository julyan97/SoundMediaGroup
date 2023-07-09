using System.Collections.Generic;

namespace WebApplication1.Models.PortfolioModels
{
    public class PortfolioOutputModel
    {
        public List<Video> MP4s { get; set; } = new  List<Video>();
        public List<Video> MP3s { get; set; } = new List<Video>();
    }

    public class Video
    { 
        public string Name { get; set; }
        public string Path { get; set; }
    }

}
