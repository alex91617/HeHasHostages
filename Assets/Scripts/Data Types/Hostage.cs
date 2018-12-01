using UnityEngine;
//Gameplay actions, additional will be added for any game mechanic changes
public enum HostageType
{
    NORMAL
};

//Collectable hostage information
public class Hostage {
    //Variables
    public string id;
    public string name;
    public string background;
    private string sprite;
    public HostageType type;


    public Sprite getSprite()
    {
        try
        {
            return Resources.Load<Sprite>("Art/Hostages/" + sprite + ".png");
        }
        catch
        {
            return Resources.Load<Sprite>("Art/error.png");
        }
    }

    public Hostage()
    {}
    public Hostage(string spr)
    {
        this.sprite = spr;
    }
}
