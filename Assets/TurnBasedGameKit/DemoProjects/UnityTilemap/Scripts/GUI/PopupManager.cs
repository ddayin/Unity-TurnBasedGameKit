using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanzyeeStudio;

namespace TurnBasedGameKit.DemoUnityTilemap
{

    /// <summary>
    /// 팝업들 관리
    /// </summary>
    public class PopupManager : BaseSingleton<PopupManager>
    {
        #region Properties
        /// <summary>
        /// 유닛의 정보를 표시하는 팝업
        /// </summary>
        [HideInInspector]
        public Popup_UnitInfo m_UnitInfo;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_UnitInfo = transform.Find( "Panel_UnitInfo" ).GetComponent<Popup_UnitInfo>();

            m_UnitInfo.gameObject.SetActive( false );
        }
        #endregion

        /// <summary>
        /// 유닛 팝업창을 활성화 / 비활성화 시킨다.
        /// </summary>
        /// <param name="_enable"></param>
        public void SetUnitInfo( bool _enable )
        {
            m_UnitInfo.gameObject.SetActive( _enable );
        }
    }
}
