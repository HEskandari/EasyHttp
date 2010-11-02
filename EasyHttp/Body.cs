﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Text;
using JsonFx.Serialization;
using JsonFx.Serialization.Providers;

namespace EasyHttp
{
    public class Body: DynamicObject
    {
        readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public string RawText { get; set; }

       
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _properties[binder.Name.ToLower()];
            return result != null;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _properties[binder.Name.ToLower()] = value;
            return true;
        }

      
    }
}