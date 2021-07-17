using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjective : MonoBehaviour
{
    public enum objectives
    {
        objectCollect,
        timeAttack
    }

    public objectives myObjective;

    public struct objectObjective
    {
        public int id; //colour
        public int required;
    }

    public List<objectObjective> objectObjectives = new List<objectObjective>();

    public float timeLimit = 120f;
}
