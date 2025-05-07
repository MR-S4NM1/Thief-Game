using System.Collections;
using UnityEngine;

namespace Mr_Sanmi.ThiefGame
{
    public class DoorCode : MonoBehaviour
    {
        #region References
        [SerializeField] protected MeshRenderer _securityPanelMeshRenderer;
        [SerializeField] protected Material _securityPanelMaterialWhenActivated;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected BoxCollider _boxCollider;
        #endregion

        #region Knobs
        [SerializeField] protected int _doorID;
        #endregion

        #region RuntimeVariables
        [SerializeField] protected bool _doorHasBeenOpened;
        protected Coroutine _coroutine;
        #endregion
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void OpenDoor(int p_numberOfKeys)
        {
            if (_doorHasBeenOpened) return;

            if (p_numberOfKeys != _doorID) return;

            _coroutine = StartCoroutine(OpenDoorCoroutine());
        }

        protected IEnumerator OpenDoorCoroutine()
        {
            _doorHasBeenOpened = true;
            _animator.Play("OpenDoor");
            _securityPanelMeshRenderer.material = _securityPanelMaterialWhenActivated;
            yield return new WaitForSeconds(2.0f);
            _boxCollider.enabled = false;
        }
    }
}
