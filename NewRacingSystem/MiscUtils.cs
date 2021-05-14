using GTA;
using GTA.Native;

namespace ARS
{
    public static class MiscUtils
    {
        public static bool CanWeUse(Entity entity)
        {
            return entity != null && entity.Exists();
        }

        public static bool WasCheatStringJustEntered(string cheat)
        {
            return Function.Call<bool>(Hash._0x557E43C447E700A8, Game.GenerateHash(cheat));
        }
    }
}
