using System;
using System.Data;
using System.Data.Common;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 存储提供程序工厂
    /// </summary>
    public class StorageProviderFactory : DapperStorage, IStorageProvider
    {
        protected override IDbConnection CreateInternalConnection()
        {
            return this.BuildDbProviderFactory(this.ProviderName).CreateConnection();
        }

        private DbProviderFactory BuildDbProviderFactory(string providerName)
        {
            var typeQualifuedName = StorageProviderSetting.GetTypeQualifiedName(providerName);
            return (DbProviderFactory) Activator.CreateInstance(Type.GetType(typeQualifuedName, true));
        }
    }
}
