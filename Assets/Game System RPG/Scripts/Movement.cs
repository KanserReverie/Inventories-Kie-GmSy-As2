using UnityEngine;

namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        private float _gravity = 10.0f;
        private Vector3 _moveDir;
        private CharacterController _charC;

        private void Start()
        {
            _charC = GetComponent<CharacterController>();
        }
        
        private void Update()
        {
            Move();

            if (Input.GetKeyDown("q"))
            {
                Time.timeScale = 1;
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        private void Move()
        {
            Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_charC.isGrounded)
            {
                if (Input.GetButton("Crouch"))
                {
                    moveSpeed = runSpeed;
                }
                else
                {
                    if (Input.GetButton("Sprint"))
                    {
                        moveSpeed = crouchSpeed;
                    }
                    else if (!Input.GetButton("Sprint"))
                    {
                        moveSpeed = walkSpeed;
                    }
                }

                _moveDir = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * moveSpeed);
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpSpeed;
                }
            }
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }
    }
}