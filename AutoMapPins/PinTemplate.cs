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
        internal static PinTemplate Name(string template)
        {
            return new PinTemplate().Matching(new InstanceNameRegex(template));
        }

        internal static PinTemplate Hovr(string template)
        {
            return new PinTemplate().Matching(new HoverTextRegex(template));
        }
    }

    internal class PinTemplate
    {
        public string Label { get; private set; }
        public ObjectMatcher Matcher { get; private set; }
        public string ConfigurationKey { get; private set; }
        public bool IsGrouped { get; private set; }

        public bool IsMatch(MonoBehaviour obj)
        {
            return Matcher.IsMatch(obj);
        }
        public bool IsEnabled()
        {
            return Mod.IsEnabled(this.ConfigurationKey);
        }

        internal PinTemplate Matching(ObjectMatcher matcher)
        {
            this.Matcher = matcher;
            return this;
        }

        internal PinTemplate Lbl(string label)
        {
            this.Label = label;
            return this;
        }

        internal PinTemplate Nbl(string configurationKey)
        {
            this.ConfigurationKey = configurationKey;
            return this;
        }

        internal PinTemplate Grp()
        {
            this.IsGrouped = true;
            return this;
        }
    }

    internal abstract class ObjectMatcher
    {
        abstract public bool IsMatch(MonoBehaviour obj);
    }

    internal class InstanceNameRegex : ObjectMatcher
    {
        private Regex _pattern;

        internal InstanceNameRegex(string template)
        {
            _pattern = new Regex(template);
        }

        public override bool IsMatch(MonoBehaviour obj)
        {
            return _pattern.IsMatch(obj.name);
        }
    }

    internal class HoverTextRegex : ObjectMatcher
    {
        private Regex _pattern;

        internal HoverTextRegex(string template)
        {
            _pattern = new Regex(template);
        }

        public override bool IsMatch(MonoBehaviour obj)
        {
            var hoverTextComponent = obj.GetComponent<HoverText>();

            //if (!(bool)(Object)hoverTextComponent) return;

            string hoverText = (string)hoverTextComponent?.m_text;

            return hoverText != null && _pattern.IsMatch(hoverText);
        }
    }
}
