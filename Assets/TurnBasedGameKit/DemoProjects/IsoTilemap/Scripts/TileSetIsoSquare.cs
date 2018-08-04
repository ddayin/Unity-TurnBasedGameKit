using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoIsoTilemap
{
    public class TileSetIsoSquare : MonoBehaviour
    {
        #region Properties
        public Vector3Int m_Size;

        public List<IsoSquareTile> m_Tiles = new List<IsoSquareTile>();
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
        }
        #endregion

        public void CreateTiles()
        {
            if ( m_Tiles == null )
            {
                m_Tiles = new List<IsoSquareTile>();
            }

            for ( int x = 0; x < m_Size.x; x++ )
            {
                for ( int y = 0; y < m_Size.y; y++ )
                {
                    for ( int z = 0; z < m_Size.z; z++ )
                    {
                        Vector3Int position = new Vector3Int( x, y, z );

                        IsoSquareTile tile = TileFactory.CreateIsoSquareTileAt( position );

                        m_Tiles.Add( tile );
                    }
                }
            }
        }

        public void ClearAllTiles()
        {
            if ( m_Tiles.Count == 0 ) return;

            foreach ( IsoSquareTile tile in m_Tiles )
            {
                DestroyImmediate( tile.gameObject );
            }
            m_Tiles.Clear();
            m_Tiles = null;
        }
    }
}