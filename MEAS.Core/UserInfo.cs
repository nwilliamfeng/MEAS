
namespace MEAS
{
    public sealed class UserInfo:Entity
    {
      

        public string LoginName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        
        public string[] Roles { get; set; }
    }
}
