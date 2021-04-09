using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tamagotchi
{
    class ItemCollecionable : Collecionable
    {
        public enum TYPES
        {
            REFILL,
            RESTORE_ENERGY,
            RESTORE_FOOD,
            RESTORE_DIVERSION,
            FREEZE_ENERGY,
            FREEZE_FOOD,
            FREEZE_DIVERSION
        }

        private static Dictionary<TYPES, string> tooltipPairs = new Dictionary<TYPES, string>() {
            { TYPES.REFILL, "Haz click para rellenar estado del Tamagotchi" },
            { TYPES.RESTORE_ENERGY, "Haz click para rellenar energia" },
            { TYPES.RESTORE_DIVERSION, "Haz click para rellenar diversion" },
            { TYPES.RESTORE_FOOD, "Haz click para rellenar alimento" },
            { TYPES.FREEZE_ENERGY, "Haz click para frenar bajando de energia" },
            { TYPES.FREEZE_DIVERSION, "Haz click para frenar bajando de diversion" },
            { TYPES.FREEZE_FOOD, "Haz click para frenar bajando de alimento" }
        };

        private TYPES mType;
        
        public TYPES Type { get => mType; }

        public ItemCollecionable(string icon, TYPES type, Action<object> clickHandler)
            : base(icon, tooltipPairs[type], clickHandler, false)
        {
            mType = type;
            if (type == TYPES.REFILL)
                OneShot = true;
        }

        public override Collecionable Copy()
        {
            return new ItemCollecionable(mPath, Type, mAction);
        }
    }
}
