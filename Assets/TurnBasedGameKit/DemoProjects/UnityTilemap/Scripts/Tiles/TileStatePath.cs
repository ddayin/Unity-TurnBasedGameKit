using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 이동 가능한 타일에 유닛이 갈 경로를 표시
    /// </summary>
    public class TileStatePath : TileState
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_tilemap"></param>
        /// <param name="_tile"></param>
        public TileStatePath( Vector3Int _position ) : base( _position )
        {
            
        }

        public override void SetUnitPlacedTile( Vector3Int _unitPosition ) { }

        public override void OnClicked()
        {
            base.OnClicked();

            UnitKnight knight = UnitManager.GetReadyToMovePlayer();
            if ( knight != null )
            {
                knight.Move( m_Position );
            }
        }
    }
}