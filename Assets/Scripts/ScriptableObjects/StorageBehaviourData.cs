using System.Collections.Generic;
using UnityEngine;


namespace Game.SO
{
    [CreateAssetMenu(fileName = "StorageBehaviourData", menuName = "Behaviour/Building/StorageBehaviourData")]
    public class StorageBehaviourData : BuildingBehaviourData
    {
        public List<StorageCellDataPair> storageData;
        public bool strict;
        public int capacity;
        public override IBuildingBehaviour CreateBehaviour(IBuilding owner, IServiceProvider services)
        {
            var manager = services.Get<IResourceManager>();
            return new StorageBehaviour(storageData, capacity, strict);
        }
    }
}

