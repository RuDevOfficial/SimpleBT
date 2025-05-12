using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.Editor.Data
{
    // Stores all data related to the blackboard values
    [System.Serializable]
    public class BlackboardCollection
    {
        public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
        
        public BlackboardCollection() { }

        public BlackboardCollection(List<ExposedProperty> exposedProperties)
        {
            ExposedProperties = exposedProperties;
        }
    }

}
