using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolMetaverse
{
    /// <summary>
    /// ルーム参加後のUIを制御する
    /// </summary>
    public class UIManagerMain : MonoBehaviour, ISetUp
    {
        [SerializeField]
        private Image imgMainBackground;//メインの背景

        [SerializeField]
        private Image imgSubBackground;//サブの背景

        [SerializeField]
        private Button btnSendPicture;//画像送信ボタン

        [SerializeField]
        private Button btnMessage;//メッセージボタン

        [SerializeField]
        private Button btnMute;//ミュートボタン

        [SerializeField]
        private Button btnSetting;//設定ボタン

        [SerializeField]
        private Button btnSendMessage;//メッセージ送信ボタン

        [SerializeField]
        private Slider sldBgmVolume;//BGMの音量のスライダー

        [SerializeField]
        private Slider sldLookSensitivity;//視点感度のスライダー

        [SerializeField]
        private Text txtMessage;//メッセージのテキスト

        [SerializeField]
        private InputField inputField;//InputField

        [SerializeField]
        private CanvasGroup cgButton;//ボタンのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgSetting;//スライダーのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgMessage;//メッセージのキャンバスグループ

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        /// <summary>
        /// InputField（取得用）
        /// </summary>
        public InputField InputField { get => inputField; }

        /// <summary>
        /// UIManagerMainの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //メインの背景を黒色で表示する
            imgMainBackground.color = Color.black;

            //全てのボタンを表示する
            cgButton.alpha = 1f;

            //全てのスライダーと、メッセージのキャンバスグループを非表示にする
            cgSetting.alpha = cgMessage.alpha = 0f;

            //メッセージを空にする
            txtMessage.text = string.Empty;

            //サブの背景を非活性化する
            imgSubBackground.gameObject.SetActive(false);

            //メインの背景をフェードアウトさせる
            imgMainBackground.DOFade(0f, ConstData.BACKGROUND_FADE_OUT_TIME)

                //メインの背景を消す
                .OnComplete(() => Destroy(imgMainBackground.gameObject));

            //メッセージボタンを押された際の処理
            btnMessage.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //設定が表示されているなら
                    if (cgSetting.alpha != 0f)
                    {
                        //ボタンのアニメーションを行う
                        PlayButtonAnimation(btnSetting);

                        //以降の処理を行わない
                        return;
                    }

                    //メッセージが表示されていないなら
                    if (cgMessage.alpha == 0f)
                    {
                        //メッセージのキャンバスグループを表示する
                        cgMessage.alpha = 1f;

                        //サブの背景を活性化する
                        imgSubBackground.gameObject.SetActive(true);

                        //InputFieldとメッセージ送信ボタンを活性化する
                        inputField.interactable = btnSendMessage.interactable = true;

                        //メッセージ入力欄を空にする
                        inputField.text = string.Empty;
                    }
                    //メッセージが表示されているなら
                    else
                    {
                        //メッセージのキャンバスグループを非表示にする
                        cgMessage.alpha = 0f;

                        //サブの背景を非活性化する
                        imgSubBackground.gameObject.SetActive(false);

                        //InputFieldとメッセージ送信ボタンを非活性化する
                        inputField.interactable = btnSendMessage.interactable = false;
                    }
                })
                .AddTo(this);

            //設定ボタンを押された際の処理
            btnSetting.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //メッセージが表示されているなら
                    if (cgMessage.alpha != 0f)
                    {
                        //ボタンのアニメーションを行う
                        PlayButtonAnimation(btnMessage);

                        //以降の処理を行わない
                        return;
                    }

                    //設定が表示されていないなら
                    if (cgSetting.alpha == 0f)
                    {
                        //設定のキャンバスグループを表示する
                        cgSetting.alpha = 1f;

                        //サブの背景を活性化する
                        imgSubBackground.gameObject.SetActive(true);

                        //BGMの音量のスライダーと、視点感度のスライダーを活性化する
                        sldBgmVolume.interactable = sldLookSensitivity.interactable = true;

                        //視点感度のスライダーの初期値を設定する
                        sldLookSensitivity.value = GameData.instance.lookSensitivity / 10f;
                    }
                    //設定が表示されているなら
                    else
                    {
                        //設定のキャンバスグループを非表示にする
                        cgSetting.alpha = 0f;

                        //サブの背景を非活性化する
                        imgSubBackground.gameObject.SetActive(false);

                        //BGMの音量のスライダーと、視点感度のスライダーを非活性化する
                        sldBgmVolume.interactable = sldLookSensitivity.interactable = false;

                        //設定された視点感度を取得する
                        GameData.instance.lookSensitivity= sldLookSensitivity.value * 10f;

                        //設定された視点感度をデバイスに保存する
                        GameData.instance.SavelookSensitivityInDevice();
                    }
                })
                .AddTo(this);

            //メッセージ送信ボタンを押された際の処理
            btnSendMessage.OnClickAsObservable()
                .Where(_ => inputField.text != string.Empty)
                .Subscribe(_ => { messageManager.PrepareSendMessage(GameData.instance.playerName, inputField.text); })
                .AddTo(this);

            //画像送信ボタンを押された際の処理
            btnSendPicture.OnClickAsObservable()
                .Subscribe(_ => Application.OpenURL(ConstData.SEND_PICTURE_URL))
                .AddTo(this);

            //ボタンのアニメーションを行う
            static void PlayButtonAnimation(Button button) { button.transform.DOScale(ConstData.BUTTON_ANIMATION_SIZE, 0.25f).SetLoops(2, LoopType.Yoyo).SetLink(button.gameObject); }
        }

        /// <summary>
        /// メッセージのテキストを更新する
        /// </summary>
        public void UpdateTxtMessage()
        {
            //メッセージ（保持用）
            string message = string.Empty;

            //メッセージの配列の要素数だけ繰り返す
            for (int i = 0; i < GameData.instance.messages.Length; i++)
            {
                //メッセージのテキストを設定する
                message += GameData.instance.messages[i];
            }

            //メッセージのテキストを設定する
            txtMessage.text = message;

            //メッセージ入力欄を空にする
            inputField.text = string.Empty;
        }
    }
}
