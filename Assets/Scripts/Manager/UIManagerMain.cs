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
        private Button btnPicture;//画像送信ボタン

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
        private Slider sldPictureSize;//画像のサイズのスライダー

        [SerializeField]
        private Text txtMessage;//メッセージのテキスト

        [SerializeField]
        private Text txtPlayerEntered;//プレイヤーが入力したテキスト

        [SerializeField]
        private InputField inputField;//InputField

        [SerializeField]
        private CanvasGroup cgButton;//ボタンのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgSetting;//スライダーのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgMessage;//メッセージのキャンバスグループ

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

                        //BGMの音量のスライダーと、画像のサイズのスライダーを活性化する
                        sldBgmVolume.interactable = sldPictureSize.interactable = true;
                    }
                    //設定が表示されているなら
                    else
                    {
                        //設定のキャンバスグループを非表示にする
                        cgSetting.alpha = 0f;

                        //サブの背景を非活性化する
                        imgSubBackground.gameObject.SetActive(false);

                        //BGMの音量のスライダーと、画像のサイズのスライダーを非活性化する
                        sldBgmVolume.interactable = sldPictureSize.interactable = false;
                    }
                })
                .AddTo(this);

            //ボタンのアニメーションを行う
            void PlayButtonAnimation(Button button)
            {
                button.transform.DOScale(ConstData.BUTTON_ANIMATION_SIZE, 0.25f)
                    .SetLoops(2, LoopType.Yoyo).SetLink(button.gameObject);
            }
        }
    }
}
