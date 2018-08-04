using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 마우스 입력 처리 클래스
    /// </summary>
    public class TilemapInput : TileMapBase
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private TilemapController m_TilemapController;

        /// <summary>
        /// 
        /// </summary>
        private TilemapPathFind m_TilemapPathFind;

        /// <summary>
        /// 카메라
        /// </summary>
        private Camera m_Camera;

        /// <summary>
        /// 이전에 선택한 타일 저장
        /// </summary>
        private SquareTile m_PreviousTile = null;

        /// <summary>
        /// 마우스로 터치해서 선택된 타일
        /// </summary>
        private SquareTile m_SelectedTile;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_Camera = Camera.main;

            m_TilemapController = this.GetComponent<TilemapController>();
            m_TilemapPathFind = this.GetComponent<TilemapPathFind>();
        }

        private void Update()
        {
            if ( Input.GetMouseButtonUp( 0 ) == true )
            {
                for ( int i = 0; i < UnitManager.m_PlayerUnits.Count; i++ )
                {
                    // 플레이어 유닛이 이동 중일 때에는 마우스 입력을 막는다.
                    if ( UnitManager.m_PlayerUnits[ i ].m_Command == eUnitCommand.Move )
                    {
                        return;
                    }
                }

                ProcessMouseUp();
            }
        }
        #endregion


        /// <summary>
        /// 마우스 버튼 클릭한 위치에 선택 타일 표시
        /// </summary>
        private void ProcessMouseUp()
        {
            if ( m_SelectedTile != null )
            {
                m_PreviousTile = m_SelectedTile;
            }

            // 이전에 선택했던 타일은 삭제
            if ( m_PreviousTile != null )
            {
                m_TilemapController.DeleteTile( m_PreviousTile.m_Position );
            }

            // 현재 선택한 타일을 가져온다.
            m_SelectedTile = SelectedTile();
        }

        /// <summary>
        /// 현재 마우스 위치에 선택된 타일을 반환한다.
        /// </summary>
        private SquareTile SelectedTile()
        {
            Vector3 worldPosition = m_Camera.ScreenToWorldPoint( Input.mousePosition );
            Vector3Int cellPosition = m_Tilemap.WorldToCell( worldPosition );

            // 예외 처리
            if ( cellPosition.x >= m_Bounds.xMax || cellPosition.x < m_Bounds.xMin ) return null;
            if ( cellPosition.y >= m_Bounds.yMax || cellPosition.y < m_Bounds.yMin ) return null;

            SquareTile tile = m_Tilemap.GetTile<SquareTile>( cellPosition );
            tile.m_Position = cellPosition;
            Debug.Log( "selected tile = " + tile.m_Position + " : type = " + tile.m_State.m_State );

            UnitKnight knight = UnitManager.GetReadyToMovePlayer();
            if ( knight != null )
            {
                tile.m_State.SetUnitPlacedTile( knight.m_Position );
            }

            tile.m_State.OnClicked();

            return tile;
        }
    }
}