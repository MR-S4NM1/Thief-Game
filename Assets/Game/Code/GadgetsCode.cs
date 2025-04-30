using UnityEngine;

namespace Mr_Sanmi.Gadgets
{
    public enum Gadgets
    {
        NONE,
        ORIGINAL_MATERIALS,
        CAMOUFLAGE,
        HOLOGRAM
    }

    public class GadgetsCode : MonoBehaviour
    {
        #region References
        [SerializeField] protected SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] protected Material[] _originalMaterials;
        [SerializeField] protected Material _camouflageMaterial;
        [SerializeField] protected Material _hologramMaterial;
        #endregion

        #region RuntimeVariables
        [SerializeField] protected Gadgets _gadgetsState;
        #endregion

        private void Start()
        {
            _gadgetsState = Gadgets.ORIGINAL_MATERIALS;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
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

            if (Input.GetKeyDown(KeyCode.H))
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
    }
}
