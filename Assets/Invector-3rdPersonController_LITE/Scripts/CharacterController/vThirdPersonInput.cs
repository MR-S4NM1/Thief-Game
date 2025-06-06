﻿using Mr_Sanmi.ThiefGame;
using System.Collections;
using TheDeveloperTrain.SciFiGuns;
using UnityEngine;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        #endregion


        [SerializeField] protected Transform _shotOrigin;
        [SerializeField] protected Transform _shotDirRef;
        [SerializeField] protected GameObject _plasmaObj;
        [SerializeField] protected Gun _gunCode;

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();

            _plasmaObj.SetActive(false);
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();      // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindFirstObjectByType<vThirdPersonCamera>(); /*FindObjectOfType<vThirdPersonCamera>();*/
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            if (!cc.isShooting)
            {
                MoveInput();
                CameraInput();
                SprintInput();
                StrafeInput();
                JumpInput();

                ShootInput(); //Miguel Elizalde
            }
        }

        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput) && (GameManager.instance.GetGameState() == GeneralGameStates.GAME))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions() && (GameManager.instance.GetGameState() == GeneralGameStates.GAME))
                cc.Jump();
        }

        //Written by Miguel Elizalde
        protected IEnumerator ShootRoutine()
        {
            yield return new WaitForSeconds(.35f);
            Ray ray = new Ray(_shotOrigin.position, (_shotDirRef.position - _shotOrigin.position).normalized);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, 20.0f))
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Disolve")) yield break;

                _plasmaObj.SetActive(true);
                print("AHHHHHHHHHHHHHHH ");
                GameManager.instance.CallDisolveCoroutine(hit.collider.gameObject);
                _gunCode.Shoot();
                AudioManager.instance.PlayAudio("Shoot");
                yield return new WaitForSeconds(.3f);
                _plasmaObj.SetActive(false);
            }
        }

        protected void ShootInput()
        {
            if (Input.GetMouseButtonDown(0) && (GameManager.instance.GetGameState() == GeneralGameStates.GAME))
            {
                cc.Shoot();
                StartCoroutine(ShootRoutine());
            }

            #endregion
        }
    }
}