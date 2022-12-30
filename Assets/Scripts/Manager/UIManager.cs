using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolMetaverse
{
    /// <summary>
    /// UIを制御する
    /// </summary>
    public class UIManager : MonoBehaviour,ISetUp
    {
        [SerializeField]
        private Image imgBackground;//背景のイメージ

        [SerializeField]
        private Image imgBtnMain;//メインボタンのイメージ

        [SerializeField]
        private Image imgBtnSub;//サブボタンのイメージ

        [SerializeField]
        private Text txtPlaceholder;//スペースホルダのテキスト

        [SerializeField]
        private Text txtPlayerEntered;//プレイヤーが入力したテキスト

        [SerializeField]
        private Text txtBtnMain;//メインボタンのテキスト

        [SerializeField]
        private Text txtBtnSub;//サブボタンのテキスト

        [SerializeField]
        private Button btnMain;//メインボタン

        [SerializeField]
        private Button btnSub;//サブボタン

        [SerializeField]
        private InputField inputField;//InputField

        /// <summary>
        /// UIManagerの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //背景を黒色に設定する
            imgBackground.color=Color.black;


        }
    }
}
