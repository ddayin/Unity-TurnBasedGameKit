using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanzyeeStudio;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 타일맵을 가지고 있는 맵
    /// </summary>
    public class Map : BaseSingleton<Map>
    {
        #region Properties
        /// <summary>
        /// 배경 타일맵
        /// </summary>
        [HideInInspector]
        public TilemapBG m_TilemapBG;

        /// <summary>
        /// 장애물 타일맵
        /// </summary>
        [HideInInspector]
        public TilemapObstacle m_TilemapObstacle;

        /// <summary>
        /// 격자 모양을 표시해서 타일 간 구분이 잘 가도록 하는 타일맵
        /// </summary>
        [HideInInspector]
        public TilemapGrid m_TilemapGrid;

        /// <summary>
        /// 타일 상태 표시할 타일맵
        /// </summary>
        [HideInInspector]
        public TilemapHighlight m_TilemapHighlight;

        /// <summary>
        /// 길찾기 알고리즘을 위한 타일맵
        /// </summary>
        [HideInInspector]
        public TilemapPathFind m_TilemapPathFind;

        [HideInInspector]
        public TilemapController m_TilemapController;

        [HideInInspector]
        public TilemapInput m_TilemapInput;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_TilemapBG = transform.Find( "Tilemap_BG" ).GetComponent<TilemapBG>();
            m_TilemapObstacle = transform.Find( "Tilemap_Obstacle" ).GetComponent<TilemapObstacle>();
            m_TilemapGrid = transform.Find( "Tilemap_Grid" ).GetComponent<TilemapGrid>();
            m_TilemapHighlight = transform.Find( "Tilemap_Highlight" ).GetComponent<TilemapHighlight>();
            m_TilemapPathFind = m_TilemapHighlight.transform.GetComponent<TilemapPathFind>();
            m_TilemapController = m_TilemapHighlight.transform.GetComponent<TilemapController>();
            m_TilemapInput = m_TilemapHighlight.transform.GetComponent<TilemapInput>();
        }
        #endregion
    }
}