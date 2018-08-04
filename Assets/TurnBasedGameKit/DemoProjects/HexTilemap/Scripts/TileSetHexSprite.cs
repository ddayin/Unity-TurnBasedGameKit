using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoHexTilemap
{
    public class TileSetHexSprite : TileSetBase
    {
        #region Properties
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            Init();
        }

        #endregion

        /// <summary>
        /// initialize cube tile set
        /// </summary>
        public void Init()
        {
            SetTiles();
        }

        private void SetTiles()
        {
            if ( m_Tiles.Count == 0 )
            {
                CreateTiles();
            }
        }
    }
}