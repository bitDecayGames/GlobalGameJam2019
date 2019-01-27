using UnityEngine;

namespace PlayerScripts
{
    public class KnockOutPuncher : MonoBehaviour
    {
        private int intruderLayer = -1;

        void Start()
        {
            intruderLayer = LayerMask.NameToLayer("Intruder");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == intruderLayer)
            {
                var knockoutable = GetKnockoutable(other.transform);
                if (knockoutable)
                {
                    knockoutable.KnockOut();
                }
                else Debug.Log("Ran into something from the 'Intruder' layer that did not have a 'Knockoutable' component");
            }
        }

        private Knockoutable GetKnockoutable(Transform t)
        {
            if (!t.parent) return t.gameObject.GetComponentInChildren<Knockoutable>();
            return GetKnockoutable(t.parent);
        }
    }
}