using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 유닛이 배치된 타일
    /// </summary>
    public class TileStateUnitPlaced : TileState
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_tilemap"></param>
        /// <param name="_tile"></param>
        public TileStateUnitPlaced( Vector3Int _position ) : base( _position )
        {
            
        }

        public override void SetUnitPlacedTile( Vector3Int _unitPosition )
        { }

        public override void OnClicked()
        {
            base.OnClicked();

            // 플레이어 캐릭터의 경우에만, 이동 가능 / 공격 가능한 타일을 그린다.
            Unit unit = UnitManager.GetUnitAt( m_Position );
            if ( unit.m_Type != eCampType.Player )
            {
                return;
            }

            // 이동 가능한 영역을 그린다.
            Map.instance.m_TilemapController.ResetAllTiles();
            Map.instance.m_TilemapPathFind.DrawMovableRange( unit, m_Tile );

            // 공격 가능한 타일이 있다면 공격 가능한 영역을 그린다.
            if ( Map.instance.m_TilemapPathFind.IsAttackable( unit, m_Tile ) == true )
            {
                Map.instance.m_TilemapPathFind.DrawAttackableRange( unit, m_Tile );
            }

            // 초기화
            Map.instance.m_TilemapPathFind.ResetTileDistance();


        }
    }
}