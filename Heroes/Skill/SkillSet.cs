using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Folder.Heroes.Skill
{
    class SkillSet
    {

        private List<Skill> list_skill;

        public SkillSet() {
            list_skill = new List<Skill>();
        }

        public void AddSkill(Skill skill) {
            list_skill.Add(skill);
        }

        public void UseSkill(Entity _subject, Entity _object, int index) {
            if (IsNormalIndex(index)) 
                list_skill[index].Use(_subject, _object);
        }

        public string GetSkillInformation(int index) {
            if(IsNormalIndex(index))
                return list_skill[index].ToString();
            return "";
        }

        public int GetSkillSetSize() {
            return list_skill.Count;
        }

        private bool IsNormalIndex(int index) {
            return index >= 0 && index < list_skill.Count;
        }
    }
}
