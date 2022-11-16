using AutoMapper;
using JobCandidateHub.Core.Application.Interfaces;
using JobCandidateHub.Core.Application.Services;
using JobCandidateHub.Core.Domains.Dtos;
using JobCandidateHub.Core.Domains.Entities;
using Moq;
using Xunit;
using Microsoft.Extensions.Caching.Memory;

namespace JobCandidateHub.Tests.Core
{
    public class CandidateServiceTests
    {
        private readonly ICandidateService _candidateService;
        private readonly Mock<ICandidateRepository> _mockCandidateRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IMemoryCache _memoryCache;

        public CandidateServiceTests()
        {
            _mockCandidateRepo = new Mock<ICandidateRepository>();
            _mockMapper = new Mock<IMapper>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _candidateService = new CandidateService(_mockCandidateRepo.Object, _mockMapper.Object, _memoryCache);

        }
        [Fact]
        public async Task AddOrUpdate_CandidateExists_UpdateCandidate()
        {
            // Arrange
            List<Candidate> existsCandidates = new List<Candidate>()
            {
                new Candidate()
                {
                    FirstName = "test",
                    LastName = "abc",
                    Email = "test@01.com",
                    PhoneNumber = "01231231239",
                    Comment = "xxx yyy zzz",
                }
            };

            CandidateDto candidateDto = new CandidateDto()
            {
                FirstName = "test",
                LastName = "abc",
                Email = "test@01.com",
                PhoneNumber = "01231231239",
                Comment = "xxx yyy zzz"
            };


            _mockCandidateRepo.Setup(x => x.GetAll()).ReturnsAsync(existsCandidates);
            _mockMapper.Setup(m => m.Map<Candidate, CandidateDto>(It.IsAny<Candidate>())).Returns(new CandidateDto());

            //Act
            var result = await _candidateService.AddOrUpdate(candidateDto);

            //Assert
            _mockCandidateRepo.Verify(x => x.Update(It.IsAny<Candidate>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(candidateDto.FirstName, result.FirstName);
            Assert.Equal(candidateDto.Email, result.Email);

        }

        [Fact]
        public async Task AddOrUpdate_CandidateNotExists_AddCandidate()
        {
            // Arrange
            CandidateDto candidateDto = new CandidateDto()
            {
                FirstName = "test",
                LastName = "abc",
                Email = "test@01.com",
                PhoneNumber = "01231231239",
                Comment = "xxx yyy zzz"
            };

            _mockCandidateRepo.Setup(x => x.GetCandidateByEmail(It.IsAny<string>())).ReturnsAsync((Candidate)default!);

            _mockMapper.Setup(m => m.Map<Candidate, CandidateDto>(It.IsAny<Candidate>())).Returns(new CandidateDto());

            //Act
            var result = await _candidateService.AddOrUpdate(candidateDto);

            //Assert
            _mockCandidateRepo.Verify(x => x.Add(It.IsAny<Candidate>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(candidateDto.FirstName, result.FirstName);
            Assert.Equal(candidateDto.Email, result.Email);
        }
    }
}
