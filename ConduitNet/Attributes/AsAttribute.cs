﻿using System;

namespace Conduit.Net.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AsAttribute : ConduitAttribute
    {
        public int TypeHashCode;

        public AsAttribute(Type type) => TypeHashCode = type.GetHashCode();
        public AsAttribute(int typeHashCode) => TypeHashCode = typeHashCode;
    }
}