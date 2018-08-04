using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 이동 가능한 타일
    /// </summary>
    public class TileStateMovable : TileState
    {
        private Vector3Int m_UnitPlacedPosition = Vector3Int.zero;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_tilemap"></param>
        /// <param name="_tile"></param>
        public TileStateMovable( Vector3Int _position ) : base( _position )
        {
            m_State = eTileType.Movable;
        }

        public override void SetUnitPlacedTile( Vector3Int _unitPosition )
        {
            m_UnitPlacedPosition = _unitPosition;
        }

        public override void OnClicked()
        {
            base.OnClicked();

            if ( Map.instance.m_TilemapPathFind.HasMovableTile() == false ) return;

            // 이동 가능한 타일을 선택하면 이동할 경로가 표시된다.
            Map.instance.m_TilemapPathFind.SetPathFindGrid( m_UnitPlacedPosition, m_Position );
        }
    }
}