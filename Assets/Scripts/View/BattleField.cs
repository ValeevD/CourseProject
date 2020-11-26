using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    [SerializeField] private float circleRadius;

    public float GetRadius()
    {
        return circleRadius;
    }
}
