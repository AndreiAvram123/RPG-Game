using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Scheduler : MonoBehaviour
    {
        private IActionHandler curretAction;
        public void StartAction(IActionHandler action)
        {
            if (action == curretAction) return;

            if (curretAction != null)
            {
                curretAction.Cancel();
            }
            curretAction = action;
        }
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
