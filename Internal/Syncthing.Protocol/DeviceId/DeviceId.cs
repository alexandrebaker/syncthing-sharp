using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base32;

namespace Syncthing.Protocol
{
    /// <summary>
    /// Device identifier.
    /// </summary>
    public class DeviceId : IComparable<DeviceId>
    {
        private const int BytesArrayLength = 32;

        /// <summary>
        /// The local device identifier.
        /// </summary>
        public static readonly DeviceId LocalDeviceId = new DeviceId(0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                                                            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                                                            0xff, 0xff, 0xff, 0xff);

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <value>The bytes.</value>
        protected byte[] Bytes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.Protocol.DeviceId"/> class.
        /// </summary>
        /// <param name="deviceId">Device identifier.</param>
        public DeviceId(params byte[] deviceId)
        {
            if (deviceId.Length != BytesArrayLength)
                throw new ArgumentOutOfRangeException("deviceId", "Incorrect length (32) of byte slice representing device ID");

            Bytes = deviceId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.Protocol.DeviceId"/> class.
        /// </summary>
        internal DeviceId()
        {
        }

        /// <summary>
        /// UnMarshal the byte into a device id.
        /// </summary>
        /// <param name="bs">Byte representation of the device id.</param>
        public void UnMarshalText(byte[] bs)
        {
            var id = Encoding.UTF8.GetString(bs);
            id = id.Trim('=').ToUpper();
            id = DeviceIdUtils.Untypeoify(id);
            id = DeviceIdUtils.UnChukify(id);

            if (id.Length == DeviceIdUtils.LuhnifiedStringLength)
                id = DeviceIdUtils.UnLuhnify(id);    // New style, with check digits


            if (id.Length != DeviceIdUtils.UnluhnifiedStringLength)
                throw new Exception("Device Id invalid: incorrect length.");


            // Old style, no check digits
            var bytes = Base32Encoder.Decode(id + DeviceIdUtils.TrailingEqual);

            Bytes = bytes;
        }

        /// <summary>
        /// Marshal the device id into bytes.
        /// </summary>
        public byte[] MarshalText()
        {
            return Encoding.UTF8.GetBytes(ToString());
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            if (Bytes == null || Bytes.Length == 0)
                return LocalDeviceId.ToString();

            var id = Base32Encoder.Encode(Bytes);
            id = id.Trim('=');

            id = DeviceIdUtils.Luhnify(id);
            id = DeviceIdUtils.Chunkify(id);

            return id;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(DeviceId other)
        {
            for (int i = 0; i < Bytes.Length - 1; i++)
            {
                var comp = Bytes[i].CompareTo(other.Bytes[i]);
                if (comp != 0)
                    return comp;
            }
            return 0;
        }


        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        int IComparable<DeviceId>.CompareTo(DeviceId other)
        {
            return CompareTo(other);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return (Bytes != null ? Bytes.GetHashCode() : 0);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            var otherDeviceId = obj as DeviceId;
            return otherDeviceId != null && DeviceIdUtils.UnsafeCompare(this.Bytes, otherDeviceId.Bytes);
        }


        /// <summary>
        /// Determines whether the specified <see cref="T:Syncthing.Protocol.DeviceId"/> is equal to the current <see cref="T:Syncthing.Protocol.DeviceId"/>.
        /// </summary>
        /// <param name="other">The DeviceId to compare with the current DeviceId. </param>
        /// <returns></returns>
        protected bool Equals(DeviceId other)
        {
            return Equals(Bytes, other.Bytes);
        }
    }
}
