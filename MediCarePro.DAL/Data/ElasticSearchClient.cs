using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using MediCarePro.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nest;

namespace MediCarePro.DAL.Data
{
	public class ElasticSearchClient
	{
		private readonly ElasticClient _client;

		public ElasticSearchClient(IConfiguration configuration)
		{
			var uri = new Uri(configuration["ElasticSearch:Url"]);


			// Replace with your Elasticsearch username and password
			var username = configuration["ElasticSearch:Username"];
			var password = configuration["ElasticSearch:Password"];

			// Set up connection settings
			var settings = new ConnectionSettings(uri)
				.BasicAuthentication(username, password)
				.DefaultIndex("patients")  // Set default index
				.DefaultMappingFor<Patient>(m => m
				.IndexName("patients")  // Map index name for Patient type
				)
				.DisablePing();

			_client = new ElasticClient(settings);
		}

		public ElasticClient Client => _client;
	}
}
