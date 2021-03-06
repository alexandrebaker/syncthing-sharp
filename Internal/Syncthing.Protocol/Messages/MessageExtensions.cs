﻿using System;
using System.Linq;
using Syncthing.Protocol.v1.Messages;
using Syncthing.IO.Xdr;

namespace Syncthing.Protocol.v1.Messages
{
    /// <summary>
    /// Message extensions.
    /// </summary>
    public static class MessageExtensions
    {
        /// <summary>
        /// Validate the specified instance.
        /// </summary>
        /// <param name="instance">Instance.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void ValidateLength<T>(this T instance)
        where T : IXdrEncodable
        {
            Type t = typeof(T);
            var props = (from p in t.GetProperties()
                                  let attr = p.GetCustomAttributes(typeof(MaxLengthAttribute), true)
                                  let val = p.GetValue(instance)
                                  where attr.Length == 1 && val != null
                                  select new { PropertyName = p.Name, PropertyLength = ((dynamic)p.GetValue(instance)).Length, Attribute = attr[0] as MaxLengthAttribute})
                                 .Where(p => p.PropertyLength > p.Attribute.Length);
                                

            var error = props.FirstOrDefault();
            if (error != null)
                throw new XdrElementSizeExceeded(error.PropertyName, error.PropertyLength, (int)error.Attribute.Length);
        }
    }
}

