using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    public List<Obstacle> obstacles = new List<Obstacle>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
