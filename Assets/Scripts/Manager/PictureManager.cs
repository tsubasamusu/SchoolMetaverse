using Photon.Pun;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// �摜�Ɋւ��鏈�����s��
    /// </summary>
    public class PictureManager : MonoBehaviour, ISetUp
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        /// <summary>
        /// PictureManager�̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //�摜�𓯊�����
            this.UpdateAsObservable()
                .Where(_ => PhotonNetwork.CurrentRoom.CustomProperties["IsSettingPicture"] is bool isSettingPicture && !isSettingPicture)
                .Where(_ => PhotonNetwork.CurrentRoom.CustomProperties["PictureBites"] is byte[])
                .ThrottleFirst(System.TimeSpan.FromSeconds(ConstData.PICTURE_SYNCHRONIZE_SPAN))
                .Subscribe(_ => { SetPictureFromBytes((byte[])PhotonNetwork.CurrentRoom.CustomProperties["PictureBites"]); })
                .AddTo(this);
        }

        /// <summary>
        /// �摜�𑗐M����
        /// </summary>
        /// <param name="pictureURL">�摜��URL</param>
        public void SendPicture(string pictureURL)
        {
            //���̃v���C���[���摜��ݒ蒆�Ȃ�
            if (PhotonNetwork.CurrentRoom.CustomProperties["IsSettingPicture"] is bool isSettingPicture && isSettingPicture)
            {
                //���ʉ����Đ�����
                SoundManager.instance.PlaySound(SoundDataSO.SoundName.�G���[��\�����鎞�̉�);

                //�G���[��\������
                uiManagerMain.SetTxtSendPictureError("���̃v���C���[���摜�𑗐M���ł��B");

                //�ȍ~�̏������s��Ȃ�
                return;
            }

            //Hashtable���쐬����
            var hashtable = new ExitGames.Client.Photon.Hashtable
            {
                //�Q�[���T�[�o�[�Ɂu�摜�ݒ蒆�v�Ƃ���������������
                ["IsSettingPicture"] = true
            };

            //�쐬�����J�X�^���v���p�e�B��o�^����
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

            //�C���[�W�i�ێ��p�j
            Image imgPicture = GetImageFromURL(pictureURL);

            //�C���[�W���擾�ł��Ȃ�������A�ȍ~�̏������s��Ȃ�
            if (imgPicture == null) return;

            //�擾�����摜�̃T�C�Y���傫���Ȃ�
            if (imgPicture.Width >= ConstData.MAX_PICTURE_SIZE || imgPicture.Height >= ConstData.MAX_PICTURE_SIZE)
            {
                //���̉摜�̃T�C�Y��ύX����
                imgPicture = imgPicture
                    .GetThumbnailImage(imgPicture.Width / ConstData.DIVIDE_BIG_PICTURE_VALUE,
                    imgPicture.Height / ConstData.DIVIDE_BIG_PICTURE_VALUE,
                    delegate { return false; }, IntPtr.Zero);
            }

            //ImageConverter���쐬����
            ImageConverter imageConverter = new();

            //�C���[�W���o�C�i���f�[�^�ɕϊ�����
            byte[] bytes = (byte[])imageConverter.ConvertTo(imgPicture, typeof(byte[]));

            //�o�C�i���f�[�^����摜�����ɐݒu����
            SetPictureFromBytes(bytes);
        }

        /// <summary> 
        /// URL����C���[�W���擾���� 
        /// </summary> 
        /// <param name="pictureURL">�摜��URL</param> 
        /// <returns>�C���[�W</returns> 
        private Image GetImageFromURL(string pictureURL)
        {
            //WebClient���쐬���� 
            WebClient webClient = new();

            //Stream���쐬����
            Stream stream = null;

            //URL����Stream���擾���� 
            try { stream = webClient.OpenRead(pictureURL); }

            //URL����Stream���擾�ł��Ă��A�ł��Ȃ��Ă�
            finally
            {
                //Stream���擾�ł��Ȃ�������i������URL����͂��ꂽ��j
                if (stream == null)
                {
                    //���ʉ����Đ�����
                    SoundManager.instance.PlaySound(SoundDataSO.SoundName.�G���[��\�����鎞�̉�);

                    //�摜�̃T�C�Y���ߗp�̃X���C�_�[��񊈐�������
                    uiManagerMain.SetSldPictureSizeActive(false);

                    //�G���[��\������
                    uiManagerMain.SetTxtSendPictureError("�������摜��URL����͂��Ă��������B");

                    //Hashtable���쐬����
                    var hashtable1 = new ExitGames.Client.Photon.Hashtable
                    {
                        //�Q�[���T�[�o�[�Ɂu�摜�ݒ蒆�ł͂Ȃ��v�Ƃ���������������
                        ["IsSettingPicture"] = false
                    };

                    //�쐬�����J�X�^���v���p�e�B��o�^����
                    PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable1);
                }
            }

            //Bitmap���쐬���� 
            Bitmap bitmap = new(stream);

            //�쐬����Stream��j������ 
            stream.Close();

            //�쐬����Bitmap��Ԃ� 
            return bitmap;
        }

        /// <summary>
        /// �o�C�i���f�[�^����摜�����ɐݒu����
        /// </summary>
        /// <param name="bytes">�o�C�i���f�[�^</param>
        private void SetPictureFromBytes(byte[] bytes)
        {
            //Texture2D���쐬����
            Texture2D texture = new(1, 1);

            //�e�N�X�`�����쐬�ł��Ȃ�������
            if (!texture.LoadImage(bytes))
            {
                //���ʉ����Đ�����
                SoundManager.instance.PlaySound(SoundDataSO.SoundName.�G���[��\�����鎞�̉�);

                //�摜�̃T�C�Y���ߗp�̃X���C�_�[��񊈐�������
                uiManagerMain.SetSldPictureSizeActive(false);

                //�G���[��\������
                uiManagerMain.SetTxtSendPictureError("�摜�̃e�N�X�`�����쐬�ł��܂���ł����B\n�J���҂ɖ₢���킹�Ă��������B\nhttps://tsubasamusu.com");

                //Hashtable���쐬����
                var hashtable2 = new ExitGames.Client.Photon.Hashtable
                {
                    //�Q�[���T�[�o�[�Ɂu�摜�ݒ蒆�ł͂Ȃ��v�Ƃ���������������
                    ["IsSettingPicture"] = false
                };

                //�쐬�����J�X�^���v���p�e�B��o�^����
                PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable2);

                //�ȍ~�̏������s��Ȃ�
                return;
            }

            //�X�v���C�g���쐬����
            Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);

            //Hashtable���쐬����
            var hashtable = new ExitGames.Client.Photon.Hashtable
            {
                //�Q�[���T�[�o�[�ɉ摜�̃o�C�i���f�[�^����������
                ["PictureBites"] = bytes
            };

            //�쐬�����J�X�^���v���p�e�B��o�^����
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

            //���̃X�v���C�g��ݒ肷��
            uiManagerMain.SetImgBlackBordSprite(sprite, texture.width, texture.height);

            //Hashtable���쐬����
            var hashtable1 = new ExitGames.Client.Photon.Hashtable
            {
                //�Q�[���T�[�o�[�Ɂu�摜�ݒ蒆�ł͂Ȃ��v�Ƃ���������������
                ["IsSettingPicture"] = false
            };

            //�쐬�����J�X�^���v���p�e�B��o�^����
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable1);
        }
    }
}
