using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ziggurat
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Configurable Properties")]
        [Tooltip("This is the Y offset of our focal point. 0 Means we're looking at the ground.")]
        public float lookOffset;
        [Tooltip("The angle that we want the camera to be at.")]
        public float cameraAngle;
        [Tooltip("The default amount the player is zoomed into the game world.")]
        public float defaultZoom;
        [Tooltip("The most a player can zoom in to the game world.")]
        public float zoomMax;
        [Tooltip("The furthest point a player can zoom back from the game world.")]
        public float zoomMin;
        [Tooltip("How fast the camera moves")]
        public float movementSpeed;
        [Tooltip("How fast the camera rotates")]
        public float rotationSpeed;
        [Tooltip("How fast the camera zoomes")]
        public float zoomSpeed;

        //Camera specific variables
        private Camera _actualCamera;
        private Vector3 _cameraPositionTarget;

        //Movement variables
        private float InternalMoveTargetSpeed = 8;
        private float InternalMoveSpeed = 4;
        private Vector3 _moveTarget;
        private Vector3 _moveDirection;

        //Zoom variables
        private float _currentZoomAmount;
        public float CurrentZoom
        {
            get => _currentZoomAmount;
            private set
            {
                _currentZoomAmount = value;
                UpdateCameraTarget();
            }
        }
        private float _internalZoomSpeed = 4;

        //Rotation variables
        private bool _rotationButtonDown = false;
        private float InternalRotationSpeed = 4;
        private Quaternion _rotationTarget;
        private Vector2 _mouseDelta;

        private InputActions _input;

        private void Awake()
        {
            _input = new InputActions();
            _input.MainActionMap.CameraMovement.performed += OnMove;
            _input.MainActionMap.CameraMovement.canceled += OnMove;
            _input.MainActionMap.CameraRotate.performed += OnRotate;
            _input.MainActionMap.CameraRotate.canceled += OnRotate;
            _input.MainActionMap.CameraRotateToggle.performed += OnRotateToggle;
            _input.MainActionMap.CameraRotateToggle.canceled += OnRotateToggle;
            _input.MainActionMap.CameraZoom.performed += OnZoom;

        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        void Start()
        {
            //Store a reference to the camera rig
            _actualCamera = GetComponentInChildren<Camera>();
            //Set the rotation of the camera based on the CameraAngle property
            _actualCamera.transform.rotation = Quaternion.AngleAxis(cameraAngle, Vector3.right);
            //Set the position of the camera based on the look offset, angle and default zoom properties. 
            //This will make sure we're focusing on the right focal point.
            CurrentZoom = defaultZoom;
            _actualCamera.transform.position = _cameraPositionTarget;
            //Set the initial rotation value
            _rotationTarget = transform.rotation;
        }

        private void FixedUpdate()
        {
            //Sets the move target position based on the move direction. Must be done here 
            //as there's no logic for the input system to calculate holding down an input
            _moveTarget += (transform.forward * _moveDirection.z + transform.right * _moveDirection.x) * Time.fixedDeltaTime * InternalMoveTargetSpeed * movementSpeed;
        }

        private void LateUpdate()
        {
            //Lerp the camera rig to a new move target position
            transform.position = Vector3.Lerp(transform.position, _moveTarget, Time.deltaTime * InternalMoveSpeed);
            //Move the _actualCamera's local position based on the new zoom factor
            _actualCamera.transform.localPosition = Vector3.Lerp(_actualCamera.transform.localPosition, _cameraPositionTarget, Time.deltaTime * _internalZoomSpeed);
            //Set the target rotation based on the mouse delta position and our rotation speed
            _rotationTarget *= Quaternion.AngleAxis(_mouseDelta.x * Time.deltaTime * rotationSpeed * 10f, Vector3.up);
            //Slerp the camera rig's rotation based on the new target
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotationTarget, Time.deltaTime * InternalRotationSpeed);
        }

        /// <summary>
        /// Sets the direction of movement based on the input provided by the player
        /// </summary>
        private void OnMove(InputAction.CallbackContext context)
        {
            //Read the input value that is being sent by the Input System
            Vector2 value = context.ReadValue<Vector2>();
            //Store the value as a Vector3, making sure to move the Y input on the Z axis.
            _moveDirection = new Vector3(value.x, 0, value.y);
        }

        /// <summary>
        /// Calculates a new position based on various properties
        /// </summary>
        private void UpdateCameraTarget()
        {
            _cameraPositionTarget = (Vector3.up * lookOffset) + (Quaternion.AngleAxis(cameraAngle, Vector3.right) * Vector3.back) * _currentZoomAmount;
        }

        /// <summary>
        /// Sets the logic for zooming in and out of the level. Clamped to a min and max value.
        /// </summary>
        /// <param name="context"></param>
        private void OnZoom(InputAction.CallbackContext context)
        {
            // Adjust the current zoom value based on the direction of the scroll - this is clamped to our zoom min/max. 
            CurrentZoom = Mathf.Clamp(_currentZoomAmount - context.ReadValue<Vector2>().y, zoomMax, zoomMin);
        }

        /// <summary>
        /// Sets whether the player has the right mouse button down
        /// </summary>
        /// <param name="context"></param>
        private void OnRotateToggle(InputAction.CallbackContext context)
        {
            _rotationButtonDown = context.ReadValue<float>() == 1;
        }

        /// <summary>
        /// Sets the rotation target quaternion if the right mouse button is pushed when the player is 
        /// moving the mouse
        /// </summary>
        /// <param name="context"></param>
        private void OnRotate(InputAction.CallbackContext context)
        {
            // If the button is down then we'll read the mouse delta value. If it is not, we'll clear it out.
            // Note: Clearing the mouse delta prevents a 'death spin' 
            //from occurring if the player flings the mouse really fast in a direction.
            _mouseDelta = _rotationButtonDown ? context.ReadValue<Vector2>() : Vector2.zero;
        }
    }
}