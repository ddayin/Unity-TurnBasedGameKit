using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 장애물 타일맵
    /// </summary>
    public class TilemapObstacle : TileMapBase
    {
        #region Properties

        /// <summary>
        /// 장애물의 위치들
        /// </summary>
        private HashSet<Vector3Int> m_ObstacleTiles = new HashSet<Vector3Int>();
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            SetObstacles();
        }
        #endregion

        /// <summary>
        /// 장애물 타일이 위치들을 반환한다.
        /// </summary>
        /// <returns></returns>
        public HashSet<Vector3Int> GetObstacleTiles()
        {
            return m_ObstacleTiles;
        }

        /// <summary>
        /// 장애물 타일들을 찾는다.
        /// </summary>
        private void SetObstacles()
        {
            foreach ( var pos in m_Tilemap.cellBounds.allPositionsWithin )
            {
                Vector3Int localPlace = new Vector3Int( pos.x, pos.y, pos.z );
                Vector3 place = m_Tilemap.CellToWorld( localPlace );
                if ( m_Tilemap.HasTile( localPlace ) )
                {
                    m_ObstacleTiles.Add( localPlace );
                }
            }
        }
    }
}