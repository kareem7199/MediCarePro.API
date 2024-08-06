using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using Nest;

namespace MediCarePro.DAL.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IElasticClient _client;

        public PatientRepository(ElasticSearchClient client)
        {
            _client = client.Client;
        }

        public async Task<IReadOnlyList<Patient>> SearchPatientsAsync(string searchTerm)
        {
            var searchResponse = await _client.SearchAsync<Patient>(s => s
                .Index("patients")
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            sh => sh.Wildcard(w => w
                                .Field(f => f.Name)
                                .Value($"*{searchTerm?.ToLower()}*")
                            ),
                            sh => sh.Wildcard(w => w
                                .Field(f => f.PhoneNumber)
                                .Value($"*{searchTerm?.ToLower()}*")
                            )
                        )
                    )
                )
                .Size(10)
            );

            return searchResponse.Documents.ToList();
        }
    }
}
