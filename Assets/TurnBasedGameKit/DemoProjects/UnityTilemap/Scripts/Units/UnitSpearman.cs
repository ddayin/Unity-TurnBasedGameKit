using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    public class UnitSpearman : Unit
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

            m_Type = eCampType.Allies;
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