using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 디버깅을 위한 타일맵
    /// </summary>
    public class TilemapDebug : TileMapBase
    {
        #region Properties
        private Transform m_Panel;
        private List<Text> m_TileIndexs = new List<Text>();
        private List<NumberRenderer> m_TileIndexRenderers = new List<NumberRenderer>();
        private GameObject m_TileIndexPrefab;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_Panel = this.transform.Find( "Canvas/Panel" );
            m_TileIndexPrefab = Resources.Load( "Prefabs/Sprite_TileIndex" ) as GameObject;
        }

        private void Start()
        {
            CreateBorderTileIndex();
        }

        #endregion

        /// <summary>
        /// 타일 맵 가장 자리에 타일 인덱스를 표시한다.
        /// </summary>
        private void CreateBorderTileIndex()
        {
            // 상
            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                GameObject tileIndex = Instantiate( m_TileIndexPrefab, this.transform ) as GameObject;
                tileIndex.transform.position = new Vector3( x + 0.5f, m_Bounds.yMax + 0.5f, 0f );
                NumberRenderer tileIndexNumber = tileIndex.GetComponent<NumberRenderer>();
                tileIndexNumber.RenderNumber( Mathf.Abs( x ) );

                m_TileIndexRenderers.Add( tileIndexNumber );
            }

            // 하
            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                GameObject tileIndex = Instantiate( m_TileIndexPrefab, this.transform ) as GameObject;
                tileIndex.transform.position = new Vector3( x + 0.5f, m_Bounds.yMin - 0.5f, 0f );
                NumberRenderer tileIndexNumber = tileIndex.GetComponent<NumberRenderer>();
                tileIndexNumber.RenderNumber( Mathf.Abs( x ) );

                m_TileIndexRenderers.Add( tileIndexNumber );
            }

            // 좌
            for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
            {
                GameObject tileIndex = Instantiate( m_TileIndexPrefab, this.transform ) as GameObject;
                tileIndex.transform.position = new Vector3( m_Bounds.xMin - 0.5f, y + 0.5f, 0f );
                NumberRenderer tileIndexNumber = tileIndex.GetComponent<NumberRenderer>();
                tileIndexNumber.RenderNumber( Mathf.Abs( y ) );

                m_TileIndexRenderers.Add( tileIndexNumber );
            }

            // 우
            for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
            {
                GameObject tileIndex = Instantiate( m_TileIndexPrefab, this.transform ) as GameObject;
                tileIndex.transform.position = new Vector3( m_Bounds.xMax + 0.5f, y + 0.5f, 0f );
                NumberRenderer tileIndexNumber = tileIndex.GetComponent<NumberRenderer>();
                tileIndexNumber.RenderNumber( Mathf.Abs( y ) );

                m_TileIndexRenderers.Add( tileIndexNumber );
            }
        }
    }
}