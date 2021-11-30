using MediaLakeCore.BuildingBlocks.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace MediaLakeCore.Infrastructure.Vault
{
    public class VaultConnectionFactory
    {
        private VaultOptions _options;
        private VaultClient _vaultClient;

        public VaultConnectionFactory(VaultOptions options)
        {
            _options = options;

            var authMethod = new TokenAuthMethodInfo(_options.Token);
            var vaultClientSettings = new VaultClientSettings(_options.Address, authMethod);
            _vaultClient = new VaultClient(vaultClientSettings);
        }

        public async Task<IDictionary<string, object>> GetSection<T>(string path)
        {
            Secret<SecretData> kv2Secret = await _vaultClient.V1.Secrets.KeyValue.V2
                               .ReadSecretAsync(path: path, mountPoint: _options.MountPoint);

            var secterData = kv2Secret.Data.Data;

            return secterData;
        }
    }
}
