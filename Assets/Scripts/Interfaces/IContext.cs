using Newtonsoft.Json.Linq;
using UnityEngine;

public interface IContext
{
    Rigidbody Rigidbody { get; }
    Faction Faction { get; }
    IHealth Health { get; }
    
}
