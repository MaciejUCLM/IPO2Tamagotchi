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
            eating,
            playing,
            sleeping
        }

        private static string[] COMMENTS_EATING = {
            "Mmmm, que rica!", "Me gusta comer"
        };
        private static string[] COMMENTS_PLAYING = {
            "Yaaaay!", "Bailando!", "Subeme la radio!"
        };
        private static string[] COMMENTS_SLEEPING = {
            "zzz"
        };

        private Random mRnd;

        public CommentsFactory()
        {
            mRnd = new Random();
        }

        public string GetComment(TYPES type)
        {
            string[] set;
            if (type == TYPES.eating)
                set = COMMENTS_EATING;
            else if (type == TYPES.playing)
                set = COMMENTS_PLAYING;
            else
                set = COMMENTS_SLEEPING;
            return set[mRnd.Next(0, set.Length)];
        }
    }
}
