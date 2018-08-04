using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit
{
    /// <summary>
    /// 탐색 할 방향 (상하좌우)
    /// </summary>
    public enum eDirection
    {
        North = 0,
        South,
        West,
        East,
        NorthWest,
        SouthEast
    }

    public class TileMapBase : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// 타일 맵
        /// </summary>
        [HideInInspector]
        public Tilemap m_Tilemap;

        /// <summary>
        /// 타일 맵의 크기
        /// </summary>
        [HideInInspector]
        public BoundsInt m_Bounds;
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            m_Tilemap = GetComponent<Tilemap>();
            m_Bounds = m_Tilemap.cellBounds;
        }
        #endregion
    }
}