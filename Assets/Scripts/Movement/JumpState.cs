using UnityEngine;

namespace ZovRodini.Movement
{
    public class JumpState : StateManager
    {
        private float jumpForce;
        private float jumpTime = 0f;
        private float gravity;
        private Vector3 velocity;

        // ���������� ����������� ��� ����� AnimationCurve
        public JumpState(Character character, float jumpForce, float gravity) : base(character)
        {
            this.jumpForce = jumpForce;
            this.gravity = gravity;
        }

        public override void EnterState(Character character)
        {
            // ������������� ������� �������� ��� ������
            velocity = character.controller.velocity;
            velocity.y = jumpForce;
        }

        public override void UpdateState(Character character)
        {
            velocity.y += gravity * Time.deltaTime;
            character.controller.Move(velocity * Time.deltaTime);

            jumpTime += Time.deltaTime;

            // �������� �� ���������� ������
            if (character.controller.isGrounded)
            {
                character.controller.Move(velocity * Time.deltaTime);

                character.SetState(new IdleState(character)); // ������������ � IdleState ��� ������ ���������
                jumpTime = 0f; // ����� ������� ������
            }
            else
            {
                // ������� �������
                character.controller.Move(velocity * Time.deltaTime);
            }
        }
    }
}
