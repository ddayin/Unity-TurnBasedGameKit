using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    public enum eAI_Difficulty
    {
        Easy = 0,
        Normal,
        Hard
    }

    public enum eAI_Type
    {
        Passive = 0,
        Aggressive
    }

    public class AIEditor : MonoBehaviour
    {
        public int m_GroupID = 0;

        [Tooltip( "" )]
        public eAI_Difficulty m_AI_Difficulty;

        [Tooltip( "" )]
        public eAI_Type m_AI_Type;
    }
}