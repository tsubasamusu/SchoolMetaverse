using System.Collections.Generic; 
using System; 
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataSO", menuName = "Create SoundDataSO")]
public class SoundDataSO : ScriptableObject
{
    /// <summary>   
    /// 音の名前   
    /// </summary>   
    public enum SoundName
    {
        ボタンを押した時の音,
        無効なボタンを押した時の音,
        歩く時の足音,
        BGM,
        メッセージ送信ボタンを押した時の音
    }

    /// <summary>   
    /// 音のデータを管理する 
    /// </summary>   
    [Serializable]
    public class SoundData
    {
        public SoundName name;//名前  
        public AudioClip clip;//クリップ 
    }

    public List<SoundData> soundDataList = new();//音のデータのリスト  
}