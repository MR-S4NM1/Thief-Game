using System.Collections.Generic;
using System.Net.Sockets;
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
        #endregion

        #region RuntimeVariables
        [SerializeField] protected Gadgets _gadgetsState;
        [SerializeField] protected List<GameObject> _collectibles;
        [SerializeField] protected bool _isInteractingWithACollectible;
        [SerializeField] protected int _numberOfKeys;
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

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Collectibles"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _collectibles.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                    Debug.Log($"Collectible: {other.gameObject.name}");
                    _numberOfKeys++;
                }
            }

            if(_numberOfKeys > 0)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Doors"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        other.GetComponent<DoorCode>().OpenDoor(_numberOfKeys);
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
        }
        protected void ChangeState(Gadgets p_state){
            if (p_state == _gadgetsState) return;

            switch (p_state)
            {
                case Gadgets.ORIGINAL_MATERIALS:
                    _gadgetsState = Gadgets.ORIGINAL_MATERIALS;
                    this.gameObject.layer = LayerMask.NameToLayer("Default");
                    ChangeToOriginalMaterials();
                    break;
                case Gadgets.CAMOUFLAGE:
                    _gadgetsState = Gadgets.CAMOUFLAGE;
                    this.gameObject.layer = LayerMask.NameToLayer("Camouflage");
                    ChangeMaterial(_camouflageMaterial);
                    break;
                case Gadgets.HOLOGRAM:
                    _gadgetsState = Gadgets.HOLOGRAM;
                    this.gameObject.layer = LayerMask.NameToLayer("Hologram");
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
        public int GetCollectiblesCount
        {
            get { return _collectibles.Count; }
        }
        #endregion 

    }
}
