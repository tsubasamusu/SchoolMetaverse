using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using System;

namespace SchoolMetaverse
{
    /// <summary>
    /// ルーム参加前のUIを制御する
    /// </summary>
    public class UIManagerLobby : MonoBehaviour
    {
        [SerializeField]
        private Text txtPlaceholder;//スペースホルダのテキスト

        [SerializeField]
        private Button btnMain;//メインボタン

        [SerializeField]
        private Button btnSub;//サブボタン

        [SerializeField]
        private InputField inputField;//InputField

        [SerializeField]
        private PhotonController photonController;//PhotonController

        /// <summary>
        /// ゲーム開始直後に呼び出される
        /// </summary>
        private void Start()
        {
            //今、名前を入力する場面かどうか
            bool isEnterNameScene = false;

            //スペースホルダのテキストを設定する
            txtPlaceholder.text = "パスコードを入力...";

            btnMain.OnClickAsObservable()
            .Subscribe(_ =>
            {
                //名前を入力する場面なら、以降の処理を行わない
                if (isEnterNameScene) return;

                //入力されたパスコードが正しくないなら
                if (inputField.text != ConstData.PASSCODE)
                {
                    //効果音を再生する
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.無効なボタンを押した時の音);

                    //以降の処理を行わない
                    return;
                }

                //効果音を再生する
                SoundManager.instance.PlaySound(SoundDataSO.SoundName.ボタンを押した時の音);

                //名前を入力する場面に移る
                GoToEnterNameScene();
            });

            //サブボタンを押された際の処理
            btnSub.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //効果音を再生する
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.ボタンを押した時の音);

                    //名前を入力する場面に遷移する
                    GoToEnterNameScene();
                })
                .AddTo(btnSub);

            //名前を入力する場面に移る
            void GoToEnterNameScene()
            {
                //名前を入力する場面に遷移した状態に切り替える
                isEnterNameScene = true;

                //サブボタンを消す
                Destroy(btnSub.gameObject);

                //プレイヤーが入力したテキストを初期化する
                inputField.text = GameData.instance.playerName;

                //テキストを変更する
                txtPlaceholder.text = "名前を入力...";

                //メインボタンが押された際の処理
                btnMain.OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        //名前が入力されていないなら
                        if (inputField.text == string.Empty)
                        {
                            //効果音を再生する
                            SoundManager.instance.PlaySound(SoundDataSO.SoundName.無効なボタンを押した時の音);

                            //以降の処理を行わない
                            return;
                        }

                        //効果音を再生する
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.ボタンを押した時の音);

                        //プレイヤーの名前を取得する
                        GameData.instance.playerName = inputField.text;

                        //プレイヤーの名前をデバイスに保存する
                        GameData.instance.SavePlayerNameInDevice();

                        //InputFieldを消す
                        Destroy(inputField.gameObject);

                        //メインボタンを消す
                        Destroy(btnMain.gameObject);

                        //メインシーンに移る
                        StartCoroutine(GoToMain());
                    })
                    .AddTo(btnMain);
            }

            //メインシーンに移る
            IEnumerator GoToMain()
            {
                //マスターサーバーに繋ぐ
                photonController.ConnectMasterServer();

                //ルームに参加するまで待つ
                yield return new WaitUntil(() => photonController.JoinedRoom);

                //メインシーンを読み込む
                SceneManager.LoadScene("Main");
            }
        }
    }
}
