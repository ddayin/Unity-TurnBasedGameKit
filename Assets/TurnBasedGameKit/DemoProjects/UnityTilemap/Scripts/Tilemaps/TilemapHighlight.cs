using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 타일의 상태를 표시하기 위한 타일맵
    /// </summary>
    public class TilemapHighlight : TileMapBase
    {
        #region Properties
        /// <summary>
        /// 길찾기를 위한 타일맵
        /// </summary>
        private TilemapPathFind m_TilemapPathFind;

        /// <summary>
        /// 
        /// </summary>
        private TilemapController m_TilemapController;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_TilemapPathFind = this.GetComponent<TilemapPathFind>();
            m_TilemapController = this.GetComponent<TilemapController>();
        }
        #endregion
    }
}