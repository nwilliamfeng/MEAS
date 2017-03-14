

namespace MEAS.Models
{
    public interface IPagingInfo
    {
         int TotalItems { get; set; }

         int ItemsPerPage { get; set; }

         int CurrentPage { get; set; }

        int TotalPage { get; }
       
    }
}
