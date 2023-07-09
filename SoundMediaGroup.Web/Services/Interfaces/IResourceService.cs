using WebApplication1.Models.BioModels;
using WebApplication1.Models.ContactsModel;
using WebApplication1.Models.HomeModels;
using WebApplication1.Models.PortfolioModels;

namespace WebApplication1.Services.Interfaces
{
    public interface IResourceService
    {
        string GetHomeRoot();
        HomeOutputModel GetHomeInfo();
        PortfolioOutputModel GetPortfolioInfo();
        string GetPortfolioRoot();
        string GetRootContacts();
        ContactsOutputModel GetContactsInfo();
        BioOutputModel GetBioInfo();
        string GetRootBio();
    }
}