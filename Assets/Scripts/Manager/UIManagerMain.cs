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
        private Button btnPicture;//�摜���M�{�^��

        [SerializeField]
        private Button btnMessage;//���b�Z�[�W�{�^��

        [SerializeField]
        private Button btnMute;//�~���[�g�{�^��

        [SerializeField]
        private Button btnSetting;//�ݒ�{�^��

        [SerializeField]
        private Slider sldBgmVolume;//BGM�̉��ʂ̃X���C�_�[

        [SerializeField]
        private Slider sldPictureSize;//�摜�̃T�C�Y�̃X���C�_�[

        [SerializeField]
        private Text txtMessage;//���b�Z�[�W�̃e�L�X�g

        [SerializeField]
        private Text txtPlayerEntered;//�v���C���[�����͂����e�L�X�g

        [SerializeField]
        private InputField inputField;//InputField

        [SerializeField]
        private CanvasGroup cgButton;//�{�^���̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgSlider;//�X���C�_�[�̃L�����o�X�O���[�v

        [SerializeField]
        private CanvasGroup cgMessage;//���b�Z�[�W�̃L�����o�X�O���[�v

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
            cgSlider.alpha = cgMessage.alpha = 0f;

            //�T�u�̔w�i��񊈐�������
            imgSubBackground.gameObject.SetActive(false);

            //���C���̔w�i���t�F�[�h�A�E�g������
            imgMainBackground.DOFade(0f, ConstData.BACKGROUND_FADE_OUT_TIME)

                //���C���̔w�i������
                .OnComplete(() => Destroy(imgMainBackground.gameObject));

            //���b�Z�[�W�{�^���������ꂽ�ۂ̏���
            btnMessage.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //���b�Z�[�W���\������Ă��Ȃ��Ȃ�
                    if (cgMessage.alpha == 0f)
                    {
                        //���b�Z�[�W�̃L�����o�X�O���[�v��\������
                        cgMessage.alpha = 1f;

                        //�T�u�̔w�i������������
                        imgSubBackground.gameObject.SetActive(true);
                    }
                    //���b�Z�[�W���\������Ă���Ȃ�
                    else
                    {
                        //���b�Z�[�W�̃L�����o�X�O���[�v���\���ɂ���
                        cgMessage.alpha = 0f;

                        //�T�u�̔w�i��񊈐�������
                        imgSubBackground.gameObject.SetActive(false);
                    }
                })
                .AddTo(this);
        }
    }
}
