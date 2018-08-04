using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    

    /// <summary>
    /// UnitState의 인자값들 모은 클래스
    /// </summary>
    public class UnitStateParameter
    {
        public Unit m_Unit = null;
        public SpriteRenderer m_SpriteRenderer = null;
        public Animator m_Animator = null;
    }


    /// <summary>
    /// 유닛의 상태 클래스
    /// </summary>
    public abstract class UnitState
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public eUnitCommand m_Command;

        /// <summary>
        /// 
        /// </summary>
        protected UnitStateParameter m_Parameter;
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_animator"></param>
        /// <param name="_spriteRenderer"></param>
        public UnitState( UnitStateParameter _param )
        {
            m_Command = eUnitCommand.Idle;

            m_Parameter = new UnitStateParameter();
            m_Parameter.m_Unit = _param.m_Unit;
            m_Parameter.m_SpriteRenderer = _param.m_SpriteRenderer;
            m_Parameter.m_Animator = _param.m_Animator;
        }

        /// <summary>
        /// 스프라이트 애니메이션 재생
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// 마우스 커서가 유닛 위에 있을 때 처리
        /// </summary>
        public abstract void OnHover();

        /// <summary>
        /// 유닛을 클릭했을 때의 처리
        /// </summary>
        public abstract void OnClicked();
    }
}