using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    /// <summary>
    /// y 축을 기준으로 sorting order를 정렬해서 순차적으로 그린다.
    /// </summary>
    public class SetZOrder : MonoBehaviour
    {
        private SpriteRenderer m_SpriteRenderer;

        private void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSortingOrder( int y )
        {
            m_SpriteRenderer.sortingOrder = y * -1;
        }
    }
}