using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    class DemoUnityTilemap : MonoBehaviour
    {
        private void Awake()
        {
            ResourcesLoader.Init();
            //UnitManager.Init();
        }
    }
}