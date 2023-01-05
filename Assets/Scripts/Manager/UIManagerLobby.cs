using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        private Image imgLoad;//ロード中のイメージ

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

            //ロード中のイメージを非表示にする
            imgLoad.DOFade(0f, 0f);

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

                //ロード中のアニメーションを行う
                PlayLoadAnimation(this.GetCancellationTokenOnDestroy()).Forget();

                //ルームに参加するまで待つ
                yield return new WaitUntil(() => photonController.JoinedRoom);

                //メインシーンを読み込む
                SceneManager.LoadScene("Main");
            }

            //ロード中のアニメーションを行う
            async UniTaskVoid PlayLoadAnimation(CancellationToken token)
            {
                //ロード中のイメージを表示する
                imgLoad.DOFade(1f, 0f);

                //無限に繰り返す
                while(true)
                {
                    //回転する
                    imgLoad.transform.Rotate(0f,0f,-360f/8f);

                    //一定時間待つ
                    await UniTask.Delay(TimeSpan.FromSeconds(ConstData.IMG_LOAD_ROT_SPAN), cancellationToken: token);
                }
            }
        }
    }
}
