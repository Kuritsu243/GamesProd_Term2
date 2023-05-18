using Player.Health;
using UnityEngine;

namespace Environment
{
    public class DeathBox : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                other.transform.root.GetComponent<PlayerHealth>().Die();
        }
    }
}
