using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Item
{
    public interface IItem
    {
        public void Use(GameObject target);
    }   
}
