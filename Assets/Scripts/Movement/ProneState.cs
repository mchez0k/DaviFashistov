using UnityEngine;

namespace ZovRodini.Movement
{
    public class ProneState : StateManager
    {
        private float speed;

        public override void EnterState(Character character)
        {
            character.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
        }

        public ProneState(Character character, float proneSpeed) : base(character)
        {
            speed = proneSpeed;
        }

        public override void UpdateState(Character character)
        {
            Vector2 move = character.MoveInput;
            if (move.magnitude > 0.1f)
            {
                character.Move(move, speed);
            }

            if (character.JumpInput || character.CrouchInput || character.ProneInput)
            {
                character.SetState(new CrouchState(character, character.CrouchSpeed));
            }
        }
    }
}
