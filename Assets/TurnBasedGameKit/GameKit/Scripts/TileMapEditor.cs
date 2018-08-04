using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    public class TileMapEditor : MonoBehaviour
    {
        [Tooltip( "Size of tile map" )]
        public Vector3Int m_TileMapSize;

        [Tooltip( "Size of each tile" )]
        public float m_TileSize;
    }
}