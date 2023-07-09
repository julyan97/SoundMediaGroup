using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using WebApplication1.Models.BioModels;
using WebApplication1.Models.ContactsModel;
using WebApplication1.Models.HomeModels;
using WebApplication1.Models.PortfolioModels;
using WebApplication1.Services.Interfaces;
using IResourceService = WebApplication1.Services.Interfaces.IResourceService;

namespace WebApplication1.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _context;
        private readonly string _root;
        private readonly string _rootHome;
        private readonly string _rootPortfolio;
        private readonly string _rootContacts;
        private readonly string _rootBio;

        public ResourceService(
              IWebHostEnvironment webHostEnvironment,
              IHttpContextAccessor context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;


            var lang = _context.HttpContext.Session.GetString("Language");
            _root = _webHostEnvironment.WebRootPath;
            _rootHome = Path.Combine(_root, "Resourses",lang, "Home");
            _rootPortfolio = Path.Combine(_root, "Resourses",lang, "Portfolio");
            _rootContacts = Path.Combine(_root, "Resourses",lang, "Contacts");
            _rootBio = Path.Combine(_root, "Resourses", lang, "Bio");
        }

        public string GetHomeRoot()
        {
            return _rootHome;
        }

        public string GetPortfolioRoot()
        {
            return _rootPortfolio;
        }

        public string GetRootContacts()
        {
            return _rootContacts;
        }

        public string GetRootBio()
        {
            return _rootBio;
        }

        public HomeOutputModel GetHomeInfo()
        {
            var currentDir = "Home";
            var map = GetDirectoryFilesMap(GetHomeRoot());
            var suffix = ExtractResoursePath(GetHomeRoot());

            var filesContents = map[currentDir]
                .Select(x => new FileInfo(Path.Combine(GetHomeRoot(), x)))
                .ToList();

            var outputModel = new HomeOutputModel();
            var header = filesContents.FirstOrDefault(x => x.Name.Contains("About Us") || x.Name.Contains("За Нас"));

            outputModel.Intro.Heading = Path.GetFileNameWithoutExtension(header.Name);
            outputModel.Intro.Description = System.IO.File.ReadAllText(header.FullName);
            foreach (var file in filesContents)
            {
                if (file.Name.Contains("About Us") || file.Name.Contains("За Нас"))
                    continue;

                var imgDir = Path.Combine(GetHomeRoot(), "Img");

                var img = Directory.GetFiles(imgDir, $"{Path.GetFileNameWithoutExtension(file.Name)}.*")?
                    .Select(x => new FileInfo(x))?
                    .FirstOrDefault();

                var imgSource = string.Empty;
                if (img != null)
                {
                    imgSource = Path.Combine(suffix, "Img", img.Name).Replace("\\", "/");
                }

                var txt = File.ReadAllText(file.FullName);
                outputModel.Contents.Add(new Content
                {
                    Img = imgSource,
                    File = file,
                    Name = Path.GetFileNameWithoutExtension(file.Name),
                    Text = txt
                });
            }

            return outputModel;
        }

        public PortfolioOutputModel GetPortfolioInfo()
        {
            var currentDir = "Portfolio";
            var map = GetDirectoryFilesMap(GetPortfolioRoot());
            var suffix = ExtractResoursePath(GetPortfolioRoot());

            var outputModel = new PortfolioOutputModel();

            var mp4s = Directory.GetFiles(GetPortfolioRoot() + "\\MP4s", "*.mp4")
                .Select(x => new FileInfo(x))
                .ToList();

            var mp3s = Directory.GetFiles(GetPortfolioRoot() + "\\MP3s", "*.mp3")
                .Select(x => new FileInfo(x))
                .ToList();

            outputModel.MP4s.AddRange(mp4s.Select(x => new Video
            {
                Name = Path.GetFileNameWithoutExtension(x.Name),
                Path = Path.Combine(suffix, "MP4s", x.Name).Replace("\\", "/")
            }));

            outputModel.MP3s.AddRange(mp3s.Select(x => new Video
            {
                Name = Path.GetFileNameWithoutExtension(x.Name),
                Path = Path.Combine(suffix, "MP3s", x.Name).Replace("\\", "/")
            }));

            return outputModel;
        }

        public ContactsOutputModel GetContactsInfo()
        {
            var currentDir = "Contacts";
            var map = GetDirectoryFilesMap(GetRootContacts());
            var suffix = ExtractResoursePath(GetRootContacts());

            var outputModel = new ContactsOutputModel();

            outputModel.Description = File.ReadAllText(Path.Combine(GetRootContacts(), "Description.txt"));
            outputModel.GoogleMapsScr = File.ReadAllText(Path.Combine(GetRootContacts(), "GoogleMapsAddress.txt"));

            return outputModel;

        }

        public BioOutputModel GetBioInfo()
        {
            var currentDir = "Bio";
            var map = GetDirectoryFilesMap(GetRootBio());
            var suffix = ExtractResoursePath(GetRootBio());

            var outputModel = new BioOutputModel();

            outputModel.Description = File.ReadAllText(Path.Combine(GetRootBio(), "Description.txt"));
            var filepath = Directory.GetFiles(GetRootBio(), "Profile.*").FirstOrDefault();
            outputModel.ProfilePhotoPath = Path.Combine(suffix, Path.GetFileName(filepath));
            outputModel.ProfilePhotoPath = outputModel.ProfilePhotoPath.Replace('\\', '/');

            return outputModel;

        }

        public static Dictionary<string, List<string>> GetDirectoryFilesMap(string rootDirectory)
        {
            Dictionary<string, List<string>> directoryFilesMap = new Dictionary<string, List<string>>();

            var directories = Directory.GetDirectories(rootDirectory, "*", SearchOption.AllDirectories);

            // Add root directory to the directories array
            string[] allDirectories = new string[directories.Length + 1];
            directories.CopyTo(allDirectories, 0);
            allDirectories[allDirectories.Length - 1] = rootDirectory;

            foreach (var directory in allDirectories)
            {
                var files = Directory.GetFiles(directory);
                var fileNames = new List<string>();
                foreach (var file in files)
                {
                    fileNames.Add(Path.GetFileName(file));
                }

                var key = directory == rootDirectory ? Path.GetFileName(directory) : Path.GetFileName(directory);
                directoryFilesMap.Add(key, fileNames);
            }

            return directoryFilesMap;
        }

        private string ExtractResoursePath(string fullPath)
        {
            string rootPath = _root;
            string remainingPath = string.Empty;

            if (fullPath.StartsWith(rootPath))
            {
                remainingPath = fullPath.Substring(rootPath.Length);
            }

            return remainingPath;
        }
    }
}
