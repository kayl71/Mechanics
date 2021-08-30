using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Folder.Heroes.Effect
{
    class EffectHandler
    {
        private List<EffectNode> effects_update;
        private ListEffectOneAction effects_oneAction;
        protected static ulong timeCounter = 0;

        public EffectHandler() {
            effects_oneAction = new ListEffectOneAction();
            effects_update = new List<EffectNode>();
        }

        public void AddEffect(Effect effect, Entity entity) {
            effect.Start(entity);
            if (effect.dontUpdater)
                effects_oneAction.AddEffect(effect);
            else
                effects_update.Add(new EffectNode(effect));
            
        }

        public void Update(Entity entity) {
            timeCounter++;

            for (int i = 0; i < effects_update.Count; i++) { 
                effects_update[i].effect.Update(entity);
                if (timeCounter >= effects_update[i].GetTimeEffectEnd())
                {
                    effects_update[i].effect.End(entity);
                    effects_update.RemoveAt(i);
                }
            }

            effects_oneAction.Update(entity);
        }



        protected struct EffectNode {
            public Effect effect;
            public ulong timeStart;

            public EffectNode(Effect effect) {
                this.effect = effect;
                timeStart = timeCounter;
            }

            public ulong GetTimeEffectEnd() {
                return effect.duration + timeStart;
            }
        }

        protected class ListEffectOneAction {

            ChainEffect root_chainEffect;

            public ListEffectOneAction() {
            }

            public void Update(Entity entity) {
                if(root_chainEffect != null)
                    root_chainEffect = root_chainEffect.Update(entity);
            }

            public void AddEffect(Effect effect) {
                if (root_chainEffect == null)
                    root_chainEffect = new ChainEffect(effect);
                root_chainEffect.AddEffect(effect);
            }

            private class ChainEffect {
                private Stack<Effect> effects;

                public ChainEffect nextChainEffect;

                private ulong timeStartFirstEffect; // need for update

                public ChainEffect(Effect effect) {
                    effects = new Stack<Effect>();
                    effects.Push(effect);
                    timeStartFirstEffect = timeCounter;
                    nextChainEffect = null;
                }

                public ChainEffect Update(Entity entity) {
                    if (timeCounter >= effects.Peek().duration + timeStartFirstEffect) {
                        while (effects.Count > 0)
                            effects.Pop().End(entity);

                        if (nextChainEffect != null)
                            return nextChainEffect.Update(entity);
                        return null;
                    }
                    return this;
                }

                public ulong GetTimeEffectEnd() {
                    return timeStartFirstEffect + effects.Peek().duration;
                }

                public void AddEffect(Effect effect) {
                    ulong timeEndThisEffect = timeCounter + effect.duration,
                        timeEndThisChain = GetTimeEffectEnd();
                    
                    if (timeEndThisEffect > timeEndThisChain)
                    {
                        if (nextChainEffect == null)
                            nextChainEffect = new ChainEffect(effect);
                        else if (timeEndThisEffect < nextChainEffect.GetTimeEffectEnd()) {
                            ChainEffect newChain = new ChainEffect(effect);
                            newChain.nextChainEffect = nextChainEffect;
                            nextChainEffect = newChain;
                        }else
                            nextChainEffect.AddEffect(effect);
                    }
                    else if (timeEndThisEffect == timeEndThisChain)
                        effects.Push(effect);                    
                }
            }

        }

    }
}
