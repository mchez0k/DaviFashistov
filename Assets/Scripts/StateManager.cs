using UnityEngine;

namespace ZovRodini
{
    public abstract class StateManager
    {
        protected NaziAi character;

        public StateManager(NaziAi character) // TODO: Оптимизировать
        {
            this.character = character;
        }

        public virtual void EnterState(NaziAi character)
        {

        }

        public virtual void UpdateState(NaziAi character)
        {

        }

        public virtual void ExitState(NaziAi character)
        {

        }
    }
}

