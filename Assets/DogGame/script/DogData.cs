
using UnityEngine;

namespace StarForce
{
	public class DogData : EntityData
	{
        [SerializeField]
        private string m_Name = null;

        public DogData(int entityId, int typeId)
            : base(entityId, typeId)
        {

        }

    }

}
