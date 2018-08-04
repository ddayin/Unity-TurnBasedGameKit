using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    public enum eVictoryCondition
    {
        ClearAllEnemies,
        KillHeroEnemy,
        ArriveAtGoal
    }


    public class GameEditor : MonoBehaviour
    {
        public int m_TotalTurn = 0;

        public eVictoryCondition m_VictoryCondition;
    }
}


