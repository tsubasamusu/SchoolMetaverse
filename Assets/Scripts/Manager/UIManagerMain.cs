using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SchoolMetaverse
{
    /// <summary>
    /// ���[���Q�����UI�𐧌䂷��
    /// </summary>
    public class UIManagerMain : MonoBehaviour,ISetUp
    {
        [SerializeField]
        private Image imgMainBackground;//���C���̔w�i

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
        private CanvasGroup cgSlider;//�X���C�_�[�̃L�����o�X�O���[�v

        [SerializeField]
        private PictureManager pictureManager;//PictureManager

        /// <summary>
        /// UIManagerMain�̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //�摜���M�{�^���������ꂽ�ۂ̏���
            btnPicture.OnClickAsObservable()
                .Subscribe(_ =>pictureManager.GetPicture())
                .AddTo(this);
        }
    }
}
