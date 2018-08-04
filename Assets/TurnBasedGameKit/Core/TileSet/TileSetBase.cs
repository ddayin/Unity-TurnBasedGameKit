using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGameKit
{
    /// <summary>
    /// base class of tile set
    /// </summary>
    public class TileSetBase : MonoBehaviour
    {
        #region Properties
        public eTileShape m_TileShape = eTileShape.Cube;
        public int m_Width = 0;
        public int m_Length = 0;
        public Vector3IntTileBaseDictionary m_Tiles = new Vector3IntTileBaseDictionary();
        public List<Text> m_CoordinateText = new List<Text>();
        public Canvas m_Canvas = null;
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            m_Canvas = GameObject.Find( "TileSet" ).transform.Find( "Canvas_Tile" ).GetComponent<Canvas>();
        }

        protected virtual void Start()
        {
            if ( m_Tiles.Count == 0 || m_Tiles == null )
            {
                CreateTiles();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public virtual void Init()
        {
            m_Canvas = GameObject.Find( "TileSet" ).transform.Find( "Canvas_Tile" ).GetComponent<Canvas>();

            if ( m_Tiles.Count > 0 )
            {
                ClearAllTiles();
            }

            if ( m_CoordinateText == null )
            {
                m_CoordinateText = new List<Text>();
                return;
            }
            if ( m_CoordinateText.Count > 0 )
            {
                ClearAllCoordinates();
            }
        }

        /// <summary>
        /// return cube tile placed at specific coordinate
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        public TileBase GetTileAt( Vector3Int _coordinate )
        {
            if ( m_Tiles.ContainsKey( _coordinate ) == true )
            {
                return m_Tiles[ _coordinate ];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateTiles()
        {
            if ( m_Tiles == null )
            {
                m_Tiles = new Vector3IntTileBaseDictionary();
            }

            for ( int x = 0; x < m_Width; x++ )
            {
                for ( int y = 0; y < m_Length; y++ )
                {
                    Vector3Int position = new Vector3Int( x, y, 0 );
                    TileBase tile = null;
                    switch ( m_TileShape )
                    {
                        case eTileShape.Cube:
                            tile = DemoCubeTilemap.TileFactory.CreateCubeTileAt( position ) as TileBase;
                            break;

                        case eTileShape.SquareSprite:
                            tile = null;
                            break;

                        case eTileShape.Hex:
                            tile = DemoHexTilemap.TileFactory.CreateHexTileAt( position );
                            break;

                        case eTileShape.HexSprite:
                            tile = null;
                            break;

                        case eTileShape.UnityTilemap:
                            tile = null;
                            break;
                        default:
                            tile = null;
                            break;
                    }

                    if ( m_Tiles.ContainsKey( position ) == false )
                    {
                        m_Tiles.Add( position, tile );
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearAllTiles()
        {
            if ( m_Tiles.Count == 0 ) return;
            if ( m_Tiles == null ) return;

            foreach ( KeyValuePair<Vector3Int, TileBase> kv in m_Tiles )
            {
                if ( kv.Value != null )
                {
                    DestroyImmediate( kv.Value.gameObject );
                }
            }
            m_Tiles.Clear();
            m_Tiles = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateCoordinateTexts()
        {
            if ( m_CoordinateText == null )
            {
                m_CoordinateText = new List<Text>();
            }

            for ( int x = 0; x < m_Width; x++ )
            {
                for ( int y = 0; y < m_Length; y++ )
                {
                    GameObject prefab;
                    switch ( m_TileShape )
                    {
                        case eTileShape.Cube:
                            prefab = TurnBasedGameKit.DemoCubeTilemap.ResourcesLoader.m_CoordinatePrefab;
                            break;

                        case eTileShape.SquareSprite:
                            prefab = null;
                            break;

                        case eTileShape.Hex:
                            prefab = TurnBasedGameKit.DemoHexTilemap.ResourcesLoader.m_CoordinatePrefab;
                            break;

                        case eTileShape.HexSprite:
                            prefab = null;
                            break;

                        case eTileShape.UnityTilemap:
                            prefab = null;
                            break;
                        default:
                            prefab = null;
                            break;
                    }
                    
                    GameObject obj = Instantiate( prefab );
                    Text label = obj.GetComponent<Text>();
                    label.rectTransform.SetParent( m_Canvas.transform, false );
                    label.rectTransform.anchoredPosition = new Vector2( x, y );
                    label.text = x.ToString() + "\n" + y.ToString();

                    m_CoordinateText.Add( label );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearAllCoordinates()
        {
            if ( m_CoordinateText.Count == 0 ) return;

            foreach ( Text text in m_CoordinateText )
            {
                if ( text != null )
                {
                    DestroyImmediate( text.gameObject );
                }
            }
            m_CoordinateText.Clear();
            m_CoordinateText = null;
        }
    }
}