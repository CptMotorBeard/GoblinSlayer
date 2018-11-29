using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "New Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Stats")]
    public float MaximumHealth  = 100;
    public float Damage         =  10;
    public float Speed          =   3;

    [Header("Money and Points")]
    public int PointValue       =   5;
    public int CoinValueMax     =  10;
    public int CoinValueMin     =   5;    
    public int MinCoinsDropped  =   1;
    public int MaxCoinsDropped  =   3;
}
