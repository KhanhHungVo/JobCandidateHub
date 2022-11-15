﻿using JobCandidateHub.Core.Application.Interfaces;
using JobCandidateHub.Core.Domains.Entities;

namespace JobCandidateHub.Infrastructure
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ICsvStorageService _csvStorageService;

        public CandidateRepository(ICsvStorageService csvStorageService)
        {
            _csvStorageService = csvStorageService;
        }

        public async Task Add(Candidate candidate)
        {
            var records = (await _csvStorageService.GetAllRecords<Candidate>()).ToList();
            records.Add(candidate);
            await _csvStorageService.AddRecords(records);
        }

        public async Task Update(Candidate candidate)
        {
            var records = await _csvStorageService.GetAllRecords<Candidate>();
            var updatedRecords = records.Select(x => x.Email == candidate.Email ? candidate : x);
            await _csvStorageService.AddRecords(updatedRecords);
        }

        public async Task<Candidate?> GetCandidateByEmail(string email)
        {
            var records = await _csvStorageService.GetAllRecords<Candidate>();
            return records.Where(x => x.Email == email).FirstOrDefault();
        }

        
    }
}
