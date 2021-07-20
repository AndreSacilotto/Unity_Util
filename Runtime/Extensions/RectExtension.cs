using UnityEngine;

namespace Spectra.Extensions
{
    public static class RectExtension
    {
        public static Rect KeepYH(this Rect rect, float x, float width) => new Rect(x, rect.y, width, rect.height);
        public static Rect KeepXW(this Rect rect, float y, float height) => new Rect(rect.x, y, rect.width, height);
        public static Rect KeepPosition(this Rect rect, float width, float height) => new Rect(rect.x, rect.y, width, height);
        public static Rect KeepSize(this Rect rect, float x, float y) => new Rect(x, y, rect.width, rect.height);
    }
}
