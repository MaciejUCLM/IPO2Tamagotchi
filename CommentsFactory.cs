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
            SLEEPING
        }

        private static string[] COMMENTS_EATING = {
            "Mmmm, que rica!", "Me gusta comer", "Tamagotchi está comiendo!"
        };
        private static string[] COMMENTS_PLAYING = {
            "Yaaaay!", "Bailando!", "Subeme la radio!", "Tamagotchi está jugando!"
        };
        private static string[] COMMENTS_SLEEPING = {
            "zzz", "Tamagotchi ahora se siente más descansado"
        };

        private Random mRnd;

        public CommentsFactory()
        {
            mRnd = new Random();
        }

        public string GetComment(TYPES type)
        {
            string[] set;
            if (type == TYPES.EATING)
                set = COMMENTS_EATING;
            else if (type == TYPES.SLEEPING)
                set = COMMENTS_SLEEPING;
            else
                set = COMMENTS_PLAYING;
            return set[mRnd.Next(0, set.Length)];
        }
    }
}
