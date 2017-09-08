
namespace MEAS
{
    public sealed class UserInfo:Entity
    {
      

        public string LoginName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        
        public string[] Roles { get; set; }

        public byte[] Avatar { get; set; }       
 
        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string EmailAddress { get; set; }
    }
}
