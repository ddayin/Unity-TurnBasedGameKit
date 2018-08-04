using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 일반 상태의 타일
    /// </summary>
    public class TileStateNormal : TileState
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_tilemap"></param>
        /// <param name="_tile"></param>
        public TileStateNormal( Vector3Int _position ) : base( _position )
        {
            
        }

        public override void SetUnitPlacedTile( Vector3Int _unitPosition )
        { }

        public override void OnClicked()
        {
            base.OnClicked();

            // 선택된 타일로 설정
            SquareTile tile = SquareTileFactory.CreateSelected( m_Position );

            Map.instance.m_TilemapHighlight.m_Tilemap.SetTile( m_Position, tile );

            // 이동 준비 상태의 플레이어가 있으면 대기 상태로 되돌린다
            UnitKnight knight = UnitManager.GetReadyToMovePlayer();
            if ( knight != null )
            {
                knight.SetToIdle();
            }

            Map.instance.m_TilemapController.ResetAllTiles();
            Map.instance.m_TilemapController.SetAllUnitPlacedTiles();
        }
    }
}