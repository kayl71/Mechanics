using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Folder.Heroes.Effect
{
    abstract class Effect
    {

        public ulong duration { protected set; get; }

        public bool dontUpdater { protected set; get; }

        public Effect(ulong duration, bool dontUpdater = false) {
            this.duration = duration;
            this.dontUpdater = dontUpdater;
        }

        public abstract void Start(Entity entity);

        public abstract void Update(Entity entity);

        public abstract void End(Entity entity);

        public abstract void Stop(Entity entity); // early completion
    }

    struct SideEffects
    {
        public int dop_maxHp;
        public int dop_maxMp;
        public int dop_damage;
        public int dop_hpRegeneration, dop_hpDeegeneration;
        public int dop_mpRegeneration, dop_mpDeegeneration;

    }
}
