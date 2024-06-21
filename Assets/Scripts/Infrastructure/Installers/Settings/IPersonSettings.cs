using UnityEngine;

public interface IPersonSettings
{
    Color Color { get; }
    string Letter { get; }
     string Name { get; }
     int Coins { get; set; }
}