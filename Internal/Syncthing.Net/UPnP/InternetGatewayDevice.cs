using System;

namespace Syncthing.Net
{
	/// <summary>
	/// Internet gateway device.
	/// </summary>
	public class InternetGatewayDevice
	{
		/// <summary>
		/// Gets or sets the UUID.
		/// </summary>
		/// <value>The UUID.</value>
		public string Uuid { get; set; }

		/// <summary>
		/// Gets or sets the name of the friendly.
		/// </summary>
		/// <value>The name of the friendly.</value>
		public string FriendlyName { get; set; }

		/// <summary>
		/// Gets the friendly indentifier.
		/// </summary>
		/// <value>The friendly indentifier.</value>
		public string FriendlyIndentifier 
		{ 
			get { return "'" + this.FriendlyName + "'(" + this.Url.Host.Split (':') [0] + ")"; }
		}

		/// <summary>
		/// Gets or sets the services.
		/// </summary>
		/// <value>The services.</value>
		public InternetGatewayDeviceService[] Services { get; set; }

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>The URL.</value>
		public Uri Url{ get; set; }

		/// <summary>
		/// Gets or sets the local address.
		/// </summary>
		/// <value>The local address.</value>
		public string LocalAddress { get; set; }
	}
}

