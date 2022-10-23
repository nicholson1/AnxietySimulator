using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LookAt : MonoBehaviour
{
   public GameObject target;

   public GameObject pointer;

   private bool isOn = false;

   public void SetTarget(GameObject obj)
   {

      target = obj;
      pointer.SetActive(true);
         
      
   }

   public void ClearTarget()
   {
      target = null;
      pointer.SetActive(false);
   }
   private void Update()
   {
      if (target != null)
      {
         transform.LookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z));
      }
      
   }
}
