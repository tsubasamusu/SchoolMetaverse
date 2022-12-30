using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SchoolMetaverse
{
    /// <summary>
    /// ルーム参加前のUIを制御する
    /// </summary>
    public class UIManagerBeforeJoinRoom : MonoBehaviour
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
        /// ゲーム開始直後に呼び出される
        /// </summary>
        private void Start()
        {
            //背景を黒色に設定する
            imgBackground.color = Color.black;

            //各テキストを設定する
            txtPlaceholder.text = "パスコードを入力...";
            txtBtnMain.text = "完了";
            txtBtnSub.text = "スキップ";

            //SingleAssignmentDisposableを作成する
            var disposable = new SingleAssignmentDisposable();

            //メインボタンを押された際の処理
            disposable.Disposable = btnMain.OnClickAsObservable()
                .Where(_ => txtPlayerEntered.text == ConstData.PASSCODE)
                .Subscribe(_ =>
                {
                    //名前を入力する場面に移る
                    GoToEnterNameScene();

                    //購読を停止する
                    disposable.Dispose();
                });

            //サブボタンを押された際の処理
            btnSub.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //名前を入力する場面に移る
                    GoToEnterNameScene();
                })
                .AddTo(btnSub);

            //名前を入力する場面に移る
            void GoToEnterNameScene()
            {
                //サブボタンを消す
                Destroy(btnSub.gameObject);

                //プレイヤーが入力したテキストを空にする
                inputField.text=string.Empty;

                //テキストを変更する
                txtPlaceholder.text = "名前を入力...";
            }
        }
    }
}
