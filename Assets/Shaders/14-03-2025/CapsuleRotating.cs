using System.Threading;
using UnityEngine;

public class CapsuleRotating : MonoBehaviour
{
    [SerializeField] protected float _rotationSpeed;
    [SerializeField] protected Vector3 _rotationDirection;

    void Update()
    {
        this.gameObject.transform.Rotate(_rotationDirection * _rotationSpeed * Time.deltaTime);
    }
}
