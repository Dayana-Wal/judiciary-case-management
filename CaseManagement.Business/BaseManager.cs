namespace CaseManagement.Business
{
    public class BaseManager
    {
         public static string NewUlid() { return Ulid.NewUlid().ToString(); }
    }
}
