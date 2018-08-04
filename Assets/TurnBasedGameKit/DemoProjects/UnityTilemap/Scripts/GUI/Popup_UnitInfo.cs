using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 유닛의 정보를 표시하는 팝업
    /// </summary>
    public class Popup_UnitInfo : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// 유닛의 아이디
        /// </summary>
        private Text m_ID;

        /// <summary>
        /// 유닛의 체력
        /// </summary>
        private Text m_HP;

        /// <summary>
        /// 유닛의 마력
        /// </summary>
        private Text m_MP;

        /// <summary>
        /// 유닛이 이동할 수 있는 범위
        /// </summary>
        private Text m_MoveRange;

        /// <summary>
        /// 유닛이 공격할 수 있는 범위
        /// </summary>
        private Text m_AttackRange;

        /// <summary>
        /// 유닛이 공격할 때 상대방에게 입히는 피해 수치
        /// </summary>
        private Text m_HitPoint;

        /// <summary>
        /// 
        /// </summary>
        private Unit m_Unit;

        private UnitStat m_UnitStat;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_ID = transform.Find( "Image_BG/Text_ID" ).GetComponent<Text>();
            m_HP = transform.Find( "Image_BG/Text_HP" ).GetComponent<Text>();
            m_MP = transform.Find( "Image_BG/Text_MP" ).GetComponent<Text>();
            m_MoveRange = transform.Find( "Image_BG/Text_MoveRange" ).GetComponent<Text>();
            m_AttackRange = transform.Find( "Image_BG/Text_AttackRange" ).GetComponent<Text>();
            m_HitPoint = transform.Find( "Image_BG/Text_HitPoint" ).GetComponent<Text>();
        }

        private void OnEnable()
        {
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        private void OnDisable()
        {

        }
        #endregion

        public void SetUnit( Unit _unit )
        {
            m_Unit = _unit;
            m_UnitStat = m_Unit.m_Stat;

            SetData();
        }

        private void SetData()
        {
            m_ID.text = "ID : " + m_Unit.m_ID.ToString();
            m_HP.text = "HP : " + m_UnitStat.m_HealthPoint.ToString();
            m_MP.text = "MP : " + m_UnitStat.m_MagicPoint.ToString();
            m_MoveRange.text = "Move Range : " + m_UnitStat.m_MoveRange.ToString();
            m_AttackRange.text = "Attack Range : " + m_UnitStat.m_AttackRange.ToString();
            m_HitPoint.text = "Hit Point : " + m_UnitStat.m_HitPoint.ToString();
        }
    }
}