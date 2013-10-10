using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.Loader
{
    class Loader
    {
        private static Loader loader;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Loader GetInstance()
        {
            if (loader == null)
            {
                loader = new Loader();
            }
            return loader;
        }

        private Loader()
        {
        }
    }
}
