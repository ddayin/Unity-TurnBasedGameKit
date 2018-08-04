using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 공격 가능한 타일
    /// </summary>
    public class TileStateAttackable : TileState
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_tilemap"></param>
        /// <param name="_tile"></param>
        public TileStateAttackable( Vector3Int _position ) : base( _position )
        {
            
        }

        public override void SetUnitPlacedTile( Vector3Int _unitPosition )
        { }

        public override void OnClicked()
        {
            base.OnClicked();

            // TODO : 공격
        }
    }
}