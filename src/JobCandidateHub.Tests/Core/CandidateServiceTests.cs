using AutoMapper;
using JobCandidateHub.Core.Application.Interfaces;
using JobCandidateHub.Core.Application.Services;
using JobCandidateHub.Core.Domains.Dtos;
using JobCandidateHub.Core.Domains.Entities;
using Moq;
using Xunit;

namespace JobCandidateHub.Tests.Core
{
    public class CandidateServiceTests
    {
        private readonly ICandidateService _candidateService;
        private readonly Mock<ICandidateRepository> _mockCandidateRepo;
        private readonly Mock<IMapper> _mockMapper;

        public CandidateServiceTests()
        {
            _mockCandidateRepo = new Mock<ICandidateRepository>();
            _mockMapper = new Mock<IMapper>();
            _candidateService = new CandidateService(_mockCandidateRepo.Object, _mockMapper.Object);

        }
        [Fact]
        public async Task AddOrUpdate_CandidateExists_UpdateCandidate()
        {
            // Arrange
            Candidate candidate = new()
            {
                FirstName = "test",
                LastName = "abc",
                Email = "test@01.com",
                PhoneNUmber = "01231231239",
                Comment = "xxx yyy zzz",
            };
            _mockCandidateRepo.Setup(x => x.GetCandidateByEmail(It.IsAny<string>())).ReturnsAsync(candidate);
            _mockMapper.Setup(m => m.Map<Candidate, CandidateDto>(It.IsAny<Candidate>())).Returns(new CandidateDto());

            //Act
            var result = await _candidateService.AddOrUpdate(new CandidateDto()
            {
                FirstName = "test",
                LastName = "abc",
                Email = "test@01.com",
                PhoneNUmber = "01231231239",
                Comment = "xxx yyy zzz"
            });

            //Assert
            _mockCandidateRepo.Verify(x => x.Update(It.IsAny<Candidate>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(candidate.FirstName, result.FirstName);
            Assert.Equal(candidate.Email, result.Email);

        }

        [Fact]
        public async Task AddOrUpdate_CandidateNotExists_AddCandidate()
        {
            // Arrange
            Candidate candidate = new()
            {
                FirstName = "test",
                LastName = "abc",
                Email = "test@01.com",
                PhoneNUmber = "01231231239",
                Comment = "xxx yyy zzz",
            };
            _mockCandidateRepo.Setup(x => x.GetCandidateByEmail(It.IsAny<string>())).ReturnsAsync((Candidate)default!);

            _mockMapper.Setup(m => m.Map<Candidate, CandidateDto>(It.IsAny<Candidate>())).Returns(new CandidateDto());

            //Act
            var result = await _candidateService.AddOrUpdate(new CandidateDto()
            {
                FirstName = "test",
                LastName = "abc",
                Email = "test@01.com",
                PhoneNUmber = "01231231239",
                Comment = "xxx yyy zzz",
            });

            //Assert
            _mockCandidateRepo.Verify(x => x.Add(It.IsAny<Candidate>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(candidate.FirstName, result.FirstName);
            Assert.Equal(candidate.Email, result.Email);
        }
    }
}
