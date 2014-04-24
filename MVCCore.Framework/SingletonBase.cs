using System;

namespace MVCCore.Framework
{
    public static class SingletonBase<T> where T : class   
    {
        #region Parameters

        // ReSharper disable StaticFieldInGenericType
        static volatile T _instance;
        // ReSharper restore StaticFieldInGenericType
        // ReSharper disable StaticFieldInGenericType
        static readonly object Lock = new object();
        // ReSharper restore StaticFieldInGenericType

        #endregion

        #region Constructors

        static SingletonBase()
        {

        }

        #endregion

        #region Get/Set

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = Activator.CreateInstance<T>();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion
       
    }
}
