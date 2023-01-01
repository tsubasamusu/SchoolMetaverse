using System.Collections;
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
        private Text txtPlayerEntered;//プレイヤーが入力したテキスト

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
            //スペースホルダのテキストを設定する
            txtPlaceholder.text = "パスコードを入力...";

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
                .Subscribe(_ =>GoToEnterNameScene())
                .AddTo(btnSub);

            //名前を入力する場面に移る
            void GoToEnterNameScene()
            {
                //サブボタンを消す
                Destroy(btnSub.gameObject);

                //プレイヤーが入力したテキストを初期化する
                inputField.text=GameData.instance.playerName;

                //テキストを変更する
                txtPlaceholder.text = "名前を入力...";

                //メインボタンが押された際の処理
                btnMain.OnClickAsObservable()
                    .Where(_=>txtPlayerEntered.text!=string.Empty)
                    .Subscribe(_ =>
                    {
                        //プレイヤーの名前を取得する
                        GameData.instance.playerName = txtPlayerEntered.text;

                        //プレイヤーの名前をデバイスに保存する
                        GameData.instance.SetDevicePlayerName();

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
