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
    public string name = "Unamed";
    public string background;
    public string sprite;
    public int hp = 1;
    public HostageType type;
    public float friction = 0.5f;
    public int mass = 2;


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
