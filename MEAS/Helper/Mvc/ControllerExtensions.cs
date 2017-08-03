using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEAS
{
    /// <summary>
    ///
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// 从Controller的ErrorStates返回所有的错误状态信息
        /// </summary>
        /// <param name="controller">指定的控制器</param>
        /// <see cref="https://stackoverflow.com/questions/2845852/asp-net-mvc-how-to-convert-modelstate-errors-to-json"/>
        /// <returns></returns>
        public static IEnumerable<string> ErrorsFromErrorState(this Controller controller)
        {
            return controller.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
        }


        /// <summary>
        /// 返回指定的view是否存在
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">指定的View名称</param>
        /// <see cref="https://stackoverflow.com/questions/946990/does-a-view-exist-in-asp-net-mvc"/>
        /// <returns></returns>
        public static bool ViewExists(this Controller controller, string viewName)
        {
          
            ViewEngineResult result = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null);
            return (result.View != null);
        }
    }
}