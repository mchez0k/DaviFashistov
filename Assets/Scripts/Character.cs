using UnityEngine;
using ZovRodini.Movement;

namespace ZovRodini
{
    public class Character : MonoBehaviour
    {
        internal bool isGrounded;
        [SerializeField] internal Transform WeaponHolder, WeaponBackpack;

        #region Rotation

        [Range(0.0f, 0.3f)]
        private float RotationSmoothTime = 0.12f;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;

        #endregion

        #region Movement

        [Header("Movement Var")]
        [Space(10)]
        [SerializeField] internal CharacterController controller;
        [SerializeField] private float RotationSpeed = 1.0f;
        internal float WalkSpeed = 2.5f;
        internal float RunSpeed = 5f;
        internal float CrouchSpeed = 1.5f;
        internal float ProneSpeed = 1.0f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private LayerMask aimColliderMask = new LayerMask();
        [SerializeField] internal Transform DebugTransform;

        private StateManager currentState;
        private float VerticalVelocity = -2f;

        #endregion

        #region Inputs

        internal Vector2 MoveInput;
        internal Vector2 LookInput;
        internal bool JumpInput;
        internal bool CrouchInput;
        internal bool ProneInput;
        internal bool ShootInput;
        internal bool AimInput;
        internal bool InteractInput;
        internal bool DropInput;
        internal bool RunInput;

        #endregion

        private void Start()
        {
            SetState(new IdleState(this));
        }

        private void Update()
        {
            if (ShootInput) Debug.Log("Shoot");
            currentState.UpdateState(this);
        }

        public void SetState(StateManager state)
        {
            currentState?.ExitState(this);
            currentState = state;
            currentState.EnterState(this);
        }

        public void Move(Vector2 direction, float speed)
        {
            if (controller.isGrounded && VerticalVelocity < 0)
            {
                VerticalVelocity = -2f; // Ќебольшое отрицательное значение, чтобы персонаж "прилипал" к земле
            }
            else
            {
                VerticalVelocity += gravity * Time.deltaTime; // ѕрименение гравитации
            }

            if (MoveInput != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            controller.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                             new Vector3(0.0f, VerticalVelocity, 0.0f) * Time.deltaTime);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}