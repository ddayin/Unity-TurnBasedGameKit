using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 장애물 타일
    /// </summary>
    public class TileStateObstacle : TileState
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_position"></param>
        public TileStateObstacle( Vector3Int _position ) : base( _position )
        {
            m_State = eTileType.Obstacle;
        }

        public override void SetUnitPlacedTile( Vector3Int _unitPosition )
        { }

        public override void OnClicked()
        {
            base.OnClicked();
        }
    }
}