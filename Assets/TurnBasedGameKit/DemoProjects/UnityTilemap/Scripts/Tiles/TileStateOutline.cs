using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 격자 모양의 상태의 타일
    /// </summary>
    public class TileStateOutline : TileState
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_tilemap"></param>
        /// <param name="_tile"></param>
        public TileStateOutline( Vector3Int _position ) : base( _position )
        {
            
        }

        public override void SetUnitPlacedTile( Vector3Int _unitPosition )
        { }

        public override void OnClicked()
        {
            base.OnClicked();

            // 격자 타일을 선택하더라도 아무런 동작을 하지 않는다.
        }
    }
}