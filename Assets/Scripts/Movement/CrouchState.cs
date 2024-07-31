using UnityEngine;

namespace ZovRodini.Movement
{
    public class CrouchState : MovementState
    {
        private float speed;

        public CrouchState(Character character, float crouchSpeed) : base(character)
        {
            speed = crouchSpeed;
        }

        public override void EnterState(Character character)
        {
            character.transform.localScale = new Vector3(1.0f, 0.7f, 1.0f);
        }

        public override void UpdateState(Character character)
        {
            Vector2 move = character.MoveInput;
            if (move.magnitude > 0.1f)
            {
                character.Move(move, speed);
            }

            if (character.JumpInput || !character.CrouchInput)
            {
                character.SetState(new IdleState(character));
            }
            if (character.RunInput)
            {
                character.SetState(new RunState(character, character.RunSpeed));
            }
            if (character.ProneInput)
            {
                character.SetState(new ProneState(character, character.ProneSpeed));
            }
        }
    }
}
