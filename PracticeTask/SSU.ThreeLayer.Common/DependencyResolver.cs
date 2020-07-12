using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.DAL;

namespace SSU.ThreeLayer.Common
{
    public static class DependencyResolver
    {
        static private IShopAccess shopAccess;
        static private IUserAccess userAccess;
        static private IBusinessLogic businessLogic;
        
        static public IShopAccess ShopAccess { get => shopAccess ?? new ShopAccess(); }
        static public IUserAccess UserAccess { get => userAccess ?? new UserAccess(); }
        static public IBusinessLogic BusinessLogic { get => businessLogic ?? new BusinessLogic(ShopAccess, UserAccess); }
    }
}
