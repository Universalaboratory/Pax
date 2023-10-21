using UnityEngine;
public class ImageLoop : MonoBehaviour
{
    [SerializeField] private float _rotateVelocity;
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotateVelocity);
    }
}
