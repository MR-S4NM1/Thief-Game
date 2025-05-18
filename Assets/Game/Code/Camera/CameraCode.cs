using System.Collections;
using UnityEngine;

namespace Mr_Sanmi.ThiefGame
{
    public class CameraCode : MonoBehaviour
    {
        [SerializeField] protected Transform _avatarsTransform;
        [SerializeField] protected Transform _rayOrigin;
        RaycastHit _currentRaycastHit;

        private void Start()
        {
            _avatarsTransform = null;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Camouflage")
                || other.gameObject.layer == LayerMask.NameToLayer("Hologram"))
            {
                _avatarsTransform = other.gameObject.transform;
                StartCoroutine(RayDetectorCoroutine());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Camouflage")
                || other.gameObject.layer == LayerMask.NameToLayer("Hologram"))
            {
                _avatarsTransform = null;
                StopCoroutine(RayDetectorCoroutine());
            }
        }

        protected IEnumerator RayDetectorCoroutine()
        {
            yield return new WaitForSeconds(0.1f);

            if (_avatarsTransform != null)
            {
                if (Physics.Linecast(_rayOrigin.position, _avatarsTransform.position, out _currentRaycastHit))
                {
                    if (_currentRaycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Camouflage"))
                    {
                        StartCoroutine(RayDetectorCoroutine());
                    }
                    if (_currentRaycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Player") ||
                        _currentRaycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Hologram"))
                    {
                        //print("¡Ya te vi!");
                        _avatarsTransform = null;
                        GameManager.instance.ResetGame();
                        yield break;
                    }
                    StartCoroutine(RayDetectorCoroutine());
                }
            }
            else
            {
                yield break;
            }
        }
    }
}
