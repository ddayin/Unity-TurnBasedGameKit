using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 유닛이 대기 중인 상태
    /// </summary>
    public class UnitStateIdle : UnitState
    {
        public UnitStateIdle( UnitStateParameter _param )
            : base( _param )
        {
            m_Command = eUnitCommand.Idle;
        }

        public override void Draw()
        {
            m_Parameter.m_SpriteRenderer.color = Color.white;

            if ( m_Parameter.m_Animator != null )
            {
                m_Parameter.m_Animator.SetBool( "Idle", true );
                m_Parameter.m_Animator.SetBool( "Move", false );
            }
        }

        public override void OnHover()
        {

        }

        public override void OnClicked()
        {
            if ( UnitManager.GetMovingPlayer() != null )
            {
                return;
            }

            // 이동 준비
            m_Command = eUnitCommand.ReadyToMove;
            Debug.Log( "Ready To Move" );

            UnitStateParameter param = new UnitStateParameter();
            param.m_Unit = m_Parameter.m_Unit;
            param.m_SpriteRenderer = m_Parameter.m_Unit.m_SpriteRenderer;
            param.m_Animator = m_Parameter.m_Unit.m_Animator;

            m_Parameter.m_Unit.m_State = null;
            m_Parameter.m_Unit.m_State = new UnitStateReadyToMove( param );
            m_Parameter.m_Unit.m_State.Draw();
        }
    }
}