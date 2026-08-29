using System;

namespace DefaultNamespace
{
    public static class GlobalLevelManager
    {
        private static LevelSwitcher switcher;
        private static bool loaded = false;
        public static void LoadLevel(int level)
        {
            if (loaded)
            {
                switcher.LoadLevel(level);
            }
        }

        public static void Init(LevelSwitcher _switcher)
        {
            switcher = _switcher;
            loaded = true;
        }
    }
}