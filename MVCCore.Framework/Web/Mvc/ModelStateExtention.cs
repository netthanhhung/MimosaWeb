using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MVCCore.Framework.Web.Mvc
{
    public static class ModelStateExtention
    {
        public static void AddModelError<T>(this ModelStateDictionary modelstate, Expression<Func<T>> expression, string Error)
        {
            var member = (MemberExpression)expression.Body;
            string name =  member.Member.Name;
            modelstate.AddModelError(name, Error);
        }
    }
}
