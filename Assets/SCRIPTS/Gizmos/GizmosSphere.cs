using UnityEngine;

public class GizmosSphere : MonoBehaviour
{

    public float _Size = .25f;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, .092f, .016f, .5f);
        Gizmos.DrawSphere(transform.position, _Size);
    }

}
