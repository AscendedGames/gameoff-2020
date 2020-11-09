using UnityEngine;

class NPCBase : MonoBehaviour
{
    public virtual void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 9);
    }
}
