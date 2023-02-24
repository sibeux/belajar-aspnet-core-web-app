using System.Text.RegularExpressions;

namespace FirstApp.Course_Udemy.Routing.CustomConstraints
{
    public class MonthCustomConstraints : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // chech wether the value exist
            if (!values.ContainsKey(routeKey)) // month
            {
                return false; // return false if the value is not exist
            }

            Regex regex = new Regex("^(apr|jul|oct|jan)$");
            String? monthValue = Convert.ToString(values[routeKey]); // get the value of the month

            // check whether the value is valid
            if (regex.IsMatch(monthValue))
            {
                return true; // return true if the value is valid
            }
            return false; // return false if the value is not valid
        }
    }
}