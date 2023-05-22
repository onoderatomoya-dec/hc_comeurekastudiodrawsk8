using UnityEngine;

namespace DLMG.Sample
{
    public class LineDestroyer : MonoBehaviour
    {
        private void Update()
        {
            if (transform.position.y < -50)
            {
                Destroy(gameObject);
            }
        }
    }
}
