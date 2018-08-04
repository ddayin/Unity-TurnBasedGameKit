using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 격자 모양의 타일맵
    /// </summary>
    public class TilemapGrid : TileMapBase
    {
        #region Properties
        #endregion

        #region MonoBehaviour

        private void Start()
        {
            CreateGridTiles();
        }
        #endregion

        /// <summary>
        /// 타일맵을 격자 형태의 타일들로 설정한다.
        /// </summary>
        private void CreateGridTiles()
        {
            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
                {
                    Vector3Int pos = new Vector3Int( x, y, 0 );

                    SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
                    tile.m_Position = pos;
                    tile.m_State = new TileStateOutline( tile.m_Position );
                    SquareTile gridTile = SquareTileFactory.CreateGrid( tile.m_Position );

                    m_Tilemap.SetTile( gridTile.m_Position, gridTile );
                }
            }
        }
    }
}