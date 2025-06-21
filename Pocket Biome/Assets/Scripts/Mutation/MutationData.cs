using UnityEngine;

[CreateAssetMenu(fileName="Mutation", menuName="PocketBiome/Mutation")]
public class MutationData : ScriptableObject
{
    public string mutationName;
    [TextArea] public string description;
    public Sprite icon;
    public MutationType type;     

    // parametreler
    public float value = 1.2f;   
}
public enum MutationType
{
    SpreadBoost, BirthBoost, MoistLonger
}