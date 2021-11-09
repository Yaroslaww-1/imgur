using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace MediaLakeUsers.Infrastructure.IdentityServer
{
	public class IdentityServerConfig
	{
		public static IEnumerable<ApiResource> GetApis()
		{
			return new List<ApiResource>
			{
				new ApiResource("UsersAPI", "Users API", new List<string> {
					CustomClaimTypes.Email,
					CustomClaimTypes.Name,
					CustomClaimTypes.Roles,
					CustomClaimTypes.UserId
				})
			};
		}

		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResource(CustomClaimTypes.Roles, new List<string>
				{
					CustomClaimTypes.Roles
				}),
				new IdentityResource(
					name: CustomClaimTypes.Email,
					userClaims: new[] { CustomClaimTypes.Email },
					displayName: "email"),
				new IdentityResource(
					name: CustomClaimTypes.Name,
					userClaims: new[] { CustomClaimTypes.Name },
					displayName: "name"),
				new IdentityResource(
					name: CustomClaimTypes.UserId,
					userClaims: new[] { CustomClaimTypes.UserId },
					displayName: "userid"),
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "ro.client",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					AllowOfflineAccess = true,
					AccessTokenLifetime = 300,
					SlidingRefreshTokenLifetime = 3600,
					RefreshTokenUsage = TokenUsage.OneTimeOnly,
					RefreshTokenExpiration = TokenExpiration.Sliding,
					AlwaysSendClientClaims = true,
					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					AllowedScopes =
					{
						"UsersAPI",
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile
					}
				}
			};
		}
	}
}
