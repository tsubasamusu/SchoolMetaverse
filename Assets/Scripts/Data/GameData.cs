using UnityEngine;

namespace SchoolMetaverse
{
    public class GameData : MonoBehaviour,ISetUp
    {
        [SerializeField]
        private Transform spawnTran;//�X�|�[���n�_

        [SerializeField]
        private GameObject objPlayerPrefab;//�v���C���[�̃v���t�@�u

        /// <summary>
        /// �u�X�|�[���n�_�v�̎擾�p
        /// </summary>
        public Transform SpawnTran { get => spawnTran; }

        /// <summary>
        /// �u�v���C���[�̃v���t�@�u�v�̎擾�p
        /// </summary>
        public GameObject ObjPlayerPrefab { get => objPlayerPrefab; }

        public static GameData instance;//�C���X�^���X 

        /// <summary>
        /// GameData�̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //TODO:GameData�N���X�̏����ݒ菈��
        }

        /// <summary> 
        /// Start���\�b�h���O�ɌĂяo����� 
        /// </summary> 
        private void Awake()
        {
            //�ȉ��A�V���O���g���ɕK�{�̋L�q 
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
