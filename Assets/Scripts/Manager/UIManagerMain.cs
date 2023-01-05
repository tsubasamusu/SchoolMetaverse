using DG.Tweening;
using Photon.Pun;
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
        private Image imgBlackBord;//黒板のイメージ

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
        private Button btnPicturePath;//画像のパスの入力完了ボタン

        [SerializeField]
        private Slider sldBgmVolume;//BGMの音量のスライダー

        [SerializeField]
        private Slider sldLookSensitivity;//視点感度のスライダー

        [SerializeField]
        private Slider sldPictureSize;//画像のサイズのスライダー

        [SerializeField]
        private Text txtMessage;//メッセージのテキスト

        [SerializeField]
        private Text txtSendPictureError;//画像送信画面のエラー表示用のテキスト

        [SerializeField]
        private InputField ifMessage;//メッセージ入力用のインプットフィールド

        [SerializeField]
        private InputField ifPicturePath;//画像のパス入力用のインプットフィールド

        [SerializeField]
        private CanvasGroup cgButton;//ボタンのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgSetting;//スライダーのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgMessage;//メッセージのキャンバスグループ

        [SerializeField]
        private CanvasGroup cgSendPicture;//画像送信用のキャンバスグループ

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        [SerializeField]
        private PictureManager pictureManager;//PictureManager

        [SerializeField]
        private RectTransform rtBlackBord;//黒板のRectTransform

        /// <summary>
        /// メッセージ入力用のインプットフィールド（取得用）
        /// </summary>
        public InputField IfMessage { get => ifMessage; }

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

            //黒板を非活性化する
            imgBlackBord.gameObject.SetActive(false);

            //メインの背景をフェードアウトさせる
            imgMainBackground.DOFade(0f, ConstData.BACKGROUND_FADE_OUT_TIME)

                //メインの背景を消す
                .OnComplete(() => Destroy(imgMainBackground.gameObject));

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

                    //画像送信画面が表示されているなら
                    if (cgSendPicture.alpha != 0f)
                    {
                        //効果音を再生する
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.無効なボタンを押した時の音);

                        //ボタンのアニメーションを行う
                        PlayButtonAnimation(btnSendPicture);

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
                    }
                    //メッセージが表示されているなら
                    else
                    {
                        //メッセージのキャンバスグループを非表示にする
                        cgMessage.alpha = 0f;

                        //サブの背景を非活性化する
                        imgSubBackground.gameObject.SetActive(false);

                        //InputFieldとメッセージ送信ボタンを非活性化する
                        ifMessage.interactable = btnSendMessage.interactable = false;
                    }
                })
                .AddTo(this);

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

                    //画像送信画面が表示されているなら
                    if (cgSendPicture.alpha != 0f)
                    {
                        //効果音を再生する
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.無効なボタンを押した時の音);

                        //ボタンのアニメーションを行う
                        PlayButtonAnimation(btnSendPicture);

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
                        GameData.instance.lookSensitivity = sldLookSensitivity.value * 10f;

                        //設定されたBGMの音量を取得する
                        GameData.instance.bgmVolume = sldBgmVolume.value;

                        //設定された視点感度をデバイスに保存する
                        GameData.instance.SavelookSensitivityInDevice();

                        //設定されたBGMの音量をデバイスに保存する
                        GameData.instance.SaveBgmVolumeInDevice();

                        //BGMの音量を更新する
                        SoundManager.instance.UpdateBgmVolume();
                    }
                })
                .AddTo(this);

            //画像送信ボタンを押された際の処理
            btnSendPicture.OnClickAsObservable()
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

                    //SingleAssignmentDisposableを作成する
                    var disposable = new SingleAssignmentDisposable();

                    //画像送信画面が表示されていないなら
                    if (cgSendPicture.alpha == 0f)
                    {
                        //画像送信用のキャンバスグループを表示する
                        cgSendPicture.alpha = 1f;

                        //サブの背景を活性化する
                        imgSubBackground.gameObject.SetActive(true);

                        //InputFieldと画像送信ボタンを活性化する
                        ifPicturePath.interactable = btnPicturePath.interactable = true;

                        //エラーを表示を空にする
                        txtSendPictureError.text = string.Empty;

                        //黒板に画像が表示されているなら
                        if (imgBlackBord.sprite != null)
                        {
                            //他のプレイヤーが画像のサイズを設定でなければ
                            if (PhotonNetwork.CurrentRoom.CustomProperties["IsSettingPictureSize"] is bool isSettingPictureSize && !isSettingPictureSize)
                            {
                                //Hashtableを作成する
                                var hashtable = new ExitGames.Client.Photon.Hashtable
                                {
                                    //ゲームサーバーに「画像のサイズを設定中」という情報を持たせる
                                    ["IsSettingPictureSize"] = true
                                };

                                //作成したカスタムプロパティを登録する
                                PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

                                //スライダーのゲームオブジェクトを活性化する
                                sldPictureSize.gameObject.SetActive(true);

                                //スライダーを活性化する
                                sldPictureSize.interactable = true;

                                //画像のサイズの変更処理
                                disposable.Disposable = this.UpdateAsObservable()
                                .Subscribe(_ =>
                                {
                                    //入力されたサイズを取得する
                                    float scale = ConstData.PICTURE_SIZE_RATIO * sldPictureSize.value;

                                    //Hashtableを作成する
                                    var hashtable = new ExitGames.Client.Photon.Hashtable
                                    {
                                        //ゲームサーバーに画像のサイズを持たせる
                                        ["PictureSize"] = scale
                                    };

                                    //作成したカスタムプロパティを登録する
                                    PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

                                    //画像のサイズを設定する
                                    rtBlackBord.localScale = new(scale, scale, scale);
                                });


                                //パス入力欄を空にする
                                ifPicturePath.text = string.Empty;
                            }
                            //他のプレイヤーが画像のサイズの変更中なら
                            else
                            {
                                //効果音を再生する
                                SoundManager.instance.PlaySound(SoundDataSO.SoundName.エラーを表示する時の音);

                                //エラーを表示する
                                SetTxtSendPictureError("他のプレイヤーが画像のサイズを変更中です。");
                            }
                        }
                    }
                    //画像送信画面が表示されているなら
                    else
                    {
                        //画像送信用のキャンバスグループを非表示にする
                        cgSendPicture.alpha = 0f;

                        //サブの背景を非活性化する
                        imgSubBackground.gameObject.SetActive(false);

                        //InputFieldと画像送信ボタンを非活性化する
                        ifPicturePath.interactable = btnPicturePath.interactable = false;

                        //黒板に画像が表示されているなら
                        if (imgBlackBord.sprite != null)
                        {
                            //スライダーのゲームオブジェクトを非活性化する
                            sldPictureSize.gameObject.SetActive(false);

                            //スライダーを非活性化する
                            sldPictureSize.interactable = false;

                            //Hashtableを作成する
                            var hashtable = new ExitGames.Client.Photon.Hashtable
                            {
                                //ゲームサーバーに「画像のサイズを設定中ではない」という情報を持たせる
                                ["IsSettingPictureSize"] = false
                            };

                            //作成したカスタムプロパティを登録する
                            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

                            //購読を停止する
                            disposable?.Dispose();
                        }
                    }
                })
                .AddTo(this);

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

            //画像のパスの入力完了ボタンを押された際の処理
            btnPicturePath.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //画像のパスが入力されていないなら
                    if (ifPicturePath.text == string.Empty)
                    {
                        //効果音を再生する
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.無効なボタンを押した時の音);

                        //以降の処理を行わない
                        return;
                    }

                    //効果音を再生する
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.送信ボタンを押した時の音);

                    //画像を送信する準備を行う
                    pictureManager.SendPicture(ifPicturePath.text);

                    //プレイヤーが入力したテキストを空にする
                    ifPicturePath.text = string.Empty;
                })
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
            ifMessage.text = string.Empty;
        }

        /// <summary>
        /// 黒板のイメージのスプライトを設定する
        /// </summary>
        /// <param name="sprite">スプライト</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        public void SetImgBlackBordSprite(Sprite sprite, float width, float height)
        {
            //黒板を活性化する
            imgBlackBord.gameObject.SetActive(true);

            //黒板のサイズを設定する
            rtBlackBord.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rtBlackBord.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            //黒板のスプライトを設定する
            imgBlackBord.sprite = sprite;

            //入力されたサイズを取得する
            float scale = ConstData.PICTURE_SIZE_RATIO * sldPictureSize.value;

            //Hashtableを作成する
            var hashtable = new ExitGames.Client.Photon.Hashtable
            {
                //ゲームサーバーに画像のサイズを持たせる
                ["PictureSize"] = scale
            };

            //作成したカスタムプロパティを登録する
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

            //大きさを初期値に設定する
            rtBlackBord.localScale = new(scale, scale, scale);
        }

        /// <summary>
        /// 画像送信画面にてエラーを表示する
        /// </summary>
        /// <param name="text">テキスト</param>
        public void SetTxtSendPictureError(string text) { txtSendPictureError.text = text; }
    }
}
