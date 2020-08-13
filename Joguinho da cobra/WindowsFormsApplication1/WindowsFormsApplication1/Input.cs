using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Input
    {
        private static Hashtable keyTable = new Hashtable();

        public static bool keyPressed(Keys key) { if (keyTable[key] == null) { return false; } return (bool)keyTable[key]; }

        public static void changeState(Keys key, bool state) { keyTable[key] = state; }

    }
}
