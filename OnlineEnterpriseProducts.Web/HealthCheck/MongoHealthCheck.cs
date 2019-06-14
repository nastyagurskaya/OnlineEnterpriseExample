using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using OnlineEnterprice.Data.Settings;

namespace OnlineEnterpriseProducts.Web.HealthCheck
{
    public class MongoHealthCheck : IHealthCheck
    {
        private static readonly ConcurrentDictionary<MongoClientSettings, MongoClient> _mongoClient = new ConcurrentDictionary<MongoClientSettings, MongoClient>();
        private readonly MongoClientSettings _mongoClientSettings;
        private readonly string _specifiedDatabase;
        public MongoHealthCheck(IShopDatabaseSettings settigs, string databaseName = default)
            : this (MongoClientSettings.FromUrl(MongoUrl.Create(settigs.ConnectionString)), databaseName)
        {
        }
        public MongoHealthCheck(MongoClientSettings clientSettings, string databaseName = default)
        {
            _specifiedDatabase = databaseName;
            _mongoClientSettings = clientSettings;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!_mongoClient.TryGetValue(_mongoClientSettings, out var mongoClient))
                {
                    mongoClient = new MongoClient(_mongoClientSettings);

                    if (!_mongoClient.TryAdd(_mongoClientSettings, mongoClient))
                    {
                        return new HealthCheckResult(context.Registration.FailureStatus, description: "New MongoClient can't be added into dictionary.");
                    }
                }

                if (!string.IsNullOrEmpty(_specifiedDatabase))
                {
                    // some users can't list all databases depending on database privileges, with
                    // this you can list only collection on specified database.
                    // Related with issue #43

                    await mongoClient
                        .GetDatabase(_specifiedDatabase)
                        .ListCollectionsAsync(cancellationToken: cancellationToken);
                }
                else
                {
                    await mongoClient
                        .ListDatabasesAsync(cancellationToken);
                }

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}
