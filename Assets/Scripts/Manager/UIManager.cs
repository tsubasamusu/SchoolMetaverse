using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolMetaverse
{
    /// <summary>
    /// UI‚ğ§Œä‚·‚é
    /// </summary>
    public class UIManager : MonoBehaviour,ISetUp
    {
        [SerializeField]
        private Image imgBackground;//”wŒi‚ÌƒCƒ[ƒW

        /// <summary>
        /// UIManager‚Ì‰Šúİ’è‚ğs‚¤
        /// </summary>
        public void SetUp()
        {
            //”wŒi‚ğ•F‚Éİ’è‚·‚é
            imgBackground.color=Color.black;


        }
    }
}
