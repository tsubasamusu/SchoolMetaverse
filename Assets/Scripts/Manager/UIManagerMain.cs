using DG.Tweening;
using Photon.Pun;
using System.Collections;
using UniRx;
using UniRx.Triggers;
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
        private Image imgNotice;//通知のイメージ

        [SerializeField]
        private Button btnMessage;//メッセージボタン

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
        private InputField ifMessage;//メッセージ入力用のインプットフィールド

        [SerializeField]
        private CanvasGroup cgButton;//ボタンのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgSetting;//スライダーのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgMessage;//メッセージのキャンバスグループ

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        /// <summary>
        /// メッセージ入力用のインプットフィールド（取得用）
        /// </summary>
        public InputField IfMessage { get => ifMessage; }

        /// <summary>
        /// UIManagerMainの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //UIの初期設定を行う
            SetUpUI();

            //各ボタンの制御を開始する
            StartControlBtnMessage();
            StartControlBtnSetting();
            StartControlBtnSendMessage();

            //通知のイメージの制御を開始する
            StartControlImgNotice();
        }

        /// <summary>
        /// UIの初期設定を行う
        /// </summary>
        private void SetUpUI()
        {
            //メインの背景を黒色で表示する
            imgMainBackground.color = Color.black;

            //全てのボタンを表示する
            cgButton.alpha = 1f;

            //通知を非活性化する
            imgNotice.gameObject.SetActive(false);

            //設定画面のスライダーを非活性化する
            sldBgmVolume.interactable = sldLookSensitivity.interactable = false;

            //メッセージ送信ボタンを非活性化する
            btnSendMessage.interactable = false;

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
        }

        /// <summary>
        /// メッセージボタンの制御を開始する
        /// </summary>
        private void StartControlBtnMessage()
        {
            //メッセージボタンを押された際の処理
            btnMessage.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //設定画面が表示されているなら
                    if (cgSetting.alpha != 0f)
                    {
                        //効果音を再生する
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.無効なボタンを押した時の音);

                        //ボタンのアニメーションを行う
                        PlayButtonAnimation(btnSetting);

                        //以降の処理を行わない
                        return;
                    }

                    //効果音を再生する
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.ボタンを押した時の音);

                    //メッセージが表示されていないなら
                    if (cgMessage.alpha == 0f)
                    {
                        //メッセージのキャンバスグループを表示する
                        cgMessage.alpha = 1f;

                        //サブの背景を活性化する
                        imgSubBackground.gameObject.SetActive(true);

                        //InputFieldとメッセージ送信ボタンを活性化する
                        ifMessage.interactable = btnSendMessage.interactable = true;

                        //メッセージ入力欄を空にする
                        ifMessage.text = string.Empty;

                        //通知を非表示にする
                        imgNotice.gameObject.SetActive(false);

                        //以降の処理を行わない
                        return;
                    }

                    //メッセージのキャンバスグループを非表示にする
                    cgMessage.alpha = 0f;

                    //サブの背景を非活性化する
                    imgSubBackground.gameObject.SetActive(false);

                    //InputFieldとメッセージ送信ボタンを非活性化する
                    ifMessage.interactable = btnSendMessage.interactable = false;
                })
                .AddTo(this);
        }

        /// <summary>
        /// 設定ボタンの制御を開始する
        /// </summary>
        private void StartControlBtnSetting()
        {
            //設定ボタンを押された際の処理
            btnSetting.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //メッセージ画面が表示されているなら
                    if (cgMessage.alpha != 0f)
                    {
                        //効果音を再生する
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.無効なボタンを押した時の音);

                        //ボタンのアニメーションを行う
                        PlayButtonAnimation(btnMessage);

                        //以降の処理を行わない
                        return;
                    }

                    //効果音を再生する
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.ボタンを押した時の音);

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

                        //BGMの音量のスライダーの初期値を設定する
                        sldBgmVolume.value = GameData.instance.bgmVolume;

                        //以降の処理を行わない
                        return;
                    }

                    //設定のキャンバスグループを非表示にする
                    cgSetting.alpha = 0f;

                    //サブの背景を非活性化する
                    imgSubBackground.gameObject.SetActive(false);

                    //BGMの音量のスライダーと、視点感度のスライダーを非活性化する
                    sldBgmVolume.interactable = sldLookSensitivity.interactable = false;

                    //設定された視点感度を取得する
                    GameData.instance.lookSensitivity = sldLookSensitivity.value * 10f;

                    //設定されたBGMの音量を取得する
                    GameData.instance.bgmVolume = sldBgmVolume.value;

                    //設定された視点感度をデバイスに保存する
                    GameData.instance.SavelookSensitivityInDevice();

                    //設定されたBGMの音量をデバイスに保存する
                    GameData.instance.SaveBgmVolumeInDevice();

                    //BGMの音量を更新する
                    SoundManager.instance.UpdateBgmVolume();
                })
                .AddTo(this);
        }

        /// <summary>
        /// メッセージ送信ボタンの制御を開始する
        /// </summary>
        private void StartControlBtnSendMessage()
        {
            //メッセージ送信ボタンを押された際の処理
            btnSendMessage.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //メッセージが入力されていないなら
                    if (ifMessage.text == string.Empty)
                    {
                        //効果音を再生する
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.無効なボタンを押した時の音);

                        //以降の処理を行わない
                        return;
                    }

                    //効果音を再生する
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.送信ボタンを押した時の音);

                    //メッセージ送信の準備を行う
                    messageManager.PrepareSendMessage(GameData.instance.playerName, ifMessage.text);
                })
                .AddTo(this);
        }

        /// <summary>
        /// ボタンのアニメーションを行う
        /// </summary>
        /// <param name="button">ボタン</param>
        private void PlayButtonAnimation(Button button) { button.transform.DOScale(ConstData.BUTTON_ANIMATION_SIZE, 0.25f).SetLoops(2, LoopType.Yoyo).SetLink(button.gameObject); }

        /// <summary>
        /// 「他のプレイヤーが画像のサイズを変更中かどうか」を調べる
        /// </summary>
        /// <returns>他のプレイヤーが画像のサイズを変更中かどうか</returns>
        private bool CheckIsSettingPictureSizeOther()
        {
            //同じルームに居る他のプレイヤーの数だけ繰り返す
            for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++)
            {
                //繰り返し処理で取得したプレイヤーが、画像のサイズを変更中なら、trueを返す
                if (PhotonNetwork.PlayerListOthers[i].CustomProperties["IsSettingPictureSize"] is bool isSettingPictureSize
                    && isSettingPictureSize) return true;
            }

            //falseを返す
            return false;
        }

        /// <summary>
        /// 通知のイメージの制御を開始する
        /// </summary>
        private void StartControlImgNotice()
        {
            //送信回数（記憶用）
            int latestCount = 0;
            int newCount = 0;

            //通知の制御処理
            this.UpdateAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(ConstData.CHECK_MESSAGES_SPAN))
                .Subscribe(_ =>
                {
                    //ゲームサーバーにメッセージの送信回数の情報がないなら、以降の処理を行わない
                    if (PhotonNetwork.CurrentRoom.CustomProperties["SendMessageCount"] is not int) return;

                    //メッセージの送信回数が増えていないなら、以降の処理を行わない
                    if (!IncreasedSendMessageCount()) return;

                    //メッセージ画面が表示されていないなら、通知を表示する
                    if (cgMessage.alpha == 0f) imgNotice.gameObject.SetActive(true);
                })
                .AddTo(this);

            //メッセージの送信回数が増えたかどうか
            bool IncreasedSendMessageCount()
            {
                //ゲームサーバーに保存されている、メッセージの送信回数を取得する
                newCount = (int)PhotonNetwork.CurrentRoom.CustomProperties["SendMessageCount"];

                //送信回数が増えているなら
                if (newCount > latestCount)
                {
                    //ゲームサーバーに保存されている、メッセージの送信回数を取得する
                    latestCount = newCount;

                    //trueを返す
                    return true;
                }

                //ゲームサーバーに保存されている、メッセージの送信回数を取得する
                latestCount = newCount;

                //falseを返す
                return false;
            }
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
            ifMessage.text = string.Empty;
        }
    }
}
