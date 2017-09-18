
namespace MEAS
{
    public   class UserInfo:Entity
    {
      

        public string LoginName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        
        public string[] Roles
        {
            get
            {
                return RoleString == null ? new string[] { } : RoleString.Split(',');
            }
        }

        public string RoleString { get; set; }

        public byte[] Avatar { get; set; }       
 
        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string EmailAddress { get; set; }
    }
}
