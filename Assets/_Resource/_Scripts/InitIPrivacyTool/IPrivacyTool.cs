using System.Threading.Tasks;
namespace PT
{
    public interface IPrivacyTool
    {
        Task Init(bool isOptIn);
        void OptIn();
        void OptOut();
    }
}