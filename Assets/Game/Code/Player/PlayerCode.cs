using System.Collections.Generic;
using UnityEngine;

namespace Mr_Sanmi.ThiefGame
{ 
    public enum Gadgets
    {
        NONE,
        ORIGINAL_MATERIALS,
        CAMOUFLAGE,
        HOLOGRAM
    }

    public class PlayerCode : MonoBehaviour
    {
        #region References

        [Header("Skinned Mesh Renderer")]
        [SerializeField] protected SkinnedMeshRenderer _skinnedMeshRenderer;

        [Header("Materials")]
        [SerializeField] protected Material[] _originalMaterials;
        [SerializeField] protected Material _camouflageMaterial;
        [SerializeField] protected Material _hologramMaterial;

        [Header("Command Input")]
        [SerializeField] protected KeyCode _pauseResumeGameInput = KeyCode.Escape;
        [SerializeField] protected KeyCode _hologramInput = KeyCode.H;
        [SerializeField] protected KeyCode _camouflageInput = KeyCode.C;
        [SerializeField] protected KeyCode _pickUpInput = KeyCode.F;
        #endregion

        #region RuntimeVariables
        [SerializeField] protected Gadgets _gadgetsState;
        protected HashSet<GameObject> _collectibles;
        protected bool _hasCollectedDiamond;
        #endregion

        #region UnityMethods

        private void Start()
        {
            InitializeGame();
        }
        void Update()
        {
            InputHandle();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Final"))
            {
                GameManager.instance.ChangeToWinState();
            }

            if (other.CompareTag("FinalRemind"))
            {
                _hasCollectedDiamond = _collectibles.Count >= 6;

                GameManager.instance.ActivateOrDeactivateFinalCollision(_hasCollectedDiamond);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Collectibles"))
            {
                if (Input.GetKey(_pickUpInput))
                {
                    other.gameObject.SetActive(false);
                    _collectibles.Add(other.gameObject); 
                    UIManager.instance.UpdateCollectibles(_collectibles.Count);
                    AudioManager.instance.PlayAudio("PickUp");
                }
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("StencilLayer1"))
            {
                if (Input.GetKey(_pickUpInput))
                {
                    other.gameObject.SetActive(false);
                    _collectibles.Add(other.gameObject);
                    UIManager.instance.CallUIFunction("SetDiamond");
                    AudioManager.instance.PlayAudio("PickUp");
                }
            }

            if (_collectibles.Count > 0)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Doors"))
                {
                    if (Input.GetKey(_pickUpInput))
                    {
                        other.GetComponent<DoorCode>().OpenDoor(_collectibles.Count);
                        AudioManager.instance.PlayAudio("Door");
                    }
                }
            }
        }
        #endregion

        #region LocalMethods

        protected virtual void InputHandle()
        {
            PauseResumeGameInput();
            HologramInput();
            CamouflageInput();
        }

        protected void InitializeGame()
        {
            _gadgetsState = Gadgets.ORIGINAL_MATERIALS;
            _collectibles = new HashSet<GameObject>();
            _hasCollectedDiamond = false;
        }
        protected void ChangeState(Gadgets p_state){
            if (p_state == _gadgetsState) return;

            switch (p_state)
            {
                case Gadgets.ORIGINAL_MATERIALS:
                    _gadgetsState = Gadgets.ORIGINAL_MATERIALS;
                    this.gameObject.layer = LayerMask.NameToLayer("Player");
                    ChangeToOriginalMaterials();
                    break;
                case Gadgets.CAMOUFLAGE:
                    _gadgetsState = Gadgets.CAMOUFLAGE;
                    this.gameObject.layer = LayerMask.NameToLayer("Camouflage");
                    AudioManager.instance.PlayAudio("Camouflage");
                    ChangeMaterial(_camouflageMaterial);
                    break;
                case Gadgets.HOLOGRAM:
                    _gadgetsState = Gadgets.HOLOGRAM;
                    this.gameObject.layer = LayerMask.NameToLayer("Hologram"); 
                    AudioManager.instance.PlayAudio("Hologram");
                    ChangeMaterial(_hologramMaterial);
                    break;
            }
        }

        protected void ChangeMaterial(Material p_material)
        {
            Material[] newMaterials = new Material[_skinnedMeshRenderer.materials.Length];
            for (int i = 0; i < newMaterials.Length; ++i)
            {
                newMaterials[i] = p_material;
            }
            _skinnedMeshRenderer.materials = newMaterials;
        }

        protected void ChangeToOriginalMaterials()
        {
            if (_originalMaterials.Length != _skinnedMeshRenderer.materials.Length) return;

            _skinnedMeshRenderer.materials = _originalMaterials;
        }
        #endregion

        #region Inputs

        protected void PauseResumeGameInput()
        {
            if (Input.GetKeyDown(_pauseResumeGameInput))
            {
                GameManager.instance.PauseResumeGame();
            }
        }

        protected void HologramInput()
        {
            if (Input.GetKeyDown(_hologramInput))
            {
                switch (_gadgetsState)
                {
                    case Gadgets.ORIGINAL_MATERIALS:
                    case Gadgets.CAMOUFLAGE:
                        ChangeState(Gadgets.HOLOGRAM);
                        break;
                    case Gadgets.HOLOGRAM:
                        ChangeState(Gadgets.ORIGINAL_MATERIALS);
                        break;
                }
            }
        }

        protected void CamouflageInput()
        {
            if (Input.GetKeyDown(_camouflageInput))
            {
                switch (_gadgetsState)
                {
                    case Gadgets.ORIGINAL_MATERIALS:
                    case Gadgets.HOLOGRAM:
                        ChangeState(Gadgets.CAMOUFLAGE);
                        break;
                    case Gadgets.CAMOUFLAGE:
                        ChangeState(Gadgets.ORIGINAL_MATERIALS);
                        break;
                }
            }
        }

        #endregion 

        #region GettersAndSetters

        #endregion 

    }
}
