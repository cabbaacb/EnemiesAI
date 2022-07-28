using System.Collections;
using UnityEngine;

namespace Ziggurat
{
    public class ActiveRadius : MonoBehaviour
    {
        public delegate void FightCollisionHandler(Collision collision);
        public static event FightCollisionHandler OnFightCollision;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != 8) return;
            if (collision.gameObject.GetComponent<UnitData>().Color == GetComponent<UnitData>().Color) return;
            OnFightCollision?.Invoke(collision);
            print("sphere");

        }
    }
}
