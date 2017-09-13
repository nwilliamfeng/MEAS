using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.Core.Objects;
 

namespace MEAS.Data.SqlClient
{
    public static class EntityFrameworkExtenion
    {
        public static ObjectStateManager ObjectStateManager(this DbContext dc)
        {
            return ((IObjectContextAdapter)dc).ObjectContext.ObjectStateManager;
        }


        public static void Dump(this DbEntityValidationException ex)
        {
            foreach (var validationErrors in ex.EntityValidationErrors) //打印错误
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    System.Console.WriteLine("Property: {0} Error: {1}" +
                                            validationError.PropertyName +
                                            validationError.ErrorMessage);
                }
            }
        }

    }
}
