using AutoMapPins.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using static WorldGenerator;

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
        public const double DISTANCE_MUSHROOMS = 10;
        public const double DISTANCE_OBSIDIAN = 80;

        public const string DIGITS = "\\d+";
        public const string DIGITS_BRACED_OPTIONAL = "( \\(\\d+\\))?";
        public const string CLONE = "\\(Clone\\)";

        public string Label { get; private set; }
        public ObjectMatcher Matcher { get; protected set; }
        public string ConfigurationKey { get; private set; }
        public Cat Category { get; private set; }
        public bool IsGrouped { get; protected set; }

        public bool IsMatch(MonoBehaviour obj)
        {
            return Matcher.IsMatch(obj);
        }
        public bool IsEnabled()
        {
            return Mod.IsEnabled(this);
        }

        public Type IngameType { get; protected set; }
        public double GroupingDistance { get; protected set; }
        public String SingleLabel { get => Label; protected set => Label = value; }
        public String MultipleLabel { get; protected set; }
        public Sprite NormalIcon { get; protected set; }
        public Sprite SmallerIcon { get; protected set; }
        public Biome FirstBiome { get; protected set; }
        public bool IsPersistent { get; protected set; }

        public bool IsMatchV2(MonoBehaviour obj)
        {
            if (IngameType.IsInstanceOfType(obj))
            {
                return Matcher.IsMatch(obj);
            }
            return false;
        }

        public Sprite Icon
        {
            get
            {
                if (NormalIcon != null && SmallerIcon != null)
                    return Mod.useSmallerIcons.Value ? SmallerIcon : NormalIcon;
                else
                    return Category?.Icon;
            }
        }


        #region Fluent API
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

        internal PinTemplate Nbl(Cat category)
        {
            this.Category = category;
            this.ConfigurationKey = category.Key;
            return this;
        }

        internal PinTemplate Grp()
        {
            this.IsGrouped = true;
            return this;
        }
        #endregion
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
