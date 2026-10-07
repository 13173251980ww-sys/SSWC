
public class DataManager
{
    private static DataManager _instance;
    
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }
    
    public MusicData _musicData; 
        
    public DataManager()
    {
        _musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        if (_musicData == null)
        {
            _musicData = new MusicData();
        }
    }
    
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(_musicData, "MusicData");
    }
}
