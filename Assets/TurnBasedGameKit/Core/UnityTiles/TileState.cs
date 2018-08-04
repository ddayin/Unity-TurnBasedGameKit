using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit
{
    /// <summary>
    /// 타일 상태 추상 클래스
    /// </summary>
    public abstract class TileState
    {
        #region Properties

        public SquareTile m_Tile;

        /// <summary>
        /// 타일 종류
        /// </summary>
        public eTileType m_State;

        /// <summary>
        /// 타일의 좌표
        /// </summary>
        public Vector3Int m_Position;

        /// <summary>
        /// 
        /// </summary>
        public EventHandler m_OnClickHandler;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_tilemap"></param>
        /// <param name="_tile"></param>    
        public TileState( Vector3Int _position )
        {
            m_State = eTileType.Movable;
            m_Position = _position;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unitPosition"></param>
        public abstract void SetUnitPlacedTile( Vector3Int _unitPosition );

        /// <summary>
        /// 마우스로 선택했을 때 처리
        /// </summary>
        //public abstract SquareTile OnSelected();

        public virtual void OnClicked()
        {
            if ( m_OnClickHandler != null )
            {
                m_OnClickHandler.Invoke( this, new EventArgs() );
            }
        }
    }
}