using NpgsqlTypes;

namespace crm_core.Utils
{
    public class CrmFunctions
    {
        public static DateTime GetDateTime() => DateTime.UtcNow;
    }
}