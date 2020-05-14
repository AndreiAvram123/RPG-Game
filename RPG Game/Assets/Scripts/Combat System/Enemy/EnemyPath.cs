using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class EnemyPath : MonoBehaviour
    {
        [SerializeField] Waypoint lastWaypoint; 
        
    

        public bool isPathValid()
        {
            List<Waypoint> path = GetPath();
            return path!=null && path.Count >= 2; 
        }

        public List<Waypoint> GetPath()
        {
            //create a reverse order path and then
            //order it 
            List<Waypoint> newPath = new List<Waypoint>();
            Waypoint current = lastWaypoint;
            newPath.Add(current);

            while (current.previousWaypoint != null
                &&current.previousWaypoint !=lastWaypoint)
            {
                newPath.Add(current.previousWaypoint);
                current = current.previousWaypoint;
                
            }
            
            //reorder the list
             newPath.Reverse();
             return newPath;
        }
    }
}
