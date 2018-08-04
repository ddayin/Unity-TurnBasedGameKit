using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOrc : Unit
    {
        #region Properties
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            m_Type = eCampType.Enemies;

            // 시작은 Idle로 시작        
            m_Command = eUnitCommand.Idle;

            m_Direction = eDirection.West;

            m_State = null;
            UnitStateParameter param = new UnitStateParameter();
            param.m_Unit = this;
            param.m_Animator = m_Animator;
            param.m_SpriteRenderer = m_SpriteRenderer;

            m_State = new UnitStateIdle( param );
            m_State.Draw();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void OnMouseEnter()
        {

        }

        private void OnMouseOver()
        {
            // 유닛의 정보를 표시한다.
            TurnBasedGameKit.DemoUnityTilemap.PopupManager.instance.SetUnitInfo( true );
        }

        private void OnMouseExit()
        {
            // 유닛의 정보를 표시하지 않는다.
            TurnBasedGameKit.DemoUnityTilemap.PopupManager.instance.SetUnitInfo( false );
        }

        private void OnMouseDown()
        {

        }

        private void OnMouseUp()
        {
            if ( m_State != null )
            {
                m_State.OnClicked();
            }
        }
        #endregion


    }
}