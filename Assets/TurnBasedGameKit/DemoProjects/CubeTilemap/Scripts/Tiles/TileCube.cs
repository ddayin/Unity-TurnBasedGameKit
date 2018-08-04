using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    /// <summary>
    /// 
    /// </summary>
    public class TileCube : TileBase
    {
        #region Properties
        protected MeshRenderer m_Renderer;
        private TileSetPathFind m_TileSet;

        /// <summary>
        /// mouse cursor is over or not
        /// </summary>
        private bool m_IsMouseOver = false;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_Renderer = this.GetComponent<MeshRenderer>();
            m_TileSet = GameObject.Find( "TileSet" ).GetComponent<TileSetPathFind>();
        }

        private void OnMouseDown()
        {
            Debug.Log( m_Property.m_Coordinate + " : type = " + m_Property.m_Type );

            ProcessMouseDown();
        }

        private void OnMouseOver()
        {
            if ( m_Property.m_Type != eTileType.Movable ) return;

            m_IsMouseOver = true;

            SetColor( eTileType.Movable );
        }

        private void OnMouseExit()
        {
            if ( m_Property.m_Type != eTileType.Movable ) return;

            m_IsMouseOver = false;

            SetColor( eTileType.Movable );
        }
        #endregion

        public override void Init()
        {
            base.Init();

            m_Unit = UnitManager.instance.GetPlayerUnitAt( m_Property.m_Coordinate );
        }

        /// <summary>
        /// 
        /// </summary>
        public void ProcessMouseDown()
        {
            switch ( m_Property.m_Type )
            {
                case eTileType.Movable:
                    {
                        if ( m_Unit == null )
                        {
                            UnitBase unit = UnitManager.instance.GetSelectedUnit();
                            if ( unit == null ) return;

                            if ( m_Property.m_IsPath == false )
                            {
                                m_TileSet.DrawPath( unit.m_Property.m_Coordinate, m_Property.m_Coordinate );
                            }
                            else
                            {
                                unit.Move( m_Property.m_Coordinate );
                            }
                        }
                        else
                        {
                            m_TileSet.DrawMovableRange( m_Unit, this );

                            //if ( m_TileSet.HasAttackable( m_Unit, this ) == true )
                            //{
                            //    m_TileSet.DrawAttackableRange( m_Unit, this );
                            //}
                        }
                    }
                    break;

                case eTileType.Obstacle:
                    break;

                case eTileType.Breakable:
                    break;

                default:
                    break;
            }
        }

        public override void SetColor( eTileType _type )
        {
            base.SetColor( _type );

            Color newColor = TileTypeColor.Normal;
            
            switch ( m_Property.m_Type )
            {
                case eTileType.Movable:
                    {
                        if ( m_IsMouseOver == true )
                        {
                            newColor = TileTypeColor.Over;
                        }
                        else
                        {
                            newColor = TileTypeColor.Normal;
                        }

                        if ( m_Unit == null )
                        {
                            if ( m_Property.m_IsAttackable == true )
                            {
                                newColor = TileTypeColor.Attackable;
                            }
                            if ( m_Property.m_IsMovable == true )
                            {
                                newColor = TileTypeColor.Movable;
                            }
                            if ( m_Property.m_IsPath == true )
                            {
                                newColor = TileTypeColor.Path;
                            }
                        }
                        // if unit exists on this tile
                        else
                        {
                            if ( m_Unit.m_Property.m_CampType == eCampType.Enemies )
                            {
                                if ( m_Property.m_IsAttackable == true )
                                {
                                    newColor = TileTypeColor.Attackable;
                                }
                            }
                        }
                    }
                    break;

                case eTileType.Obstacle:
                    newColor = TileTypeColor.Obstacle;
                    break;

                case eTileType.Breakable:
                    newColor = TileTypeColor.Breakable;
                    break;

                default:
                    Debug.LogError( "type == " + m_Property.m_Type );
                    break;
            }

            m_Renderer.material.color = newColor;
        }
    }
}