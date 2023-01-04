using DG.Tweening;
using UniRx;
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
        private Button btnSendPicture;//�摜���M�{�^��

        [SerializeField]
        private Button btnMessage;//���b�Z�[�W�{�^��

        [SerializeField]
        private Button btnMute;//�~���[�g�{�^��

        [SerializeField]
        private Button btnSetting;//�ݒ�{�^��

        [SerializeField]
        private Button btnSendMessage;//���b�Z�[�W���M�{�^��

        [SerializeField]
        private Button btnPicturePath;//�摜�̃p�X�̓��͊����{�^��

        [SerializeField]
        private Slider sldBgmVolume;//BGM�̉��ʂ̃X���C�_�[

        [SerializeField]
        private Slider sldLookSensitivity;//���_���x�̃X���C�_�[

        [SerializeField]
        private Text txtMessage;//���b�Z�[�W�̃e�L�X�g

        [SerializeField]
        private InputField ifMessage;//���b�Z�[�W���͗p�̃C���v�b�g�t�B�[���h

        [SerializeField]
        private InputField ifPicturePath;//�摜�̃p�X���͗p�̃C���v�b�g�t�B�[���h

        [SerializeField]
        private CanvasGroup cgButton;//�{�^���̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgSetting;//�X���C�_�[�̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgMessage;//���b�Z�[�W�̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgSendPicture;//�摜���M�p�̃L�����o�X�O���[�v

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        [SerializeField]
        private PictureManager pictureManager;//PictureManager

        /// <summary>
        /// ���b�Z�[�W���͗p�̃C���v�b�g�t�B�[���h�i�擾�p�j
        /// </summary>
        public InputField IfMessage { get => ifMessage; }

        /// <summary>
        /// �摜�̃p�X���͗p�̃C���v�b�g�t�B�[���h�i�擾�p�j
        /// </summary>
        public InputField IfPicturePath { get => ifPicturePath; }

        /// <summary>
        /// UIManagerMain�̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //���C���̔w�i�����F�ŕ\������
            imgMainBackground.color = Color.black;

            //�S�Ẵ{�^����\������
            cgButton.alpha = 1f;

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

            //���b�Z�[�W�{�^���������ꂽ�ۂ̏���
            btnMessage.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //���̉�ʂ��\������Ă���Ȃ�
                    if (cgSetting.alpha != 0f||cgSendPicture.alpha!=0f)
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
                    }
                    //���b�Z�[�W���\������Ă���Ȃ�
                    else
                    {
                        //���b�Z�[�W�̃L�����o�X�O���[�v���\���ɂ���
                        cgMessage.alpha = 0f;

                        //�T�u�̔w�i��񊈐�������
                        imgSubBackground.gameObject.SetActive(false);

                        //InputField�ƃ��b�Z�[�W���M�{�^����񊈐�������
                        ifMessage.interactable = btnSendMessage.interactable = false;
                    }
                })
                .AddTo(this);

            //�ݒ�{�^���������ꂽ�ۂ̏���
            btnSetting.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //���̉�ʂ��\������Ă���Ȃ�
                    if (cgMessage.alpha != 0f||cgSendPicture.alpha!=0f)
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
                    }
                    //�ݒ肪�\������Ă���Ȃ�
                    else
                    {
                        //�ݒ�̃L�����o�X�O���[�v���\���ɂ���
                        cgSetting.alpha = 0f;

                        //�T�u�̔w�i��񊈐�������
                        imgSubBackground.gameObject.SetActive(false);

                        //BGM�̉��ʂ̃X���C�_�[�ƁA���_���x�̃X���C�_�[��񊈐�������
                        sldBgmVolume.interactable = sldLookSensitivity.interactable = false;

                        //�ݒ肳�ꂽ���_���x���擾����
                        GameData.instance.lookSensitivity = sldLookSensitivity.value * 10f;

                        //�ݒ肳�ꂽBGM�̉��ʂ��擾����
                        GameData.instance.bgmVolume=sldBgmVolume.value;

                        //�ݒ肳�ꂽ���_���x���f�o�C�X�ɕۑ�����
                        GameData.instance.SavelookSensitivityInDevice();

                        //�ݒ肳�ꂽBGM�̉��ʂ��f�o�C�X�ɕۑ�����
                        GameData.instance.SaveBgmVolumeInDevice();

                        //BGM�̉��ʂ��X�V����
                        SoundManager.instance.UpdateBgmVolume();
                    }
                })
                .AddTo(this);

            //�摜���M�{�^���������ꂽ�ۂ̏���
            btnSendPicture.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //���̉�ʂ��\������Ă���Ȃ�
                    if (cgMessage.alpha != 0f || cgSetting.alpha != 0f)
                    {
                        //���ʉ����Đ�����
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.�����ȃ{�^�������������̉�);

                        //�{�^���̃A�j���[�V�������s��
                        PlayButtonAnimation(btnSendPicture);

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //���ʉ����Đ�����
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.�{�^�������������̉�);

                    //�摜���M��ʂ��\������Ă��Ȃ��Ȃ�
                    if (cgSendPicture.alpha == 0f)
                    {
                        //�摜���M�p�̃L�����o�X�O���[�v��\������
                        cgSendPicture.alpha = 1f;

                        //�T�u�̔w�i������������
                        imgSubBackground.gameObject.SetActive(true);

                        //InputField�Ɖ摜���M�{�^��������������
                        ifPicturePath.interactable = btnPicturePath.interactable = true;

                        //�p�X���͗�����ɂ���
                        ifPicturePath.text = string.Empty;
                    }
                    //�摜���M��ʂ��\������Ă���Ȃ�
                    else
                    {
                        //�摜���M�p�̃L�����o�X�O���[�v���\���ɂ���
                        cgSendPicture.alpha = 0f;

                        //�T�u�̔w�i��񊈐�������
                        imgSubBackground.gameObject.SetActive(false);

                        //InputField�Ɖ摜���M�{�^����񊈐�������
                        ifPicturePath.interactable = btnPicturePath.interactable = false;
                    }
                })
                .AddTo(this);

            //���b�Z�[�W���M�{�^���������ꂽ�ۂ̏���
            btnSendMessage.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //���b�Z�[�W�����͂���Ă��Ȃ��Ȃ�
                    if(ifMessage.text == string.Empty)
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

            //�摜�̃p�X�̓��͊����{�^���������ꂽ�ۂ̏���
            btnPicturePath.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //�摜�̃p�X�����͂���Ă��Ȃ��Ȃ�
                    if (ifPicturePath.text == string.Empty)
                    {
                        //���ʉ����Đ�����
                        SoundManager.instance.PlaySound(SoundDataSO.SoundName.�����ȃ{�^�������������̉�);

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //���ʉ����Đ�����
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.���M�{�^�������������̉�);

                    //�摜�𑗐M����
                    pictureManager.SendPicture();

                    //�v���C���[�����͂����e�L�X�g����ɂ���
                    ifPicturePath.text = string.Empty;
                })
                .AddTo(this);

            //�{�^���̃A�j���[�V�������s��
            static void PlayButtonAnimation(Button button) { button.transform.DOScale(ConstData.BUTTON_ANIMATION_SIZE, 0.25f).SetLoops(2, LoopType.Yoyo).SetLink(button.gameObject); }
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
