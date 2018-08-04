using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    // developer's custom type
    // this is only example
    public enum eUnitType
    {
        Knight,
        Orc,
        Spearman
    }

    public enum eAttackType
    {
        Close,      // 근접 공격
        LongRange,  // 원거리 공격
        WideArea    // 광역 공격
    }



    public class UnitEditor : MonoBehaviour
    {
        [Tooltip( "Unit's ID" )]
        public int m_UnitID;

        [Tooltip( "Unit's camp" )]
        public eCampType m_CampType;

        [Tooltip( "Type of unit" )]
        public eUnitType m_UnitType;

        [Tooltip( "How far unit can attack" )]
        public eAttackType m_AttackType;
    }
}