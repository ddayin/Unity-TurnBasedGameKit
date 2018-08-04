using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit
{
    /// <summary>
    /// 정사각형의 타일
    /// </summary>
    public class SquareTile : Tile
    {
        #region Properties    
        /// <summary>
        /// 타일의 위치 (좌표)
        /// </summary>
        public Vector3Int m_Position = Vector3Int.zero;

        /// <summary>
        /// 탐색할 때 쓰이는 타일로부터 거리
        /// </summary>
        public int m_Distance = 0;

        /// <summary>
        /// 타일의 상태 클래스
        /// </summary>
        public TileState m_State;

        /// <summary>
        /// 타일 위에 위치한 유닛
        /// </summary>
        public TurnBasedGameKit.DemoUnityTilemap.Unit m_Unit = null;

        /// <summary>
        /// tile price.
        ///     0.0f = Unwalkable tile.
        ///     1.0f = Normal tile.
        ///     > 1.0f = costy tile.
        ///     < 1.0f = cheap tile.
        /// </summary>
        public float m_Cost = 0f;
        #endregion
    }
}