using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    /// <summary>
    /// 
    /// </summary>
    public enum eCampType
    {
        Player = 0,     // 플레이어 진영
        Allies,     // 아군 진영
        Enemies     // 적군 진영
    }

    public class CampEditor : MonoBehaviour
    {
        public int m_GroupID = 0;

        [Tooltip( "Type of camp to place" )]
        public eCampType m_CampType;
    }
}