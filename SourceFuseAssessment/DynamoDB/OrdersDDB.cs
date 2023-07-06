using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;
using SourceFuseAssessment.Interfaces;
using SourceFuseAssessment.Models;

using System.Collections.Generic;

using System.Threading.Tasks;

using System;
using System.Linq;

namespace SourceFuseAssessment.DynamoDB
{
    public class OrdersDDB : IOrdersDDB
    {
         /// <summary>
         /// Get App Settings
         /// </summary>
         /// <param name="config"></param>
         /// <returns>Access and Secret Keys</returns>
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
        /// Get Orders from DynamoDB
        /// </summary>
        /// <param name="config"></param>
        /// <returns>Orders</returns>
        public async Task<List<Orders>> GetOrders(IConfiguration config)
        {
            try
            {
                AppSettings appSettings = GetAppSettings(config);                
                DynamoDBContextConfig ddbconfig = new DynamoDBContextConfig();
                ddbconfig.TableNamePrefix = Constant.ORDER_DDBTABLE;
                AmazonDynamoDBClient client = new AmazonDynamoDBClient(appSettings.AccessKey, appSettings.SecretKey, (Amazon.RegionEndpoint.GetBySystemName("us-east-1")));
                var context = new DynamoDBContext(client);
                var conditions = new List<ScanCondition>();
                var response = await context.ScanAsync<Orders>(conditions).GetRemainingAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Orders by passing OrderId
        /// </summary>
        /// <param name="config"></param>
        /// <param name="orderId"></param>
        /// <returns>Order</returns>
        public async Task<Orders> GetOrdersByOrderId(IConfiguration config, string orderId)
        {
            try
            {
                AppSettings appSettings = GetAppSettings(config);                
                DynamoDBContextConfig ddbconfig = new DynamoDBContextConfig();
                ddbconfig.TableNamePrefix = Constant.ORDER_DDBTABLE;
                AmazonDynamoDBClient client = new AmazonDynamoDBClient(appSettings.AccessKey, appSettings.SecretKey, (Amazon.RegionEndpoint.GetBySystemName("us-east-1")));
                var context = new DynamoDBContext(client);
                var oConfig = new DynamoDBOperationConfig { ConsistentRead = true };
                var response = (context.QueryAsync<Orders>(orderId, oConfig))?.GetRemainingAsync().Result.FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
