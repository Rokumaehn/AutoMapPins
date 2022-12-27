using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins
{
    public static class Pin
    {
        internal static PinTemplate<T> Make<T>(ObjectMatcher<T> matcher, string label = null, string enabledBy = null) where T : MonoBehaviour
        {
            return new PinTemplate<T>() { Matcher = matcher, Label = label, EnabledBy = enabledBy };
        }

        internal static ObjectMatcher<T> Name<T>(string template) where T : MonoBehaviour
        {
            return new InstanceNameRegex<T>(template);
        }

        internal static ObjectMatcher<T> Hover<T>(string template) where T : MonoBehaviour
        {
            return new HoverTextRegex<T>(template);
        }
    }

    internal class PinTemplate<T> where T : MonoBehaviour
    {
        public string Label;
        public ObjectMatcher<T> Matcher;
        public string EnabledBy;

        public bool IsMatch(T obj)
        {
            return Matcher.IsMatch(obj);
        }
        public bool IsEnabled()
        {
            return Mod.IsEnabled(this.EnabledBy);
        }
    }

    internal abstract class ObjectMatcher<T> where T : MonoBehaviour
    {
        abstract public bool IsMatch(T obj);
    }

    internal class InstanceNameRegex<T> : ObjectMatcher<T> where T : MonoBehaviour
    {
        private Regex _pattern;

        internal InstanceNameRegex(string template)
        {
            _pattern = new Regex(template);
        }

        public override bool IsMatch(T obj)
        {
            return _pattern.IsMatch(obj.name);
        }
    }

    internal class HoverTextRegex<T> : ObjectMatcher<T> where T : MonoBehaviour
    {
        private Regex _pattern;

        internal HoverTextRegex(string template)
        {
            _pattern = new Regex(template);
        }

        public override bool IsMatch(T obj)
        {
            var hoverTextComponent = obj.GetComponent<HoverText>();

            //if (!(bool)(Object)hoverTextComponent) return;

            string hoverText = (string)hoverTextComponent?.m_text;

            return hoverText != null && _pattern.IsMatch(hoverText);
        }
    }
}
