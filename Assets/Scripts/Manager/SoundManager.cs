using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>  
    /// 音の処理を行う
    /// </summary>  
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private SoundDataSO soundDataSO;//SoundDataSO

        private AudioSource[] audioSources;//音再生用のAudioSourceの配列 

        public static SoundManager instance;//インスタンス  

        /// <summary>
        /// インスタンス化直後に呼び出される
        /// </summary>
        private void Awake() { if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); } }

        /// <summary>
        /// ゲーム開始直後に呼び出される
        /// </summary>
        private void Start()
        {
            //音再生用のAudioSourceの配列の要素数を設定する
            audioSources = new AudioSource[soundDataSO.soundDataList.Count];

            //音再生用のAudioSourceの配列の要素数だけ繰り返す  
            for (int i = 0; i < audioSources.Length; i++)
            {
                //AudioSorceコンポーネントを作成し、自身にアタッチした後に、配列に格納する  
                audioSources[i] = gameObject.AddComponent<AudioSource>();
            }
        }

        /// <summary> 
        /// 音のクリップを取得する 
        /// </summary> 
        /// <param name="name">音の名前</param> 
        /// <returns>音のクリップ</returns> 
        public AudioClip GetAudioClip(SoundDataSO.SoundName name)
        {
            //音のクリップを返す 
            return soundDataSO.soundDataList.Find(x => x.name == name).clip;
        }

        /// <summary>  
        /// 音を再生する  
        /// </summary>  
        /// <param name="name">音の名前</param>  
        /// <param name="volume">音のボリューム</param>  
        /// <param name="loop">繰り返すかどうか</param>  
        public void PlaySound(SoundDataSO.SoundName name, float volume = 1f, bool loop = false)
        {
            //音再生用のAudioSourceの配列の要素を1つずつ取り出す  
            foreach (AudioSource source in audioSources)
            {
                //取り出したAudioSourceが再生中ではない（使用されていない）なら  
                if (source.isPlaying == false)
                {
                    //音のクリップを登録する  
                    source.clip = GetAudioClip(name);

                    //音のボリュームを設定する  
                    source.volume = volume;

                    //繰り返すかどうかを設定する 
                    source.loop = loop;

                    //音を再生する  
                    source.Play();

                    //繰り返し処理から抜け出す 
                    break;
                }
            }
        }
    }
}