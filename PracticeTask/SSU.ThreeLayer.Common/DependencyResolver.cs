using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.DAL;

namespace SSU.ThreeLayer.Common
{
    public static class DependencyResolver
    {
        static private IDataAccess dataAccess;
        static private IBusinessLogic businessLogic;

        static public IDataAccess DataAccess { get => dataAccess ?? new DataAccess(); }
        static public IBusinessLogic BusinessLogic { get => businessLogic ?? new BusinessLogic(DataAccess); }
    }
}
