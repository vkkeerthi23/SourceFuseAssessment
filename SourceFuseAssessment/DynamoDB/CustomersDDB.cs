using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Configuration;
using SourceFuseAssessment.Interfaces;
using SourceFuseAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace SourceFuseAssessment.DynamoDB
{
    public class CustomersDDB : ICustomersDDB
    {
         /// <summary>
         /// Get AccessKey and Secret Key from App Settings
         /// </summary>
         /// <param name="config">Access and Secret Keys</param>
         /// <returns></returns>
        private AppSettings GetAppSettings(IConfiguration config)
        {
            var appSettings = new AppSettings
            {
                AccessKey = config.GetValue<string>("AppSettings:AccessKey"),
                SecretKey = config.GetValue<string>("AppSettings:SecretKey")
            };

            return appSettings;
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <param name="config"></param>
        /// <returns>Customers</returns>
        public async Task<List<Customers>> GetCustomers(IConfiguration config)
        {
            try
            {
                AppSettings appSettings = GetAppSettings(config);                
                DynamoDBContextConfig ddbconfig = new DynamoDBContextConfig();
                ddbconfig.TableNamePrefix = Constant.CUSTOMER_DDBTABLE;
                AmazonDynamoDBClient client = new AmazonDynamoDBClient(appSettings.AccessKey, appSettings.SecretKey, (Amazon.RegionEndpoint.GetBySystemName("us-east-1")));
                var context = new DynamoDBContext(client);
                var conditions = new List<ScanCondition>();
                var response = await context.ScanAsync<Customers>(conditions).GetRemainingAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Customer BY passing Id
        /// </summary>
        /// <param name="config"></param>
        /// <param name="customerId"></param>
        /// <returns>Customer</returns>
        public async Task<Customers> GetCustomersById(IConfiguration config, string customerId)
        {
            try
            {
                AppSettings appSettings = GetAppSettings(config);                
                DynamoDBContextConfig ddbconfig = new DynamoDBContextConfig();
                ddbconfig.TableNamePrefix = Constant.CUSTOMER_DDBTABLE; ;
                AmazonDynamoDBClient client = new AmazonDynamoDBClient(appSettings.AccessKey, appSettings.SecretKey, (Amazon.RegionEndpoint.GetBySystemName("us-east-1")));
                var context = new DynamoDBContext(client);
                var oConfig = new DynamoDBOperationConfig { ConsistentRead = true };
                var response = (context.QueryAsync<Customers>(customerId, oConfig))?.GetRemainingAsync().Result.FirstOrDefault();
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert Customers
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="SecretKey"></param>
        /// <param name="doc"></param>
        public static void InsertCustomers(string AccessKey, string SecretKey, Amazon.DynamoDBv2.DocumentModel.Document doc)
        {
            using (var client = new AmazonDynamoDBClient(AccessKey, SecretKey, (Amazon.RegionEndpoint.GetBySystemName("us-east-1"))))
            {
                Table table = Table.LoadTable(client, "Customers");
                table.PutItemAsync(doc);
            }
        }
    }
}
