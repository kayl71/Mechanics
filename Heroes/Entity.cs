using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Folder.Heroes
{
    class Entity : _Object
    {
        protected int maxHp, maxMp;
        public int hp { protected set; get; }
        public int damage { protected set; get; }

        public int mp { protected set; get; }

        private Effect.EffectHandler effectHandler;

        public Effect.SideEffects sideEffects;

        protected Skill.SkillSet skillSet;

        public Entity(string name, int hp, int mp, int damage) : base(name) {
            SetHp(hp);
            SetMp(mp);
            this.damage = damage;
            effectHandler = new Effect.EffectHandler();
            skillSet = new Skill.SkillSet();
        }

        public override void Update() {
            effectHandler.Update(this);
            HpUpdate();
            MpUpdate();
            CheckHp();
        }

        public void AddSkill(Skill.Skill skill) {
            skillSet.AddSkill(skill);
        }

        public void UseSkill(Entity _object, int index) {
            skillSet.UseSkill(this, _object, index);
        }

        public virtual void Load(Entity entity) {
            name = entity.name;
            maxHp = entity.maxHp;
            hp = entity.hp;
        }

        public virtual void SetHp(int hp) {
            maxHp = hp;
            this.hp = hp;
        }
        public virtual void SetMp(int Mp)
        {
            maxMp = mp;
            this.mp = mp;
        }

        protected virtual void HpUpdate() {
            hp += sideEffects.dop_hpRegeneration + sideEffects.dop_hpDeegeneration;
        }

        protected virtual void MpUpdate() {
            mp += sideEffects.dop_mpRegeneration + sideEffects.dop_mpDeegeneration;
        }

        private void CheckHp() {
            int maxHp = GetMaxHp();
            if (hp > maxHp)
                hp = maxHp;
            else if (hp <= 0)
                DoAfterDie();
        }

        protected virtual void DoAfterDie() { 

        }
        public virtual void TakeDamage(int damage) {
            hp -= damage;
        }

        public void AddEffect(Effect.Effect effect) {
            effectHandler.AddEffect(effect, this);
        }

        public virtual int GetMaxHp() {
            return maxHp + sideEffects.dop_maxHp;
        }

        public virtual int GetMaxMp() {
            return maxMp + sideEffects.dop_maxMp;
        }

        public virtual int GetDamage()
        {
            return damage + sideEffects.dop_damage;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(name).Append(":\n");
            sb.Append("  MaxHp: ").Append(maxHp).Append('\n');
            sb.Append("  MaxMp: ").Append(maxMp).Append('\n');
            sb.Append("  Damage: ").Append(damage).Append('\n');

            return sb.ToString();
        }

        public virtual string ToStringAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(name).Append(":\n");
            sb.Append("  Hp: ").Append(hp).Append("/").Append(GetMaxHp()).Append('\n');
            sb.Append("  Mp: ").Append(mp).Append("/").Append(GetMaxMp()).Append('\n');
            sb.Append("  Damage: ").Append(GetDamage()).Append('\n');

            return sb.ToString();

        }
    }
}
