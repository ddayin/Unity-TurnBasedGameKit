using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoHexTilemap
{
    public class TileHex : TileBase
    {
        #region Properties
        protected MeshRenderer m_Renderer;

        private DemoCubeTilemap.TileSetPathFind m_TileSet;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_Renderer = this.GetComponentInChildren<MeshRenderer>();
            m_TileSet = GameObject.Find( "TileSet" ).GetComponent<DemoCubeTilemap.TileSetPathFind>();
        }

        private void OnMouseUp()
        {
            TestDrawMovable();
            //SetColor( eTileType.Selected );
        }
        #endregion


        public void TestDrawMovable()
        {
            // 플레이어 캐릭터의 경우에만, 이동 가능 / 공격 가능한 타일을 그린다.
            UnitBase unit = UnitManager.GetUnitAt( m_Property.m_Coordinate );
            if ( unit.m_Property.m_CampType != eCampType.Player )
            {
                return;
            }

            // 이동 가능한 영역을 그린다.
            m_TileSet.DrawMovableRange( unit, this );

            // 공격 가능한 타일이 있다면 공격 가능한 영역을 그린다.
            //if ( m_TileSet.IsAttackable( unit, this ) == true )
            //{
            //    m_TileSet.DrawAttackableRange( unit, this );
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        public override void SetColor( eTileType _type )
        {
            m_Property.m_Type = _type;

            Color newColor = TileTypeColor.Normal;
            switch ( m_Property.m_Type )
            {
                case eTileType.Obstacle:
                    break;

                case eTileType.Movable:
                    newColor = TileTypeColor.Movable;
                    break;

                case eTileType.Breakable:

                    break;
                default:
                    Debug.LogError( "type == " + m_Property.m_Type );
                    break;
            }

            m_Renderer.material.color = newColor;
        }
        
    }
}