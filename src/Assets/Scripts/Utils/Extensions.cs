using System;

namespace Assets.Scripts.Utils
{
    public static class Extensions
    {
        public static float MoveTowards(this float current, float target, float moveTowards)
        {
            if (Math.Abs(current - target) < moveTowards)
            {
                return target;
            }

            if (current > target )
            {
                return current - moveTowards;
            }
            else
            {
                return current + moveTowards;
            }
        }
    }
}
