using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            var cube = GetComponent<Cube>();
            cube.ChangeColor();
            cube.StartDeactivation();
        }
    }
}