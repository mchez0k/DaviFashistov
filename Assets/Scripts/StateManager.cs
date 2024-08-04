using UnityEngine;

namespace ZovRodini
{
    public abstract class StateManager
    {
        protected Character character;

        public StateManager(Character character) // TODO: Оптимизировать
        {
            this.character = character;
        }

        public virtual void EnterState(Character character)
        {
            character.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        public virtual void UpdateState(Character character)
        {

        }

        public virtual void ExitState(Character character)
        {

        }
    }
}

