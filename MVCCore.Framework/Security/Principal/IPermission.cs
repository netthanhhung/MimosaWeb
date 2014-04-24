namespace MVCCore.Framework.Security.Principal
{
    public interface IPermission
    {       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permission">permission list, comma separated. E.g: permision1,permission2</param>
        /// <returns></returns>
        bool HasPermission(string permission);
    }
}
