using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{

    public class PortalPlatform : MonoBehaviour
    {
        public GameObject target;
        //public char jump;

        private void OnTriggerEnter2D(Collider2D other)
        {
            //if (jump == 'f')
            //{
                if (other.tag == "Player")
                {
                    //target.GetComponent<PortalPlatform>().jump = 't';
                    other.gameObject.transform.position = new Vector2(target.transform.position.x, target.transform.position.y - 2);
                }
            //}
        }

       //private void OnTriggerExit2D(Collider2D other)
       //{
       //    if (other.tag == "Player")
       //    {
       //        jump = 'f';
       //    }
       //}
    }
}
