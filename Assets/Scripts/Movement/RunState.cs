using UnityEngine;

namespace ZovRodini.Movement
{
    public class RunState : MovementState
    {
        private float speed;

        public RunState(Character character, float runSpeed) : base(character)
        {
            speed = runSpeed;
        }

        public override void UpdateState(Character character)
        {
            Vector2 move = character.MoveInput;
            character.Move(move, speed);

            // Проверка на возвращение к ходьбе
            if (!character.RunInput)
            {
                character.SetState(new WalkState(character, character.WalkSpeed));
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