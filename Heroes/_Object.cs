using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Folder.Heroes
{
    abstract class _Object
    {
        protected string name;

        public _Object(string name) {
            this.name = name;
        }

        public abstract void Update();
    }
}
