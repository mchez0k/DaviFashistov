using UnityEngine;

namespace ZovRodini.Movement
{
    public class WalkState : MovementState
    {
        private float speed;

        public WalkState(Character character, float walkSpeed) : base(character)
        {
            speed = walkSpeed;
        }

        public override void UpdateState(Character character)
        {
            Vector2 move = character.MoveInput;
            if (move.Equals(Vector3.zero))
            {
                character.SetState(new IdleState(character));
            }
            character.Move(move, speed);

            if (character.RunInput)
            {
                character.SetState(new RunState(character, character.RunSpeed));
            }
            if (character.JumpInput && character.controller.isGrounded)
            {
                character.SetState(new JumpState(character, 5f, -9.81f));
            }
            if (character.ProneInput)
            {
                character.SetState(new ProneState(character, character.ProneSpeed));
            }
            if (character.CrouchInput)
            {
                character.SetState(new CrouchState(character, character.CrouchSpeed));
            }
        }
    }
}
