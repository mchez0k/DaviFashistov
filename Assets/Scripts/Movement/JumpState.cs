using UnityEngine;

namespace ZovRodini.Movement
{
    public class JumpState : StateManager
    {
        private float jumpForce;
        private float jumpTime = 0f;
        private float gravity;
        private Vector3 velocity;

        // Обновлённый конструктор для приёма AnimationCurve
        public JumpState(Character character, float jumpForce, float gravity) : base(character)
        {
            this.jumpForce = jumpForce;
            this.gravity = gravity;
        }

        public override void EnterState(Character character)
        {
            // Инициализация вектора скорости для прыжка
            velocity = character.controller.velocity;
            velocity.y = jumpForce;
        }

        public override void UpdateState(Character character)
        {
            velocity.y += gravity * Time.deltaTime;
            character.controller.Move(velocity * Time.deltaTime);

            jumpTime += Time.deltaTime;

            // Проверка на завершение прыжка
            if (character.controller.isGrounded)
            {
                character.controller.Move(velocity * Time.deltaTime);

                character.SetState(new IdleState(character)); // Возвращаемся в IdleState или другое состояние
                jumpTime = 0f; // Сброс времени прыжка
            }
            else
            {
                // Обычное падение
                character.controller.Move(velocity * Time.deltaTime);
            }
        }
    }
}
