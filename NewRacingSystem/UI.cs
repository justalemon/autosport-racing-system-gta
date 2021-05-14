using GTA.Native;
using GTA.Math;
using System.Drawing;

namespace ARS
{
    public static class ARSUI
    {
        public enum DrawTextAlign { Center, Left, Right }
        public enum DrawTextFont { Default, Italics, Squared }
        public static void DrawText(Vector3 pos, string t, Color c, float scale)
        {
            Vector2 screeninfo = World3DToScreen2d(pos);
            Function.Call(Hash._SET_TEXT_ENTRY, "STRING");
            Function.Call(Hash.SET_TEXT_CENTRE, true);
            Function.Call(Hash.SET_TEXT_COLOUR, c.R, c.G, c.B, c.A);
            Function.Call(Hash.SET_TEXT_SCALE, 1f, scale);
            Function.Call(Hash.SET_TEXT_DROP_SHADOW, true);
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, t);
            Function.Call(Hash._DRAW_TEXT, screeninfo.X, screeninfo.Y);
        }


        public static float DrawText(Vector2 pos, string t, Color c, DrawTextFont font, DrawTextAlign align, float scale)
        {
            Function.Call(Hash._SET_TEXT_ENTRY, "STRING");
            Function.Call(Hash.SET_TEXT_COLOUR, c.R, c.G, c.B, c.A);
            Function.Call(Hash.SET_TEXT_SCALE, 1f, scale);
            Function.Call(Hash.SET_TEXT_RIGHT_JUSTIFY, true);
            Function.Call(Hash.SET_TEXT_DROP_SHADOW, true);
            Function.Call(Hash.SET_TEXT_JUSTIFICATION, (int)align);
            Function.Call(Hash.SET_TEXT_FONT, (int)font);
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, t);
            Function.Call(Hash._DRAW_TEXT, pos.X, pos.Y);
            Function.Call(Hash._0x54CE8AC98E120CAB, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, t);

            float size = Function.Call<float>(Hash._0x85F061DA64ED2F67, 1);

            return size;
        }

        public static Vector2 World3DToScreen2d(Vector3 pos)
        {
            var x2dp = new OutputArgument();
            var y2dp = new OutputArgument();

            Function.Call<bool>(Hash._WORLD3D_TO_SCREEN2D, pos.X, pos.Y, pos.Z, x2dp, y2dp);
            return new Vector2(x2dp.GetResult<float>(), y2dp.GetResult<float>());
        }

        public static void DrawLine(Vector3 from, Vector3 to, Color color)
        {
            Function.Call(Hash.DRAW_LINE, from.X, from.Y, from.Z, to.X, to.Y, to.Z, color.R, color.G, color.B, color.A);
        }

        static public void DisplayHelpTextTimed(string text, int time)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
            Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, false, false, time);
        }

        static public void DisplayHelpText(string text)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
            Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, false, false, -1f);
        }
    }
}
