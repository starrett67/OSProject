using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.ControlUnit
{
    class Dispatcher
    {
        private static Dispatcher dispatch;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Dispatcher GetInstance()
        {
            if (dispatch == null)
            {
                dispatch = new Dispatcher();
            }
            return dispatch;
        }

        private Dispatcher()
        {
        }
    }
}
