using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    class CommentsFactory
    {
        public enum TYPES
        {
            EATING,
            PLAYING,
            SLEEPING,
            ACHIEVEMENT,
            COLLECIONABLE,
            BONUS
        }

        private static Dictionary<TYPES, string[]> COMMENTS = new Dictionary<TYPES, string[]>() {
            { TYPES.EATING, new string[] { "Mmmm, que rica comida!", "Me gusta comer", "Tamagotchi está comiendo!" } },
            { TYPES.PLAYING, new string[] { "Yaaaay!", "Bailando!", "Subeme la radio!", "Tamagotchi está jugando!" } },
            { TYPES.SLEEPING, new string[] { "zzz", "Tamagotchi ahora se siente más descansado" } },
            { TYPES.ACHIEVEMENT, new string[] { "Logro nuevo conseguido!" } },
            { TYPES.COLLECIONABLE, new string[] { "Coleccionable nuevo conseguido!" } }
            { TYPES.BONUS, new string[] { "Premio nuevo conseguido!" } }
        };

        private Random mRnd;

        public CommentsFactory()
        {
            mRnd = new Random();
        }

        public string GetComment(TYPES type)
        {
            string[] set = COMMENTS[type];
            return set[mRnd.Next(0, set.Length)];
        }
    }
}
