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
        private Slider sldBgmVolume;//BGM�̉��ʂ̃X���C�_�[

        [SerializeField]
        private Slider sldLookSensitivity;//���_���x�̃X���C�_�[

        [SerializeField]
        private Text txtMessage;//���b�Z�[�W�̃e�L�X�g

        [SerializeField]
        private InputField inputField;//InputField

        [SerializeField]
        private CanvasGroup cgButton;//�{�^���̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgSetting;//�X���C�_�[�̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgMessage;//���b�Z�[�W�̃L�����o�X�O���[�v

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        /// <summary>
        /// InputField�i�擾�p�j
        /// </summary>
        public InputField InputField { get => inputField; }

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
                    //�ݒ肪�\������Ă���Ȃ�
                    if (cgSetting.alpha != 0f)
                    {
                        //�{�^���̃A�j���[�V�������s��
                        PlayButtonAnimation(btnSetting);

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

                    //���b�Z�[�W���\������Ă��Ȃ��Ȃ�
                    if (cgMessage.alpha == 0f)
                    {
                        //���b�Z�[�W�̃L�����o�X�O���[�v��\������
                        cgMessage.alpha = 1f;

                        //�T�u�̔w�i������������
                        imgSubBackground.gameObject.SetActive(true);

                        //InputField�ƃ��b�Z�[�W���M�{�^��������������
                        inputField.interactable = btnSendMessage.interactable = true;

                        //���b�Z�[�W���͗�����ɂ���
                        inputField.text = string.Empty;
                    }
                    //���b�Z�[�W���\������Ă���Ȃ�
                    else
                    {
                        //���b�Z�[�W�̃L�����o�X�O���[�v���\���ɂ���
                        cgMessage.alpha = 0f;

                        //�T�u�̔w�i��񊈐�������
                        imgSubBackground.gameObject.SetActive(false);

                        //InputField�ƃ��b�Z�[�W���M�{�^����񊈐�������
                        inputField.interactable = btnSendMessage.interactable = false;
                    }
                })
                .AddTo(this);

            //�ݒ�{�^���������ꂽ�ۂ̏���
            btnSetting.OnClickAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    //���b�Z�[�W���\������Ă���Ȃ�
                    if (cgMessage.alpha != 0f)
                    {
                        //�{�^���̃A�j���[�V�������s��
                        PlayButtonAnimation(btnMessage);

                        //�ȍ~�̏������s��Ȃ�
                        return;
                    }

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
                        GameData.instance.lookSensitivity= sldLookSensitivity.value * 10f;

                        //�ݒ肳�ꂽ���_���x���f�o�C�X�ɕۑ�����
                        GameData.instance.SavelookSensitivityInDevice();
                    }
                })
                .AddTo(this);

            //���b�Z�[�W���M�{�^���������ꂽ�ۂ̏���
            btnSendMessage.OnClickAsObservable()
                .Where(_ => inputField.text != string.Empty)
                .Subscribe(_ => { messageManager.PrepareSendMessage(GameData.instance.playerName, inputField.text); })
                .AddTo(this);

            //�摜���M�{�^���������ꂽ�ۂ̏���
            btnSendPicture.OnClickAsObservable()
                .Subscribe(_ => Application.OpenURL(ConstData.SEND_PICTURE_URL))
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
            inputField.text = string.Empty;
        }
    }
}
