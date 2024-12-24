[System.Serializable]
public class PlayerData
{
    public int totalCoin;
    public bool hasGoblin;
    public bool hasDevil;
    public bool hasSanta;
    public bool hasSnowMan;
    public bool hasDoggo;
    public bool hasCate;
    public bool hasReindeer;
    public bool hasBlackCat;

    public PlayerData(int totalCoin, bool hasGoblin, bool hasDevil, bool hasSanta, bool hasSnowMan, bool hasDoggo, bool hasCate, bool hasReindeer, bool hasBlackCat)
    {
        this.totalCoin = totalCoin;
        this.hasGoblin = hasGoblin;
        this.hasDevil = hasDevil;
        this.hasSanta = hasSanta;
        this.hasSnowMan = hasSnowMan;
        this.hasDoggo = hasDoggo;
        this.hasCate = hasCate;
        this.hasReindeer = hasReindeer;
        this.hasBlackCat = hasBlackCat;
    }
}
