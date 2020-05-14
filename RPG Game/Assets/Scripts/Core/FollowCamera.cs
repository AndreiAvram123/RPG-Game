using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        

        private void LateUpdate()
        {
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;

        }

    }

}
