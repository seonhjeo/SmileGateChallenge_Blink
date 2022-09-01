using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Item
{
    public class Sunglass : MonoBehaviour, IItem
    {
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 45f);
        }

        public void Use(GameObject target)
        {
            WorldController.Instance.IncreaseShadeTime(0.1f);
            Destroy(gameObject);
        }
    }
}
