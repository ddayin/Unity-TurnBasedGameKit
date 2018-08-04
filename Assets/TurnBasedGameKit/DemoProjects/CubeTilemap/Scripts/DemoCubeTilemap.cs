using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    /// <summary>
    /// main class which will be called at first
    /// </summary>
    public class DemoCubeTilemap : MonoBehaviour
    {
        private void Awake()
        {
            // It's important to call method in order
            ResourcesLoader.LoadAll();

            TileFactory.Init();

            //UnitManager.Init();
        }
    }
}