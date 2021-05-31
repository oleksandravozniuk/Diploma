using DatabaseAccess;
using Microsoft.EntityFrameworkCore;


namespace ProblemManager
{
    public static class DbAccessor
    {
        public static void RubDb()
        {
            using (var db = new DataContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
