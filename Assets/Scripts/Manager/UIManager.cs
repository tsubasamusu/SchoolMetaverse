using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolMetaverse
{
    /// <summary>
    /// UI�𐧌䂷��
    /// </summary>
    public class UIManager : MonoBehaviour,ISetUp
    {
        [SerializeField]
        private Image imgBackground;//�w�i�̃C���[�W

        [SerializeField]
        private Image imgBtnMain;//���C���{�^���̃C���[�W

        [SerializeField]
        private Image imgBtnSub;//�T�u�{�^���̃C���[�W

        [SerializeField]
        private Text txtPlaceholder;//�X�y�[�X�z���_�̃e�L�X�g

        [SerializeField]
        private Text txtPlayerEntered;//�v���C���[�����͂����e�L�X�g

        [SerializeField]
        private Text txtBtnMain;//���C���{�^���̃e�L�X�g

        [SerializeField]
        private Text txtBtnSub;//�T�u�{�^���̃e�L�X�g

        [SerializeField]
        private Button btnMain;//���C���{�^��

        [SerializeField]
        private Button btnSub;//�T�u�{�^��

        [SerializeField]
        private InputField inputField;//InputField

        /// <summary>
        /// UIManager�̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //�w�i�����F�ɐݒ肷��
            imgBackground.color=Color.black;


        }
    }
}
