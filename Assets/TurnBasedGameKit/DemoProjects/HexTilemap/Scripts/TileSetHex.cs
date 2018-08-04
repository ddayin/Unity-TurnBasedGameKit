using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoHexTilemap
{
    public class TileSetHex : TileSetBase
    {
        #region Properties
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            Init();

            SetTiles();
        }

        #endregion
        
        private void SetTiles()
        {
            if ( m_Tiles == null )
            {
                m_Tiles = new Vector3IntTileBaseDictionary();
            }
            if ( m_Tiles.Count == 0 )
            {
                CreateTiles();
            }
        }
    }
}