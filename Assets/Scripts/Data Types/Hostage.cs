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
    public string sprite;
    public HostageType type;


    public Sprite getSprite()
    {
        return Resources.Load<Sprite>("Art/Hostages/" + sprite);
    }

    public Hostage()
    {}
    public Hostage(string spr)
    {
        this.sprite = spr;
    }
}
