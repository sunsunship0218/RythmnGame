using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NotesManager : MonoBehaviour
{
    //Unity動態載入 resources
    //Resources.Load( 位置+檔名 );

    // full combo 數量
    public int Total_NoteNum;
    private string songName;
    //在哪道掉落
    public List<int> TrackNum = new List<int>();
    //note種類
    public List<int> NoteType = new List<int>();
    //判定時間
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] float NoteSpeed;
    [SerializeField] GameObject noteObj;

     void OnEnable()
     {
       Total_NoteNum = 0;
        //名字要跟Json檔一樣
       songName = "USAO - Wildfire";
       Load(songName);
     }
     void Load(string Song_Name)
     {
        //解析Jason
        string inputString = Resources.Load<TextAsset>(Song_Name).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        //譜面Json中的note的長度
        Total_NoteNum = inputJson.notes.Length;

        for(int i =0; i < inputJson.notes.Length; i++)
        {
            float BeatInterval = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            //每個音符占用的時間,每節音符拍數*每拍時間間格
            float beatSec = BeatInterval * (float)inputJson.notes[i].LPB;
            //音符出現的時間點,
            float time = (beatSec * inputJson.notes[i].num/(float)inputJson.notes[i].LPB) + inputJson.offeset+0.01f;
            //放置到List儲存
            NotesTime.Add(time);
            TrackNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            //每個音符的時間資訊 * 速度
            float z = NotesTime[i] * NoteSpeed;
            //生成OBJ物件,Quaternion.identity沒有旋轉
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f , 0.55f, z), Quaternion.identity));
        }

     }
}
//反序列化做譜Jason
[Serializable]
public class Data
{
    public string name;
    public int  maxBlock;
    public int BPM;
    public int offeset;
    public Note[] notes;

}
[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;//Lines Per Beat） 拍數

}