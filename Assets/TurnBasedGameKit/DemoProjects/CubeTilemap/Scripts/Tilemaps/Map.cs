using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanzyeeStudio;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    public class Map : BaseSingleton<Map>
    {
        [HideInInspector]
        public TileSetPathFind m_TileSetPathFind;

        protected override void Awake()
        {
            base.Awake();

            m_TileSetPathFind = this.GetComponent<TileSetPathFind>();
        }
    }
}


