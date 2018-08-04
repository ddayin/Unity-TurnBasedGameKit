using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WanzyeeStudio;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    /// <summary>
    /// unit manager
    /// </summary>
    public class UnitManager : BaseSingleton<UnitManager>
    {
        #region Properties
        [SerializeField]
        private List<UnitBase> m_PlayerUnits = new List<UnitBase>();

        [SerializeField]
        private List<UnitBase> m_EnemyUnits = new List<UnitBase>();

        private Transform m_Parent;

        private UnitBase m_SelectedUnit = null;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            m_Parent = GameObject.Find( "UnitParent" ).transform;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coordinate"></param>
        /// <returns></returns>
        public UnitBase GetPlayerUnitAt( Vector3Int _coordinate )
        {
            foreach ( UnitBase player in m_PlayerUnits )
            {
                if ( player.m_Property.m_Coordinate == _coordinate )
                {
                    return player;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coordinate"></param>
        /// <returns></returns>
        public UnitBase GetEnemyUnitAt( Vector3Int _coordinate )
        {
            foreach ( UnitBase enemy in m_EnemyUnits )
            {
                if ( enemy.m_Property.m_Coordinate == _coordinate )
                {
                    return enemy;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UnitBase GetSelectedUnit()
        {
            if ( m_SelectedUnit == null ) return null;

            if ( m_SelectedUnit.m_Property.m_CampType == eCampType.Player )
            {
                return m_SelectedUnit;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unit"></param>
        public void SetSelectedUnit( UnitBase _unit )
        {
            if ( _unit.m_Property.m_CampType == eCampType.Player )
            {
                m_SelectedUnit = _unit;
            }
        }
    }
}