using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.DAL;

namespace SSU.ThreeLayer.Common
{
    public static class DependencyResolver
    {
        static private IShopAccess shopAccess;
        static private IUserAccess userAccess;
        static private IDataValidator dataValidator;
        static private IShopBusinessLogic shopBusinessLogic;
        static private IUserBusinessLogic userBusinessLogic;

        static public IShopAccess ShopAccess { get => shopAccess ?? new ShopAccess(); }
        static public IUserAccess UserAccess { get => userAccess ?? new UserAccess(); }
        static public IDataValidator DataValidator { get => dataValidator ?? new DataValidator(); }
        static public IShopBusinessLogic ShopBusinessLogic { get => shopBusinessLogic ?? new ShopBusinessLogic(ShopAccess, DataValidator); }
        static public IUserBusinessLogic UserBusinessLogic { get => userBusinessLogic ?? new UserBusinessLogic(UserAccess, DataValidator); }
    }
}
