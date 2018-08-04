using UnityEngine;
using System.Collections;

namespace TurnBasedGameKit.DemoHexTilemap
{
    public class DemoHexTilemap : MonoBehaviour
    {
        private void Awake()
        {
            // It's important to call method in order
            ResourcesLoader.LoadAll();

            TileFactory.Init();

            UnitManager.Init();
        }
    }
}