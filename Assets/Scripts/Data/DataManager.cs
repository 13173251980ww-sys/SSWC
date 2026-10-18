using System.Collections.Generic;
using Data;
using UnityEngine;

public class DataManager
{
    private static DataManager _Instance;
    
    public static DataManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new DataManager();
            }
            return _Instance;
        }
    }
    
    public MusicData MusicData; 
    public List<RoleInfoData> RoleInfoDatas;
    public PlayerData PlayerData;
    public RoleInfoData NowRoleInfoData;
    public List<SceneData> SceneDatas; 
    public List<MonsterInfoData>  MonsterInfoDatas;
    public List<TowerInfo>  TowerInfoDatas;
        
    public DataManager()
    {
        MusicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        PlayerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        RoleInfoDatas = JsonMgr.Instance.LoadData<List<RoleInfoData>>("RoleInfoData");
        SceneDatas = JsonMgr.Instance.LoadData<List<SceneData>>("SceneData");
        MonsterInfoDatas = JsonMgr.Instance.LoadData<List<MonsterInfoData>>("MonsterInfo");
        TowerInfoDatas = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
    }
    
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(MusicData, "MusicData");
    }

    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(PlayerData, "PlayerData");
    }

    public void PlaySound(string name)
    {
        GameObject audioObj = new GameObject("SFX");
        AudioSource audioSource=audioObj.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Music/" + name);
        
        if (!MusicData.IsOpenSfx)
            return;
        
        audioSource.volume = MusicData.SfxVolume;
        audioSource.Play();

        Object.Destroy(audioObj,audioSource.clip.length);
    }
}
