using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Gear.Infrastructure.Net.Dns
{
    /// <summary>
    /// Dns 工具类
    /// </summary>
    public sealed class DnsUtility
    {
        /// <summary>
        /// 获取本地主机名
        /// </summary>
        /// <returns>主机名称, 若不存在，则返回 null</returns>
        public static string GetLocalHostName()
        {
            try
            {
                return System.Net.Dns.GetHostName();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取本机 IP4 
        /// </summary>
        /// <returns>IP4 地址，若不存在，则返回 null</returns>
        public static string GetLocalIpAddressV4()
        {
            var hostName = GetLocalHostName();
            if (hostName == null)
                return null;

            var ipAddresses = System.Net.Dns.GetHostAddresses(hostName);
            var ipAddressV4 = ipAddresses.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return ipAddressV4 != null ? ipAddressV4.ToString() : null;
        }

        /// <summary>
        /// 异步操作，获取本机 IP4 
        /// </summary>
        /// <returns>IP4 地址，若不存在，则返回 null</returns>
        public static async Task<string> GetLocalIpAddressV4Async()
        {
            var hostName = GetLocalHostName();
            if (hostName == null)
                return await Task.FromResult((string) null);

            var ipAddresses = await System.Net.Dns.GetHostAddressesAsync(hostName);
            var ipAddressV4 = ipAddresses.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return ipAddressV4 != null ? ipAddressV4.ToString() : null;
        }
    }
}
