using System;


namespace MEAS.Data
{

    public  class UserProfileDao
    {
        public int Id { get; set; }

        public byte[] Avatar { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string EmailAddress { get; set; }

    }
}
