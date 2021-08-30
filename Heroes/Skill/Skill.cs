using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Folder.Heroes.Skill
{
    abstract class Skill
    {
        abstract protected void Failed(Entity _subject, Entity _object);
        abstract protected void Failed(Entity _subject, _Object _object);

        abstract protected void Used(Entity _subject, Entity _object);
        abstract protected void Used(Entity _subject, _Object _object);
        abstract protected bool TermUse(Entity _subject, Entity _object);
        abstract protected bool TermUse(Entity _subject, _Object _object);

        public void Use(Entity _subject, Entity _object)
        {
            if (TermUse(_subject, _object))
                Used(_subject, _object);
            else
                Failed(_subject, _object);
        }
        public void Use(Entity _subject, _Object _object)
        {
            if (TermUse(_subject, _object))
                Used(_subject, _object);
            else
                Failed(_subject, _object);
        }

    }
}
