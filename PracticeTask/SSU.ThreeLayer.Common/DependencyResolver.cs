using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.DAL;

namespace SSU.ThreeLayer.Common
{
    public static class DependencyResolver
    {
        static private IBaseDatabase baseDatabase;
        static private IDatabaseLogic databaseLogic;

        static public IBaseDatabase BaseDatabase { get => baseDatabase ?? new BaseDatabase(); }
        static public IDatabaseLogic DatabaseLogic { get => databaseLogic ?? new DatabaseLogic(BaseDatabase); }
    }
}
