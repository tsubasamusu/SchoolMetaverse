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
    /// ���[���Q�����UI�𐧌䂷��
    /// </summary>
    public class UIManagerMain : MonoBehaviour, ISetUp
    {
        [SerializeField]
        private Image imgMainBackground;//���C���̔w�i

        [SerializeField]
        private Image imgSubBackground;//�T�u�̔w�i

        [SerializeField]
        private Image imgNotice;//�ʒm�̃C���[�W

        [SerializeField]
        private Button btnMessage;//���b�Z�[�W�{�^��

        [SerializeField]
        private Button btnSetting;//�ݒ�{�^��

        [SerializeField]
        private Button btnSendMessage;//���b�Z�[�W���M�{�^��

        [SerializeField]
        private Slider sldBgmVolume;//BGM�̉��ʂ̃X���C�_�[

        [SerializeField]
        private Slider sldLookSensitivity;//���_���x�̃X���C�_�[

        [SerializeField]
        private Text txtMessage;//���b�Z�[�W�̃e�L�X�g

        [SerializeField]
        private InputField ifMessage;//���b�Z�[�W���͗p�̃C���v�b�g�t�B�[���h

        [SerializeField]
        private CanvasGroup cgButton;//�{�^���̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgSetting;//�X���C�_�[�̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgMessage;//���b�Z�[�W�̃L�����o�X�O���[�v

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        /// <summary>
        /// ���b�Z�[�W���͗p�̃C���v�b�g�t�B�[���h�i�擾�p�j
        /// </summary>
        public InputField IfMessage { get => ifMessage; }

        /// <summary>
        /// UIManagerMain�̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //UI�̏����ݒ���s��
            SetUpUI();

            //�e�{�^���̐�����J�n����
            StartControlBtnMessage();
            StartControlBtnSetting();
            StartControlBtnSendMessage();

            //�ʒm�̃C���[�W�̐�����J�n����
            StartControlImgNotice();
        }

        /// <summary>
        /// UI�̏����ݒ���s��
        /// </summary>
        private void SetUpUI()
        {
            //���C���̔w�i�����F�ŕ\������
            imgMainBackground.color = Color.black;

            //�S�Ẵ{�^����\������
            cgButton.alpha = 1f;

            //�ʒm��񊈐�������
            imgNotice.gameObject.SetActive(false);

            //�ݒ��ʂ̃X���C�_�[��񊈐�������
            sldBgmVolume.interactable = sldLookSensitivity.interactable = false;

            //���b�Z�[�W���M�{�^����񊈐�������
            btnSendMessage.interactable = false;

            //�S�ẴX���C�_�[�ƁA���b�Z�[�W�̃L�����o�X�O���[�v���\���ɂ���
            cgSetting.alpha = cgMessage.alpha = 0f;

            //���b�Z�[�W����ɂ���
            txtMessage.text = string.Empty;

            //�T�u�̔w�i��񊈐�������
            imgSubBackground.gameObject.SetActive(false);

            //���C���̔w�i���t�F�[�h�A�E�g������
            imgMainBackground.DOFade(0f, ConstData.BACKGROUND_FADE_OUT_TIME)

                //���C���̔w�i������
                .OnComplete(() => Destroy(imgMainBackground.gameObject));
        }

        /// <summary>
        /// ���b�Z�[�W�{�^���̐�����J�n����
        /// </summary>
        private void StartControlBtnMessage()
        {
            //���b�Z�[�W�{�^���������ꂽ�ۂ̏���
            btnMessage.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //�ݒ��ʂ��\������Ă���Ȃ�
                    if (cgSetting.alpha != 0f)
                    {
                        //���ʉ����Đ�����
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.�����ȃ{�^�������������̉�);

                        //�{�^���̃A�j���[�V�������s��
                        PlayButtonAnimation(btnSetting);

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //���ʉ����Đ�����
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.�{�^�������������̉�);

                    //���b�Z�[�W���\������Ă��Ȃ��Ȃ�
                    if (cgMessage.alpha == 0f)
                    {
                        //���b�Z�[�W�̃L�����o�X�O���[�v��\������
                        cgMessage.alpha = 1f;

                        //�T�u�̔w�i������������
                        imgSubBackground.gameObject.SetActive(true);

                        //InputField�ƃ��b�Z�[�W���M�{�^��������������
                        ifMessage.interactable = btnSendMessage.interactable = true;

                        //���b�Z�[�W���͗�����ɂ���
                        ifMessage.text = string.Empty;

                        //�ʒm���\���ɂ���
                        imgNotice.gameObject.SetActive(false);

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //���b�Z�[�W�̃L�����o�X�O���[�v���\���ɂ���
                    cgMessage.alpha = 0f;

                    //�T�u�̔w�i��񊈐�������
                    imgSubBackground.gameObject.SetActive(false);

                    //InputField�ƃ��b�Z�[�W���M�{�^����񊈐�������
                    ifMessage.interactable = btnSendMessage.interactable = false;
                })
                .AddTo(this);
        }

        /// <summary>
        /// �ݒ�{�^���̐�����J�n����
        /// </summary>
        private void StartControlBtnSetting()
        {
            //�ݒ�{�^���������ꂽ�ۂ̏���
            btnSetting.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //���b�Z�[�W��ʂ��\������Ă���Ȃ�
                    if (cgMessage.alpha != 0f)
                    {
                        //���ʉ����Đ�����
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.�����ȃ{�^�������������̉�);

                        //�{�^���̃A�j���[�V�������s��
                        PlayButtonAnimation(btnMessage);

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //���ʉ����Đ�����
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.�{�^�������������̉�);

                    //�ݒ肪�\������Ă��Ȃ��Ȃ�
                    if (cgSetting.alpha == 0f)
                    {
                        //�ݒ�̃L�����o�X�O���[�v��\������
                        cgSetting.alpha = 1f;

                        //�T�u�̔w�i������������
                        imgSubBackground.gameObject.SetActive(true);

                        //BGM�̉��ʂ̃X���C�_�[�ƁA���_���x�̃X���C�_�[������������
                        sldBgmVolume.interactable = sldLookSensitivity.interactable = true;

                        //���_���x�̃X���C�_�[�̏����l��ݒ肷��
                        sldLookSensitivity.value = GameData.instance.lookSensitivity / 10f;

                        //BGM�̉��ʂ̃X���C�_�[�̏����l��ݒ肷��
                        sldBgmVolume.value = GameData.instance.bgmVolume;

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //�ݒ�̃L�����o�X�O���[�v���\���ɂ���
                    cgSetting.alpha = 0f;

                    //�T�u�̔w�i��񊈐�������
                    imgSubBackground.gameObject.SetActive(false);

                    //BGM�̉��ʂ̃X���C�_�[�ƁA���_���x�̃X���C�_�[��񊈐�������
                    sldBgmVolume.interactable = sldLookSensitivity.interactable = false;

                    //�ݒ肳�ꂽ���_���x���擾����
                    GameData.instance.lookSensitivity = sldLookSensitivity.value * 10f;

                    //�ݒ肳�ꂽBGM�̉��ʂ��擾����
                    GameData.instance.bgmVolume = sldBgmVolume.value;

                    //�ݒ肳�ꂽ���_���x���f�o�C�X�ɕۑ�����
                    GameData.instance.SavelookSensitivityInDevice();

                    //�ݒ肳�ꂽBGM�̉��ʂ��f�o�C�X�ɕۑ�����
                    GameData.instance.SaveBgmVolumeInDevice();

                    //BGM�̉��ʂ��X�V����
                    SoundManager.instance.UpdateBgmVolume();
                })
                .AddTo(this);
        }

        /// <summary>
        /// ���b�Z�[�W���M�{�^���̐�����J�n����
        /// </summary>
        private void StartControlBtnSendMessage()
        {
            //���b�Z�[�W���M�{�^���������ꂽ�ۂ̏���
            btnSendMessage.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //���b�Z�[�W�����͂���Ă��Ȃ��Ȃ�
                    if (ifMessage.text == string.Empty)
                    {
                        //���ʉ����Đ�����
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.�����ȃ{�^�������������̉�);

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //���ʉ����Đ�����
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.���M�{�^�������������̉�);

                    //���b�Z�[�W���M�̏������s��
                    messageManager.PrepareSendMessage(GameData.instance.playerName, ifMessage.text);
                })
                .AddTo(this);
        }

        /// <summary>
        /// �{�^���̃A�j���[�V�������s��
        /// </summary>
        /// <param name="button">�{�^��</param>
        private void PlayButtonAnimation(Button button) { button.transform.DOScale(ConstData.BUTTON_ANIMATION_SIZE, 0.25f).SetLoops(2, LoopType.Yoyo).SetLink(button.gameObject); }

        /// <summary>
        /// �u���̃v���C���[���摜�̃T�C�Y��ύX�����ǂ����v�𒲂ׂ�
        /// </summary>
        /// <returns>���̃v���C���[���摜�̃T�C�Y��ύX�����ǂ���</returns>
        private bool CheckIsSettingPictureSizeOther()
        {
            //�������[���ɋ��鑼�̃v���C���[�̐������J��Ԃ�
            for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++)
            {
                //�J��Ԃ������Ŏ擾�����v���C���[���A�摜�̃T�C�Y��ύX���Ȃ�Atrue��Ԃ�
                if (PhotonNetwork.PlayerListOthers[i].CustomProperties["IsSettingPictureSize"] is bool isSettingPictureSize
                    && isSettingPictureSize) return true;
            }

            //false��Ԃ�
            return false;
        }

        /// <summary>
        /// �ʒm�̃C���[�W�̐�����J�n����
        /// </summary>
        private void StartControlImgNotice()
        {
            //���M�񐔁i�L���p�j
            int latestCount = 0;
            int newCount = 0;

            //�ʒm�̐��䏈��
            this.UpdateAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(ConstData.CHECK_MESSAGES_SPAN))
                .Subscribe(_ =>
                {
                    //�Q�[���T�[�o�[�Ƀ��b�Z�[�W�̑��M�񐔂̏�񂪂Ȃ��Ȃ�A�ȍ~�̏������s��Ȃ�
                    if (PhotonNetwork.CurrentRoom.CustomProperties["SendMessageCount"] is not int) return;

                    //���b�Z�[�W�̑��M�񐔂������Ă��Ȃ��Ȃ�A�ȍ~�̏������s��Ȃ�
                    if (!IncreasedSendMessageCount()) return;

                    //���b�Z�[�W��ʂ��\������Ă��Ȃ��Ȃ�A�ʒm��\������
                    if (cgMessage.alpha == 0f) imgNotice.gameObject.SetActive(true);
                })
                .AddTo(this);

            //���b�Z�[�W�̑��M�񐔂����������ǂ���
            bool IncreasedSendMessageCount()
            {
                //�Q�[���T�[�o�[�ɕۑ�����Ă���A���b�Z�[�W�̑��M�񐔂��擾����
                newCount = (int)PhotonNetwork.CurrentRoom.CustomProperties["SendMessageCount"];

                //���M�񐔂������Ă���Ȃ�
                if (newCount > latestCount)
                {
                    //�Q�[���T�[�o�[�ɕۑ�����Ă���A���b�Z�[�W�̑��M�񐔂��擾����
                    latestCount = newCount;

                    //true��Ԃ�
                    return true;
                }

                //�Q�[���T�[�o�[�ɕۑ�����Ă���A���b�Z�[�W�̑��M�񐔂��擾����
                latestCount = newCount;

                //false��Ԃ�
                return false;
            }
        }

        /// <summary>
        /// ���b�Z�[�W�̃e�L�X�g���X�V����
        /// </summary>
        public void UpdateTxtMessage()
        {
            //���b�Z�[�W�i�ێ��p�j
            string message = string.Empty;

            //���b�Z�[�W�̔z��̗v�f�������J��Ԃ�
            for (int i = 0; i < GameData.instance.messages.Length; i++)
            {
                //���b�Z�[�W�̃e�L�X�g��ݒ肷��
                message += GameData.instance.messages[i];
            }

            //���b�Z�[�W�̃e�L�X�g��ݒ肷��
            txtMessage.text = message;

            //���b�Z�[�W���͗�����ɂ���
            ifMessage.text = string.Empty;
        }
    }
}
