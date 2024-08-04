using UnityEngine;

namespace ZovRodini.Movement
{
    public class IdleState : StateManager
    {
        public IdleState(Character character) : base(character)
        {

        }

        public override void UpdateState(Character character)
        {
            Vector2 move = character.MoveInput;

            if(!character.controller.isGrounded)
            {
                Vector3 gravity = new Vector3 (0, -9.81f * Time.deltaTime, 0);
                character.controller.Move(gravity);
            }
            if (move.magnitude > 0.1f)
            {
                character.SetState(new WalkState(character, character.WalkSpeed));
            }
            if (character.JumpInput && character.controller.isGrounded)
            {
                character.SetState(new JumpState(character, 5f, -9.81f));
            }
            if (character.CrouchInput)
            {
                character.SetState(new CrouchState(character, character.CrouchSpeed));
            }
            if (character.ProneInput)
            {
                character.SetState(new ProneState(character, character.ProneSpeed));
            }
        }
    }
}